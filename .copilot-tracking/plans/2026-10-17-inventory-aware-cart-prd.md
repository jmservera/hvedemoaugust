<!-- markdownlint-disable-file -->
# Inventory-Aware Cart — User-Approved Local-Demo PRD v1.0 — amended by approved local-demo A1

| Document control | Value |
|---|---|
| Document ID / version | PRD-inventory-aware-cart / 1.0 local-demo requirements; amended by approved local-demo A1 (no version bump) |
| Run / authoring role | init-2026-10-17-001 / analyst Primary (PRD Builder) |
| Lifecycle | Validate — actual amended PRD reviewed: NEEDS_REVIEW; Validate APPROVED_WITH_COMMENTS; Finalize NOT_EVALUATED [ACTUAL-REVIEW]. User-approved local-demo v1.0 plus A1 unchanged; implementation awaits revised full-plan critique/council gates |
| Authority | Requesting user, self-declared authority; no name or organizational/PM role supplied or verified. LOCAL DEMO ONLY; not production authority, a BRD, engineering commitment, or code authorization |
| Boundary A | Ready-With-Gaps, zero blocking gaps for PRD refresh, research, and implementation planning under [current intake][INTAKE] |
| Boundary B | B1–B5 locally resolved/approved with recorded deferrals; B6 locally waived. Code dispatch still awaits matching PRD, research, plan, and council gates |
| Classification | Not confirmed; handle as potentially confidential |
| Artifact date | 2026-10-17 naming convention; authored 2026-10-18 local time, not a delivery date |
| Deliverable root | `.copilot-tracking/plans/` |
| Historical product evidence | [Original research: 157 items](../research/2026-10-17/inventory-aware-cart-analysis.md); unchanged Confluence v1 provenance |
| Evidence-input SHA-256 | `9a7906b94f572bc6a7e7ccaa90ef6ebce20112625a75d6166586e10fd3e29bb3` |
| Local approval / correction | [Approved packet][APPROVAL] (2026-10-18T08:38:58Z); [correction][CORRECTION] (2026-10-18T08:47:56Z); formal UQ-001–UQ-017 |
| Separate amendment approval | [Approved local-demo A1][A1] (2026-10-18T10:50:56Z), requesting user; invalid-quantity behavior and exact message only; not an original UQ answer or Confluence v1 claim |
| Descriptive engineering evidence | [Code research, read-only](../research/2026-10-18/inventory-aware-cart-code-research.md); 31,019 bytes; not product-decision authority |
| Source BRD / feasibility handoff | None supplied; no BRD v1.0 or feasibility approval claimed |
| Product owner / reviewers / release date | No named PM assigned; actual Validate review persisted [ACTUAL-REVIEW], not a new review by this metadata update; no deployment/release date authorized |
| Sole companion | [2026-10-17-inventory-aware-cart-prd.state.json](2026-10-17-inventory-aware-cart-prd.state.json); binding root `.copilot-tracking/plans/` |

## Executive Summary

The user approved the exact local-demo packet after all 17 questions: **“Approve this exact local-demo decision packet as version 1.0; record it in the repo and resume the RPI loop”** ([APPROVAL], UQ-013). This refresh expresses that packet as PRD v1.0; it does not assert implementation, deployment, publication, Confluence approval, or formal PRD quality scores.

This actual PRD is now **amended by approved local-demo A1**, retaining version **1.0**. The user's separate approval at 2026-10-18T10:50:56Z requires omitted Add quantity to default to 1; explicit 0 to remove an existing item on Add/Update; and negative, nonnumeric, overflowing, or missing Update quantities to leave the cart UNCHANGED, show the ANNOUNCED message `Enter a valid whole-number quantity of zero or more.`, and log only safe structured numeric/nullable context. Negative Update no longer removes an item. This supersedes earlier current-policy omissions, not historical source questions or original approval choices. [A1] is user authority, not critique advice or Confluence v1 evidence.

The customer problem is a quantity promise contradicted later. Approved local-demo behavior covers pre-add stock information, shared inclusive add/edit limits, cumulative adds, zero-quantity removal on both paths, and rejection that preserves the last valid cart and stays on the originating page. Validation neither reserves nor decrements stock. Historical evidence: G-003–G-005, N-006–N-011, D-001, D-003–D-007, C-006, C-007; current authority: [APPROVAL], UQ-007–UQ-013.

Primary outcome: improve completed demo checkout confirmations, not payment/orders. Planning baseline is exactly **40%**, user-reported for the last **6 months**, unverified; target **60%** is user-selected, not a validated outcome promise. Finance is accountable using aggregate existing data; review **3 months after deployment** is timing, not deployment authorization. Exact report/date range remain deferred. [APPROVAL], [CORRECTION], UQ-001–UQ-006, UQ-017.

Historical Research + documentation Plan P01–P03 are complete; the [old plan](2026-10-17-inventory-aware-cart-plan.md), Finalization Record, and [closure](../reviews/2026-10-17-inventory-aware-cart-plan-closure.md) supply methodology only, not code authorization. No concurrent new code-plan file was read. The prior business-question hold is locally resolved and replaced by `inventory-cart-v1-artifact-validation-pending`. [INTAKE] permits refresh/research/planning with zero blocking intake gaps; actual Validate review is NEEDS_REVIEW / APPROVED_WITH_COMMENTS [ACTUAL-REVIEW], while matching revised full-plan critique/council gates remain pending. No code, tests, tracker writes, publication, Git index action, commit, or deployment is performed by this refresh.

## Product Context

### Baseline versus proposal

| Layer | Source-preserving account | Evidence / status |
|---|---|---|
| Baseline experience | Descriptions/prices; add/review/edit/remove; account-free checkout confirmation; session-bound cart. | N-001–N-003, M-001–M-003, A-002; baseline context / source assumption |
| Operating posture | Responsive experience; limited dependencies/data handling; controlled catalog, observable flows, bounded state. | G-001, G-002, N-004, N-005; baseline context |
| Historical baseline exclusion | Inventory availability outside the Confluence v1 release; not a prohibition on a separately approved local extension. | C-001; unchanged historical boundary |
| Approved local-demo extension | Pre-add stock information, shared inclusive/cumulative validation, valid-state preservation, exact invalid-ID copy `Out of stock`, accessible recovery. | N-007–N-011, D-003–D-007; [APPROVAL], UQ-007–UQ-013 |
| Preserved semantics | Validation only; checkout confirms/clears cart, without a commercial transaction. | D-001, D-002, C-006, C-007, A-004; agreement/source assumption |
| Descriptive catalog discrepancy | Confluence v1 describes eight products/static image paths. Code research reports ten actual products (IDs 1–10) and remote URLs in `ImageUrl`, not those static assets. Preserve the existing actual products, prices, and images; do not reduce the catalog to eight or replace images. | Code research, What You May Not Know / F1 / F6; [APPROVAL] preserved constraints. Code facts do not override product authority |
| Evidence limits | No production-baseline validation or runtime verification in this refresh. Finance's baseline remains user-reported; research suggestions are not new requirements. | R-011, UQ-003; [CORRECTION] |

### In scope and out of scope

**Approved local-demo scope:** static-catalog/session-cart validation on both mutation paths, accessible recovery, bounded structured warnings, representative stock-state review, preserved actual catalog/prices/images/checkout. No delivery commitment. D-001–D-009, C-008, C-009, C-014–C-029, DEP-001, V-001–V-024; [APPROVAL], UQ-007–UQ-014, UQ-016–UQ-017.

**Preserved exclusions:** no database, reservation, decrement, catalog administration, warehouse synchronization, payment change, or persistent inventory. A-006 remains an Engineer feasibility assertion, not proof. C-006, C-007, C-010–C-013; [APPROVAL] Authority Layers A.

**Baseline exclusions, not new decisions:** accounts/authentication, persistent carts/orders, tax/shipping/discounts/fulfillment, personalized recommendations/customer analytics. C-002–C-005.

**Refresh exclusions:** no direct source-code/runtime investigation, code/test edits or execution, new plans, squad-state writes, publishing, Jira/Confluence mutations, Git index alterations/commits, or deployment. Only this PRD and its already relocated companion may be written. Shared Confluence production governance is unchanged; the local Jira waiver is not a production waiver.

## Users and Personas

Roles below are needs, not demographic personas or assigned owners.

| User / role | Job and pain point | Source-stated success intent | Evidence |
|---|---|---|---|
| Shopper | Understand products/prices; manage cart without late quantity surprises. | Informed choice, clear confirmation, recoverable rejection. | N-001–N-003, N-006–N-009, G-004, G-005 |
| AT / keyboard shopper | Perceive rejection and recover without losing place. | Announcement, association, keyboard recovery; no ARIA/focus mechanism selected. | N-010, N-011, C-016–C-018 |
| Product / operations reviewer | Evaluate controlled catalog and stock states. | Each state represented; boundary results understandable unaided. | N-004, N-012, G-006, G-007 |
| Engineering / support | Diagnose consistent limits. | Actionable feedback/technical context without customer/session exposure. | N-005, N-008, M-005, C-021–C-029 |

### Journeys and mutation paths

| Journey | Approved local-demo flow | Current authority / limits |
|---|---|---|
| Browse/add | Descriptions/prices → approved stock label → omitted quantity defaults to 1 → validate positive cumulative line quantity. Explicit Add quantity 0 removes an existing item. Invalid quantities follow A1 without cart mutation. | Low-only counts; rejection-count exception in all states. UQ-007–UQ-012; [A1]; N-001, N-007, D-003, D-005, D-006 |
| Edit/recover | Edit → same inclusive rule for valid positive quantity → accept or reject/retain last valid cart → nearby announced feedback; stay on originating page. Explicit Update quantity 0 removes the item; negative, nonnumeric, overflowing, or missing Update quantity leaves the cart unchanged under A1. | Exact copy approved; programmatic/keyboard recovery remains required. UQ-008–UQ-013; [A1]; N-002, N-008–N-011, C-019, C-020, D-007 |
| Invalid identifier | Mutation → `Out of stock`; no internal error. | Explicit user override of earlier neutral wording, not a Confluence v1 change. UQ-009; D-004, C-021 |
| Checkout | Confirm/clear session cart; no reservation/decrement. | No guarantee/payment/commercial-transaction expansion. D-001, D-002, C-006, C-007, A-004 |

## Design Decisions

Stable organizational families `CFR`, `CNFR`, `CCON`, `CAC`, `PG` are retained from v0.1 for trace continuity. Their historical candidate prefixes do not mean currently unapproved: the current statements below are **approved for local-demo v1.0 only, amended by approved local-demo A1**, not formal production approval. No research ID or historical evidence state is promoted or renamed.

| Record | Current local-demo decision | Disposition and evidence |
|---|---|---|
| Validation semantics | Validation only, not reservation; checkout clears cart without decrement. | Preserved local scope: D-001, D-002, C-006, C-007; [APPROVAL] |
| Rule consistency | Same rule for add/edit; equality valid; cumulative over-limit adds invalid; rejected update retains valid state. | Recorded direction/boundaries: D-003, D-005–D-007 |
| Identifier handling | `Out of stock` for invalid IDs and zero stock; no internal error. | Explicit local override: UQ-009, [APPROVAL]; D-004 remains historical |
| Focused tests | Focused automated verification was agreed; test-project enablement is meeting-stated, not repository-verified. | D-008, DEP-007, R-011 |
| Logging endorsement | Immediate 19:34 endorsement belongs to the preceding bounded warning-log proposal. | Recorded immediate approval of that proposal only: D-009; not approval of this PRD or story |
| Primary outcome | Improve completed demo checkout confirmations, not payment or commercial orders. Other alternatives not selected. | UQ-001–UQ-006, UQ-017, [APPROVAL], [CORRECTION] |
| Disclosure | Exact counts only for low stock in routine display; explicit exception allows exact available counts in ALL inventory-limit rejections, including zero, low, and normal states. | UQ-007–UQ-009, [APPROVAL] |
| Low-stock policy | One global threshold: 5 inclusive; 0 unavailable takes precedence; 1..5 low; >5 normal. | UQ-010–UQ-011, [APPROVAL] |
| Zero behavior | Add 0 and Update 0 both REMOVE an existing cart item; stock 0 cannot be bought. | UQ-012, [APPROVAL]; not inferred from old code |
| Invalid-quantity behavior | Omitted Add defaults to 1; negative/nonnumeric/overflowing quantities on either path and missing Update quantity preserve the cart, announce the exact A1 message, and produce safe numeric/nullable structured warning context. Negative Update does NOT remove. | Separate explicit user approval [A1]; replaces earlier negative-Update removal, not the original UQ answers. Critique's no-log/no-op and silent-malformed advice not adopted |
| Approval and permission | Requesting user's local-demo approval; no Jira story exists; bounded local Jira waiver. Refresh/research/planning allowed; code awaits RPI/council gates. | UQ-013–UQ-014, [APPROVAL], [INTAKE]; shared production governance unchanged |

Historical exact-count suggestions were not approval. Current disclosure permission comes from the requesting user, UQ-007–UQ-009 and [APPROVAL], including the explicit rejection exception; it is not attributed to Confluence v1. Page-context recovery is now locally required by the exact approved packet, not elevated on the strength of C-020 alone.

### Approved Local-Demo Amendment A1

* **Provenance:** requesting user, self-declared authority; separate [A1] approval at **2026-10-18T10:50:56Z**, after critique findings PC003/PC004/PC009 exposed the gap. No organizational role or named approver is invented.
* **Exact approved responses:** `invalid_quantity_message` = `Enter a valid whole-number quantity of zero or more.`; `quantity_validation_approval` = `Approve this invalid-quantity behavior and message`.
* **Current policy:** explicit 0 removes an existing item on Add and Update; omitted Add defaults to 1. Negative, nonnumeric, overflowing quantities on either path and missing Update quantity leave the cart UNCHANGED, show the ANNOUNCED exact message, and are logged safely using structured numeric/nullable values only. NEVER log raw input strings or session/cart/customer data.
* **Intentional replacement:** negative Update no longer removes the item. Earlier current text saying the negative/numeric-input policy was unselected is superseded by A1. Neither “negative Add no-op, no log” nor “silent malformed accepted risk” was approved.
* **Preservation:** all 157 historical trace rows, all 17 historical question texts and original answers/choices, original v1 approval, stock/copy/threshold decisions, Finance 40% → 60% / 3-month review / aggregate privacy, catalog constraints, local Jira waiver, and local-only authority remain unchanged. A1 is not a Confluence v1 claim.
* **Review boundary:** actual amended PRD and sole companion have the persisted paired Validate assessment [ACTUAL-REVIEW]: NEEDS_REVIEW; Validate APPROVED_WITH_COMMENTS; Finalize NOT_EVALUATED. This metadata update is not a fresh assessment, phase advancement, code authorization, or RPI critique/council approval.

## Product Goals

| Goal | Approved local-demo intended result | Measurement / priority |
|---|---|---|
| PG-001 | Preserve the simple, responsive, bounded actual catalog/cart experience. G-001, G-002; [APPROVAL]. | M-001–M-004 / CAC-011–CAC-016, CAC-024; desktop 1024 CSS px; mobile standard deferred. No priority order invented. |
| PG-002 | Improve completed demo checkout confirmations through clearer stock information and recoverable quantity handling. G-003–G-005; UQ-001. | Exactly 40% user-reported baseline, last 6 months; 60% user-selected planning target; Finance aggregate existing data, review 3 months after deployment. UQ-002–UQ-006, UQ-017. Not a validated promise. |
| PG-003 | Demonstrate each stock state and understandable inclusive boundary results. G-006, G-007; UQ-010–UQ-012. | M-005, M-006 / CAC-001–CAC-010, CAC-019–CAC-020; no stock values assigned to particular products here. |
| PG-004 | Preserve evidence-to-decision-to-delivery traceability without misrepresenting local authority. G-008, G-009; UQ-013–UQ-014. | Local approval/waiver recorded; formal refresh review and implementation gates pending. No BRD v1.0, production approval, or new release cutline inferred. |

The outcome, planning values, accountable department, and review timing are locally selected. Evidence strength remains limited: exact Finance dataset/report and baseline dates are deferred, and neither baseline nor target is independently validated. Priority ordering and a deployment date are not supplied.

## Functional Requirements

All six current statements are **user-approved local-demo v1.0 requirements, amended by approved local-demo A1**. Original choices cite [APPROVAL]/UQ IDs; quantity-input amendments cite [A1] separately. Historical research citations retain their original authority.

| ID | Actor and trigger → approved local-demo outcome | Current business status / provenance | Goals / checks / research |
|---|---|---|---|
| CFR-001 | Before add, show `In-stock` for >5, `Only {availableQuantity} left` for 1..5, and `Out of stock` for 0. Use one global threshold of 5 inclusive; zero precedence; stock 0 cannot be bought. | Approved locally: UQ-007, UQ-009–UQ-012, [APPROVAL]. No particular control mechanism or product stock assignment selected. | PG-002, PG-003; CAC-010, CAC-013, CAC-019, CAC-020; N-007, C-014, V-010, V-013, V-019, V-020 |
| CFR-002 | On Add, default an omitted quantity to 1. For a valid positive quantity, including on an existing line, apply the shared inclusive limit to resulting cumulative quantity; reject excess and preserve the last valid cart. Explicit 0 removes an existing item. Negative, nonnumeric, or overflowing quantity leaves the cart UNCHANGED and invokes A1 announced feedback and safe logging, never a silent/no-log no-op. | Original approval: UQ-012–UQ-013, [APPROVAL]. Default/invalid-input policy: separate [A1]. Shared cumulative inclusive semantics preserved. | PG-002, PG-003; CAC-001–CAC-003, CAC-009, CAC-025–CAC-028, CAC-030; D-003, D-005, D-006, V-001–V-003, V-009 |
| CFR-003 | On Update with valid positive quantity, apply the same inclusive limit; reject excess while preserving the last valid cart/quantity. Explicit 0 removes the item. Negative, nonnumeric, overflowing, or missing Update quantity leaves the cart UNCHANGED and invokes A1 announced feedback and safe logging. Negative Update does NOT remove the item. | Original approval: UQ-012–UQ-013, [APPROVAL]. Invalid-input policy: separate [A1], intentionally replacing negative-Update removal. No automatic-clamping, storage-width, or response-code policy added. | PG-002, PG-003; CAC-004–CAC-006, CAC-009, CAC-026–CAC-030; D-003, D-005, D-007, N-009, V-004–V-006, V-009 |
| CFR-004 | On missing/invalid product identifier, show `Out of stock` without exposing internal errors. | Approved local override: UQ-009, [APPROVAL]; does not rewrite D-004 or claim a Confluence update. | PG-002; CAC-007, CAC-008; D-004, C-021, V-007, V-008 |
| CFR-005 | On rejection from either Add or Update, preserve the last valid cart, stay on the originating page, and give nearby programmatically associated/ANNOUNCED feedback with keyboard recovery. For valid positive quantities exceeding positive availability, use exactly `Stock unavailable. Available quantity: {availableQuantity}. Reduce the quantity.` Zero stock and missing/invalid product IDs retain `Out of stock`. For A1 invalid quantities, use exactly `Enter a valid whole-number quantity of zero or more.` and safe logging under CNFR-005. | Original copy/disclosure: UQ-008–UQ-009, UQ-013, [APPROVAL]. Exact-count permission in ALL inventory-limit rejection states is unchanged; it does not replace zero-stock/invalid-ID copy. Invalid-quantity copy/behavior: separate [A1]. | PG-002, PG-003; CAC-014–CAC-018, CAC-020, CAC-025–CAC-030; N-008–N-011, C-015–C-020, V-014–V-018, V-020 |
| CFR-006 | On checkout, preserve demo confirmation and session-cart clearing without reservation, stock decrement, or commercial transaction. | Approved local preservation: [APPROVAL] Authority Layers A, UQ-001; not a payment feature. | PG-001; CAC-022–CAC-024; D-001, D-002, C-006, C-007, V-022–V-024 |

## Non-Functional Requirements

All six `CNFR` statements are **approved for local-demo v1.0**, with A1 invalid-quantity logging/announcement elaborations separately attributed to [A1]. Categories remain unchanged; empty categories do not justify inventing requirements or conformance targets.

### Performance and Capacity

No latency/load/capacity/SLO target supplied. Session idle timeout is context, not a target or measurement window (DEP-005, A-003).

### Reliability and Resilience

CFR-003/CAC-006 preserve valid state (D-007, V-006). No availability/recovery/stock-guarantee target; validation is not reservation (D-001, C-006, C-007, R-009).

### Security

**CNFR-001 — Local-demo approved:** no internal errors in invalid-ID responses; no credentials/sensitive values in logs or named delivery/documentation surfaces. Preserved constraint, not certification. C-021, C-029, C-033; CFR-004, CAC-008, CAC-021; [APPROVAL].

### Privacy

**CNFR-002 — Local-demo approved:** diagnostics exclude cart/session contents, session identifiers, customer information, credentials/other sensitive values. A1 failures also NEVER log raw input strings; all A1 structured context values must be safe numeric/nullable values only. Finance uses aggregate counts from existing data; NO new app analytics, customer identifiers, or session identifiers. Retention/classification remain unestablished. C-026–C-029, C-033, C-005, V-021; CAC-021, CAC-030; UQ-017, [APPROVAL]; separate [A1] for invalid-input protection.

### Scalability and Elasticity

No scaling/load range supplied; no infrastructure change added.

### Maintainability and Operability

**CNFR-003 — Local-demo approved:** retain separation of business rules, HTTP flow/state operations, and session cart. Preserved architecture boundary, not a newly chosen design or completed implementation. C-030–C-032; [APPROVAL].

**CNFR-004 — Local-demo approved:** focused automated tests/reviewable delivery evidence remain future delivery requirements. Code research reports no source-controlled test project; test implementation and commands belong to the gated engineering plan, not this refresh. No coverage target or test pass invented. D-008, DEP-007, C-034, C-035, DEP-013, V-001–V-024; [APPROVAL].

### Observability

**CNFR-005 — Local-demo approved, amended by A1:** structured rejection warnings remain bounded to the existing `productId`, `requestedQty`, `availableQty`, `operation` field allowlist in [APPROVAL] Authority Layers A. Every A1 validation failure is logged; its structured context contains ONLY safe numeric or nullable values. Unparseable, overflowing, or unavailable values are null, NEVER raw input strings. This numeric/nullable restriction also applies to `operation` for A1 warnings; no numeric operation-code mapping is selected here. NEVER log session/cart/customer data or the CNFR-002 exclusions. D-009, C-022–C-029, V-021; CAC-021, CAC-030; separate [A1]. These are existing packet-defined fields, not invented OpenTelemetry semantic conventions; safe representation is an engineering detail within A1, not permission to omit the warning.

No new spans, metric names, resource values, correlation/customer/session identifiers, backend, retention, alerts, or pipeline selected. C-026–C-029, C-033, UQ-017. The business measurement is existing Finance aggregate data, not new service telemetry.

### Usability and Accessibility

**CNFR-006 — Local-demo approved:** understandable non-color-only status/text; programmatic association; AT rejection announcement; recoverable keyboard interaction without unexpected loss of place; rejection stays on the originating page. A1 invalid-quantity feedback is ANNOUNCED, never a silent malformed-input no-op. Desktop review at 1024 CSS px. Mobile “modern standards” remains undefined: preserve existing responsiveness; engineering may choose clearly labelled representative test widths, not product-supported-device policy. No WCAG level, conformance standard, ARIA/focus mechanism, or test pass claimed. C-014–C-019, N-010, N-011, V-013–V-018; CAC-013–CAC-018, CAC-026–CAC-029; UQ-016, [APPROVAL]; separate [A1].

### Compatibility and Interoperability

Catalog/session/images are compatibility context (DEP-003–DEP-006, C-030–C-032). Runtime changes need migration/compatibility documentation (C-036); none requested.

### Portability

No portability/new-environment requirement supplied; no deployment/migration commitment added.

## Constraints

Current constraints are user-approved for this local demo; original source states remain historical, and no constraint is represented as new production authority.

| ID / boundary | Constraint and acceptance impact | Imposing evidence / authority |
|---|---|---|
| CCON-001 / scope | Inventory is a locally approved extension, not the unchanged Confluence v1 baseline; retain other baseline exclusions and actual existing products. | C-001–C-005, W2 historical context; [APPROVAL] local authority; code research descriptive only |
| CCON-002 / inventory semantics | No reservation or checkout decrement; no persistent inventory/database feature expansion, warehouse synchronization, catalog administration, or payment change. | C-006, C-007, C-010–C-013; recorded agreement/exclusions. A-006 is only a feasibility assertion. CAC-022, CAC-023 |
| CCON-003 / catalog assets | Preserve actual existing products, prices, and catalog images; no conversion to historical eight-product/static-image descriptions. | C-008, C-009; historical operations/PM agreement; [APPROVAL] preserved intent; code research describes actual catalog. CAC-011, CAC-012 |
| CCON-004 / UX and privacy | Respect CNFR-001, CNFR-002, CNFR-005, CNFR-006 and the exact approved copy, disclosure exception, and originating-page recovery. | C-014–C-029 historical evidence; UQ-007–UQ-009, UQ-013, UQ-016–UQ-017, [APPROVAL] current authority |
| CCON-005 / technical boundary | Business rules outside Razor views; controllers delegate state operations; retain session cart unless an approved requirement changes it; exclude credentials/session data from source, Confluence, Jira, and logs. | C-030–C-033; architecture constraints, not an implemented design prescription |
| CCON-006 / delivery | PR and automated-validation evidence; infrastructure/workflows remain reviewable; runtime changes document migration/compatibility impact. | C-034–C-036; architecture constraints; future delivery, not completed checks |
| CCON-007 / governance | Preserve shared production governance. Local user approval applies only to this demo; Jira story prerequisite locally waived, no story exists and none may be fabricated. Code awaits matching RPI/council artifacts. | C-037, C-038, D-010–D-012 historical gates; UQ-013–UQ-014, [APPROVAL], [INTAKE] current bounded disposition |

## Process Models

`diagram_format: none`. Journeys above preserve both paths without selecting a component design.

## Acceptance Criteria

### User-approved local-demo acceptance criteria — not executed

All 30 current CAC statements are **approved for local-demo v1.0 and unexecuted**, amended by approved local-demo A1. CAC-001–CAC-024 retain original [APPROVAL] provenance; A1-specific elaborations and new CAC-025–CAC-030 cite [A1] separately. They are not production sign-off, independent review, or code authorization. For valid positive quantities the inclusive rule applies; explicit zero removal and zero availability follow UQ-012 and remain unchanged by A1.

| ID | Given / When / Then | Covers / source and current authority | Current status |
|---|---|---|---|
| CAC-001 | Given positive resulting add quantity equal to positive availability, when added, then accept it. | CFR-002; V-001, D-003, D-005; [APPROVAL] | Local approved; unexecuted |
| CAC-002 | Given cumulative add quantity above availability, when submitted, then reject it and preserve the last valid cart. | CFR-002; V-002, D-006; [APPROVAL] | Local approved; unexecuted |
| CAC-003 | Given a line at availability, when a positive add exceeds that limit, then reject the cumulative add. | CFR-002; V-003, D-006; [APPROVAL] | Local approved; unexecuted |
| CAC-004 | Given an edit to exactly positive available quantity, when submitted, then accept under the same inclusive rule. | CFR-003; V-004, D-003, D-005; [APPROVAL] | Local approved; unexecuted |
| CAC-005 | Given an edit above availability, when submitted, then reject it. | CFR-003; V-005, D-003; [APPROVAL] | Local approved; unexecuted |
| CAC-006 | Given a last valid cart, when either mutation is rejected, including A1 invalid-quantity rejection, then preserve that cart and its valid quantity. | CFR-002, CFR-003; V-006, D-007; [APPROVAL]; separate [A1] for invalid quantities | Local approved; unexecuted |
| CAC-007 | Given a missing/invalid product identifier, when either mutation is attempted, then show `Out of stock`. | CFR-004; V-007, D-004 historical; UQ-009, [APPROVAL] explicit override | Local approved; unexecuted |
| CAC-008 | Given an invalid identifier, when responding, then expose no internal error. | CFR-004, CNFR-001; V-008, C-021; [APPROVAL] | Local approved; unexecuted |
| CAC-009 | Given an existing item, when Add quantity 0 or Update quantity 0 is submitted, then REMOVE that item on either path. Given available stock 0, when a positive quantity is requested, then it cannot be bought. | CFR-002, CFR-003; V-009 historical; UQ-012, [APPROVAL] | Local approved; unexecuted |
| CAC-010 | Given one global threshold of 5 inclusive, when reviewing stock 0, 1..5, and >5, then classify unavailable, low, and normal respectively, with zero precedence and at least one example per state. | CFR-001; V-010, N-012; UQ-010–UQ-011, [APPROVAL] | Local approved; unexecuted |
| CAC-011 | Given the actual existing products/prices, when reviewing this iteration, then products are retained and prices unchanged. | CCON-003; V-011, C-008; [APPROVAL]; code research descriptive baseline | Local approved; unexecuted |
| CAC-012 | Given the actual existing catalog images, when reviewing this iteration, then images are unchanged. | CCON-003; V-012, C-009; [APPROVAL]; code research descriptive baseline | Local approved; unexecuted |
| CAC-013 | Given stock status text, when reviewed without color cues, then stock state remains understandable. | CFR-001, CNFR-006; V-013, C-014; [APPROVAL] | Local approved; unexecuted |
| CAC-014 | Given status/error text, including the exact A1 message, when its programmatic relationship is inspected, then it is associated with the relevant interface context. | CFR-005, CNFR-006; V-014, C-016; [APPROVAL]; separate [A1] | Local approved; unexecuted |
| CAC-015 | Given a rejection, including an A1 invalid-quantity rejection, when feedback appears, then assistive technology announces it; A1 failures are not silent. | CFR-005, CNFR-006; V-015, C-017; [APPROVAL]; separate [A1] | Local approved; unexecuted |
| CAC-016 | Given keyboard interaction during rejection, when recovering, then stay on the originating page without unexpected loss of place. At desktop 1024 CSS px and clearly labelled representative engineering mobile widths, preserve usability without claiming a mobile standard. | CFR-005, CNFR-006; V-016, C-018; UQ-016, [APPROVAL] | Local approved; unexecuted; mobile standard deferred |
| CAC-017 | Given rejection feedback, when presented, then place it near the cart summary within the originating page context. | CFR-005, CNFR-006; V-017, C-019; [APPROVAL] | Local approved; unexecuted |
| CAC-018 | Given a valid positive quantity exceeding positive availability on either mutation path, when rejection feedback is read, then show exactly `Stock unavailable. Available quantity: {availableQuantity}. Reduce the quantity.` with the actual available quantity substituted. Zero stock and missing/invalid product IDs retain `Out of stock`; A1 invalid quantities use CAC-026–CAC-029, not the overstock template. | CFR-005; V-018, N-008; UQ-008–UQ-009, [APPROVAL]; separate [A1] for invalid-quantity copy | Local approved; unexecuted |
| CAC-019 | Given stock 0, when a positive add is attempted, then show `Out of stock` and do not add the unavailable item. | CFR-001; V-019; UQ-009, UQ-012, [APPROVAL] | Local approved; unexecuted; control mechanism not selected |
| CAC-020 | Given routine stock display, when availability is >5 / 1..5 / 0, then use exactly `In-stock` / `Only {availableQuantity} left` / `Out of stock`. Given an inventory-limit rejection in ANY stock state, exact available counts remain permitted by the original disclosure exception. Zero stock/invalid IDs retain `Out of stock`; valid positive quantities exceeding positive stock use CAC-018. The permission is not a change to those copy branches. | CFR-001, CFR-005; V-020; UQ-007–UQ-011, [APPROVAL] | Local approved; unexecuted |
| CAC-021 | Given rejection warnings, when inspected, then structured context is bounded to `productId`, `requestedQty`, `availableQty`, `operation`; exclude all cart/session/customer contents and identifiers, credentials, and sensitive values. For every A1 failure, log ONLY safe structured numeric/nullable context and NEVER raw input strings (CAC-030). No new analytics or identifiers are introduced for Finance measurement. | CNFR-001, CNFR-002, CNFR-005; V-021, C-022–C-029; UQ-017, [APPROVAL]; separate [A1] | Local approved; unexecuted |
| CAC-022 | Given an add, when accepted, then do not reserve inventory. | CFR-006, CCON-002; V-022, C-006; [APPROVAL] | Local approved; unexecuted |
| CAC-023 | Given checkout, when completed, then do not decrement inventory. | CFR-006, CCON-002; V-023, C-007; [APPROVAL] | Local approved; unexecuted |
| CAC-024 | Given a populated session cart, when demo checkout confirms, then clear that cart without a commercial transaction. | CFR-006; V-024, D-002; UQ-001, [APPROVAL] | Local approved; unexecuted |
| CAC-025 | Given a valid product and an omitted Add quantity, when Add is submitted, then default to 1 and evaluate the resulting cumulative line quantity using the unchanged inclusive stock rule. This is not a default for missing Update quantity. | CFR-002, CFR-005; PG-002, PG-003; separate [A1]; CAC-001–CAC-003, CAC-019 | A1 locally approved; unexecuted |
| CAC-026 | Given an existing valid cart, when a negative quantity is submitted on Add or Update, then leave the entire cart UNCHANGED, show the ANNOUNCED message `Enter a valid whole-number quantity of zero or more.`, and log safely under CAC-030. Negative Update does NOT remove the item; negative Add is not silent or unlogged. | CFR-002, CFR-003, CFR-005, CNFR-005, CNFR-006; PG-002, PG-003; separate [A1] | A1 locally approved; unexecuted |
| CAC-027 | Given an existing valid cart, when a nonnumeric quantity is submitted on Add or Update, then leave the entire cart UNCHANGED, show the ANNOUNCED message `Enter a valid whole-number quantity of zero or more.`, and log safely under CAC-030 without raw input strings. | CFR-002, CFR-003, CFR-005, CNFR-005, CNFR-006; PG-002, PG-003; separate [A1] | A1 locally approved; unexecuted |
| CAC-028 | Given an existing valid cart, when an overflowing quantity is submitted on Add or Update, then leave the entire cart UNCHANGED, show the ANNOUNCED message `Enter a valid whole-number quantity of zero or more.`, and log safely under CAC-030 without raw input strings or a fabricated parsed quantity. | CFR-002, CFR-003, CFR-005, CNFR-005, CNFR-006; PG-002, PG-003; separate [A1] | A1 locally approved; unexecuted |
| CAC-029 | Given an existing valid cart, when Update quantity is missing, then leave the entire cart UNCHANGED, show the ANNOUNCED message `Enter a valid whole-number quantity of zero or more.`, and log safely under CAC-030. Do not apply Add's omitted-quantity default or remove the item. | CFR-003, CFR-005, CNFR-005, CNFR-006; PG-002, PG-003; separate [A1] | A1 locally approved; unexecuted |
| CAC-030 | Given each A1 validation failure in CAC-026–CAC-029, when its warning is inspected, then a warning exists and structured context is limited to safe numeric/nullable values within `productId`, `requestedQty`, `availableQty`, `operation`; unparseable, overflowing, or unavailable values are null. NEVER log raw input strings or session/cart/customer data, identifiers, credentials, or sensitive values. Apply the numeric/nullable restriction to operation context too; no code mapping is prescribed. | CFR-002, CFR-003, CFR-005, CNFR-001, CNFR-002, CNFR-005; PG-002; separate [A1]; original CAC-021 exclusions retained | A1 locally approved; unexecuted |

UQ-012 resolves zero-removal and zero-stock behavior locally; research V-009 remains a historical undefined test suggestion. A1 now explicitly resolves the listed invalid-input cases and supersedes prior current text that left negative/numeric-input behavior unselected. Negative Update removal is intentionally replaced, not carried forward as current policy. No automatic-clamping, numeric storage-width, simultaneous-fault precedence, or response-code policy is inferred; these ACs do not invent new business questions or promote engineering/critique suggestions.

### Current A1 and Preserved Edge-Case Traces

These are current requirement/AC links, not additions to or edits of the 157 historical source IDs. Each case isolates the stated condition; no cross-error precedence is invented.

| Input / condition | Required current result | Stable requirement / AC trace | Authority |
|---|---|---|---|
| Explicit Add 0 / Update 0, existing item | REMOVE that item on either path; no negative-to-zero removal substitution | CFR-002, CFR-003; CAC-009, CAC-026 | UQ-012, [APPROVAL]; confirmed by [A1] |
| Omitted Add quantity | Default 1, then unchanged cumulative/inclusive stock validation | CFR-002; CAC-025, CAC-001–CAC-003 | [A1] |
| Negative Add / Update quantity | Cart UNCHANGED; exact A1 message ANNOUNCED; safe warning; negative Update never removes | CFR-002, CFR-003, CFR-005; CAC-026, CAC-030 | [A1] |
| Nonnumeric Add / Update quantity | Cart UNCHANGED; exact A1 message ANNOUNCED; safe numeric/nullable warning, never raw input | CFR-002, CFR-003, CFR-005; CAC-027, CAC-030 | [A1] |
| Overflowing Add / Update quantity | Cart UNCHANGED; exact A1 message ANNOUNCED; safe warning, no fabricated parsed value | CFR-002, CFR-003, CFR-005; CAC-028, CAC-030 | [A1] |
| Missing Update quantity | Cart UNCHANGED; exact A1 message ANNOUNCED; safe warning; no default/removal | CFR-003, CFR-005; CAC-029, CAC-030 | [A1] |
| Valid positive quantity, stock 0 | Cannot buy; `Out of stock`, not the positive-overstock template | CFR-001, CFR-005; CAC-019, CAC-020 | UQ-009, UQ-012, [APPROVAL]; unchanged |
| Missing/invalid product ID | `Out of stock`, no internal error; not A1 copy or positive-overstock template merely because ID is invalid | CFR-004, CFR-005; CAC-007, CAC-008 | UQ-009, [APPROVAL]; unchanged |
| Valid positive quantity above positive stock | Reject/preserve cart; `Stock unavailable. Available quantity: {availableQuantity}. Reduce the quantity.` | CFR-002, CFR-003, CFR-005; CAC-002, CAC-005, CAC-006, CAC-018 | UQ-008–UQ-009, [APPROVAL]; unchanged |

## Traceability Matrix

### Current local-demo FR-to-AC and goal alignment

| Stable FR ID | Current AC links | Goal links |
|---|---|---|
| CFR-001 | CAC-010, CAC-013, CAC-019, CAC-020 | PG-002, PG-003 |
| CFR-002 | CAC-001–CAC-003, CAC-009, CAC-025–CAC-028, CAC-030 | PG-002, PG-003 |
| CFR-003 | CAC-004–CAC-006, CAC-009, CAC-026–CAC-030 | PG-002, PG-003 |
| CFR-004 | CAC-007, CAC-008 | PG-002 |
| CFR-005 | CAC-014–CAC-018, CAC-020, CAC-025–CAC-030 | PG-002, PG-003 |
| CFR-006 | CAC-022–CAC-024 | PG-001 |

Author-maintained link coverage: 6/6 FR-to-AC, 6/6 FR-to-goal; not executable coverage or a formal quality score. Local-demo approved statements: 6 functional, 6 non-functional, 7 constraints, 30 AC, 4 goals. CAC-025–CAC-030 are new A1-derived criteria, not historical research IDs. No feasibility handoff/disposition applicable.

### Source trace index

This is the complete **157-item historical accounting index**, preserved from v0.1. Each research ID occurs once in this index, even when used elsewhere. Evidence states and location notes (including historical “blocked”/“suggestion only” wording) describe the original extraction, not current local approval status. No item is approved merely because it is research. Current statements and the answer/disposition registers separately apply [APPROVAL], UQ IDs, and [CORRECTION]; the original analysis remains authoritative for extraction wording.

**Citation expansion:** every `[Wn]` below binds to the canonical URL, page ID, **version 1**, and cached-read limitation in the [source manifest](#source-manifest-and-cached-read-boundary). `T` means the exact `Transcript > MM:SS` heading, not an access timestamp. Role codes expand exactly as follows: `PM` = Product Manager; `Ops` = Store Operations Lead; `Eng` = Engineer; `Des` = Designer; `Sup` = Support Lead; `Ctx` = **not attributed (context page)**; `Summary` = **summary not attributed**. No timestamp is supplied for context sections. These bindings form each claim's complete provenance tuple; references to research IDs elsewhere inherit that tuple, not a newly invented source.

Location keys: `Context` = Product Context / Users and Personas; `Goals` = Product Goals; `Decisions` = Design Decisions; `FR` = Functional Requirements; `NFR` = Non-Functional Requirements; `Constraints` = Constraints; `Metrics` = Success Metrics; `Deps` = Dependencies and Operational Readiness; `Risks` = Risk Register; `Assumptions` = Key Assumptions; `Inferences` = Research Finding Dispositions; `Open` = verbatim unresolved-question register; `AC` = corresponding CAC suffix.

| Research ID | Research evidence state retained | Canonical evidence locator / role | PRD disposition / location |
|---|---|---|---|
| G-001 | Baseline context | [W2]; Purpose; Ctx | Context; PG-001 |
| G-002 | Baseline context | [W2]; Purpose; Ctx | Context; PG-001 |
| G-003 | Recorded goal | [W1]; T00:00; PM | PG-002 |
| G-004 | Recorded user-problem goal | [W1]; T01:12; Sup | Context; PG-002 |
| G-005 | Recorded goal | [W1]; T12:18; PM | PG-002 |
| G-006 | Recorded goal | [W1]; T02:35; Ops | PG-003 |
| G-007 | Recorded review goal | [W1]; T22:04; Ops | PG-003 |
| G-008 | Recorded gate | [W1]; T23:10; PM | PG-004; B1 |
| G-009 | Recorded direction | [W1]; T19:34; PM | PG-004; B5/B6 |
| N-001 | Baseline context | [W2]; Users and Needs; Ctx | Context |
| N-002 | Baseline context | [W2]; Users and Needs; Ctx | Context |
| N-003 | Baseline context | [W2]; Users and Needs; Ctx | Context |
| N-004 | Baseline context | [W2]; Users and Needs; Ctx | Context |
| N-005 | Baseline context | [W2]; Users and Needs; Ctx | Context; NFR |
| N-006 | Recorded need | [W1]; T00:00, T01:12; PM, corroborated by Sup | Context; PG-002 |
| N-007 | Recorded design direction | [W1]; T05:10; Des | CFR-001 |
| N-008 | Recorded need | [W1]; T20:51; Sup | CFR-005 |
| N-009 | Recorded design direction | [W1]; T10:42; Des | CFR-003 |
| N-010 | Recorded design direction | [W1]; T10:42; Des | CNFR-006 |
| N-011 | Recorded design direction | [W1]; T10:42, T17:20; Des | CNFR-006 |
| N-012 | Recorded review need | [W1]; T15:19; Ops | CAC-010 |
| D-001 | Recorded agreement | [W1]; T04:25; PM | Decisions; CCON-002 |
| D-002 | Recorded agreement | [W1]; T04:25; PM | CFR-006 |
| D-003 | Recorded direction; corroborated by Meeting Outcome | [W1]; T07:31; Meeting Outcome; Eng, Summary | CFR-002, CFR-003 |
| D-004 | Recorded direction | [W1]; T08:15; PM | CFR-004 |
| D-005 | Recorded boundary; corroborated by Meeting Outcome | [W1]; T09:28; Meeting Outcome; Ops, Summary | CFR-002, CFR-003 |
| D-006 | Recorded boundary; corroborated by Meeting Outcome | [W1]; T09:28; Meeting Outcome; Ops, Summary | CFR-002 |
| D-007 | Recorded direction; corroborated by Meeting Outcome | [W1]; T10:42; Meeting Outcome; Des, Summary | CFR-003 |
| D-008 | Recorded engineering agreement | [W1]; T13:27, T14:02; Sup, Eng | CNFR-004 |
| D-009 | Recorded immediate approval, not document approval | [W1]; T18:41, T19:34; Eng, PM | Decisions; CNFR-005 |
| D-010 | Recorded gate | [W1]; T19:34; PM | B5; Sign-Off |
| D-011 | Recorded research permission | [W1]; T27:24; PM | Decisions; Sign-Off |
| D-012 | Recorded gate | [W1]; T27:24; PM | B1–B6; Sign-Off |
| C-001 | Baseline boundary, not a proposal prohibition | [W2]; Current Scope; Ctx | Context; CCON-001 |
| C-002 | Baseline boundary | [W2]; Current Scope; Ctx | CCON-001 |
| C-003 | Baseline boundary | [W2]; Current Scope; Ctx | CCON-001 |
| C-004 | Baseline boundary | [W2]; Current Scope; Ctx | CCON-001 |
| C-005 | Baseline boundary | [W2]; Current Scope; Ctx | CCON-001; CNFR-002 |
| C-006 | Recorded agreement | [W1]; T04:25; PM | CCON-002; CAC-022 |
| C-007 | Recorded agreement | [W1]; T04:25; PM | CCON-002; CAC-023 |
| C-008 | Recorded constraint; explicit agreement follows | [W1]; T15:19, T16:08; Ops, PM | CCON-003; CAC-011 |
| C-009 | Recorded constraint; explicit agreement follows | [W1]; T15:19, T16:08; Ops, PM | CCON-003; CAC-012 |
| C-010 | Recorded exclusion | [W1]; T16:08; PM | CCON-002 |
| C-011 | Recorded exclusion | [W1]; T16:08; PM | CCON-002 |
| C-012 | Recorded exclusion | [W1]; T16:08; PM | CCON-002 |
| C-013 | Recorded exclusion | [W1]; T16:08; PM | CCON-002 |
| C-014 | Recorded design constraint | [W1]; T05:10; Des | CNFR-006 |
| C-015 | Recorded accessibility expectation | [W1]; T17:20; Des | CNFR-006 |
| C-016 | Recorded accessibility expectation | [W1]; T17:20; Des | CNFR-006 |
| C-017 | Recorded accessibility expectation | [W1]; T10:42; Des | CNFR-006 |
| C-018 | Recorded accessibility expectation | [W1]; T17:20; Des | CNFR-006 |
| C-019 | Recorded design direction | [W1]; T10:42; Des | CFR-005 |
| C-020 | Suggestion; exact navigation behavior not specified | [W1]; T06:22; Sup | CFR-005; suggestion only |
| C-021 | Recorded direction | [W1]; T08:15; PM | CFR-004; CNFR-001 |
| C-022 | Proposed permitted field; immediate endorsement follows | [W1]; T18:41, T19:34; Eng, PM | CNFR-005 |
| C-023 | Proposed permitted field; immediate endorsement follows | [W1]; T18:41, T19:34; Eng, PM | CNFR-005 |
| C-024 | Proposed permitted field; immediate endorsement follows | [W1]; T18:41, T19:34; Eng, PM | CNFR-005 |
| C-025 | Proposed permitted field; immediate endorsement follows | [W1]; T18:41, T19:34; Eng, PM | CNFR-005 |
| C-026 | Recorded privacy constraint | [W1]; T08:15, T18:41; PM, Eng | CNFR-002 |
| C-027 | Recorded privacy constraint | [W1]; T18:41; Eng | CNFR-002 |
| C-028 | Recorded privacy constraint | [W1]; T08:15, T18:41; PM, Eng | CNFR-002 |
| C-029 | Architecture context | [W3]; Observability; Ctx | CNFR-001, CNFR-002 |
| C-030 | Architecture constraint | [W3]; Architecture Constraints; Ctx | CNFR-003 |
| C-031 | Architecture constraint | [W3]; Architecture Constraints; Ctx | CNFR-003 |
| C-032 | Architecture constraint | [W3]; Architecture Constraints; Ctx | CNFR-003 |
| C-033 | Architecture constraint | [W3]; Architecture Constraints; Ctx | CNFR-001, CNFR-002; CCON-005 |
| C-034 | Architecture constraint | [W3]; Architecture Constraints; Ctx | CNFR-004; CCON-006 |
| C-035 | Architecture constraint | [W3]; Architecture Constraints; Ctx | CCON-006 |
| C-036 | Architecture constraint, not a requested runtime change | [W3]; Architecture Constraints; Ctx | CCON-006 |
| C-037 | Governance context | [W4]; Document Lifecycle; Ctx | CCON-007 |
| C-038 | Governance context | [W4]; Document Lifecycle; Ctx | CCON-007; Sign-Off |
| M-001 | Baseline observable success, not a business target | [W2]; Success Measures; Ctx | Metrics; PG-001 |
| M-002 | Baseline observable success | [W2]; Success Measures; Ctx | Metrics; PG-001 |
| M-003 | Baseline observable success | [W2]; Success Measures; Ctx | Metrics; PG-001 |
| M-004 | Baseline observable success; supported widths unspecified | [W2]; Success Measures; Ctx | Metrics; UQ-016 |
| M-005 | Recorded functional success expectation | [W1]; T20:51; Sup | Metrics; PG-003 |
| M-006 | Recorded functional success expectation | [W1]; T22:04; Ops | Metrics; PG-003 |
| M-007 | Candidate business outcome, not selected | [W1]; T23:10; PM | Metrics; B1 |
| M-008 | Candidate business outcome, not selected | [W1]; T23:10; PM | Metrics; B1 |
| M-009 | Candidate business outcome, not selected | [W1]; T23:10; PM | Metrics; B1 |
| M-010 | Candidate evidence source, not a baseline or ownership commitment | [W1]; T26:42; Sup | Metrics; B2 |
| DEP-001 | Recorded operations direction | [W1]; T02:35; Ops | Deps |
| DEP-002 | Recorded dependency limit | [W1]; T02:35; Ops | Deps |
| DEP-003 | Architecture context | [W3]; State and Data; Ctx | Deps |
| DEP-004 | Architecture context | [W3]; State and Data; Ctx | Deps |
| DEP-005 | Architecture context, not a measurement review window | [W3]; State and Data; Ctx | Deps |
| DEP-006 | Architecture context | [W3]; State and Data; Ctx | Deps |
| DEP-007 | Recorded engineering dependency; current repository not checked | [W1]; T14:02; Eng | Deps; CNFR-004 |
| DEP-008 | Recorded approval dependency | [W1]; T12:18; PM | Deps; B3 |
| DEP-009 | Recorded approval dependency | [W1]; T23:10; PM | Deps; B1 |
| DEP-010 | Recorded measurement dependency; neither assigned | [W1]; T26:42; Sup | Deps; B2 |
| DEP-011 | Governance context | [W4]; Jira Work Hierarchy; Ctx | Deps; B6 |
| DEP-012 | Governance context; no publishing authorized here | [W4]; Definition of Ready; Ctx | Deps; B5/B6 |
| DEP-013 | Governance context | [W4]; Definition of Done; Ctx | Deps; CNFR-004 |
| DEP-014 | Governance context; example is not a real feature story | [W4]; GitHub Traceability Convention; Ctx | Deps; B6 |
| DEP-015 | Governance ownership context | [W4]; Knowledge and Work Ownership; Definition of Ready; Ctx | Deps; B5/B6 |
| R-001 | Derived risk | [W2]; Current Scope; [W4]; Document Lifecycle; Ctx | Risks |
| R-002 | Derived risk | [W1]; T07:31, T09:28; Eng, Ops | Risks |
| R-003 | Derived risk | [W1]; T10:42; Des | Risks |
| R-004 | Derived risk | [W1]; T05:10, T10:42, T17:20; Des | Risks |
| R-005 | Derived risk | [W1]; T05:10, T06:22, T12:18, T24:18; Des, Sup, PM | Risks |
| R-006 | Source-stated concern, recorded as risk | [W1]; T25:31; Ops | Risks; recorded, not derived |
| R-007 | Derived risk | [W1]; T23:10, T26:42; PM, Sup | Risks |
| R-008 | Derived risk | [W1]; T08:15, T18:41; PM, Eng; [W3]; Observability; Ctx | Risks |
| R-009 | Derived risk | [W1]; T03:48, T04:25; Eng, PM | Risks |
| R-010 | Derived risk | [W1]; T13:27, T14:02; Sup, Eng | Risks |
| R-011 | Derived evidence risk | [W1]; T14:02; Eng; [W3]; Storefront Application; Ctx | Risks |
| R-012 | Derived governance risk | [W1]; T19:34, T27:24; PM; [W4]; Definition of Ready; Document Lifecycle; Ctx | Risks |
| A-001 | Explicit source assumption | [W2]; Product Assumptions; Ctx | Assumptions |
| A-002 | Explicit source assumption | [W2]; Product Assumptions; Ctx | Assumptions |
| A-003 | Explicit source assumption; architecture specifies idle timeout | [W2]; Product Assumptions; [W3]; State and Data; Ctx | Assumptions |
| A-004 | Explicit source assumption | [W2]; Product Assumptions; Ctx | Assumptions |
| A-005 | Explicit source assumption | [W2]; Product Assumptions; Ctx | Assumptions |
| A-006 | Speaker technical feasibility assertion, not repository-proven | [W1]; T03:48; Eng | Inferences; assertion, not adopted assumption |
| A-007 | Derived inference, not stakeholder speech | [W1]; T02:35, T04:25, T16:08; Ops, PM | Inferences; not adopted assumption |
| A-008 | Derived conditional inference, not a measurement commitment | [W1]; T26:42; Sup | Inferences; not adopted assumption |
| UQ-001 | Unresolved choice; determines BRD direction | [W1]; T23:10, T27:24; PM | Open; B1 |
| UQ-002 | Unresolved measurement evidence | [W1]; T26:42, T27:24; Sup, PM | Open; B2 |
| UQ-003 | Unresolved; none supplied in the meeting | [W1]; T23:10; PM | Open; B2 |
| UQ-004 | Unresolved; no percentage may be invented | [W1]; T26:42, T27:24; Sup, PM | Open; B2 |
| UQ-005 | Unresolved owner; support tracking capability is not assignment | [W1]; T26:42; Sup | Open; B2 |
| UQ-006 | Unresolved; no duration supplied | [W1]; T26:42; Sup | Open; B2 |
| UQ-007 | Unresolved policy | [W1]; T12:03, T12:18, T24:18; Eng, PM, Des | Open; B3 |
| UQ-008 | Derived clarification; prevents an error-copy disclosure loophole | [W1]; T06:22, T12:18, T24:18; Sup, PM, Des | Open; B3 |
| UQ-009 | Unresolved product/design approval | [W1]; T12:18, T24:18; Meeting Outcome; PM, Des, Summary | Open; B3 |
| UQ-010 | Unresolved policy | [W1]; T25:31; Ops | Open; B4 |
| UQ-011 | Unresolved values; no default authorized | [W1]; T25:31; Meeting Outcome; Ops, Summary | Open; B4 |
| UQ-012 | Derived clarification of an underspecified suggested test | [W1]; T13:27, T14:02; Sup, Eng | Open; B5 |
| UQ-013 | Derived authorization-evidence question | [W1]; T19:34, T27:24; PM; [W4]; Definition of Ready; Document Lifecycle; Ctx | Open; B5 |
| UQ-014 | Unresolved authorization gap; no actual feature issue supplied | [W1]; T27:24; PM; [W4]; Jira Work Hierarchy; Definition of Ready; Ctx | Open; B6 |
| UQ-015 | Source gap; ask for an intact authoritative snapshot, not a guessed ending | [W2]; Success Measures, final bullet; Ctx | Open; non-gating source gap |
| UQ-016 | Derived verification clarification; no dimensions supplied | [W2]; Success Measures; Ctx | Open; non-gating source gap |
| UQ-017 | Derived feasibility/privacy clarification | [W1]; T18:41, T26:42; Eng, Sup; [W2]; Current Scope; Ctx | Open; B2 navigation inferred, also B5/B6 |
| V-001 | Derived verification statement from a recorded boundary | [W1]; T07:31, T09:28; Eng, Ops | AC-001 |
| V-002 | Derived verification statement | [W1]; T07:31, T09:28; Eng, Ops | AC-002 |
| V-003 | Explicit cumulative-add example generalized only to the same boundary | [W1]; T09:28; Ops | AC-003 |
| V-004 | Derived verification statement from shared-rule requirement | [W1]; T07:31, T09:28; Eng, Ops | AC-004 |
| V-005 | Derived verification statement from shared-rule requirement | [W1]; T07:31, T09:28; Eng, Ops | AC-005 |
| V-006 | Recorded expectation | [W1]; T10:42; Des | AC-006 |
| V-007 | Recorded test scope; expected neutral handling sourced separately | [W1]; T07:31, T08:15, T14:02; Eng, PM | AC-007 |
| V-008 | Recorded expectation | [W1]; T08:15; PM | AC-008 |
| V-009 | Suggested test plus analyst limitation; no behavior selected | [W1]; T13:27, T14:02; Sup, Eng | AC-009; blocked |
| V-010 | Recorded review expectation; low-stock fixtures depend on approved policy | [W1]; T15:19; Meeting Outcome; Ops, Summary | AC-010; blocked |
| V-011 | Recorded preservation expectation | [W1]; T15:19, T16:08; Ops, PM | AC-011 |
| V-012 | Recorded preservation expectation | [W1]; T15:19, T16:08; Ops, PM | AC-012 |
| V-013 | Recorded accessibility expectation | [W1]; T05:10; Des | AC-013 |
| V-014 | Recorded accessibility expectation; no mechanism prescribed | [W1]; T17:20; Des | AC-014 |
| V-015 | Recorded accessibility expectation | [W1]; T10:42; Des | AC-015 |
| V-016 | Recorded accessibility expectation; no invented focus target | [W1]; T10:42, T17:20; Des | AC-016 |
| V-017 | Recorded placement expectation | [W1]; T10:42; Des | AC-017 |
| V-018 | Recorded support expectation; exact approved copy pending | [W1]; T20:51; Sup | AC-018; blocked |
| V-019 | Design proposal, not an approved disabled-control specification | [W1]; T05:10; Des | AC-019; blocked |
| V-020 | Derived conditional verification statement; policy not selected | [W1]; T06:22, T12:18, T24:18; Sup, PM, Des | AC-020; blocked |
| V-021 | Derived verification statement; no executed log inspection | [W1]; T08:15, T18:41, T19:34; PM, Eng; [W3]; Observability; Ctx | AC-021 |
| V-022 | Recorded scope verification | [W1]; T04:25; PM | AC-022 |
| V-023 | Recorded scope verification | [W1]; T04:25; PM | AC-023 |
| V-024 | Recorded baseline-preservation expectation | [W1]; T04:25; PM; [W2]; Product Baseline; Ctx | AC-024 |

## MVP and Release Framing

Scope is the exact user-approved local-demo packet, amended by separately approved [A1], not a newly invented MVP cutline or priority split. Preserved exclusions: D-001, C-006, C-007, C-010–C-013 and [APPROVAL]. No committed delivery date, deployment authorization, named engineering owner, or production release is supplied. The three-month review timing is not a release schedule.

## Success Metrics

### Historical measures and current local-demo measurement decision

| Evidence | Reviewable source measure / candidate | Interpretation |
|---|---|---|
| M-001 | Complete catalog-to-checkout without an account. | Baseline observable success; PG-001; not a conversion target |
| M-002 | Quantity changes/removals remain consistent in an active session. | Baseline observable success; PG-001 |
| M-003 | Checkout clears the cart and gives understandable confirmation. | Baseline observable success; PG-001 / CAC-024 |
| M-004 | Usable at supported desktop/mobile widths. | Historical source leaves widths unspecified; current UQ-016 selects desktop 1024 CSS px, defers a defined mobile standard |
| M-005 | Consistent boundary behavior for support verification. | Functional review expectation; PG-003, not a business KPI |
| M-006 | Reviewer understands stock-state/boundary results unaided. | Functional review expectation; PG-003, no invented task-success percentage |
| M-007 | Reduce avoidable support contacts. | Historical alternative, not selected as primary |
| M-008 | Reduce rejected cart updates. | Historical alternative, not selected as primary |
| M-009 | Improve completed checkouts. | Selected locally as completed demo checkout confirmations, UQ-001; not payment/orders |
| M-010 | Tagged cart-quantity-confusion contacts may provide evidence. | Historical possible source, not selected as the Finance dataset and not new tracking authority |

| Current measurement field | Approved planning value / evidence limit | User provenance |
|---|---|---|
| Outcome / rate | Improve the proportion of carts reaching demo checkout confirmation in the existing demo, not real transactions. No new cohort/instrumentation definition invented. | UQ-001, [APPROVAL] |
| Evidence | Finance has existing data and will measure; exact dataset/report not supplied and remains deferred. | UQ-002, [APPROVAL] |
| Baseline | Exactly **40%** of carts reach demo checkout confirmation over the **last 6 months**, user-reported and not independently verified. Exact date range unknown. | UQ-003, [APPROVAL], [CORRECTION] |
| Target | **60%**, user-selected and approved as a planning target, not an empirically validated outcome promise. | UQ-004, [APPROVAL] |
| Accountability | Finance department; no named individual or verified organizational attestation supplied. | UQ-005, [APPROVAL] |
| Review timing | **3 months after deployment**; no deployment date or authorization inferred. Session idle timeout is unrelated. | UQ-006, [APPROVAL] |
| Privacy / method | Finance supplies aggregate counts from existing data; NO new app analytics, customer identifiers, or session identifiers. | UQ-017, [APPROVAL] |

These selections resolve B1/B2 for the local demo, with explicit non-blocking evidence deferrals. They do not verify Finance data or assert that product behavior will cause the target to be achieved.

Product Overview's final Success Measures bullet ends **“without storing”**; no ending/privacy promise reconstructed (UQ-015; [W2], Success Measures, final bullet; not attributed (context page)). CNFR-002 uses complete prohibitions instead.

No new business pipeline, SLO, telemetry metric, or tracking permission selected. Bounded warning-content check: CNFR-005/CAC-021 within CNFR-002.

## Dependencies and Operational Readiness

| Research dependencies | Source-stated dependency / limit | Readiness disposition |
|---|---|---|
| DEP-001, DEP-002 | Proposed inventory values may remain in the catalog; warehouse integration is not ready. | Recorded direction / dependency limit; no real stock assignment |
| DEP-003, DEP-004 | Confluence v1 describes a static eight-product catalog and session cart. | Code research reports ten actual products; preserve actual catalog under user-approved intent, not an eight-product conversion. No product authority delegated to code |
| DEP-005, DEP-006 | Confluence v1 specifies 30-minute session idle timeout and static image paths. | Code research instead reports remote `ImageUrl` values; preserve actual images. Historical path assertion retained in the trace index, not adopted as an asset-change requirement |
| DEP-007 | Meeting calls for test-project enablement. | Code research reports no source-controlled test project; engineering planning responsibility, not a refresh test run |
| DEP-008, DEP-009 | Exact-copy approval; primary outcome before BRD approval. | Locally resolved by UQ-001, UQ-009, UQ-013 and [APPROVAL]; no BRD/Confluence approval claimed |
| DEP-010 | Selected measure needs an accountable owner and review period. | Finance; 3 months after deployment; exact report and baseline dates deferred. UQ-002–UQ-006 |
| DEP-011 | Historical stories require sources, criteria, exclusions, dependencies, validation commands, review expectations. | UQ-014 locally waives Jira use only; no story exists, no fake key or issue created. Engineering plan/review gates remain |
| DEP-012 | Shared production readiness needs recorded BRD/PRD approvals in Confluence and Jira. | Shared production governance unchanged; local self-declared user approval/waiver is not a production or Confluence sign-off |
| DEP-013 | Delivery completion needs clean-checkout build and automated-test evidence. | Future requirement, not evidence of a build/test run |
| DEP-014 | Approved story key supplies production branch/commit/PR traceability. | No story exists; local Jira waiver supplies no fake issue/key and no Git authorization |
| DEP-015 | Confluence product context, Jira work/approval state, and GitHub delivery evidence support outcomes. | Historical shared governance context unchanged; local packet and pending RPI/council artifacts do not claim completed delivery |

Operational check CAC-021 is unexecuted. The packet supplies bounded warning fields but no sink, retention, runbook owner, alerts, deployment, or production-readiness verdict (D-009, C-022–C-029, V-021). Document checks are not product tests or implementation authorization.

## Risks and Assumptions

### Key Assumptions

Only these five **explicit source assumptions** are retained; none verified here.

| Research ID | Explicit source assumption | Evidence status / limitation |
|---|---|---|
| A-001 | Curated catalog changes only through a software release. | W2 Product Assumptions; context, unverified here |
| A-002 | Cart belongs to one browser session. | W2 Product Assumptions; context, unverified here |
| A-003 | Cart expires after 30 minutes. | W2 assumption; W3 specifies **idle timeout**, not a guaranteed fixed lifetime or measurement window |
| A-004 | Checkout confirms the journey without a commercial transaction. | W2 Product Assumptions; not a payment feature |
| A-005 | Static product images have a placeholder fallback. | W2 Product Assumptions; not asset verification |

### Research Finding Dispositions

| Evidence | Current disposition | Authority limit |
|---|---|---|
| A-006 | Retain the Engineer's assertion that limits can be enforced without a database. | Technical feasibility assertion, not an adopted explicit assumption or proof |
| A-007 | Retain inference that proposed values are review-time catalog data, not a warehouse promise. | Derived inference, not stakeholder speech or a stock guarantee |
| A-008 | Retain conditional inference that tagged contacts may support a chosen measure if suitable. | Derived inference, not a measurement commitment or owner assignment |
| C-001; D-001–D-007 | Preserve historical baseline/agreement states; current local behavior approved separately by the user. | Invalid-ID and zero decisions cite UQ-009/UQ-012, not rewritten Confluence provenance |
| C-014–C-029; V-013–V-021 | Preserve accessibility/privacy evidence; apply packet-defined copy/disclosure and bounded warning fields. | No conformance verdict or expanded analytics scope |
| M-007–M-010; UQ-001–UQ-014, UQ-017 | Local decisions accepted/approved; B1–B5 locally resolved, B6 waived; retain Finance evidence limitations. | [APPROVAL], [CORRECTION]; does not clear pending artifact-validation/council gates |
| UQ-015, UQ-016 | Source gap deferred; desktop 1024 CSS px accepted, mobile standard undefined/deferred. | No reconstructed source ending or new supported-device/conformance policy |

No new RPI segment or phase-exit authorization is claimed.

### Risk Register

Probability/severity/owner/due date: **unassessed/unassigned**. Treatment is document handling, not a committed mitigation project.

| Research ID | Risk | Evidence status | Draft treatment |
|---|---|---|---|
| R-001 | Proposal mistaken for baseline erases the approval boundary. | Derived risk | Separate unchanged Confluence v1 evidence from local v1.0 user approval |
| R-002 | Separate add/edit rules admit cumulative excess or contradictory results. | Derived risk | CFR-002/CFR-003 and CAC-001–CAC-005 preserve shared rule |
| R-003 | Rejected update replaces valid cart state. | Derived risk | CAC-006 preserves previous valid quantity |
| R-004 | Color-only or unannounced/unassociated errors prevent recovery. | Derived risk | Retain CNFR-006; no claimed accessibility pass |
| R-005 | Exact counts leak through UI or error copy before disclosure approval. | Derived risk | Local policy now low-only display with explicit all-state rejection exception; CAC-020 verifies the distinction |
| R-006 | Uniform threshold misleads across differing sales patterns. | **Recorded source-stated concern**, not merely derived | User locally selected global 5 inclusive despite historical concern; no warehouse accuracy claim |
| R-007 | Invented baseline, target, or duration makes success unsupported. | Derived risk | Exactly 40% user-reported/unverified, 60% user-selected planning target, Finance, last 6 months / review 3 months after deployment; dates/report deferred |
| R-008 | Diagnostic/measurement instrumentation leaks customer/session information. | Derived risk | Preserve exclusions; Finance aggregate existing data only, no new analytics/identifiers |
| R-009 | Validation is described as reservation or stock guarantee. | Derived risk | Retain CCON-002 and CAC-022/CAC-023 |
| R-010 | Assumed zero semantics produces a wrong expectation. | Derived risk | CAC-009 reflects explicit user Add 0 / Update 0 removal and zero-stock prohibition, not old code |
| R-011 | Meeting architecture/test statements are mistaken for current code verification. | Derived evidence risk | Separate ten-product/remote-image research facts from historical eight/static context; no direct code/test execution here |
| R-012 | Coding from meeting endorsement bypasses product/Jira gates. | Derived governance risk | Record bounded local approval/waiver; independent refresh review and matching RPI/council gates remain pending |

## Glossary

| Term | Meaning in this document |
|---|---|
| Historical baseline | What W2/W3 v1 document, not tested current runtime behavior; descriptive engineering discrepancies are separately labelled (C-001, DEP-003–DEP-006). |
| Validation only | Check quantities without reserving stock or decrementing it at checkout (D-001, C-006, C-007). |
| Inclusive limit | For positive quantity/availability, equality is valid; excess invalid, including cumulative add (D-005, D-006). UQ-012 separately requires Add 0 / Update 0 removal and forbids buying zero stock. |
| Low stock | One shared threshold: 1..5 available units inclusive; 0 unavailable has precedence; >5 normal (UQ-010–UQ-011, [APPROVAL]). |
| Recorded agreement | Meeting agreement, not approved PRD/BRD/criteria (C-038, D-010–D-012). |
| Historical candidate | v0.1 draft organization of evidence; stable CFR/CNFR/CCON/CAC prefixes now carry separately stated local v1.0 approval, not production authority. |
| C1 | Historical 2026-10-17 intake reported in research; superseded for local refresh/research/planning by [INTAKE], not a current all-open decision state. |
| Local-demo approved | Requesting user's self-declared approval of the exact packet; not a named PM, verified corporate/Finance authority, Confluence update, or implementation authorization. |

## Sign-Off

### Approval Checklist and Handoff Readiness

* **User-approved local-demo requirements v1.0.** Requesting user, self-declared authority, final approval after 17 questions at 2026-10-18T08:38:58Z; [APPROVAL] and [CORRECTION]. No named/verified PM role supplied.
* **Amended by approved local-demo A1**, separately approved at 2026-10-18T10:50:56Z; [A1]. Version remains 1.0; original 17 question texts/answers/choices and original approval are unchanged. Negative Update removal and prior current-policy omissions are superseded by A1.
* B1–B5 locally resolved/approved subject to explicit deferrals; B6 bounded Jira waiver. No story exists, no fake issue; shared Confluence production governance remains unchanged.
* **Current phase: Validate — actual review recorded**, scoped to this actual amended PRD and its sole same-root companion, not the earlier inline A1 packet. The persisted `PRD_STANDARD_FINDINGS_V1` and `PRD_QUALITY_REPORT_V1` [ACTUAL-REVIEW] report NEEDS_REVIEW, Validate APPROVED_WITH_COMMENTS, and Finalize NOT_EVALUATED. This metadata-only update does not rerun review, dispatch a subagent, advance Finalize, or assert a clean PASS.
* Existing documentation Plan P01–P03 finalized; it is methodology only. No concurrent new code plan read, no code/test execution or implementation performed.
* [INTAKE] is Ready-With-Gaps with zero blocking refresh/research/planning gaps. `inventory-cart-v1-artifact-validation-pending` remains open until matching PRD, research, plan, and council gates clear; this refresh does not claim those gates passed.
* No backlog upload, Confluence publication, production approval, Git operation, or deployment authorized/performed.

### Validate Intake Contract — Actual Assessment Recorded

This records the **existing actual Validate result by reference**, not a new reviewer payload or a clean PASS. The reviewed input is `.copilot-tracking/plans/2026-10-17-inventory-aware-cart-prd.md`, document ID `PRD-inventory-aware-cart`, version `1.0`, phase `Validate`, **amended by approved local-demo A1**; the only state/identifier-family companion is `.copilot-tracking/plans/2026-10-17-inventory-aware-cart-prd.state.json`. Both paired payloads are persisted in [ACTUAL-REVIEW]; neither an inline A1 packet nor a default PRD-session root is substituted.

The persisted `PRD_STANDARD_FINDINGS_V1` (`requirements-quality-PRD-inventory-aware-cart-20261018T110756Z-cycle1`) has status CAUTION and **40 findings: 29 COVERED, 11 CAUTION, 0 RISK, 0 NOT_APPLICABLE; 4 MEDIUM and 7 LOW cautions**. The paired `PRD_QUALITY_REPORT_V1` (`PRD-inventory-aware-cart-quality-20261018T110756Z-cycle1`) has `overall_status: NEEDS_REVIEW`, `gate_decisions.validate_exit: APPROVED_WITH_COMMENTS`, and `gate_decisions.finalize_exit: NOT_EVALUATED`. Only that report owns these gate decisions. Intake is **Ready-With-Gaps, zero blocking intake gaps**. The assessment's caller-supplied timestamp is **2026-10-18T11:07:56.430Z**; the decision-entry write timestamp is **2026-10-18T11:15:56Z**. This update claims neither independent freshness nor programmatic schema validation; the recorded review checked the contracts and arithmetic by inspection.

Review scope includes all current FR/NFR/AC/goal links, A1 exact copy and input rules, safe numeric/nullable logging and announcement, preserved zero-stock/invalid-ID versus positive-overstock copy, unchanged historical 157/17 records and original choices, local approval provenance/waiver, and retained non-blocking deferrals. Author preservation checks are not a new domain review. The artifact-validation/code-execution hold remains open pending a matching revised full-plan critique and council review, not reopened business questions or a Jira prerequisite. Local user approval of v1.0 plus A1 is unchanged; no Finalize or production approval is implied.

**Review-comment routing (carry-forward, not closure):** REC-001 canonical current-catalog availability, including the stale-session/catalog case, and REC-004 independently reportable CAC path/input/outcome crosswalk are assigned to the code plan, without changing PRD requirements or CAC IDs. REC-002 supporting SMART formulation for PG-001/PG-003/PG-004 remains due **before Finalize**, using existing obligations without invented targets; REC-003 CCON-004 classification is deferred to **formal Finalize**, not a code gate. REC-005 retains the five absent NFR categories (performance/capacity, reliability/resilience, scalability/elasticity, compatibility/interoperability, portability) as scoped demo limitations, not new NFRs or operational commitments.

### B1–B6 Historical Questions and Current Local-Demo Dispositions

Six questions below remain **verbatim analysis**, not transcript quotes. The historical provenance is unchanged; current dispositions come only from the user packet and correction, not retroactive Confluence approval.

| Gate / required package | Exact historical blocking question from research | Canonical historical evidence locator / speaker role | Current local-demo disposition / user provenance |
|---|---|---|---|
| B1 — Selected primary business outcome | Which outcome is primary, and what approved outcome statement should the BRD carry? | [W1] 27951105 v1; Transcript > 23:10, 27:24; Product Manager; C1 | Locally resolved: completed demo checkout confirmations, not commercial orders. UQ-001, [APPROVAL]; no BRD v1.0 claimed |
| B2 — Evidence-backed measurement package | What available evidence supports the baseline and target, who owns measurement, and when will results be reviewed? | [W1] 27951105 v1; Transcript > 23:10, 26:42, 27:24; Product Manager; Support Lead; C1 | Locally approved with deferrals: exactly 40% user-reported last 6 months, 60% planning target, Finance accountable, review 3 months after deployment, aggregate existing data only; report/dates unverified/deferred. UQ-002–UQ-006, UQ-017, [APPROVAL], [CORRECTION] |
| B3 — Disclosure policy and matching approved copy | Are exact quantities disclosed for all products, only low-stock products, or neither, and does that policy also govern rejection messages? What copy is approved? | [W1] 27951105 v1; Transcript > 06:22, 12:18, 24:18, 27:24; Support Lead; Product Manager; Designer; C1 | Locally approved: low-only routine counts, exact-count exception in ALL rejection states; exact copy in CFR-001/CFR-004/CFR-005. UQ-007–UQ-009, [APPROVAL] |
| B4 — Low-stock policy, thresholds, and boundaries | Which policy applies, what are the approved thresholds, and how are boundary quantities classified? | [W1] 27951105 v1; Transcript > 25:31; Meeting Outcome; Store Operations Lead; summary not attributed; C1 | Locally approved: global 5 inclusive, 0 unavailable precedence, 1..5 low, >5 normal. UQ-010–UQ-011, [APPROVAL] |
| B5 — Explicit approval of requirements and acceptance criteria | Who is the authorized approver, where will approval be recorded, and which document versions are approved? What does “zero” mean, and what behavior is approved for that case? | [W1] 27951105 v1; Transcript > 13:27, 19:34, 27:24; Support Lead; Product Manager; [W4] 27852801 v1; Definition of Ready; Document Lifecycle; not attributed (context page); C1 | Locally approved: requesting user, self-declared, packet/matching PRD v1.0; Add 0 and Update 0 remove, stock 0 cannot be bought. UQ-012–UQ-013, [APPROVAL]. Independent refresh review remains pending |
| B6 — Approved Jira delivery story and traceability | What approved Jira story authorizes implementation, and where are its source links, approved acceptance criteria, exclusions, dependencies, validation commands, and review expectations? | [W1] 27951105 v1; Transcript > 27:24; Product Manager; [W4] 27852801 v1; Jira Work Hierarchy; Definition of Ready; GitHub Traceability Convention; not attributed (context page); C1 | Locally WAIVED: “don't use it for this demo”; no story exists, no Jira mutation/fake issue. UQ-014, [APPROVAL]. Shared production governance unchanged; matching RPI/council gates still required |

### Historical Question Texts and Source Evidence

All 17 historical question texts are preserved verbatim from research. Their current answers/statuses are in the next register, joined by formal UQ ID; they are **not all currently open**. Quoted support below is exact **analysis wording, not transcript speech**. Raw transcript-quotation fidelity is unavailable under this source boundary; nothing is reconstructed.

Gate associations are **navigation, not source statements**. UQ-017 → B2 is inferred, also relevant to B5/B6. UQ-015/UQ-016 are non-gating source gaps. Speakers are not assigned owners.

| Research ID | Verbatim research question | Verbatim research evidence / limitation | Canonical locator / role; navigation |
|---|---|---|---|
| UQ-001 | Which candidate business outcome is primary? | G-008: “Establish a primary business result before BRD approval.” | [W1] 27951105 v1; Transcript > 23:10, 27:24; Product Manager; B1 |
| UQ-002 | What available evidence supports the selected measure? | M-010: “Contacts tagged for cart-quantity confusion can provide measurement evidence.” | [W1] 27951105 v1; Transcript > 26:42, 27:24; Support Lead; Product Manager; B2 |
| UQ-003 | What verified baseline applies to the selected outcome? | UQ-003 evidence limitation: “Unresolved; none supplied in the meeting” | [W1] 27951105 v1; Transcript > 23:10; Product Manager; B2 |
| UQ-004 | What evidence-backed target will be approved? | UQ-004 evidence limitation: “Unresolved; no percentage may be invented” | [W1] 27951105 v1; Transcript > 26:42, 27:24; Support Lead; Product Manager; B2 |
| UQ-005 | Who is accountable for measuring the outcome? | UQ-005 evidence limitation: “Unresolved owner; support tracking capability is not assignment” | [W1] 27951105 v1; Transcript > 26:42; Support Lead; B2 |
| UQ-006 | What review period will be approved? | DEP-010: “The selected measure needs an accountable owner and review period.” | [W1] 27951105 v1; Transcript > 26:42; Support Lead; B2 |
| UQ-007 | Are exact counts disclosed broadly, only for low stock, or not at all? | Research alternatives: “Exact counts broadly; exact counts only for low stock; stock-state labels without counts” | [W1] 27951105 v1; Transcript > 12:03, 12:18, 24:18; Engineer; Product Manager; Designer; B3 |
| UQ-008 | Does the approved disclosure policy also govern rejection messages? | UQ-008 limitation: “Derived clarification; prevents an error-copy disclosure loophole” | [W1] 27951105 v1; Transcript > 06:22, 12:18, 24:18; Support Lead; Product Manager; Designer; B3 |
| UQ-009 | What customer-facing status and rejection copy is approved? | DEP-008: “Exact customer-facing copy needs product/design approval in the PRD.” | [W1] 27951105 v1; Transcript > 12:18, 24:18; Meeting Outcome; Product Manager; Designer; summary not attributed; B3 |
| UQ-010 | Is low-stock classification uniform or product-specific? | Research alternatives: “Uniform threshold; per-product threshold” | [W1] 27951105 v1; Transcript > 25:31; Store Operations Lead; B4 |
| UQ-011 | What threshold values and boundary classifications are approved? | UQ-011 evidence limitation: “Unresolved values; no default authorized” | [W1] 27951105 v1; Transcript > 25:31; Meeting Outcome; Store Operations Lead; summary not attributed; B4 |
| UQ-012 | Which input does zero refer to, and what behavior is expected? | V-009: “The zero case is reviewed only after its input and expected result are clarified.” | [W1] 27951105 v1; Transcript > 13:27, 14:02; Support Lead; Engineer; B5, not a seventh package |
| UQ-013 | Who can approve which BRD/PRD/acceptance versions, and where is that approval recorded? | DEP-012: “Implementation readiness needs recorded BRD/PRD approvals in Confluence and Jira.” | [W1] 27951105 v1; Transcript > 19:34, 27:24; Product Manager; [W4] 27852801 v1; Definition of Ready; Document Lifecycle; not attributed (context page); B5 |
| UQ-014 | Which approved Jira story supplies the implementation authority and delivery evidence? | D-012: “Implementation waits for the open business decisions, approved product requirements, and a Jira story.” | [W1] 27951105 v1; Transcript > 27:24; Product Manager; [W4] 27852801 v1; Jira Work Hierarchy; Definition of Ready; not attributed (context page); B6 |
| UQ-015 | What completes the truncated baseline logging success statement? | Research source gap: “The final Product Overview Success Measures bullet is visibly incomplete at “without storing”.” | [W2] 27688962 v1; Success Measures, final bullet; no timestamp; not attributed (context page); non-gating source gap |
| UQ-016 | Which desktop/mobile widths are supported for usability review? | M-004: “The interface remains usable at supported desktop/mobile widths.” Limitation: “supported widths unspecified” | [W2] 27688962 v1; Success Measures; no timestamp; not attributed (context page); non-gating source gap |
| UQ-017 | How will candidate measurement evidence be used without prohibited customer/session data or new customer analytics? | C-005: “Personalized recommendations and customer analytics are outside the baseline release.” C-027: “Logs must exclude session identifiers.” | [W1] 27951105 v1; Transcript > 18:41, 26:42; Engineer; Support Lead; [W2] 27688962 v1; Current Scope; not attributed (context page); inferred B2, also B5/B6 |

### Current Answers and Statuses — All 17 User Decisions

Every answer below is attributed to **Requesting user (self-declared authority, no named PM role)**, keyed by the formal historical question ID and `approvalDecisionRef` = [APPROVAL]. [CORRECTION] controls the hyphenated IDs and exact **40%** baseline wording. Original Confluence v1 locators/speakers in the preceding table describe the questions' origins, not the authority for these new answers.

| Question ID | Current user-approved local-demo answer | Current status / approvalDecisionRef |
|---|---|---|
| UQ-001 | Improve completed checkout confirmations in the existing demo. Not payment or commercial orders. | Locally approved; [APPROVAL] |
| UQ-002 | Finance has existing data and will measure. Exact dataset or report not provided. | Method locally approved; dataset/report deferred, non-blocking; [APPROVAL] |
| UQ-003 | Exactly **40%** of carts reach demo checkout confirmation over the **last 6 months**, user-reported. Finance holds the data. Not independently verified; exact date range unknown. | Planning baseline accepted; verification/date range deferred, non-blocking; [APPROVAL], [CORRECTION] |
| UQ-004 | **60%** target, user-selected and approved as a planning value. Not an empirically validated outcome promise. | Locally approved planning target; [APPROVAL] |
| UQ-005 | Finance department is accountable. | Locally approved accountability; no named person/verified attestation invented; [APPROVAL] |
| UQ-006 | Review **3 months after deployment**. This is not deployment authorization. | Locally approved review timing; [APPROVAL] |
| UQ-007 | Exact quantities only for low-stock products; status labels otherwise. | Locally approved routine-display policy; [APPROVAL] |
| UQ-008 | Explicit exception: exact available counts allowed in ALL inventory-limit rejection messages, across zero/low/normal stock states. | Locally approved exception; [APPROVAL] |
| UQ-009 | Normal: `In-stock`. Low: `Only {availableQuantity} left`. Zero: `Out of stock`. Invalid product: `Out of stock` (explicit override). Final rejection on both add/update: `Stock unavailable. Available quantity: {availableQuantity}. Reduce the quantity.` | Exact copy locally approved; [APPROVAL] |
| UQ-010 | One shared threshold for all products. | Locally approved global policy; [APPROVAL] |
| UQ-011 | **5 available units, INCLUSIVE**. Zero unavailable takes precedence; **1..5 low**, **>5 normal**. | Locally approved boundaries; [APPROVAL] |
| UQ-012 | Zero available inventory cannot be bought. Add quantity **0 REMOVES** an existing item; Update quantity **0 REMOVES** the item. Explicit user decision, not an unsourced old-code inference. | Locally approved zero semantics; [APPROVAL] |
| UQ-013 | Requesting user's self-declared approval, recorded in repo, version **1.0**; final exact packet confirmed after all 17 questions. | Locally approved; independent refresh review and RPI gates still pending; [APPROVAL] |
| UQ-014 | No story exists. User: “don't use it for this demo”. Bounded JIRA WAIVER, local demo only; no Jira create/mutate and no fake issue. Shared Confluence production governance unchanged. | Locally waived, not a production waiver; [APPROVAL] |
| UQ-015 | Truncated Product Overview v1 “without storing” gap remains unresolved; no ending invented. | Deferred source gap, non-blocking; [APPROVAL] |
| UQ-016 | Desktop **1024 CSS px**. Mobile “modern standards” undefined as viewport/conformance standard. Preserve existing responsiveness; engineering may choose clearly labelled representative test widths, not supported-device policy. | Desktop locally approved; mobile exact standard/dimensions deferred, non-blocking; [APPROVAL] |
| UQ-017 | Finance supplies aggregate counts from existing data. NO new app analytics, customer identifiers, or session identifiers. | Locally approved measurement/privacy constraint; [APPROVAL] |

All 17 have a current disposition. Four question records retain non-blocking unknowns (UQ-002, UQ-003, UQ-015, UQ-016); UQ-014 is waived for the local demo, not answered with a fictitious story. There are zero blocking human questions for this refresh/research/planning per [INTAKE]. The artifact-validation hold is separate and remains pending.

## Disclaimer

AI-assisted refresh requires independent product/engineering review. The requesting user's local-demo approval is recorded, not verified production/Finance/Confluence authority, feasibility validation, an engineering commitment, or a formal quality/implementation gate exit. Planning caution was previously displayed; its existing timestamp is preserved in the sole companion.

## Document Metadata

* Canonical template lineage: `requirements-author/templates/prd/prd-full.md`, v1.0.0; existing document shape adapted to the binding root and local-demo scope.
* Template: Microsoft HVE-Core Team, CC-BY 4.0; source rights/classification unchanged.
* Existing state contract retained in the sole same-root companion; prior A1 authoring remains unchanged. Current phase Validate; existing paired actual assessment and comment routing recorded by reference [ACTUAL-REVIEW]: NEEDS_REVIEW / Validate APPROVED_WITH_COMMENTS / Finalize NOT_EVALUATED. Metadata-only update; no interview, new business decisions, fresh assessment, or phase advancement.
* Version lineage: v0.1.0 working draft → v1.0 user-approved local-demo requirements, based on [APPROVAL] and [CORRECTION]. Prior PRD SHA-256: `75e004ae32343a49c9a209781a9f3f14c4fa8de749a3878db2ac6d7476cb5f70`; changed bytes legitimately have a new hash.
* In-place amendment lineage: v1.0 → v1.0 **amended by approved local-demo A1**, based only on separate [A1] user approval. Immediate pre-A1 PRD SHA-256: `af1d470345a49547b9379d13eceb30c83e809e07886a8a1fe9ec2e96be86b1d3`. This is not a new Confluence version or approval, a new plan, or a formal quality report.
* Historical documentation review repairs RV001/RV002 remain closed; no additional instrumentation scope or unsourced MVP cutline introduced.
* Self-check scope: preservation of 157 historical IDs/rows/citations and 17 exact question texts; current answers, six dispositions, exact copy/numbers, local authority, requirement links, local file/anchor links, JSON, and Markdown structure. Actual commands/results returned at closeout; not product tests, source freshness, independent review, or a formal quality score.

[A1]: ../squad/decisions.md#2026-10-18t105056z-localdemoamendmenta1-inventory-aware-cart-invalid-quantity-behavior
[ACTUAL-REVIEW]: ../squad/decisions.md#intake-readiness-verdict-2026-10-18t111556z-inventory-aware-cart

### Source Manifest and Cached-Read Boundary

The initial builder read the analysis, not caches/live pages. This refresh additionally reads the explicit repo approval/correction/current intake, existing documentation methodology, and bounded descriptive code-research evidence; no live pages or direct code are read. The historical manifest preserves upstream provenance, not builder retrieval/freshness verification. URLs do not prove inter-page/story links. New local decisions cite the requesting user, UQ ID, and repo approval, never an invented Confluence version update.

| Alias | Canonical page / ID / version | Reported upstream read boundary |
|---|---|---|
| [W1] | Stakeholder Meeting - Inventory-Aware Cart / **27951105 v1** | Parent Atlassian MCP; analyst cached-read 2026-10-17. Leading JSON ID/URL/v1/body, not duplicate tail. MM:SS/roles available; meeting labels itself synthetic/role-anonymized. |
| [W2] | Product Overview / **27688962 v1** | Parent Atlassian MCP; analyst cached-read 2026-10-17. Plaintext provenance supplies v1, no JSON version verification. No speaker/time; “without storing” incomplete. |
| [W3] | Application and Platform Architecture / **27820033 v1** | Parent Atlassian MCP; analyst cached-read 2026-10-17. Leading JSON ID/URL/v1/body; duplicate tail excluded. No speaker/time. |
| [W4] | Delivery and Governance / **27852801 v1** | Parent Atlassian MCP; analyst cached-read 2026-10-17. Leading JSON ID/URL/v1/body; duplicate tail excluded. No speaker/time. |
| C1 | [.copilot-tracking/squad/decisions.md](../squad/decisions.md#intake-readiness-verdict-2026-10-17t205734z-inventory-aware-cart), Intake Readiness Verdict 2026-10-17T20:57:34Z inventory-aware-cart | Historical intake as reported in analysis/dispatch, A Ready / B Not-Ready at that time; not current all-open status or Confluence product evidence. |
| Current local authority | [APPROVAL]; [CORRECTION]; [INTAKE] | Requesting user (self-declared); 17 answers; exactly 40% correction; Ready-With-Gaps / zero blocking gaps for refresh, research, and planning only. |
| Engineering evidence | [2026-10-18 code research](../research/2026-10-18/inventory-aware-cart-code-research.md) | Read-only descriptive evidence; SHA-256 `3dda2d7d908ed542ae5615127f533cb7d439fc35a790dbfb4dda518dcd3fb876`; ten products/remote images; no product-decision authority. |

Local recorded user approval and current intake were read; live source freshness, corporate authority, Finance data, deployed inventory/logs, direct code, and test infrastructure were not independently verified. Actual model identifier: **not exposed**; no subagents used.

[W1]: https://m365cpi37447403.atlassian.net/wiki/spaces/HVE/pages/27951105/Stakeholder+Meeting+-+Inventory-Aware+Cart
[W2]: https://m365cpi37447403.atlassian.net/wiki/spaces/HVE/pages/27688962/Product+Overview
[W3]: https://m365cpi37447403.atlassian.net/wiki/spaces/HVE/pages/27820033/Application+and+Platform+Architecture
[W4]: https://m365cpi37447403.atlassian.net/wiki/spaces/HVE/pages/27852801/Delivery+and+Governance
[APPROVAL]: ../squad/decisions.md#2026-10-18t083858z-local-demo-approval-inventory-aware-cart-version-10
[CORRECTION]: ../squad/decisions.md#2026-10-18t084756z-correction-note-local-demo-approval-table-labels-and-uq003-baseline-wording
[INTAKE]: ../squad/decisions.md#intake-readiness-verdict-2026-10-18t111556z-inventory-aware-cart
