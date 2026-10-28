# Step 11: Deferred: uniqueness check

Do this after the backtracking solver from Goal 2 exists, since it reuses that solver.

- [ ] After clue conflicts are ruled out, count valid complete solutions, stopping once two are found.
- [ ] Accept and lock givens only when exactly one solution exists; explain and reject puzzles with no solution or multiple solutions.
- [ ] Add unit tests for unique, unsolvable, and multiple-solution givens (see `PuzzleSampleBoards`), and verify that only a unique puzzle can be confirmed.

**Done when:** Only conflict-free givens with exactly one solution can be confirmed; rejected givens remain editable.
