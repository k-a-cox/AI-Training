# AI-Training

A collection of programming assignments and projects developed with AI assistance.

## Projects

### [1_python_homework](1_python_homework/)

Personal expense tracker assignment, with Python, C#, and Java examples.
See the [project README](1_python_homework/README.md) for details and instructions.

**AI-assisted development:** VS Code Copilot was used to help generate the assignment and write the expense-tracker code. The implementation was tested with valid, invalid, and edge-case inputs and compared with the assignment requirements.

### [2_chess](2_chess/)

A local two-player chess game for Windows, built with C# and Windows Forms.
See the [project README](2_chess/README.md) for features, run instructions, and project status.

**AI-assisted development:** GPT models were used to plan and implement the application, primarily GPT-6 Luna in Agent mode, with some Plan mode usage. The workflow is to prompt for next steps, have the model generate the development plan and changes, sanity-check those changes, and then ask GPT to make the commits.

### [3_sudoku_android](3_sudoku_android/)

A native Android Sudoku app built with Kotlin and Jetpack Compose. Planned features include a manual Sudoku game with on-screen entry, givens validation, candidate notes, and solution verification; a sequential solver that shows its progress; and camera-based clue entry as a stretch goal.

See the [project README](3_sudoku_android/README.md) for setup and run instructions, the [Sudoku design document](3_sudoku_android/design/SODUKO_DESIGN.md) for scope and architecture, and the [Phase 1 checklist](3_sudoku_android/design/todo_phase_1.md) for implementation progress.

**AI-assisted development:** GPT models are used primarily as interactive assistants for local development, helping refine the overall design and patterns, explain options, review focused changes, and suggest tests or next steps. Development is not fully automated: changes are implemented and reviewed locally, then validated with the build, unit tests, and Android emulator as appropriate. GPT-6 Luna in Agent mode has been used most often, with some Plan mode usage.
