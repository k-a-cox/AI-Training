# S.1 Add Authentication and API Policies

## Prerequisites

Core acceptance and separate security-scope approval.

## Learning Goal

Practice framework authentication, policy authorization, and rate limiting safely.

## Allowed Scope

Selected maintained identity provider/framework, API policies, client sign-in integration, and requested security tests.

## Actions

- [ ] Agree a supported provider and viewer/admin permissions; never invent password hashing or token validation.
- [ ] Protect reads/mutations consistently and add suitable rate limits.
- [ ] Keep credentials/tokens out of logs, source, and fixtures.
- [ ] Test unauthorized/forbidden requests, role changes, expiry, and throttling.

## Verification and Acceptance

Run approved authorization tests and demonstrate denied access as well as successful sign-in. Public deployment needs a separate readiness decision.
