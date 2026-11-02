# OrderOps: Fullstack and Parallel BI

A learner-led orders-and-inventory project for rebuilding C#/.NET skills, practicing SQL Server administration and two BI pipelines, and adding a mobile-friendly React frontend.

## Status

The documentation and directory foundation are captured. Application scaffolding and installations have not started. The intended Windows host and existing Visual Studio/.NET setup have been inventoried. The development topology is same-host, with BI services staged and SQL memory-limited as needed.

## Documentation

- [Design and directory structure](design/DESIGN.md)
- [Indexed implementation tasks](design/todo/index.md)
- [Original project request](../fullstack_project_prompt.md)

## Layers

| Layer | Purpose |
| --- | --- |
| `dotnet/` | ASP.NET Core .NET 10 API, domain/application logic, mock and EF Core adapters, xUnit tests |
| `sql/operational/` | SQL Server administration and seed scripts; EF migrations own operational schema |
| `sql/classic/` | Separate warehouse, SSIS ETL, SSAS Tabular model, SSRS reports |
| `sql/modern/` | Separate warehouse, T-SQL loads, Power Query and Power BI model/reports |
| `react/` | React/TypeScript/Vite, SCSS, MSW mocks and frontend tests |
| `shared/` | API contracts, deterministic fixtures and independent expected metrics |
| `tests/` | Full-stack integration workflows and reconciliation |
| `scripts/` | Explicitly approved setup, load, reset and verification helpers |

## Working Agreement

The learner writes and owns the project. Copilot defaults to chat guidance: explanations, recommendations, and steps for the learner to perform in the IDE or terminal. Requests to "help set up" or "walk through" something are not permission to scaffold, edit files, install tools, or run commands. Such actions require explicit, task-specific authorization; ask if it is unclear whether guidance or execution is wanted. An indexed task is not permission to execute it, and the learner owns task status and acceptance. Documentation is updated in batches at milestones or when requested, not alongside every small implementation change.

## Running the Project

There is no runnable application yet. Follow [task 0.2](design/todo/phase_0/02_verify_machine.md) before any SQL/BI installation.

**AI-assisted development:** GitHub Copilot is intended primarily as a chat-based tutor and guide for design choices, IDE workflows, and focused reviews. The learner authors and validates the implementation; Copilot edits, scaffolds, installs, and runs commands only when explicitly asked for the specific action.
