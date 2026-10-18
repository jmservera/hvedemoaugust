<!-- markdownlint-disable-file -->
# Document Review: Inventory-Aware Cart UNAPPROVED Working PRD

| Field | Value |
|---|---|
| Run / topic | init-2026-10-17-001 / inventory-aware-cart |
| Reviewer role | Squad Reviewer (tester, P02), documentation content review only |
| Review date | 2026-10-18 |
| Mode | rpi-review documentation check, standalone caller path (plan P02-T04); no code diff, no branch diff |
| Depth | standard; explicit selected documents only |
| Reviewed artifacts | PRD `.copilot-tracking/plans/2026-10-17-inventory-aware-cart-prd.md` (UNAPPROVED); plan `.copilot-tracking/plans/2026-10-17-inventory-aware-cart-plan.md` (finalized P01–P03 scope, read for STOP and P02 criteria); research `.copilot-tracking/research/2026-10-17/inventory-aware-cart-analysis.md`; closure `.copilot-tracking/reviews/2026-10-17-inventory-aware-cart-plan-closure.md`; metadata `.copilot-tracking/plans/2026-10-17-inventory-aware-cart-prd.state.json`; intake C1 `.copilot-tracking/squad/decisions.md`; four cached snapshots (transcript, product overview, architecture, governance) |
| Review execution | Complete (independent content review, once) |
| Formal scores | None. No PRD Quality Reviewer score is claimed; formal scoring remains deferred as the plan states. |

## Verdict

**Pass-With-Findings** for the UNAPPROVED working document. The PRD is a faithful, source-grounded draft: all 157 research IDs are carried and none is promoted into an approved requirement. One provenance sentence needs a material repair before the human STOP (RV-001). This verdict is a content check only. It is not product, business, engineering, or Jira approval, and it does not change implementation status: **Code implementation remains NOT STARTED / Not Ready (boundary B).**

## Findings

| ID | Severity | Location | Issue (expected vs observed) | Source locator | Smallest fix / route |
|---|---|---|---|---|---|
| RV-001 | Medium | PRD line 149, Observability (CNFR-005) | Expected: process claims match the evidence record. Observed: the PRD says `telemetry-foundations` guidance "was consulted" and that "general guidance grants no hashing exception to source prohibitions." No cited source supports either statement. Research records telemetry skills as **Skipped**, and PRD `phaseSkillsLoaded` lists only prd-author phases. | Research "Extensions and participation" row for telemetry/privacy skills (line ~478): "Skipped". | Delete the "was consulted" clause and the hashing sentence, or replace with: "telemetry-foundations was not used as evidence; no hashing exception is inferred from any source." Route: PRD Builder edit, then reviewer re-checks this one check. |
| RV-002 | Low | PRD line 401, MVP and Release Framing | Expected: scope groupings trace to a research ID. Observed: "Candidate cut line: validation-only CFR-001–CFR-006/CNFR-001–CNFR-006" has no research ID and no research MVP statement. It also places the stock-state presentation proposal (CFR-001, blocked by B3/B4/B5) inside "validation-only." | Research has no MVP cut; exclusions are D-001, C-006, C-007, C-010–C-013. | Relabel as "PRD grouping for discussion; not sourced; not a scope decision," or cite D-001–D-009 and C-010–C-013 for each element. Route: PRD Builder. Optional. |
| RV-003 | Low (plan, not PRD) | Plan Executive Summary line 17 and Next Steps (hash-covered body) | Expected: plan status is consistent. Observed: body still says "not finalized" and "closure pending." The excluded Finalization Record and closure Pass say finalized. The PRD describes this accurately, so the PRD is not at fault. Closure CL-001 already flagged this. | Closure CL-001; plan Finalization Record. | Planning parent decides: a status-only delta with one targeted closure, or accept the excluded-record reconciliation. Route: Squad Coordinator. |
| RV-004 | Info | Squad `decisions.md`, C1 Consistency row | The intake verdict calls the truncated "without storing" bullet a "meeting cache" bullet. It is in Product Overview (27688962 v1). The PRD and research attribute it correctly. Do not edit the append-only log. | Snapshot W2, Success Measures. | Optional Squad Scribe correction note. No PRD change. |

No Critical or High findings. No finding depends on a formal PRD Quality Reviewer result.

## Checks performed and limits

1. **Coverage of research IDs.** Research has 157 unique table rows. PRD trace index has 157 unique rows. Missing: 0. Extra: 0. Duplicates: 0. Family counts match: G 9, N 12, D 12, C 38, M 10, DEP 15, R 12, A 8, UQ 17, V 24. Every ID appears in the PRD body. No invented ID such as C-039 exists.
2. **Locators.** For all 157 IDs, the set of transcript timestamps (MM:SS) matches the research row exactly. Snapshot tags W1–W4 match exactly. I spot-checked about 45 rows' speaker roles against the transcript speaker headings and found no mismatch. Not every row was re-verified.
3. **Verbatim questions.** UQ-001–UQ-017 question text is string-equal across the research register, the PRD verbatim register, and the plan STOP table (17 of 17 in each). B1–B6 question text is string-equal across research, PRD, and plan (6 of 6). All 17 questions and all 6 packages are visible for the human STOP.
4. **Quotations.** The PRD has 24 quoted strings. 21 appear verbatim in the research or the four snapshots. The other 3 are the PRD's own status labels ("not finalized", "Draft-testable", "policy-blocked"), not source claims. The 17 UQ items are research wording and are labelled as analysis, not transcript speech.
5. **Source fidelity (sampled).** Checked the 8-product catalog, 30-minute idle timeout, `wwwroot/images/products` path, page IDs and v1 versions, and webUrls against the snapshots. Confirmed the "without storing" ending is not reconstructed. The governance example key RETAIL-123 is not used as an authority, and the PRD contains no Jira key.
6. **Hashes.** Research SHA-256 `9a7906b9…e29bb3` matches the PRD evidence-input hash. Plan canonical projection, recomputed read-only with the rpi-plan `Get-PlanAssessmentHash.ps1` helper, gives `a830c795…683797`, matching the closure and PRD. No file was written.
7. **Metadata.** `prd.state.json` exists at the relocated path. Its `referencesProcessed` hash matches. Its authority fields show UNAPPROVED and implementation NOT STARTED. No escaped or duplicate metadata file exists under `.copilot-tracking`.
8. **Boundary behavior.** Zero: CAC-009 is TBD and CFR-002/CFR-003 leave zero undefined. Exact-count tension: the PRD states early suggestions do not override later disclosure, including error copy. Accessibility: no ARIA, focus, or WCAG claim is made. Logging: allowlist and prohibitions are preserved, with no telemetry schema invented. Validation-only semantics and exclusions match C-006, C-007, C-010–C-013.
9. **Invented values.** No metric, baseline, target, percentage, duration, low-stock threshold, disclosure choice, copy, owner, approver, or Jira ID was found. Owners and approvers are marked TBD.
10. **Links.** Six internal relative and anchor links resolve. The `#source-trace-index` and `#source-manifest-and-cached-read-boundary` anchors match headings.

**Not verified:** live Confluence or Jira freshness; code behavior; product tests; that the PRD is byte-identical to any earlier draft (no baseline copy exists). I did not treat markdownlint results as evidence. The PRD carries `markdownlint-disable-file`, so lint is weak. I did not read the rpi-review reference or canonical log template; the user-specified path and plan P02-T04 standalone path were used. No RPI review state, parent decision record, or squad state was written.

## Human implementation blockers (not resolved by this review)

* **B1** Primary business outcome, and the approved outcome statement for the BRD.
* **B2** Measurement package: evidence source, baseline, target, accountable owner, review period.
* **B3** Disclosure policy and approved copy, including whether it governs rejection messages.
* **B4** Low-stock policy, thresholds, and boundary classification.
* **B5** Approver, approval record, and approved document versions, including the quantity-zero behavior (UQ-012).
* **B6** Approved Jira delivery story with source links, criteria, exclusions, dependencies, validation commands, and review expectations.
* Also awaited: product approval of requirements, a source BRD (none supplied), the deferred formal PRD-quality review, and an intact source for UQ-015 (non-gating). UQ-016 (supported widths) is also non-gating.

## Material repairs and routing

1. **RV-001 (required before STOP):** one-sentence provenance edit at PRD line 149. Re-check only that CNFR-005 provenance item.
2. **RV-002 (recommended):** relabel the MVP cut line at PRD line 401.
3. **RV-003 (planning parent):** resolve the stale plan status wording.
4. **RV-004 (optional):** Squad Scribe correction note. Log is append-only.

A later re-review is needed only for the changed check. No cycle ceiling is set. The verdict does not block the UNAPPROVED working document from existing alongside Not Ready implementation.

## Model and call count

* Model: unknown.
* Internal calls: about 50 tool calls in total: file views, read-only searches, Python cross-checks, and one read-only helper run. No subagents. No writes except this review file.
* No code changes, product tests, Git, Jira, or Confluence writes.
