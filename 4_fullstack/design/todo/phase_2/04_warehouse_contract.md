# 2.4 Define Warehouse Parity Contract

## Prerequisites

Tasks 0.4 and 2.3; explicit data-contract/schema authorization.

## Learning Goal

Specify warehouse grain, incremental-change safety, lineage, and measurable reconciliation.

## Allowed Scope

Shared BI fixture/metric contract and classic/modern warehouse schema definitions. No ETL packages or reports yet.

## Actions

- [ ] Define FactSales at one confirmed order line and Date/Product/Customer dimensions with Type 1 behavior.
- [ ] Define cancelled-sale handling, currency/calendar, sale-time costs, and identical revenue/profit/unit measures.
- [ ] Choose stable immutable source batches/cutoffs and a committed-change extraction strategy that cannot miss out-of-order commits.
- [ ] Define separate staging, reject, batch/audit, watermark, and stable-key deduplication structures per warehouse.
- [ ] Define atomic watermark advancement and recovery behavior for each track.
- [ ] Create a tiny golden fixture and independently calculated totals; specify late changes, cancellations, and invalid rows.
- [ ] Plan seeded twelve-month synthetic data adjusted to hardware; exclude large generated output from Git.

## Verification and Acceptance

Manually trace one initial batch, one update/cancellation batch, a rerun, and a failed batch through BOTH schemas. Confirm identical expected outputs and explain why raw timestamp/increasing-ID watermarks alone can be unsafe.
