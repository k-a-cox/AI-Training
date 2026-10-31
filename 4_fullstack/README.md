# OrderOps: Fullstack and Parallel BI

A learner-led orders-and-inventory project for rebuilding C#/.NET skills, practicing SQL Server administration and two BI pipelines, and adding a mobile-friendly React frontend.

## Status

The documentation and directory foundation are captured. Application code, databases, dependencies, and IDE installations have not been created. Development starts with a mocked .NET API; SQL tooling belongs on a different Windows machine that must be verified before installation.

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

The learner leads development and checks off tasks. An indexed task is not permission to execute it. Request one scoped change at a time; installs, code changes, tests, and commits require explicit authorization. Documentation is updated in batches at milestones or when requested, not alongside every small implementation change.

## Running the Project

There is no runnable application yet. Commands will be recorded at the relevant implementation milestones after verification on the intended host. Follow [task 0.2](design/todo/phase_0/02_verify_machine.md) before installing tools.

**AI-assisted development:** GitHub Copilot helped design the project, compare classic Microsoft BI with Power BI, and capture the initial documentation, task breakdown, and ignore rules. Implementation and task acceptance remain learner-led.
