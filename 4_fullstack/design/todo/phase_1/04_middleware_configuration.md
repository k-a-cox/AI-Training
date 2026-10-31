# 1.4 Add Middleware and Configuration

## Prerequisites

Task 1.3; explicit code/configuration authorization.

## Learning Goal

Understand middleware order, scoped dependencies, async cancellation, and safe operational configuration.

## Allowed Scope

API composition/middleware/options/health endpoints, safe sample settings, and explicitly requested focused tests. No secrets or production deployment.

## Actions

- [ ] Add safe ProblemDetails handling, correlation IDs, and structured logs without sensitive payloads.
- [ ] Make environment options and DI lifetimes explicit; validate required configuration at startup.
- [ ] Use async I/O and propagate CancellationToken across API/application/adapter boundaries.
- [ ] Add health checks appropriate to mock mode; distinguish liveness from dependency readiness later.
- [ ] Restrict CORS to configured development origins and keep credentials out of Git.
- [ ] Keep authentication/rate limiting as separate approved work; do not expose this unauthenticated learning API publicly.

## Verification and Acceptance

When requested, check middleware order, unhandled exceptions, correlation propagation, startup failures, cancellation, and allowed/disallowed origins. The learner can explain each DI lifetime and why no shared DbContext will be used concurrently.
