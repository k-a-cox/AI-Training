# 4.6 Verify the Integrated Services

## Prerequisites

Tasks 4.3, 4.4, and 4.5 accepted, with Phase 3 core acceptance retained.

## Learning Goal

Validate the new service boundary across the real .NET, SQL Server, React, and BI stack.

## Allowed Scope

Approved end-to-end checks, focused defect fixes authorized by the learner, and Phase 4 milestone documentation/status updates when requested.

## Actions

- [ ] Start both APIs against their separately owned SQL Server databases and verify their health/configuration.
- [ ] Create and update a Catalog product through React, then create and confirm an order using it through the OrderOps API.
- [ ] Verify the order snapshot and stock transaction in OrderOps and confirm later Catalog edits do not rewrite the order line.
- [ ] Stop or make Catalog unavailable and verify confirmation fails clearly without changing order state or stock.
- [ ] Run both BI tracks from the same immutable batch manifest and reconcile Catalog attributes, sales, and metrics.
- [ ] Verify frontend and API failure handling, logs/correlation where configured, and no mock fallback in real integration.
- [ ] Record reproducible commands and any limitations; the learner decides acceptance and updates task status.

## Verification and Acceptance

The learner can reproduce the cross-service workflow, explain the ownership and consistency boundaries, and verify both BI tracks at the same batch. No additional services, cloud deployment, or commit is implied.
