# 2.2 Implement SQL Persistence

## Prerequisites

Tasks 1.5 and 2.1; explicit code/database-change authorization.

## Learning Goal

Translate port contracts into EF Core SQL Server queries and atomic persistence.

## Allowed Scope

Infrastructure adapter/EF migrations in `dotnet/`, safe configuration and seed/admin scripts in `sql/operational/`, requested tests.

## Actions

- [ ] Create OrderOpsOperational through versioned migrations; avoid competing hand-maintained table definitions.
- [ ] Add keys, relationships, decimal precision, check constraints, indexes, and rowversion mapping.
- [ ] Implement server-side filters/sorts/pagination and asynchronous SQL operations.
- [ ] Make confirmation/stock changes and cancellation stock restoration atomic with safe concurrency handling.
- [ ] Register the SQL adapter explicitly with least-privilege access; fail readiness on missing SQL rather than selecting mocks.
- [ ] Add reproducible seed/reset steps for isolated development databases only.
- [ ] Practice backup and restore into a separate disposable database, never overwriting unrelated data.

## Verification and Acceptance

When requested, apply migrations to an isolated SQL database, exercise CRUD/state changes, and verify constraints/restore. The learner explains schema ownership, transaction boundaries, and what an index changes.
