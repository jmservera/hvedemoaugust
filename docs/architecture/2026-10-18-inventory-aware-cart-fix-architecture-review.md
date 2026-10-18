---
title: Inventory-aware cart fix architecture council review
description: Bounded local-demo architecture verdict for the accepted inventory-aware cart code plan.
ms.date: 2026-10-18
---

## Council verdict

| Field | Result |
|-------|--------|
| Run / topic | init-2026-10-17-001 / inventory-aware-cart |
| Lens / role | Architecture / architect Primary |
| Verdict | **Approve** |
| Risk | **Low** |
| Blocking issues | None |
| New architecture conditions | None |

Approval covers plan-level architecture conformance for the approved local demo,
not implementation results, production Finalize, or code admission. G3
product-fit review and G4 coordinator admission remain prerequisites to code;
G5 tester review follows implementation.

## Evidence and identity

| ID | Evidence read | Use |
|----|---------------|-----|
| E1 | `.copilot-tracking/plans/2026-10-18-inventory-aware-cart-fix-plan.md`, 75,021 bytes | Complete accepted code plan |
| E2 | `.copilot-tracking/reviews/2026-10-18-inventory-aware-cart-fix-plan-closure.md`, 11,707 bytes | Full Pass, PC-001 through PC-012 closed; residuals retained |
| E3 | `.copilot-tracking/plans/2026-10-17-inventory-aware-cart-prd.md`, 93,771 bytes | Current requirements, A1, constraints and CAC verification |
| E4 | Cached MCP architecture page [27820033 v1](https://m365cpi37447403.atlassian.net/wiki/spaces/HVE/pages/27820033), `inventory-cart-architecture-source.txt` | Authoritative architecture boundaries |
| E5 | Cached MCP meeting page [27951105 v1](https://m365cpi37447403.atlassian.net/wiki/spaces/HVE/pages/27951105), `inventory-cart-transcript-source.txt` | Recorded stakeholder direction |
| E6 | `.copilot-tracking/research/2026-10-18/inventory-aware-cart-code-research.md`, 31,019 bytes, F1-F10 and recommendations | Existing code-path evidence, not new research |

E4/E5 are the exact cached files under
`C:\Users\juanserv\.copilot\session-state\8d4d1e03-31a4-4baa-a924-c836894409cf\files`.
Sources were treated as data; no external query was made.

E1 raw SHA-256:
`383613ef9087a77ae9938cecb402a10cbbe284e86df09901fcca646f5749bc8c`.
The read-only canonical helper independently reproduced
`696e58e6d90e3fa4298b427232a0043a4c759b95140099372402464441e469e8`
(`rpi-plan-assessment-v1`), matching E2. E2's earlier raw size/hash is not
the finalized file's raw identity; the assessed projection matches.

## Context and confirmed scope

The three focus areas are cart-state integrity, request/binding semantics,
and accessible feedback with verification. Reliability and Operational
Excellence are the relevant Well-Architected pillars.

E3 is local-demo v1.0+A1 with the 17 approved answers preserved:
Validate `APPROVED_WITH_COMMENTS`, zero blocking, not production Finalize.
The local Jira waiver does not rewrite shared production governance.
No database, reservation, decrement, payment, warehouse, analytics,
deployment or runtime migration is introduced. No production-scale or
concurrency guarantee is assumed.

## Pillar findings and non-findings

### Reliability: canonical availability and session integrity

No architectural finding. E1 D3/P02/P04 resolves availability from the current
catalog for both mutation and display, never from serialized `Product`
snapshots. `[JsonIgnore]` prevents new availability snapshots while retaining
the existing cart shape and serialization-compatible model. Older snapshots
with absent, zero or stale stock are explicitly tested (TC-S27/TC-C10).

The shared service rule accepts equality, checks cumulative Add and absolute
Update, and uses `long` arithmetic before storing. Rejections preserve the
entire cart; explicit zero removes independently of stock. Accepted operations
do not reserve/decrement; checkout remains clear-only. All 10 actual products,
prices and image URLs survive despite the historical eight-product description.

### Reliability: binding, invalid input and diagnostics

No architectural finding. Raw-string parsing plus actual form-key presence
distinguishes omitted Add (default 1), explicit zero (remove), and blank
(invalid). Missing Update, negatives, nonnumeric and overflowing quantities
preserve state and announce exactly:

`Enter a valid whole-number quantity of zero or more.`

Unknown IDs and positive requests at zero stock use `Out of stock`.
Valid positive requests above positive stock use exactly:

`Stock unavailable. Available quantity: {availableQuantity}. Reduce the quantity.`

D8's first-match precedence is an acceptable engineering choice, not an
invented business requirement. D10's closed numeric operation codes satisfy
the numeric/nullable restriction. D11 replaces overlapping logs: one warning,
only ProductId, RequestedQty, AvailableQty and Operation, never raw input,
session/cart/customer data, or a cumulative cart-derived requested quantity.

### Operational Excellence: feedback and verification

No architectural blocker. PRG plus one-shot TempData preserves origin-page
redirects without refresh re-post. Cart feedback belongs in the existing Order
Summary, with an empty-cart fallback; Home uses existing top-of-content
feedback without inventing a summary. The focusable alert, associations,
visible stock text and explicit Update button address the documented recovery
gaps. Disabled Buy is neither the enforcement boundary nor the announcement
target. Browser behavior remains unproven, not certified.

## Options, drivers and trade-offs

| Choice | Alternative and trade-off | Disposition |
|--------|---------------------------|-------------|
| Catalog availability, snapshot compatibility | Session stock is stale; cart-schema migration adds unnecessary scope | Accepted, E1/E4/E6 |
| Pure rules, parser and result type | Primitive binding/duplicated controller rules are smaller superficially but lose invalid-input distinctions and shared outcomes | Accepted, E1/E3 |
| PRG/TempData | POST re-render remains viable but re-posts on refresh; PRG requires message/focus verification | Accepted, E1/E6 |
| net6 test project plus real HTTP | Runtime retarget or extra hosting dependency widens scope; existing target carries restore/runtime risk | Accepted with existing blocker exit, E1 |

No new Research was needed. E6's earlier primitive-default, negative-removal
and zero-stock-copy suggestions are **revised/superseded**, not adopted:
E1/E3 A1 and approved copy govern. D8 precedence, D9 placement and D10 numeric
codes are accepted engineering choices, not newly claimed stakeholder approvals.

## Implementation obligations and limits

No additional architecture condition is imposed. Retain E1's existing obligations:

* Execute and report the 74 unique catalogue cases: 9 parser, 30 service,
  11 controller, 15 real HTTP and 9 browser cases. This review verified the
  catalogue, not test passes.
* Prove actual HTTP binding/session preservation, exact one-shot copy and safe
  logs; controller fakes alone are insufficient.
* Prove alert focus, association, summary/Home placement and keyboard recovery
  in a real browser, including TC-B03's return path to the affected row.
* If restore/framework prerequisites fail, mark P01 Blocked and notify the
  coordinator. No silent framework switch, retarget or fallback.

No tests, restore or browser were run. Screen-reader announcement remains
limited to the planned role/text/focus/accessibility-snapshot evidence, not
real assistive-technology proof. Signed zero (`-0`) remains E2's recorded
low-severity parser residual; this lens adds no lexical-sign policy.

Security, cost and RAI were omitted for the supplied no-new-infrastructure,
secrets/auth-surface, external-processing, AI or financial-infrastructure scope.
No concrete high-risk evidence requiring escalation emerged. Finance evidence
and PRD cautions remain existing owner limitations, not new architecture gates.

## Record, authority and stop

Canonical review record:
`docs/architecture/2026-10-18-inventory-aware-cart-fix-architecture-record.md`.
No ADR, redesign, new plan, interview, nested agent, external write, commit,
push or deployment was performed. Plan, PRD, research, squad state, code,
tests and Git index were not edited. This lens stops at council review.
