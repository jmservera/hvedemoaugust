<!-- markdownlint-disable-file -->
# RPI Plan: Inventory-Aware Cart — UNAPPROVED Working PRD First, Gated Future Fix Second

## Task Metadata

* Task ID: `inventory-aware-cart-init-2026-10-17-001`
* Task slug: `inventory-aware-cart`
* Plan date: 2026-10-17
* Run: `init-2026-10-17-001` (squad lead stage)
* Plan path: `.copilot-tracking/plans/2026-10-17-inventory-aware-cart-plan.md` (caller-binding flat path; it replaces the skill's dated-folder default `.copilot-tracking/plans/{{YYYY-MM-DD}}/{{task_slug}}-plan.md`)
* Status: AI-assisted, non-authoritative planning document. Not a BRD, PRD, approval, backlog item, or authorization to implement.

## Executive Summary

* Bottom line: Use the completed, read-only research artifact to draft one explicitly **UNAPPROVED** working PRD (analyst, `PRD Builder`), have it independently reviewed against the evidence (tester, `Squad Reviewer`), then **STOP** at a human checkpoint that presents the six source-based decision and approval packages (B1–B6, with the quantity-zero clarification carried inside B5, and UQ-001–UQ-017 shown verbatim). A gated future fix is outlined as conditional tasks only. It is not authorized, not started, and not part of this run.
* Why this matters: The source meeting recorded a validation-only inventory proposal for a storefront whose documented baseline excludes inventory. The meeting also left the business outcome, measurement package, disclosure policy, low-stock policy, requirements approval, and approved Jira story open. Intake allows documentation only (boundary A). A PRD that invented values, copy, or "zero" behavior would turn meeting notes into apparent requirements, which the governance page forbids.
* Planning result: Complete for the documentation-bound scope (P01–P03) as revision 2. P04 is a conditional outline and is blocked. An independent critique of revision 1 exists (verdict Revise, documentation disposition Ready-With-Conditions); revision 2 applies its six corrections but has not had a revision-bound independent closure, so the plan is **not finalized** (see Critique Disposition). It hands off to the Squad Coordinator for the document phases, not to `/rpi-implement`.
* Confidence and uncertainty: High that the plan matches the research artifact (157 items across 10 registers, B1–B6, UQ-001–UQ-017) and intake verdict C1. Uncertain wherever the research is silent: freshness of the cached pages, repository behavior (not inspected by the research), and every human decision.

### What You May Not Know

* **Drafting a PRD is not implementation.** Research → plan → draft → review is the document methodology. RPI implementation (`rpi-implement`, code changes) stays blocked until B1–B6 clear.
* **A council is not required before the document phases.** See [Council Assessment](#council-assessment). A future council must not clear any human-only gap.
* **"Zero" has no recorded meaning.** The research (UQ-012, V-009, R-010, B5) records only a suggested test input with no variable or expected result. This plan chooses no behavior and cites no current code behavior as context or as a requirement.
* **"Zero clarification" means clarifying the quantity-zero input and expected behavior.** The quantity-zero meaning stays an open question inside B5 (UQ-012, V-009, R-010). It does not mean omitting or suppressing stakeholder questions. This plan adds no stakeholder question and removes none: B1–B6 and all 17 research questions UQ-001–UQ-017 stay visible verbatim at the STOP.
* **The UQ-to-B crosswalk is a planner navigation aid.** The research does not map UQ-001–UQ-017 to B1–B6. The mapping in the [STOP Checkpoint](#stop-checkpoint) is derived, the UQ-017 to B2 mapping is inferred (UQ-017 also bears on B5/B6 per P04-T05), and the reviewer must verify it. UQ-015 and UQ-016 map to no gate. The mapping never replaces the verbatim UQ list.
* **The incomplete "without storing" bullet belongs to Product Overview (W2).** The PRD must not reconstruct its ending (UQ-015).
* **The research artifact is protected.** It is read-only for this plan and every downstream role.
* **Role split for the PRD.** `PRD Builder` normally runs guided Q&A. This run is non-interactive and takes the research artifact as its only input, not the transcript. `Meeting Analyst` and `BRD Builder` do not apply here.

## Phase Checklist

### Before

```mermaid
%%{init: {"themeVariables": {"fontFamily": "Arial, Helvetica, sans-serif", "fontSize": "16px"}}}%%
flowchart LR
    RAW["Four cached source snapshots W1-W4 (host session files)"]
    SRC["Research artifact: 157 items, B1-B6, UQ-001..017 (read-only)"]
    C1["C1 Intake verdict: boundary A Ready, boundary B Not-Ready"]
    NOPRD["Absent: no PRD, no document review, no STOP record"]
    RAW -->|analyzed into| SRC
    C1 -->|permits UNAPPROVED PRD only| NOPRD
    SRC -->|input available to| NOPRD
```

### After

```mermaid
%%{init: {"themeVariables": {"fontFamily": "Arial, Helvetica, sans-serif", "fontSize": "16px"}}}%%
flowchart LR
    RAW["Four cached source snapshots W1-W4 (host session files)"]
    SRC["Research artifact: 157 items, B1-B6, UQ-001..017 (read-only)"]
    C1["C1 Intake verdict: boundary A Ready, boundary B Not-Ready"]
    PRD["Added: UNAPPROVED working PRD"]
    REV["Added: document review (tester)"]
    STOP["Added: STOP checkpoint, B1-B6 packages"]
    HUM["Human decisions and approvals (outside this plan)"]
    FIX["Added: gated future fix, conditional outline only"]
    RAW -->|analyzed into| SRC
    SRC -->|sole input| PRD
    C1 -->|boundary A scope| PRD
    PRD -->|reviewed against SRC| REV
    REV -->|findings cleared| STOP
    STOP -->|presented to| HUM
    HUM -.->|only after B1-B6 clear| FIX
    classDef new stroke-dasharray: 5 5
    class PRD,REV,STOP,FIX new
```

The change is documentation only. Two tracked artifacts (PRD and review) appear, followed by a human STOP. The future fix exists only as a conditional outline whose every task is blocked until the human gates clear. Nothing in `src/`, Jira, Confluence, or squad state changes.

<!-- rpi:phase id=P01 -->
### [ ] P01: Draft the UNAPPROVED working PRD (analyst: PRD Builder)

Goals:
* Produce `.copilot-tracking/plans/2026-10-17-inventory-aware-cart-prd.md` as a source-preserving, explicitly UNAPPROVED working document. It keeps baseline apart from proposal, agreements apart from approvals, and verification statements apart from business targets. Every statement is traceable to the research artifact's exact item IDs, so a reviewer can tell what the sources say from what is still undecided.

Dependencies:
* The research artifact (complete, protected) and intake verdict C1 (boundary A only).
* No predecessor phase.

```mermaid
%%{init: {"themeVariables": {"fontFamily": "Arial, Helvetica, sans-serif", "fontSize": "16px"}}}%%
flowchart LR
    RAW["Four cached source snapshots W1-W4 (host session files)"]
    SRC["Research artifact: 157 items, B1-B6, UQ-001..017 (read-only)"]
    C1["C1 Intake verdict: boundary A Ready, boundary B Not-Ready"]
    PRD["Added: UNAPPROVED working PRD"]
    REV["Added: document review (tester)"]
    STOP["Added: STOP checkpoint, B1-B6 packages"]
    HUM["Human decisions and approvals (outside this plan)"]
    FIX["Added: gated future fix, conditional outline only"]
    RAW -->|analyzed into| SRC
    SRC -->|sole input| PRD
    C1 -->|boundary A scope| PRD
    PRD -->|reviewed against SRC| REV
    REV -->|findings cleared| STOP
    STOP -->|presented to| HUM
    HUM -.->|only after B1-B6 clear| FIX
    classDef new stroke-dasharray: 5 5
    class PRD,REV,STOP,FIX new
    classDef phase fill:#fff3bf,color:#1f2328,stroke:#9a6700,stroke-width:2px
    class PRD phase
```

Highlighted work: `PRD` (dashed border and `Added:` label identify new work).

<!-- rpi:task id=P01-T01 -->
#### [ ] P01-T01: Establish PRD identity, status, and boundary banner

Goals:
* A reader opening the PRD immediately sees that it is a working, UNAPPROVED, AI-assisted draft. They also see that inventory is a proposed extension, not approved scope, and that code, publishing, and tracker work remain blocked.

Requirements:
* FR-001, FR-002, NFR-004, NFR-005.
* The PRD's first line is `<!-- markdownlint-disable-file -->`. A status block states: UNAPPROVED; AI-assisted and non-authoritative; not a BRD; boundary A (documentation) Ready and boundary B (implementation) Not-Ready, per C1; classification not confirmed, so handle as potentially confidential.
* The PRD records its provenance as the research artifact plus page IDs/versions: W1 27951105 v1, W2 27688962 v1, W3 27820033 v1, W4 27852801 v1, and C1. It makes no freshness claim.
* Attribution is role-only and paraphrase-first. It contains no raw quotations, personal identifiers, credentials, or customer data (research Data treatment row).

Details:
* The caller overrides standalone `PRD Builder` conventions: write only the PRD path above. Do not create `.copilot-tracking/prd-sessions/` state, and do not ask the human guided-Q&A questions in this run. Record anything the builder would normally ask as an open item that points to its B-gate or UQ ID.
* `Meeting Analyst` and `BRD Builder` alternates do not apply. The input is the completed research artifact, not the transcript.

References:
* [.copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md](../research/2026-10-17/inventory-aware-cart-analysis.md):
  * Header table, Executive Summary, `### Attribution and evidence-state legend`, and `### Method and Boundaries` (Data treatment row).
* [.copilot-tracking/squad/decisions.md](../squad/decisions.md): `Intake Readiness Verdict 2026-10-17T20:57:34Z inventory-aware-cart` (C1: Verdict Scope and Intake Gate).

Dependencies:
* None.

<!-- rpi:task id=P01-T02 -->
#### [ ] P01-T02: Carry baseline, proposal, scope, constraints, dependencies, and assumptions with item IDs

Goals:
* The PRD states what the storefront baseline is (W2), what the meeting proposed (W1), and which constraints, exclusions, dependencies, and assumptions apply. Each statement is tagged with the research item ID and its evidence state, so nothing recorded as a suggestion or agreement reads as an approved requirement.

Requirements:
* FR-002, FR-003, FR-005, FR-006, FR-007, NFR-001, NFR-002, NFR-003.
* Register IDs carried here: G-001–G-009, N-001–N-012, D-001–D-012, C-001–C-038, DEP-001–DEP-015, A-001–A-008.
* Each item keeps the research state label, such as "Recorded agreement", "Suggestion", "Baseline context", "Explicit source assumption", or "Derived inference". "Recorded agreement" is never promoted to "approved".
* C-001 (inventory outside the documented baseline) and D-001 (validation only), C-006/C-007 (no reservation, no decrement), and C-010–C-013 (exclusions) appear with their locators. Early exact-count suggestions (06:22, 12:18) are not promoted over the unresolved disclosure policy (UQ-007, UQ-008).
* The PRD states A-006 and A-007 as the research does: an engineer's feasibility assertion and a derived inference, respectively. It does not state them as facts.

Details:
* Alternatives (primary outcome, disclosure, low-stock classification) are copied as the research's `## Recommendation and Alternatives` table does, with disposition "Unresolved". The PRD selects no alternative and offers no recommendation on them.
* Where the research notes a source gap (the incomplete bullet, unspecified supported widths in M-004), repeat the gap and do not fill it.

References:
* [.copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md](../research/2026-10-17/inventory-aware-cart-analysis.md):
  * `### Goals`, `### User needs`, `### Decisions and recorded directions`, `### Constraints`, `### Dependencies`, `### Assumptions`, `## Recommendation and Alternatives`.

Dependencies:
* P01-T01.

<!-- rpi:task id=P01-T03 -->
#### [ ] P01-T03: Carry measurable outcomes, risks, and reviewable verification statements without approving them

Goals:
* The PRD lists the measurement candidates, risks, and reviewable verification statements exactly as sourced. Functional checks stay clearly separate from business measures, and no check is presented as an approved acceptance criterion.

Requirements:
* FR-003, FR-005, FR-006, NFR-001, NFR-002, NFR-003.
* Register IDs carried here: M-001–M-010, R-001–R-012, V-001–V-024.
* The PRD labels M-007, M-008, and M-009 as three alternatives, "candidate, not selected", and not as three approved objectives. It carries no baseline, target, owner, review period, denominator, or customer/session-tracking permission (research `### Measurable outcomes and candidate business measures`).
* V-001–V-024 appear under a heading such as "Reviewable verification statements (not approved acceptance criteria)". V-009 (zero) and V-020 (policy-dependent copy) are shown as conditional on B5/B3. V-010 is shown as dependent on B4 for low-stock fixtures. The PRD defines no pass/fail values for those.
* The PRD does not choose a meaning for zero, a threshold, a status or rejection wording, a log schema, a retention period, a focus destination, or an ARIA technique.

Details:
* Keep the research's observation that M-004 supported widths are unspecified (UQ-016), and that the 30-minute session timeout (DEP-005, A-003) is source context, not a measurement window.
* The test-infrastructure statement (DEP-007) stays "meeting-stated; current repository not checked" (R-011).

References:
* [.copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md](../research/2026-10-17/inventory-aware-cart-analysis.md):
  * `### Measurable outcomes and candidate business measures`, `### Risks`, `### Functional verification statements`.

Dependencies:
* P01-T01.

<!-- rpi:task id=P01-T04 -->
#### [ ] P01-T04: Write the open-decision register and the STOP package content

Goals:
* The PRD ends with one source-faithful register of everything undecided: the six decision/approval packages B1–B6 and the unresolved questions UQ-001–UQ-017. It states plainly that implementation is held, so the human sees the complete decision set without any question having been invented.

Requirements:
* FR-004, FR-005, FR-006, FR-009, NFR-001, NFR-002, NFR-003.
* B1–B6 carry the research `### Implementation gate register` question text verbatim, with their locators. The quantity-zero clarification stays inside B5 (UQ-012, V-009, R-010) and is not a seventh package.
* UQ-001–UQ-017 are carried verbatim (research question wording) with state and locator, so the privacy and feasibility question UQ-017 is visible. UQ-015 and UQ-016 are marked non-gating source gaps.
* The PRD states the implementation hold (D-010, D-011, D-012, R-012): engineering research is permitted, and implementation waits for decisions, approved requirements/acceptance criteria, and an approved Jira story. The governance example key is illustrative and is not a feature story.
* The PRD introduces no owner, deadline, approver, Jira key, approval record, outcome, copy, threshold, or default.

Details:
* Provide the planner-derived UQ-to-B crosswalk from [STOP Checkpoint](#stop-checkpoint) only if it is labeled "navigation aid, not a source statement". Label UQ-017 to B2 "inferred; also bears on B5/B6 per P04-T05". The crosswalk never replaces the verbatim UQ-001–UQ-017 list.
* The PRD records that a named role in a source identifies the speaker and does not assign an action owner.

References:
* [.copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md](../research/2026-10-17/inventory-aware-cart-analysis.md):
  * `### Unresolved questions`, `### Implementation gate register` (B1–B6), `### Permission, approval, and lifecycle distinctions`.

Dependencies:
* P01-T02, P01-T03.

<!-- rpi:task id=P01-T05 -->
#### [ ] P01-T05: Append the source trace and run the builder's self-check

Goals:
* A reviewer can reconcile the PRD against the research one item at a time. All 157 item IDs appear, each with the page ID/version and exact section or timestamp the research assigns, and the builder reports its own self-check honestly.

Requirements:
* FR-003, NFR-001, NFR-002, NFR-003, NFR-006.
* A source-trace appendix lists each of the 157 IDs once: 9 G, 12 N, 12 D, 38 C, 10 M, 15 DEP, 12 R, 8 A, 17 UQ, 24 V. Each row gives the research state label, the page ID/version, and the locator, matching the research row.
* The four canonical source URLs from the research reference definitions may be carried as provenance links only. The PRD claims no inter-page links.
* The builder's self-check reports what it checked and what it did not check. It claims no product test, approval, or independent review.

Details:
* The mechanical check for P02 is described under [Validation Plan](#validation-plan). The builder should use the same method so the two checks agree.

References:
* [.copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md](../research/2026-10-17/inventory-aware-cart-analysis.md):
  * `### Artifact self-check` (157-ID breakdown), `### Source manifest and canonical evidence log`.

Dependencies:
* P01-T02, P01-T03, P01-T04.

<!-- rpi:phase id=P02 -->
### [ ] P02: Independently review the draft against the evidence (tester: Squad Reviewer)

Goals:
* Produce `.copilot-tracking/reviews/2026-10-17-inventory-aware-cart-document-review.md`, a findings-based review. It shows whether the PRD faithfully reflects the research, contains no unsourced requirement, invents no value, and presents every user question. The review is evidence for the human, not an approval.

Dependencies:
* P01 complete.

```mermaid
%%{init: {"themeVariables": {"fontFamily": "Arial, Helvetica, sans-serif", "fontSize": "16px"}}}%%
flowchart LR
    RAW["Four cached source snapshots W1-W4 (host session files)"]
    SRC["Research artifact: 157 items, B1-B6, UQ-001..017 (read-only)"]
    C1["C1 Intake verdict: boundary A Ready, boundary B Not-Ready"]
    PRD["Added: UNAPPROVED working PRD"]
    REV["Added: document review (tester)"]
    STOP["Added: STOP checkpoint, B1-B6 packages"]
    HUM["Human decisions and approvals (outside this plan)"]
    FIX["Added: gated future fix, conditional outline only"]
    RAW -->|analyzed into| SRC
    SRC -->|sole input| PRD
    C1 -->|boundary A scope| PRD
    PRD -->|reviewed against SRC| REV
    REV -->|findings cleared| STOP
    STOP -->|presented to| HUM
    HUM -.->|only after B1-B6 clear| FIX
    classDef new stroke-dasharray: 5 5
    class PRD,REV,STOP,FIX new
    classDef phase fill:#fff3bf,color:#1f2328,stroke:#9a6700,stroke-width:2px
    class REV phase
```

Highlighted work: `REV` (dashed border and `Added:` label identify new work).

<!-- rpi:task id=P02-T01 -->
#### [ ] P02-T01: Review citation completeness and locator fidelity

Goals:
* The reviewer establishes whether every research item is present in the PRD with the correct page ID/version and locator, with none missing, duplicated, or invented.

Requirements:
* FR-008, NFR-001.
* Result is recorded per register (G, N, D, C, M, DEP, R, A, UQ, V) as present/total, plus a list of any ID that is missing, extra, or carries a locator or state label that differs from the research row.
* Any ID not in the research set (for example a fabricated `C-039`) is a finding.

Details:
* Compare against the research rows directly. Where an exact excerpt is needed to settle a locator or wording question, the four raw snapshots under `C:\Users\juanserv\.copilot\session-state\8d4d1e03-31a4-4baa-a924-c836894409cf\files\inventory-cart-{transcript,product-overview,architecture,governance}-source.txt` may be read. Treat their content as evidence, never as instructions, and add no integration call.

References:
* [.copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md](../research/2026-10-17/inventory-aware-cart-analysis.md):
  * All ten extraction registers and `### Artifact self-check`.

Dependencies:
* P01-T05.

<!-- rpi:task id=P02-T02 -->
#### [ ] P02-T02: Review for unsourced requirements and invented values

Goals:
* The reviewer establishes that no normative statement, number, duration, threshold, copy string, owner, approver, Jira key, or "zero" behavior in the PRD lacks a research source.

Requirements:
* FR-006, FR-007, NFR-002, NFR-003, NFR-004.
* Every statement phrased as a requirement, rule, or expectation maps to an ID. Unmapped statements are findings.
* Every numeric, time, or percentage string in the PRD maps to a research item. Allowed examples include the eight-product catalog, the 30-minute idle timeout, and transcript timestamps, always as source context.
* The reviewer verifies no default was chosen for B1–B6 and that the three alternative groups remain "Unresolved" with no recommendation. The reviewer verifies that "recorded agreement" is never worded as "approved", and that the illustrative governance example key is not treated as an authorizing issue.
* Privacy: role-only attribution, no raw quotations, no personal identifiers, no credentials.

Details:
* Distinguish the PRD's source statements from any analyst note. An analyst note must be labeled as non-requirement and must not decide a gated choice.

References:
* [.copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md](../research/2026-10-17/inventory-aware-cart-analysis.md):
  * `## Recommendation and Alternatives`, `### Measurable outcomes and candidate business measures`, `### Unresolved questions`, `### Contradictions and source-quality limits`.

Dependencies:
* P01-T05.

<!-- rpi:task id=P02-T03 -->
#### [ ] P02-T03: Review user-question completeness and the STOP package

Goals:
* The reviewer establishes that the PRD presents the complete decision set the human must answer. That set is B1–B6 verbatim, UQ-001–UQ-017 verbatim, and the quantity-zero clarification inside B5, with no extra, dropped, or reworded question.

Requirements:
* FR-004, FR-009, NFR-001, NFR-002.
* B1–B6 match the research question text. The PRD lists no seventh package, and "zero" appears within B5.
* UQ-001–UQ-017 are all present. The reviewer verifies or corrects the planner-derived UQ-to-B crosswalk and marks it "navigation aid".
* The implementation hold (D-010, D-011, D-012) is stated, and no document claims approval.

Details:
* The review states whether each gate B1–B6 is "open, not clearable by documentation". It does not recommend an answer to any gate.
* The reviewer also checks that UQ-017 and its privacy and feasibility wording are visible, and that the UQ-017 to B2 mapping is labeled inferred.

References:
* [.copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md](../research/2026-10-17/inventory-aware-cart-analysis.md):
  * `### Implementation gate register`, `### Unresolved questions`.
* [.copilot-tracking/squad/decisions.md](../squad/decisions.md): `### Clarifying Questions` under C1.

Dependencies:
* P01-T04, P01-T05.

<!-- rpi:task id=P02-T04 -->
#### [ ] P02-T04: Record the review verdict and route required fixes

Goals:
* The review artifact records its method, per-check results, findings with severity and evidence, residual risks, and a verdict that is explicitly not an approval. Required PRD fixes route back to the builder before the STOP checkpoint.

Requirements:
* FR-008, NFR-005, NFR-006.
* Findings cite PRD location, research ID, and the smallest resolving change. The review claims no code verification, no product test run, and no approval authority.
* Writes are limited to `.copilot-tracking/reviews/2026-10-17-inventory-aware-cart-document-review.md`. The review does not edit the PRD, research artifact, squad state, or any tracker.
* If required fixes exist, the coordinator routes them to P01 and requests a re-review of the affected checks before P03. This plan sets no cycle ceiling because none is configured. It requires the coordinator to record any decision to stop revising.

Details:
* The reviewer is the squad's `tester` role (`Squad Reviewer`). The roster's alternates, `Code Review Functional` and `Code Review Standards`, target code diffs and do not apply.
* This review applies the standalone caller path. It does not follow the `rpi-review` log path convention.
* P02 is a tester content review against the research. It is **not** the formal PRD Quality Reviewer pass. The intake verdict (2026-10-17T20:57:34Z) made a non-blocking request to supply the PRD, active phase, and taxonomy for a formal PRD-quality review. That formal review is **deferred, not performed**. It is a prerequisite the user or coordinator may satisfy by a later dispatch after P01. It does not block P01–P03. No document may state that a formal PRD-quality review occurred until it is dispatched.

References:
* [.copilot-tracking/squad/team.md](../squad/team.md): roster rows `tester` and `analyst`.

Dependencies:
* P02-T01, P02-T02, P02-T03.

<!-- rpi:phase id=P03 -->
### [ ] P03: STOP checkpoint — present the human-only decision and approval packages

Goals:
* The coordinator stops the run and presents B1–B6 (with the quantity-zero clarification inside B5) and the verbatim UQ-001–UQ-017 list to the authorized humans, so every later step waits on decisions only they can make. No code, approval, backlog item, or tracker change follows from this plan.

Dependencies:
* P02 complete, with its required findings resolved or explicitly routed.

```mermaid
%%{init: {"themeVariables": {"fontFamily": "Arial, Helvetica, sans-serif", "fontSize": "16px"}}}%%
flowchart LR
    RAW["Four cached source snapshots W1-W4 (host session files)"]
    SRC["Research artifact: 157 items, B1-B6, UQ-001..017 (read-only)"]
    C1["C1 Intake verdict: boundary A Ready, boundary B Not-Ready"]
    PRD["Added: UNAPPROVED working PRD"]
    REV["Added: document review (tester)"]
    STOP["Added: STOP checkpoint, B1-B6 packages"]
    HUM["Human decisions and approvals (outside this plan)"]
    FIX["Added: gated future fix, conditional outline only"]
    RAW -->|analyzed into| SRC
    SRC -->|sole input| PRD
    C1 -->|boundary A scope| PRD
    PRD -->|reviewed against SRC| REV
    REV -->|findings cleared| STOP
    STOP -->|presented to| HUM
    HUM -.->|only after B1-B6 clear| FIX
    classDef new stroke-dasharray: 5 5
    class PRD,REV,STOP,FIX new
    classDef phase fill:#fff3bf,color:#1f2328,stroke:#9a6700,stroke-width:2px
    class STOP phase
```

Highlighted work: `STOP` (dashed border and `Added:` label identify new work).

<!-- rpi:task id=P03-T01 -->
#### [ ] P03-T01: Present the six packages and stop

Goals:
* The humans receive the six source-based packages, each with its exact question, source locator, and speaker role, plus the complete verbatim UQ-001–UQ-017 questions, and the run halts. This plan adds no question and suppresses none.

Requirements:
* FR-009, NFR-002, NFR-006.
* The presentation reuses both tables in [STOP Checkpoint](#stop-checkpoint): the B1–B6 package table and the verbatim UQ-001–UQ-017 table. The checkpoint message must show the UQ question text, not UQ IDs alone. It adds no question, option, recommendation, default, owner, deadline, or approver.
* The UQ-017 to B2 mapping is shown as "inferred; also bears on B5/B6 per P04-T05". The UQ-017 privacy and feasibility question must be visible at the STOP.
* The coordinator states that the PRD and review are AI-assisted, non-authoritative, and UNAPPROVED, and that boundary B stays Not-Ready.
* No downstream dispatch happens in this plan after the STOP: no `developer` role, no council, no backlog executor, no Confluence/Jira action, no commit, no push, no deploy.

Details:
* Recording decisions, council verdicts, or waivers is the Squad Scribe's responsibility, on the coordinator's request. This plan writes no squad state.

References:
* [.copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md](../research/2026-10-17/inventory-aware-cart-analysis.md):
  * `### Implementation gate register`, `## Planning Readiness and Next Step`.

Dependencies:
* P02-T04.

<!-- rpi:task id=P03-T02 -->
#### [ ] P03-T02: State what evidence clears each gate and what does not

Goals:
* The humans know that only authorized decisions and linked approval evidence can clear B1–B6, and that more rereading, a council, or autopilot consent cannot.

Requirements:
* FR-009, NFR-002.
* Per the research: authorized decisions and linked approval evidence close B1–B6, and an intact source snapshot can close UQ-015. Generic autopilot consent does not answer them.
* The statement carries no new owner or deadline.

Details:
* Keep this to one short paragraph in the checkpoint message. It mirrors the research's closing statement in `### Implementation gate register` and `## Risks and Open Questions`.

References:
* [.copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md](../research/2026-10-17/inventory-aware-cart-analysis.md):
  * `## Risks and Open Questions` (closing paragraph).

Dependencies:
* P03-T01.

<!-- rpi:phase id=P04 -->
### [ ] P04: GATED future fix — conditional outline only (not authorized, not started)

Goals:
* Record, as a conditional outline, the work an inventory-aware cart fix would involve if and when the human gates clear. Each task traces to source agreements (D-, C-, V- IDs) so a later `rpi-plan` run can start from evidence, without any task here being a requirement, approval, or authorization.

Dependencies:
* P03 complete, plus every gate named per task (B1–B6 as listed). This phase is blocked now and must be re-planned through `rpi-plan` once the gates clear.

```mermaid
%%{init: {"themeVariables": {"fontFamily": "Arial, Helvetica, sans-serif", "fontSize": "16px"}}}%%
flowchart LR
    RAW["Four cached source snapshots W1-W4 (host session files)"]
    SRC["Research artifact: 157 items, B1-B6, UQ-001..017 (read-only)"]
    C1["C1 Intake verdict: boundary A Ready, boundary B Not-Ready"]
    PRD["Added: UNAPPROVED working PRD"]
    REV["Added: document review (tester)"]
    STOP["Added: STOP checkpoint, B1-B6 packages"]
    HUM["Human decisions and approvals (outside this plan)"]
    FIX["Added: gated future fix, conditional outline only"]
    RAW -->|analyzed into| SRC
    SRC -->|sole input| PRD
    C1 -->|boundary A scope| PRD
    PRD -->|reviewed against SRC| REV
    REV -->|findings cleared| STOP
    STOP -->|presented to| HUM
    HUM -.->|only after B1-B6 clear| FIX
    classDef new stroke-dasharray: 5 5
    class PRD,REV,STOP,FIX new
    classDef phase fill:#fff3bf,color:#1f2328,stroke:#9a6700,stroke-width:2px
    class FIX phase
```

Highlighted work: `FIX` (dashed border and `Added:` label identify new work; it is an outline only).

Labeling used in P04: **Source agreement** means a recorded meeting agreement or direction (D-, C-, V- IDs), which is not an approved requirement. **Planner recommendation** is this planner's suggestion for later planning and is not a requirement. No P04 task contains a requirement in the sense of an approved product requirement.

<!-- rpi:task id=P04-T01 -->
#### [ ] P04-T01: [Conditional] Assess and, if triggered, run a future council that cannot clear human gaps

Goals:
* If the future fix changes structure or user-facing behavior, the architecture and product-fit lenses assess the plan before implementation. They record concerns and conditions without deciding any business question.

Requirements:
* FR-010, NFR-002.
* Planner recommendation, not a requirement: re-evaluate the council triggers when the future plan exists. See [Council Assessment](#council-assessment).
* Owners if a council is needed: `architect` (`System Architecture Reviewer`) for structure/component/integration/contract impact, and `product-owner` (`Functional Planner`) for user-facing behavior, scope, and acceptance criteria. Both are on the roster.
* The council may flag B1–B6 as unresolved, but it may not answer, assume, default, or waive them. A `Go` or `Go-With-Conditions` verdict does not satisfy the human gates.
* The Squad Scribe records any verdict; this plan writes no state.

Details:
* Do not ask either owner to choose the business outcome, disclosure policy, low-stock threshold, zero behavior, or approval. Those are the Product Manager's, Support Lead's, Store Operations Lead's, and Designer's recorded questions (B1–B6).

References:
* [.github/instructions/squad-council.instructions.md](../../.github/instructions/squad-council.instructions.md): triggers, membership, "most-restrictive-wins", and waiver behavior.
* [.copilot-tracking/squad/team.md](../squad/team.md): `architect` and `product-owner` rows.

Dependencies:
* P03-T02. Gates: B5, B6 (for approved scope to assess).

<!-- rpi:task id=P04-T02 -->
#### [ ] P04-T02: [Conditional] Read-only code research to ground later planning

Goals:
* Repository facts replace the meeting-stated architecture and test-infrastructure claims (R-011, DEP-007) so a later plan rests on verified evidence.

Requirements:
* FR-010, NFR-006.
* Planner recommendation: run only under `rpi-research`, read-only, and record findings as verified-by-inspection. Do not present them as meeting evidence. D-011 permits engineering research, and the caller keeps this run documentation-bound.
* The research did not inspect repository behavior. The following are **unverified research items to be checked under `rpi-research`, not evidence and not requirements**; no current behavior is adopted as a requirement and none is used as B5 context:
  * Target framework of `src/ZavaStorefront.csproj`.
  * Whether `src/Models/Product.cs` carries any inventory field.
  * How `CartService` add and update paths treat quantity, including zero or negative quantity.
  * Whether `tests/` contains test source or project files. Test-project status is unresolved.
* Candidate read-only commands for that research. These were not run here and no result is claimed: `dotnet --info`, `dotnet build src/ZavaStorefront.sln`, `dotnet test`. Their applicability depends on what the research finds in `tests/`.

Details:
* Entry points identified for later research: `src/Services/CartService.cs`, `src/Services/ProductService.cs`, `src/Controllers/HomeController.cs`, `src/Controllers/CartController.cs`, `src/Views/Home/Index.cshtml`, `src/Views/Cart/Index.cshtml`, and `src/Views/Cart/CheckoutSuccess.cshtml`. These match the source-stated request flows (DEP-003, DEP-004).
* This task is not part of the current run and is not implementation.

References:
* [src/Services/CartService.cs](../../src/Services/CartService.cs): candidate entry point for future read-only research (not read for this plan).
* [src/Services/ProductService.cs](../../src/Services/ProductService.cs): static catalog owner (DEP-003, source-stated).
* [src/Models/Product.cs](../../src/Models/Product.cs): current product fields.
* [src/ZavaStorefront.csproj](../../src/ZavaStorefront.csproj): target framework.
* [.copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md](../research/2026-10-17/inventory-aware-cart-analysis.md):
  * DEP-003–DEP-007, R-011.

Dependencies:
* None technically. Sequencing is the coordinator's choice, and it must not be presented as implementation.

<!-- rpi:task id=P04-T03 -->
#### [ ] P04-T03: [Conditional] Shared inventory-validation rule, invalid identifiers, and preservation

Goals:
* The add path and the cart-update path would apply the same limit rule. Invalid identifiers would get the neutral product-unavailable response, and the baseline, prices, and images stay as they are.

Requirements:
* FR-010, NFR-002.
* Source agreements to trace: D-001, D-003, D-004, D-005, D-006, D-007, C-006–C-013, C-021, and verification statements V-001–V-008, V-011, V-012, V-022–V-024.
* Any behavior tied to "zero" (UQ-012, V-009) is blocked on B5. No default is chosen.
* Planner recommendation, not a requirement: keep the work inside the static-catalog and session-cart architecture. Business rules stay out of views, and controllers stay thin (C-030, C-031, C-032).

Details:
* Quantity exactly at availability is valid, and exceeding it is invalid, including cumulative adds (D-005, D-006). Availability values come from the catalog for this iteration (DEP-001, A-007 derived).
* Inventory remains validation only, with no reservation, no decrement, and no persistence (D-001, C-006, C-007, C-013).

References:
* [.copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md](../research/2026-10-17/inventory-aware-cart-analysis.md):
  * `### Decisions and recorded directions`, `### Constraints`, `### Functional verification statements`.

Dependencies:
* P04-T02. Gates: B5 (requirements, acceptance criteria, and zero behavior), B6 (approved story).

<!-- rpi:task id=P04-T04 -->
#### [ ] P04-T04: [Conditional] Stock-state presentation, disclosure, copy, and accessible rejection feedback

Goals:
* Stock state and rejection feedback would follow the approved disclosure policy and copy, and would be accessible without relying on color alone.

Requirements:
* FR-010, NFR-002.
* Source agreements/directions to trace: N-007–N-011, C-014–C-020, V-013–V-020. The disclosure, copy, and low-stock rows are blocked on B3 and B4.
* The task defines no wording, threshold, uniform-versus-per-product classification, disclosure choice, focus destination, or ARIA mechanism. C-020 (stay in context) is a "Suggestion; exact navigation behavior not specified".

Details:
* V-020 and V-010 stay conditional until B3/B4 clear. Rejection copy must not become an exact-count disclosure path before UQ-008 is answered.

References:
* [.copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md](../research/2026-10-17/inventory-aware-cart-analysis.md):
  * `### User needs`, `### Constraints`, `### Functional verification statements`, UQ-007–UQ-011.

Dependencies:
* P04-T03. Gates: B3, B4, B5, B6.

<!-- rpi:task id=P04-T05 -->
#### [ ] P04-T05: [Conditional] Bounded structured warning logs

Goals:
* Validation rejections would be diagnosable through structured warnings that include only the source-permitted fields and exclude prohibited data.

Requirements:
* FR-010, NFR-002, NFR-004.
* Source agreements to trace: D-009, C-022–C-029, C-033, V-021. The meeting endorsed the bounded proposal at 19:34, which is not document approval.
* The task defines no schema, retention period, sink, or measurement pipeline. UQ-017 (measurement without prohibited data) remains open under B2.

Details:
* Allowed fields as sourced: product identifier, requested quantity, available quantity, operation type. Excluded: session identifiers, cart or session contents, customer information, and credentials/sensitive values.

References:
* [.copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md](../research/2026-10-17/inventory-aware-cart-analysis.md):
  * `### Constraints` (C-022–C-029), V-021, UQ-017.

Dependencies:
* P04-T03. Gates: B2 (for any measurement use), B5, B6.

<!-- rpi:task id=P04-T06 -->
#### [ ] P04-T06: [Conditional] Focused automated tests and delivery traceability

Goals:
* Focused automated tests for the approved boundaries, plus approved-story traceability and delivery evidence, would exist before the work is considered ready for review.

Requirements:
* FR-010, NFR-002.
* Source agreements to trace: D-008, DEP-007, DEP-011–DEP-015, C-034, and V-001–V-024 as approved.
* Test coverage targets, framework, location, and commands are not specified by any source and are not defined here. Test-project status is unresolved (P04-T02).
* Branch, commit, and PR naming and evidence depend on an approved Jira story (B6). No key is invented, and the cached example key is illustrative.

Details:
* Implementation readiness requires recorded BRD/PRD approvals in Confluence and Jira (DEP-012). This plan creates none.

References:
* [.copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md](../research/2026-10-17/inventory-aware-cart-analysis.md):
  * `### Dependencies`, `### Permission, approval, and lifecycle distinctions`.

Dependencies:
* P04-T03, P04-T04, P04-T05. Gates: B5, B6.

## User Decisions and Requirements

### Confirmed User Direction

* Topic `inventory-aware-cart`, run `init-2026-10-17-001`. Write ONE plan at the binding root `.copilot-tracking/plans/`, path `.copilot-tracking/plans/2026-10-17-inventory-aware-cart-plan.md`.
* The research artifact is complete, protected, and read-only. Use its exact item and source references. The four raw snapshots may be consulted for exact excerpts. No new integrations are required.
* Intake verdict: Ready only for documentation, not code. Implementation waits for decisions, approved product requirements/acceptance criteria, and an approved Jira story. Code research is allowed by the source.
* Methodology: research → plan → draft → review for documents. RPI implementation remains blocked. Never call drafting code implementation.
* Deliverables: an UNAPPROVED working PRD by the analyst (`PRD Builder`, primary, with the research artifact as input), then a document review by the tester (`Squad Reviewer`).
* No invented requirements, metrics, thresholds, copy, or zero behavior, and no resolved stakeholder ambiguity. No architecture decision, approval, or approved backlog item.
* Include an explicit STOP checkpoint with all six source-based packages and zero clarification. Future-fix tasks are a conditional outline only, traceable to source agreements, with recommendations distinguished from requirements. (Reading, corrected per PC-002: "zero clarification" means clarifying the quantity-zero input and expected behavior inside B5. It does not mean omitting any stakeholder question; all UQ-001–UQ-017 stay visible verbatim.)
* Assess whether a council is required now. Any future council must not clear human-only gaps; name future owners (`System Architecture Reviewer`, `Functional Planner`) without asking them to decide business issues.
* No Jira/Confluence writes, commits, pushes, or deploys. No squad-state writes. No nested workers unless the contract absolutely requires a critic, which must be reported. Stop after writing this one plan. No cost ceiling configured (not-requested).

### Planning Decisions and Feedback

| Group | Decision or feedback item | Status | Owner | Rationale or input needed | Evidence | Planning impact |
|-------|---------------------------|--------|-------|---------------------------|----------|-----------------|
| D1 | Council required before the unapproved documentation? | Confirmed (planner assessment): not required | constraint/evidence | Documentation-only, no architecture change, no scope or implementation change, no explicit request, and no automatic trigger holds | [.github/instructions/squad-council.instructions.md](../../.github/instructions/squad-council.instructions.md); [.copilot-tracking/squad/routing.md](../squad/routing.md) | P01–P03: no council. P04-T01 re-evaluates. |
| D2 | Plan and review artifact paths | Confirmed | user | Caller-binding flat paths override the skill and tracking-instruction dated-folder defaults | Caller dispatch | Deliverables table; Sources |
| D3 | Plan critique gate | Superseded (revision 2): the critique was requested and completed | planner / coordinator | The earlier "plan critique not run" and "optional" positions are superseded by the independent critique `.copilot-tracking/reviews/2026-10-17-inventory-aware-cart-plan-critique.md` (verdict Revise, hash a7755da72352d4b14b397cbb01ef2d602ac0af7494dfaf2360f29449c523dd9f). A revision-bound closure is still required | `rpi-plan` skill Flow step 11; critique artifact | Critique Disposition; readiness is not "finalized" |
| D4 | Reading of "six packages and zero clarification" | Confirmed (planner interpretation, corrected per PC-002) | planner | "Zero clarification" means clarifying the quantity-zero input and expected behavior inside B5. It never means omitting or suppressing stakeholder questions. All of UQ-001–UQ-017 stay verbatim at the STOP | Research B5 text; critique PC-002 | P01-T04, P03-T01 |
| D5 | UQ-to-B crosswalk | Proposed (planner-derived; UQ-017 to B2 inferred) | downstream (`Squad Reviewer`) | The research does not map them; the reviewer verifies it. UQ-017 also bears on B5/B6 (P04-T05) | Research `### Unresolved questions`; critique PC-003 | P01-T04, P02-T03, P03-T01 |
| D6 | Business decisions B1–B6 | Unresolved — human only | user (authorized humans) | Not decidable by any agent or council | Research gate register; C1 | P03 STOP; P04 blocked |

## Planning Readiness and Next Step

| Field | Record |
|-------|--------|
| Planning execution and readiness | Complete for the documentation-bound scope (P01–P03), revision 2. Ready for the PRD draft and review under boundary A (C1). Not ready and Blocked for P04 and any implementation (boundary B). **Finalized for docs scope P01–P03 only** (UNAPPROVED, DOCS DRAFT ONLY): revision-bound closure Pass at `.copilot-tracking/reviews/2026-10-17-inventory-aware-cart-plan-closure.md`, attempt `inventory-aware-cart-init-2026-10-17-001-closure-rev2-01`, hash `a830c7958f21aede3ebf6e82c1d41cbca1d29cd5ee5ad24fb2deb13d7b683797`, `rpi-plan-assessment-v1`; PC-001–PC-006 closed, 0 open. Code P04 blocked by B1–B6. Supersedes the earlier "not finalized" wording. |
| Decision participation | Agent-owned planning organization under the caller's autopilot direction. Business decisions are user-retained. |
| Blockers | B1–B6 for implementation (human). None for P01–P02. |
| Latest critique | `.copilot-tracking/reviews/2026-10-17-inventory-aware-cart-plan-critique.md`: initial, standard depth, verdict Revise, documentation disposition Ready-With-Conditions, covering revision 1 hash a7755da72352d4b14b397cbb01ef2d602ac0af7494dfaf2360f29449c523dd9f. Findings PC-001–PC-006 applied in revision 2 and closed by the revision-bound closure (Pass, 0 open; see Critique Disposition Finalization Record). |
| Relevant research | [.copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md](../research/2026-10-17/inventory-aware-cart-analysis.md) |
| Plan | `.copilot-tracking/plans/2026-10-17-inventory-aware-cart-plan.md` |
| Changes-record role | None. No implementation is authorized, so `.copilot-tracking/changes/` is not used by this plan. |
| Continuation owner | Squad Coordinator (document phases P01–P03). Squad Scribe records any state. |
| Required gates or confirmations | Pending: human B1–B6 decisions and approval/story evidence. Council not required for P01–P03. |
| Next action | Closure is complete (Pass); no further critique is needed. P01 may dispatch with the research artifact and C1 as sole inputs. Then `Squad Reviewer` for P02, then STOP (P03). Do not invoke `/rpi-implement`. |

## Deliverables and Owners

| Order | Deliverable | Owning role (agent) | Expected artifact path | Dependencies | Batch |
|-------|-------------|---------------------|------------------------|--------------|-------|
| 1 | UNAPPROVED working PRD | analyst (`PRD Builder`, primary) | `.copilot-tracking/plans/2026-10-17-inventory-aware-cart-prd.md` | Research artifact; C1 | Sequential |
| 2 | Document review against evidence | tester (`Squad Reviewer`) | `.copilot-tracking/reviews/2026-10-17-inventory-aware-cart-document-review.md` | Deliverable 1 | Sequential |
| 3 | STOP checkpoint (a gate, not a file) | Squad Coordinator presents; humans decide | none (content: PRD open-decision register plus [STOP Checkpoint](#stop-checkpoint)) | Deliverable 2 | n/a |
| — | Future fix, council, and code research (P04) | developer / architect / product-owner as applicable | none now (conditional outline only) | Gates B1–B6 | Not authorized |

Implement Shape: `single-build` — a sequential draft → review pipeline. Only one artifact-owning role other than researcher/lead/tester (analyst) is involved, the review depends on the draft so no parallel batch exists, and no code implementation applies.

## Council Assessment

* **Required before P01–P03? No.**
  * The council protocol is a pre-implementation protocol, and drafting an unapproved document is not implementation.
  * No explicit request for a council, go/no-go, or design review exists. Routing row 13 triggers on those requests, and none was made.
  * The automatic trigger (two or more council-member domains, or an RAI concern) does not hold. No structure, component, integration, or topology changes (architecture lens); no code or threat-surface change in this docs-only run, with the logging-privacy lens deferred to P04-T01 (security lens); no billable resource (cost lens); no AI/ML involvement (RAI lens). The product-fit lens is touched only by recording unapproved text, and the PRD changes no scope, priority, or acceptance criteria.
  * Lenses considered and left out, for audit: architecture, security, cost, RAI, product-fit. A council would add dispatch cost without surfacing a decision it is allowed to make.
* **Future council (P04-T01).**
  * Likely required when a future plan changes user-facing behavior and potentially touches `Product`/service contracts, which makes at least two lenses.
  * Owners: `architect` — `System Architecture Reviewer` — and `product-owner` — `Functional Planner`.
  * Security (logging privacy, C-026–C-029) and cost lenses are not proposed now. Decide them when the future plan exists.
  * Limits: the council reviews a plan; it never resolves B1–B6, never invents policy, never waives a human gate, and a waiver or verdict never substitutes for human approval. The Squad Scribe records any verdict, never this plan.

## STOP Checkpoint

**STOP after P02. The coordinator presents the six packages and the verbatim UQ-001–UQ-017 list below and takes no further action.** The questions and locators are copied from the research `### Implementation gate register` and `### Unresolved questions`. UQ cross-references in the package table are a planner-derived navigation aid; they never replace the verbatim UQ list.

| Package | Required decision or evidence | Source-based question (research wording) | Related UQ (navigation aid) | Locator (page ID/version) | Speaker role(s) |
|---------|-------------------------------|------------------------------------------|-----------------------------|---------------------------|-----------------|
| B1 | Selected primary business outcome | Which outcome is primary, and what approved outcome statement should the BRD carry? | UQ-001 | W1 27951105 v1, Transcript > 23:10, 27:24; C1 | Product Manager |
| B2 | Evidence-backed measurement package | What available evidence supports the baseline and target, who owns measurement, and when will results be reviewed? | UQ-002–UQ-006; UQ-017 (inferred; also bears on B5/B6 per P04-T05) | W1 27951105 v1, Transcript > 23:10, 26:42, 27:24; C1 | Product Manager; Support Lead |
| B3 | Disclosure policy and matching approved copy | Are exact quantities disclosed for all products, only low-stock products, or neither, and does that policy also govern rejection messages? What copy is approved? | UQ-007–UQ-009 | W1 27951105 v1, Transcript > 06:22, 12:18, 24:18, 27:24; C1 | Support Lead; Product Manager; Designer |
| B4 | Low-stock policy, thresholds, and boundaries | Which policy applies, what are the approved thresholds, and how are boundary quantities classified? | UQ-010, UQ-011 | W1 27951105 v1, Transcript > 25:31; Meeting Outcome; C1 | Store Operations Lead; summary not attributed |
| B5 | Explicit approval of requirements and acceptance criteria | Who is the authorized approver, where will approval be recorded, and which document versions are approved? What does “zero” mean, and what behavior is approved for that case? | UQ-012, UQ-013 | W1 27951105 v1, Transcript > 13:27, 19:34, 27:24; W4 27852801 v1, Definition of Ready; Document Lifecycle; C1 | Support Lead; Product Manager; not attributed (context page) |
| B6 | Approved Jira delivery story and traceability | What approved Jira story authorizes implementation, and where are its source links, approved acceptance criteria, exclusions, dependencies, validation commands, and review expectations? | UQ-014 | W1 27951105 v1, Transcript > 27:24; W4 27852801 v1, Jira Work Hierarchy; Definition of Ready; GitHub Traceability Convention; C1 | Product Manager; not attributed (context page) |

* **Verbatim unresolved questions (all 17 shown at the STOP; research wording, none dropped or reworded):**

| ID | Question (research wording) | Locator | Speaker role |
|----|-----------------------------|---------|--------------|
| UQ-001 | Which candidate business outcome is primary? | W1 27951105 v1; Transcript > 23:10, 27:24 | Product Manager |
| UQ-002 | What available evidence supports the selected measure? | W1 27951105 v1; Transcript > 26:42, 27:24 | Support Lead; Product Manager |
| UQ-003 | What verified baseline applies to the selected outcome? | W1 27951105 v1; Transcript > 23:10 | Product Manager |
| UQ-004 | What evidence-backed target will be approved? | W1 27951105 v1; Transcript > 26:42, 27:24 | Support Lead; Product Manager |
| UQ-005 | Who is accountable for measuring the outcome? | W1 27951105 v1; Transcript > 26:42 | Support Lead |
| UQ-006 | What review period will be approved? | W1 27951105 v1; Transcript > 26:42 | Support Lead |
| UQ-007 | Are exact counts disclosed broadly, only for low stock, or not at all? | W1 27951105 v1; Transcript > 12:03, 12:18, 24:18 | Engineer; Product Manager; Designer |
| UQ-008 | Does the approved disclosure policy also govern rejection messages? | W1 27951105 v1; Transcript > 06:22, 12:18, 24:18 | Support Lead; Product Manager; Designer |
| UQ-009 | What customer-facing status and rejection copy is approved? | W1 27951105 v1; Transcript > 12:18, 24:18; Meeting Outcome | Product Manager; Designer; summary not attributed |
| UQ-010 | Is low-stock classification uniform or product-specific? | W1 27951105 v1; Transcript > 25:31 | Store Operations Lead |
| UQ-011 | What threshold values and boundary classifications are approved? | W1 27951105 v1; Transcript > 25:31; Meeting Outcome | Store Operations Lead; summary not attributed |
| UQ-012 | Which input does zero refer to, and what behavior is expected? | W1 27951105 v1; Transcript > 13:27, 14:02 | Support Lead; Engineer |
| UQ-013 | Who can approve which BRD/PRD/acceptance versions, and where is that approval recorded? | W1 27951105 v1; Transcript > 19:34, 27:24; W4 27852801 v1; Definition of Ready; Document Lifecycle | Product Manager; not attributed (context page) |
| UQ-014 | Which approved Jira story supplies the implementation authority and delivery evidence? | W1 27951105 v1; Transcript > 27:24; W4 27852801 v1; Jira Work Hierarchy; Definition of Ready | Product Manager; not attributed (context page) |
| UQ-015 | What completes the truncated baseline logging success statement? | W2 27688962 v1; Success Measures, final bullet | not attributed (context page) |
| UQ-016 | Which desktop/mobile widths are supported for usability review? | W2 27688962 v1; Success Measures | not attributed (context page) |
| UQ-017 | How will candidate measurement evidence be used without prohibited customer/session data or new customer analytics? | W1 27951105 v1; Transcript > 18:41, 26:42; W2 27688962 v1; Current Scope | Engineer; Support Lead; not attributed (context page) |

* **Non-gating source gaps carried in the PRD:**
  * UQ-015, the incomplete "without storing" bullet in W2, closed only by an intact authoritative snapshot.
  * UQ-016, the supported desktop/mobile widths.
* **Stakeholder questions this plan adds or removes: none.** The quantity-zero meaning is the research's own B5 question (UQ-012), carried inside B5 and not a seventh package. The privacy and feasibility question UQ-017 is not suppressed and must be visible; it bears on B2 (inferred) and on B5/B6 per P04-T05.
* **STOP effect:** boundary B stays Not-Ready, and P04 stays blocked. Nothing in this plan authorizes the next step.

## Goals

* Deliver an UNAPPROVED working PRD and an independent document review that preserve the source, with every research item traceable.
* Present the complete human decision set (B1–B6) without answering any of it.
* Leave a conditional, traceable outline for a later gated fix, without authorizing it.

## Scope and Non-Goals

### In Scope

* P01 PRD draft, P02 document review, and P03 STOP checkpoint. P04 as an outline only.
* Research-only reading of the artifact, C1, and roster/instruction files. No repository behavior was inspected or is cited by this plan.

### Non-Goals

* Code changes, tests, builds, or any `rpi-implement` work.
* Approving or publishing a BRD/PRD or acceptance criteria. Creating Jira issues or Confluence pages. Commits, pushes, deploys.
* Selecting a business outcome, disclosure policy, threshold, copy, owner, review period, or zero behavior.
* Architecture decisions or ADRs.
* Squad-state writes, council dispatch now, nested workers, and any new integration or source query.
* Editing the protected research artifact.

## Functional Requirements

* FR-001: The PRD exists at `.copilot-tracking/plans/2026-10-17-inventory-aware-cart-prd.md` and states it is UNAPPROVED, AI-assisted, and non-authoritative, with boundary A/B status per C1.
* FR-002: The PRD keeps the documented baseline (W2) distinct from the proposed inventory extension (W1) and labels the extension "proposed".
* FR-003: The PRD cites all 157 research items by their exact IDs, each with page ID/version and locator matching the research row. The registers are G-001–G-009, N-001–N-012, D-001–D-012, C-001–C-038, M-001–M-010, DEP-001–DEP-015, R-001–R-012, A-001–A-008, UQ-001–UQ-017, V-001–V-024.
* FR-004: The PRD presents B1–B6 verbatim and UQ-001–UQ-017 verbatim, with the quantity-zero question inside B5, no extra package, and no question dropped.
* FR-005: The PRD keeps source states distinct (recorded agreement, suggestion, baseline context, assumption, derived inference, unresolved) and never presents a recorded agreement or verification statement as an approved requirement or acceptance criterion.
* FR-006: The PRD contains no value, threshold, copy, owner, approver, deadline, Jira key, default, or zero behavior absent from the research. Unknowns read "not decided" with their B-gate or UQ ID.
* FR-007: The PRD selects no alternative and makes no recommendation on gated choices. Any analyst note is labeled non-requirement.
* FR-008: The review exists at `.copilot-tracking/reviews/2026-10-17-inventory-aware-cart-document-review.md`. It records per-check results, findings with evidence, residual risks, and a non-approval verdict.
* FR-009: The STOP checkpoint presents the six packages and adds no question.
* FR-010: Any future-fix task is conditional, blocked by named gates, traceable to source IDs, and labels recommendation versus source agreement.

## Non-Functional Requirements

* NFR-001: Citation completeness.
  * Objective threshold or evaluation condition: the PRD's distinct ID set equals the research's 157-ID set, with 0 missing, 0 extra, and 0 locator or state-label mismatches.
* NFR-002: No unsourced requirement.
  * Objective threshold or evaluation condition: 0 normative statements in the PRD or STOP message without a research ID.
* NFR-003: No invented quantity.
  * Objective threshold or evaluation condition: 0 numbers, durations, percentages, thresholds, or copy strings that lack a research source.
* NFR-004: Data handling.
  * Objective threshold or evaluation condition: role-only attribution, paraphrase-first, and 0 raw quotations, personal identifiers, credentials, or customer data.
* NFR-005: Tracking conventions.
  * Objective threshold or evaluation condition: each Markdown artifact starts with `<!-- markdownlint-disable-file -->`, uses no `#file:` directives or line-number references, and sits at its binding path.
* NFR-006: Containment.
  * Objective threshold or evaluation condition: writes are limited to this plan, the PRD, and the review, with 0 writes to `src/`, the research artifact, squad state, trackers, or git history.

## Validation Plan

These are planned checks for the later roles. None has been run, and no result is claimed here.

| Check | Method (read-only) | Requirement |
|-------|--------------------|-------------|
| Every-item citation | Extract the distinct IDs from the PRD with a regular expression such as `\b(?:UQ|DEP|G|N|D|C|M|R|A|V)-\d{3}\b` and compare against the research registers. Then compare each cited row's page ID/version, section/timestamp, and state label with the research row. | NFR-001, FR-003 |
| Unsourced requirement | Enumerate every sentence phrased as a rule, must, shall, expectation, or acceptance statement in the PRD and require a mapped ID. Any sentence without one is a finding. | NFR-002, FR-005–FR-007 |
| Invented quantity or copy | Extract numeric, percent, duration, and quoted-copy strings from the PRD and require a research source. Verify that B1–B6 carry no default. | NFR-003, FR-006 |
| Zero handling | Confirm "zero" appears only as the open B5/UQ-012/V-009/R-010 question and no expected behavior is stated. | FR-004, FR-006 |
| User-question completeness | Compare B1–B6 text with the research gate register and UQ-001–UQ-017 with the research list. Verify there is no seventh package and no added question. | FR-004, FR-009 |
| Recommendation versus requirement | Confirm that alternatives remain "Unresolved" and that P04 and any analyst notes carry the recommendation/requirement labels. | FR-007, FR-010 |
| Privacy/data handling | Scan for quotations, personal identifiers, credentials, and customer data. | NFR-004 |
| Containment | Review the working-tree status for changes outside the three named artifacts. | NFR-006 |

## Risks and Open Questions

| Priority | Type | Risk, question, or planning item | Affected work | Impact | Smallest action or evidence needed | Owner |
|----------|------|----------------------------------|---------------|--------|------------------------------------|-------|
| H | risk | PRD Builder defaults (guided Q&A, `prd-sessions` state files) could violate the non-interactive, single-path scope | P01 | Out-of-scope files or questions to the human | State the scope in the dispatch prompt and have the reviewer check for strays (NFR-006) | planner / coordinator |
| H | risk | A PRD can read as approved if states are not preserved ("recorded agreement" vs "approved") | P01-T02, P01-T03 | Meeting notes read as approved requirements (R-012) | Keep research state labels and the status banner; reviewer check P02-T02 | `PRD Builder` / `Squad Reviewer` |
| M | risk | Zero meaning gets assumed in the PRD or by adopting any current code behavior as a requirement | P01-T03, P04-T02, P04-T03 | A wrong acceptance rule (R-010) | State zero only as the open B5 question; the research did not inspect code, so no code behavior is cited or adopted before B5 | `PRD Builder` / `Squad Reviewer` |
| M | open question | The UQ-to-B crosswalk is planner-derived; UQ-017 to B2 is inferred | P01-T04, P02-T03, P03-T01 | A mis-mapped question, or the UQ-017 privacy question lost at the STOP | Show UQ-001–UQ-017 verbatim at the STOP; the reviewer verifies the crosswalk and labels it a navigation aid | `Squad Reviewer` |
| M | risk | Revision 2 has not had a revision-bound independent closure | Plan-wide | Applied corrections unverified; plan not finalized | Coordinator dispatches the roster `tester` for revision-bound closure (required) | coordinator |
| L | open question | A formal PRD-quality review requested by intake is deferred | P02 | A reader may assume it occurred | P02 records it as deferred and non-blocking | user / coordinator |
| M | risk | Cached pages are not freshness-checked. The architecture and test claims are meeting- or page-stated, not repository-verified | P01, P04-T02 | A stale baseline | Keep "no freshness claim". Verify during P04-T02 research. | downstream |
| L | open question | The tests directory contains only build outputs at the glob level, so test-project status is unresolved | P04-T02, P04-T06 | Test planning rests on an unverified claim | Read-only code research (not in this run) | downstream |
| L | risk | The classification of the transcript-derived artifacts is not confirmed | P01-T01 | Over- or under-handling | Keep "handle as potentially confidential" | user |

## Dependencies

* Research artifact [.copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md](../research/2026-10-17/inventory-aware-cart-analysis.md): sole input to the PRD; protected and read-only.
* Intake verdict C1 in [.copilot-tracking/squad/decisions.md](../squad/decisions.md): permits boundary A only.
* Roster [.copilot-tracking/squad/team.md](../squad/team.md): analyst and tester roles are present. `architect` and `product-owner` are also present for the future council.
* Human B1–B6 decisions: the only way to unblock P04.

## Sources

* [.copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md](../research/2026-10-17/inventory-aware-cart-analysis.md): all 157 items, B1–B6, UQ-001–UQ-017, alternatives table, readiness table. Registers by number: 9 G, 12 N, 12 D, 38 C, 10 M, 15 DEP, 12 R, 8 A, 17 UQ, 24 V.
* [.copilot-tracking/squad/decisions.md](../squad/decisions.md): `Intake Readiness Verdict 2026-10-17T20:57:34Z inventory-aware-cart` (C1; supersedes the earlier 20:45:21Z entry for the cached-source boundary).
* [.copilot-tracking/squad/team.md](../squad/team.md) and [.copilot-tracking/squad/routing.md](../squad/routing.md): roles, roots, and the council row.
* [.github/instructions/squad-council.instructions.md](../../.github/instructions/squad-council.instructions.md): council triggers, membership, and waiver rules.
* [.github/instructions/copilot-tracking.instructions.md](../../.github/instructions/copilot-tracking.instructions.md) and [.github/instructions/copilot-tracking-location.instructions.md](../../.github/instructions/copilot-tracking-location.instructions.md): tracking conventions and root.
* [src/Services/CartService.cs](../../src/Services/CartService.cs), [src/Models/Product.cs](../../src/Models/Product.cs), [src/ZavaStorefront.csproj](../../src/ZavaStorefront.csproj): entry points for future P04-T02 read-only research only. Not read for this plan, not PRD evidence, and no behavior of them is asserted.
* [.copilot-tracking/reviews/2026-10-17-inventory-aware-cart-plan-critique.md](../reviews/2026-10-17-inventory-aware-cart-plan-critique.md): independent critique of revision 1 (PC-001–PC-006).
* Raw snapshots W1–W4 (host-session `files` directory): consulted by name only, for exact-excerpt needs. They were not re-read for this plan.
* Caller dispatch for this run (scope, paths, prohibitions).

## Critique Disposition

* Critique candidate identity: `inventory-aware-cart-init-2026-10-17-001`. Critiqued: plan revision 1. Current: plan revision 2 (PC-001–PC-006 applied).
* Independent critique artifact (binding path): `.copilot-tracking/reviews/2026-10-17-inventory-aware-cart-plan-critique.md`. Initial attempt `inventory-aware-cart-init-2026-10-17-001-critique-initial-01`, standard depth, verdict **Revise**, documentation disposition **Ready-With-Conditions**.
* Revision 1 assessed hash (covered by that critique): `a7755da72352d4b14b397cbb01ef2d602ac0af7494dfaf2360f29449c523dd9f`.
* Canonical projection evidence, revision 1: helper `Get-PlanAssessmentHash.ps1` (installed hve-core `rpi-plan` skill), `projection_version` `rpi-plan-assessment-v1`; excluded sections Critique Disposition, Artifact Self-Check, Planning Readiness and Next Step, Follow-Up Items, Handoff; projection not persisted (recomputable).
* Revision 2 assessed hash (computed by the same helper after all body edits, identical on two runs): `a830c7958f21aede3ebf6e82c1d41cbca1d29cd5ee5ad24fb2deb13d7b683797` (projection 65,125 characters, `rpi-plan-assessment-v1`). This hash is **not** covered by any independent assessment yet.
* Delivered covering assessment: revision 1 only. Revision 2 has none.
* Critique depth and provenance: `standard`, default (no explicit request for deep), per the critique artifact.
* Critique execution: **completed for revision 1**. The earlier "not run" and "optional" wording is superseded.
* Initial attempt consumed: yes (revision 1). Recovery attempt consumed: no.
* Infrastructure retry reservations: zero.
* Legacy reservation reconciliation: not applicable.
* Invocation outcome and assessment availability: completed; complete full assessment for revision 1.
* Attempt provenance: critique artifact Metadata; no nested subagent used by this correction.
* Revision-bound closure: **COMPLETED (Pass)** for revision 2; see the Finalization Record below, which supersedes the earlier "REQUIRED and PENDING" and "not finalized" wording.
* **Finalization Record (excluded bookkeeping; supersedes stale wording):** Closure artifact `.copilot-tracking/reviews/2026-10-17-inventory-aware-cart-plan-closure.md`, attempt `inventory-aware-cart-init-2026-10-17-001-closure-rev2-01`, projection `rpi-plan-assessment-v1`, covered hash `a830c7958f21aede3ebf6e82c1d41cbca1d29cd5ee5ad24fb2deb13d7b683797`. Result: Pass; PC-001–PC-006 closed, 0 open; readiness Ready, UNAPPROVED, DOCS DRAFT ONLY. Plan status: **finalized for documentation scope P01–P03 only**; code phase P04 remains **blocked** by B1–B6. This excluded finalization record supersedes every stale "not finalized", "closure pending", "REQUIRED and PENDING", and "Applied in rev 2; closure pending" statement in the earlier hash-included historical summary (Executive Summary and Next Steps, left unchanged because they are assessed body) and in the table below and Artifact Self-Check. Recomputed canonical hash after this bookkeeping edit is unchanged (`a830c795…683797`); no further critique is needed for excluded bookkeeping.
* Recovery eligibility and consent: not applicable.
* Infrastructure classification and reconciliation: not applicable.
* Exhaustion and human assessment: not applicable.
* Documentation conditions from the critique: (1) P01 dispatches with research artifact and C1 as sole inputs, PC-004 already applied; (2) PC-002 applied; (3) PC-003, PC-001, PC-005 applied before the P03 STOP; (4) PC-006 applied. Only P01–P03 are eligible; P04 stays stopped at B1–B6.

| Critique run and finding | Disposition | Action owner | Exact resolving evidence | Decision route | Plan response or residual risk |
|--------------------------|-------------|--------------|--------------------------|----------------|--------------------------------|
| Critique rev 1 / PC-001 | Applied in rev 2; closure pending | Squad Coordinator; tester closure | This section cites the binding critique path, verdict Revise, and hash a7755da7…23dd9f; D3, Risks, Planning Readiness, Self-Check, Next Steps no longer call the gate optional or not run; `reviews/plans/` path removed | Direct planner correction | Closure of rev 2 is required, not optional |
| Critique rev 1 / PC-002 | Applied in rev 2; closure pending | Squad Coordinator; tester closure | Executive Summary "What You May Not Know", D4, P03 goals, P03-T01, STOP section: "zero clarification" means quantity-zero clarification inside B5; no stakeholder question added or removed; B5 keeps "What does “zero” mean…?" | Direct planner correction | None remaining beyond closure |
| Critique rev 1 / PC-003 | Applied in rev 2; closure pending | Squad Coordinator; tester closure | STOP Checkpoint carries a verbatim UQ-001–UQ-017 table (UQ-017 included); UQ-017→B2 labeled "inferred; also bears on B5/B6 per P04-T05" in the package table, summary, D5, P01-T04; P03-T01 requires UQ text | Direct planner correction | None remaining beyond closure |
| Critique rev 1 / PC-004 | Applied in rev 2; closure pending | Squad Coordinator; tester closure | No CartService/UpdateQuantity/AddToCart/Product.cs/net6.0 behavior asserted anywhere; P04-T02 lists unverified research items "not evidence and not requirements"; Sources and Scope say no repository behavior was inspected | Direct planner correction (code verification routed to future `rpi-research`) | None remaining beyond closure |
| Critique rev 1 / PC-005 | Applied in rev 2; closure pending | Squad Coordinator; user optional | P02 Details states P02 is a tester content review, the intake formal PRD-quality review request is deferred, non-blocking, not performed | Direct planner correction; not a blocking user decision | Deferred formal PRD Quality Reviewer pass remains a prerequisite for any claim that it occurred |
| Critique rev 1 / PC-006 | Applied in rev 2; closure pending | Squad Coordinator; tester closure | B5 label equals the research label “Explicit approval of requirements and acceptance criteria” with zero kept in the question column; Council Assessment says “no code or threat-surface change in this docs-only run; logging-privacy lens deferred to P04-T01” | Direct planner correction | None remaining beyond closure |

## Artifact Self-Check

* [x] Executive Summary, What You May Not Know, and the Phase Checklist come first.
* [x] Confirmed direction, grouped decisions, readiness, goals, scope, requirements, risks, and dependencies are present and consistent with the Phase Checklist.
* [x] Decision participation is recorded. Business decisions B1–B6 are user-retained and unresolved. Planning decisions D1–D5 carry rationale.
* [x] FR-001–FR-010 and NFR-001–NFR-006 are cited by at least one task's Requirements.
* [x] Every phase has Goals, Dependencies, and a diagram. Every task has Goals, Requirements, Details, References, and Dependencies.
* [x] Open decisions, risks, and questions live in their tables with affected work named.
* [x] Quantity-zero clarification, verbatim UQ-001–UQ-017 at the STOP, B5 research label, and no unsourced code observation were verified by search after the PC-001–PC-006 edits.
* [x] Code, commands, and symbols use backticks. Existing files are links relative to the plan file. Not-yet-created paths are in backticks.
* [x] Before reflects the evidence-backed pre-change state. After shows the intended result. Added nodes use dashed borders and the `Added:` label. Initialization lines use the prescribed font strings.
* [ ] Dual-theme rendering was not verified (no preview used); the labels and dashed borders carry the meaning without color.
* [x] No code, state, tracker, commit, or research-artifact change was made.
* [x] Independent critique of revision 1 exists and its six findings (PC-001–PC-006) are applied in revision 2.
* [ ] Revision-bound independent closure of revision 2 is **pending and required**; the plan is not finalized.

## Next Steps

Revision 2 applies the six critique findings, but the plan is **not finalized** until a revision-bound independent closure of revision 2 is complete. The Squad Coordinator dispatches the roster `tester` for that closure; no nested subagent was used here and the closure is not optional. After closure, the Coordinator may dispatch `PRD Builder` for P01 with the research artifact and intake verdict C1 as sole inputs, then `Squad Reviewer` for P02, then STOP at P03 showing B1–B6 and all of UQ-001–UQ-017 verbatim. Only documentation phases P01–P03 are eligible. Do not run `/rpi-implement`. Boundary B and the future fix stay stopped at B1–B6 until the authorized humans answer them.
