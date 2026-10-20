# Project

This lab guides you through a series of practical exercises focused on modernising Zava's business applications and databases by migrating everything to Azure, leveraging GitHub Enterprise, Copilot, and Azure services. Each exercise is designed to deliver hands-on experience in governance, automation, security, AI integration, and observability, ensuring Zava’s transition to Azure is robust, secure, and future-ready.

## Product Owner / Program Manager workspace

This repository includes a shared Copilot canvas extension for teammates who
prefer guided forms and document navigation over an IDE. In the Copilot app,
ask **"Open the product-owner-workspace canvas with workspaceId product"**.
After adding or updating the extension, reload extensions first.

1. Set your workspace name and product outcome.
2. Open **Confluence & sources** and choose **Use app connection**. The canvas
   uses the Atlassian and Jira MCP servers currently configured in the app,
   without hard-coding a provider name or requesting passwords or tokens.
   If needed, enable or authenticate the existing connection in the app's MCP
   settings. Optionally save a Confluence page or space URL to narrow the scope.
3. Import a meeting transcript, or discover existing team documents. Live
   imports preserve source URLs, page IDs and versions.
4. Open **Business requirements**, **Product requirements**, or
   **Architecture decisions** to read by section, edit a Markdown draft, or
   request the corresponding HVE authoring or quality-review specialist.
   Continue agent questions and permission decisions in Copilot chat.
5. Review the draft, request a quality review, then separately request
   publication. Copilot must obtain explicit human approval of the exact
   content and destination before writing to Confluence. A quality review is
   not approval. This canvas does not mutate Jira.

The HVE plugin and an authenticated app-configured Atlassian MCP integration must be available
to the current Copilot session to run specialists and read Confluence.
Jira MCP reads provide linked delivery context when relevant; the canvas does
not create its own API connection. Accessible site evidence is shown separately
from verified page reads. Missing access is a blocker, not a simulated success. Agent requests are
submitted to the current conversation, not standalone background workflows;
the agent records outcomes back through the canvas's `complete_job` action.
**Agent activity** distinguishes submitted, completed, blocked and failed
requests. A pending request needs an outcome before another is submitted.
If an agent was interrupted or a request was abandoned, **Mark interrupted**
closes the pending record and rejects late results. It does not stop Copilot;
check the conversation before starting more work.

Extension code lives in `.github/extensions/product-owner-workspace/`.
Drafts, source metadata and activity live outside the repository in
`<session.workspacePath>\files\product-owner-workspace\<workspaceId>.json`.
They survive extension reloads and additional panels with the same workspace
ID in that session, but are not a cross-session document repository.
Confluence remains authoritative; local documents are working artifacts.
No project source documents are bundled as sample data or committed by the
canvas. Review and sanitize any material before sharing it.

For extension contributors, run
`node --test .github\extensions\product-owner-workspace\workspace.test.mjs`.

## Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com. 

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

## Trademarks

This project may contain trademarks or logos for projects, products, or services. Authorized use of Microsoft 
trademarks or logos is subject to and must follow 
[Microsoft's Trademark & Brand Guidelines](https://www.microsoft.com/legal/intellectualproperty/trademarks/usage/general).
Use of Microsoft trademarks or logos in modified versions of this project must not cause confusion or imply Microsoft sponsorship.
Any use of third-party trademarks or logos are subject to those third-party's policies.
