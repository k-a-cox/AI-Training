# 1.1 Implement Domain Rules

## Prerequisites

Tasks 0.3 and 0.4; explicit scoped code/test authorization.

## Learning Goal

Practice C# modeling and invariants without coupling business behavior to HTTP or EF Core.

## Allowed Scope

`dotnet/src/OrderOps.Domain/`, required application coordination, and focused `dotnet/tests/OrderOps.UnitTests/` files when requested.

## Actions

- [ ] Model products, customers, orders/lines, stock movements, and agreed state transitions.
- [ ] Enforce valid quantities, decimal price/cost values, and sale-time snapshots.
- [ ] Implement confirmation/cancellation semantics, preserving order history.
- [ ] Express stock consumption as an operation requiring an atomic store boundary, not separate uncoordinated saves.
- [ ] Cover insufficient stock, invalid transitions, repeated commands, and cancellation stock behavior with independent expected fixtures.

## Verification and Acceptance

Run only the requested unit-test slice. The learner explains the invariants and identifies which atomic/concurrency guarantees still need the real SQL adapter. No SQL/HTTP dependency in core domain logic.
