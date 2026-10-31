# 3.3 Verify Frontend Isolation

## Prerequisites

Task 3.2 and explicit test authoring/execution approval.

## Learning Goal

Test user-visible behavior while guarding against mock/API contract drift.

## Allowed Scope

Colocated Vitest/Testing Library tests, shared test helpers, MSW handlers, and requested focused UI repairs.

## Actions

- [ ] Test entry/filter workflows, invalid inputs, loading/empty states, server failures, and 409 recovery.
- [ ] Assert request DTOs/query parameters and rendered results against shared fixtures.
- [ ] Fail unhandled MSW requests and reset handlers/state between tests.
- [ ] Compare handler status/error shapes with the real API contract; do not treat mocks as authoritative.
- [ ] Cover keyboard interaction and saved/unsaved input behavior where relevant.

## Verification and Acceptance

Run requested frontend test/lint/build scripts. Demonstrate tests work with API and SQL stopped. The learner accepts the isolated frontend before real wiring.
