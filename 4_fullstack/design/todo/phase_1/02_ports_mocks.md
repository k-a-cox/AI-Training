# 1.2 Add Application Ports and Mocks

## Prerequisites

Task 1.1; explicit scoped code authorization.

## Learning Goal

Apply dependency inversion and DI without inventing unnecessary generic abstractions.

## Allowed Scope

Application/domain-specific ports, development mock adapters, composition registration, and requested focused tests in `dotnet/`.

## Actions

- [ ] Define small use-case-oriented ports such as `IOrderStore` and `IProductCatalog`.
- [ ] Coordinate order/stock operations through one atomic adapter contract.
- [ ] Implement deterministic fake data/reset behavior and version conflict responses.
- [ ] Register lifetimes intentionally; ensure a shared mock store cannot corrupt state under simultaneous requests.
- [ ] Permit mock selection only in Development/Test; fail startup on mock selection elsewhere.
- [ ] Keep real integration configuration separate, without automatic fallback.

## Verification and Acceptance

When requested, test reset reproducibility, state transitions, overlapping mock requests, and environment guards. Demonstrate application services through ports without SQL or HTTP and explain the limits of fake concurrency behavior.
