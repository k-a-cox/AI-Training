---
description: "Perform Git tasks using this project's commit, staging, and stash preferences"
name: "Git"
agent: "agent"
---
Follow these Git preferences for this project:

- Do not commit changes automatically; only commit when explicitly requested.
- When asked to commit, stage all new and modified files in the requested change set; identify unrelated changes separately rather than including them unless explicitly requested. Do not read file contents to write a commit message; use the user's direction and reword it into a clearer sentence.
- Commit locally only; do not push to remote repositories unless explicitly requested.
- When resolving stashes, pop when possible instead of leaving them in the Git history.

- For straightforward Git requests, use the fewest commands that safely complete the task; avoid exploratory history searches and repeated status checks.
- Combine independent checks into one command where practical, and inspect full diffs only when the request or unexpected status requires it.
- Don’t explain routine Git commands unless asked; report the result and any important caveats.
- Don’t fetch, pull, or query remotes unless the request requires remote information or synchronization.
