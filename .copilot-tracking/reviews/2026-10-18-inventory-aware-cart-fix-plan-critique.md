<!-- markdownlint-disable-file -->
# RPI Plan Critique: inventory-aware-cart-fix (initial reservation)

## Attempt Reservation

* Task ID: inventory-aware-cart-fix
* Run ID: init-2026-10-17-001
* Topic: inventory-aware-cart
* Plan: .copilot-tracking/plans/2026-10-18-inventory-aware-cart-fix-plan.md
* Attempt ID and kind: inv-cart-fix-critique-initial-20261018-1139 (initial)
* Attempt status: started
* Attempt slot consumed: yes
* Critique depth: standard
* Depth provenance: default (no explicit user request for deep mode)
* Candidate identity and saved hash boundary: plan as saved at reservation time; SHA-256 ac5e7ea3505d79bf9d05ec868738091509463976dd817738695b70c9fab2fd2b
* Canonical projection evidence: helper scripts/Get-PlanAssessmentHash.ps1 under the resolved rpi-plan skill root (plugin path rpi-plan); projection_version rpi-plan-assessment-v1; sha256 ac5e7ea3505d79bf9d05ec868738091509463976dd817738695b70c9fab2fd2b
* Output path: .copilot-tracking/reviews/2026-10-18-inventory-aware-cart-fix-plan-critique.md
* Current-run provenance: standalone initial reservation persisted in this output before assessment; host invocation identifier not exposed; reservation-to-assessment sequence is uninterrupted within this background run
* Prior attempts: none (plan Critique Disposition records initial attempt not consumed; output path did not exist)
* Original attempt and recovery approval: not_applicable
* Human assessor provenance: not_applicable

## Assessment Result (appended after reservation; reservation retained above)

* Invocation outcome: completed
* Assessment execution/availability: Complete (standard depth, full plan boundary)
* Verdict: Revise (plan is not Ready for implementation)
* Attempt status: completed; attempt slot consumed; no replay permitted for this candidate
* Hash covered: ac5e7ea3505d79bf9d05ec868738091509463976dd817738695b70c9fab2fd2b (projection `rpi-plan-assessment-v1`; matched helper output at preflight; plan not modified by this run)
* Model: unknown (not exposed to this agent); tool calls: approximately 40, read-only apart from the reservation and this artifact; no subagents, no nested critic, no tests, installs, Jira, Confluence, or Git

## Inputs and Criterion Boundary

* Full plan (39,391 B); targeted research (F1–F10, U1–U8); PRD v1.0 and state file (header, CAC-001–024, answer register, trace-index labels); approved packet approval entry and correction; team.md; checkout and serializer code. Not re-extracted: 72 KB analysis, transcript, 157 historical IDs. Boundary: whole plan, assessed once.

## Coverage

| Area | Coverage | Evidence or concern |
|------|----------|---------------------|
| Root cause F1–F5 and serialization (P02-T01, D3) | Covered | `CartService` uses `System.Text.Json`, so `[JsonIgnore]` is the right attribute. Stock is read from `ProductService` each call. |
| Approved copy, zero stock, invalid ID (FR-002, FR-005, FR-007, P02-T01, D5) | Partial | Conflicts with CAC-007 and CAC-019 (PC-001). |
| Placement and accessibility (P04-T01, CAC-013–017) | Partial | CAC-017 conflict (PC-002); mechanics unpinned (PC-008). |
| Input binding and negative input (P01-T02, P02-T02, P03-T01, D4) | Partial | Binding pin not executable (PC-004); negative Add unapproved (PC-003). |
| Test harness, restore, net6 (P01-T01) | Partial | No blocker exit (PC-011). |
| Fan-out and roster (Deliverables, G1–G5) | Partial | Stale G1; council rows duplicated (PC-005). |
| Logging privacy and warning fields (FR-009, NFR-001, CAC-021) | Covered | Four approved fields match PRD and packet. Overlap noted (PC-012). |
| Catalog, prices, images, scope containment (FR-001, FR-011, NFR-003, NFR-005) | Covered | Preserved in P01-T02 and P02-T01. Non-goals consistent. |

## Verdict

* Verdict: Revise. Five High findings require direct planner correction. Nothing here needs a user decision, because the approved packet already settles the zero-stock and placement items.
* Rationale: The plan's central user-visible copy contradicts approved rules, and its binding guarantee is not testable as written.
* Hash covered by this assessment: ac5e7ea3505d79bf9d05ec868738091509463976dd817738695b70c9fab2fd2b
* Closure chain: not applicable. This is the initial full assessment. Any revision needs a fresh full assessment, because requirements and acceptance coverage change.

## Findings

### PC-001 [High]: Zero-stock and invalid-ID copy contradicts approved rules

* Related IDs: FR-005, FR-007, P02-T02, P03-T01, D5; CAC-007, CAC-019; UQ-009.
* Evidence: plan lines 314, 480 (D5), 519, 521. PRD CAC-007 says invalid ID shows `Out of stock`. CAC-019 says a positive add at stock 0 shows `Out of stock` and is not added. decisions.md UQ-009 and state.json UQ-009 say the same. Research F4 read one rejection string for all states, and the plan inherited that reading.
* Concern: a valid zero-stock Add, or any invalid-ID Add, would show "Stock unavailable. Available quantity: 0. Reduce the quantity." D5 is labeled "confirmed" while changing an approved message.
* Impact: Breaks explicit user copy answers. Tests would lock in the wrong copy.
* Smallest useful change: map availability 0 (valid or invalid ID) to `Out of stock` for positive Add and Update. Use the rejection string only for overstock at 1 or more. Restate D5, FR-005, and FR-007 to match.
* Action owner: planning parent.
* Exact resolving evidence: P02-T02 tests assert `Out of stock` for stock 0 and for unknown IDs on both paths. FR-005 names only the overstock case.
* Decision route: direct correction. The user's answer resolves it.

### PC-002 [High]: Rejection placement contradicts approved CAC-017

* Related IDs: P03-T01 (line 314), P04-T01 (line 369); CAC-015, CAC-017.
* Evidence: PRD CAC-017 requires placement "near the cart summary within the originating page context" and is local-approved. Research F-surface list names a message region near the Order Summary.
* Concern: the plan places rejections next to the affected row or card and never mentions the cart summary.
* Smallest useful change: put the `role="alert"` region in the cart summary area, and link the affected row or input to it with `aria-describedby`. This satisfies CAC-017 and keeps row association.
* Action owner: planning parent. Exact resolving evidence: P04-T01 names the summary region, and P05-T01 checks its placement.
* Decision route: direct correction.

### PC-003 [High]: Negative Add removal is an engineering choice labeled as confirmed

* Related IDs: FR-003, FR-008, P02-T02 (line 252), D4 (line 479), R2 (line 543).
* Evidence: PRD says no negative-input policy is selected; research U2 flags it. Only the pre-existing Update `<= 0` branch has precedent. Line 313 says explicit 0 is the only removal, which contradicts it.
* Concern: a new Add parameter with a negative value silently deletes a cart item. Nothing approved supports that. D4 is labeled "confirmed", but no user answer covers it.
* Smallest useful change: Add with quantity below 0 is invalid input, with no mutation, message, or log. Keep the Update negative branch as preserved behavior. Relabel D4 as engineering-proposed, pending council confirmation.
* Action owner: planning parent, with the architecture council confirming.
* Exact resolving evidence: a P02-T02 test that Add with -1 leaves the cart unchanged.
* Decision route: direct correction. No user question is needed, because the approved packet does not settle it.

### PC-004 [High]: The binding guarantee cannot be pinned as written

* Related IDs: P01-T02 (line 167), P03-T01 (line 313), R1 (line 542), D4.
* Evidence: research F10 notes no public `Program` class and no net6 `Mvc.Testing`; P01-T02 allows only a minimal binder check, deferring real verification to P05 smoke.
* Concern: the rule that missing or unparsable quantity must never delete depends on binder behavior the test harness cannot reach. If the pin fails, P03 depends on untested binding.
* Smallest useful change: bind `string? quantity` on both actions. Parse it in a pure helper with `int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var n)`. Null, empty, unparsable, or overflow means invalid. This is unit-testable and removes R1.
* Action owner: planning parent.
* Exact resolving evidence: P01-T02 asserts the helper on `""`, `"abc"`, `"2147483648"`, `"-1"`, and `"3"`. P03-T01 drops the `ModelState` wording.
* Decision route: direct correction.

### PC-005 [High]: Fan-out is stale and mismatched with the roster

* Related IDs: G1 (line 550), G3, Deliverables rows 1 to 3 (lines 560 to 562), Planning Readiness "G1 in progress".
* Evidence: PRD v1.0 and its state file (11:33:02) postdate the plan (11:21:58). Roster per team.md.
* Concern: row 1 still reads "in progress" for PRD Builder. Re-dispatch could overwrite the approved v1.0. Rows 2 and 3 list council reviews as Batch A deliverables while G3 lists them as prerequisites, which risks double dispatch.
* Smallest useful change: record PRD 1.0 as written, with independent refresh review pending. Move the architecture and product-fit reviews to G3 prerequisites with no dispatch row. Keep only rows 4 (developer) and 5 (tester review) as dispatchable.
* Action owner: planning parent.
* Exact resolving evidence: the deliverables table lists only dispatchable rows, and G1 cites the PRD state.
* Decision route: direct correction.

### PC-006 [Medium]: No traceability to approved PRD acceptance criteria

* Related IDs: FR-001 to FR-012, NFR-001 to NFR-005, CAC-001 to CAC-024.
* Concern: the plan maps only its own FR and NFR IDs. None of the 24 approved CAC statements is mapped. PC-001 and PC-002 went unnoticed for that reason.
* Smallest useful change: add a crosswalk from CAC-001 to CAC-024 to tasks and tests, and mark any gaps.
* Action owner: planning parent. Exact resolving evidence: each CAC has a task and a test row.
* Decision route: direct correction.

### PC-007 [Medium]: Check precedence is undefined

* Related IDs: P02-T02 (lines 252 to 256), FR-006, FR-008, P03-T01.
* Concern: the order of checks is not stated for these cases: Update over-limit on an absent item (no-op or rejection, and whether it logs); Update 0 on an absent item with zero stock; Add over-limit on an unknown ID; overflow on an absent item.
* Smallest useful change: an ordered rule: unknown ID; invalid quantity (no-op); 0 or negative (per PC-003); absent item on Update (no-op); availability (`Out of stock` at 0, rejection at overstock). One test per step.
* Action owner: planning parent. Exact resolving evidence: the P02-T02 test list has one case per step.
* Decision route: direct correction.

### PC-008 [Medium]: Accessibility mechanics are not pinned

* Related IDs: P04-T01 (lines 373 to 375), R4, CAC-013 to CAC-016.
* Concern: (a) "focus or scroll" offers two unverified mechanisms. (b) `role="alert"` present at page load is not reliably announced; a focusable `tabindex="-1"` container is dependable. (c) disabled `Buy` cannot take focus, so its `aria-describedby` is never announced; card text must carry the cue.
* Smallest useful change: pin one focus target, make card status text the primary cue, and have P05-T01 record focus and announcement as pass or fail.
* Action owner: planning parent. Exact resolving evidence: P05-T01 records focus and announcement results.
* Decision route: direct correction.

### PC-009 [Medium]: Silent malformed input may conflict with CAC-015

* Related IDs: P03-T01 (line 313), D4, CAC-008, CAC-015.
* Concern: malformed or out-of-range quantity gives no message and no log. If that counts as a rejection under CAC-015, which requires announced feedback, the plan fails it. No approved copy exists for that case.
* Smallest useful change: state the interpretation. Either a no-op recorded as accepted residual risk, or a rejection that needs approved copy.
* Action owner: planning parent. This is the only item that may need the user, and only if the parent treats it as a rejection.
* Decision route: direct interpretation; user decision only if CAC-015 applies.

### PC-010 [Medium]: Root-cause and outcome wording is overstated

* Related IDs: Executive Summary (lines 14 to 15), Goals.
* Evidence: `CartController.Checkout` clears the cart and redirects with no inventory check (research U4). PRD and UQ-004 and UQ-006 say 60% is user-selected, not a validated promise, and Finance measures about 3 months after deployment.
* Concern: "only finds out at checkout" implies a checkout rejection the code does not have. "Expected to improve toward 60%" is a promise the packet disclaims.
* Smallest useful change: say "nothing validates quantity at add or update, and checkout does not revalidate," and "60% is the user-selected target, not a validated outcome."
* Action owner: planning parent. Exact resolving evidence: neither phrase remains in the plan.
* Decision route: direct correction.

### PC-011 [Low]: Restore and test-harness failure has no blocker exit

* Related IDs: P01-T01, R3, research U7.
* Concern: restore and framework availability is checked "at start", with no stop condition if both the cached packages and the public feed fail.
* Smallest useful change: state that if restore fails, P01 is Blocked and the parent is notified. Do not swap frameworks.
* Action owner: planning parent. Decision route: direct correction.

### PC-012 [Low]: Logging overlap and decision labels

* Related IDs: P03-T01, D1 to D6, `HomeController` line 39, `CartController` line 28.
* Concern: the existing warning at `HomeController` line 39 would fire alongside the new one; `CartController` line 28 would log a null quantity once nullable. D1 to D6 are all labeled "confirmed" though none is a user answer.
* Smallest useful change: state whether line 39 and line 28 are kept or replaced. Relabel engineering decisions "engineering-proposed (agent-owned)", keeping "approved" only for UQ-backed rules.
* Action owner: planning parent. Decision route: direct correction.

## Strengths and Residual Risk

* Credible: `[JsonIgnore]` matches the serializer in use. Stock is read live from `ProductService`. Catalog, prices, and images are preserved (P01-T02 snapshot). PRG plus `TempData` fits the existing redirects. Checkout is untouched.
* Residual, accepted: focus restoration needs browser smoke (R4). Mobile widths are representative test inputs only, with no claimed standard.

## PRD Refresh Fidelity Status

* Status: aligned with the approved packet: 17 answers, local approval, bounded Jira waiver (UQ-014, B6), exact 40% baseline, 60% user-selected target, 3-month review, desktop 1024 CSS px.
* No literal `UNAPPROVED` string or current-status drift found; Source trace index "Open" rows are labeled historical.
* Hazard: historical 2026-10-17 IDs reuse UQ-001 to UQ-017 with different meanings; cite decisions.md entries downstream.
* Independent refresh review pending; no PRD quality score assessed. Plan G1 status is stale (PC-005).

## Limitations

* No code, test, restore, or browser run; binding claims are static. Targeted reads only. Model not exposed (recorded as unknown).

## Questions or Blocking Evidence Gaps

* None blocking; PC-009 may need the user only if malformed input counts as a rejection.

## Recommended Next Action

* Highest-impact finding: PC-001
* Action owner: planning parent
* Smallest next action: correct the zero-stock and invalid-ID mapping and D5, then apply PC-002 to PC-005 directly, and record the new hash.
* User response required: no

## Closure Conditions

1. Parent resolves PC-001 to PC-005; PC-006 to PC-012 resolved or accepted with evidence.
2. Parent records the revised helper hash, correction delta, and affected IDs.
3. Requirement and acceptance changes require a fresh full assessment via a new revision-closure reservation with predecessor ac5e7ea3505d79bf9d05ec868738091509463976dd817738695b70c9fab2fd2b; targeted closure does not apply.
4. Council G3 artifacts and PRD refresh review remain pre-code prerequisites, outside this closure.

## Artifacts

| Artifact | Description |
|----------|-------------|
| [.copilot-tracking/plans/2026-10-18-inventory-aware-cart-fix-plan.md](.copilot-tracking/plans/2026-10-18-inventory-aware-cart-fix-plan.md) | Plan critiqued, unchanged, hash ac5e7ea3… |
| [.copilot-tracking/research/2026-10-18/inventory-aware-cart-code-research.md](.copilot-tracking/research/2026-10-18/inventory-aware-cart-code-research.md) | Research basis, targeted sections |
| [.copilot-tracking/plans/2026-10-17-inventory-aware-cart-prd.md](.copilot-tracking/plans/2026-10-17-inventory-aware-cart-prd.md) | Refreshed PRD v1.0, alignment check |
| [.copilot-tracking/plans/2026-10-17-inventory-aware-cart-prd.state.json](.copilot-tracking/plans/2026-10-17-inventory-aware-cart-prd.state.json) | PRD 17-answer register |
| [.copilot-tracking/squad/decisions.md](.copilot-tracking/squad/decisions.md) | Approved packet and correction |
| [.copilot-tracking/squad/team.md](.copilot-tracking/squad/team.md) | Roster used for fan-out check |

## Next Steps

* Planning parent (not the user) revises the plan as listed under Recommended Next Action, then requests the fresh full revision-closure assessment in Closure Conditions. No user action is required.
* This standalone critique does not invoke `/rpi-plan`. No compaction is advised.
