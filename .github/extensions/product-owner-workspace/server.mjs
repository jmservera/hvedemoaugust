import { createServer } from "node:http";
import { readFile } from "node:fs/promises";
import { randomBytes } from "node:crypto";
import { saveWorkspace, createJob, interruptJob, taskPrompt } from "./workspace.mjs";

async function body(req) {
    if (!req.headers["content-type"]?.startsWith("application/json")) throw new Error("JSON content is required.");
    let size = 0;
    const chunks = [];
    for await (const chunk of req) {
        size += chunk.length;
        if (size > 600000) throw new Error("Request is too large.");
        chunks.push(chunk);
    }
    return JSON.parse(Buffer.concat(chunks).toString("utf8"));
}

export async function startServer({ store, session, workspaceId, instanceId }) {
    const token = randomBytes(24).toString("hex");
    const template = await readFile(new URL("./workspace.html", import.meta.url), "utf8");
    let origin;
    const server = createServer(async (req, res) => {
        res.setHeader("Cache-Control", "no-store");
        res.setHeader("X-Content-Type-Options", "nosniff");
        res.setHeader("Content-Security-Policy", "default-src 'none'; script-src 'unsafe-inline'; style-src 'unsafe-inline'; connect-src 'self'; img-src 'self'; base-uri 'none'; form-action 'none'");
        try {
            if (req.headers.host !== new URL(origin).host) throw new Error("Invalid host.");
            const url = new URL(req.url, origin);
            if (req.method === "GET" && url.pathname === "/favicon.ico") {
                res.writeHead(204).end();
                return;
            }
            if (url.searchParams.get("token") !== token) {
                res.writeHead(403).end("This workspace link is not authorized.");
                return;
            }
            if (req.method === "GET" && url.pathname === "/") {
                res.setHeader("Content-Type", "text/html; charset=utf-8");
                res.end(template);
                return;
            }
            res.setHeader("Content-Type", "application/json; charset=utf-8");
            if (req.method === "GET" && url.pathname === "/state") {
                res.end(JSON.stringify(await store.read(workspaceId)));
                return;
            }
            if (req.method !== "POST" || req.headers.origin !== origin) throw new Error("Only same-origin workspace requests are allowed.");
            const input = await body(req);
            let state;
            if (url.pathname === "/save") {
                state = await store.update(workspaceId, (draft) => saveWorkspace(draft, input));
            } else if (url.pathname === "/interrupt") {
                state = await store.update(workspaceId, (draft) => interruptJob(draft, input));
            } else if (url.pathname === "/run") {
                let job;
                state = await store.update(workspaceId, (draft) => { job = createJob(draft, input); });
                try {
                    const messageId = await session.send({ prompt: taskPrompt(state, job, instanceId), mode: "enqueue" });
                    state = await store.update(workspaceId, (draft) => {
                        const saved = draft.jobs.find((item) => item.id === job.id);
                        if (saved.status === "submitting") {
                            saved.status = "submitted";
                            saved.summary = "Sent to Copilot. Continue questions and approvals in chat.";
                        }
                        saved.messageId = messageId;
                    });
                } catch (error) {
                    await store.update(workspaceId, (draft) => {
                        const saved = draft.jobs.find((item) => item.id === job.id);
                        if (saved.status === "submitting") {
                            saved.status = "failed";
                            saved.summary = `Copilot submission failed: ${error.message}`;
                        }
                    });
                    throw error;
                }
            } else {
                res.writeHead(404).end(JSON.stringify({ error: "Unknown workspace endpoint." }));
                return;
            }
            res.end(JSON.stringify(state));
        } catch (error) {
            await session.log(`Product workspace: ${error.message}`, { level: "error" });
            if (!res.headersSent) res.writeHead(400, { "Content-Type": "application/json; charset=utf-8" });
            res.end(JSON.stringify({ error: error.message }));
        }
    });
    server.requestTimeout = 30000;
    await new Promise((resolve, reject) => {
        server.once("error", reject);
        server.listen(0, "127.0.0.1", resolve);
    });
    origin = `http://127.0.0.1:${server.address().port}`;
    return { workspaceId, server, url: `${origin}/?token=${token}` };
}
