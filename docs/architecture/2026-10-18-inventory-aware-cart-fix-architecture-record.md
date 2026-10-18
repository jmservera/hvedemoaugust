<!-- markdownlint-disable-file -->
# Architecture Review Record: Inventory-aware cart council

## Review Identity and Confirmed Scope

* Slug: inventory-aware-cart-fix; date: 2026-10-18.
* Run: init-2026-10-17-001; topic: inventory-aware-cart; role: architect Primary.
* User-confirmed bounded council lens: cart-state integrity, request/binding semantics, and accessible feedback/verification within the existing local-demo MVC architecture.
* Decision owners: user and council coordinator; this lens is not implementation admission.
* Requested judgment: docs/architecture/2026-10-18-inventory-aware-cart-fix-architecture-review.md.

## Evidence Sources and Research Artifacts

| ID | Source | State | Use |
|---|---|---|---|
| E1 | .copilot-tracking/plans/2026-10-18-inventory-aware-cart-fix-plan.md | Read; canonical hash recomputed and matched | Accepted candidate |
| E2 | .copilot-tracking/reviews/2026-10-18-inventory-aware-cart-fix-plan-closure.md | Read; PC-001 to PC-012 closed, full Pass | Closure and residuals |
| E3 | .copilot-tracking/plans/2026-10-17-inventory-aware-cart-prd.md | Current requirements, constraints, A1 and CAC checked | v1.0+A1 authority |
| E4 | Cached MCP page 27820033 v1, inventory-cart-architecture-source.txt | Read first complete JSON payload | Authoritative architecture |
| E5 | Cached MCP page 27951105 v1, inventory-cart-transcript-source.txt | Complete first JSON body read | Recorded stakeholder constraints |
| E6 | .copilot-tracking/research/2026-10-18/inventory-aware-cart-code-research.md | F1-F10 and recommendations read | Existing local code research |

E1: 75,021 bytes; raw SHA-256 383613ef9087a77ae9938cecb402a10cbbe284e86df09901fcca646f5749bc8c.
Recomputed rpi-plan-assessment-v1: 696e58e6d90e3fa4298b427232a0043a4c759b95140099372402464441e469e8.
E2: 11,707 bytes; E3: 93,771 bytes; E6: 31,019 bytes.
The closure's earlier raw-file size/hash is not the final plan's raw identity; its assessed projection matches.

## Context, Constraints, and Assumptions

| ID | Type | Statement | Evidence | Status |
|---|---|---|---|---|
| CTX-1 | Constraint | Local demo only; no external writes, commits, push, deploy, code edits, new research, nested agents, or ADR creation | User | Confirmed |
| CTX-2 | Context | Static catalog, transient session cart, MVC boundaries, checkout clear-only | E4, E5 | Verified |
| CTX-3 | Constraint | Jira waived; Validate APPROVED_WITH_COMMENTS with zero blocking, not production Finalize | User, E2 | Confirmed |
| CTX-4 | Assumption | No production scale, concurrency, or persistent-stock guarantees are inferred | User, E4 | Review limit |

## Pillar Findings and Non-Findings

| Focus | Disposition | Evidence | Assessment | Priority |
|---|---|---|---|---|
| Reliability: canonical availability and session integrity | No finding | E1 P02, P04; E3 CAC-022 to CAC-024; E4; E6 F6 | Catalog lookup drives mutation and display; JsonIgnore excludes availability from snapshots. Retains transient session shape, no reservation/decrement, checkout clear-only, all 10 actual products/prices/images. | Low |
| Reliability: request parsing and ordered mutation rules | No finding | E1 D7-D8, P02-P03; E3 CAC-001 to CAC-009, CAC-025 to CAC-030 | Form-key presence distinguishes omission/blank. Shared rule enforces cumulative Add, absolute Update, inclusive bounds and long arithmetic. Invalid input preserves state with exact A1 copy. D8 resolves simultaneous faults without inventing policy. | Low |
| Operational Excellence: accessible feedback and verification | No architectural blocker; execution unverified | E1 D9-D11, P04-P05; E3 CAC-013 to CAC-021, CAC-030; E6 F8 | PRG/TempData, focusable alert, Cart summary/Home top placement, associations, explicit Update and safe four-field warning are coherent. Browser/HTTP proof remains required. | Low |

## Options, Drivers, Trade-Offs, and Research Recommendation

### Decision Drivers

* Preserve approved requirements and existing MVC/session boundaries without redesign.
* Prevent stale stock, parse-failure deletion, duplicated rules and undisclosed log payloads.

### Candidate Options

| Option | Benefits | Trade-offs | Evidence | Disposition |
|---|---|---|---|---|
| Canonical lookup with unchanged cart snapshot vs session availability | Avoids stale stock without migration | Catalog lookup on mutation/render; no persistent stock guarantee | E1 D3; E6 F6 | Canonical lookup accepted; snapshot stock rejected |
| Pure parser/rules/result vs primitive binding and controller duplication | Testable omission/error distinction and shared behavior | Three small types | E1 D2, D7; E3 A1 | Accepted |
| PRG/TempData vs POST re-render | Retains redirects and avoids refresh re-post | One-shot message and focus must be verified | E1 D1, D9; E6 F8 | PRG accepted; re-render viable but unnecessary |
| Existing net6 harness plus real HTTP vs runtime retarget/new hosting dependency | Preserves runtime and avoids unrelated changes | Restore/runtime can block | E1 P01, P05; E6 F10 | Existing target accepted; silent fallback rejected |

### Research Recommendation

No new Research activation: this lens evaluates the accepted local evidence, not current-service selection.
Confidence: high for plan conformance, not execution. E6's earlier optional-int, negative-removal and zero-stock-copy suggestions are superseded by E1/E3, not adopted.

## Reviewer Disposition

| Recommendation | Disposition | Rationale | Owner |
|---|---|---|---|
| Canonical catalog, MVC/service separation, outcome type | accepted | E1/E4/E6 prevent stale-state and duplicated-rule defects | Developer |
| E6 primitive default/negative handling and zero-stock template | revised | E1/E3 A1 and approved copy take precedence | User authority; developer implements |
| D8 precedence, D9 placement/focus, D10 numeric operation codes | accepted | Engineering choices consistent with requirements; not stakeholder approvals | Developer; tester verifies |
| Persistent inventory, checkout revalidation, runtime migration | rejected for this scope | Unrequested expansion, not needed for validation-only local demo | User |
| Real screen-reader verification | deferred | Disclosed evidence limit, not a conformance claim | Coordinator |

## Recommendations and Priorities

Verdict: **Approve**. Risk: **Low**. Blocking issues: none. New architectural conditions: none.

Retain existing implementation obligations: canonical/stale-session tests, all A1 mutation and exact-copy checks, four numeric/nullable log fields with no raw/session/cart/customer data, HTTP binding, browser focus/keyboard recovery and regression checks. E1's catalogue has 74 unique cases (9 parser, 30 service, 11 controller, 15 HTTP, 9 browser); these are planned, not passed. Restore/framework failure blocks P01 and must be reported without fallback.

## Escalations and Human Decision Owners

No new architectural escalation. G3 product-fit and G4 coordinator admission remain required before code; G5 tester closes implementation. Finance baseline and PRD cautions remain with existing owners, not new architectural prerequisites.

## ADR Links

None created or requested; this is council conformance, not architecture adoption.

## Limits and Unassessed Areas

* Plan-level static review only. No test execution, runtime/browser proof, or production certification.
* Security, cost, and RAI omitted as directed unless concrete high-risk evidence emerges.
* No concrete high-risk signal emerged; no extra audit or external lookup was performed.
* No real screen-reader proof; no production scale, session concurrency, or persistent-stock guarantee.
* Signed zero (-0) remains E2's low-severity parser residual; no lexical-sign policy is invented.
* Final requested council judgment is persisted at docs/architecture/2026-10-18-inventory-aware-cart-fix-architecture-review.md. No ADR or implementation-plan handoff is invoked.
