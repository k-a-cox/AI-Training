# 4.4 Add Catalog Workflows to React

## Prerequisites

Tasks 4.1 and 4.2 accepted; task 3.3 frontend isolation verification completed.

## Learning Goal

Build a frontend workflow that consumes two separately owned APIs and handles their failures clearly.

## Allowed Scope

React/TypeScript UI, API clients, MSW handlers, colocated tests, and mobile/desktop browser verification for Catalog workflows.

## Actions

- [ ] Add responsive product list/filter and create/edit/deactivate workflows using the Catalog API.
- [ ] Keep order and inventory workflows on the OrderOps API; do not route Catalog writes through OrderOps.
- [ ] Configure each API endpoint explicitly; do not silently redirect to mocks or use mock fallback at runtime.
- [ ] Add MSW handlers for both APIs and test loading, empty, validation, conflict, unavailable-service, and successful states.
- [ ] Ensure unhandled requests fail tests and display actionable UI errors for Catalog or OrderOps outages.
- [ ] Verify keyboard use and mobile/desktop layout for the new workflows.

## Verification and Acceptance

Frontend tests use isolated API mocks and cover both service contracts. Browser checks confirm Catalog changes use Catalog and order actions continue to use OrderOps.
