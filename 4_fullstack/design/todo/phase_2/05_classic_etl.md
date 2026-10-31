# 2.5 Build Classic Warehouse and SSIS

## Prerequisites

Task 2.4; explicit SQL/SSIS implementation and execution approval. Independent of modern-track implementation.

## Learning Goal

Practice SSIS control/data flow, validation, deployment, incremental loading, and operations.

## Allowed Scope

`sql/classic/database/`, `sql/classic/ssis/`, their fixture assertions and safe deployment parameters.

## Actions

- [ ] Deploy OrderOpsClassicDW schema and its own control/audit/reject objects.
- [ ] Build SSIS staging, lookups/transformations, dimension loads, and fact loads at the agreed grain.
- [ ] Consume the shared immutable batch; apply changes/cancellations and avoid duplicate facts on reruns.
- [ ] Capture row lineage, rejects, errors, and batch counts; commit load/watermark safely.
- [ ] Parameterize connections through safe runtime configuration; deploy to SSISDB when appropriate.
- [ ] Schedule warehouse loads with SQL Server Agent and retain execution history.

## Verification and Acceptance

When requested, execute initial/incremental fixtures, rerun a batch, inject a failure, and verify independent expected counts/totals and watermark recovery. Check SSIS catalog/Agent history and absence of committed credentials.
