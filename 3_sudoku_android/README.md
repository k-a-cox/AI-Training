# Sudoku Android

A native Android Sudoku project built with Kotlin and Jetpack Compose. The manual Sudoku workflow is playable from entering givens through verifying a solution. The automatic solver and camera entry are not yet implemented.

## Playable manual game

- Enter givens with the on-screen keypad. Confirming givens rejects an empty board or a row, column, or box conflict; accepted givens are bold and locked.
- Enter and erase values, use candidate notes, and start over to enter a different puzzle. The 3×3 boxes are visually distinct.
- Verify checks completeness first. A full invalid board identifies an invalid row, column, or box; a valid solution is accepted and can be reviewed or followed by a new game.

The full manual flow has been exercised in the emulator, and the Android build and existing unit tests have been run.

## Open and run the app

1. Open this `3_sudoku_android` directory in Android Studio.
2. Let Gradle sync and install any SDK components Android Studio requests. The app is configured for compile SDK 37 and minimum SDK 24.
3. Select an Android emulator or connected device, then click **Run**.

From PowerShell in this directory, run the local unit tests or build the debug app:

```powershell
.\gradlew.bat testDebugUnitTest
.\gradlew.bat assembleDebug
```

## Remaining work

- **Manual game improvements:** Report conflicting values and cell locations and highlight them for both verification and rejected givens; add deferred validator and UI tests; and polish the UI, especially the Notes keypad state.
- **Givens uniqueness:** Once the backtracking solver exists, reject puzzles with no solution or multiple solutions. Until then, conflict-free givens can still describe an unsolvable or ambiguous puzzle.
- **Automatic solver:** Add independently testable techniques that run sequentially on one shared board and show their progress.

**Stretch goal — camera clue entry:** Allow camera-based entry with user review and correction before the normal givens validation, including the uniqueness check.

See the [TODO index](design/todo/index.md) for the remaining tasks, dependencies, and acceptance criteria.

## AI-assisted development

GPT models are being used primarily as an interactive development assistant, not to automate the project end to end. GPT-6 Luna in Agent mode has been used most often, with some Plan mode usage. Claude Sonnet 5.5 also helped update the UI and generate sample boards for testing. The models help refine the overall design and patterns, explain options, review focused changes, and suggest tests or next steps. Implementation remains locally driven; the playable manual workflow has been exercised in the emulator and checked with a build and existing unit tests.

## Project documents

- [App design](design/SODUKO_DESIGN.md) — feature scope, interaction flow, and architecture notes.
- [TODO index](design/todo/index.md) — incremental steps for the manual game, automatic solver, and stretch camera entry, including tests and completion criteria.
