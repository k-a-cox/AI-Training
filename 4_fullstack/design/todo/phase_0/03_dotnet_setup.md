# 0.3 Set Up .NET Workspace

## Prerequisites

Task 0.2 passed on the intended machine; explicit installation/scaffolding approval. SQL/BI installation is deferred to 2.1.

## Learning Goal

Understand SDK selection, solution/project references, and the API/test development loop.

## Allowed Scope

Approved Visual Studio Community/.NET setup and `dotnet/` solution, API, minimal domain/application projects as needed, test project, and safe sample configuration. No business features or SQL packages yet.

## Actions

- [ ] Verify current .NET 10 LTS SDK and Visual Studio Community compatibility/licensing.
- [ ] Install only approved .NET/ASP.NET components; the learner handles elevation directly.
- [ ] Create the smallest supported solution and API/test skeleton; add projects only when their boundaries are needed.
- [ ] Establish namespace/reference direction and pin an installed SDK if appropriate.
- [ ] Use secrets/local environment configuration; do not commit credentials.

## Verification and Acceptance

When requested, run `dotnet --info`, build the selected solution, and run the approved smoke check. Demonstrate an API response without SQL. Record verified run commands in the next approved documentation batch.
