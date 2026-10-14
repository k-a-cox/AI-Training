# Sudoku Android App Design

## Purpose

Build a native Android Sudoku game first, then add an automatic solver. The app is built in this directory using Kotlin and Jetpack Compose.

## Goal 1: Playable manual Sudoku game

### Enter and confirm givens

The user enters givens into an empty 9×9 board. All cell values are entered using on-screen number buttons; do not require or invoke the software keyboard. Tapping a cell selects it, and tapping a number button enters that value in the selected cell. An erase control clears the selected editable cell.

An explicit **Confirm givens** action validates the givens before starting play: first check for row, column, and 3×3-box conflicts, then perform the uniqueness check described below. Only accepted givens start play. Once play starts, given values appear bold and cannot be changed for that puzzle. A **Start over** action returns to puzzle entry to create a different puzzle.

#### Validate uniqueness before locking givens

As part of Goal 1's givens-confirmation flow, first reject any row, column, or 3×3-box conflicts. If there are no conflicts, determine how many valid complete solutions are consistent with the givens. Lock the givens and start play only when exactly one solution exists.

- If there are no solutions, explain that the entered puzzle cannot be solved.
- If there is more than one solution, explain that the givens do not determine a unique puzzle.
- Do not silently pick one solution to make ambiguous givens appear valid.
- For efficiency, the uniqueness check may stop counting once it finds two solutions.

Keep this validation distinct from the player's **Verify** action (which checks a filled solution) and from Goal 2's solver progress.

### Play, notes, and verification

The player fills empty cells using the same on-screen number buttons, without using the software keyboard. A **Notes** control switches the number buttons to toggle candidate digits 1–9 in the selected empty cell. Notes are user annotations, not solver assertions, and are cleared from a cell when a value is entered.

When the user clicks **Verify**, the app first checks completeness. If any cell is empty, it asks the player to fill all cells and does not report number conflicts yet. Once all 81 cells are filled, check rows, columns, and 3×3 boxes for duplicate numbers. Identify conflicting numbers and their locations, or accept the board as solved if no conflicts exist.

## Goal 2: Automatic solver

Implement independently testable solving techniques, ordered from basic to advanced. The program iterates through the techniques sequentially on the same evolving board state and shows each deduction and its explanation. It must distinguish solved, invalid, and stuck outcomes; getting stuck with the configured techniques does not prove that a puzzle has no solution. Solver runs should be cancellable, and manual edits invalidate stale solver progress.

Keep techniques replaceable and their initial set small. A starter progression may use naked singles, hidden singles, and backtracking search. The final initial set can be adjusted during implementation.

## Stretch goal: Camera clue entry

Allow a player to enter givens using the device camera. Scanned values must be editable and reviewed by the user before following the normal givens validation and confirmation flow, including the uniqueness check.

## Platform and app structure

- **Platform:** Native Android.
- **Language:** Kotlin.
- **UI:** Jetpack Compose.
- **Project root:** `3_sudoku_android/`.
- **Git branch:** `sudoku`, created from `main`.

Keep the presentation, Sudoku rules, and solving techniques separate so the solver can be tested without the Android UI. A suitable initial organization is:

- **UI:** Compose screens and reusable board/cell components; render state and send user actions to the app logic.
- **Puzzle domain:** Puzzle state, cell values, clue/editability status, and rule validation.
- **Solver domain:** A common contract for individual techniques and a runner that executes them in configured order.
- **App state:** The current puzzle, selection, validation feedback, and solver progress exposed to the UI.

These are logical boundaries, not a requirement to create one Gradle module per boundary.

## Suggested user flow

1. Enter puzzle clues and confirm them. Reject conflicting, unsolvable, or ambiguous puzzles; accept only givens with one solution.
2. Play manually, using notes as desired; verify the filled solution.
3. Optionally run a solving technique or the ordered automatic solver and inspect its progress.
4. In a later stretch-goal milestone, scan clues and review them before confirmation.

## Testing and acceptance

- Write and run focused unit tests while implementing each corresponding feature; do not defer board-rule tests to the end.
- When adding the board model and cell entry, test empty-cell state, selecting/updating cells, erasing values, and protection of givens.
- When adding confirmation, test valid givens, duplicates in rows/columns/boxes, and rejection of invalid givens.
- When adding notes, test toggling candidate digits and clearing notes when a value is entered.
- When adding **Verify**, first test that any incomplete board returns an incomplete result without conflict reports. Then test full boards with row, column, and box conflicts, and a valid completed solution.
- As part of Goal 1's givens confirmation, test puzzles with exactly one solution, no solutions, and multiple solutions. Check uniqueness only after clue conflicts are ruled out, and confirm that only a unique puzzle can be locked.
- Exercise the full Goal 1 flow in the emulator, including that on-screen controls do not open the software keyboard.
- When adding the solver, unit-test each technique and verify sequential shared-state deductions, visible explanations, and solved/stuck/invalid outcomes.
- For the camera stretch goal, verify scanned givens can be edited and reviewed before the normal validation and confirmation flow.
- Run the Android build, unit tests, and relevant UI tests.
