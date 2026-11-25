# Sudoku Android TODO Index

Open only the first step file whose status is not `done` (the first `todo` row in the table below). Do not open any other step files unless asked. Work steps in order.

## Directives (token economy)

- Do not describe or preview the next step's contents unless the user asks (e.g. "What's next?").
- Treat checkboxes as user-managed: assume the user checks tasks off, and do not re-verify checked items against the code or solution.
- Read only the files and line ranges the current step names. Do not open [SODUKO_DESIGN.md](../SODUKO_DESIGN.md) unless a step explicitly references it.
- Ask once, briefly. If something is ambiguous, ask one short question.
- When a step is done, collapse it to a one-line summary, as steps 1–5 already are.

## Phase 1: Playable Manual Sudoku Game

Steps 1-5 are done (no need to open them). Folder: [phase_1/](phase_1)

| Step | Status | File |
|---|---|---|
| 1. Define the board state | done | [01](phase_1/01_board_state.md) |
| 2. Display and select cells | done | [02](phase_1/02_display_select_cells.md) |
| 3. On-screen number and erase controls | done | [03](phase_1/03_number_erase_controls.md) |
| 4. Givens-entry and confirmation flow | done | [04](phase_1/04_givens_confirmation.md) |
| 5. Lock and distinguish confirmed givens | done | [05](phase_1/05_lock_givens.md) |
| 6. Candidate notes | todo | [06](phase_1/06_candidate_notes.md) |
| 7. Completeness-first verification | todo | [07](phase_1/07_completeness_verification.md) |
| 8. Check conflicts on full boards | todo | [08](phase_1/08_conflict_checks.md) |
| 9. Exercise the complete game flow | todo | [09](phase_1/09_full_game_flow.md) |
| 10. Deferred cleanup | todo | [10](phase_1/10_deferred_cleanup.md) |
| 11. Deferred: uniqueness check | deferred (needs Goal 2 solver) | [11](phase_1/11_deferred_uniqueness_check.md) |
