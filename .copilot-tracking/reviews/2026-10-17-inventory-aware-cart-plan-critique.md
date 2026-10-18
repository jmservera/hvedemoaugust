<!-- markdownlint-disable-file -->
# RPI Plan Critique: inventory-aware-cart

## Metadata

* Task ID: inventory-aware-cart-init-2026-10-17-001
* Critique date: 2026-10-17
* Plan: .copilot-tracking/plans/2026-10-17-inventory-aware-cart-plan.md
* Invocation outcome: completed
* Assessment execution/availability: Complete
* Critique depth: standard
* Depth provenance: default (no explicit user request for deep)
* Attempt slot consumed: yes
* Attempt ID and kind: inventory-aware-cart-init-2026-10-17-001-critique-initial-01; initial
* Candidate identity and saved hash boundary: plan revision 1 (plan as saved; Critique Disposition, Artifact Self-Check, Planning Readiness and Next Step, Follow-Up Items, Handoff excluded by the projection); sha256 a7755da72352d4b14b397cbb01ef2d602ac0af7494dfaf2360f29449c523dd9f
* Canonical projection evidence: helper .github/skills/rpi/rpi-plan/scripts/Get-PlanAssessmentHash.ps1 under the installed rpi-plan root (plugin 1a0deae6ef6); projection_version rpi-plan-assessment-v1; digest reproduced on a second run (identical); projection not persisted (58,546 characters, recomputable by the helper)
* Revision closure: not applicable (initial attempt)
* Current-run provenance: standalone initial reservation, persisted in this output before assessment and read back
* Original attempt and recovery approval: not applicable
* Prior attempt and reconciliation pointers: none. The plan's Critique Disposition records no critique run and no consumed attempt.
* Human assessor provenance: not applicable

## Inputs and Criterion Boundary

* Task context and caller requirements: independent plan-quality critique (not an implementation or diff review). Scope: documentation-only drafting; no product, requirement, or code approval. Six human packages (primary outcome; baseline/target/evidence/owner/window; disclosure and copy; stock threshold policy; product-approved BRD/PRD/acceptance criteria including zero; approved Jira story) stay open and unwaived.
* Research and evidence considered: .copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md (register rows, gate register, UQ list, self-check); .copilot-tracking/squad/decisions.md, Intake Readiness Verdict 2026-10-17T20:57:34Z (current; 20:45:21Z entry superseded); .copilot-tracking/squad/team.md (tester, analyst, architect, product-owner rows); .github/instructions/squad-council.instructions.md (trigger conditions); rpi-plan references and helper.
* Decisions, dependencies, task Goals, and task Requirements considered: plan FR-001–FR-010, NFR-001–NFR-006, P01–P04 tasks, Planning Decisions D1–D6, STOP checkpoint, Critique Disposition.
* Assessment boundary: the saved plan against its cited research and intake. This critique cannot conclude that the requirements, metrics, copy, or code are correct, and it did not verify repository behavior or re-read the raw snapshots (outside the boundary, per the skill).

## Coverage Assessment

| Requirement, research, phase, or task ID | Coverage | Evidence or concern |
|------------------------------------------|----------|---------------------|
| B1–B6 question wording, speaker roles, locators | Covered | Verbatim match to research gate register for all six; B5 label adds "including zero" (PC-006). |
| Register counts (157: 9 G, 12 N, 12 D, 38 C, 10 M, 15 DEP, 12 R, 8 A, 17 UQ, 24 V) | Covered | Matches research self-check. |
| Spot-checked citations: M-004, M-007–M-009, V-009, V-010, R-010–R-012, A-006, A-007, D-008, D-009, C-006, C-007, C-010–C-021, C-025–C-029, DEP-001, DEP-003–DEP-005, DEP-007, DEP-011–DEP-015 | Covered | Each cited ID carries the meaning and state the plan attributes to it. |
| UQ-001–UQ-017 to B-gate crosswalk | Partial | 16 of 17 supported by question wording; UQ-017 is a weaker inference; checkpoint shows IDs only (PC-003). |
| Zero handling (UQ-012, V-009, R-010, B5) | Partial | Substance correct; "two readings" wording ambiguous (PC-002). |
| Intake C1 boundary A/B separation | Covered | Boundary A only; boundary B Not-Ready; no waiver. |
| Intake non-blocking formal PRD-quality review request | Missing | Not dispositioned (PC-005). |
| Planning-time code observations | Partial | Not in research; unsourced (PC-004). |
| Critique gate status and artifact path | Partial | Recorded as optional; wrong path (PC-001). |
| Council rationale (D1) | Partial | Security-lens wording conflicts with P04-T05 (PC-006). |
| Executable P01–P03 steps | Covered | Tasks are specific; P02 regex and comparisons are executable. |

## Verdict

* Verdict: Revise
* Rationale: No finding blocks documentation drafting. P01 needs only the research artifact and intake boundary A. Five material corrections are needed before finalization, and two should be applied before P01 dispatch. None changes B1–B6 or permits implementation.
* Documentation disposition: **Ready-With-Conditions** (documentation drafting may proceed). Conditions: (1) dispatch P01 with the research artifact and C1 as sole inputs, or apply PC-004 first; (2) apply PC-002 before dispatch; (3) apply PC-003, PC-001, and PC-005 before the P03 STOP is presented; (4) apply PC-006 at finalization.
* Hash covered by this assessment: a7755da72352d4b14b397cbb01ef2d602ac0af7494dfaf2360f29449c523dd9f
* Closure chain: root complete full assessment for plan revision 1. Any changed plan revision requires revision-bound closure.

## Findings

<!-- rpi:critique id=PC-001 -->
### PC-001 [Medium]: Critique gate recorded as optional, with a non-binding critique path

* Related IDs: Critique Disposition; D3; Risks row M ("plan has had no independent plan critique"); Planning Readiness "Latest critique".
* Evidence: plan line 629 (D3, "Plan critique not run"); line 641 (Latest critique cites `.copilot-tracking/reviews/plans/2026-10-17/inventory-aware-cart-plan-critique.md`); line 765 ("Optional" critique); line 788 (Critique Disposition, "open ... Optional"); line 827 (Next Steps).
* Concern: The plan treats the rpi-plan critique gate as optional. It also names a critique path that differs from the caller-binding path `.copilot-tracking/reviews/2026-10-17-inventory-aware-cart-plan-critique.md`.
* Impact: Finalization could cite a path that does not exist, or treat the gate as skipped.
* Smallest useful change: Record this artifact, its Revise verdict, and hash a7755da7…23dd9f. Mark D3 and the Risks "Optional" row as superseded by the requested critique. Replace the `reviews/plans/` path.
* Action owner: planning parent (Squad Coordinator).
* Exact resolving evidence: Critique Disposition cites this exact path, verdict Revise, and the hash above; no "optional" wording remains for the gate.
* Decision route: direct planner correction.

<!-- rpi:critique id=PC-002 -->
### PC-002 [Medium]: "Zero clarification" wording is ambiguous

* Related IDs: UQ-012, V-009, R-010, B5, FR-004.
* Evidence: plan line 25 ("two readings ... This plan applies both"); line 689 ("Clarification questions this plan adds: zero"); line 719 (FR-004).
* Concern: Read as "no clarification needed," the wording could suggest the zero question is settled or may be dropped. The intended meaning, per the plan's own B5 row and the research, is that the quantity-zero meaning is an open question inside B5. No stakeholder question is added, dropped, or answered.
* Impact: A reader may conclude the zero case is resolved, or that the question set was reduced.
* Smallest useful change: Replace line 25 with one reading: "'Zero clarification' means the quantity-zero meaning stays an open question inside B5 (UQ-012, V-009, R-010). This plan adds no stakeholder question and removes none." Make line 689 match.
* Action owner: planning parent.
* Exact resolving evidence: The sentence appears once; the B5 row keeps "What does 'zero' mean ...?" verbatim; UQ-001 through UQ-017 remain present.
* Decision route: direct planner correction.

<!-- rpi:critique id=PC-003 -->
### PC-003 [Medium]: UQ-017 crosswalk is inferred, and the STOP checkpoint shows only IDs for UQ questions

* Related IDs: UQ-017, B2, B5, B6, P04-T05, P03-T01.
* Evidence: plan line 26 (crosswalk labeled navigation aid); line 680 (B2 row lists UQ-017); P04-T05 Dependencies (gates B2, B5, B6); research UQ-017 ("without prohibited customer/session data or new customer analytics") versus B2 question ("What available evidence supports the baseline and target, who owns measurement, and when will results be reviewed?"). Crosswalk checked against research UQ wording: UQ-001→B1; UQ-002–006→B2; UQ-007–009→B3; UQ-010–011→B4; UQ-012–013→B5; UQ-014→B6 all supported. UQ-015/016 non-gating is correct.
* Concern: The privacy and feasibility condition in UQ-017 appears in no package question. The checkpoint's Related UQ column gives IDs only, so the human sees no UQ wording at the STOP.
* Impact: A human could answer B2 without addressing the prohibited-data constraint. "All unresolved questions preserved" is met in the PRD register but not at the STOP.
* Smallest useful change: In P03-T01, require the checkpoint to show verbatim UQ-001–UQ-017 question text (or display the PRD register). Label the UQ-017 to B2 mapping "inferred; also bears on B5/B6 per P04-T05."
* Action owner: planning parent.
* Exact resolving evidence: The STOP section shows UQ-017 text; the mapping is marked inferred; P03-T01 requires UQ text.
* Decision route: direct planner correction before P03.

<!-- rpi:critique id=PC-004 -->
### PC-004 [Medium]: Planning-time code observations are not in the research and carry no evidence pointer

* Related IDs: UQ-012, B5, P04-T02, Research Method boundary ("repository behavior ... not inspected").
* Evidence: plan line 24 (Executive Summary "What You May Not Know": `CartService.UpdateQuantity` "was observed to remove an item when quantity is zero or below"); line 498 and following (P04-T02 observations, including `AddToCart` and `Product.cs`).
* Concern: These code behaviors are stated without file lines, read method, or date. The research says code was not inspected. The zero-removal observation sits before the STOP and is the only concrete zero behavior in the document. That can anchor B5.
* Impact: Evidence fidelity gap, and a risk of an implied zero default. The critique cannot verify the code claims (out of boundary).
* Smallest useful change: Remove the observation from line 24. Keep P04-T02 as an unverified research item to check under rpi-research, or cite the exact read. Do not use it as B5 context.
* Action owner: planning parent (route to research if verification is needed).
* Exact resolving evidence: Line 24 names no UpdateQuantity behavior; P04-T02 labels the items "unverified; not evidence"; no zero behavior appears before the STOP.
* Decision route: direct planner correction.

<!-- rpi:critique id=PC-005 -->
### PC-005 [Low]: Intake's non-blocking formal PRD-quality review request is not dispositioned

* Related IDs: intake 20:57:34Z Clarifying Questions; P02 Details.
* Evidence: decisions.md intake 20:57:34Z (non-blocking request to supply the PRD, active phase, and taxonomy for a formal PRD-quality review); plan P02 Details (tester content review only; no mention of the request).
* Concern: P02 is not the formal PRD Quality Reviewer pass the intake named, and the plan does not say whether that pass is wanted.
* Impact: Traceability gap; a reader may assume a formal PRD-quality review occurred.
* Smallest useful change: Add one P02 Details sentence stating that P02 is a content review and that the formal PRD-quality review request is deferred to the user or a later dispatch after P01. Non-blocking.
* Action owner: planning parent (user only if they want the formal review now).
* Exact resolving evidence: P02 Details names the request and its disposition.
* Decision route: direct planner correction; not a blocking user decision.

<!-- rpi:critique id=PC-006 -->
### PC-006 [Low]: Gate label drift and council rationale conflict

* Related IDs: B5, D1, P04-T01, P04-T05, squad-council trigger conditions.
* Evidence: plan line 683 (B5 label adds "**including zero**"; research label is "Explicit approval of requirements and acceptance criteria"); line 665 ("no ... threat surface changes (security lens)"); line 670 (security lens deferred to a future council); P04-T05 (new warning-log fields).
* Concern: The label changes a research gate label, which weakens exact gate preservation. The council rationale says no threat-surface change while P04-T05 adds a logging surface that the plan defers.
* Smallest useful change: Restore the research B5 label verbatim, keeping "zero" in the question column. Reword line 665 to "no code or threat-surface change in this docs-only run; logging-privacy lens deferred to P04-T01."
* Action owner: planning parent.
* Exact resolving evidence: B5 label matches research text; line 665 no longer claims no threat surface.
* Decision route: direct planner correction.

## Strengths and Residual Risk

* Gate text B1–B6, speaker roles, and locators match the research gate register. No default, owner, approver, copy, threshold, or Jira key is invented. Boundary B stays Not-Ready, and no waiver is recorded.
* The 157-ID breakdown and the spot-checked citations match the research rows.
* Intake handling is correct: 20:57:34Z supersedes 20:45:21Z, and boundary A is separated from boundary B.
* P04 remains conditional and blocked by named gates.
* Residual risk accepted for documentation only: cached sources are not freshness-checked, and repository claims remain unverified.

## Questions or Blocking Evidence Gaps

* None blocks P01 drafting. The formal PRD-quality review choice (PC-005) is optional and is not a decision-critical user question.

## Limitations

* Research and sources were not re-read beyond the supplied artifacts. The raw snapshots, repository code, and routing.md row 13 were not verified.
* PC-004 code claims are unverified by design; the skill forbids research beyond the supplied inputs.
* The helper projection is recomputable but not persisted as immutable evidence (identical digest on re-run).
* No subagents were used. Model identity is unknown.

## Recommended Next Action

* Highest-impact finding: PC-003
* Action owner: planning parent (Squad Coordinator)
* Smallest next action: Apply PC-002 and PC-004 text fixes; dispatch P01 with research-only input; apply PC-003, PC-001, and PC-005 before the P03 STOP.
* User response required: no. The formal PRD-quality review (PC-005) is an optional user choice that does not block P01.

| [.copilot-tracking/plans/2026-10-17-inventory-aware-cart-plan.md](.copilot-tracking/plans/2026-10-17-inventory-aware-cart-plan.md) | Plan critiqued (revision 1, unfinalized) |
| [.copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md](.copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md) | Research artifact checked against the plan (read-only) |
| [.copilot-tracking/squad/decisions.md](.copilot-tracking/squad/decisions.md) | Intake verdict 2026-10-17T20:57:34Z (C1) |

## Next Steps

Return this verdict and disposition to the Squad Coordinator as the planning parent. The coordinator applies the minimal corrections (PC-001 through PC-006), then dispatches P01 with research-only input. It must not dispatch P04 or run `/rpi-implement`. Boundary B stays blocked until the authorized humans answer B1–B6. This standalone critique does not invoke `/rpi-plan`.
