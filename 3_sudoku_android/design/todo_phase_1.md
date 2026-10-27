# Phase 1: Playable Manual Sudoku Game

Work through the steps in order. Add and run focused unit tests as each model or rule is introduced; do not defer completeness and conflict tests until the end.

## 1. Define the board state

- [x] Represent the 81 cell values, which cells are givens, and the selected cell.
- [x] Represent candidate notes separately from cell values.
- [x] Add unit tests for an empty board and basic cell-value state.

**Done when:** The board model can be created and inspected in a unit test.

## 2. Display and select cells

- [x] Display the 9×9 board.
- [x] Visually distinguish the 3×3 boxes from individual cells, for example with stronger separators after every third row and column.
- [x] Make cells selectable and visibly indicate the selection.
- [x] Convert the selected cell to a row and column consistently.
- [ ] Add UI tests for selecting cells, if supported by the current test setup.

**Done when:** Tapping cells selects the intended cell, and the nine 3×3 boxes are easy to distinguish.

## 3. Add on-screen number and erase controls

- [x] Add on-screen number buttons for entering digits 1–9.
- [x] Add an on-screen erase control for the selected editable cell.
- [x] Do not require or invoke the software keyboard for cell input.
- [x] Add unit tests for entering and erasing values in the board model.

**Done when:** Values can be entered and corrected using only on-screen controls.

## 4. Add givens-entry and confirmation flow

- [x] Let the player enter initial clues into the empty board.
- [x] Add an explicit **Confirm givens** action.
- [x] Validate givens against row, column, and 3×3-box rules before confirming.
- [x] Add unit tests for valid givens and duplicate clues in rows, columns, and boxes. Invalid givens must not be confirmed.
The uniqueness check (counting solutions, stopping at two) is deferred until the backtracking solver exists; see [Deferred: uniqueness check](#deferred-uniqueness-check) below. Showing why givens were rejected is deferred to the conflict highlighting in [Step 8](#8-check-conflicts-on-full-boards), which also applies to rejected givens.

**Done when:** Only conflict-free givens can be confirmed; rejected givens remain editable.

## 5. Lock and distinguish confirmed givens

- [x] Display confirmed given values in bold.
- [x] Make confirmed givens read-only during play.
- [x] Keep non-given cells editable.
- [x] Add a **Start over** action to clear the puzzle and enter new givens.
- [x] Add unit tests for clue locking and start-over state.
- [ ] Complete and run the deferred `GivensValidatorTest` cases for givens validation and locking.

**Done when:** On-screen number and erase controls cannot change a confirmed given.

## 6. Add candidate notes

- [ ] Add an on-screen **Notes** toggle that changes the number buttons to toggle candidate digits 1–9.
- [ ] Allow notes only in empty, non-given cells.
- [ ] Show notes within their selected cell.
- [ ] Clear a cell's notes when entering a value there.
- [ ] Revisit erase behavior: currently `setValue(null)` also clears notes. Decide whether erase should clear notes only when a cell has notes and no value, and update the Notes-mode controls accordingly.
- [ ] Add unit tests for toggling notes, note eligibility, and clearing notes on value entry.

**Done when:** Candidate notes can be entered and edited using on-screen controls, without the keyboard.

## 7. Implement completeness-first verification

- [ ] Add a **Verify** action for confirmed puzzles.
- [ ] Check for empty cells before checking for duplicates.
- [ ] If any cells are empty, prompt the player to fill the board and do not report conflicts.
- [ ] Add unit tests for incomplete boards, including a board that also contains duplicate values. The result should report incompleteness only.

**Done when:** Every incomplete board produces an incomplete result before conflict checking.

## 8. Check conflicts on full boards

- [ ] For a full board, report duplicates in each affected row, column, and 3×3 box.
- [ ] Identify the conflicting numbers and cell locations, returned as cell indexes.
- [ ] Highlight the conflicting cells on the board, both when Verify finds conflicts and when Confirm givens rejects conflicting givens.
- [ ] Accept a full board as solved only when it has no row, column, or box conflicts.
- [ ] Add unit tests for row conflicts, column conflicts, box conflicts, the reported conflicting cell indexes, and a valid completed board.

**Done when:** Full invalid boards identify and highlight their conflicts and a valid full board is accepted.

## 9. Exercise the complete game flow

- [ ] In the emulator, enter givens, confirm them, and verify they are bold and locked.
- [ ] Solve cells manually with on-screen controls and use notes where desired.
- [ ] Verify that an incomplete board asks for completion before conflict feedback.
- [ ] Verify that a complete invalid board shows conflicts and a correct solution is accepted.
- [ ] Check that cell and note input never opens or requires the software keyboard.
- [ ] Check that the 3×3 box boundaries are clear on the emulator.
- [ ] Run the Android build and all unit tests created in earlier steps.

**Done when:** The manual game works end-to-end on the emulator and its model/rule tests pass.

## Deferred: uniqueness check

Do this after the backtracking solver from Goal 2 exists, since it reuses that solver.

- [ ] After clue conflicts are ruled out, count valid complete solutions, stopping once two are found.
- [ ] Accept and lock givens only when exactly one solution exists; explain and reject puzzles with no solution or multiple solutions.
- [ ] Add unit tests for unique, unsolvable, and multiple-solution givens (see `PuzzleSampleBoards`), and verify that only a unique puzzle can be confirmed.

**Done when:** Only conflict-free givens with exactly one solution can be confirmed; rejected givens remain editable.
