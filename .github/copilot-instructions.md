# Copilot Instructions

## Project Guidelines
- When repeated test-position setup exists, prefer a genuinely generic MakeBoard helper accepting a sequence of piece/square pairs (and side to move), rather than a helper with arguments hard-coded to a specific position or duplicating setup.
- Do not restart a full second-pass manual UI review of all engine tests after each incremental test change. Wait until the user says the final engine tests are finished, then loop back across the complete test suite and perform the requested manual UI sanity-check handoffs.
- For step 2.5 manual unit-test review, preserve existing [x] confirmations as already verified; only guide the user through unchecked [ ] test groups and mark them as confirmed when the user reports them. After the user confirms a test or group of tests, immediately provide the next unchecked test from remaining_work.md, with the exact unit-test move sequence when available.
- Prefer plain-language chess terminology in explanations and project checklists; use “individual move(s)” instead of the technical term “ply/plies.”
- Use the board-state loading syntax already established in this project consistently; do not switch or invent alternate notation in explanations/tests. Preserve the user's chosen syntax unless they explicitly request a change.
- When adding engine tests, automatically guide the user through the corresponding manual GUI visual-confirmation scenarios, without requiring them to ask; preserve established custom-position loading syntax exactly.
- When explaining chess undo sequences, track the side to move accurately: undoing a move restores the turn to the player who made that move.

## AI-Assisted Workflow Documentation
- Keep the README section documenting how GPT models were used to complete the chess project; this AI-assisted workflow history is important repository documentation and should not be removed during documentation cleanup. Specifically, note that GPT-6 Luna in Agent mode was used primarily, with some Plan mode usage.
