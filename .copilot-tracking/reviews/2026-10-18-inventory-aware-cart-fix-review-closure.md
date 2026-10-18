<!-- markdownlint-disable-file -->
# Inventory-aware cart: G5 bounded final closure (revision 3)

* Run: `init-2026-10-17-001`, topic `inventory-aware-cart`, Squad Reviewer (primary). Targeted closure of the prior G5 review (`2026-10-18-inventory-aware-cart-fix-review.md`). No new full audit. No tests run by this reviewer.
* Plan basis: revision 3, canonical hash `e8faea492df9c092200c7c95bffb60370ceba2ddb850ebd4fd9cb782ff19d9f7`, recomputed today with `Get-PlanAssessmentHash.ps1` (pwsh 7): match.
* Code basis: 24 changed files; aggregate `FE8693066F3E6B85D96D9124C4EDB989A4FE28E84E59906A7A4B2C8709C3758D`.

## Verdict

* Review verdict: **Pass-With-Findings**. No blocking finding. Five non-blocking items below.
* Local demo completion: **Pass, with the disclosed limit.** CAC-015 screen-reader announcement is UNVERIFIED and NOT PASSED (council C1 route).
* Not claimed: a clean all-accessibility PASS, all 30 CAC formally proven, production readiness, or PRD Finalize.

## Prior findings

| ID | Status | Basis |
|----|--------|-------|
| RV-001 TC-B03 oracle conflict | Closed | Plan rev3 rewrites TC-B03 to the natural-DOM oracle. Rerun: alert in Order Summary; forward Tab reaches Checkout; reverse Tab reaches the affected labelled input in a finite count; no trap; no activation or state change from keys. The rev2 literal FAIL stays as history and is not relabelled. |
| RV-002 multi-row recovery untested | Closed by evidence, bounded | Three-row cart, rejected row first, middle, last, both overstock and A1. Shift+Tab counts 9, 6, 3 predicted and observed. Recovery Update verified each time. Residual: three rows, 1024 px only, cost linear in rows below, no assistive technology. |
| RV-003 absent-line Update over limit is silent | Closed as recorded interpretation | Plan D8 records absent-line Update as no mutation attempt, so the FR-011 no-op stands. TC-S25 and TC-C09 are unchanged. This is an owner interpretation, not new user approval. Residual wording risk: N-5. |

## Verified this pass (targeted)

* **Browser rev3 JSON:** 8 runs (one-row and three-row first, middle, last; each with stock and A1 inputs), 15 checks per run, all true. Predicted equals observed for every reverse-Tab count. The script's oracle matches the rev3 TC-B03 and TC-B03m text.
* **Server console (rev3):** 8 `Inventory request rejected` warnings, one per rejection. Four are overstock (requested 6, 6, 4, 7) and four are A1 (requested -1). Each line carries only operation, product, requested, and available. No match for `abc`, `2147483648`, `zzz`, `3.5`, or `ShoppingCart`. Blank, `abc`, and overflow A1 rejections are not in this log; they rest on the earlier HTTP run.
* **Build:** incremental (16:49:40): 0 errors, 3 NETSDK1138. No-incremental (17:27:14): 0 errors, 11 warnings = 3 NETSDK1138 plus 8 nullable (CS8618 x4, CS8602 x3, CS8603 x1). I compared all eight locations with HEAD. Each expression is unchanged from HEAD: the `HttpContext.Session` dereferences in `CartService`, the `GetProductById` return in `ProductService`, and the property declarations in `Product` and `CartItem`. None is in a new file. Journal correction 1 holds.
* **Journal correction 2** (TC-S08 mapping) holds in the case-row file.
* **Case count:** 75 = 50 unit + 15 HTTP + 10 browser. Basis: unit, HTTP, and eight browser cases (B01, B02, B04 to B09) come from earlier final-build runs on the same code. TC-B03 and TC-B03m come from the rev3 run. Rev2 base was 73 PASS plus 1 FAIL, and that FAIL is preserved. These are not 75 newly executed cases.

## Hash coverage

* The 98-file aggregate equals the git-tracked `src` and `tests` set, staged new files included. A filesystem walk finds one more file: a gitignored `TestResults` coverage XML dated 2026-08-28, outside the set.
* No `src` or `tests` file was written after 16:46:48. The before snapshot (17:23:40) and after snapshot (17:26:00) both postdate that, so they are identical. The incremental build (16:49:40) and the first G5 review (17:13:57) also postdate it. Reviewed, tested, and current code are therefore the same.
* The combined FE8693 value was not recomputed; the aggregation method is not recorded in the artifact.
* A no-incremental rebuild (`ZavaStorefront.dll` at 17:26:55) followed the rev3 browser run (17:25:27). Sources did not change, so this is not a code change.

## Non-blocking items (no route required before demo acceptance)

* **N-1** Journal Execution Status bullets (about lines 16 to 18) are unlabelled and stale: "P05-T01 is open only for the TC-B03 clause" and "plan hash equals 696e58e6". The CURRENT line 13 governs. Test results are not misreported. Label the bullets historical.
* **N-2** Plan rev3 TC-B03 says that on a one-row cart "the first Shift+Tab may reach the row." Measured: the first press reaches Remove and the third reaches the input. The finite-count oracle is met, so this is not a contradiction, only a misleading example.
* **N-3** The browser results JSON gained a TC-B03-rev2 FAIL row at 17:26:16. The entry says it was "added afterwards." That disclosure is faithful. Other rows are from the original run.
* **N-4** `forwardTabsToLeaveSummary` is a loop index. The footer Privacy link is reached on the third forward Tab, with one unlisted control between Checkout and the footer. The forwardCheckout and noTrap checks are unaffected.
* **N-5** RV-003 residual: PRD CFR-005 ("rejection from either Add or Update") and CAC-018 ("either mutation path") do not define absent-line Update. The D8 reading is reasonable. A stricter owner could call this a rejection without feedback. Impact is low: cart unchanged, no false success, reachable only from stale or forged posts. This is an owner decision, not a challenge to the plan.

## Conditions and honest limits

* **C1:** CAC-015 UNVERIFIED, NOT PASSED. No assistive technology was run. The council accepted this route.
* **C2:** all 74 named cases plus TC-B03m are accounted for by recorded runs. Unit, HTTP, and eight browser results are from earlier runs on unchanged code, not rerun under rev3.
* **CAC-016:** keyboard recovery is evidenced at 1024 px for one-row and three-row carts. The mobile standard is deferred in the PRD. Widths 768 and 375 rest on TC-B08 from the earlier run.
* **Pre-existing, out of scope:** no antiforgery on POSTs; net6.0 end of support; cart table scrolls at 375 px; `UseHttpsRedirection` warning in HTTP-only runs.
* **Not checked here:** raw unit (160) and HTTP (21) outputs. They were not among my inputs; the journal matrix is the record.

## Demo decision

Accept the local demo fix at **Pass with the disclosed AT limit**. Do not present it as an all-accessibility pass, production readiness, or a formal PRD close-out.

## Review record

* Output: this file only. Plan, PRD, journal, source, tests, squad state, and Git content are unchanged. `git status` and `git ls-files` may refresh `.git/index` stat data; no staged content changed.
* Calls: about 41 tool calls, 0 subagents. Model identity is not exposed to this charter. Artifact bytes: recorded in the response.
* Server on port 5256: not started by this reviewer. The parent verifies listeners.
