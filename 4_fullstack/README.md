# OrderOps: Fullstack, Parallel BI, and Microservices

A learner-led orders-and-inventory project for rebuilding C#/.NET skills, practicing SQL Server administration and two BI pipelines, and adding a mobile-friendly React frontend. The core is designed as a modular monolith; an optional Phase 4 adds one Product Catalog microservice across .NET, SQL/BI, and React.

## Status

The documentation and directory foundation are captured. Application scaffolding and installations have not started. The intended Windows host and existing Visual Studio/.NET setup have been inventoried. The development topology is same-host, with BI services staged and SQL memory-limited as needed. Phase 4 is planned after core acceptance and is not required to complete Phases 0-3.

## Documentation

- [Design and directory structure](design/DESIGN.md)
- [Indexed implementation tasks](design/todo/index.md)
- [Original project request](../fullstack_project_prompt.md)

## Layers

| Layer | Purpose |
| --- | --- |
| `dotnet/` | Phase 1-3 modular OrderOps API and, in Phase 4, an independent Catalog API with its own tests and persistence |
| `sql/operational/` | Order/inventory database; EF migrations own its schema |
| `sql/catalog/` | Phase 4 Catalog-owned database schema and seed data |
| `sql/classic/` | Separate warehouse, SSIS ETL, SSAS Tabular model, SSRS reports |
| `sql/modern/` | Separate warehouse, T-SQL loads, Power Query and Power BI model/reports |
| `react/` | React/TypeScript/Vite, SCSS, MSW mocks and frontend tests; Phase 4 adds Catalog workflows using its API |
| `shared/` | API contracts, deterministic fixtures and independent expected metrics |
| `tests/` | Full-stack integration workflows, service integration and reconciliation |
| `scripts/` | Explicitly approved setup, load, reset and verification helpers |

In Phase 4, the Catalog service owns product definitions and its database. The OrderOps API continues to own orders and inventory; it fetches an active product snapshot from Catalog before confirming an order and stores sale-time values locally. The two BI tracks consume both service databases through the same immutable source-batch manifest. This adds one bounded service boundary without requiring a message broker or distributed transaction.

## Working Agreement

The learner leads development and checks off tasks. An indexed task is not permission to execute it. Request one scoped change at a time; installs, code changes, tests, and commits require explicit authorization. Documentation is updated in batches at milestones or when requested, not alongside every small implementation change.

## Running the Project

There is no runnable application yet. Commands will be recorded at the relevant implementation milestones after verification on the intended host. Follow [task 0.2](design/todo/phase_0/02_verify_machine.md) before installing tools.

**AI-assisted development:** GitHub Copilot is intended primarily as a chat-based tutor and guide for design choices, IDE workflows, service boundaries, and focused reviews. The learner authors and validates the implementation; Copilot edits, scaffolds, installs, and runs commands only when explicitly asked for the specific action.
