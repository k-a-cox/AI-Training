# Sudoku Android TODO Index

Open only the first step file whose status is not `done` (the first `todo` row in the table below). Do not open any other step files unless asked. Work steps in order.

## Directives (token economy)

- Do not describe or preview the next step's contents unless the user asks (e.g. "What's next?").
- Treat checkboxes as user-managed: assume the user checks tasks off, and do not re-verify checked items against the code or solution.
- Read only the files and line ranges the current step names. Do not open [SODUKO_DESIGN.md](../SODUKO_DESIGN.md) unless a step explicitly references it.
- Ask once, briefly. If something is ambiguous, ask one short question.
- When a phase is done, collapse the tasks to one-line summaries while updating documentation.

## Phase 1: Playable Manual Sudoku Game

Folder: [phase_1/](phase_1)

| Step | Status | File |
|---|---|---|
| 1. Define the board state | done | [01](phase_1/01_board_state.md) |
| 2. Display and select cells | done | [02](phase_1/02_display_select_cells.md) |
| 3. On-screen number and erase controls | done | [03](phase_1/03_number_erase_controls.md) |
| 4. Givens-entry and confirmation flow | done | [04](phase_1/04_givens_confirmation.md) |
| 5. Lock and distinguish confirmed givens | done | [05](phase_1/05_lock_givens.md) |
| 6. Candidate notes | done | [06](phase_1/06_candidate_notes.md) |
| 7. Completeness-first verification | done | [07](phase_1/07_completeness_verification.md) |
| 8. Exercise the complete game flow | done | [08](phase_1/08_full_game_flow.md) |

## Phase 1.5: Manual Game Improvements

Folder: [phase_1_5/](phase_1_5)

| Step | Status | File |
|---|---|---|
| 1. Check conflicts on full boards | todo | [01](phase_1_5/01_conflict_checks.md) |
| 2. Deferred cleanup | todo | [02](phase_1_5/02_deferred_cleanup.md) |
| 3. Deferred: uniqueness check | deferred (needs Goal 2 solver) | [03](phase_1_5/03_deferred_uniqueness_check.md) |

## Phase 2: Automatic Solver

Folder: [phase_2/](phase_2)

Start after Phase 1.5 steps 1 and 2. Phase 1.5 step 3 remains deferred until the backtracking search in Phase 2, Step 4 is complete.

| Step | Status | File |
|---|---|---|
| 1. Define solver state and contracts | todo | [01](phase_2/01_solver_contracts.md) |
| 2. Implement logical solving techniques | todo | [02](phase_2/02_logical_techniques.md) |
| 3. Add the ordered solver runner | todo | [03](phase_2/03_solver_runner.md) |
| 4. Implement backtracking search | todo | [04](phase_2/04_backtracking_search.md) |
| 5. Integrate solver progress and controls | todo | [05](phase_2/05_solver_ui.md) |
| 6. Exercise the solver end to end | todo | [06](phase_2/06_solver_acceptance.md) |
