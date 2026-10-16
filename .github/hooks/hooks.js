#!/usr/bin/env node

// Passive logger: appends every hook event, plus agent responses and user answers
// read from the session transcript, to one append-only JSONL file. It never writes
// to stdout, never reads configuration, and always exits 0 so it cannot change agent behavior.

const fs = require('node:fs/promises');
const fsSync = require('node:fs');
const path = require('node:path');
const { randomUUID } = require('node:crypto');

const LOG_FILE = path.join(__dirname, '..', '..', 'logs', 'hooks.jsonl');
const LOCK_FILE = `${LOG_FILE}.lock`;

const STARTED = Date.now();
const WATCHDOG_MS = 4000; // Host timeout is 5s.
const LOCK_DEADLINE = STARTED + 3000;
const LOCK_STALE_MS = 2000;
const INGEST_DEADLINE = STARTED + 3000;
const CURSOR_SCAN_CHUNK = 1024 * 1024;
const CURSOR_SCAN_MAX = 64 * 1024 * 1024;
const TRANSCRIPT_READ_MAX = 16 * 1024 * 1024;

let heldToken = null;

function warn(message) {
  process.stderr.write(`[hook-logger] ${message}\n`);
}

function sleep(ms) {
  return new Promise((resolve) => setTimeout(resolve, ms));
}

// Finish before the host's timeout, even if stdin or disk I/O stalls.
const watchdog = setTimeout(() => {
  warn('Logging timed out; this event may be missing or incomplete. Agent execution will continue.');
  process.exit(0);
}, WATCHDOG_MS);

// Also covers the watchdog path, so a stalled run does not leave the lock behind.
process.on('exit', releaseLockSync);

function releaseLockSync() {
  if (!heldToken) {
    return;
  }
  try {
    // Only remove the lock if it is still ours (it may have been broken as stale).
    if (fsSync.readFileSync(LOCK_FILE, 'utf8') === heldToken) {
      fsSync.unlinkSync(LOCK_FILE);
    }
  } catch {
    // Lock already gone.
  }
  heldToken = null;
}

async function breakStaleLock() {
  try {
    const { mtimeMs } = await fs.stat(LOCK_FILE);
    if (Date.now() - mtimeMs > LOCK_STALE_MS) {
      // Rename is atomic, so only one waiter removes the stale lock.
      const stale = `${LOCK_FILE}.stale-${randomUUID()}`;
      await fs.rename(LOCK_FILE, stale);
      await fs.rm(stale, { force: true });
    }
  } catch {
    // Lock released or already broken by another waiter.
  }
}

// Async lock: exclusive creation of a lock file with jittered retries and a bounded wait.
async function acquireLock() {
  const token = `${process.pid}:${randomUUID()}`;
  for (;;) {
    try {
      await fs.writeFile(LOCK_FILE, token, { flag: 'wx' });
      heldToken = token;
      return true;
    } catch (error) {
      if (!['EEXIST', 'EPERM', 'EBUSY', 'EACCES'].includes(error.code)) {
        throw error;
      }
    }
    await breakStaleLock();
    if (Date.now() >= LOCK_DEADLINE) {
      return false;
    }
    await sleep(10 + Math.random() * 40);
  }
}

async function readStdin() {
  const chunks = [];
  for await (const chunk of process.stdin) {
    chunks.push(chunk);
  }
  return Buffer.concat(chunks);
}

function buildHookRecord(input) {
  const text = input.toString('utf8');
  let payload;
  try {
    payload = JSON.parse(text);
  } catch {
    warn('Invalid JSON on stdin; preserving raw input under Unknown.');
  }
  const isObject = payload !== null && typeof payload === 'object' && !Array.isArray(payload);
  if (payload !== undefined && !isObject) {
    warn('Expected an event object; preserving raw input under Unknown.');
  }
  const base = { ts: new Date().toISOString(), source: 'hook' };
  if (!isObject) {
    return { ...base, event: 'Unknown', session_id: null, raw: text };
  }
  const name = typeof payload.hook_event_name === 'string' && /^[A-Za-z][A-Za-z0-9]*$/.test(payload.hook_event_name)
    ? payload.hook_event_name
    : 'Unknown';
  if (name === 'Unknown' && payload.hook_event_name !== 'Unknown') {
    warn('Missing or invalid hook_event_name; using Unknown.');
  }
  return {
    ...base,
    event: name,
    session_id: typeof payload.session_id === 'string' && payload.session_id ? payload.session_id : null,
    payload,
  };
}

// The cursor (last transcript byte consumed) lives in the log itself, on the hook record
// written with each ingested batch, so no state file is needed.
async function findCursor(transcriptPath) {
  let handle;
  try {
    handle = await fs.open(LOG_FILE, 'r');
  } catch {
    return 0;
  }
  try {
    const marker = Buffer.from('"transcript_cursor":');
    const matches = (line) => {
      if (!line.includes(marker)) {
        return undefined;
      }
      try {
        const cursor = JSON.parse(line.toString('utf8')).transcript_cursor;
        if (cursor && cursor.path === transcriptPath && Number.isSafeInteger(cursor.offset)) {
          return cursor.offset;
        }
      } catch {
        // Partial or foreign line.
      }
      return undefined;
    };

    let position = (await handle.stat()).size;
    let scanned = 0;
    let carry = Buffer.alloc(0);
    while (position > 0 && scanned < CURSOR_SCAN_MAX) {
      const length = Math.min(CURSOR_SCAN_CHUNK, position);
      position -= length;
      const chunk = Buffer.alloc(length);
      await handle.read(chunk, 0, length, position);
      scanned += length;
      const data = Buffer.concat([chunk, carry]);
      let end = data.length;
      for (;;) {
        const newline = end > 0 ? data.lastIndexOf(0x0a, end - 1) : -1;
        if (newline === -1) {
          break;
        }
        const found = matches(data.subarray(newline + 1, end));
        if (found !== undefined) {
          return found;
        }
        end = newline;
      }
      carry = Buffer.from(data.subarray(0, end));
    }
    if (position === 0) {
      const found = matches(carry);
      if (found !== undefined) {
        return found;
      }
    }
    return 0;
  } catch {
    return 0;
  } finally {
    await handle.close();
  }
}

function transcriptRecord(sessionId, event, data) {
  return {
    ts: typeof event.timestamp === 'string' ? event.timestamp : new Date().toISOString(),
    source: 'transcript',
    event: event.type,
    session_id: sessionId,
    event_id: event.id,
    data,
  };
}

function pick(source, keys) {
  const picked = {};
  for (const key of keys) {
    if (source[key] !== undefined) {
      picked[key] = source[key];
    }
  }
  return picked;
}

// Keeps what the hooks do not carry: assistant text, the user's message, and the
// ask_user question and the user's answer. Large or opaque fields are dropped.
function toTranscriptRecord(sessionId, line) {
  const type = /^\{"type":"([^"]+)"/.exec(line.slice(0, 80))?.[1];
  const wanted = type === 'user.message'
    || type === 'assistant.message'
    || (type === 'tool.execution_start' && line.includes('"toolName":"ask_user"'))
    || (type === 'tool.execution_complete' && line.includes('"elicitation_action"'));
  if (!wanted) {
    return undefined;
  }
  let event;
  try {
    event = JSON.parse(line);
  } catch {
    return undefined;
  }
  const data = event.data || {};
  switch (type) {
    case 'user.message':
      return transcriptRecord(sessionId, event, pick(data, ['content', 'interactionId', 'turnId', 'source']));
    case 'assistant.message': {
      if (typeof data.content !== 'string' || data.content === '') {
        return undefined; // Tool-call-only turns are already covered by PreToolUse and PostToolUse.
      }
      const kept = pick(data, ['content', 'messageId', 'turnId', 'interactionId', 'parentToolCallId', 'model']);
      if (Array.isArray(data.toolRequests)) {
        kept.toolRequests = data.toolRequests.map((request) => ({ toolCallId: request.toolCallId, name: request.name }));
      }
      return transcriptRecord(sessionId, event, kept);
    }
    case 'tool.execution_start':
      return transcriptRecord(sessionId, event, {
        ...pick(data, ['toolCallId', 'toolName', 'turnId']),
        question: data.arguments,
      });
    default:
      return transcriptRecord(sessionId, event, {
        ...pick(data, ['toolCallId', 'success', 'turnId']),
        answer: data.result,
        elicitation_action: data.toolTelemetry?.properties?.elicitation_action,
      });
  }
}

// Returns the new transcript records and the cursor to store, or undefined when there is nothing new.
async function collectTranscript(sessionId, transcriptPath) {
  let handle;
  try {
    handle = await fs.open(transcriptPath, 'r');
  } catch {
    return undefined;
  }
  try {
    const { size } = await handle.stat();
    let offset = await findCursor(transcriptPath);
    if (offset > size) {
      offset = 0; // Transcript was replaced or truncated.
    }
    if (offset >= size) {
      return undefined;
    }
    const length = Math.min(size - offset, TRANSCRIPT_READ_MAX);
    const buffer = Buffer.alloc(length);
    const { bytesRead } = await handle.read(buffer, 0, length, offset);
    const lastNewline = bytesRead > 0 ? buffer.lastIndexOf(0x0a, bytesRead - 1) : -1;
    let consumed;
    if (lastNewline !== -1) {
      consumed = lastNewline + 1;
    } else if (bytesRead === TRANSCRIPT_READ_MAX) {
      consumed = bytesRead; // One oversized line; skip it rather than stall forever.
    } else {
      return undefined; // Last line is still being written; pick it up on a later event.
    }
    const records = [];
    for (const line of buffer.subarray(0, consumed).toString('utf8').split('\n')) {
      const record = line ? toTranscriptRecord(sessionId, line) : undefined;
      if (record) {
        records.push(record);
      }
    }
    return { records, cursor: { path: transcriptPath, offset: offset + consumed } };
  } catch (error) {
    warn(`Unable to read transcript (${error.code || error.message}).`);
    return undefined;
  } finally {
    await handle.close();
  }
}

function toLines(records) {
  return Buffer.from(records.map((record) => `${JSON.stringify(record)}\n`).join(''), 'utf8');
}

async function logEvent() {
  const hookRecord = buildHookRecord(await readStdin());
  await fs.mkdir(path.dirname(LOG_FILE), { recursive: true });

  let locked = false;
  try {
    locked = await acquireLock();
  } catch (error) {
    warn(`Lock unavailable (${error.code || error.message}); appending without it.`);
  }
  if (!locked) {
    warn('Could not acquire the log lock in time; appending the hook record without transcript ingestion.');
    await fs.appendFile(LOG_FILE, toLines([hookRecord]));
    return;
  }

  try {
    const records = [hookRecord];
    const transcriptPath = hookRecord.payload?.transcript_path;
    if (hookRecord.session_id && typeof transcriptPath === 'string' && transcriptPath && Date.now() < INGEST_DEADLINE) {
      const transcript = await collectTranscript(hookRecord.session_id, transcriptPath);
      if (transcript) {
        hookRecord.transcript_cursor = transcript.cursor;
        records.push(...transcript.records);
      }
    }
    // One write for the whole batch keeps the hook record, its transcript records, and its cursor together.
    await fs.appendFile(LOG_FILE, toLines(records));
  } finally {
    releaseLockSync();
  }
}

logEvent().then(
  () => clearTimeout(watchdog),
  (error) => {
    clearTimeout(watchdog);
    warn(`Unable to record event (${error.code || error.message}). Agent execution will continue.`);
    process.exit(0);
  },
);
