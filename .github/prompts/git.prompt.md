---
description: "Perform Git tasks using this project's commit, staging, and stash preferences"
name: "Git"
agent: "agent"
---
Follow these Git preferences for this project:

- Commit only when explicitly requested.
- When the user requests a commit and changes are staged, commit exactly what is staged without inspecting file contents or staging additional files. If nothing is staged, stage only the files in the requested change set.
- Base the commit message on the user’s direction; don’t inspect file contents solely to write it.
- Commit locally; do not push unless explicitly requested.
- When restoring a stash, prefer git stash pop. If conflicts occur, resolve and verify the restored changes before dropping the stash.
- Use the fewest commands that safely complete straightforward Git requests; avoid unnecessary history searches and repeated status checks.
- Combine independent checks into a single command, or run them in parallel; inspect full diffs only when needed.
- Do not query or synchronize with remotes unless the request requires it.

- Use git commit -m "<message>" directly. Do not run git status, git diff or git log beforehand unless the request is ambiguous.
- Match the repository's commit style only if the user mentions it. Do not read git log to infer it.
- When inspection is needed, use compact output: git status -sb, git diff --stat, git --no-pager log --oneline -n 5.
- Always pass --no-pager and avoid commands that produce large output, such as full log, show, or diff without a path.
- After committing, reply with only the short hash and subject. Do not re-run git status or git log to verify.

