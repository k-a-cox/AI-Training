# 2.3 Verify Real SQL Behavior

## Prerequisites

Task 2.2 and explicit test execution approval; disposable SQL Server database available.

## Learning Goal

Recognize differences between mock behavior and database translation, locking, and transactions.

## Allowed Scope

Existing .NET API/SQL integration tests, fixtures, SQL assertions, and explicitly approved local repairs. No replacement SQLite/EF InMemory provider.

## Actions

- [ ] Run the shared API behavior contract against the real SQL adapter with isolated data.
- [ ] Race two independent stock claims and assert no oversell or partial order commit.
- [ ] Verify rowversion conflict handling and rollback after injected persistence failure.
- [ ] Test cancellation and repeated commands against actual stored state.
- [ ] Inspect representative LINQ-generated SQL, stable pagination, indexes, and an execution plan.
- [ ] Verify SQL outages cause explicit failures and never mock fallback.

## Verification and Acceptance

Run the requested integration collection and SQL assertions. Every test cleans up only its own disposable database/data. Accept real persistence only after concurrency, rollback, and translation checks pass.
