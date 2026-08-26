---
title: Repository Copilot Instructions
description: Repository-wide source-of-truth, traceability, and approval rules for Copilot agents
---

## Authoritative Sources

* Treat Confluence as the source of truth for project documentation, meeting transcripts, BRDs, PRDs, and ADRs.
* Before drafting, changing, or implementing requirements or architecture decisions, read the relevant Confluence pages through the `atlassian-mcp-server` MCP integration.
* Start BRDs and PRDs from meeting transcripts stored in Confluence. Clearly distinguish recorded decisions from assumptions, and preserve each source page URL, page ID, and version in generated artifacts.
* Publish approved BRDs, PRDs, ADRs, and durable documentation to Confluence. Treat repository drafts as non-authoritative working artifacts unless a Confluence page explicitly identifies them as authoritative.
* Treat Jira as the source of truth for work items and delivery status.

## Development Traceability

* Include the relevant Jira issue key, such as `PROJ-123`, in every commit message and pull request title so GitHub for Jira can link the activity to the work item.
* Include a direct Jira work item link and links to all supporting Confluence pages in every pull request description.
* Do not create an untracked commit or pull request. Ask for the Jira issue when no work item is available.

## Evidence, Approval, and Security

* Do not invent missing source material or report that a Confluence, Jira, or GitHub operation succeeded without tool evidence.
* Ask for clarification when authoritative information is missing or conflicting.
* Obtain human review before publishing documentation or mutating Jira.
* Never place credentials, API tokens, or secrets in repository instructions or chat.