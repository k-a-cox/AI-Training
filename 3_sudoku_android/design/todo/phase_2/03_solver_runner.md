# Step 3: Add the ordered solver runner

- [ ] Run configured techniques in order against one evolving solver state.
- [ ] Continue from each deduction using the updated state, and record progress in an ordered form the UI can present.
- [ ] Stop with a solved outcome when the board is complete and valid, or a stuck outcome when no configured technique can make progress.
- [ ] Add unit tests for technique ordering, shared-state deductions, progress explanations, and solved and stuck outcomes.

**Done when:** The runner applies configured techniques sequentially to shared state and returns tested outcomes with inspectable progress.
