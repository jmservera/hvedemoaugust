<!-- markdownlint-disable-file -->
# Review: Inventory-aware cart fix (G5 tester acceptance)

## Executive Summary

* Assessment: The approved inventory fix matches plan revision 2 and amendment A1. I found no implementation defect that blocks acceptance. The ordered rule, approved copy, stock source, A1 handling, and structured logging match the plan.
* Why this matters: Shoppers can no longer add or set a cart quantity above catalog stock, and invalid quantities no longer silently delete cart lines. Both defects are closed on the tested paths.
* Review execution: Complete (standard depth, one pass over the supplied evidence, no helpers, no test runs).
* Assessed outcome: Residual work. No implementation defect; one plan-oracle decision, one unverified keyboard path, and one plan decision are routed.
* Acceptance disposition: **Conditional**.
* Validation coverage: 74 named cases reported as 73 PASS, 1 FAIL (TC-B03 literal clause), 0 NOT RUN. Re-read evidence: unit 160/160, baseline 29/29, HTTP 21/21, browser 8 of 9 cases with TC-B03 literal excepted. Coverage 78.6% line and 52.2% branch overall; CartService 100% line and 94% branch.
* Confidence and limitations: High on the code path (every changed and untracked source and test file read). Medium on keyboard recovery (single-row evidence only). The screen-reader announcement (CAC-015) is unverified.

## What You May Not Know

* TC-B03 fails only on its literal wording. The plan places the alert after the cart rows, so a forward Tab cannot reach the row. Recovery itself works (Shift+Tab reaches the row). See RV-001.
* No assistive technology was run. CAC-015 stays unproven; council C1 accepts UNVERIFIED.
* A valid over-limit Update on a product not in the cart is a silent no-op by plan design. See RV-003.

## Findings and Proposed Routes

<!-- rpi:review id=RV-001 -->
### RV-001 [Medium]: Plan TC-B03 clause conflicts with the plan's own placement

The plan requires that "the next `Tab` reaches the row" after the alert takes focus. The same plan places the alert inside the Order Summary card (P04-T01, D9, TC-B09). That card follows the row controls in DOM order, so a forward Tab from the alert can only reach what comes after it. No placement that keeps D9 satisfies the literal clause. Observed: Tab from the alert reaches Checkout; Shift+Tab twice reaches the row's Update button (`inventory-cart-test-browser.js` lines 124 to 132, result TC-B03b).

* Related scope: P05-T01 TC-B03; CAC-014, CAC-016, CAC-017.
* Expected behavior: CAC-016 "stay on the originating page without unexpected loss of place"; plan clause "next Tab reaches the row".
* Observed behavior and evidence: stays on `/Cart`; alert focused; inside Order Summary; row controls reachable by Shift+Tab; input `aria-describedby` resolves.
* Impact: A literal test failure, not a user-facing defect on the tested path. Moving the alert or adding a tab trap to pass the wording would break D9 and the no-trap rule.
* Resolution condition: plan owner amends the TC-B03 oracle, or accepts the literal failure as a documented deviation.
* Proposed destination: rpi-plan, as a bounded test-oracle correction. Not a new business rule.
* Smallest useful next action: replace the clause with "Shift+Tab from the alert reaches the affected row's controls; Tab from the alert reaches Checkout", citing TC-B03b. Owner decides.

<!-- rpi:review id=RV-002 -->
### RV-002 [Medium]: Keyboard recovery is evidenced only for a last-row case

The TC-B03 cart holds one line (5 of product 3; browser script lines 110 to 112). The rejected row is therefore the last row in DOM order, and recovery takes two Shift+Tab presses. If a shopper rejects a row that is not last in a multi-row cart, recovery to that row crosses every later row's quantity, Update, and Remove controls. Cost grows with the rows below it. This is untested, not a demonstrated failure.

* Related scope: CAC-016.
* Expected behavior: no unexpected loss of place when recovering from a rejection.
* Observed behavior and evidence: not observed on a multi-row cart.
* Impact: possible keyboard-effort regression on longer carts, the case CAC-016 targets.
* Resolution condition: one keyboard run on a three-row cart where the rejected row is first, with recovery measured; or owner acceptance of the single-row evidence.
* Proposed destination: evidence gap. Bounded retest (tester or owner) before final acceptance; owner may accept the limit instead.
* Smallest useful next action: run the retest. Do not change the focus target now. If the owner rejects the path, the smallest candidate is focusing the affected quantity input, whose `aria-describedby` already names the message. That changes D9 and needs owner approval.

<!-- rpi:review id=RV-003 -->
### RV-003 [Low]: Absent-line Update with a valid over-limit quantity gives no feedback

For Update on a product not in the cart, `CartService.Apply` returns NoOp before the stock checks (step 4 precedes steps 6 and 7). A valid positive quantity above stock on that path therefore shows no message. TC-S25 pins this order. PRD CAC-018 says the overstock text applies "on either mutation path". The plan records the order as PROPOSED (D8), so this is a decision gap, not a coding error. No hidden success occurs: the cart is unchanged and the line is absent on the redirected page. It is reachable only from a stale page or a forged post.

* Related scope: CAC-018; D8; TC-S25, TC-C09.
* Expected behavior: CAC-018 read literally for the Update path.
* Observed behavior and evidence: silent no-op, no message and no warning.
* Impact: low; a rejection with no feedback on a stale-page path only.
* Resolution condition: owner records whether CAC-018 covers absent-line Update. Either accept and document the no-op, or reorder the stock check (implementation and test change).
* Proposed destination: rpi-plan decision record. Non-blocking.

## Acceptance and Change Coverage

| Requirement | Evidence | Assessment |
|-------------|----------|------------|
| Stock from current catalog; never serialized (D3, CAC-010) | `Product.cs` `[JsonIgnore]`; `Apply` reads `ProductService`; `AvailableQuantity` assigned only in `ProductService` seeds; TC-S27, TC-C10 | Met |
| Unknown ID and zero stock give `Out of stock`; overstock text only for positive over positive (CAC-007, CAC-018, CAC-019) | `Apply` steps 1 and 6; `CartOperationResult.Message`; TC-C03, TC-H09 to TC-H11 | Met |
| Add cumulative, Update absolute, inclusive, no wrap (CAC-001 to CAC-005) | `resulting > available` in `long`; TC-S01 to TC-S05, TC-S26 | Met |
| Explicit 0 removes; omitted Add is 1; key presence separates blank from omitted (CAC-009, CAC-025, CAC-029) | `ParseQuantity` with `Request.Form.ContainsKey`; TC-P01 to TC-P09, TC-C05, TC-H01, TC-H08 | Met |
| A1 invalid input: cart unchanged, exact copy, one four-field warning (CAC-006, CAC-026 to CAC-030) | TC-S20 to TC-S24, TC-C04, TC-C06 to TC-C08, TC-H02 to TC-H07; server console shows four-field warnings and none of `abc`, `2147483648`, `zzz`, `3.5`, `ShoppingCart` | Met |
| Negative Update never removes (CAC-026) | TC-S21, TC-H04 | Met |
| Names, prices, images unchanged (CAC-011, CAC-012) | Diff adds only `AvailableQuantity` and commas; baseline snapshot; TC-H14 | Met |
| No reservation, decrement, payment, analytics (CAC-022 to CAC-024) | Source search: no stock write outside seeds; TC-S28, TC-S29, TC-H13 | Met |
| Status thresholds and copy (CAC-010, CAC-013, CAC-020) | `InventoryRules`; TC-S14, TC-S18, TC-B01 | Met |
| Labeled quantity, explicit Update, no auto-submit (CAC-014) | Cart view; TC-B05, TC-B06 | Met |
| Announcement (CAC-015) | `role="alert"`, focus, accessibility snapshot only; no assistive technology run | Not verified (C1 UNVERIFIED) |
| Keyboard recovery (CAC-016) | RV-001, RV-002 | Partly evidenced |
| Placement near cart summary (CAC-017) | Order Summary; Home top; empty cart above empty state (TC-B09) | Met under D9 |
| Widths 1024, 768, 375 (CAC-016) | TC-B08: status and alert fit. Cart table scrolls horizontally at 375 px, pre-existing (479 px vs 498 px unmodified) | Met for status and alert |

## Test Quality

I read all nine test sources. Assertions check real outcomes: the returned result, session JSON read back, redirect target, TempData text, and captured log entries. Test doubles are real services with a fake session and a real form collection; none mocks the unit under test. By design, controller tests call actions directly, so MVC binding is proven only by HTTP. The mutation spot check is reported in the journal and was not re-run.

## Plan Deviations

* Planned, not done: plan checkboxes not updated (intentional; progress lives in the journal). TC-B03 literal clause not met (RV-001). C1 UNVERIFIED, which the council accepts.
* Done, not planned: `coverlet.collector` (plan said "only if needed"; disclosed; on the allowed list). Internal static `InventoryFeedback` helper beyond D2's list (low). README "Tests" section (documentation only).
* Done differently: the "Adding product" information log fires only for accepted adds, so removals and no-ops no longer log it. Plan said keep it. Low; consistent with TC-C09; disclosed.
* Not a deviation: seed for Id 7 is 3. The plan allows other values that cover the boundaries.

## Conditions Honored (council 2026-10-18T14:22:25Z)

* C1: honored as written. CAC-015 is marked UNVERIFIED, NOT PASSED, with DOM and focus evidence only. Residual.
* C2: honored. All 74 named cases are reported as PASS, FAIL, or NOT RUN, each backed by a recorded run. The case-row mapping file has one mismatch (see evidence corrections).

## Evidence Corrections (non-blocking; implementer to fix the record)

1. The journal cites "8 nullable warnings (CS8618, CS8602, CS8603)". The cited build log shows 0 errors and 3 warnings, all NETSDK1138, and no CS warnings. The claim "none in new files" cannot be checked from that log.
2. `inventory-cart-test-case-rows.txt` marks TC-S08 NOT-COVERED. The shared theory `TC_S07_S08` covers it (6 rows). The journal matrix is correct; the mapping file is not.
3. The browser results JSON has no literal TC-B03 FAIL row. The FAIL is recorded only in the journal.

## Scope

* In scope: the 24 files of the approved fix, from the precomputed patch plus `git ls-files --others --exclude-standard -- src tests`. Nine modified source files, five new source files, and ten new test files including the csproj. All read.
* Out of scope (observations, not findings): no antiforgery validation on any POST (pre-existing); `UseHttpsRedirection` warning in HTTP-only runs; net6.0 end-of-support warning NETSDK1138; docs claim 8 products against 10 in code; cart table horizontal scroll at 375 px; nullable warnings in existing code. Staged `.copilot-tracking` and `docs/architecture` files were not inspected, per instruction.

## Parent Decision Record (proposed; the review parent owns final decisions)

* Participation: agent-owned (dispatched by Squad Coordinator). Per-item walkthrough skipped-auto. No user question raised.
* Proposed routes: RV-001 to rpi-plan (oracle correction, owner decision). RV-002 to a bounded keyboard retest or owner acceptance. RV-003 to an rpi-plan decision.
* Proposed outcome: Residual work. Acceptance Conditional.

## Review Record

* Record path: this file, at the caller-bound output path. The rpi-review default path was not used. No parent state was written.
* Plan: `.copilot-tracking/plans/2026-10-18-inventory-aware-cart-fix-plan.md`, hash `696e58e6d90e3fa4298b427232a0043a4c759b95140099372402464441e469e8` (cited from the journal, not recomputed). `git status` shows the plan's working tree equals its index.
* Plan closure: `.copilot-tracking/reviews/2026-10-18-inventory-aware-cart-fix-plan-closure.md` (PASS).
* PRD: `.copilot-tracking/plans/2026-10-17-inventory-aware-cart-prd.md` (v1.0 plus A1).
* Changes: `.copilot-tracking/changes/2026-10-18-inventory-aware-cart-fix-changes.md`.
* Council: `.copilot-tracking/squad/decisions.md#council-verdict-2026-10-18t142225z-inventory-aware-cart`.
* Model: unknown. Tool calls: about 40. Subagents: none. No source, plan, PRD, or state edits. No test runs. No Git index changes.

## Next Steps

* Plan owner: decide RV-001 (TC-B03 oracle) and RV-003 (absent-line over-limit).
* Tester or owner: run the RV-002 keyboard retest, or record acceptance of the limit.
* Implementer: correct the three evidence items.
* Separate follow-up: manual assistive-technology pass for CAC-015. C1 stays UNVERIFIED until then.
* Stop after G5. No further dispatch.
