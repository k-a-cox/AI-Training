# 0.3 Set Up .NET Workspace

## Prerequisites

Task 0.2 accepted. Work on the confirmed intended machine; SQL/BI installation is deferred to 2.1.

## Learning Goal

Learn to set up a .NET workspace, choose the required SDK and components, and understand solution/project references and the API/test development loop.

## Allowed Scope

Guide the learner through Visual Studio Community/.NET setup and creating a `dotnet/` solution, API, minimal domain/application projects as needed, test project, and safe sample configuration. Explain how to install missing required tools or packages; the learner performs installations and handles elevation. No business features or SQL packages yet.

## Actions

- [ ] Guide the learner in checking the installed .NET 10 LTS SDK and Visual Studio Community compatibility/licensing.
- [ ] Identify any missing .NET/ASP.NET components that are actually needed and explain how to install them; the learner approves and handles installations and elevation.
- [ ] Walk the learner through creating the smallest supported solution and API/test skeleton; add projects only when their boundaries are needed.
- [ ] Explain namespace/reference direction and whether pinning an installed SDK is appropriate.
- [ ] Show how to use secrets/local environment configuration and keep credentials out of commits.

## Verification and Acceptance

Guide the learner through running `dotnet --info`, building the selected solution, and performing the approved smoke check. The learner demonstrates an API response without SQL. Record verified run commands in the next approved documentation batch.
