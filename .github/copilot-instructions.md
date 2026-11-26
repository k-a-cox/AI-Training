# Copilot Instructions

## Learner-Led Development
- The user leads development. Explain, plan, or review by default; do not change code, scaffold projects, install tools, refactor, or write/run tests unless explicitly requested.
- Authorization is scoped to the requested task, not the entire roadmap. A task checklist is not permission to execute its steps or verification commands.
- The user owns task status and acceptance. Do not check off tasks or re-verify completed work unless asked.
- Do not commit, push, or create branches unless explicitly requested.
- For general requests to start or continue an indexed project, work only on the current eligible task. If it is blocked, explain the prerequisite; do not skip ahead without approval.
- Read the current task and the nearby files it needs; do not implement future tasks or perform unrelated cleanup.

## Project Documentation
- Batch design and README updates at the initial planning capture, meaningful milestones, or explicit documentation requests. Small implementation changes do not authorize documentation churn.
- When updating or refining documentation for a project folder, review the top-level `README.md` and update its matching project description, documentation links, and AI-assisted development summary so they remain accurate.

## Fullstack Setup
- SQL Server and Visual Studio setup for `4_fullstack` belongs on a different Windows machine. Verify the intended host's identity, architecture, resources, permissions, and existing tools before any installation. Never assume the current workspace host is the target.
- Keep .NET mock adapters and frontend MSW mocks explicit and development/test-only. Real integration must use the real API and SQL Server without mock fallback.
