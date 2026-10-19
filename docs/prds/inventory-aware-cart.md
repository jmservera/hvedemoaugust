---
title: "Inventory-Aware Cart"
description: "Internal local-demo PRD with owner-approved policies derived from Confluence evidence and recorded user decisions."
prd_id: "PRD-INVENTORY-AWARE-CART"
status: "approved-for-local-demo"
version: "0.2.0"
classification: "internal"
authority: "local-demo approval only; not an authoritative Confluence document"
owners: ["Juan Manuel Servera"]
reviewers: ["Juan Manuel Servera"]
local_demo_approval:
  approver: "Juan Manuel Servera"
  date: "2026-10-19"
  scope: "local repository demo requirements and acceptance criteria"
  evidence: "User responses and explicit PRD approval in this conversation"
demo_date: "2026-10-19"
created_date: "2026-10-19"
last_updated: "2026-10-19"
product_goal_ids: ["GOAL-001", "GOAL-002", "GOAL-003"]
product_goal_smart_status: "deferred"
fr_to_ac_coverage_threshold_pct: 80.0
fr_to_goal_coverage_threshold_pct: 100.0
diagram_format: "none"
source_brd_id: null
lineage:
  supersedes: []
  superseded_by: []
requirement_id_prefixes:
  fr: "FR"
  ac: "AC"
  nfr: "NFR"
  con: "CON"
  goal: "GOAL"
---

## Executive Summary

Shoppers can request more units than the catalog has available and discover the mismatch after deciding to check out. The requested improvement is to communicate stock state before adding an item and apply consistent inventory limits when adding products or updating cart quantities. The customer problem is misleading quantity expectations, not a requirement to prevent real-world sellouts. Source: [SM], Transcript 00:00 (Product Manager) and 01:12 (Support Lead).

The proposed iteration uses the existing static catalog and session-backed cart. It validates quantities without reserving stock or decrementing availability after checkout. Checkout remains a confirmation that clears the cart, not a payment or order transaction. Source: [SM], Transcript 02:35 (Store Operations Lead), 03:48 (Engineer), and 04:25 (Product Manager).

The owner selected reducing inventory-rejected cart quantity updates from a stated 30% baseline to a 15% target. The denominator is all cart quantity-update attempts, not adds. The baseline is user-supplied and unverified; measurement is postponed until after the demo. The local demo is planned for 2026-10-19. Source: [LD], owner responses on 2026-10-19, not the original meeting.

For the demo, zero availability means out of stock; availability 1 through 5 inclusive means low stock; availability above 5 means in stock. Listings show exact counts only for low stock, while inventory-rejection messages always disclose the exact available quantity. Zero-quantity adds are rejected without changing the cart; updating a cart line to zero removes it. Source: [LD].

> [!IMPORTANT]
> Juan Manuel Servera approved this PRD for the local repository demo. That approval does not publish or approve a Confluence document, mutate Jira, satisfy the shared-delivery workflow, or demonstrate that implementation or tests are complete. Formal PRD Validate/Finalize approval is not claimed.

## Product Context

### Source register

All pages were read through the Atlassian MCP integration on 2026-10-19. References such as SM 09:28 identify the exact transcript heading and speaker role. Context pages are documents, not speeches; no speaker role is attributed to them.

| Key | Confluence page                                  | Page ID  | Version | Evidence used                                                                                                  |
| --- | ------------------------------------------------ | -------- | ------- | -------------------------------------------------------------------------------------------------------------- |
| SM  | [Stakeholder Meeting - Inventory-Aware Cart][SM] | 27951105 | 1       | Meeting Details; Transcript; Meeting Outcome                                                                   |
| PO  | [Product Overview][PO]                           | 27688962 | 1       | Users and Needs; Product Baseline; Customer Journey; Current Scope; Success Measures; Product Assumptions      |
| AA  | [Application and Platform Architecture][AA]      | 27820033 | 1       | System Context; Storefront Application; Request Flows; State and Data; Observability; Architecture Constraints |
| DG  | [Delivery and Governance][DG]                    | 27852801 | 1       | Knowledge and Work Ownership; Jira Work Hierarchy; Definition of Ready; Definition of Done; Document Lifecycle |

The meeting took place on 2026-07-28 and explicitly identifies its transcript as synthetic, with speaker roles replacing names and no customer or employee personal data. Source: [SM], Meeting Details.

### Local demo decisions

LD records Juan Manuel Servera's answers and explicit approval in this conversation on 2026-10-19. These are owner-approved local-demo decisions, not statements made by the meeting's speaker roles or changes published to Confluence.

* Reduce the inventory-rejected cart quantity-update rate from a user-supplied 30% baseline to a 15% target; postpone measurement until after the demo.
* Use one global low-stock threshold of 5, with counts on low-stock listings only and exact availability in every inventory-rejection message.
* Reject zero-quantity adds without state changes; remove a cart line when its quantity is updated to zero.
* Preserve all existing cart quantities on rejected adds as well as rejected updates.
* For invalid products, omit available quantity from logs and record an explicit `product-not-found` reason.
* Retain all ten functional requirements and nine NFRs as MUST priorities. Focused automated tests remain engineering work; manual final validation is by the owner in Chrome and Edge on desktop.
* Use the plain-language copy proposed in this PRD under the owner's express authorization to propose suitable copy. No external standard endorsement or separate design-team approval is implied.
* Keep the increment a local demo for 2026-10-19. Juan Manuel Servera is the owner, reviewer, and local approver; no Confluence/Jira approval workflow is requested for this demo.

### Existing product and scope tension

The documented baseline is an ASP.NET Core MVC storefront with eight static products, responsive Razor views, a session cart, quantity updates, item removal, checkout confirmation, and structured logging. Sessions have a 30-minute idle timeout. There is no database, payment integration, persistent cart, or order store. These are documentation statements, not findings from a fresh source-code audit. Sources: [PO], Product Baseline and Product Assumptions; [AA], State and Data.

[PO], Current Scope excludes inventory availability, reservation, and warehouse synchronization. The meeting proposes validation-only availability as a new increment. The owner confirmed that extension for the local demo only; the Confluence baseline remains unchanged. Reservation and warehouse integration remain excluded. Sources: [SM], Transcript 02:35 (Store Operations Lead), 04:25 and 16:08 (Product Manager); [LD], OQ-008 resolution.

### Evidence and priority conventions

* Meeting statements are cited to their Confluence page, section, and speaker role; subsequent owner decisions are cited separately to LD.
* Requirements and acceptance criteria have local-demo owner approval. No meeting, Confluence, Jira, or shared-delivery approval is inferred from that approval.
* MoSCoW MUST priorities were retained by the owner for this demo because omitting these behaviors defeats the validation-only iteration or removes existing behavior. No delivery-capacity assessment is implied.
* Excluded capabilities are WONT for this increment, not permanent product prohibitions.

## Users and Personas

| User or stakeholder role              | Need or review responsibility                                                                                   | Source                                                                                               |
| ------------------------------------- | --------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------- |
| Shopper                               | Understand availability before adding; change quantities without misleading promises or losing valid cart state | [SM], Transcript 01:12 and 06:22 (Support Lead), 10:42 (Designer)                                    |
| Keyboard or assistive-technology user | Perceive stock and error text, hear rejection feedback, and recover without unexpected focus loss               | [SM], Transcript 05:10, 10:42, and 17:20 (Designer)                                                  |
| Support Lead                          | Verify boundary cases, diagnose quantity confusion, and provide actionable recovery guidance                    | [SM], Transcript 13:27, 20:51, and 26:42 (Support Lead)                                              |
| Store Operations Lead                 | Review all stock states and agree the disclosure and low-stock policies                                         | [SM], Transcript 15:19 and 25:31 (Store Operations Lead); 24:18 (Designer raises disclosure concern) |
| Product Manager and Designer          | Resolve product policy and approve customer-facing copy; product approves acceptance criteria                   | [SM], Transcript 12:18 and 19:34 (Product Manager)                                                   |
| Engineer                              | Apply one inventory rule to both mutation paths and provide focused validation and diagnostic evidence          | [SM], Transcript 07:31, 14:02, and 18:41 (Engineer)                                                  |

The original pages do not assign named signatories. For this local demo, Juan Manuel Servera explicitly identifies himself as owner and approver and will perform final manual validation. Source: [LD].

## Design Decisions

| ID     | Decision or authoring treatment                                                                                                        | Status and decision maker                                                               | Related items and source                                                                                                                         |
| ------ | -------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------ |
| DD-001 | Validate against catalog availability without reservation or post-checkout decrement                                                   | Recorded scope decision, Product Manager, 2026-07-28                                    | FR-004, FR-005, FR-010; [SM], Transcript 04:25                                                                                                   |
| DD-002 | Initially preserve unresolved policies rather than invent counts, thresholds, copy, or business targets                                | Historical draft-author treatment; later demo dispositions are DD-004 through DD-006    | OQ-001 through OQ-005; [SM], Transcript 12:18 and 23:10 (Product Manager), 24:18 (Designer), 25:31 (Store Operations Lead), 26:42 (Support Lead) |
| DD-003 | Generate a local working PRD directly from transcript evidence, without claiming an approved BRD handoff                               | User-requested local drafting path, 2026-10-19; not approval of requirements            | All sections; [DG], Document Lifecycle still distinguishes notes from approved requirements                                                      |
| DD-004 | Use threshold 5, low-stock-only listing counts, exact rejection counts, path-specific zero behavior, and preservation on rejected adds | Local-demo policy approved by Juan Manuel Servera, 2026-10-19                           | FR-001 through FR-009; [LD]                                                                                                                      |
| DD-005 | Accept local-demo PRD approval without asserting shared Confluence/Jira approval                                                       | Local approval by Juan Manuel Servera, 2026-10-19; does not supersede shared governance | CON-006, CON-007; [LD]                                                                                                                           |
| DD-006 | Select the quantity-update rejection metric and defer measurement until after the demo                                                 | Owner decision, Juan Manuel Servera, 2026-10-19                                         | GOAL-002, OQ-001, OQ-009; [LD]                                                                                                                   |

## Product Goals

| ID       | Product goal for the local demo                                                                                                                   | Priority | Verification or measurement                                                                       | Source                                                                                                                                                             |
| -------- | ------------------------------------------------------------------------------------------------------------------------------------------------- | -------- | ------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| GOAL-001 | Let shoppers make an informed stock-related choice before adding a product                                                                        | MUST     | Show stock state before adding; show exact counts only for availability 1 through 5               | [SM], Transcript 05:10 (Designer), 12:18 (Product Manager); [LD]                                                                                                   |
| GOAL-002 | Make quantity limits consistent across mutation paths and reduce inventory-rejected quantity updates from the stated 30% baseline to a 15% target | MUST     | Verify boundary and recovery scenarios now; measure the business target after the demo            | [SM], Transcript 07:31 (Engineer), 09:28 (Store Operations Lead), 20:51 (Support Lead); [PO], Customer Journey; [LD], unverified baseline and deferred measurement |
| GOAL-003 | Make inventory rejection understandable, accessible, and diagnosable without exposing sensitive information                                       | MUST     | Review customer feedback, keyboard and assistive-technology behavior, and structured warning logs | [SM], Transcript 08:15 (Product Manager), 17:20 (Designer), 18:41 (Engineer), 19:34 (Product Manager)                                                              |

The primary business measure supports GOAL-002: reduce the inventory-rejected quantity-update rate from the stated 30% baseline to 15%. Source: [LD]. SMART status remains deferred because measurement timing, baseline evidence, and a complete collection plan are postponed until after the demo. The demo date is not an outcome-measurement deadline.

## Functional Requirements

All rows have owner-approved MUST priority for the local demo. Acceptance-criterion links are in the traceability matrix. The stock, disclosure, zero-input, and rejected-add policies below incorporate LD without attributing them to the original meeting.

| ID     | Actor and trigger                                                  | Required outcome                                                                                                                                                                                                     | Evidence and qualification                                                                                                                             |
| ------ | ------------------------------------------------------------------ | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------ |
| FR-001 | Operations reviewer opens the catalog                              | Each product shall have an available quantity; review data shall include availability 0, availability from 1 through 5, and availability above 5                                                                     | [SM], Transcript 02:35 and 15:19 (Store Operations Lead); [LD], global threshold 5                                                                     |
| FR-002 | Shopper views a product before adding it                           | The storefront shall display out of stock for 0, low stock with the exact count for 1 through 5, and an in-stock label without the exact count above 5                                                               | [SM], Transcript 05:10 (Designer), 12:18 (Product Manager); [LD], listing disclosure and threshold                                                     |
| FR-003 | Shopper attempts to add a product with no available stock          | The storefront shall prevent the add operation                                                                                                                                                                       | [SM], Transcript 05:10 (Designer), supported by Meeting Outcome. No-stock means zero availability; it does not define a request quantity of zero       |
| FR-004 | Shopper submits an add operation for an existing product           | For a positive add quantity, the rule shall accept a resulting total at or below availability and reject one above it, counting existing cart units; a zero-quantity add shall be rejected without changing the cart | [SM], Transcript 07:31 (Engineer), 09:28 (Store Operations Lead); [LD], zero-add decision. Other malformed-input semantics are unchanged               |
| FR-005 | Shopper submits a cart-line quantity update                        | For a positive quantity, the same rule shall accept a requested line total at or below availability and reject one above it; updating to zero shall remove the line                                                  | [SM], Transcript 07:31 (Engineer), 09:28 (Store Operations Lead); [LD], zero-update decision. Update quantity is the resulting total, not an increment |
| FR-006 | An inventory validation rejects an add or update                   | The cart shall retain every existing valid line quantity                                                                                                                                                             | [SM], Transcript 10:42 (Designer) covers updates; [LD] explicitly extends preservation to rejected adds                                                |
| FR-007 | Shopper encounters an inventory-limit rejection                    | The storefront shall show a specific, actionable message with the exact available quantity, including zero when out of stock, instead of a generic server error                                                      | [SM], Transcript 06:22 and 20:51 (Support Lead); [LD], exact counts on every inventory rejection regardless of listing policy                          |
| FR-008 | Shopper receives rejection feedback after an add or cart update    | The storefront shall keep the shopper on the page where the action originated                                                                                                                                        | [SM], Transcript 06:22 (Support Lead). HTTP navigation technique is not prescribed                                                                     |
| FR-009 | A cart mutation references a missing or invalid product identifier | The storefront shall show a neutral product-unavailable response without internal error details                                                                                                                      | [SM], Transcript 07:31 (Engineer), 08:15 (Product Manager)                                                                                             |
| FR-010 | Shopper completes checkout confirmation                            | Checkout shall continue to clear the session cart without reserving or decrementing catalog inventory                                                                                                                | [SM], Transcript 04:25 (Product Manager); [PO], Product Baseline and Customer Journey                                                                  |

## Non-Functional Requirements

The following categories follow the PRD template's quality-category grouping; they do not imply certification or compliance with an unstated standard. All identified NFRs retain MUST priority under the owner's local-demo approval.

### Performance and Capacity

No latency, throughput, concurrency, or capacity target is supplied. No numeric performance budget is introduced by this local-demo PRD.

### Reliability and Resilience

FR-006 and AC-005/AC-007 cover rejection-state preservation. No availability, recovery-time, or fault-tolerance target is supplied. Cross-session stock guarantees are excluded by the validation-only boundary, not silently promised.

### Security

NFR-001: Customer-visible invalid-product responses shall not expose technical exception details, stack traces, session data, or credentials. Verify through AC-010 and AC-014. Sources: [SM], Transcript 08:15 (Product Manager); [AA], Observability and Architecture Constraints.

### Privacy

NFR-002: Inventory and invalid-product diagnostic logs shall exclude cart contents, session payloads, session identifiers, customer information, credentials, and other sensitive values. Verify through AC-014. Sources: [SM], Transcript 08:15 (Product Manager), 18:41 (Engineer), approved at 19:34 (Product Manager); [AA], Observability.

### Scalability and Elasticity

No new scaling requirements or warehouse-wide availability guarantees are in scope. Sources: [SM], Transcript 04:25 and 16:08 (Product Manager).

### Maintainability and Operability

NFR-003: Inventory rules shall remain outside Razor views; controllers shall delegate state operations to services and enforce the same rule across add and update paths. Verify through code review and AC-004 through AC-007. Sources: [AA], Architecture Constraints; [SM], Transcript 07:31 (Engineer).

NFR-004: Delivery evidence shall include focused automated tests for both mutation paths, invalid identifiers, and the requested boundary cases, with build and tests passing from a clean checkout. Tests shall include the approved path-specific zero behavior. Final owner-led manual validation complements, rather than replaces, these automated tests. Verify through AC-004 through AC-007, AC-010, and AC-012. Sources: [SM], Transcript 13:27 (Support Lead), 14:02 (Engineer); [DG], Definition of Done; [LD].

### Observability

NFR-005: Inventory validation rejection shall produce structured warning logs identifying product, requested quantity, available quantity when known, and operation type. Verify through AC-014. Sources: [SM], Transcript 18:41 (Engineer), approved at 19:34 (Product Manager).

NFR-006: Invalid-product handling shall log permitted technical context for diagnosis, including the operation type and an explicit `product-not-found` reason, without exposing that context to the customer. Omit the available-quantity field when the product is missing or invalid; do not replace it with zero or another fabricated quantity. Verify through AC-010 and AC-014. Sources: [SM], Transcript 08:15 (Product Manager); [LD], logging decision.

### Usability and Accessibility

NFR-007: Stock state shall have understandable text and shall not rely on color alone. Verify through AC-002 and AC-013. Source: [SM], Transcript 05:10 (Designer).

NFR-008: Rejection feedback shall be programmatically associated with the affected interaction, announced to assistive technology, and recoverable by keyboard without unexpected loss of place. Cart-update feedback shall appear near the cart summary. Verify through AC-009 and AC-013. Sources: [SM], Transcript 10:42 and 17:20 (Designer); Meeting Outcome confirms accessibility expectations.

Demo validation is manual in Chrome and Edge on desktop, performed by Juan Manuel Servera at the end of development. Record actual browser versions, desktop viewport sizes, and any assistive technology used in the validation evidence. No formal WCAG level or certification is claimed. The text, association, announcement, and keyboard-recovery requirements remain in scope; any behavior not verified must remain explicitly pending. Source: [LD], OQ-007 resolution.

### Compatibility and Interoperability

NFR-009: The increment shall preserve the existing MVC, responsive Razor/Bootstrap, static-catalog, and session-cart behavior, including checkout confirmation and item removal. Verify through AC-011 and AC-015. Sources: [PO], Product Baseline, Customer Journey, and Success Measures; [AA], Architecture Constraints.

### Portability

No new hosting environment, deployment target, or runtime migration is requested. Sources: [SM], Transcript 03:48 (Engineer), 16:08 (Product Manager); [AA], Architecture Constraints.

## Constraints

These are source-imposed boundaries for the local-demo increment, not permission to change the shared product baseline.

| ID      | Constraint                                                                                                                                                          | Category and affected boundary              | Imposing source and acceptance impact                                                                                                                                       |
| ------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| CON-001 | Retain the static product catalog and session-backed cart; do not introduce persistent inventory or a database for this increment                                   | Technical; state and storage                | [SM], Transcript 02:35 (Store Operations Lead), 03:48 (Engineer), 16:08 (Product Manager); [AA], State and Data. Review data stays catalog-based                            |
| CON-002 | Do not reserve inventory on add or decrement it after checkout                                                                                                      | Operational; inventory semantics            | [SM], Transcript 04:25 (Product Manager). AC-011 must show unchanged availability                                                                                           |
| CON-003 | Preserve existing product prices and catalog images                                                                                                                 | Operational; catalog content                | [SM], Transcript 15:19 (Store Operations Lead), agreed at 16:08 (Product Manager). Verify AC-015                                                                            |
| CON-004 | Exclude catalog administration, warehouse synchronization, payment changes, persistent inventory, and order storage                                                 | Technical and operational; integrations     | [SM], Transcript 16:08 (Product Manager); [PO], Current Scope. These are WONT in this increment                                                                             |
| CON-005 | Preserve the existing no-account journey, removal behavior, 30-minute session timeout, and checkout confirmation                                                    | Technical; compatibility                    | [PO], Customer Journey and Product Assumptions; [AA], State and Data. Verify AC-011 and AC-015                                                                              |
| CON-006 | Obtain owner approval of demo requirements and acceptance criteria before local-demo implementation; retain the meeting's product-approval gate for shared delivery | Organizational; implementation readiness    | [SM], Transcript 19:34 and 27:24 (Product Manager); [LD], explicit local approval by Juan Manuel Servera, 2026-10-19                                                        |
| CON-007 | For shared or production delivery, link approvals and work in Confluence and Jira, and link validation and review evidence in GitHub                                | Organizational; governance and traceability | [DG], Definition of Ready, Definition of Done, and GitHub Traceability Convention; [LD], no shared approval workflow for this demo. No Confluence/Jira operation is claimed |

Additional baseline exclusions remain unchanged: customer accounts, persistent carts, tax, shipping, discounts, fulfillment, recommendations, and customer analytics. Source: [PO], Current Scope. Candidate business metrics do not authorize adding these capabilities.

## Acceptance Criteria

These scenarios incorporate owner-approved local-demo policies; they are not test results. Every scenario is Not Started until implementation and validation provide evidence. Sources are inherited from the listed FR/NFR/CON items and LD. Final manual review is by Juan Manuel Servera in desktop Chrome and Edge; required automated tests remain in scope.

| ID     | Given / When / Then                                                                                                                                                                                                                                                                                              | Covers                             | Status      |
| ------ | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------- | ----------- |
| AC-001 | Given the review catalog, when all products are inspected, then each has an available quantity and the data includes at least one product with 0, one with 1 through 5, and one above 5                                                                                                                          | FR-001                             | Not Started |
| AC-002 | Given products with availability 0, 1, 5, and 6, when their listing states are inspected, then 0 is out of stock, 1 and 5 show their exact low-stock counts, and 6 shows an in-stock label without its count; all states use text before the add action                                                          | FR-002; NFR-007                    | Not Started |
| AC-003 | Given a product with availability 0, when its add control is inspected or a positive add is submitted, then the interface prevents adding and the operation does not create or increase a cart line                                                                                                              | FR-003                             | Not Started |
| AC-004 | Given availability 3, when a shopper adds 3 units to an empty cart or adds 1 unit to an existing quantity of 2, then the resulting cart quantity is 3                                                                                                                                                            | FR-004; NFR-003, NFR-004           | Not Started |
| AC-005 | Given availability 3, when a shopper adds 4 units to an empty cart or adds 1 unit to an existing quantity of 3, then the operation is rejected and every existing cart quantity remains unchanged                                                                                                                | FR-004, FR-006; NFR-003, NFR-004   | Not Started |
| AC-006 | Given availability 3 and an existing cart line, when the shopper updates its quantity to 3, then the line quantity becomes 3                                                                                                                                                                                     | FR-005; NFR-003, NFR-004           | Not Started |
| AC-007 | Given availability 3 and an existing valid cart line, when the shopper updates its quantity to 4, then the update is rejected and the previous valid cart quantities remain unchanged                                                                                                                            | FR-005, FR-006; NFR-003, NFR-004   | Not Started |
| AC-008 | Given an inventory-limit rejection for a product with known availability, when feedback is shown, then it states the exact available quantity, including 0 for out-of-stock rejection, explains recovery using the approved copy, and never substitutes a generic server error                                   | FR-007                             | Not Started |
| AC-009 | Given an add or update action that is rejected, when feedback is rendered, then the shopper remains on the originating page; cart-update feedback appears near the cart summary                                                                                                                                  | FR-008; NFR-008                    | Not Started |
| AC-010 | Given a missing or invalid product identifier on either mutation path, when the operation is submitted, then the customer receives the neutral product-unavailable copy without internal details, and the diagnostic event omits available quantity and records a product-not-found reason                       | FR-009; NFR-001, NFR-004, NFR-006  | Not Started |
| AC-011 | Given a valid cart and recorded catalog availability, when checkout confirmation completes, then the cart is empty, the existing confirmation is shown, and catalog availability has not changed                                                                                                                 | FR-010; NFR-009; CON-002, CON-005  | Not Started |
| AC-012 | Given an existing product and valid cart, when an add request specifies 0, then it is rejected with unchanged cart quantities and explanatory feedback; when a cart-line update specifies 0, then that line is removed and other lines remain unchanged; both paths have automated tests                         | FR-004, FR-005; NFR-004            | Not Started |
| AC-013 | Given desktop Chrome and Edge with actual versions and review setup recorded, when the owner manually tests keyboard rejection and assistive-technology announcement behavior, then error text is associated, announced, and recoverable without unexpected focus loss; untested behaviors remain pending        | NFR-007, NFR-008                   | Not Started |
| AC-014 | Given inventory rejections and invalid identifiers on both paths, when logs and responses are inspected, then inventory rejections emit structured warnings with known diagnostic fields; invalid-product events omit available quantity and record product-not-found; prohibited sensitive data appears nowhere | NFR-001, NFR-002, NFR-005, NFR-006 | Not Started |
| AC-015 | Given the existing journey, when automated regression checks and final owner-led desktop Chrome/Edge review are performed, then product prices, images, removal, session configuration, and existing presentation behavior are preserved; mobile-specific validation is outside this demo's manual review        | NFR-009; CON-003, CON-005          | Not Started |

The zero-request scenario in AC-012 is distinct from zero available stock in AC-003. The original meeting requested zero tests without defining the outcome; LD now defines rejection for zero adds and line removal for zero updates.

### Customer-facing copy

The following conventional plain-language messages are authored under the owner's authorization to propose copy for this demo. They are not quotations from the meeting, certified industry-standard text, or separately approved Confluence copy. Substitute the catalog's actual quantity for `{availableQuantity}`. Source: [LD], copy authorization and disclosure decisions.

* In stock, availability above 5: "In stock"
* Low stock, availability 1 through 5: "{availableQuantity} left in stock"
* Zero availability: "Out of stock"
* Over-limit add or update, positive availability: "Only {availableQuantity} available. Your cart has not changed. Choose a total quantity of {availableQuantity} or fewer."
* Rejected add for zero availability: "0 available. This product is out of stock. Your cart has not changed. Choose another product."
* Zero-quantity add: "Choose a quantity of at least 1. Your cart has not changed."
* Missing or invalid product: "This product is unavailable. Return to the catalog and choose another product."
* Zero-quantity update: "Item removed from your cart."

These messages do not promise inventory reservation or fulfillment. For repeated adds, the stated limit is the total cart quantity, not the number of additional units allowed.

## Traceability Matrix

### FR-to-AC Coverage

Coverage records scenario links, not executable-test completeness or passing results. Owner-approved local-demo scenarios remain Not Started until tested.

| Source ID | Source title                 | Target IDs             | Coverage |
| --------- | ---------------------------- | ---------------------- | -------- |
| FR-001    | Catalog availability         | AC-001                 | 1        |
| FR-002    | Stock-state visibility       | AC-002                 | 1        |
| FR-003    | Out-of-stock add prevention  | AC-003                 | 1        |
| FR-004    | Add quantity rule            | AC-004, AC-005, AC-012 | 3        |
| FR-005    | Update quantity rule         | AC-006, AC-007, AC-012 | 3        |
| FR-006    | Rejection-state preservation | AC-005, AC-007         | 2        |
| FR-007    | Specific recovery message    | AC-008                 | 1        |
| FR-008    | Same-page feedback           | AC-009                 | 1        |
| FR-009    | Invalid-product response     | AC-010                 | 1        |
| FR-010    | Unchanged checkout semantics | AC-011                 | 1        |

Linkage coverage: 10 of 10 FRs, 100.0%, against the template's 80.0% threshold. This does not authorize formal Validate or Finalize exit.

### FR-to-GOAL Alignment

| Source ID | Source title                 | Target IDs         | Coverage |
| --------- | ---------------------------- | ------------------ | -------- |
| FR-001    | Catalog availability         | GOAL-001           | 1        |
| FR-002    | Stock-state visibility       | GOAL-001           | 1        |
| FR-003    | Out-of-stock add prevention  | GOAL-001, GOAL-002 | 2        |
| FR-004    | Add quantity rule            | GOAL-002           | 1        |
| FR-005    | Update quantity rule         | GOAL-002           | 1        |
| FR-006    | Rejection-state preservation | GOAL-002           | 1        |
| FR-007    | Specific recovery message    | GOAL-003           | 1        |
| FR-008    | Same-page feedback           | GOAL-003           | 1        |
| FR-009    | Invalid-product response     | GOAL-003           | 1        |
| FR-010    | Unchanged checkout semantics | GOAL-002           | 1        |

Alignment coverage: 10 of 10 FRs, 100.0%, against the template's 100.0% target. Local-demo goal approval is recorded, while quantitative outcome measurement remains deferred.

No approved BRD or feasibility handoff was supplied. No upstream requirement IDs, waivers, research findings, or feasibility dispositions are invented.

## MVP and Release Framing

The owner-approved local-demo increment includes FR-001 through FR-010, NFR-001 through NFR-009, and the related acceptance scenarios. The cut line is validation against static catalog availability, not real inventory management. Accessibility, safe diagnostic logging, consistent rules, and preserved existing behavior remain in scope. Sources: [SM], Meeting Outcome; Transcript 16:08 (Product Manager); [LD].

The owner confirmed a local demo on 2026-10-19 and approved this PRD without the shared Confluence/Jira workflow. The original meeting's shared-delivery gate remains recorded in SM 27:24 (Product Manager) and DG; the owner's local decision does not satisfy or rewrite it for production. No implementation, delivery-capacity validation, build result, or test result is claimed by this requirements update. Source: [LD].

### Dependencies and delivery evidence

* Use the owner's local outcome, stock policy, and delegated copy direction recorded in LD for the demo. Separate shared-release approval remains necessary before treating these as authoritative product changes.
* Establish catalog fixtures for all three stock states without changing prices or images. Source: [SM], Transcript 15:19 (Store Operations Lead).
* Verify the current test setup before planning test-project work. The Engineer reported no automated test project at the meeting; that historical statement is not a current code audit. Source: [SM], Transcript 14:02 (Engineer).
* For shared delivery, record BRD/PRD approval and a real Jira story, then include the issue key in branch, commit, and PR names. The key is not known yet. Source: [DG], Definition of Ready and GitHub Traceability Convention.
* Before delivery, link clean-checkout build/test results, security/accessibility/logging checks, resolved review findings, and residual-risk ownership. No validation commands or pass results are asserted by this drafting task. Source: [DG], Definition of Done.

## Success Metrics

### Selected business measure

The owner selected the cart quantity-update rejection rate, not support-contact volume or completed checkout confirmations. The selected metric is:

```text
Inventory-rejected cart quantity updates / all cart quantity-update attempts * 100
```

Adds are excluded from both counts. The baseline is 30% and the target is 15%, a reduction of 15 percentage points. Both values were supplied by the owner, not established by the Confluence transcript or verified operational evidence. Source: [LD].

| Metric                                                             | Evidence and possible data source                                                            | Baseline                          | Target            | Owner and window                                           | Related goals |
| ------------------------------------------------------------------ | -------------------------------------------------------------------------------------------- | --------------------------------- | ----------------- | ---------------------------------------------------------- | ------------- |
| Inventory-rejected quantity updates / all quantity-update attempts | Owner-selected operational data; exact source and denominator evidence are not yet validated | 30%, user-supplied and unverified | 15%, not measured | Measurement owner and period deferred until after the demo | GOAL-002      |

Measurement is postponed until after the demo. Operational data is the permitted source category, but the specific source, completeness of attempt counts, baseline period, observation window, sampling rules, and measurement owner remain deferred. A denominator of zero must not be reported as a measured 0% rejection rate. No new user tracking, session identifiers, analytics service, or instrumentation is authorized by this PRD. Source: [LD], post-demo deferral.

Support contacts and checkout confirmations remain unselected alternatives from SM 23:10 (Product Manager); they are not additional demo success commitments. Passing acceptance scenarios or a successful demo does not establish the 15% outcome.

### Product acceptance observations

* Support expects consistent boundary outcomes and actionable messages, without generic server errors for normal inventory limits. Source: [SM], Transcript 20:51 (Support Lead).
* Operations expects a reviewer to see all stock states, try the valid maximum and one above it, and understand the result without additional explanation. Source: [SM], Transcript 22:04 (Store Operations Lead).
* Passing technical acceptance scenarios is not evidence of a reduction in support contacts or an improvement in checkout completion.

## Risks and Assumptions

### Key Assumptions

| ID      | Assumption                                                                        | Evidence status and source                                                                                                 | Impact if false                                        | Proposed verification                                                       |
| ------- | --------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------ | --------------------------------------------------------------------------- |
| ASM-001 | Static catalog quantities are sufficient for this iteration                       | Recorded scope premise: [SM], Transcript 02:35 (Store Operations Lead), 04:25 (Product Manager); locally confirmed by [LD] | A different inventory source would change scope        | Preserve the local-demo boundary                                            |
| ASM-002 | Limits can be enforced within the existing application without a database         | Engineering assessment, not implementation proof: [SM], Transcript 03:48 (Engineer)                                        | Revisit feasibility and scope                          | Inspect code during authorized research                                     |
| ASM-003 | The documented static catalog, session timeout, and checkout baseline are current | Documented, not freshly code-verified: [PO], Product Assumptions; [AA], State and Data                                     | Regression expectations may be stale                   | Compare implementation against these source versions                        |
| ASM-004 | Clear feedback at the point of action will address most quantity confusion        | Stakeholder hypothesis, unmeasured: [SM], Transcript 01:12 (Support Lead)                                                  | Technical success may not produce the intended outcome | Observe the selected metric after the demo; do not claim an improvement now |

### Risk Register

Likelihood and impact ratings were not assessed in the meeting. Mitigations below are proposed review actions, not assigned work or approvals.

| ID       | Risk and evidence status                                                                                                                                         | Source                                                                                           | Likelihood / impact     | Proposed mitigation                                                                          |
| -------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------ | ----------------------- | -------------------------------------------------------------------------------------------- |
| RISK-001 | Exact counts could disclose inventory information operations does not want exposed; original concern remains relevant outside the demo                           | [SM], Transcript 24:18 (Designer); [LD], local disclosure choice                                 | Unassessed / unassessed | Use owner-approved demo disclosure; do not infer separate operations approval for production |
| RISK-002 | A uniform low-stock threshold could mislead for products with different sales patterns; local selection does not prove business suitability                      | [SM], Transcript 25:31 (Store Operations Lead); [LD], threshold 5                                | Unassessed / unassessed | Test boundaries 0, 1, 5, and 6; reassess policy before a shared release                      |
| RISK-003 | An unverified baseline or deferred measurement could misrepresent business benefit                                                                               | [SM], Transcript 23:10 (Product Manager), 26:42 (Support Lead); [LD], stated target and deferral | Unassessed / unassessed | Label 30% unverified and 15% a target; do not claim improvement from the demo                |
| RISK-004 | Inaccessible feedback or unexpected focus loss could prevent recovery; derived risk from explicit design expectations                                            | [SM], Transcript 10:42 and 17:20 (Designer)                                                      | Unassessed / unassessed | Perform AC-013 in the agreed test environment                                                |
| RISK-005 | Errors or logs could expose sensitive information; derived risk from explicit prohibitions                                                                       | [SM], Transcript 08:15 (Product Manager), 18:41 (Engineer); [AA], Observability                  | Unassessed / unassessed | Inspect responses and captured logs under AC-014                                             |
| RISK-006 | Validation-only quantities could be mistaken for reserved stock or real fulfillment guarantees; engineering implication, not an incident reported in the meeting | [SM], Transcript 04:25 (Product Manager)                                                         | Unassessed / unassessed | Review copy so it does not promise reservation                                               |

### Question Dispositions

The nine original questions retain their IDs for traceability. LD resolves the demo's immediate behavioral choices; measurement planning is explicitly deferred until after the demo. No new response is required before using these requirements for the local demo.

| ID     | Question and owner disposition                                                                                                                                                               | Evidence                                                                                      | Owner or reviewer                                                               | Status                                                    |
| ------ | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------- | --------------------------------------------------------- |
| OQ-001 | Select inventory-rejected quantity updates / all quantity-update attempts; stated baseline 30%, target 15%; defer measurement evidence, owner, and window until after the demo               | [SM], Transcript 23:10 and 27:24 (Product Manager), 26:42 (Support Lead); [LD]                | Juan Manuel Servera approves the local goal; measurement owner not yet assigned | Outcome resolved; measurement planning deferred post-demo |
| OQ-002 | Show exact counts only for low-stock listings; use labels otherwise                                                                                                                          | [SM], Transcript 12:03 (Engineer), 12:18 (Product Manager), 24:18 (Designer); [LD]            | Juan Manuel Servera                                                             | Resolved for local demo                                   |
| OQ-003 | Use a global threshold of 5: 0 unavailable, 1 through 5 low stock, above 5 in stock                                                                                                          | [SM], Transcript 25:31 (Store Operations Lead); [LD]                                          | Juan Manuel Servera                                                             | Resolved for local demo                                   |
| OQ-004 | Use the plain-language copy authored in this PRD under the owner's authorization to propose suitable demo copy                                                                               | [SM], Transcript 06:22 (Support Lead), 08:15 and 12:18 (Product Manager); [LD]                | Juan Manuel Servera                                                             | Resolved by delegated copy direction                      |
| OQ-005 | Always disclose exact available quantity in inventory-rejection messages, independent of listing disclosure                                                                                  | [SM], Transcript 06:22 (Support Lead), 24:18 (Designer); [LD]                                 | Juan Manuel Servera                                                             | Resolved for local demo                                   |
| OQ-006 | Reject a zero-quantity add without state changes; remove the line on a zero-quantity update                                                                                                  | [SM], Transcript 13:27 (Support Lead), 14:02 (Engineer); [LD]                                 | Juan Manuel Servera                                                             | Resolved for local demo                                   |
| OQ-007 | Owner manually validates desktop Chrome and Edge at the end of development; retain behavioral accessibility checks without claiming formal conformance; record actual setup during execution | [SM], Transcript 17:20 (Designer); [PO], Success Measures; [LD]                               | Juan Manuel Servera                                                             | Scope resolved; test execution pending                    |
| OQ-008 | Extend availability validation for a local demo on 2026-10-19; retain exclusions; owner approves locally without a Confluence/Jira approval workflow                                         | [PO], Current Scope; [SM], Transcript 27:24 (Product Manager); [DG], Document Lifecycle; [LD] | Juan Manuel Servera                                                             | Approved for local demo only                              |
| OQ-009 | Use operational data for the selected metric, with no new customer tracking; defer exact source, collection plan, and evidence until after the demo                                          | [SM], Transcript 18:41 (Engineer), 23:10 (Product Manager); [PO], Current Scope; [LD]         | Measurement owner not yet assigned                                              | Deferred post-demo; not a demo implementation blocker     |

The post-demo measurement date and owner are not assigned. Desktop viewport sizes and actual browser versions are recorded at validation time rather than invented in this PRD. A shared or production release still requires the appropriate separate review and approvals.

## Glossary

* Available quantity: the product's catalog-held quantity used for validation, not a live warehouse count.
* Resulting cart quantity: units in the cart after an accepted add or quantity update; repeated adds must include units already present.
* Low stock: catalog availability from 1 through 5 inclusive under the owner-approved global demo policy.
* Reservation: setting stock aside for a shopper; excluded from this increment.
* Checkout: a confirmation that clears the session cart, without payment processing or persistent order creation.
* Local working artifact: a repository document that can have local-demo owner approval without becoming an authoritative Confluence document.

Definitions derive from [SM], Transcript 02:35 and 09:28 (Store Operations Lead), 04:25 (Product Manager), 25:31 (Store Operations Lead); [PO], Product Baseline and Product Assumptions; [LD], low-stock and approval-scope decisions.

## Sign-Off

### Approval Checklist

* [x] Record Juan Manuel Servera as owner, reviewer, and local-demo approver on 2026-10-19.
* [x] Record the owner's behavioral decisions and explicitly postpone outcome measurement.
* [x] Retain all MUST priorities and approve preservation of every valid cart quantity on rejected adds.
* [x] Record local approval of the PRD and acceptance criteria, including authorization to propose the demo copy.
* [ ] Implement and run the required build and automated tests.
* [ ] Complete the owner's final desktop Chrome/Edge manual validation, including the accessibility behaviors.
* [ ] Complete formal PRD quality review before any Validate or Finalize exit.
* [ ] For shared or production delivery, record the approved BRD/PRD and real Jira story with source links.

Approval evidence: the owner identified himself and stated "Approve the current PRD" in the first answer round, then supplied threshold 5, the metric definition, and the post-demo measurement deferral in the follow-up. Source: [LD]. This is local-demo approval only; no approval by the meeting's roles or a separate design team is asserted.

### Waivers

No formal governance waiver is supplied or granted. The owner explicitly selected the repository-supported local demo path without the shared approval workflow. Measurement is deferred by the owner; this does not waive shared-release quality gates.

### Handoff Readiness

Status: requirements approved for the local demo; implementation and technical validation are not performed by this update. Local approval is by Juan Manuel Servera on 2026-10-19. Outcome measurement is deferred until after the demo.

Shared-delivery readiness is not established: no formal PRD quality report, Confluence approval, approved Jira story, clean-checkout build/test evidence, or governed release hash is recorded. The linkage metrics above do not authorize formal Validate or Finalize exit. No Confluence or Jira write has been performed.

## Disclaimer

This AI-assisted local-demo PRD records owner approval, not implementation quality, formal standards conformance, or measured business improvement. Required technical and manual checks remain pending. Keep it internal; local approval does not authorize public publication or changes to Confluence or Jira.

## Document Metadata

* Working version: 0.2.0, approved for the local demo only
* Created and source-checked: 2026-10-19
* Template basis: Microsoft HVE-Core requirements-author PRD template, version 1.0.0
* Template attribution: Microsoft HVE-Core Team; template licensed under [CC BY 4.0](https://creativecommons.org/licenses/by/4.0/)
* Template adaptations: frontmatter supplies the title; no process diagram is needed; local-demo approval is distinguished from formal PRD lifecycle approval
* Source authority: the four versioned Confluence pages; subsequent demo-only owner decisions are recorded separately in LD
* Upstream approved BRD and Jira issue: not supplied; no identifiers invented
* Local approval: Juan Manuel Servera, 2026-10-19, via this conversation

[SM]: https://m365cpi37447403.atlassian.net/wiki/spaces/HVE/pages/27951105/Stakeholder+Meeting+-+Inventory-Aware+Cart
[PO]: https://m365cpi37447403.atlassian.net/wiki/spaces/HVE/pages/27688962/Product+Overview
[AA]: https://m365cpi37447403.atlassian.net/wiki/spaces/HVE/pages/27820033/Application+and+Platform+Architecture
[DG]: https://m365cpi37447403.atlassian.net/wiki/spaces/HVE/pages/27852801/Delivery+and+Governance
[LD]: #local-demo-decisions
