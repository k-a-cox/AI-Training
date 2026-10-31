# 2.8 Build Power Query and Power BI

## Prerequisites

Task 2.6 and explicit Power BI authoring/refresh approval.

## Learning Goal

Practice Power Query shaping, semantic modeling, DAX, and interactive reporting.

## Allowed Scope

`sql/modern/powerbi/`, safe source configuration, and requested model checks. No service publication or Report Server installation.

## Actions

- [ ] Import modern warehouse tables using Power Query; preserve query folding where supported.
- [ ] Define relationships/date model and equivalent DAX revenue/profit/unit semantics.
- [ ] Build monthly revenue, gross profit, and category sales views with useful filters and freshness indicators.
- [ ] Validate filter interactions and empty-data behavior rather than only decorative chart appearance.
- [ ] Prefer supported source-friendly Power BI project format; otherwise sanitize PBIX data/credentials and budget binary size.
- [ ] Refresh manually in Desktop initially; keep local caches ignored and model/report source tracked.

## Verification and Acceptance

When requested, refresh the real model, compare filtered measures with golden fixtures, and inspect reports at the shared batch cutoff. Desktop authoring is local; cloud sharing/scheduled service refresh needs separate account/licensing approval.
