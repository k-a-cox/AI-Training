# Step 7: Implement completeness-first verification

- [ ] Add a **Verify** action for confirmed puzzles.
- [ ] Check for empty cells before checking for duplicates.
- [ ] If any cells are empty, prompt the player to fill the board and do not report conflicts.
- [ ] Add unit tests for incomplete boards, including a board that also contains duplicate values. The result should report incompleteness only.

**Done when:** Every incomplete board produces an incomplete result before conflict checking.
