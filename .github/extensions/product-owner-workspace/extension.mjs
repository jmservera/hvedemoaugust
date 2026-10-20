import { joinSession, createCanvas, CanvasError } from "@github/copilot-sdk/extension";
import { WorkspaceStore, completeJob } from "./workspace.mjs";
import { startServer } from "./server.mjs";

const servers = new Map();
let store;
const workspaceInput = {
    type: "object", additionalProperties: false,
    properties: { workspaceId: { type: "string", pattern: "^[a-z0-9][a-z0-9-]{0,63}$" } },
    required: ["workspaceId"],
};

const session = await joinSession({
    canvases: [createCanvas({
        id: "product-owner-workspace",
        displayName: "Product workspace",
        description: "Navigate BRDs, PRDs and ADRs, connect Confluence sources, and request HVE authoring or review.",
        inputSchema: workspaceInput,
        actions: [
            {
                name: "get_workspace",
                description: "Read documents, source provenance and agent activity for a workspace.",
                inputSchema: workspaceInput,
                handler: (ctx) => store.read(ctx.input.workspaceId),
            },
            {
                name: "complete_job",
                description: "Record an agent task outcome using verified tool evidence, document content and Confluence provenance.",
                inputSchema: {
                    type: "object", additionalProperties: false,
                    properties: {
                        workspaceId: workspaceInput.properties.workspaceId,
                        jobId: { type: "string" },
                        status: { enum: ["completed", "blocked", "failed"] },
                        summary: { type: "string", minLength: 1, maxLength: 8000 },
                        content: { type: "string", maxLength: 500000 },
                        connection: {
                            type: "object", additionalProperties: false,
                            properties: {
                                url: { type: "string", maxLength: 2000 },
                                cloudId: { type: "string", minLength: 1, maxLength: 200 },
                            },
                            required: ["url", "cloudId"],
                        },
                        sources: {
                            type: "array", maxItems: 30,
                            items: {
                                type: "object", additionalProperties: false,
                                properties: {
                                    title: { type: "string", minLength: 1, maxLength: 500 },
                                    url: { type: "string", maxLength: 2000 },
                                    pageId: { type: "string", minLength: 1, maxLength: 100 },
                                    version: { type: "integer", minimum: 1 },
                                    kind: { enum: ["transcript", "BRD", "PRD", "ADR", "reference"] },
                                },
                                required: ["title", "url", "pageId", "version", "kind"],
                            },
                        },
                    },
                    required: ["workspaceId", "jobId", "status", "summary"],
                },
                handler: async (ctx) => {
                    try {
                        return await store.update(ctx.input.workspaceId, (state) => completeJob(state, ctx.input));
                    } catch (error) {
                        throw new CanvasError("workspace_update_failed", error.message);
                    }
                },
            },
        ],
        open: async (ctx) => {
            await store.read(ctx.input.workspaceId);
            let entry = servers.get(ctx.instanceId);
            if (entry && entry.workspaceId !== ctx.input.workspaceId) {
                throw new CanvasError("workspace_mismatch", "Open a new panel for a different workspace.");
            }
            if (!entry) {
                entry = await startServer({ store, session, workspaceId: ctx.input.workspaceId, instanceId: ctx.instanceId });
                servers.set(ctx.instanceId, entry);
            }
            return { title: "Product workspace", url: entry.url };
        },
        onClose: async (ctx) => {
            const entry = servers.get(ctx.instanceId);
            if (entry) {
                servers.delete(ctx.instanceId);
                entry.server.closeAllConnections();
                await new Promise((resolve) => entry.server.close(resolve));
            }
        },
    })],
});

if (!session.workspacePath) throw new Error("Copilot session artifact storage is unavailable.");
store = new WorkspaceStore(session.workspacePath);
