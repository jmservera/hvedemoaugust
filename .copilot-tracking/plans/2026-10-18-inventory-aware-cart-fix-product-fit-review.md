# Inventory-aware cart fix: product-owner council judgment

## Decision and authority

**Verdict: Conditional. Risk: Medium. No business-scope blocker identified.**

Run: `init-2026-10-17-001`. Topic: `inventory-aware-cart`.

This is the bounded product-owner council lens only, performed by PRD Builder under the user-approved temporary override recorded in [decisions.md](../squad/decisions.md), 2026-10-18T13:56:22Z, Scribe turn 31. It does not author requirements, reopen discovery, issue formal PRD scores, change the permanent roster, or advance PRD lifecycle state.

## Evidence reviewed and identity

| Source | Basis |
|---|---|
| [Code plan, revision 2](2026-10-18-inventory-aware-cart-fix-plan.md) | 75,021 bytes; product behavior, tasks, decisions, catalogue, crosswalk, risks, and gates reviewed directly |
| [Full plan closure](../reviews/2026-10-18-inventory-aware-cart-fix-plan-closure.md) | 11,707 bytes; Complete/PASS, PC-001–PC-012 closed; residuals and limitations retained |
| [PRD v1.0 plus A1](2026-10-17-inventory-aware-cart-prd.md) | 93,771 bytes; current approved scope, CAC-001–CAC-030, measurement, and actual Validate status |
| [Decision authority](../squad/decisions.md) | Original approval referenced by the PRD; separate A1 policy and bounded council override checked directly |

The saved plan's canonical assessed-content identity was independently recomputed with the read-only `Get-PlanAssessmentHash.ps1` helper:

```text
projection_version: rpi-plan-assessment-v1
sha256: 696e58e6d90e3fa4298b427232a0043a4c759b95140099372402464441e469e8
```

This matches the closure. Raw-file hashes and sizes are not interchangeable with this bookkeeping-excluding projection.

## Product fit

The plan faithfully translates the approved local-demo scope:

* One threshold of 5 inclusive: 0 unavailable, 1–5 low, above 5 normal. Routine exact counts appear only for low stock; count-disclosure permission in rejection states does not alter the approved copy branches.
* Exact status copy is `In-stock`, `Only {availableQuantity} left`, and `Out of stock`. Zero stock and invalid IDs retain `Out of stock`. Valid positive overstock uses `Stock unavailable. Available quantity: {availableQuantity}. Reduce the quantity.` without a leading space.
* Add is cumulative; Update is absolute; equality is accepted. Current catalog availability, not stale session stock, controls validation and display.
* Explicit 0 removes on either path; omitted Add defaults to 1. Negative, nonnumeric, overflowing, blank, and missing Update quantities preserve the cart and use `Enter a valid whole-number quantity of zero or more.` Safe warnings retain the four approved numeric/nullable contexts without raw input or cart/session/customer data.
* Cart summary feedback, Home's existing page-level context, associations, labels, focus, and keyboard recovery are planned without inventing a Home summary. Desktop 1024 CSS px and representative engineering widths are test inputs, not supported-device or conformance claims.
* All 10 actual products, prices, and images remain; no database, reservation, decrement, warehouse, payment, catalog redesign, or analytics expansion is introduced.

Finance's 40% baseline remains user-reported over six months; 60% is a user-selected target, not a promise. Measurement uses existing aggregate Finance counts three months after deployment, without new app analytics. Deferred dataset details, mobile standards, source truncation, and existing PRD cautions remain accepted non-blocking limitations, not fresh questions.

## Conditions: existing evidence obligations, not new scope

### 1. CAC-015 announcement must not be falsely passed

**The prior condition is faithful.** PRD CAC-015/CNFR-006 require assistive technology to announce rejection feedback, including A1 failures. P04-T01 supplies mechanics, but P05-T01 explicitly substitutes role/text/focus/accessibility-snapshot inspection for a real screen reader; the closure retains that limitation.

The developer/tester evidence must either record actual AT announcement observation for the applicable Add/Update rejection flows, including A1 feedback, identifying the AT/browser environment and observed message, or label the announcement outcome **UNVERIFIED — NOT PASSED** when observation is unavailable. Record a failed observation as failed. Mechanics may pass independently; they cannot establish that the announcement was heard.

This condition preserves the existing outcome requirement without imposing a new certification or supported-device standard.

### 2. Planned coverage must remain distinct from executed results

**The prior condition is faithful.** The catalogue independently counts to **74 named cases: 9 parser, 30 service, 11 controller, 15 HTTP, 9 browser**, mapped to CAC-001–CAC-030. P05-T01/P05-T02 already require per-case results and evidence.

Report every named case as **pass, fail, or not run**, with applicable Add/Update variants separately identified and supported by actual execution evidence. Include observed binding/cart preservation, exact feedback, safe warnings, regressions, and browser/keyboard outcomes at their stated verification levels. Unexecuted cases remain not run; catalogue rows, crosswalk coverage, and closure PASS are not product-test results. Variant expansion means 74 named cases is not necessarily 74 executions.

## Gate meaning and limits

Medium risk reflects outstanding user-outcome verification, especially announcement, not an unresolved business decision. These conditions govern implementation evidence and tester G5 reporting; they do not require new interviews or requirement changes.

The PRD remains **Validate APPROVED_WITH_COMMENTS**, zero blocking findings, with **Finalize NOT_EVALUATED**. Full plan closure PASS is planning evidence only. This judgment supplies the product-owner G3 artifact; architect review and coordinator G4 admission remain separate. It grants no implementation dispatch, production/Confluence approval, deployment, publication, Git, or tracker authorization.

Only this judgment file was written. No application code or tests were run, and no runtime behavior is certified by this review.
