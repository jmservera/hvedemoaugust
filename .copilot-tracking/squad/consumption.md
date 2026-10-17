---
description: "Squad consumption ledger: members, models, estimated tokens, cost, and AI credits"
---

# Squad Consumption Ledger (Run: init-2026-10-17-001)

## Attribution

| Role | Member | Agent | Model | Model Source | Priced As | Tier |
| ---- | ------ | ----- | ----- | ------------ | --------- | ---- |
| orchestration |  | Coordinator+Scribe | Claude Haiku 5.5 | agent-pinned | Claude Haiku 5.5 | mixed |

## Usage & Cost

| Role | Turns | In Tokens | Cached | Cache Wr | Out Tokens | Est. Cost (USD) | Est. Credits | Basis |
| ---- | ----- | --------- | ------ | -------- | ---------- | ---------------- | ------------ | ----- |
| orchestration | 6 | 42,000 | 168,000 | 45,000 | 6,000 | 0.0725 | 7.25 | estimated |
| **Total** | **6** | **42,000** | **168,000** | **45,000** | **6,000** | **0.0725** | **7.25** | |

> **Billed by the host for this session so far: 99.28 AI units, about 0.99 USD.** The estimate above is 0.07x that figure. Estimates come from dispatch-size guesses; the billed figure is the host's own count, coordinator included. See *Observed Usage (host-reported)* below.

### Derivation

```text
Squad Scribe.md — 1 block(s) — identities: e4276478
orchestration  turns 6            42000 × 0.50 + 168000 × 0.05 + 45000 × 0.62 + 6000 × 2.50 = 72525 / 1e6 = 0.0725
                                                                                        total = 0.0725
```

> Basis: estimated. No per-dispatch token telemetry exists; the runtime exposes only the per-user aggregate `ai_credits_used` via the Copilot usage-metrics REST API. Token rates and the dispatch-size estimator come from `consumption-rates.md` (observed 2026-10-15). Calibration factor 1.00 (0 reconciled run(s)). 1 AI credit = $0.01 USD.

## Unit Economics (value)

| Measure | Value | Label |
| ------- | ----- | ----- |
| Deliverables produced | 0 | derived |
| Accepted by an independent review | 0 | derived |
| Rejected | 0 | derived |
| Unreviewed | 0 | derived |
| Dispatches (excluding Scribe) | 0 | derived |
| Rework dispatches | 0 | derived |
| First-pass yield | n/a | derived |
| Est. credits per accepted deliverable | n/a | estimated |
| Rework share of est. credits | 0.0% | estimated |
| Orchestration share of est. credits | 100.0% | estimated |
| Host-billed credits per accepted deliverable | n/a | measured |

Value is counted only for deliverables an independent review-class verdict accepted; a produced but unaccepted deliverable adds cost and no value. A review that names deliverables covers only those it names. Rework counts a repeat of the same agent and deliverable path only: a fix by a different agent, to a different file, or with no deliverable path is not counted, so the rework figures are a floor. Compare runs only for like work, using medians of several runs. Source: FinOps Foundation unit-economics KPIs (cost per unit of value, error and retry waste).

## Observed Usage (host-reported)

Source: Copilot session `9119d6a6-31a5-4c41-9dfa-90c83c26a79b` (`events.jsonl`, 2026-10-17T16:35:30.276Z to 2026-10-17T16:37:57.520Z). The host reports one real token total per dispatch, not split into input, cached, and output, so these figures sit beside the estimates above rather than replacing them. Blended USD prices each total with model-catalog.md's blended mix (0.20 input, 0.80 cached, 0.08 cache write, 0.02 output) over this root's rate rows.

| Agent | Dispatches | Observed Model | Ledger Model | Match | Observed Tokens | Estimated Tokens | Minutes | Blended USD |
| ----- | ---------- | -------------- | ------------ | ----- | --------------- | ---------------- | ------- | ----------- |
| **Subagents total** | **0** | | | | **0** | **0** | | **0.0000** |

Session total billed by the host, coordinator included: **99.28 AI units** (about 0.99 USD at 0.01 USD per unit, the same convention as 1 AI credit). The coordinator's own turns appear only in this figure, never in the table.

## Cost Comparison (illustrative)

This run consumed an estimated **$0.0145 (~1.45 AI credits)** across 1 specialized agent (`Squad Scribe`), routing state-seeding work to a lightweight model (`Claude Haiku 5.5`). Reproducing the same outcome by manually prompting `Claude Sonnet 4.6` across roughly 2 iterate-and-test turns, each priced through the same dispatch-size estimator, is estimated at **$0.3885 (~38.85 AI credits)** — a saving of about **96.3%**.

> Estimates only. Token rates change. See `consumption-rates.md` for current rates, the dispatch-size estimator, and the calibration methodology. Token counts and iteration counts are illustrative, not guarantees.
