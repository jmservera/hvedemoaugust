---
description: "Squad consumption ledger: members, models, estimated tokens, cost, and AI credits"
---

# Squad Consumption Ledger (Run: init-2026-10-17-001)

## Attribution

| Role | Member | Agent | Model | Model Source | Priced As | Tier |
| ---- | ------ | ----- | ----- | ------------ | --------- | ---- |
| researcher | Alpha | Meeting Analyst | unknown | unresolved | Claude Sonnet 4.6 | default |
| researcher | Alpha | Squad Researcher | claude-sonnet-5.5 | agent-pinned | Claude Sonnet 5.5 | default |
| lead | Beta | Squad Lead | claude-sonnet-5.5 | dispatch-reported, agent-pinned | Claude Sonnet 5.5 | default |
| developer | Gamma | Squad Implementor | claude-sonnet-5.5 | agent-pinned | Claude Sonnet 5.5 | default |
| tester | Delta | Squad Reviewer | Claude Haiku 5.5, claude-haiku-5.5 | agent-pinned, dispatch-reported | Claude Haiku 5.5 | fast |
| intake-validator |  | PRD Quality Reviewer | unknown, gpt-6.1-sol | unresolved, dispatch-reported | Claude Haiku 4.5, GPT-6.1 Sol | fast |
| analyst |  | PRD Builder | gpt-6.1-sol, unknown | dispatch-reported, unresolved | GPT-6.1 Sol, Claude Sonnet 4.6 | default |
| architect |  | System Architecture Reviewer | unknown | unresolved | Claude Sonnet 4.6 | default |
| product-owner |  | Functional Planner | unknown | unresolved | Claude Sonnet 4.6 | default |
| orchestration |  | Coordinator+Scribe | Claude Haiku 5.5, unknown, claude-haiku-5.5 | agent-pinned, unresolved, dispatch-reported | Claude Haiku 5.5, Claude Haiku 4.5 | mixed |

## Usage & Cost

| Role | Turns | In Tokens | Cached | Cache Wr | Out Tokens | Est. Cost (USD) | Est. Credits | Basis |
| ---- | ----- | --------- | ------ | -------- | ---------- | ---------------- | ------------ | ----- |
| researcher | 57 | 1,732,800 | 6,931,200 | 264,000 | 71,250 | 9.3365 | 933.65 | tier-default |
| researcher | 36 | 792,000 | 3,168,000 | 180,000 | 45,000 | 2.8008 | 280.08 | estimated |
| lead | 173 | 4,143,200 | 16,572,800 | 1,084,000 | 346,000 | 16.1137 | 1,611.37 | estimated |
| developer | 220 | 17,478,000 | 69,912,000 | 1,482,000 | 440,000 | 50.0522 | 5,005.22 | estimated |
| tester | 324 | 8,977,000 | 35,908,000 | 1,702,000 | 486,000 | 8.5626 | 856.26 | estimated |
| intake-validator | 90 | 1,512,000 | 6,048,000 | 472,000 | 135,000 | 4.6800 | 468.00 | tier-default |
| analyst | 260 | 9,410,200 | 37,640,800 | 836,000 | 495,900 | 53.2975 | 5,329.75 | tier-default |
| architect | 81 | 2,494,800 | 9,979,200 | 416,000 | 121,500 | 13.8607 | 1,386.07 | tier-default |
| product-owner | 93 | 4,352,400 | 17,409,600 | 418,000 | 139,500 | 21.9401 | 2,194.01 | tier-default |
| orchestration | 1,328 | 25,623,386 | 102,493,544 | 6,028,617 | 1,130,400 | 24.6468 | 2,464.68 | tier-default |
| **Total** | **2,662** | **76,515,786** | **306,063,144** | **12,882,617** | **3,410,550** | **205.2908** | **20,529.08** | |

> **Billed by the host for this session so far: 3,881.53 AI units, about 38.82 USD.** The estimate above is 5.29x that figure. Estimates come from dispatch-size guesses; the billed figure is the host's own count, coordinator included. See *Observed Usage (host-reported)* below.

### Derivation

```text
autopilot-run-init-2026-10-17-001.md — 0 block(s) — identities: (none)
Functional Planner.md — 1 block(s) — identities: 5ed2777e
Meeting Analyst.md — 1 block(s) — identities: 92e9b045
PRD Builder.md — 7 block(s) — identities: 50a47fba,10276b17,9d12e03c,4da8587c,584e9564,4fd09d3b,ab3f09d3
PRD Quality Reviewer.md — 5 block(s) — identities: d8776ea0,c29f9504,78746f15,ebab6c81,bd77ce9b
Squad Implementor.md — 3 block(s) — identities: 2160350d,e4311e33,ddc64d78
Squad Lead.md — 7 block(s) — identities: 4d2a8692,dcb50c17,c4d72d0c,fbf9691a,ad0cfc23,9aff32f9,c25a767e
Squad Researcher.md — 1 block(s) — identities: 8889d583
Squad Reviewer.md — 8 block(s) — identities: e17a9db4,76008ccb,88580403,4d336ec6,24fdf7a4,a926bfe6,0b33b405,ab164c9c
Squad Scribe.md — 42 block(s) — identities: e4276478,f1957d4f,d1ceb563,eb4c2b9e,e7bde12c,bd884b92,3a7677b2,1babbc79,e71f4be2,83847340,341be6a3,dd02881e,e825328e,89ccb48b,61d9479f,ccdd4d4a,700fb922,9b7e68f0,aa155ae3,93b73288,54396497,9c0baa5e,1673d855,328b0425,82b0fc89,72b51ec4,f8e5646a,77aad031,172c98da,b827f13d,6b89b7d2,d3f8875c,b58bfd4f,8e18f03e,a12b9c2f,d0209084,20f819c5,4bd92228,a5325ffc,cdbf5e26,49682b9c,918dac12
System Architecture Reviewer.md — 2 block(s) — identities: 733a7f50,990a7927
researcher     turns 57           1732800 × 3.00 + 6931200 × 0.30 + 264000 × 3.75 + 71250 × 15.00 = 9336510 / 1e6 = 9.3365
researcher     turns 36           792000 × 2.00 + 3168000 × 0.10 + 180000 × 2.50 + 45000 × 10.00 = 2800800 / 1e6 = 2.8008
lead           turns 34+46+15+15+35+13+15=173 4143200 × 2.00 + 16572800 × 0.10 + 1084000 × 2.50 + 346000 × 10.00 = 16113680 / 1e6 = 16.1137
developer      turns 150+35+35=220 17478000 × 2.00 + 69912000 × 0.10 + 1482000 × 2.50 + 440000 × 10.00 = 50052200 / 1e6 = 50.0522
tester         turns 51+36+50+18+56+31+40+42=324 8977000 × 0.50 + 35908000 × 0.05 + 1702000 × 0.62 + 486000 × 2.50 = 8562650 / 1e6 = 8.5626
NOTE: intake-validator blocks priced at more than one rate; the line below uses the first block's rate over the aggregated tokens, but the role's Cost column above sums each block at its own rate.
intake-validator turns 18+18+18+18+18=90 1512000 × 1.00 + 6048000 × 0.10 + 472000 × 1.25 + 135000 × 5.00 = 3381800 / 1e6 = 4.6800
NOTE: analyst blocks priced at more than one rate; the line below uses the first block's rate over the aggregated tokens, but the role's Cost column above sums each block at its own rate.
analyst        turns 60+3+35+39+47+35+41=260 9410200 × 4.00 + 37640800 × 0.20 + 836000 × 5.00 + 495900 × 15.00 = 56787460 / 1e6 = 53.2975
architect      turns 63+18=81     2494800 × 3.00 + 9979200 × 0.30 + 416000 × 3.75 + 121500 × 15.00 = 13860660 / 1e6 = 13.8607
product-owner  turns 93           4352400 × 3.00 + 17409600 × 0.30 + 418000 × 3.75 + 139500 × 15.00 = 21940080 / 1e6 = 21.9401
NOTE: orchestration blocks priced at more than one rate; the line below uses the first block's rate over the aggregated tokens, but the role's Cost column above sums each block at its own rate.
orchestration  turns 6+24+10+22+38+20+20+40+20+45+30+23+27+30+40+34+30+34+34+24+52+38+32+20+40+40+40+35+47+22+24+40+40+38+40+45+34+40+30+22+30+28=1328 25623386 × 0.50 + 102493544 × 0.05 + 6028617 × 0.62 + 1130400 × 2.50 = 24530255.825 / 1e6 = 24.6468
                                                                                        total = 205.2908
```

> Basis: estimated. No per-dispatch token telemetry exists; the runtime exposes only the per-user aggregate `ai_credits_used` via the Copilot usage-metrics REST API. Token rates and the dispatch-size estimator come from `consumption-rates.md` (observed 2026-10-15). Calibration factor 1.00 (0 reconciled run(s)). 1 AI credit = $0.01 USD.

## Unit Economics (value)

| Measure | Value | Label |
| ------- | ----- | ----- |
| Deliverables produced | 27 | derived |
| Accepted by an independent review | 0 | derived |
| Rejected | 0 | derived |
| Unreviewed | 27 | derived |
| Dispatches (excluding Scribe) | 35 | derived |
| Rework dispatches | 0 | derived |
| First-pass yield | n/a | derived |
| Est. credits per accepted deliverable | n/a | estimated |
| Rework share of est. credits | 0.0% | estimated |
| Orchestration share of est. credits | 12.0% | estimated |
| Host-billed credits per accepted deliverable | n/a | measured |

Value is counted only for deliverables an independent review-class verdict accepted; a produced but unaccepted deliverable adds cost and no value. A review that names deliverables covers only those it names. Rework counts a repeat of the same agent and deliverable path only: a fix by a different agent, to a different file, or with no deliverable path is not counted, so the rework figures are a floor. Compare runs only for like work, using medians of several runs. Source: FinOps Foundation unit-economics KPIs (cost per unit of value, error and retry waste).

## Observed Usage (host-reported)

Source: Copilot session `8d4d1e03-31a4-4baa-a924-c836894409cf` (`events.jsonl`, 2026-10-17T20:27:36.747Z to 2026-10-18T16:11:49.414Z). The host reports one real token total per dispatch, not split into input, cached, and output, so these figures sit beside the estimates above rather than replacing them. Blended USD prices each total with model-catalog.md's blended mix (0.20 input, 0.80 cached, 0.08 cache write, 0.02 output) over this root's rate rows.

| Agent | Dispatches | Observed Model | Ledger Model | Match | Observed Tokens | Estimated Tokens | Minutes | Blended USD |
| ----- | ---------- | -------------- | ------------ | ----- | --------------- | ---------------- | ------- | ----------- |
| Functional Planner | 1 | gpt-6.1-sol | unknown | n/a | 588,913 | 22,319,500 | 3.2 | 0.5182 |
| Meeting Analyst | 1 | gpt-6.1-sol | unknown | n/a | 1,564,111 | 8,999,250 | 33.5 | 1.3764 |
| PRD Builder | 7 | gpt-6.1-sol | gpt-6.1-sol, unknown | yes | 5,515,145 | 48,382,900 | 69.2 | 4.8533 |
| PRD Quality Reviewer | 5 | gpt-6.1-sol | unknown, gpt-6.1-sol | yes | 542,009 | 8,167,000 | 10.1 | 0.4770 |
| Squad Implementor | 2 | claude-sonnet-5.5 | claude-sonnet-5.5 | yes | 19,772,974 | 89,312,000 | 25.1 | 17.4002 |
| Squad Lead | 7 | claude-sonnet-5.5 | claude-sonnet-5.5 | yes | 12,795,773 | 22,146,000 | 45.7 | 11.2603 |
| Squad Researcher | 1 | claude-sonnet-5.5 | claude-sonnet-5.5 | yes | 1,326,930 | 4,185,000 | 5.5 | 1.1677 |
| Squad Reviewer | 7 | claude-haiku-5.5 | Claude Haiku 5.5, claude-haiku-5.5 | yes | 17,549,039 | 47,073,000 | 46.0 | 0.8424 |
| System Architecture Reviewer | 2 | gpt-6.1-sol | unknown | n/a | 1,340,253 | 13,011,500 | 13.0 | 1.1794 |
| Squad Scribe | 40 | claude-haiku-5.5 | Claude Haiku 5.5, unknown, claude-haiku-5.5 | yes | 106,279,381 | 135,275,947 | 224.1 | 5.1014 |
| **Subagents total** | **73** | | | | **167,274,528** | **398,872,097** | | **44.1763** |

Session total billed by the host, coordinator included: **3,881.53 AI units** (about 38.82 USD at 0.01 USD per unit, the same convention as 1 AI credit). The coordinator's own turns appear only in this figure, never in the table.

### Without HVE Squad

| Scenario | Tokens | Model | Blended USD |
| -------- | ------ | ----- | ----------- |
| With HVE Squad: routed roles plus Scribe, as observed | 167,274,528 | as routed | 44.1763 |
| Without HVE Squad: the same role work on one model, no Scribe | 60,995,147 | gpt-6.1-sol | 53.6757 |

HVE Squad is **9.4994 USD (17.7%) lower** than running the same role work on `gpt-6.1-sol`. Both rows use the same blended mix; neither includes coordinator turns, and the single-model row adds no context growth or rework a long single chat would carry, so it is a floor for that scenario rather than a forecast.

## Cost Comparison (illustrative)

This run consumed a host-billed **$31.72 (~3171.80 AI credits)** across the whole session, coordinator included, per *Observed Usage (host-reported)*. The subagent-only blended total is **$42.5410** across 65 subagent dispatches (every row of the Observed Usage table). Reproducing the same role work on one model, `gpt-6.1-sol`, with no Scribe and a single long chat, is **$51.0084** per *Without HVE Squad*, so HVE Squad is about **16.6%** (8.4674 USD) lower on the same subagent-only, same-blended-mix basis; coordinator turns are excluded from both sides. The routed figures are a floor: the Meeting Analyst, PRD Builder, PRD Quality Reviewer, and Squad Scribe rows carry `unknown` or unreconciled model attribution, which this section does not present as a confident model name. The estimated ledger total in *Usage & Cost* is **$198.5757 (~19,857.57 AI credits)**, uncalibrated (0 reconciled runs, calibration factor 1.00); it sits beside the observed figures and the two are not interchangeable.

> Estimates only. Token rates change. See `consumption-rates.md` for current rates, the dispatch-size estimator, and the calibration methodology. Token counts and iteration counts are illustrative, not guarantees.
