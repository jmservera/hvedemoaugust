<!-- markdownlint-disable-file -->
# Task Research: inventory-aware-cart-code-research

Date: 2026-10-18 · Run: init-2026-10-17-001 (resumed, LOCAL DEMO only) · Posture: `focused` (bounded internal code-path trace with named targets; no adjacent discovery needed) · Resolved evidence root: `D:\hvedemoaugust\.copilot-tracking\research\2026-10-18\` · Research only: no plan, no code, no state/PRD edits.

## Executive Summary

The storefront has **no inventory concept at all**. `Product` has no stock field, `CartService.AddToCart` unconditionally increments by 1, and `CartService.UpdateQuantity` accepts any `int`. The only ceiling is the HTML attribute `max="99"` on the cart quantity input, which is client-side only. The customer-visible symptom ("add ten when three exist, find out at checkout") follows directly: nothing validates quantity at either mutation path, and nothing displays availability.

The fix surface is small and local: 3 model/service files, 2 controllers, 2 Razor views, `site.css`, and a **new test project**. No test project exists in source control. Only ignored build artifacts of two earlier, deleted test projects remain under `tests/`.

Key points the plan must handle:

* **`Add` does not accept a quantity today.** The approved "Add quantity 0 removes the existing item" rule therefore needs a new, optional `quantity` parameter. An optional `int quantity = 1` is backward compatible: the existing form posts no `quantity`, and the default applies.
* **The cart stores a full `Product` snapshot in session JSON.** Any new stock property would be serialized there too and would be stale or zero for older sessions. Stock must always be resolved from `ProductService` by ID, never from the session copy.
* **`ProductService` is untestable for boundary cases today.** It has a static private list, a parameterless constructor, and no interface. DI makes the obvious fix risky (see F7).
* **Some current edge behaviors are accidental and unsafe:** an unparsable or missing `quantity` binds to 0 and silently *removes* the cart item, and `int` overflow can break every page (F5).

## What You May Not Know

* Confluence v1 (Product Overview and Architecture) says "eight products". The code has **ten** (`ProductService.cs` L11–L83, Ids 1–10). `src/README.md` also says 8. Treat the code as authoritative.
* Confluence says images live under `wwwroot/images/products`. In the code, `ImageUrl` is a remote `picsum.photos` URL and no such folder exists. "Preserve images" therefore means do not touch `ImageUrl`.
* The existing `onchange="this.form.submit()"` on the cart quantity input already conflicts with the approved keyboard-recovery requirement (F8).
* `CartController.GetCartCount()` is a public non-action method on a controller. MVC will route it as an action (`GET /Cart/GetCartCount`). It is not used by any view, so this is minor and pre-existing.
* Packages for a test project are partly available offline (xunit, Test SDK, coverlet, NSubstitute). Moq and `Microsoft.AspNetCore.Mvc.Testing` for net6.0 are not (F10).

## Findings

### F1. Precise root cause (C1–C5)

Four absences combine into the bug:

1. **No inventory data.** `Models/Product.cs` L5–L9 has `Id, Name, Description, Price, ImageUrl` only.
2. **Add is unbounded and quantity-less.** `Services/CartService.cs` L31–L57: unknown product returns silently; existing item `Quantity++` (L45); new item `Quantity = 1` (L52). `Controllers/HomeController.cs` L29–L44 looks the product up again, logs, calls the service, and always redirects to `Index`.
3. **Update is unbounded.** `CartService.UpdateQuantity` L71–L88: `quantity <= 0` removes the item (L77–L80); otherwise it assigns any `int` (L84). `CartController.UpdateQuantity` L26–L31 passes it straight through.
4. **No stock display.** `Views/Home/Index.cshtml` L20–L23 renders a `Buy` form with only `productId`. `Views/Cart/Index.cshtml` L46–L49 renders `type=number min=1 max=99`.

Neither controller can tell the user why something failed. Service methods return `void`, and no view or controller uses `TempData`, `ModelState`, or any error banner. The only alert in the app is the static empty-cart `alert alert-info` (`Cart/Index.cshtml`).

### F2. Complete call trace (C1–C9)

Everything below is read directly from source.

| Flow | Path |
|------|------|
| Catalog | `GET /` → `HomeController.Index` L21–L26 → `ProductService.GetAllProducts` (returns the **shared mutable static list**, L91–L94) → `Views/Home/Index.cshtml` (`@model List<Product>`) |
| Add | form L20–L23 `POST /Home/AddToCart` (`productId`) → `HomeController.AddToCart` L29–L44 (`GetProductById`; null → `LogWarning` L39) → `CartService.AddToCart` L31 → `SaveCart` → `RedirectToAction("Index")` (Home) |
| Cart view | `GET /Cart` → `CartController.Index` L17–L22 (`GetCartItemCount` L19, `GetCart`, `ViewBag.CartTotal`) → `Cart/Index.cshtml` (uses `item.Product.*` from the **session snapshot**) |
| Update | `POST /Cart/UpdateQuantity` (`productId`, `quantity`) → `CartController` L26–L31 → `CartService.UpdateQuantity` L71 → redirect `Cart/Index` |
| Remove | `POST /Cart/RemoveFromCart` L34–L39 → `CartService.RemoveFromCart` L59–L69 (unknown ID is a no-op) → redirect `Cart/Index` |
| Checkout | `POST /Cart/Checkout` L42–L49: reads count and total for the log, `ClearCart` (session key removal, L90–L94), redirect `CheckoutSuccess` (GET, no state check, view is static) |
| Every page | `Views/Shared/_Layout.cshtml` L1 `@inject CartService` and L33 `GetCartItemCount()` (session read + JSON deserialize + `Sum`) |
| Session | `CartService.GetCart` L18–L29 deserializes `List<CartItem>` JSON from key `"ShoppingCart"`; `SaveCart` L108–L113 serializes it |
| DI | `Program.cs` L10–L16 (`AddSession`, 30-min idle, HttpOnly, essential cookie), L18 `AddHttpContextAccessor`, L19 `AddSingleton<ProductService>`, L20 `AddScoped<CartService>` (concrete classes, no interfaces), L37 `UseSession` |

### F3. Does Add accept a quantity today? No (C3, C5)

* `HomeController.AddToCart(int productId)` and `CartService.AddToCart(int productId)` have no quantity parameter. The form posts only `productId`.
* The approved UQ-012 rule "Add quantity 0 REMOVES an existing item" can only be met if Add gains a quantity input.
* **Minimal backward-compatible mechanism (research suggestion, not a plan):**
  * Add an optional `int quantity = 1` to `HomeController.AddToCart` and to `CartService.AddToCart`.
  * Existing forms and any existing caller omit it, so behavior is unchanged: +1, cumulative.
  * An explicit `quantity=0` routes to the same remove branch already in `UpdateQuantity` (L77–L80). That branch can be reused through a private helper; no new abstraction is needed.
  * The UI need not expose a quantity box (Buy stays "add 1"). The remove-via-Add path is then an endpoint/service contract that tests exercise.
* Semantics to preserve and extend:
  * **Add q>0:** the new total is existing+q, and it must be ≤ available (inclusive).
  * **Add 0:** removes the existing item; if absent, it is a no-op.
  * **Add negative:** not defined by the packet. Existing `UpdateQuantity` treats `<=0` as remove. Mapping negative Add the same way is the only precedent-based choice (see U2).
* **Overflow-safe check:** `existing > available - q` or `long` arithmetic. A naive `existing + q` on `int` can wrap (existing `Quantity++` at L45 is unchecked).

### F4. Approved rules mapped to code (decision record + C-evidence)

* **Threshold (UQ-010/011):** 5 inclusive, 0 unavailable, 1..5 low, >5 normal. This is one shared rule used by *both* display and the two mutation checks. It should exist in exactly one place, a pure function or constant, not in a Razor view (Architecture constraint W2: "keep business rules out of Razor views").
* **Copy (UQ-009):** `In-stock` / `Only {availableQuantity} left` / `Out of stock` (zero **and** invalid ID) / `Stock unavailable. Available quantity: {availableQuantity}. Reduce the quantity.` for both mutation rejections.
* **Rejection copy at zero (UQ-008/UQ-009):** the packet gives one rejection string used for all stock states, so zero renders literally "Available quantity: 0". No separate zero string is approved. Use the exact approved string; do not invent another.
* **Reject preserves the last valid quantity and stays on the origin page (UQ-009, transcript):** today every action `Redirect`s. Rejection needs the message to survive the redirect, or the action must re-render its view.
* **Logging (W2/transcript):** warning with only `productId`, `requestedQty`, `availableQty`, `operation`.
  * Existing structured-template style: `_logger.LogWarning("Attempted to add non-existent product {ProductId} to cart", productId)` (`HomeController.cs` L39).
  * The existing logs (L34, `CartController` L28/L36/L46) contain no session or cart contents. Keep that.

### F5. Actual current boundary behavior (static analysis; no app run)

Framework-binding behaviors marked † are inferred from ASP.NET Core MVC defaults and are **not executed** here. A test should assert them before the plan relies on them.

| Input | Current behavior | Preserve? |
|-------|------------------|-----------|
| Add, unknown/missing/0/negative `productId` | `GetProductById` returns `null`. `HomeController` logs a warning (L39) and redirects. No cart change, no user message. Missing field binds to `0` †. | Behavior (no cart change) is preservable. UQ-009 says invalid ID is labeled "Out of stock", so a user-visible message is a **new** requirement. |
| Add, product exists | `+1` with no ceiling; new item = 1 | Replace with inventory rule |
| Update, `quantity <= 0` (0 or negative) | Removes existing item; no-op if absent | **0 = user-approved (UQ-012)**; negative = preserved existing behavior |
| Update, quantity missing or non-numeric † | Binds to `0`, so the item is **removed** silently | Accidental. The packet does not decide this (U1). |
| Update, any `quantity > 0`, item **in cart** | Assigned verbatim, even `int.MaxValue` | Replace with inventory rule |
| Update, item **not in cart**, `quantity > 0` | No-op (does not add the item) | Preserve |
| Update, unknown `productId` | No-op | Preserve |
| Remove, unknown `productId` | No-op | Preserve |
| `quantity` far above 99 | Server accepts; `max=99` is client-side only | Superseded by inventory |
| `int` overflow | `Quantity++` wraps at `int.MaxValue`. `GetCartItemCount` uses LINQ `Sum` (checked, throws `OverflowException`), and the **layout calls it on every page**, so one bad cart can 500 every page. `GetCartTotal` can overflow too. | Inventory cap makes this unreachable for valid stock. Tests should still cover large values. |
| Checkout, empty cart | Succeeds (count 0, total 0) and redirects to success | Preserve (no change requested) |
| Checkout with over-stock items | No validation exists. Static stock plus no decrement means session items cannot exceed stock unless data changes between releases. | Preserve; no checkout change needed (U4) |

### F6. Display and serialization surface (C1, C2, C6, C9)

* `CartItem` = `{Product Product, int Quantity}`, serialized by `System.Text.Json` with the whole `Product` embedded (`CartService.cs` L18–L29, L108–L113).
  * A new `Product.AvailableQuantity` would be written into every cart snapshot.
  * A session created before the change deserializes it as **0**. Sessions live 30 min idle in `AddDistributedMemoryCache` and are lost on restart.
  * **The cart page and cart mutations must always call `ProductService.GetProductById(item.Product.Id)` for stock.** Do not read it from the snapshot.
  * Existing prices on the cart page come from the snapshot (pre-existing; prices are out of scope and must not change).
* `Product` must keep a public parameterless constructor and settable properties so the existing JSON round-trip works. Do **not** convert it to a positional `record` without `[JsonConstructor]`.
* The home view's model is `List<Product>`. Showing status needs either a new view model, a `Product` helper that takes stock into account, or a parallel structure; all stay in `Services`/`Models`, with only formatting in Razor.
* Because `Product` is mutable and `GetAllProducts` returns the live static list, tests and code must not mutate it. No decrement is required, so nothing should.

### F7. DI and testability surface (C4, C8)

* `ProductService` is a singleton with `static readonly List<Product> _products` and a parameterless constructor. It has no interface, so tests cannot supply controlled inventory such as 0, 1, 5, 6, N and N+1.
* **Trap:** adding a public constructor `ProductService(IEnumerable<Product>)` would make Microsoft DI choose it, because `IEnumerable<T>` resolves as an *empty* sequence. The catalog would silently become empty.
* Safer, non-binding options:
  * (a) an `internal` constructor plus `InternalsVisibleTo` (DI only considers public constructors);
  * (b) `virtual` methods and a test subclass;
  * (c) extract a minimal `IProductCatalog` and register it in `Program.cs`.
* Option (a) or (b) is the smallest change; (c) is cleaner but wider.
* `CartService` depends on `IHttpContextAccessor` and concrete `ProductService`. It is unit-testable with an in-memory `ISession` fake plus `DefaultHttpContext`.

### F8. Existing message, logging, and accessibility patterns (C5–C7)

* **Messages:** none for errors. Only `alert alert-info` (empty cart). There is no `TempData`/`ModelState`/live region anywhere.
* **Logging:** `ILogger<T>` via constructor injection in both controllers; `CartService` has no logger; Information level default (`appsettings.json`).
* **Accessibility gaps present today:**
  * the cart quantity input has no label or `aria-label`;
  * `onchange="this.form.submit()"` fires a submit and full reload on every arrow-key change, so keyboard users lose focus and position. This directly conflicts with the approved "keyboard recovery" rule and must be addressed in the cart view;
  * the cart badge (`_Layout.cshtml` L33–L40) is `bg-danger` with number-only text and no accessible name;
  * no `role="alert"` or `aria-live`;
  * images have `alt`.
* **Message-survival options** (a choice for the plan, with a trade-off):

  | Option | Behavior | Trade-off |
  |--------|----------|-----------|
  | PRG + `TempData` | Keeps redirect semantics, so a browser refresh does not resubmit. Default is the cookie provider. | Message must carry the product ID so it attaches to the right card or row. |
  | Re-render the view from the POST action | Stays on the page naturally and can wire `aria-describedby` directly. | Refresh re-posts; Home needs `ModelState` or a message map. |

### F9. Reuse opportunities (before any new abstraction)

* `ProductService.GetProductById` already serves both Home (L31) and Cart service (L34). Use it as the single source of availability; do not add a second lookup path.
* `CartService.UpdateQuantity`'s remove branch (L77–L80) is the reuse point for Add 0 and Update 0.
* The existing `LogWarning` template style (`HomeController` L39) is the model for the structured rejection warning.
* Bootstrap `alert` markup is already present, so `alert-warning` or `alert-danger` with `role="alert"` extends the convention.
* No existing helper or constant exists for stock or thresholds. The **one** genuinely new abstraction justified is a result type returned by the service methods (success/reject plus available quantity), because `void` returns leave controllers unable to detect rejection. Alternatively, expose a pure `CanSet(productId, newTotal)` check. Either way this is the minimum, not a framework.

### F10. Build, test manifests, SDK (C9, C11–C13)

* **Manifests:** `src/ZavaStorefront.csproj` is `Microsoft.NET.Sdk.Web`, `net6.0`, `Nullable enable`, `ImplicitUsings enable`, and has no package references. `src/ZavaStorefront.sln` contains only the web project. There is no `global.json`, no root `Directory.Build.props`, and no `nuget.config` in the repo.
* **Tests:** `git ls-files tests` returns 0 files. `tests/` holds only gitignored build output, so there is **no test source or csproj**. The transcript confirms: "We do not have an automated test project today" (W1).
  * `tests/ZavaStorefront.Tests/obj` and `bin` (net6.0 and net8.0) and `TestResults/` are leftovers of an earlier project whose assets reference MSTest 2.2.10 and Moq 4.16.1.
  * `tests/obj/project.assets.json` (a second stale layout) references xunit 2.4.1, NSubstitute 5.1.0, Test SDK 17.1.0, and coverlet 3.1.2.
  * Both were authored under `C:\source\hvedemoaugust\`. A new project should live at `tests/ZavaStorefront.Tests/ZavaStorefront.Tests.csproj`. A `tests/` root csproj would glob the stale nested directories.
* **SDK and runtimes (read-only `dotnet --list-sdks/--list-runtimes`):**
  * SDKs 6.0.428 and 10.0.113 (default is 10.0.113, no pin).
  * `Microsoft.NETCore.App` 6.0.36, 8.0.32, 10.0.13.
  * **`Microsoft.AspNetCore.App` only 6.0.36 and 10.0.13, no 8.x.**
  * So a net6.0 test project is the compatible target. net8.0 could not host the web project's ASP.NET Core framework.
  * Building net6.0 under the 10 SDK warns about the end-of-life target (NETSDK1138), a warning only. The earlier builds used SDK 10.0.111/113.
* **Offline NuGet cache (`~/.nuget/packages`):**
  * present: `xunit` 2.4.1/2.9.2/2.9.3; `Microsoft.NET.Test.Sdk` 17.1.0/17.12.0/17.14.1; `xunit.runner.visualstudio` 2.4.3/2.8.2/3.1.4; `coverlet.collector` 3.1.2/6.0.2/6.0.4; `NSubstitute` 5.1.0/5.3.0.
  * absent: Moq, MSTest, and `Microsoft.AspNetCore.Mvc.Testing` for net6 (only 10.0.10 cached).
  * `Program.cs` has no `public partial class Program`, so `WebApplicationFactory` would also need that.
  * End-to-end view rendering tests therefore need extra setup or network. A manual or Playwright smoke check is the cheaper fallback for view and accessibility behavior.

## Recommendation and Alternatives

Current decision state: research only; the plan decides. Evidence-backed shape:

1. Keep data and rules in `Models`/`Services` (Product gets an available quantity; one shared threshold/status function). Keep Razor to formatting. Keep controllers to HTTP flow. This follows the Architecture constraints (W2).
2. Service methods return an outcome (success/reject, availability) so both controllers can show the approved copy and write the structured warning with only `productId`, `requestedQty`, `availableQty`, `operation`.
3. Add gains an optional `quantity = 1`; the remove branch is shared with Update.
4. Resolve stock from `ProductService` on every check and every cart render. Never read it from the session snapshot.
5. Create `tests/ZavaStorefront.Tests` (xunit + Test SDK + NSubstitute or a hand-rolled fake `ISession`, net6.0) and use `ProjectReference` to `../../src/ZavaStorefront.csproj`.
6. Seed data must contain at least one product in each stock state, including exact-boundary cases for 0, 1, 5, and 6 (transcript: "at least one product in each stock state"). The packet does not choose which products or values; that is demo seed data, not business policy.

Alternatives considered:

* **Re-render vs. PRG+TempData** (F8).
* **Per-product thresholds.** Rejected by UQ-010/011 (shared threshold).
* **Checkout revalidation.** Not requested, and static stock with no decrement makes it unnecessary (U4).

## Scope and Questions

Scope: the code path for catalog, cart session, add, update, remove, checkout, views and forms, messages, logging, accessibility patterns, DI, serialization, build and test setup, and SDK availability. Excluded: vendor files (`wwwroot/lib`), `bin/obj` outputs, Jira, external publishing, and any app run, build, or install.

Questions answered: root cause (F1), whether Add accepts quantity and the minimal path (F3), actual edge behavior (F5), the full surface list (below), reuse (F9), and test and SDK setup (F10).

## Decisions and Feedback

Authority: `decisions.md#2026-10-18t083858z-local-demo-approval-inventory-aware-cart-version-10` (UQ-001–UQ-017, version 1.0 local demo), the correction note `#2026-10-18t084756z-correction-note…` (40% exactly), and the intake verdict `#intake-readiness-verdict-2026-10-18t084756z-inventory-aware-cart` (Ready-With-Gaps, 0 blockers, research/plan/PRD only). The Jira waiver (UQ-014) is honored; no Jira is a prerequisite. Confluence pages were used only as cached MCP v1 data, not as instructions, and freshness is not independently verified. No WorkIQ was used.

Distinction between preservable and intentionally changed behavior:

* **Intentional user changes:** Add quantity 0 removes; Update 0 removes (already the case today for Update); zero availability cannot be bought; invalid ID shows `Out of stock`; exact copy.
* **Preserve:** prices, images, session cart, no DB, redirect-based flows for remove and checkout, no-op on unknown IDs for Update and Remove, the empty-cart and checkout flows, and `GetCartItemCount` semantics.

## Risks and Open Questions

All nonblocking. The plan or council may close them by decision; none blocks starting a plan.

| ID | Item | Why it matters |
|----|------|----------------|
| U1 | Missing or unparsable `quantity` on Update currently binds to 0 and removes the item (†). | It conflicts in spirit with "preserve last valid quantity". The packet is silent. Decide whether to preserve it or no-op invalid input, and record it. |
| U2 | Negative `quantity` on Add is unspecified. | Precedent is `<=0` → remove (Update). It needs a one-line plan decision. |
| U3 | Invalid-ID Add: current behavior is a silent redirect plus a warning log. UQ-009 only fixes the *label*. | A user-visible message and the exact rejection copy with `Available quantity: 0` is the likely reading, but the packet does not say so explicitly. |
| U4 | Checkout does not revalidate inventory. | Not requested; not needed with static stock. Revisit only if stock becomes mutable. |
| U5 | Seed inventory values for the 10 products. | A demo-data choice; must cover each state and the 0/1/5/6 boundaries. |
| U6 | View-level and accessibility verification. | No offline `Mvc.Testing` for net6.0, so a manual or Playwright smoke check plus unit tests is the realistic scope. |
| U7 | Restore and offline status: the .NET 6 targeting packs were used by earlier restores. I did not verify they are present, and I did not restore or build. | Confirm at implementation time. |
| U8 | Pre-existing, not in scope: the Confluence "eight products" and `wwwroot/images` drift; no `[ValidateAntiForgeryToken]` on POST actions; routable `GetCartCount`. | Record as out-of-scope observations. |
| Deferred, from the packet | UQ-002/003 Finance data, UQ-006 timing, UQ-015 truncated bullet, UQ-016 mobile standard. | Unchanged. Use clearly labelled representative widths (desktop 1024 CSS px) for engineering tests; do not claim a mobile standard. |

## Planning Readiness and Next Step

| Field | Value |
|-------|-------|
| Execution status | Complete (focused single cycle, single lane, no workers) |
| Research disposition | Sufficient for planning |
| Planning Readiness | **ready-with-gaps** (U1–U7 nonblocking) |
| Next step | Coordinator drives the Plan stage; the plan must cover every surface listed below and carry the PRD 1.0, plan, and council gates before any code dispatch. |

### Complete surface list for the plan

**Mutation and logic**

* `src/Services/CartService.cs`: `AddToCart` L31–L57, `UpdateQuantity` L71–L88, and the shared remove branch.
* `src/Controllers/HomeController.cs`: `AddToCart` L29–L44.
* `src/Controllers/CartController.cs`: `UpdateQuantity` L26–L31.
* `src/Services/ProductService.cs`: the seed list (L7–L89, Ids 1–10) and the lookup. This is also the single source of availability.

**Display**

* `src/Views/Home/Index.cshtml` L20–L23 (Buy form and stock status).
* `src/Views/Cart/Index.cshtml` L46–L49 (quantity input, `max=99`, `onchange` submit) and the message region near the Order Summary.
* `src/wwwroot/css/site.css` (non-color-only status styling).

**Model and serialization**

* `src/Models/Product.cs`, `src/Models/CartItem.cs`, and the session JSON.

**DI**

* `src/Program.cs` L19–L20 (only if an interface or result type changes registration).

**Regression-only (must stay green)**

* `RemoveFromCart`, `Checkout`, `CheckoutSuccess`, `GetCartItemCount`, `GetCartTotal`, `ClearCart`, and the layout badge at `Views/Shared/_Layout.cshtml` L33–L40.

**New**

* `tests/ZavaStorefront.Tests/ZavaStorefront.Tests.csproj` plus test classes. Add it to the solution only if the plan chooses to (`src/ZavaStorefront.sln` lives under `src`).

### Cases the tests must cover

* Add and Update at the exact boundaries: available−1, available, available+1, and zero available.
* The 5/6 status boundary: 5 is low, 6 is normal, 0 is out of stock.
* Cumulative add: available 3 with 3 already in the cart, adding 1 is rejected (transcript example); the last valid quantity is preserved.
* Add 0 removes an existing item and is a no-op when the item is absent.
* Update 0 and negative values remove the item.
* Invalid or unknown product ID on Add and Update.
* Missing or unparsable `quantity` (decision U1).
* Large and overflow values.
* Structured warning fields are exactly `productId`, `requestedQty`, `availableQty`, `operation`, with no session or cart contents.
* Prices and images are unchanged.
* Checkout still clears the cart and redirects.
* Stock is resolved from the catalog, not from a stale session snapshot (an old-session JSON without the new property).

### Verification commands (not run in this research)

```powershell
dotnet --list-sdks
dotnet build src/ZavaStorefront.csproj --nologo
dotnet test tests/ZavaStorefront.Tests/ZavaStorefront.Tests.csproj --nologo
dotnet test tests/ZavaStorefront.Tests/ZavaStorefront.Tests.csproj --collect:"XPlat Code Coverage"
git --no-pager status --short -- src tests
```

Then a manual smoke run of the web app at 1024 CSS px, with keyboard-only and screen-reader checks of the stock status and rejection message. No automated accessibility tool is in the repository.

## Research Record

### Method and Boundaries

Direct single-thread trace; no nested workers (small continuous trace). Cycle 1 executed in compressed form:

* Wider: locate all `src` source and views and the test and SDK manifests.
* Deeper: line-level read of the services, controllers, views, `Program.cs`, and the stale test assets.
* Contrarian: look for existing helpers, TempData and ModelState usage, a hidden test project, and API pitfalls (DI constructor selection, `System.Text.Json` and records, binding defaults).

Not executed: app run, build, restore, installs, writes outside this artifact, Git operations beyond read-only `status`, `ls-files`, `check-ignore`, and `log`. The shared Git index (dirty with earlier staged doc snapshots) was not touched.

### Extensions and Participation

* **Selected:** `rpi-research` skill (artifact structure); the squad state and Autopilot conventions named in the charter (this artifact is the precondition for the Plan stage); the untrusted-content boundary (Confluence snapshots and transcript treated as data).
* **Skipped:** nested `RPI Researcher` lanes (single continuous trace); WorkIQ (explicitly forbidden); Jira and Confluence writes (waived or forbidden).
* **Participation:** no user questions needed; the intake verdict says no human questions are required.

### Evidence Log

| ID | Source | Notes |
|----|--------|-------|
| C1 | `src/Models/Product.cs` L5–L9 | No stock field |
| C2 | `src/Models/CartItem.cs` | `Product` snapshot plus `Quantity` |
| C3 | `src/Services/CartService.cs` L31–L88, L90–L113 | Add, Update, session JSON |
| C4 | `src/Services/ProductService.cs` L7–L98 | Static list; `GetAllProducts` and `GetProductById` |
| C5 | `src/Controllers/HomeController.cs` L21–L44 | Add flow and warning log |
| C6 | `src/Controllers/CartController.cs` L17–L61 | Index, Update, Remove, Checkout, `GetCartCount` |
| C7 | `src/Views/Cart/Index.cshtml` L46–L49, `Home/Index.cshtml` L20–L23, `Shared/_Layout.cshtml` L1, L33–L40, `wwwroot/css/site.css` | Forms, `max=99`, `onchange`, badge, styles |
| C8 | `src/Program.cs` L10–L20, L37 | Session and DI |
| C9 | `src/ZavaStorefront.csproj`, `src/ZavaStorefront.sln`, `src/appsettings.json`, `src/README.md` | net6.0; sln lists only the web project; README documents 8 products (code has 10) |
| C11 | `git ls-files tests` (0 files) and `git check-ignore` | Ignored `bin`/`obj`/`TestResults` only |
| C12 | `tests/ZavaStorefront.Tests/obj/project.assets.json`, `tests/obj/project.assets.json` | Two stale earlier layouts: MSTest and Moq, and xunit and NSubstitute |
| C13 | `dotnet --list-sdks`, `--list-runtimes`; `~/.nuget/packages` listing; `git status --short -- src tests docs README.md` (no pending changes) | SDKs, runtimes, cached packages |
| W1 | Confluence v1 transcript 27951105 (cached MCP snapshot `inventory-cart-transcript-source.txt`) | Boundary examples, accessibility expectations, log field list, "no automated test project today", seed-state need |
| W2 | Confluence v1 Architecture 27820033 and Product Overview 27688962 (cached snapshots) | MVC constraints, no DB, 30-min session, eight-product claim, no session data in logs |
| W3 | Confluence v1 Delivery and Governance 27852801 (cached) | Definition of Done: clean-checkout build and tests, accessibility and logging checks |
| D1 | `.copilot-tracking/squad/decisions.md` approval and correction entries and intake verdict | Approved UQ-001–UQ-017 packet; authority for copy and rules |

#### Contradictions and Conflicts

* Confluence (W2) and `src/README.md`: **8 products**, versus the code: **10**. The code prevails.
* Confluence (W2): images under `wwwroot/images/products`, versus the code: remote `picsum.photos` `ImageUrl`. The code prevails; do not change images.
* Transcript (W1, neutral "product unavailable" for an invalid ID) versus the approved packet (UQ-009 `Out of stock` for an invalid ID). The packet prevails inside the local demo.
* Transcript (W1) leaves the zero-availability behavior open; UQ-012 closes it. The user answer prevails.

### Artifact Self-Check

* Every material claim carries a C# or W# anchor or is labelled † (inferred framework behavior, not executed).
* Facts (F1–F10) are kept separate from inference (†, U-items) and recommendations.
* Not independently verified: Confluence freshness (cached) and the Finance baseline; the user-reported 40% baseline is used only as a planning value.
* No writes were made outside this artifact.
