<!-- markdownlint-disable-file -->
# Inventory-aware cart changes

## Metadata

* Task: inventory-aware-cart-2026-10-19
* Date: 2026-10-19
* Plan: [.copilot-tracking/plans/2026-10-19/inventory-aware-cart-plan.md](../../plans/2026-10-19/inventory-aware-cart-plan.md)
* Declared scope: full plan
* Assessed candidate verified before implementation: `33ed772385c41259d8a23e9bbe74dc07c0d4f30f6ad19a6c4ba532e758b2d7a6`

## Execution Status

Complete for full implementation plan. Completed: P01, P01-T01, P01-T02, P01-T03. No remaining implementation markers. Owner acceptance review remains pending.

## Execution Summary

Implemented the approved local demo with no external mutations or runtime migration. Clean-output solution build and all 56 tests pass; browser behavior and no-JavaScript forms verified.

## Completed Work

### Stock validation before persistence

P01-T01: [src/Services/CartService.cs](../../../src/Services/CartService.cs) now validates current catalog stock before writes, returns [src/Models/CartMutationResult.cs](../../../src/Models/CartMutationResult.cs), and logs safe structured warnings. Repeated adds use wide totals; zero adds reject, zero updates remove; invalid products omit availability. Nullable quantities preserve malformed-input rejection without accidental zero removal. [src/Models/Product.cs](../../../src/Models/Product.cs) owns listing descriptions and [src/Services/ProductService.cs](../../../src/Services/ProductService.cs) has 0/1/3/5/6 fixtures without changing catalog content. Validation: automated checks pending P01-T03.

### Same-page recovery

P01-T02: [src/Controllers/HomeController.cs](../../../src/Controllers/HomeController.cs) and [src/Controllers/CartController.cs](../../../src/Controllers/CartController.cs) now publish typed mutation feedback through one-time TempData and PRG, protect the changed POSTs with antiforgery and reject malformed binding. [src/Views/Home/Index.cshtml](../../../src/Views/Home/Index.cshtml) shows stock and labeled quantity controls with zero-stock disabling. [src/Views/Cart/Index.cshtml](../../../src/Views/Cart/Index.cshtml) uses explicit update buttons, zero removal, associations and feedback near the summary. [src/Views/Shared/_CartFeedback.cshtml](../../../src/Views/Shared/_CartFeedback.cshtml) and [src/wwwroot/js/site.js](../../../src/wwwroot/js/site.js) provide announcement semantics and focus recovery; browser/AT evidence remains pending. The cart navigation link has an accessible name.

### Focused semantic and journey verification

P01-T03: Added the xUnit project to [src/ZavaStorefront.sln](../../../src/ZavaStorefront.sln). [tests/ZavaStorefront.Tests/CartServiceTests.cs](../../../tests/ZavaStorefront.Tests/CartServiceTests.cs) tests boundaries, repeated adds, overflow, zero/null input, all-line preservation, stale snapshot authority and exact warning shapes. [tests/ZavaStorefront.Tests/ProductServiceTests.cs](../../../tests/ZavaStorefront.Tests/ProductServiceTests.cs) verifies disclosure boundaries and all ten prices/images. [tests/ZavaStorefront.Tests/CartHttpTests.cs](../../../tests/ZavaStorefront.Tests/CartHttpTests.cs) tests binding, antiforgery, PRG, one-time feedback, session isolation, removals and checkout. [tests/ZavaStorefront.Tests/TestSupport.cs](../../../tests/ZavaStorefront.Tests/TestSupport.cs) provides in-memory session, actual captured logs and MVC hosting. Test sources total five including generated global usings. No pre-existing tests removed; only the newly scaffolded empty example was replaced. No mocking package was needed.

[src/README.md](../../../src/README.md) now records run/test commands, actual catalog fixtures, copy policy, demo steps and pending owner review. Browser keyboard and no-JavaScript checks passed; feedback accessibility tree exposes alert role and recovery text. Documentation launch verified HTTP 200 for both page and generated scoped CSS in Development. Visual check preserves Bootstrap grid and existing images.

## Implementation-Time Plan Updates

None. Local implementation choices stay within the assessed plan.

## Validation Record

| Check | Status | Evidence |
|---|---|---|
| Baseline build | Passed with existing warnings | `dotnet build src\ZavaStorefront.csproj --no-restore`; 0 errors, 9 warnings |
| Clean-output solution build | Passed | `dotnet clean src\ZavaStorefront.sln` then `dotnet build src\ZavaStorefront.sln --no-restore`; 0 errors, 8 pre-existing-category warnings |
| Candidate identity | Passed | Full projection matches Complete/Pass critique |
| Automated feature tests | Passed | 56 xUnit cases, 0 failed/skipped; service and MVC HTTP coverage |
| Browser keyboard journey | Passed | Chrome/155.0.0.0 on Windows, viewport 1038x585; exact add, repeated-add rejection, update rejection, quantity preservation, focus/description, zero removal, checkout |
| Browser no-JavaScript journey | Passed | Same browser, viewport 1440x900; rejection, association, valid add, zero removal |
| Accessibility-tree feedback | Passed for role/text | `alert` exposes exact recovery copy; real speech remains untested |
| VS Code test tool | Unavailable discovery | New tests initially not discovered; used `dotnet test` and verified nonzero count |
| Initial template runner | Failed discovery | Generated old test SDK/xUnit runner found 0 tests; upgraded test-only versions and reran successfully |
| Browser console | Pre-existing host configuration warning | Non-Development no-profile run returns 404 for generated scoped CSS; README specifies Development for local static assets |
| Documented Development launch | Passed | Page and scoped CSS both HTTP 200; fresh browser navigation has no console error |
| Whitespace | Passed | `git diff --check` |
| Owner Chrome/Edge and AT checks | Pending | Human review not performed |
| Genuine clean checkout | Unavailable | Current uncommitted implementation; no clean-checkout claim |

## Pre-Review Reconciliation

All plan markers and completed-work entries reconciled. No assessed-content changes. Automated implementation evidence ready for one post-implementation Review; owner approval is not claimed. Temporary validation servers stopped and generated browser scratch files removed.

## Blockers

No implementation blocker. Owner review remains separate pending evidence.

## Remaining Work

No remaining implementation markers. Required owner review is pending.

## Follow-Up Items

Owner Chrome/Edge and AT review; production runtime migration and post-demo outcome measurement are outside implementation.

## Return-to-Caller State

Implementation Complete, full plan; current critique Complete/Pass. Automatic parent continues to Review using recorded validation and explicitly pending human review. No implementation blockers.
