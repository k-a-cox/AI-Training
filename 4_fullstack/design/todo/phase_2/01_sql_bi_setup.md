# 2.1 Set Up SQL and BI Tools

## Prerequisites

Task 0.2, renewed confirmation of the OTHER host, and explicit installation approval. Can be prepared alongside Phase 1 if requested.

## Learning Goal

Distinguish database engine, runtime services, IDE extensions, and management tools.

## Allowed Scope

Approved installations/configuration on the verified host and `sql/` safe setup samples. No application persistence or warehouse implementation yet.

## Actions

- [ ] Verify version compatibility/licensing and inspect existing installations before downloading anything.
- [ ] Install SQL Server 2022 Developer engine/Agent, SSMS, and SSDT as approved.
- [ ] Install separate SSIS/SSAS/SSRS Visual Studio extensions; these do not replace runtime server installations.
- [ ] Install/configure SSIS/catalog, SSAS Tabular, and separate SSRS 2022 server as approved.
- [ ] Install standard Power BI Desktop for local authoring; defer Report Server/service publishing.
- [ ] Set sensible service memory limits, local identities, encrypted connections, and narrowly scoped network access.
- [ ] Confirm template availability and each service connection without committing passwords or server-specific settings.

## Verification and Acceptance

Demonstrate a database query, an SSIS smoke execution, an SSAS connection, an SSRS portal connection, and a Power BI SQL import when requested. Confirm this is development/test only. The learner handles elevation directly and accepts tool readiness.
