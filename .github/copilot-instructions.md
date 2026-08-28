---
title: Repository Copilot Instructions
description: Repository-wide source-of-truth, traceability, and approval rules for Copilot agents
---

## Authoritative Sources

* Treat Confluence as the source of truth for project documentation, meeting transcripts, BRDs, PRDs, and ADRs.
* Before drafting, changing, or implementing requirements or architecture decisions, read the relevant Confluence pages through the `atlassian-mcp-server` MCP integration.
* Start BRDs and PRDs from meeting transcripts stored in Confluence. For local demos, generate the PRD locally from the transcript without requiring an approved Confluence PRD. Clearly distinguish recorded decisions from assumptions, and preserve each source page URL, page ID, and version in generated artifacts.
* Publishing approved BRDs, PRDs, ADRs, and durable documentation to Confluence is recommended for shared or production work but is not required for local demos. Treat locally generated documents as non-authoritative working artifacts unless a Confluence page explicitly identifies them as authoritative.
* Treat Jira as the source of truth for work items and delivery status.

## Development Traceability

* When a Jira issue exists, include its key, such as `PROJ-123`, in commit messages and pull request titles so GitHub for Jira can link the activity to the work item.
* When available, include a direct Jira work item link and links to supporting Confluence pages in pull request descriptions.
* Creating a Jira issue before local development, commits, or pull requests is recommended but not mandatory. Do not block a local demo when no Jira issue is available.

## Evidence, Approval, and Security

* Do not invent missing source material or report that a Confluence, Jira, or GitHub operation succeeded without tool evidence.
* Ask for clarification when authoritative information is missing or conflicting.
* Obtain human review before publishing documentation or mutating Jira. Local generation of working documents does not require prior approval.
* Do not add `.copilot-tracking/` to `.gitignore`; keep the folder visible to Git so users can explicitly review and manage its artifacts.
* Do not commit `.copilot-tracking/` artifacts containing transcripts, PII, customer-confidential information, credentials, or secrets without explicit human approval and required sanitization.
* Never place credentials, API tokens, or secrets in repository instructions or chat.