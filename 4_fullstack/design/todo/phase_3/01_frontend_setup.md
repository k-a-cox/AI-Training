# 3.1 Set Up React and MSW

## Prerequisites

Task 1.5 and explicit frontend scaffolding/dependency approval. Can overlap Phase 2 if selected by the learner.

## Learning Goal

Establish a frontend development loop independent of the running backend.

## Allowed Scope

`react/` project/configuration, dependencies, mock handlers, safe environment examples, and requested VS Code extensions. No feature screens yet.

## Actions

- [ ] Verify the approved frontend host and supported Node LTS; scaffold React/TypeScript/Vite.
- [ ] Add Sass, ESLint, Prettier, and test tooling; commit npm lockfile.
- [ ] Add MSW handlers based on shared DTO/error fixtures, including empty and conflict responses.
- [ ] Fail unhandled requests and make mock startup Development/Test-only.
- [ ] Configure a real API base URL separately; never silently fall back to MSW.
- [ ] Install ESLint/Prettier VS Code extensions only when requested; consider SCSS Stylelint and MSSQL optional.

## Verification and Acceptance

When requested, verify mock startup, one success/error request, and frontend build/lint. No SQL dependency in mock mode; no secrets in frontend environment variables, which are delivered to the browser.
