# S.3 Add Inventory Snapshots and History

## Prerequisites

Core acceptance and explicit dimensional-scope approval.

## Learning Goal

Practice snapshot grain and slowly changing dimensions without losing BI parity.

## Allowed Scope

Both warehouses/ETL/models, shared expected fixtures, and the relevant reports.

## Actions

- [ ] Define daily inventory snapshot grain and reporting calendar.
- [ ] Define Type 2 product/category validity ranges and historical lookup semantics.
- [ ] Implement both tracks with equivalent keys, effective dates, and late-change handling.
- [ ] Add expected fixtures for category changes, late arrivals, and inventory balance.

## Verification and Acceptance

When requested, compare historical/snapshot metrics across both real models and confirm unchanged baseline sales totals.
