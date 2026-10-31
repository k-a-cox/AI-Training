# 1.5 Verify API Isolation

## Prerequisites

Task 1.4; explicit permission to write/run the verification suite.

## Learning Goal

Separate domain tests from in-process HTTP tests and recognize mock limitations.

## Allowed Scope

`dotnet/tests/OrderOps.ApiTests/`, existing test fixtures/helpers, and local repairs to the failing touched behavior only when authorized.

## Actions

- [ ] Use WebApplicationFactory with explicit mock adapters and deterministic state per test.
- [ ] Cover create/filter/confirm/cancel, invalid inputs, missing resources, insufficient stock, and version conflicts.
- [ ] Verify pagination and contract status/error payloads, not just successful HTTP status.
- [ ] Test forbidden mock configuration and absence of SQL dependencies in mock mode.
- [ ] Distinguish mocked LINQ/concurrency checks from the required real SQL tests in 2.3.

## Verification and Acceptance

Run the requested build/test slice and demonstrate the API with SQL unavailable. The learner explains middleware/port boundaries and accepts Phase 1. Update milestone documentation only as an approved batch, including the root project summary where needed.
