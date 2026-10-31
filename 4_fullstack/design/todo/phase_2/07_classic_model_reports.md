# 2.7 Build SSAS Model and SSRS Reports

## Prerequisites

Task 2.5 and explicit SSAS/SSRS implementation/deployment approval.

## Learning Goal

Separate dimensional data, semantic calculations, and paginated presentation.

## Allowed Scope

`sql/classic/ssas/`, `sql/classic/ssrs/`, safe deployment configuration and requested model/report checks.

## Actions

- [ ] Build SSAS Tabular relationships, date model, and agreed DAX revenue/profit/unit measures.
- [ ] Process fixtures and expose the loaded batch/cutoff or freshness indicator.
- [ ] Create parameterized SSRS sales/margin reports with date/category filters and readable totals.
- [ ] Use SSAS for semantic aggregates or warehouse datasets for detail reports, explicitly stating each report's source.
- [ ] Validate report exports and read-only service identities; never store credentials in source definitions.
- [ ] Keep RDL/BIM/project sources tracked and generated deployment output ignored.

## Verification and Acceptance

When requested, process the real SSAS model, query DAX against expected fixtures, deploy/open SSRS reports, and check parameters and PDF/CSV exports. Do not simulate engines to claim completion.
