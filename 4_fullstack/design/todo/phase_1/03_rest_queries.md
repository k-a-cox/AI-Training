# 1.3 Implement REST API and LINQ Queries

## Prerequisites

Task 1.2 and the shared contract; explicit code authorization.

## Learning Goal

Practice ASP.NET Core request binding, DTOs, HTTP semantics, and composable queries.

## Allowed Scope

API controllers/DTOs/OpenAPI, application query contracts, mock query adapter, and explicitly requested API tests.

## Actions

- [ ] Implement agreed product/customer/order CRUD and confirmation/cancellation endpoints.
- [ ] Keep business decisions in application/domain code, not controllers.
- [ ] Validate input and return agreed 201/400/404/409 results with resource location/version information.
- [ ] Implement allowlisted filtering/sorting, stable pagination order, and bounded page size.
- [ ] Document DTOs and error examples in the OpenAPI contract without exposing persistence entities.
- [ ] Keep future LINQ execution server-side in the SQL adapter; do not require loading an entire table for filtering.

## Verification and Acceptance

When requested, check the scoped API behavior against mocks, including invalid filters, empty results, pagination ties, and version conflicts. SQL translation remains an explicit Phase 2 check.
