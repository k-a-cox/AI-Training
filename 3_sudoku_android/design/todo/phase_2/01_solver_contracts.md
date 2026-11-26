# Step 1: Define solver state and contracts

- [ ] Define a solver-domain board/state that can be tested without Android UI dependencies and does not mutate the manually played puzzle.
- [ ] Define solver outcomes for solved, invalid, and stuck, including what makes an input invalid versus a solver stuck with its configured techniques.
- [ ] Define a common technique contract that operates on the evolving solver state and reports a single placement or no deduction, with an explanation for each placement.
- [ ] Add focused unit tests for solver-state copying, outcome semantics, and technique results.

**Done when:** Solver state and technique results are independently testable and keep solver progress separate from the player's puzzle.
