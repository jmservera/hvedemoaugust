import test from "node:test";
import assert from "node:assert/strict";
import { mkdtemp, rm } from "node:fs/promises";
import { tmpdir } from "node:os";
import { join } from "node:path";
import { WorkspaceStore, initialState, saveWorkspace, createJob, completeJob, interruptJob, taskPrompt, confluenceUrl, digest } from "./workspace.mjs";
import { startServer } from "./server.mjs";

const source = { title: "Meeting evidence", url: "https://example.atlassian.net/wiki/spaces/PROD/pages/123", pageId: "123", version: 2, kind: "transcript" };
const configured = () => ({ ...initialState("test"), confluenceUrl: source.url });
const finish = (state, job, input = {}) => completeJob(state, { jobId: job.id, status: "completed", summary: "Verified evidence", ...input });

test("only stable safe workspace IDs and HTTPS Confluence URLs are accepted", () => {
    const store = new WorkspaceStore(tmpdir());
    assert.throws(() => store.path("../escape"), /Invalid workspace/);
    for (const url of ["http://example.com/wiki", "https://user:pass@example.com/wiki", "javascript:alert(1)", "https://example.com/"]) {
        assert.throws(() => confluenceUrl(url));
    }
    assert.equal(confluenceUrl(source.url), source.url);
});

test("working state persists across stores and panels; updates serialize", async (t) => {
    const root = await mkdtemp(join(tmpdir(), "product-workspace-"));
    t.after(() => rm(root, { recursive: true, force: true }));
    const store = new WorkspaceStore(root);
    await Promise.all([store.update("test", (state) => { state.goal = "one"; }), store.update("test", (state) => { state.name = "two"; })]);
    const state = await new WorkspaceStore(root).read("test");
    assert.equal(state.revision, 2);
    assert.equal(state.goal, "one");
    assert.equal(state.name, "two");
    assert.deepEqual(await store.read("other"), initialState("other"));
    await assert.rejects(store.update("test", () => { throw new Error("Do not save"); }));
    assert.equal((await store.read("test")).revision, 2);
    await store.update("test", (draft) => { draft.goal = "recovered"; });
    assert.equal((await store.read("test")).goal, "recovered");
});

test("draft saving rejects stale revisions and invalidates only changed reviews", () => {
    const state = configured();
    state.documents[0].review = { hash: digest("") };
    assert.throws(() => saveWorkspace(state, { revision: 99, type: "BRD", content: "lost" }), /changed/);
    saveWorkspace(state, { revision: 0, type: "BRD", content: "" });
    assert.ok(state.documents[0].review);
    saveWorkspace(state, { revision: 0, type: "BRD", content: "# Business outcomes" });
    assert.equal(state.documents[0].review, null);
    state.connection.status = "verified";
    saveWorkspace(state, { revision: 0, name: "Product", goal: "", confluenceUrl: "https://example.atlassian.net/wiki/spaces/OTHER" });
    assert.equal(state.connection.status, "unverified");
});

test("authoring requires source evidence and pending jobs cannot duplicate", () => {
    const state = configured();
    assert.throws(() => createJob(state, { task: "build", type: "PRD" }), /transcript/);
    assert.throws(() => createJob(state, { task: "build", type: "ADR" }), /source/);
    state.sources.push(source);
    const job = createJob(state, { task: "build", type: "PRD" });
    assert.throws(() => createJob(state, { task: "review", type: "PRD" }), /pending/);
    assert.match(taskPrompt(state, job, "panel-1"), /hve-core:prd-builder/);
    finish(state, job, { content: "# Product\n\nEvidence-based draft" });
    assert.match(state.documents[1].content, /Evidence/);
});

test("connection/import completion requires page provenance; blocked is never connected", () => {
    const state = configured(), job = createJob(state, { task: "connect" });
    assert.throws(() => finish(state, job), /evidence/);
    finish(state, job, { sources: [source] });
    assert.equal(state.connection.status, "verified");
    assert.equal(state.sources[0].version, 2);
    assert.ok(state.sources[0].verifiedAt);
    assert.throws(() => finish(state, job, { sources: [source] }), /pending/);
    const retry = createJob(state, { task: "connect" });
    completeJob(state, { jobId: retry.id, status: "blocked", summary: "Atlassian authentication required" });
    assert.equal(state.connection.status, "unverified");
});

test("app-configured connections work without a pasted URL or invented page reads", () => {
    const state = initialState("test"), job = createJob(state, { task: "connect" });
    assert.match(taskPrompt(state, job, "panel"), /currently configured in this app/);
    finish(state, job, { connection: { url: "https://example.atlassian.net/wiki", cloudId: "site-id" } });
    assert.equal(state.connection.status, "available");
    assert.equal(state.connection.cloudId, "site-id");
    assert.equal(state.sources.length, 0);
    assert.equal(state.confluenceUrl, "https://example.atlassian.net/wiki");
});

test("review and publication are content-bound; publication requires confirmation and tool evidence", () => {
    const state = configured();
    state.documents[0].content = "# BRD";
    assert.throws(() => createJob(state, { task: "publish", type: "BRD" }), /confirmation/);
    assert.throws(() => createJob(state, { task: "publish", type: "BRD", confirm: true }), /review/);
    const review = createJob(state, { task: "review", type: "BRD" });
    finish(state, review);
    const publish = createJob(state, { task: "publish", type: "BRD", confirm: true });
    assert.match(taskPrompt(state, publish, "panel"), /obtain explicit approval before any write/);
    assert.throws(() => finish(state, publish), /Publication requires/);
    finish(state, publish, { sources: [{ ...source, kind: "BRD" }] });
    assert.equal(state.documents[0].published.hash, digest("# BRD"));
    saveWorkspace(state, { revision: 0, type: "BRD", content: "# Revised" });
    assert.throws(() => createJob(state, { task: "publish", type: "BRD", confirm: true }), /review/);
});

test("late agent results cannot overwrite human document changes", () => {
    const state = configured();
    state.sources.push(source);
    const job = createJob(state, { task: "build", type: "BRD" });
    saveWorkspace(state, { revision: 0, type: "BRD", content: "Human change" });
    assert.throws(() => finish(state, job, { content: "Agent change" }), /changed/);
    completeJob(state, { jobId: job.id, status: "blocked", summary: "Draft changed during authoring" });
    assert.equal(state.documents[0].content, "Human change");
});

test("interrupted requests can be recovered without allowing late results to overwrite state", () => {
    const state = configured(), job = createJob(state, { task: "connect" });
    assert.throws(() => interruptJob(state, { jobId: job.id }), /Confirm/);
    interruptJob(state, { jobId: job.id, confirm: true });
    assert.equal(job.status, "blocked");
    assert.throws(() => finish(state, job, { sources: [source] }), /pending/);
    assert.ok(createJob(state, { task: "connect" }));
});

test("import returns full document content with provenance", () => {
    const state = configured();
    const job = createJob(state, { task: "import", url: source.url, type: "PRD", kind: "PRD" });
    assert.throws(() => finish(state, job, { sources: [source] }), /content/);
    finish(state, job, { content: "# Product requirements", sources: [{ ...source, kind: "PRD" }] });
    assert.equal(state.documents[1].content, "# Product requirements");
});

test("HTTP canvas supports state, durable edits, safe rendering and real SDK submission", async (t) => {
    const root = await mkdtemp(join(tmpdir(), "product-http-"));
    const store = new WorkspaceStore(root), messages = [], errors = [];
    const session = { send: async (input) => { messages.push(input); return "message-1"; }, log: async (message) => errors.push(message) };
    const entry = await startServer({ store, session, workspaceId: "test", instanceId: "panel-1" });
    t.after(async () => { entry.server.closeAllConnections(); await new Promise((resolve) => entry.server.close(resolve)); await rm(root, { recursive: true, force: true }); });
    const url = new URL(entry.url), token = url.search;
    const request = (path, input, origin = url.origin) => fetch(`${url.origin}${path}${token}`, input === undefined ? {} : { method: "POST", headers: { "Content-Type": "application/json", Origin: origin }, body: JSON.stringify(input) });
    assert.equal((await fetch(url.origin)).status, 403);
    assert.equal((await request("/save", { revision: 0, type: "BRD", content: "attack" }, "https://evil.example")).status, 400);
    const page = await (await fetch(entry.url)).text();
    assert.match(page, /Confluence &amp; sources/);
    assert.match(page, /textContent/);
    assert.doesNotMatch(page, /\.innerHTML\s*=/);
    const scripts = [...page.matchAll(/<script>([\s\S]*?)<\/script>/g)];
    for (const script of scripts) assert.doesNotThrow(() => new Function(script[1]));
    let response = await request("/save", { revision: 0, name: "Product", goal: "Outcome", confluenceUrl: source.url });
    assert.equal(response.status, 200);
    response = await request("/run", { task: "connect" });
    const state = await response.json();
    assert.equal(state.jobs[0].status, "submitted");
    assert.equal(state.jobs[0].messageId, "message-1");
    assert.equal(messages[0].mode, "enqueue");
    assert.match(messages[0].prompt, /complete_job/);
    assert.equal((await (await request("/state")).json()).name, "Product");
    assert.ok(errors.length > 0);
});

test("SDK submission failures are persisted and visibly returned", async (t) => {
    const root = await mkdtemp(join(tmpdir(), "product-fail-")), store = new WorkspaceStore(root);
    await store.update("test", (state) => { state.confluenceUrl = source.url; });
    const entry = await startServer({ store, session: { send: async () => { throw new Error("Disconnected"); }, log: async () => {} }, workspaceId: "test", instanceId: "panel" });
    t.after(async () => { entry.server.closeAllConnections(); await new Promise((resolve) => entry.server.close(resolve)); await rm(root, { recursive: true, force: true }); });
    const url = new URL(entry.url);
    const response = await fetch(`${url.origin}/run${url.search}`, { method: "POST", headers: { Origin: url.origin, "Content-Type": "application/json" }, body: JSON.stringify({ task: "connect" }) });
    assert.equal(response.status, 400);
    assert.match((await response.json()).error, /Disconnected/);
    assert.equal((await store.read("test")).jobs[0].status, "failed");
});
