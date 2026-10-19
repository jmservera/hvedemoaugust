<!-- markdownlint-disable-file -->
# Inventory-aware cart research

| Field | Value |
|---|---|
| Task | inventory-aware-cart-2026-10-19 |
| Date | 2026-10-19 |
| Researcher | RPI Agent / rpi-research |
| Output mode | convergence |

## Executive Summary

Research complete. Extend the existing cart service with a shared stock rule and explicit mutation results; use post/redirect/get feedback on the originating page. The baseline builds, but there are no tracked test sources or test project.

## What You May Not Know

The catalog contains ten products, despite older documentation describing eight. Preserve all ten, prices and image URLs. The application targets .NET 6, which is installed but out of support; runtime migration is explicitly excluded. Business outcome measurement remains deferred.

## Findings

### Mutations currently have no stock validation

Q1, evidence-backed finding (C2, C3, W1, W2): CartService writes session state directly after unrestricted add/update; invalid adds silently return. Updates at zero or below remove the line. Central validation before any mutation can preserve every existing quantity on rejection. Compute repeated-add totals using a wide integer to prevent overflow. Catalog availability must come from ProductService, not serialized cart snapshots. Confidence: high, direct source inspection.

### Page feedback needs an explicit recovery path

Q2, evidence-backed finding (C4, W1): catalog adds redirect without feedback; cart changes auto-submit on blur and have a hard-coded maximum of 99. Keep PRG to avoid duplicate adds on refresh, use one-time feedback associated with the affected control, restore focus on rejection, and put cart feedback near the summary. Explicit update buttons avoid surprise context changes. A live-region attribute alone cannot prove spoken announcements; owner AT checks remain pending. Confidence: high for structure, manual behavior not yet tested.

### Focused tests must be introduced

Q3, evidence-backed finding (C5): tests contains generated build remnants but no tracked source/project. Baseline dotnet build succeeds with existing nullable warnings and NETSDK1138. Add xUnit service and MVC integration coverage on the existing runtime. Integration tests should include antiforgery, model binding, session preservation and logging field shapes. Confidence: high.

## Recommendation and Alternatives

Selected: shared service validation, explicit mutation results, stock-display properties outside Razor, PRG feedback and focused xUnit integration tests. Preserve .NET 6 and existing dependencies in the application.

Rejected: client-only limits (forged requests bypass them); AJAX-only feedback (unnecessary second transport and fallback path); a new database or runtime migration (excluded). Direct POST rendering is viable but risks resubmitting additive requests on refresh, so PRG is preferred.

## Scope and Questions

Build the approved local demo for the repository owner. Investigate cart mutations, stock presentation, feedback, logging and tests. Exclude runtime migrations, persistence, inventory reservation, analytics and external publication.

* Q1: Where do existing mutations validate input and persist session state?
* Q2: How can rejection feedback preserve the originating page and keyboard context?
* Q3: Which existing tests and tooling can verify the acceptance boundaries?

## Decisions and Feedback

User selected end-to-end automatic progression with agent-owned decisions. Preserve the existing PRD and untracked local configuration. No Confluence or Jira writes. Existing negative-update removal behavior remains unchanged; malformed binding must not become a successful zero-removal. Stock fixtures will include 0, 1, 3, 5 and 6. No unresolved material product choices; no decision walkthrough required.

## Risks and Open Questions

Browser and assistive-technology human review must not be reported as completed by automation. .NET 6 support warning is pre-existing and outside this demo's runtime boundary. A genuinely clean-checkout run is not yet available while approved source documents are untracked; use a clean build-output run and report the distinction.

## Planning Readiness and Next Step

Research disposition: executed, Complete. Planning readiness: Ready (C1-C5, W1-W4). Participation: agent-owned under user-selected automatic mode. Wider, deeper and contrarian waves complete without helpers. No research blockers. Convergence supports planning. Continuation owner: automatic RPI Agent, transition to rpi-plan. Artifact self-check: required sections, evidence, decisions and boundaries present.

## Research Record

### Method and Boundaries

Focused posture selected from the bounded PRD and named source targets. Starting areas: src/Services, src/Controllers, src/Models, src/Views, src/wwwroot and tests. External scope: four PRD-linked Confluence pages and official framework guidance only if needed. Evidence root: .copilot-tracking/research/. Prior knowledge: PRD v0.2.0, approved for local demo; no implementation results assumed.

### Extensions and Participation

Selected: repository source-of-truth rules; tracking, Markdown, writing-style and licensing conventions. C# code/test instructions apply; preserve .NET 6 per PRD rather than the generic .NET 10 convention. Accessibility skill announcement-model guidance selected for feedback and method adequacy. Other domain skills are unrelated and skipped. No helper agents needed for the continuous repository trace.

### Research Cycle Log

Cycle 1:

1. Wider: checked PRD, four current Confluence sources, repository layout and tracked tests. Reflection: existing MVC architecture fits; no new storage required.
2. Deeper: read all mutation services/controllers, both relevant views, layout, Program, project, CSS and JavaScript. Reflection: PRG feedback and shared validation cover both paths; form binding and stale session snapshots require explicit tests.
3. Contrarian: compared documentation against catalog and test inventory; checked baseline build and installed runtimes. Reflection: preserve ten products rather than force older eight-product documentation; do not upgrade runtime or trust build remnants as tests. Client-only and AJAX-only alternatives do not improve this bounded demo.
4. Synthesis: accept service-owned rules and PRG; defer measured outcomes and human browser/AT review. No further cycle needed: source contracts answer all three questions, and adjacent research would not alter the bounded choice.

### Evidence Log

* C1: docs/prds/inventory-aware-cart.md, approved local-demo requirements, policies and source register.
* C2: src/Services/CartService.cs, AddToCart and UpdateQuantity; direct view, high confidence.
* C3: src/Services/ProductService.cs and src/Models/Product.cs, static catalog and product contract; direct view, high confidence.
* C4: src/Controllers/HomeController.cs, src/Controllers/CartController.cs, src/Views/Home/Index.cshtml, src/Views/Cart/Index.cshtml and src/wwwroot/js/site.js; HTTP and feedback flow, direct view, high confidence.
* C5: src/Program.cs, src/ZavaStorefront.csproj, src/ZavaStorefront.sln and tests; direct view and terminal inventory/build. .NET 6.0.36 runtime available, SDK 10.0.113, baseline build succeeds with nine warnings.
* W1: https://m365cpi37447403.atlassian.net/wiki/spaces/HVE/pages/27951105, Stakeholder Meeting, version 1, retrieved 2026-10-19 through Atlassian MCP. Synthetic transcript supports validation-only stock and accessible recovery.
* W2: https://m365cpi37447403.atlassian.net/wiki/spaces/HVE/pages/27820033, Application and Platform Architecture, version 1, retrieved 2026-10-19 through Atlassian MCP. Supports MVC/service boundaries and 30-minute session.
* W3: https://m365cpi37447403.atlassian.net/wiki/spaces/HVE/pages/27688962, Product Overview, version 1, retrieved 2026-10-19 through Atlassian MCP. Baseline journey; older catalog details differ from current source.
* W4: https://m365cpi37447403.atlassian.net/wiki/spaces/HVE/pages/27852801, Delivery and Governance, version 1, retrieved 2026-10-19 through Atlassian MCP. Shared governance distinguished from local-demo approval.
