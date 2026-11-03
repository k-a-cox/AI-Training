# 4.1 Define the Catalog Service Boundary

## Prerequisites

Phase 3 task 3.6 accepted. Confirm the learner wants to begin Phase 4.

## Learning Goal

Define a useful microservice boundary without distributing order confirmation and stock consumption.

## Allowed Scope

Design decisions and contract documentation for the Catalog service and its use by OrderOps, React, and the BI source batch.

## Actions

- [ ] Assign product definitions, stable product IDs/SKUs, category, current price/cost, and active status to Catalog.
- [ ] Keep orders, inventory, and atomic order confirmation in the existing OrderOps service and database.
- [ ] Define Catalog REST operations and validation, including create, update, deactivate, get by ID, and filtered list.
- [ ] Define a typed HTTP client contract for OrderOps to retrieve active product data at confirmation.
- [ ] Specify order-line snapshots: stable product ID plus the product name/SKU/price/cost returned for confirmation; later Catalog edits do not rewrite history.
- [ ] Specify failure behavior: an unavailable Catalog or inactive product prevents confirmation and leaves order state and stock unchanged; no stale-data fallback or distributed transaction.
- [ ] Define separate Catalog database ownership and a coordinated immutable BI source-batch manifest with a cutoff/watermark for each service database.
- [ ] Keep a message broker, service mesh, and additional service splits out of scope.

## Verification and Acceptance

The learner can explain service ownership, API contracts, failure behavior, order snapshot semantics, and how both BI tracks will consume both sources consistently. No service code or infrastructure is created in this task.
