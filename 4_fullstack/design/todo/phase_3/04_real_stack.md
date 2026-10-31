# 3.4 Wire the Real Stack

## Prerequisites

Tasks 2.3 and 3.3; explicit integration/configuration approval.

## Learning Goal

Connect independently tested components without hiding deployment/configuration failures.

## Allowed Scope

Frontend API configuration, API CORS/options, integration environment setup, and narrowly needed read-only analytics query adapter if explicitly requested.

## Actions

- [ ] Configure real API/SQL endpoints on the agreed hosts with trusted LAN/VPN access where needed.
- [ ] Disable frontend mocks and API mock adapters explicitly; fail on missing dependencies.
- [ ] Verify encrypted connections, least-privilege database access, and restricted CORS.
- [ ] Keep service credentials server-side and out of Git/browser environment variables.
- [ ] Exercise real create/filter/confirm/cancel flows and inspect stock changes.
- [ ] If analytics are exposed through API, use read-only warehouse access and freshness metadata; never mix warehouse writes with application commands.

## Verification and Acceptance

When requested, inspect real persisted records and browser network requests. Stop API/SQL to verify visible failure rather than mock fallback. Do not expose the unauthenticated learning stack publicly.
