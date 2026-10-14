# Sudoku Android App Design

## Purpose

Build a native Android Sudoku app that lets a user enter and solve a puzzle, then watch solving techniques make progress on that same puzzle. The initial milestone focuses on validating the puzzle board and solver experience. Camera scanning is a future extension, not part of the initial implementation.

## Initial scope

- Enter a puzzle into a 9×9 board by hand.
- Edit given clues and empty cells while setting up a puzzle.
- Solve the puzzle manually by entering values in empty cells.
- Run an individual solving technique to see its deductions and board changes.
- Run all configured techniques in a basic-to-advanced sequence over one shared puzzle state.
- Show useful feedback for invalid entries, completed puzzles, and puzzles that the configured techniques cannot currently advance.

Not in the initial milestone:

- Camera capture, image recognition, or OCR.
- Puzzle generation, accounts, cloud sync, or leaderboards.
- Concurrent solvers operating on independent copies of a puzzle.

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

## Puzzle entry and manual play

Show a 9×9 grid with clear selection and visual distinction between original clues and user-entered values. In puzzle-entry mode, the user can fill, replace, and clear cells. In play mode, original clues are read-only while empty cells remain editable. Provide an explicit way to switch between entry and play so setup edits are not confused with solving.

Validate entered values against Sudoku's row, column, and 3×3-box constraints. Report conflicts to the user and prevent invalid manual values from being treated as valid solver input. A board with empty cells is an incomplete puzzle, not an error. A completed board is solved only when it satisfies all Sudoku constraints.

The app should preserve the current puzzle when switching between manual play and solver actions. Editing the puzzle after deductions have been made must invalidate or reset stale solver progress rather than presenting it as progress for the changed board.

## Solver contract and execution

Keep the initial set and exact ordering of techniques open until implementation. Select a small, testable starter set then; the architecture should allow techniques to be added, removed, and reordered without changing the board UI.

Each technique should operate on the current puzzle state and report its result explicitly: deductions made, no further progress from that technique, a solved puzzle, or an invalid/contradictory state. A deduction should identify the cell and value and provide a concise explanation suitable for display. Techniques should only place values they can justify under their own rules.

- **Run technique:** Apply the selected technique to the current puzzle and show its deductions.
- **Run all:** Visit the configured techniques in basic-to-advanced order, applying each one's deductions to the same evolving puzzle state. The next technique sees the values placed by earlier techniques.
- **Progress visibility:** Show the active technique, its current deduction or explanation, and the board changes as they occur. Keep the run observable rather than silently replacing the puzzle with a final answer.
- **No progress:** If a technique cannot advance the board, continue to the next technique during a run-all operation. If the full configured pass cannot advance an unsolved puzzle, tell the user that these techniques are stuck; do not claim that the puzzle is impossible.
- **Stop conditions:** Stop when the puzzle is solved, a contradiction or invalid state is detected, or the user cancels the run. Manual edits during a run should cancel or invalidate that run.

The execution is sequential, not concurrent: “run all” means an ordered pass through techniques sharing one puzzle state, not a race between solvers or a merged result from separate boards. Keep solver execution off the UI thread and publish progress back to the UI so the board remains responsive.

## Suggested user flow

1. Start a new puzzle and enter givens into the empty 9×9 board.
2. Review and correct the clues; resolve any displayed row, column, or box conflicts.
3. Switch to play mode and fill empty cells manually, or choose a solving technique.
4. Run one technique to inspect its deductions, or run all techniques in order.
5. Review each visible change and explanation; continue manually or rerun after the techniques get stuck.
6. Receive a clear completion state when the board is valid and full.

## Future camera-scanning extension

Add camera scanning as a separate puzzle-entry path after the manual board and solver work. A future scan flow should capture or import an image, detect the grid and digits, populate an editable review board, and require the user to correct recognition errors before submitting it as a puzzle. Image recognition must not bypass the same board validation used by manual entry. Request camera permission only when that feature is implemented and invoked.

## Testing and acceptance

- Unit-test Sudoku row, column, and 3×3-box validation, including conflicts and incomplete boards.
- Unit-test each solving technique with small known board states and verify both deductions and no-progress results.
- Unit-test the run-all ordering and shared-state behavior: deductions by an earlier technique are visible to later techniques.
- Verify solved, stuck, invalid/contradictory, and cancellation outcomes are distinguishable.
- Verify manual edits after solver progress invalidate stale progress.
- Exercise puzzle entry, manual play, individual technique execution, run-all progress, and completion through the Android UI.

Build and run the Android checks once the required Gradle and Android SDK tooling is available in the development environment.
