# 0.4 Define Domain and API Contract

## Prerequisites

Task 0.1; can be done without tool installation if explicitly selected.

## Learning Goal

Translate business invariants into an API contract and independent expected test outcomes.

## Allowed Scope

`shared/contracts/`, small `shared/fixtures/` examples, and an explicitly approved design-decision batch. No API/domain implementation.

## Actions

- [ ] Define product/customer/order identifiers, decimal currency precision, UTC timestamps, and reporting calendar.
- [ ] Define draft/confirmed/cancelled transitions, stock adjustments, cancellation stock return, and repeated-command behavior. Recommend no draft stock reservation.
- [ ] Define sale-time price/cost snapshots and conflicting-update version tokens.
- [ ] Specify REST resources, DTOs, filters/sorts/pagination, 201/400/404/409 responses, and ProblemDetails shape.
- [ ] Define success/error fixtures used by .NET mocks and MSW; never include real personal data.
- [ ] Calculate tiny expected sales/stock totals independently from future application/ETL code.

## Verification and Acceptance

Walk through normal confirmation, insufficient stock, duplicate confirmation, cancellation, and conflicting edits on paper. The learner agrees to unambiguous contracts and expected results before domain implementation.
