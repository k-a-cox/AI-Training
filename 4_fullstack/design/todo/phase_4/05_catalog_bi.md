# 4.5 Extend Source Batches and BI

## Prerequisites

Tasks 2.9, 4.1, and 4.2 accepted.

## Learning Goal

Extend the existing parallel BI comparison to ingest data from separately owned service databases reproducibly.

## Allowed Scope

Catalog source extraction, immutable batch manifest, warehouse staging/load changes, and classic/modern model/report updates required for product data.

## Actions

- [ ] Capture Catalog product identity and descriptive attributes in the shared batch alongside OrderOps sales and inventory data.
- [ ] Record a stable cutoff/watermark for each source database in one immutable batch manifest; both BI tracks must consume that same manifest.
- [ ] Update SSIS and T-SQL loads independently to stage and load Catalog data without one warehouse depending on the other.
- [ ] Preserve stable product keys and use order-line sale snapshots for historical price/cost and margin calculations.
- [ ] Keep existing cancellation treatment, metric definitions, freshness indicators, and expected-result checks consistent.
- [ ] Test reruns, source changes between batches, and recovery when one source extract or warehouse load fails.

## Verification and Acceptance

Both warehouses reconcile Catalog attributes and sales facts against independent expected results for the same batch. A failed or partial load does not advance its watermark or misrepresent the other warehouse's status.
