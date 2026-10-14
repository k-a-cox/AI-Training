# Sudoku Android App Design

## Purpose

Build a native Android Sudoku game first, then add an automatic solver. The app is built in this directory using Kotlin and Jetpack Compose.

## Goal 1: Playable manual Sudoku game

### Enter and confirm givens

The user enters givens into an empty 9×9 board. All cell values are entered using on-screen number buttons; do not require or invoke the software keyboard. Tapping a cell selects it, and tapping a number button enters that value in the selected cell. An erase control clears the selected editable cell.

An explicit **Confirm givens** action starts play mode. Confirmation validates the givens for row, column, and 3×3-box conflicts. Once confirmed, given values appear bold and cannot be changed for that puzzle. A **Start over** action returns to puzzle entry to create a different puzzle.

### Play, notes, and verification

The player fills empty cells using the same on-screen number buttons, without using the software keyboard. A **Notes** control switches the number buttons to toggle candidate digits 1–9 in the selected empty cell. Notes are user annotations, not solver assertions, and are cleared from a cell when a value is entered.

When the user clicks **Verify**, the app first checks completeness. If any cell is empty, it asks the player to fill all cells and does not report number conflicts yet. Once all 81 cells are filled, check rows, columns, and 3×3 boxes for duplicate numbers. Identify conflicting numbers and their locations, or accept the board as solved if no conflicts exist.

## Goal 2: Automatic solver

Implement independently testable solving techniques, ordered from basic to advanced. The program iterates through the techniques sequentially on the same evolving board state and shows each deduction and its explanation. It must distinguish solved, invalid, and stuck outcomes; getting stuck with the configured techniques does not prove that a puzzle has no solution. Solver runs should be cancellable, and manual edits invalidate stale solver progress.

Keep techniques replaceable and their initial set small. A starter progression may use naked singles, hidden singles, and backtracking search. The final initial set can be adjusted during implementation.

## Stretch goals

After the playable game and automatic solver:

- Determine whether a puzzle has exactly one solution.
- Enter givens using the device camera. Scanned values must be editable and reviewed by the user before following the normal confirmation flow.

## Platform and app structure

- **Platform:** Native Android.
- **Language:** Kotlin.
- **UI:** Jetpack Compose.
- **Project root:** `3_sudoku_android/`.
- **Git branch:** `soduko`, created from `main`.

Keep the presentation, Sudoku rules, and solving techniques separate so the solver can be tested without the Android UI. A suitable initial organization is:

- **UI:** Compose screens and reusable board/cell components; render state and send user actions to the app logic.
- **Puzzle domain:** Puzzle state, cell values, clue/editability status, and rule validation.
- **Solver domain:** A common contract for individual techniques and a runner that executes them in configured order.
- **App state:** The current puzzle, selection, validation feedback, and solver progress exposed to the UI.

These are logical boundaries, not a requirement to create one Gradle module per boundary.

## Suggested user flow

1. Enter puzzle clues and confirm them.
2. Play manually, using notes as desired; verify the filled solution.
3. Optionally run a solving technique or the ordered automatic solver and inspect its progress.
4. In a later stretch-goal milestone, check uniqueness or scan clues and review them before confirmation.

## Testing and acceptance

- Write and run focused unit tests while implementing each corresponding feature; do not defer board-rule tests to the end.
- When adding the board model and cell entry, test empty-cell state, selecting/updating cells, erasing values, and protection of givens.
- When adding confirmation, test valid givens, duplicates in rows/columns/boxes, and rejection of invalid givens.
- When adding notes, test toggling candidate digits and clearing notes when a value is entered.
- When adding **Verify**, first test that any incomplete board returns an incomplete result without conflict reports. Then test full boards with row, column, and box conflicts, and a valid completed solution.
- Exercise the full Goal 1 flow in the emulator, including that on-screen controls do not open the software keyboard.
- When adding the solver, unit-test each technique and verify sequential shared-state deductions, visible explanations, and solved/stuck/invalid outcomes.
- Run the Android build, unit tests, and relevant UI tests.
