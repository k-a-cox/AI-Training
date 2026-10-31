# S.2 Add a Bounded Background Job

## Prerequisites

Core acceptance and explicit background-processing scope approval.

## Learning Goal

Distinguish async I/O, bounded concurrency, and CPU parallelism while managing service lifetimes.

## Allowed Scope

One export/reconciliation job, its API/status surface, durable work state, and requested focused tests.

## Actions

- [ ] Define durable work ownership, retry/idempotency, cancellation, and restart semantics.
- [ ] Implement bounded workers with framework-managed hosting and explicit dependency scopes.
- [ ] Never share DbContext across concurrent tasks or launch untracked fire-and-forget request work.
- [ ] Test queue bounds, cancellation, failure, and process restart; measure before adding parallelism.

## Verification and Acceptance

Demonstrate bounded resources and durable recovery with approved tests. Explain which work is I/O-bound and whether CPU multithreading actually helps.
