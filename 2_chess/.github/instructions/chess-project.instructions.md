---
applyTo: '2_chess/**'
---

# Chess project instructions

- Keep `2_chess/AiAssistedChess/AI documents/remaining_work.md` current as project work is implemented and verified.
- Treat `remaining_work.md` as the authoritative project status: record verified results, distinguish completed work from optional future enhancements, and remove stale plans or test counts when updating it.
- Check off a task only after implementation and relevant tests or build/manual verification have succeeded. Update the checklist with newly discovered work when appropriate.
- Preserve the established custom-position syntax in documentation and examples unless the user explicitly requests a format change. Use the board-state loading syntax consistently; do not switch or invent alternate notation in explanations or tests.
- When repeated test-position setup exists, prefer a genuinely generic MakeBoard helper accepting a sequence of piece/square pairs (and side to move), rather than a helper with arguments hard-coded to a specific position or duplicating setup.
- Do not restart a full second-pass manual UI review of all engine tests after each incremental test change. Wait until the user says the final engine tests are finished, then loop back across the complete test suite and perform the requested manual UI sanity-check handoffs.
- For step 2.5 manual unit-test review, preserve existing [x] confirmations as already verified; only guide the user through unchecked [ ] test groups and mark them as confirmed when the user reports them. After the user confirms a test or group of tests, immediately provide the next unchecked test from remaining_work.md, with the exact unit-test move sequence when available.
- Prefer plain-language chess terminology in explanations and project checklists; use “individual move(s)” instead of the technical term “ply/plies.”
- When adding engine tests, automatically guide the user through the corresponding manual GUI visual-confirmation scenarios, without requiring them to ask.
- When explaining chess undo sequences, track the side to move accurately: undoing a move restores the turn to the player who made that move.
- Keep the README section documenting how GPT models were used to complete the chess project; this AI-assisted workflow history is important repository documentation and should not be removed during documentation cleanup. Specifically, note that GPT-6 Luna in Agent mode was used primarily, with some Plan mode usage.
- Follow the repository's general Copilot instructions for coding style and Git workflow.
