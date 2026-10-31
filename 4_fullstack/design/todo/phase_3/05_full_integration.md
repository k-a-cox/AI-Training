# 3.5 Verify Full Integration and BI

## Prerequisites

Tasks 2.9 and 3.4; explicit end-to-end test and pipeline execution approval.

## Learning Goal

Verify one business workflow across UI, API, SQL, ETL, models, and reports.

## Allowed Scope

`tests/integration/`, existing fixtures, approved SQL/DAX/refresh helpers, and scoped Playwright configuration. No unrelated feature work.

## Actions

- [ ] Start an isolated real API/database/frontend environment and seed only its owned test data.
- [ ] Use Playwright to create/select product and customer, enter/filter/edit an order, and confirm it.
- [ ] Assert real stock/order rows; cancel a separate confirmed order and verify agreed behavior.
- [ ] Capture a stable source batch/cutoff and execute both warehouse loads.
- [ ] Refresh SSAS and Power BI models explicitly; reconcile SQL/DAX metrics with golden results.
- [ ] Inspect SSRS/Power BI report totals and parameters. Playwright browser automation does not validate Power BI Desktop UI.
- [ ] Clean up only integration-owned data/services; preserve unrelated local work.

## Verification and Acceptance

Run the approved Playwright workflow plus SQL/DAX assertions and real BI report checks. Record which checks were manual, which automated, and any unavailable service/tooling. Both tracks agree at the same cutoff.
