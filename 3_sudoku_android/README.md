# Sudoku Android

A native Android Sudoku project built with Kotlin and Jetpack Compose. This project is being developed incrementally; the planned Sudoku game and solver features are not implemented yet.

## Open and run the app

1. Open this `3_sudoku_android` directory in Android Studio.
2. Let Gradle sync and install any SDK components Android Studio requests. The app is configured for compile SDK 37 and minimum SDK 24.
3. Select an Android emulator or connected device, then click **Run**.

From PowerShell in this directory, run the local unit tests or build the debug app:

```powershell
.\gradlew.bat testDebugUnitTest
.\gradlew.bat assembleDebug
```

## Development goals

1. **Manual Sudoku game:** Enter givens and player values with on-screen controls, confirm and lock the bold givens, add optional candidate notes, and verify a completed solution. Verification checks completeness first, then reports duplicate values in rows, columns, and 3×3 boxes. As part of confirming givens, reject conflicts and puzzles without exactly one solution.
2. **Automatic solver:** Add independently testable techniques that run sequentially on one shared board and show their progress.

**Stretch goal — camera clue entry:** Allow camera-based entry with user review and correction before the normal givens validation, including the uniqueness check.

Write focused unit tests as each feature is implemented, especially completeness and conflict tests alongside the Verify behavior and unique/unsolvable/ambiguous puzzle tests alongside givens validation. Cell and note input should not depend on the software keyboard.

## Project documents

- [App design](design/SODUKO_DESIGN.md) — feature scope, interaction flow, and architecture notes.
- [Phase 1 checklist](design/todo_phase_1.md) — incremental steps for the playable manual game, including tests and completion criteria.
