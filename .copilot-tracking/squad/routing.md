---
description: "Squad routing: request patterns mapped to roles, autonomy tiers, and parallel eligibility"
---

# Squad Routing

| Pattern / Keyword                                                                                                  | Role(s)                                                         | Autonomy Tier | Parallel-Eligible |
|--------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------|---------------|-------------------|
| research, investigate, explore, find out                                                                           | researcher                                                      | auto          | yes               |
| plan, break down, sequence, design plan                                                                            | lead                                                            | confirm       | no                |
| implement, build, code, fix                                                                                        | developer                                                       | confirm       | no                |
| review, validate, check quality                                                                                    | tester                                                          | auto          | yes               |
| validate, cross-check, pre-implementation review, council, design review, go/no-go, implement-and-cost, implement-and-risk | architect, security, cost-manager, product-owner, rai (optional) | confirm       | yes               |
| validate requirements, requirements readiness, requirements complete, requirements clear, intake check, are the requirements ready | intake-validator | auto | yes |
| requirements, BRD, PRD, user story, acceptance criteria | analyst | confirm | yes |
| roadmap, backlog, epic, sprint, prioritize, story, PRD to work items, work item hierarchy | product-owner | confirm | no |
| GitLab merge request, GitLab pipeline, GitLab issue, open an MR | product-owner | escalate | no |
| architecture, system design, components | architect | auto | yes |
