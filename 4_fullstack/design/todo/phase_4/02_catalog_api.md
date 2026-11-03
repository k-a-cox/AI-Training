# 4.2 Build the Catalog API and Persistence

## Prerequisites

Task 4.1 boundary and contract acceptance.

## Learning Goal

Build an independently testable ASP.NET Core service with its own persistence boundary.

## Allowed Scope

The Catalog domain/application/API, its SQL Server database and EF Core migrations, service-specific tests, and sanitized configuration examples.

## Actions

- [ ] Create a separately runnable/deployable Catalog API project and keep its domain/application logic independent from OrderOps internals.
- [ ] Implement the approved Catalog contract for creating, reading, updating, deactivating, and filtering products.
- [ ] Add Catalog-owned EF Core persistence and migrations for `OrderOpsCatalog`; do not share tables with `OrderOpsOperational`.
- [ ] Migrate existing product definitions into Catalog while preserving stable product IDs; verify the migration before switching product-definition writes to Catalog.
- [ ] Add deterministic domain tests and isolated API tests using explicit Development/Test-only adapters where appropriate.
- [ ] Ensure mock adapters fail outside Development/Test; do not silently fall back to mocks.
- [ ] Validate API errors, filtering, sorting/pagination, and relevant concurrency or duplicate-SKU rules from the accepted contract.

## Verification and Acceptance

Catalog API tests pass against isolated adapters, and approved SQL Server checks verify its migrations and persistence. The service runs independently and accesses only its owned database; it has no dependency on the OrderOps database.
