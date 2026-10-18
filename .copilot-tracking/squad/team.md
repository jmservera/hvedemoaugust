---
description: "Squad roster: roles and the deployed HVE Core agents that fill them"
---

# Squad Roster

## Members

| Role       | Member Name | Agent Name (Primary) | Alternate Agents                      | Selection Cue                                                                                          | Invocation         | Model Tier | Deliverable Root                   |
|------------|-------------|----------------------|---------------------------------------|--------------------------------------------------------------------------------------------------------|--------------------|------------|------------------------------------|
| researcher | Alpha       | Squad Researcher     | Codebase Profiler, Meeting Analyst    | technology-profile scan → Codebase Profiler; meeting-transcript mining → Meeting Analyst                | runSubagent / task | default    | .copilot-tracking/research/<date>/ |
| lead       | Beta        | Squad Lead           | —                                     | —                                                                                                      | runSubagent / task | default    | .copilot-tracking/plans/           |
| developer  | Gamma       | Squad Implementor    | —                                     | —                                                                                                      | runSubagent / task | default    | .copilot-tracking/changes/         |
| tester     | Delta       | Squad Reviewer       | Code Review Functional, Code Review Standards | correctness/edge-case diff → Code Review Functional; project-conventions diff → Code Review Standards | runSubagent / task | fast       | .copilot-tracking/reviews/         |
| scribe     |             | Squad Scribe         | —                                     | —                                                                                                      | runSubagent / task | fast       | (squad state)                      |
| intake-validator |         | PRD Quality Reviewer         | BRD Quality Reviewer                  | PRD input → PRD Quality Reviewer; BRD input → BRD Quality Reviewer; other requirement inputs → Primary | runSubagent / task | fast       | (Scribe-persisted intake verdict)  |
| analyst    |             | PRD Builder                  | BRD Builder, Meeting Analyst          | Business requirements → BRD Builder; transcript→requirements → Meeting Analyst; otherwise product requirements → PRD Builder | runSubagent / task | default    | .copilot-tracking/plans/           |
| architect  |             | System Architecture Reviewer | ADR Creator                           | Decision record → ADR Creator; otherwise architecture review → Primary                                  | runSubagent / task | default    | docs/architecture/                 |
| product-owner |          | Functional Planner           | Issue Triage Agent                    | single-issue triage → Issue Triage Agent; otherwise → Primary                                           | runSubagent / task | default    | .copilot-tracking/plans/           |
