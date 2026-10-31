# OrderOps TODO Index

Task status is user-managed. Task 0.1 artifacts are prepared but not marked done. All implementation and installation work remains pending.

## Workflow

- Open only the requested task, or the first eligible unfinished task when asked to continue. Read other files only when that task names them or a nearby dependency requires them.
- A checklist does not authorize edits, installs, tests, commits, or task checkoff. Ask for the next scoped action when blocked.
- Each task records prerequisites, learning goals, allowed scope, actions, and acceptance. Keep task files small; do not append session history.
- Batch documentation at milestones or explicit documentation requests. No incidental README/design changes for small code edits.
- Verify the other Windows host before installing tools. Do not install on the current machine.

## Phase 0: Foundation and Host Preparation

| ID | Status | Task | Prerequisites |
| --- | --- | --- | --- |
| 0.1 | awaiting user acceptance | [Capture foundation](phase_0/01_capture_foundation.md) | Approved design |
| 0.2 | todo | [Verify intended machine](phase_0/02_verify_machine.md) | 0.1 acceptance |
| 0.3 | todo | [Set up .NET workspace](phase_0/03_dotnet_setup.md) | 0.2 and installation approval |
| 0.4 | todo | [Define domain and API contract](phase_0/04_domain_contract.md) | 0.1; no installation required |

## Phase 1: .NET With Mocks

| ID | Status | Task | Prerequisites |
| --- | --- | --- | --- |
| 1.1 | todo | [Domain rules](phase_1/01_domain_rules.md) | 0.3, 0.4 |
| 1.2 | todo | [Application ports and mocks](phase_1/02_ports_mocks.md) | 1.1 |
| 1.3 | todo | [REST API and LINQ](phase_1/03_rest_queries.md) | 1.2 |
| 1.4 | todo | [Middleware and configuration](phase_1/04_middleware_configuration.md) | 1.3 |
| 1.5 | todo | [Isolated API verification](phase_1/05_api_verification.md) | 1.4 |

Gate: API works without SQL; approved tests cover domain rules and HTTP behavior. Learner demonstrates the workflow and explains the dependency boundaries.

## Phase 2: SQL Persistence and Parallel BI

| ID | Status | Task | Prerequisites |
| --- | --- | --- | --- |
| 2.1 | todo | [SQL and BI setup](phase_2/01_sql_bi_setup.md) | 0.2 and installation approval |
| 2.2 | todo | [EF Core SQL persistence](phase_2/02_sql_persistence.md) | 1.5, 2.1 |
| 2.3 | todo | [Real SQL verification](phase_2/03_sql_verification.md) | 2.2 |
| 2.4 | todo | [Warehouse parity contract](phase_2/04_warehouse_contract.md) | 0.4, 2.3 |
| 2.5 | todo | [Classic warehouse and SSIS](phase_2/05_classic_etl.md) | 2.4 |
| 2.6 | todo | [Modern warehouse and SQL loads](phase_2/06_modern_etl.md) | 2.4 |
| 2.7 | todo | [SSAS and SSRS](phase_2/07_classic_model_reports.md) | 2.5 |
| 2.8 | todo | [Power Query and Power BI](phase_2/08_modern_model_reports.md) | 2.6 |
| 2.9 | todo | [Parity and recovery verification](phase_2/09_bi_parity.md) | 2.7, 2.8 |

Tasks 2.5 and 2.6 are independently implementable; 2.7 and 2.8 follow their respective tracks. No second SQL engine version or Fabric capacity is required.

Gate: both real pipelines produce expected rows and measures at identical cutoffs, tolerate reruns and failures, and expose refresh freshness.

## Phase 3: React and Full Integration

| ID | Status | Task | Prerequisites |
| --- | --- | --- | --- |
| 3.1 | todo | [Frontend setup and MSW](phase_3/01_frontend_setup.md) | 1.5; can overlap Phase 2 |
| 3.2 | todo | [Responsive workflows](phase_3/02_frontend_workflows.md) | 3.1 |
| 3.3 | todo | [Frontend isolation verification](phase_3/03_frontend_verification.md) | 3.2 |
| 3.4 | todo | [Real-stack wiring](phase_3/04_real_stack.md) | 2.3, 3.3 |
| 3.5 | todo | [End-to-end and BI reconciliation](phase_3/05_full_integration.md) | 2.9, 3.4 |
| 3.6 | todo | [Acceptance and documentation batch](phase_3/06_acceptance.md) | 3.5 |

Gate: mobile and desktop order workflows work against the real API/database, and both BI tracks reconcile after explicit refresh.

## Stretch: Separate Approval Required

| ID | Status | Task | Prerequisites |
| --- | --- | --- | --- |
| S.1 | deferred | [Authentication and API policies](stretch/01_auth_policies.md) | Core acceptance |
| S.2 | deferred | [Bounded background job](stretch/02_background_job.md) | Core acceptance |
| S.3 | deferred | [Inventory and historical dimensions](stretch/03_history_inventory.md) | Core acceptance |
| S.4 | deferred | [Power BI Report Server migration](stretch/04_report_server.md) | Core acceptance, licensing check |
| S.5 | deferred | [Cloud deployment comparison](stretch/05_cloud.md) | Core acceptance, account/budget approval |
