# S.5 Compare Cloud Deployment

## Prerequisites

Core acceptance and explicit account, licensing, provisioning, and budget approval.

## Learning Goal

Practice deployment/CI/CD and understand where cloud changes cost and operational ownership.

## Allowed Scope

Approved Azure API hosting, Azure SQL development copy, CI/CD, secret management, and selected Power BI publishing/gateway experiment. Fabric is separately approved later.

## Actions

- [ ] Define account permissions, cost estimates, budget alerts, least privilege, and teardown before provisioning.
- [ ] Deploy a development API/SQL copy with protected access, standard identity, and managed secrets.
- [ ] Add approved build/test/deploy automation; keep credentials outside repository/workflow text.
- [ ] Verify Power BI service sharing licenses and gateway requirements for private local SQL.
- [ ] Compare local operations with hosted availability/refresh; do not assume Azure SQL includes SSIS/SSAS/SSRS.
- [ ] Treat Codespaces as a React/.NET/mock option, not the complete Windows BI workstation.
- [ ] Evaluate Fabric pipelines/warehouse only after separate capacity/cost approval.

## Verification and Acceptance

When requested, run the protected cloud workflow, inspect costs and refresh, and tear down experiment resources. Record which workloads remain local and why.
