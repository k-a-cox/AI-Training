# 4.3 Integrate OrderOps with Catalog

## Prerequisites

Tasks 4.1 and 4.2 accepted.

## Learning Goal

Practice service-to-service HTTP communication while preserving local transactional guarantees.

## Allowed Scope

The OrderOps application/API boundary, a typed Catalog HTTP client, service configuration, and focused unit/API tests.

## Actions

- [ ] Add a Catalog client behind an application-level port; do not couple domain logic to HTTP or Catalog persistence.
- [ ] Configure the Catalog base address and HTTP behavior through validated configuration.
- [ ] Remove product-definition ownership/writes from OrderOps while retaining stock quantities keyed by Catalog product ID.
- [ ] Retrieve and validate the active product snapshot before beginning the OrderOps database transaction for confirmation.
- [ ] Persist the returned stable product ID and name/SKU/price/cost snapshot on the order line.
- [ ] Keep order-state change and stock consumption atomic in the OrderOps database.
- [ ] On unavailable Catalog, invalid response, or inactive product, return an explicit failure without changing stock or order state.
- [ ] Test successful confirmation, Catalog failures, inactive products, and later Catalog edits not changing confirmed order history.

## Verification and Acceptance

Focused tests prove Catalog failure cannot produce a success-shaped response or partial stock/order update. No distributed transaction or stale Catalog fallback is introduced.
