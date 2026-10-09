# Prompt Summary

This file summarizes the requests made while working on the Personal Expense Tracker assignment.

## Learning Python basics

- Asked for a simple input/output function that collects two values.
- Asked what a Python `main()` function looks like.
- Asked for an example Python script with good structure.
- Asked how to store the results of multiple calls to the input function.
- Asked how to run a Python script.
- Asked how to find the largest value in a list.

## Building the expense tracker

- Requested a beginner Python homework assignment, saved in the `Python/Assignment 1` folder.
- Asked to refactor the expense-collection loop into `collectExpenses`.
- Asked to move the printed summary into a `summaryReport` function.
- Asked for a test mode using sample inputs, so values would not have to be typed by hand for every run, and how to call it.
- Asked whether invalid text for an amount should cause the program to prompt for the amount again.
- Asked whether to truncate amounts on input or format them to two decimal places on output.
- Requested two-decimal currency formatting for the total.
- Asked what assignment requirements were still missing and whether the assignment was complete.
- Asked to add a name and summary to the assignment, then clarified that they should go in the Python file instead.
- Asked for an alternative implementation in a separate file named `ai_expense_tracker.py`.
- Asked for the key differences between the original and alternative scripts.

## Git commands

The work was committed in four focused commits. From the repository root, the commands were:

```powershell
git add -- "Python/Assignment 1/assignment.md"
git commit -m "Add personal expense tracker assignment brief"

git add -- "Python/Assignment 1/expense_tracker.py"
git commit -m "Implement interactive expense tracker"

git add -- "Python/Assignment 1/ai_expense_tracker.py"
git commit -m "Add reference expense tracker implementation"

git add -- "Python/Assignment 1/prompt_summary.md"
git commit -m "Document Python assignment prompt summary"
```

The commits also include the Copilot co-author trailer:

```text
Co-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>
```

## Git history rewrite

- Rewrote the local `master` history so the assignment files use `Assignment 1 (python)` instead of `Python/Assignment 1`.
- Used `git filter-branch --tree-filter` in a temporary clone to move the files in each commit, then updated the local `master` ref.
- The rewritten commits have new hashes. The new `master` tip is `2ce6bb9` (`Add C# and Java expense tracker samples`).
- Verified that no `Python/` paths remain in `master` history and that the working tree is clean.
- No remote was configured, so the rewritten history was not pushed. VS Code internal checkpoint refs were left unchanged.
