# 2.9 Verify BI Parity and Recovery

## Prerequisites

Tasks 2.7 and 2.8; explicit pipeline/model/report verification approval.

## Learning Goal

Evaluate two architectures using repeatable correctness and operational evidence.

## Allowed Scope

`sql/tests/`, shared expected fixtures, approved SQL/DAX verification helpers, existing track-specific checks. Report conclusions in the approved milestone documentation batch.

## Actions

- [ ] Load both tracks with identical batches/cutoffs and compare grain/keys/rows to independent expectations.
- [ ] Compare revenue, profit, and units by date/category in SQL, SSAS DAX, and the Power BI model.
- [ ] Test reruns, rejected rows, late edits, and cancellation of previously loaded sales.
- [ ] Inject a failure before commit; confirm state/watermark recovery and independent track reruns.
- [ ] Measure refresh latency/resource usage and describe scheduling/deployment/maintenance differences.
- [ ] Inspect SSRS and Power BI visible totals/freshness; SQL totals alone do not prove report correctness.

## Verification and Acceptance

Both real models/reports reconcile at the same cutoff. Keep execution evidence sanitized. The learner accepts Phase 2 after correctness and recovery checks, not merely matching one happy-path grand total.
