# Step 4: Implement backtracking search

- [ ] Implement backtracking search as an independently testable solver strategy that finds a valid completion or determines that none exists.
- [ ] Support counting solutions up to a caller-provided limit so the deferred givens uniqueness check can stop after finding two.
- [ ] Keep search state isolated so failed branches do not corrupt the current puzzle or solver progress.
- [ ] Report search progress in a way that distinguishes trial branches and reversals from logical deductions.
- [ ] Return solved for a valid completion and invalid when the input has no valid completion; do not report stuck as proof that no solution exists.
- [ ] Add unit tests for solvable, unsolvable, and multiple-solution puzzles, bounded solution counting, preserved input state, and search branch restoration.

**Done when:** Backtracking reliably distinguishes solvable and unsolvable inputs, can count solutions up to a limit, preserves the source board, and exposes understandable progress.
