<!-- markdownlint-disable-file -->
# RPI Changes: Inventory-aware cart fix (local demo)

## Metadata

* Task ID: `inventory-aware-cart-fix` (squad run `init-2026-10-17-001`, topic `inventory-aware-cart`)
* Related plan: [.copilot-tracking/plans/2026-10-18-inventory-aware-cart-fix-plan.md](../plans/2026-10-18-inventory-aware-cart-fix-plan.md) (CURRENT: revision 3, canonical hash `e8faea492df9c092200c7c95bffb60370ceba2ddb850ebd4fd9cb782ff19d9f7`, 79,620 bytes; HISTORICAL: revision 2, hash `696e58e6d90e3fa4298b427232a0043a4c759b95140099372402464441e469e8`, which the original execution below was run against; the plan file was not edited)
* Implementation date: 2026-10-18
* Implementor: Squad Implementor (developer role), dispatched after G1 to G4

## Execution Status

* CURRENT status (after the revision 3 retest): implementation and verification complete for local demo; final tester closure pending; actual AT announcement UNVERIFIED, NOT PASSED. Current execution counts: base 74 cases plus TC-B03m = 75 PASS, 0 fail, 0 not run (revision 3 oracle); 8 new keyboard browser runs, 15 checks per run, all PASSED; the earlier 160 unit rows and 21 HTTP checks were not re-run, but the `src`/`tests` hash is identical. The TC-B03 literal-clause conflict was resolved by the lead's revision 3 and the actual retest; it is not open unless the final review finds an issue. Remaining actual risk: AT announcement unverified, and pre-existing warnings. Not all CAC items are formally passed.
* HISTORICAL status at first delivery (revision 2, kept unchanged, including the failed literal TC-B03; counts 73 PASS, 1 FAIL, 0 NOT RUN of 74): Partial at first delivery. All code, tests, README and verification are done and green except one literal plan clause (TC-B03, see Discrepancies). One council condition (C1) is intentionally left `UNVERIFIED`, not passed. UPDATE after the G5 review: under the plan owner's revision 3 oracle the TC-B03 and new TC-B03m browser runs all pass (75 of 75 on that basis); the revision 2 literal FAIL is kept as history. See `## G5 Follow-up: Revision 3 Oracle Retest`.
* Declared invocation scope: full plan (`P01` to `P05`).
* Completed scope markers (evidence below): `P01-T01`, `P01-T02`, `P02-T01`, `P02-T02`, `P03-T01`, `P04-T01`, `P05-T01` (with the TC-B03 clause exception), `P05-T02`; phases `P01` to `P05` except as qualified for `P05-T01`.
* All remaining active-plan markers: the plan file's checkboxes were left unchecked on purpose (see Plan Updates); `P05-T01` is open only for the TC-B03 clause.
* Status basis: the plan hash was recomputed with `Get-PlanAssessmentHash.ps1` (pwsh 7.6.7) and equals `696e58e6...e469e8`, so implementation was authorized. Build and 160 automated test rows pass, 21 real-HTTP checks pass, 12 real-browser checks pass; one literal clause fails (below).

## Execution Summary

The storefront now has inventory. Every product has a synthetic `AvailableQuantity`. One ordered rule serves both Add (cumulative) and Update (absolute): unknown ID, invalid quantity, Add default of 1, Update on an absent line (silent no-op), explicit `0` (remove), zero stock, inclusive over-limit (computed in `long`), then store. Posted quantities are parsed from a raw string with key-presence awareness. Rejections redirect to the origin page with the exact approved text in a focused `role="alert"` region, keep the last valid cart, and log one four-field warning. Stock status is shown on Home and Cart. The cart's auto-submit is replaced by an explicit, labeled Update.

The repository's first automated test project was created. Tests pin baseline behavior on unmodified source first (29 rows passing), then cover all unit cases; real HTTP and real browser runs cover the rest.

## Completed Work

### Test harness and behavior baseline

* Related phase or task: `P01-T01`, `P01-T02`
* Files:
  * [tests/ZavaStorefront.Tests/ZavaStorefront.Tests.csproj](../../tests/ZavaStorefront.Tests/ZavaStorefront.Tests.csproj)
  * [tests/ZavaStorefront.Tests/BaselineBehaviorTests.cs](../../tests/ZavaStorefront.Tests/BaselineBehaviorTests.cs)
  * [tests/ZavaStorefront.Tests/TestDoubles/FakeSession.cs](../../tests/ZavaStorefront.Tests/TestDoubles/FakeSession.cs)
  * [tests/ZavaStorefront.Tests/TestDoubles/CapturingLogger.cs](../../tests/ZavaStorefront.Tests/TestDoubles/CapturingLogger.cs)
  * [tests/ZavaStorefront.Tests/TestDoubles/StorefrontHost.cs](../../tests/ZavaStorefront.Tests/TestDoubles/StorefrontHost.cs)
  * [tests/ZavaStorefront.Tests/TestDoubles/CartLines.cs](../../tests/ZavaStorefront.Tests/TestDoubles/CartLines.cs)
* Behavior or functionality changed: `net6.0` xunit project (xunit 2.9.3, Test SDK 17.12.0, runner 2.8.2, coverlet.collector 6.0.2; cached versions, manifest written first, restore succeeded, no NSubstitute or Mvc.Testing). Hand-rolled fakes: in-memory session, capturing logger (all levels, structured state), real form collection, `TempDataDictionary`, and a per-host `IHttpContextAccessor` (the framework accessor uses a static `AsyncLocal` and let two hosts overwrite each other, found by TC-S28).
* Validation: 29 baseline rows passed on unmodified `src` before any source change (log `inventory-cart-test-p01-baseline-unmodified-src.log`).

### Stock data, shared rule, quantity parser, result type

* Related phase or task: `P02-T01`
* Files:
  * [src/Models/Product.cs](../../src/Models/Product.cs)
  * [src/Services/ProductService.cs](../../src/Services/ProductService.cs)
  * [src/Services/InventoryRules.cs](../../src/Services/InventoryRules.cs)
  * [src/Services/QuantityInput.cs](../../src/Services/QuantityInput.cs)
  * [src/Services/CartOperationResult.cs](../../src/Services/CartOperationResult.cs)
* Behavior or functionality changed: `Product.AvailableQuantity` is `[JsonIgnore]`, so no session snapshot carries stock. Seeds (synthetic, labeled in code): Id1=0, Id2=1, Id3=5, Id4=6, Id5=8, Id6=12, Id7=3, Id8=15, Id9=25, Id10=10. Names, descriptions, prices and `ImageUrl`s are unchanged. `InventoryRules` holds threshold 5, status and the five exact strings. `QuantityInput.Parse(string?, bool wasSupplied)` returns `Omitted`, `Valid(n)` or `Invalid(requested)`; blank, whitespace, nonnumeric and overflow give `Invalid(null)`, a negative gives `Invalid(n)`, and no failure is ever `Valid(0)`.
* Validation: TC-P01 to TC-P09, TC-S14 to TC-S18 passed (matrix).

### Ordered, bounded Add and Update

* Related phase or task: `P02-T02`
* Files:
  * [src/Services/CartService.cs](../../src/Services/CartService.cs)
* Behavior or functionality changed: `AddToCart(int, QuantityInput)` and `UpdateQuantity(int, QuantityInput)` return `CartOperationResult` and share one private ordered rule (D8). Availability is read from the current `ProductService` on every call. `GetAvailableQuantity(int)` exposes the catalog value. Nothing reserves or decrements stock. A mutation spot check proved the tests bite: changing the over-limit comparison from `>` to `>=` made 6 tests fail; it was reverted.
* Validation: TC-S01 to TC-S13, TC-S19 to TC-S30 passed.

### Rejection flow, structured warning, parsing at the controllers

* Related phase or task: `P03-T01`
* Files:
  * [src/Controllers/InventoryFeedback.cs](../../src/Controllers/InventoryFeedback.cs)
  * [src/Controllers/HomeController.cs](../../src/Controllers/HomeController.cs)
  * [src/Controllers/CartController.cs](../../src/Controllers/CartController.cs)
* Behavior or functionality changed: both actions bind `string? quantity`; key presence comes from `Request.HasFormContentType && Request.Form.ContainsKey("quantity")`. A rejection stores the exact message (and the product ID only when the product exists) in `TempData`, redirects to the origin (`Home` or `Cart`), and logs exactly one warning `Inventory request rejected: operation {Operation}, product {ProductId}, requested {RequestedQty}, available {AvailableQty}` with `Operation` 1 = add, 2 = update. The old non-existent-product warning and the raw-quantity information log are replaced (the `Updating quantity for product {ProductId}` information entry carries only the ID). The `Adding product ...` information log now fires only for an accepted add.
* Validation: TC-C01 to TC-C11 passed; real binding proven by TC-H01 to TC-H15.

### Stock status, announced message, accessible cart controls

* Related phase or task: `P04-T01`
* Files:
  * [src/Views/Home/Index.cshtml](../../src/Views/Home/Index.cshtml)
  * [src/Views/Cart/Index.cshtml](../../src/Views/Cart/Index.cshtml)
  * [src/Views/Shared/_InventoryMessage.cshtml](../../src/Views/Shared/_InventoryMessage.cshtml)
  * [src/wwwroot/css/site.css](../../src/wwwroot/css/site.css)
* Behavior or functionality changed: Home cards and Cart rows show the approved status as visible text from the current catalog (Cart gets a controller-supplied availability map). Home `Buy` is disabled at stock 0. Cart quantity input has a visually-hidden label naming the product, `inputmode="numeric"`, no `min`/`max`, no `onchange` submit, a form `novalidate`, and an explicit `Update` button. One server-rendered `role="alert"` `tabindex="-1"` region (`#inventory-message`) sits at the top of Home content, inside the Order Summary card on Cart, and above the empty-state alert on an empty Cart; a small script focuses it on load. `aria-describedby` links the matching `Buy` or quantity input to the status and the region. CSS color modifiers only reinforce the text; the Update row uses `flex-wrap` so the narrow-width cart is not wider than the unmodified app.
* Validation: TC-B01 to TC-B09 (see matrix and Discrepancies).

### README and changes record

* Related phase or task: `P05-T02`
* Files:
  * [src/README.md](../../src/README.md)
* Behavior or functionality changed: new `Inventory Behavior` and `Tests` sections (rule, copy, seed-data label, A1 behavior). The 8-product claim is untouched (observed context: code has 10 products, docs say 8).
* Validation: this record.

## Implementation-Time Plan Updates

### Plan checkboxes left untouched

* Affected plan area or markers: all `Pxx` and `Pxx-Txx` checkboxes.
* What changed: nothing in the plan file. Progress is tracked in this record; the plan's assessed-content hash is unchanged by construction.
* Why: the caller directed that normative content stay unchanged and that progress be returned for the lead rather than edited in.
* Triggering evidence: caller direction.
* User answer or decision: none.
* Reconciliation performed: checkbox updates the lead may apply (progress fields only): check `P01-T01`, `P01-T02`, `P01`, `P02-T01`, `P02-T02`, `P02`, `P03-T01`, `P03`, `P04-T01`, `P04`, `P05-T02`. Leave `P05-T01` and `P05` open until the TC-B03 clause is dispositioned.
* Planning and critique state: no assessed content changed; no new critique needed.

### Plan wording versus implementation (for the planner or tester)

* Affected plan area or markers: TC-B03 and R4.
* What changed: none in the plan.
* Why: the plan expects "the next `Tab` reaches the row". With the region inside the Order Summary (D9), the row controls precede it in DOM order, so the next `Tab` reaches `Checkout` and `Shift+Tab` reaches the row.
* Triggering evidence: TC-B03 browser run.
* User answer or decision: none; needs a plan-owner choice (accept the keyboard-recovery reading, or relocate the region).
* Reconciliation performed: recorded here and in Discrepancies only.
* Planning and critique state: awaiting a plan-owner disposition; it does not block the code.

## Validation Record

| Check | Scope | Status | Evidence or reason |
|-------|-------|--------|--------------------|
| Plan hash recompute | gate | Passed | `Get-PlanAssessmentHash.ps1` (pwsh 7.6.7, `-ExecutionPolicy Bypass` for that process only) returned `696e58e6d90e3fa4298b427232a0043a4c759b95140099372402464441e469e8` |
| `dotnet restore` of test project | P01-T01 | Passed | restored; only warning is NETSDK1138 (net6.0 end of support, SDK 10.0.113) |
| Baseline tests on unmodified `src` | P01-T02 | Passed | 29/29, `inventory-cart-test-p01-baseline-unmodified-src.log` |
| `dotnet build src\ZavaStorefront.csproj --nologo` | whole app | Passed | 0 errors. CORRECTED after G5: the saved incremental log `inventory-cart-test-build.log` shows 0 errors and 3 warnings, all NETSDK1138, and no CS warnings (incremental build, source already compiled). The nullable-warning claim now rests on a saved `--no-incremental` build, `inventory-cart-test-build-noincremental.log` (re-run in the follow-up): 0 errors, 11 reported warnings of which 8 distinct nullable ones in pre-existing code (CS8618 x4 in `CartItem.cs`/`Product.cs`, CS8602 x3 and CS8603 x1 in `CartService.cs`/`ProductService.cs`) plus NETSDK1138; none in the new files |
| `dotnet test tests\ZavaStorefront.Tests\ZavaStorefront.Tests.csproj --nologo` | final build | Passed | 160 passed, 0 failed, 0 skipped; TRX and run log in session files |
| Focused coverage (coverlet 6.0.2) | `src` | Passed (informational) | line 78.6% and branch 52.2% overall; `CartService` 100% line / 94% branch; `InventoryRules`, `QuantityInput`, `InventoryFeedback`, `Product`, `ProductService` 100% line; `CartOperationResult` 96%; `CartController` 91%; `HomeController` 65% (`Privacy` and `Error` untouched); `ErrorViewModel` 0% (pre-existing) |
| Mutation spot check | `CartService` over-limit comparator | Passed | `>` changed to `>=` caused 6 failures; reverted; suite green again |
| Real HTTP against the running local app | TC-H01 to TC-H15 | Passed | `inventory-cart-test-http-results.json`, 21/21 checks (15 cases, split Add/Update, plus 1 informational) on the final build |
| Real browser (Playwright Chromium, keyboard only) | TC-B01 to TC-B09 | Passed with one literal exception | `inventory-cart-test-browser-results.json`; TC-B03 literal clause fails (Discrepancies) |
| Actual screen reader announcement | CAC-015 (C1) | Unavailable | no screen reader run; marked `UNVERIFIED`, not passed |
| Temporary servers | cleanup | Passed | app on 5256 and a baseline copy on 5257 stopped by shell ID; no listener remains; browser page closed; scratch `.playwright-mcp` folder removed |

Session evidence files (all under `C:\Users\juanserv\.copilot\session-state\8d4d1e03-31a4-4baa-a924-c836894409cf\files\`): `inventory-cart-test-p01-baseline-unmodified-src.log`, `inventory-cart-test-build.log`, `inventory-cart-test-unit-run.log`, `inventory-cart-test-case-rows.txt`, `inventory-cart-test-coverage.cobertura.xml`, `inventory-cart-test-http.mjs`, `inventory-cart-test-http-run.log`, `inventory-cart-test-http-results.json`, `inventory-cart-test-server-console.log`, `inventory-cart-test-browser.js`, `inventory-cart-test-browser-results.json`, and `trx\`. Environment: .NET SDK 10.0.113 building `net6.0` on runtime 6.0.36; the external image host and icon CDN were answered locally in the browser run so layout checks were deterministic.

## Case Execution Matrix (74 named cases)

Result key: PASS only on an actual exercise in this run. `Rows` is the number of executed test rows (theory data rows, Add and Update variants). No case is NOT RUN. Planned is never counted as executed.

### Unit level: 50 cases, 50 PASS

Source: TRX of the final run (160 rows, 160 passed). `-a` is Add, `-u` is Update.

| Case | Result | Rows | Notes |
|------|--------|------|-------|
| TC-P01 | PASS | 1 | |
| TC-P02 | PASS | 3 | `null`, empty, whitespace |
| TC-P03 | PASS | 1 | parser returns Omitted; service maps it to invalid (TC-S24) |
| TC-P04 | PASS | 1 | |
| TC-P05 | PASS | 1 | |
| TC-P06 | PASS | 3 | `abc`, `3.5`, `1e2` |
| TC-P07 | PASS | 2 | |
| TC-P08 | PASS | 3 | `0`, ` 3 `, `2147483647` |
| TC-P09 | PASS | 9 | none returned `Valid(0)` |
| TC-S01 | PASS | 1 | |
| TC-S02 | PASS | 1 | |
| TC-S03 | PASS | 1 | |
| TC-S04 | PASS | 1 | |
| TC-S05 | PASS | 1 | |
| TC-S06 | PASS | 1 | one test covers a rejected Add then a rejected Update |
| TC-S07 | PASS | 3 | `-a` for IDs 0, -1, 999 |
| TC-S08 | PASS | 3 | `-u` for IDs 0, -1, 999 (shared test with TC-S07) |
| TC-S09 | PASS | 2 | `-a`, `-u` |
| TC-S10 | PASS | 2 | line present, line absent (`-a`) |
| TC-S11 | PASS | 2 | line present, line absent (`-u`) |
| TC-S12 | PASS | 1 | `-a` |
| TC-S13 | PASS | 2 | `-u` positive and zero |
| TC-S14 | PASS | 6 | |
| TC-S15 | PASS | 3 | |
| TC-S16 | PASS | 11 | 10 products plus the ID list |
| TC-S17 | PASS | 10 | |
| TC-S18 | PASS | 5 | |
| TC-S19 | PASS | 3 | `-a` |
| TC-S20 | PASS | 1 | `-a` |
| TC-S21 | PASS | 1 | `-u` |
| TC-S22 | PASS | 2 | `-a`, `-u` |
| TC-S23 | PASS | 2 | `-a`, `-u` |
| TC-S24 | PASS | 1 | `-u` |
| TC-S25 | PASS | 7 | each ordered pair, Add and Update where both apply |
| TC-S26 | PASS | 3 | Add, Update, stored `int.MaxValue` |
| TC-S27 | PASS | 7 | missing, zero and wrong stock in the snapshot; Add and Update |
| TC-S28 | PASS | 1 | two sessions |
| TC-S29 | PASS | 3 | |
| TC-S30 | PASS | 6 | |
| TC-C01 | PASS | 1 | |
| TC-C02 | PASS | 1 | |
| TC-C03 | PASS | 4 | |
| TC-C04 | PASS | 9 | 4 raw values x Add and Update, plus missing on Update |
| TC-C05 | PASS | 4 | |
| TC-C06 | PASS | 10 | shared scenarios with TC-C07 |
| TC-C07 | PASS | 2 (+10 shared) | |
| TC-C08 | PASS | 4 | |
| TC-C09 | PASS | 5 | |
| TC-C10 | PASS | 1 | |
| TC-C11 | PASS | 2 | |

### Real HTTP: 15 cases, 15 PASS

Method: temporary attached local server (`http://localhost:5256`, Development), Node `fetch` with a cookie jar, a real antiforgery token read from the page and posted, redirects followed manually, `GET /Cart` to read state back.

| Case | Result | Evidence |
|------|--------|----------|
| TC-H01 | PASS | Add with `quantity` omitted: 302 to `/`; cart `{3:1}`; no message |
| TC-H02 | PASS | Add `quantity=` blank: cart unchanged, A1 message once; blank not defaulted to 1 |
| TC-H03 | PASS | Add `-1` onto an existing line of 2: unchanged, A1 |
| TC-H04 | PASS | Update `-1`: line kept (not removed), landed on `/Cart`, A1 |
| TC-H05 | PASS | `abc`: `-a` and `-u` both unchanged with A1 |
| TC-H06 | PASS | `2147483648`: `-a` and `-u` both unchanged with A1 |
| TC-H07 | PASS | Update with no `quantity` field: unchanged, A1, no removal |
| TC-H08 | PASS | `quantity=0`: `-a` and `-u` remove the line, no message |
| TC-H09 | PASS | `-a` (2+4) and `-u` (6) against stock 5: unchanged, `Stock unavailable. Available quantity: 5. Reduce the quantity.` |
| TC-H10 | PASS | forged Add at stock 0, omitted and `2`: not added, `Out of stock` |
| TC-H11 | PASS | product 999, `-a` and `-u`: `Out of stock`, status 302, no stack or exception text |
| TC-H12 | PASS | second `GET` shows no message; cart unchanged; no resubmit |
| TC-H13 | PASS | checkout: badge 3 before, redirect to `/Cart/CheckoutSuccess`, empty cart, badge gone |
| TC-H14 | PASS | 10 cards; all 10 prices and all 10 `img src` values equal the baseline |
| TC-H15 | PASS | 15 rejections produced exactly 15 inventory warnings in the expected order, each `operation, product, requested, available` (`(null)` where null); no `abc`, `2147483648` or `ShoppingCart` in the console log |

Informational, pre-existing and out of scope: a post without an antiforgery token is also accepted (no validation attribute exists); `UseHttpsRedirection` logs `Failed to determine the https port for redirect.` when the app runs HTTP only.

Binding observations (the proof of real MVC binding, not a direct action call): an absent `quantity` key reaches the action as `null` with key-presence false (Add becomes 1, Update invalid); `quantity=` reaches it as `null` with key-presence true (invalid, never defaulted); `abc`, `-1`, `2147483648` and `0` arrive as their literal strings through the `string?` binder with no ModelState failure turning into `0`; `productId` binds as `int`; the `TempData` cookie survived one redirect and was consumed by the next request.

### Real browser (Playwright Chromium, keyboard only): 9 cases, 8 PASS, 1 FAIL on a literal clause

Method: `Tab`, `Shift+Tab`, `Enter`, `Space`, arrow keys; assertions on `document.activeElement`, ARIA attributes, the accessibility snapshot (`ariaSnapshot`) and bounding boxes. Forged stock-0 and unknown-ID posts use a real in-browser form submit.

| Case | Result | Evidence |
|------|--------|----------|
| TC-B01 | PASS | statuses `Out of stock`, `Only 1 left`, `Only 5 left`, `In-stock` x3, `Only 3 left`, `In-stock` x3; still present as text with all status colors forced to black |
| TC-B02 | PASS | five `Buy` activations accepted with no message; the sixth stays on `/`, region text exact, `role=alert`, `activeElement` is `#inventory-message`, `Buy` `aria-describedby` = `stock-status-3 inventory-message` and both resolve; snapshot `alert: "Stock unavailable..."` |
| TC-B03 | FAIL (literal clause only) | passes: stays on `/Cart`, alert inside the Order Summary, `activeElement` is the region, input `aria-describedby` resolves (`stock-status-cart-3 inventory-message`), input restored to 5. Fails: "the next `Tab` reaches the row". Observed: `Tab` goes to `Checkout`; `Shift+Tab` twice reaches the row's `Update` (via `Remove`). Keyboard recovery is usable, but the literal wording is not met |
| TC-B04 | PASS | typed `-1`, blank and `3.5` by keyboard, each followed by Update on `Enter`: exact A1 text, `role=alert`, region focused, inside the Order Summary, `aria-describedby` resolves, line unchanged |
| TC-B05 | PASS | typing, `ArrowUp`/`ArrowDown` and blur caused no navigation and no cart change; `Tab` reached `Update`; `Space` updated the cart to 3; `Enter` updated it to 2 |
| TC-B06 | PASS | `spinbutton` named `Quantity for 4K Action Camera` and `button` named `Update quantity for 4K Action Camera` found by role and name |
| TC-B07 | PASS | card 1 shows `Out of stock`, `Buy` disabled; 25 `Tab` presses visited 9 distinct enabled `Buy` buttons (IDs 2 to 10) and never the disabled one; a forged post at stock 0 moves focus to the alert |
| TC-B08 | PASS | at 1024, 768 and 375 CSS px (engineering test inputs): Home has no page overflow and the alert fits; the Cart alert is inside the viewport and fully wrapped. Cart at 375 scrolls horizontally on the unmodified app too (page 498 px); with the change it is 479 px, so not worse. An earlier run on this case failed at 538 px; adding `flex-wrap` to the Update row fixed it and the final run passed |
| TC-B09 | PASS | Home: region before the `h1`, no Order Summary on Home; Cart with items: inside the Order Summary; empty Cart: above the empty-state alert, not in a card, focused |

### Totals

* Unit 50/50 PASS, HTTP 15/15 PASS, Browser 8/9 PASS: 73 PASS, 1 FAIL (TC-B03 literal clause), 0 NOT RUN, of 74.
* Rows behind the 74 cases: 160 unit/service/controller rows, 21 HTTP checks, 12 browser checks (TC-B03 and TC-B04 are split into sub-checks).

## Council Condition Status

| Condition | Status | Evidence |
|-----------|--------|----------|
| C1: actual assistive-technology announcement for CAC-015, or mark `UNVERIFIED`, not `PASSED` | UNVERIFIED, NOT PASSED | No screen reader was run. Observed only DOM, role, focus and accessibility-snapshot behavior (TC-B02, TC-B03, TC-B04): `role="alert"`, exact text, focus moved to the region. These are not proof of speech output. Needs a manual NVDA, JAWS or VoiceOver pass |
| C2: report all 74 named cases plus applicable Add/Update variants as PASS, FAIL or NOT RUN, backed by actual results; planned is not executed | Satisfied | Matrix above: 73 PASS, 1 FAIL, 0 NOT RUN, each from an actual run recorded in the session evidence files; Add and Update variants reported as `-a` and `-u` rows |

## Decisions D1 to D11 as built

* D1 (PRG with `TempData`): built; cookie provider, no new configuration; consumed once (TC-H12).
* D2 (abstractions limited to rules, parser, result type): built as `InventoryRules`, `QuantityInput`, `CartOperationResult`; one small helper, `InventoryFeedback`, shares the controllers' flow; no interface or config.
* D3 (`[JsonIgnore]` stock, catalog is the source): built (TC-S27, TC-C10).
* D4 (A1 behavior): built; a negative Update never removes (TC-S21, TC-H04).
* D5 (`Out of stock` versus the overstock template): built (TC-C03, TC-H10, TC-H11).
* D6 (testability): seeded catalog used; no constructor or interface added to `ProductService`.
* D7 (raw string parser, key presence): built with `Request.HasFormContentType && Request.Form.ContainsKey("quantity")`.
* D8 (ordered rule): built exactly as listed; each adjacent pair is pinned (TC-S25). Still a PROPOSED engineering choice, not a stakeholder fact.
* D9 (placement): built; Home top of content, Cart inside the Order Summary, empty Cart above the empty state. The TC-B03 Tab-order consequence is in Discrepancies.
* D10 (`Operation` numeric): 1 = add, 2 = update, PROPOSED.
* D11 (one warning per rejection, replaced logs, `RequestedQty` is the submitted value): built.

## G5 Follow-up: Revision 3 Oracle Retest (2026-10-18, after the G5 review)

Scope: bounded verification only. No `src/` or `tests/` file changed: a SHA-256 over all 98 non-build files under `src` and `tests` was identical before and after (`FE8693066F3E6B85D96D9124C4EDB989A4FE28E84E59906A7A4B2C8709C3758D`, files `inventory-cart-test-rev3-src-tests-hash-before.txt` and `-after.txt`). The plan was recomputed with the helper at revision 3: `e8faea492df9c092200c7c95bffb60370ceba2ddb850ebd4fd9cb782ff19d9f7`, 79,620 bytes, matching the plan owner's hash. The plan, PRD, squad state and Git index were not touched.

### Evidence corrections from the G5 review

* Build warnings: the earlier line "8 nullable warnings" was not supported by the cited log. Corrected in the Validation Record row above, backed by the new saved `--no-incremental` log.
* TC-S08: the mapping file `inventory-cart-test-case-rows.txt` said NOT-COVERED because the name regex only matched `TC_S08`, not `TC_S07_S08`. TC-S08 is covered by the shared theory (6 rows: 3 Add as TC-S07 and 3 Update as TC-S08), all passing. The file is corrected; the matrix in this journal was already right.
* Browser results JSON: `inventory-cart-test-browser-results.json` now carries a separate entry `TC-B03-rev2-literal-clause-next-Tab-reaches-the-row` with `pass: false`, labelled as the rev2 historical run. The earlier entries are untouched, nothing was erased, and the new run is in a separate file, `inventory-cart-test-browser-rev3-results.json`.

### Historical result preserved

The revision 2 run of TC-B03 stays a FAIL (literal clause "the next Tab reaches the row", observed: Tab goes to Checkout, Shift+Tab twice reaches the row). It is not retroactively changed to PASS. The counts of that run (73 PASS, 1 FAIL, 0 NOT RUN of 74) remain the historical record above.

### New run under the revision 3 oracle (TC-B03 and TC-B03m)

Method: attached local server on `http://localhost:5256` (stopped afterwards), Playwright Chromium, keyboard only (`Tab`, `Shift+Tab`, `Enter`, typing) at 1024 CSS px. The cart was built by pressing `Buy` through the keyboard. The one programmatic call is documented: after the pure-keyboard reverse path, the alert was re-focused with `focus()` to repeat the forward path and the recovery path from the same starting point. Script `inventory-cart-test-browser-rev3-b03m.js`, results `inventory-cart-test-browser-rev3-results.json`, server log `inventory-cart-test-rev3-server-console.log`.

Per run the script checked: stays on `/Cart`; alert text exact, `role=alert`, focused, inside the Order Summary; the affected input's `aria-describedby` resolves and includes the region; no positive `tabindex` anywhere; forward `Tab` from the alert reaches `Checkout`; `Shift+Tab` reaches the affected input after exactly the predicted press count (number of tabbable controls between the alert and that input), and the focused input's label is `Quantity for {product}`; its value is the prior valid quantity; no navigation, no cart or message change from focus or key presses (server cart unchanged, alert still present); continuing `Tab` leaves the summary (no trap); and keyboard recovery (reverse path, `Ctrl+A`, type a valid quantity, `Tab` to `Update`, `Enter`) updates only the affected line and clears the message.

| Run | Rejection | Rows | Predicted = observed `Shift+Tab` presses | Result |
|-----|-----------|------|------------------------------------------|--------|
| TC-B03 (one-row) | overstock, `6` over stock 5 | 1 | 3 (Remove, Update, input) | PASS, all 15 checks |
| TC-B03 (one-row, A1 path) | `-1` | 1 | 3 | PASS |
| TC-B03m first row | overstock, `6` over 5 (4K Action Camera) | 3 | 9 | PASS |
| TC-B03m first row | A1, `-1` | 3 | 9 | PASS |
| TC-B03m middle row | overstock, `4` over 3 (Ergonomic Office Chair) | 3 | 6 | PASS |
| TC-B03m middle row | A1, `-1` | 3 | 6 | PASS |
| TC-B03m last row | overstock, `7` over 6 (Bluetooth Speaker) | 3 | 3 | PASS |
| TC-B03m last row | A1, `-1` | 3 | 3 | PASS |

Observed behavior: after the alert takes focus, forward `Tab` goes `Checkout`, then `Continue Shopping`, then the footer `Privacy` link (3 presses, so there is no trap). Reverse path stops are always `Remove`, `Update`, quantity input for each later row in turn, then the affected row. Cost is bounded by the number of rows below the rejected one: 3 presses for the last row, 6 for the middle, 9 for the first. Recovery by keyboard worked from every position for both the real stock over-limit path (exact `Stock unavailable. Available quantity: {n}. Reduce the quantity.`) and the A1 invalid-quantity path (exact `Enter a valid whole-number quantity of zero or more.`). Each run corrected the quantity to a valid value and the server cart then held exactly that value with all other lines unchanged. No concrete recoverability defect was found, so no code change is proposed. The multi-row cost growth is a usability cost, not a defect; a direct recovery link would be a separate decision for the plan owner.

Server console for the rev3 run: 8 inventory warnings, one per rejection, each with operation 2 and the four fields (for example `product 3, requested 6, available 5` and `requested -1`), no `fail` or `crit` lines, no raw-input or session strings. The only other warning is the pre-existing `Failed to determine the https port for redirect.`.

### Updated counts (plan revision 3 oracle)

| Basis | Total named cases | PASS | FAIL | NOT RUN |
|-------|-------------------|------|------|---------|
| Historical run under the revision 2 oracle (kept) | 74 | 73 | 1 (TC-B03 literal clause) | 0 |
| Revision 3 oracle: the 74 base cases plus new TC-B03m | 75 | 75 | 0 | 0 |

Evidence for the 75: unit 50/50 and HTTP 15/15 come from the earlier final-build runs and were NOT re-run in this follow-up (the `src` and `tests` hash is identical, so those results still describe the same code); browser cases TC-B01, TC-B02, TC-B04 to TC-B09 also come from the earlier final-build run and were not re-run; TC-B03 and TC-B03m (8 runs above) were run now under the revision 3 oracle. TC-B03 passes only under the revision 3 wording, which the plan owner adopted (revision 3 hash above); the final tester review will cover that hash. D8 (absent-line Update stays a silent no-op, no mutation) needed no code change; TC-S25 and TC-C09 are unchanged and still pass in the earlier run.

C1 remains `UNVERIFIED, NOT PASSED`: no real screen reader was observed in this retest either. C2 stands: the 74-row actual matrix is retained, with TC-B03m added as an explicit new case and the rev2 literal FAIL kept as history.

Cleanup: the app server was stopped by shell ID (no listener on 5256), the browser page was closed, and the scratch `.playwright-mcp` folder was removed. About 25 tool calls were used for the retest and evidence corrections. The model is unknown.

## Pre-Review Reconciliation

* Plan markers and task-local context: plan unchanged; suggested checkbox updates listed above for the lead.
* Completed-work entries and handoff prose: current.
* Validation, blockers, remaining work, and follow-up items: current; the HTTP and browser results come from the final build only (earlier runs were superseded and are described where they mattered).
* Review readiness: ready for tester review (G5), with the TC-B03 clause and C1 as the named open items.

## Discrepancies

* TC-B03 literal clause not met (see the matrix and Plan Updates). Keyboard recovery works through `Shift+Tab`; satisfying the literal wording would mean moving the region, which D9 forbids. A plan-owner decision is needed.
* Seed values: Id7 is 3, below the illustrative "rest 8 to 25". Chosen to show a mid-range low-stock count; the required 0, 1, 5, 6 boundaries are all present.
* `coverlet.collector` was added although the plan said "only if needed"; it was used for the requested focused coverage and is a cached package.
* The test project needed `Using Include="Xunit"`, `Microsoft.AspNetCore.Http` and an explicit `FrameworkReference` to `Microsoft.AspNetCore.App`.
* Observed context, not changed: docs claim 8 products while the code has 10; the `HomeController` information log `Adding product ...` no longer fires for a zero-quantity removal; the 375 px cart table scrolls horizontally on the unmodified app.
* Process notes: a stale Razor build (a view edit not yet compiled) was caught by inspecting the served HTML, and the final HTTP and browser runs were repeated on the rebuilt binary. One edit to a test helper (`StorefrontHost.cs`) was made with a PowerShell string replace before switching to the edit tool; it was a two-line change.

## Blockers

* None. Restore succeeded; no SDK or package blocker occurred.

## Remaining Work

* CURRENT: the TC-B03 literal-clause disposition was resolved by revision 3 and the actual retest (historical rev2 FAIL retained); reopen only if the final review finds an issue.
* C1: a real screen-reader pass, if CAC-015 must be asserted rather than left unverified (still UNVERIFIED, NOT PASSED).
* Final tester closure.

## Follow-Up Items

* Canonical plan list: [.copilot-tracking/plans/2026-10-18-inventory-aware-cart-fix-plan.md](../plans/2026-10-18-inventory-aware-cart-fix-plan.md), `## Follow-Up Items`
* Pre-existing and out of scope: antiforgery validation is not enforced on any POST; the cart table has no responsive wrapper at 375 CSS px; `UseHttpsRedirection` warns when run HTTP only; nullable warnings in existing files.
* No commits, pushes, deploys, Jira or Confluence writes, data deletion, or edits to the PRD, research, council or critique artifacts were made. The shared Git index was not touched (read-only `status`, `diff` and `archive` only).

## Return-to-Caller State

* CURRENT implementation execution status: implementation and verification complete for local demo; final tester closure pending; actual AT announcement UNVERIFIED, NOT PASSED (accepted council path).
* Declared scope and markers: full plan; all tasks implemented. Plan checkboxes and phase progress were not changed by this record.
* CURRENT validation coverage (revision 3 oracle): base 74 + TC-B03m = 75 PASS, 0 fail, 0 not run; 8 new keyboard browser runs, 15 checks per run, all PASSED (first/middle/last-row stock and A1 paths); `src`/`tests` unchanged (same hash), so the earlier 160 unit rows and 21 HTTP checks were not re-run but still describe the same code.
* HISTORICAL validation coverage (revision 2, retained): 73 PASS, 1 FAIL (TC-B03 literal clause "the next Tab reaches the row"), 0 NOT RUN of 74; 160/160 test rows; 21/21 HTTP checks; 12 browser checks. Not erased or relabeled.
* Blockers: none. TC-B03 literal-clause conflict resolved by the lead's revision 3 and the actual retest; open again only if the final review finds an issue.
* Remaining actual risk: actual assistive-technology announcement unverified; pre-existing warnings. Not all CAC items are formally passed.
* Current plan updates: none applied; checkbox suggestions listed.
* Planning and critique state: CURRENT plan revision 3, canonical hash `e8faea492df9c092200c7c95bffb60370ceba2ddb850ebd4fd9cb782ff19d9f7`, 79,620 bytes (recomputed and matching); HISTORICAL revision 2 hash `696e58e6...e469e8`.
* Follow-up items: as above.
* Review readiness or no-handoff reason: ready for final tester closure.
* Continuation owner: parent (Squad Coordinator).
