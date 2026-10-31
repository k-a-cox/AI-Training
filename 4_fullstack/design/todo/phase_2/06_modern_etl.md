# 2.6 Build Modern Warehouse and SQL Loads

## Prerequisites

Task 2.4; explicit SQL implementation/execution approval. Independent of the classic track.

## Learning Goal

Implement repeatable set-based ETL with transparent transactions and operational controls.

## Allowed Scope

`sql/modern/database/`, `sql/modern/loads/`, fixture assertions and safe Agent scheduling configuration.

## Actions

- [ ] Deploy OrderOpsModernDW with equivalent grain/metrics and independent control/audit/reject state.
- [ ] Implement versioned staging, validation, dimension/fact load procedures using set-based SQL.
- [ ] Read the same immutable source batch/cutoff as the classic track, never its warehouse output.
- [ ] Handle updates/cancellations, stable-key deduplication, and atomic load/watermark advancement.
- [ ] Add structured batch diagnostics and restart behavior; parameterize environment connections.
- [ ] Schedule loads with SQL Server Agent. Power Query model shaping is a separate task.

## Verification and Acceptance

When requested, run initial/incremental/retry/failure fixtures and compare with golden expectations. Confirm the modern pipeline can run with the classic pipeline stopped and cannot advance a watermark after failure.
