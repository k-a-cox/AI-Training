# 3.2 Build Responsive Workflows

## Prerequisites

Task 3.1 and explicit UI implementation approval.

## Learning Goal

Build practical data-entry and filtering workflows with accessible, responsive SCSS.

## Allowed Scope

React screens/components/styles/API client and existing MSW fixtures. No backend/warehouse feature expansion.

## Actions

- [ ] Build product/customer selection, stock adjustments, order entry, and filtered order/product lists.
- [ ] Support confirmation/cancellation and agreed edit/version behavior with clear conflict recovery.
- [ ] Include loading, empty, unavailable-server, validation, and successful-save states.
- [ ] Use accessible labels, keyboard focus, practical mobile layouts, and stable control sizes.
- [ ] Show operational summaries from API data; do not allow direct browser writes to SQL/warehouses.
- [ ] Keep BI reports separate from the operational UI initially; embedding is not required.

## Verification and Acceptance

When requested, exercise complete mock workflows at 360px and desktop widths with keyboard navigation. Verify text fits, controls remain usable, and error/conflict states do not lose entered data.
