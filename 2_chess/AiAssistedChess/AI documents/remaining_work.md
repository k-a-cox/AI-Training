# Remaining Chess Project Work

## Project status

### Implemented

- A .NET 10 Windows Forms application with a separate `Engine` namespace for chess rules and board state.
- Engine representation for pieces, positions, moves, an 8x8 board, current player, move history, castling rights, and en-passant target state.
- Pseudo-legal movement generation for pawns, knights, bishops, rooks, queens, and kings, followed by filtering moves that leave the moving side's king in check.
- Special-move implementation for castling, en passant, and promotion to queen, rook, bishop, or knight.
- Check, checkmate, and stalemate status calculation, plus reset and undo operations.
- Clickable board UI with legal-destination highlights, selection, piece images, and a Unicode fallback. Piece PNGs have been confirmed working; the project copies `Images\**` into build output.
- Promotion-choice dialog, current-player and game-status labels, undo and restart buttons, and a move-replay control that can switch to manual play without resetting the current position.
- Legal move generation does not allow capturing the opposing king; the game ends by checkmate.
- The `2_chess`-level README already records the piece-image source: the Maestro BW set from Kadagaden/chess-pieces.

### Existing automated tests

The xUnit project currently has eighty-three tests. Items marked `[ ]` still need manual UI review; check each one off after confirming its setup and behavior when we are on step 2.5:

- [x] Initial pawn double move and pawn movement restrictions; user confirmed White and Black initial pawn choices and White's rejected sideways move.
- [x] Rejecting moves from empty squares, opponent pieces, and off-board destinations; blocked pawn double-step. User confirmed all four scenarios in the UI.
- [x] Restart clears selected-square and legal-move overlays; user confirmed the highlights disappear.
- [x] Undo clears selected-square and legal-move overlays; user confirmed the hints disappear.
- [x] Blocked initial rook/bishop/queen and king movement, plus knight destinations at the board edge; user confirmed the starting-position checks.
- [x] Bishop, queen, and rook movement after paths are opened; king step legality. User confirmed all four pieces look correct.
- [x] Pawn, queen, and knight captures; user confirmed the replay sequence and captures behave as expected.
- [x] White en-passant capture after undo restores the target and legal capture; user confirmed it works as expected.
- [x] Black en-passant captures from either side of the target. User confirmed both sides in the UI.
- [x] White kingside castling; user confirmed the king and rook move to their castled squares and the turn changes.
- [x] Queenside castling remains legal when only the b-file square is attacked; user confirmed the GUI scenario.
- [x] Fool's Mate checkmate detection; user confirmed pieces can still be selected, but no legal destinations are offered after checkmate.
- [x] Undo restoring a pawn to its original square; user confirmed e2-e4 returns the pawn to e2 and restores White's turn.
- [x] Legal move enumeration for a non-current player; user confirmed opponent pieces cannot be selected before their turn and can be selected after the turn changes.
- [x] En-passant expires after one reply; user confirmed the capture highlight disappears after an intervening move.
- [x] Undoing an intervening move restores the en-passant target and legal capture; user confirmed the highlight returns and capture works.
- [x] Undoing a five-move sequence containing an en-passant capture restores the captured pawn, side to move, and en-passant target at each step; user confirmed the sequence behaves correctly.
- [x] Repeated legal-move simulations preserve the live board, en-passant target, and undo history; user confirmed the board remained consistent during repeated legal-move selection and undo checks.
- [x] A king cannot move onto a rook-attacked square or next to the opposing king; user confirmed both UI checks.
- [x] A pinned rook cannot move off the file and expose its king to check; user confirmed the UI rejects the move and preserves the position.
- [x] Cloning a board creates an independent position without copying its undo history; user confirmed the test flags looked correct.
- [x] Rejecting geometrically valid en-passant attempts for both colors when the capture would expose their king to a rook attack; user confirmed the tested position and rejected move behavior in the GUI.
- [x] Undo restoring both pawns and the turn after en passant; user confirmed both pawns return and White is to move.
- [x] Rejecting kingside castling when its transit square is attacked; user confirmed White cannot castle while f1 is attacked and can castle after the bishop moves away.
- [x] White queenside castling and black kingside/queenside castling; user confirmed all three sequences.
- [x] Rejecting castling when the king is in check or the destination square is attacked; user confirmed both GUI scenarios.
- [x] Clearing castling rights after king/rook movement and a rook capture on its home square; undo restores the rights. User confirmed the tests behave as expected.
- [x] Capturing a rook on its kingside home square clears and undo restores the corresponding castling rights; user confirmed the test behaves as expected.
- [x] Undoing castling restores the king, rook, castling rights, and turn; user confirmed the GUI scenario.
- [x] Undoing a later reply and a king move restores the king's original square and both castling rights; user confirmed the sequence.
- [x] Check detection for pawn, knight, king, bishop, rook, and queen attacks, each with legal responses available; user confirmed the five playable sequences and reviewed the king test code (its adjacent-kings position cannot be loaded through the UI).
- [x] Rook-check responses resolve check, including bishop interposition; double check allows only king moves even when a rook could block one line.
- [x] A blocked rook attack line does not put the king in check; user confirmed White's rook on e1 is blocked from Black's king on e8 by the pawns on e4 and e5. Black's rooks are not involved.
- [x] Scholar's Mate checkmate detection and rejection of further moves after checkmate; user confirmed no Black piece can move in the resulting position.
- [x] Undoing checkmate and stalemate restores the prior status and allows play to continue; user confirmed both GUI scenarios.
- [x] Two mirrored corner stalemate positions, no legal moves for the side to move, and rejection of further moves after stalemate; user confirmed both GUI positions.
- [x] Pawn promotion to queen, rook, bishop, and knight for both colors; promotion capture and undo restore the pawn and captured piece. User confirmed these UI scenarios.
- [x] White pawn promotion offers Queen, Rook, Bishop, and Knight; canceling the dialog leaves the pawn unmoved. User confirmed the GUI scenario.
- [x] Black pawn promotion to queen, rook, bishop, and knight. User confirmed all four promotion choices in the UI.
- [x] Rejecting invalid promotion choices, promotion without a choice, and promotion choices on non-promotion moves; user reviewed the three code tests.
- [x] Custom position validation rejects pawns on promotion ranks, an inactive king already in check, and adjacent kings; custom position loading disables castling rights. User confirmed the GUI scenarios.
- [x] A move cannot capture the opposing king; checkmate ends the game without capturing the king. User confirmed the king-capture test by code review and Scholar's Mate behavior in the UI.

The latest test run passed all eighty-three tests.

Manual movement/capture replay sequences for the visible board:

- Basic development and sliding-piece movement: `e2-e4, e7-e5, g1-f3, b8-c6, f1-c4, g8-f6, d2-d3, f8-c5, b1-c3, d7-d6`.
- Pawn captures on d5, then a knight captures Black's queen: `e2-e4, d7-d5, e4-d5, d8-d5, b1-c3, a7-a6, c3-d5`.
- Pawn check: `d2-d4, e7-e5, a2-a3, e5-d4, a3-a4, d4-d3, h2-h3, d3-d2`.
- Knight check: `d2-d4, b8-c6, a2-a3, c6-e5, a3-a4, e5-d3`.
- Bishop check: `d2-d4, e7-e6, a2-a3, f8-b4`.
- Rook check: `e2-e4, e7-e5, d2-d4, e5-d4, e4-e5, d7-d6, e5-d6, a7-a6, g1-f3, a6-a5, f1-c4, h7-h6, e1-g1, h6-h5, f1-e1`.
- Queen check: `f2-f4, e7-e5, g1-f3, a7-a6, e2-e4, a6-a5, f1-c4, d8-h4`.

### Verified baseline

- `dotnet test .\2_chess\AiAssistedChess.Tests\AiAssistedChess.Tests.csproj --nologo --verbosity minimal` — passed: 48 passed, 0 failed.
- `dotnet build .\2_chess\AiAssistedChess\AiAssistedChess.csproj --nologo --verbosity minimal` — succeeded.
- `dotnet build .\2_chess\AiAssistedChess\AiAssistedChess.slnx --nologo --verbosity minimal` — succeeded.
- The earlier `NETSDK1137` warning was addressed by switching the app project to `Microsoft.NET.Sdk`; the app build completed without that warning.

## Remaining work

### 1. Establish a verified baseline

- [x] Run all engine tests; the corrected kingside-castling test passes.
- [x] Build the WinForms project and solution successfully.
- [x] Record the baseline test and build outcomes above.

### 2. Complete engine correctness and coverage

- [x] Add focused movement tests for every piece, covering legal moves, captures, board edges, and blocked paths.
- [x] Add check-detection tests for attacks by pawns, knights, kings, and sliding pieces, including blocked attack lines. Automated tests cover pawn/knight/king/bishop/rook/queen checks and blocked rook lines.
- [x] Fix color-specific legal-move enumeration. `GetAllLegalMoves(color)` now evaluates moves for the requested side without changing `CurrentPlayer`; a regression test verifies querying Black's moves while White is to move.
- [x] Test checkmate and stalemate with more than one position each, including positions where the side to move has no legal moves. Checkmate is covered for Fool's Mate and Scholar's Mate; two mirrored stalemate positions are covered.
- [x] Test castling for both sides and colors; reject castling through attacked transit squares, while in check, and onto an attacked destination.
- [x] Test castling rights after a king/rook moves, after a rook is captured on its original square, and after undo.
- [x] Test promotion to all four legal piece types, promotion captures, invalid promotion types, and undo after promotion.
- [x] Test en-passant availability expires after exactly one reply, undo restores the target and capture opportunity after an intervening move, reject en-passant captures that expose the moving king to a rook attack for both colors, and undo the en-passant capture.
- [x] Extend undo coverage across multiple individual moves to verify restoration of castling-right changes; tests verify king position, castling rights, captured pieces, turn, active status, and en-passant target restoration.
- [x] Prevent further moves after checkmate or stalemate; post-checkmate rejection is covered by a regression test.
- [x] Review `BoardState.Clone()` and move-history behavior; repeated legal-move simulations preserve live state, and undo still restores the previous move and special state.
- [x] Directly test that `BoardState.Clone()` is independent from the original board and starts without copied move history.
- [x] Verify pinned non-king pieces cannot move if doing so exposes their own king to check.
- [x] Verify kings cannot move onto attacked squares, including squares attacked by the opposing king.
- [x] Verify every legal response to rook check actually resolves check; test bishop interposition and confirm double check allows only king moves, even with a rook able to block one line.
- [x] Test rejecting moves from empty squares, opponent pieces, and off-board destinations without changing game state.
- [x] Test that a pawn cannot advance two squares when its intermediate square is blocked.
- [x] Test legal en-passant captures for Black and captures on both sides of the en-passant target.
- [x] Test Black pawn promotion to all four legal piece types.
- [x] Test rejecting promotion moves with a missing promotion choice and non-promotion moves with an inappropriate promotion choice.
- [x] Verify queenside castling remains legal when only the b-file square is attacked.
- [x] Test that capturing a rook on its kingside home square clears and undo restores the corresponding castling rights.
- [x] Test undoing castling restores the king, rook, castling rights, and turn.
- [x] Test undoing a move that caused checkmate or stalemate restores the prior status and allows play to continue.
- [x] Test custom-position validation for pawns on promotion ranks, an inactive king already in check, and adjacent kings; custom-position loading disables castling.
- [x] Reject moves that would capture the opposing king; checkmate ends the game without capturing the king.

### 2.5. Manually review all engine unit tests again

- [x] Complete the step 2.5 review of all listed engine-test scenarios; verify UI-representable cases in the app and review code for cases the UI cannot represent.
- [x] Record confirmations alongside each test group above, preserving prior confirmed items.

### 3. Verify and polish the UI

- [ ] Manually play legal and illegal moves; verify selection, destination highlighting, turn changes, and status updates.
- [ ] Manually verify the custom board-position GUI: load a valid position, reject invalid input without changing the board, then move/undo/restart.
- [ ] Verify illegal sequence moves stop at the failing move and manual play resumes after clearing/restarting.
- [x] Confirm “Swap to manual game” exits replay while preserving the current board and enables manual moves; confirm “Clear replay” still resets the board. User confirmed both behaviors.
- [ ] Exercise castling, en passant, all promotion choices, check, checkmate, stalemate, undo, and restart through the interface.
- [ ] Confirm promotion-dialog cancellation behaves cleanly and does not execute a move.
- [ ] Test rendering when the control is resized and verify click-to-square mapping matches the displayed board at different sizes.
- [x] Display board coordinates along all four edges; user confirmed the updated UI looks good.
- [ ] Confirm images remain correctly loaded from the build output and glyph fallback remains usable when an image is absent.
- [ ] Check disposal and file-lock behavior for loaded piece images when the board control or application closes.
- [ ] Verify board and form layout at common window sizes and check piece contrast, borders, and selected/legal-move overlays.

### 4. Finish project documentation and usability checks

- [x] Record the piece-image source in the `2_chess`-level README.
- [ ] Keep this checklist updated as verification and fixes are completed.
- [ ] Consider optional enhancements only after the required rules and UI behavior are verified: move history display, last-move highlighting, board coordinates, animations, sound, or save/load.

### Stretch goals

- [ ] Add threefold repetition draw detection.
- [ ] Add the fifty-move rule draw detection.
- [ ] Add insufficient-material draw detection.

## Suggested order

1. Add coverage for remaining castling sides/colors, promotion, and terminal-state edge cases.
2. Complete movement, check-detection, and multi-ply undo coverage.
3. Manually verify standard play, special moves, terminal states, and UI interactions.
4. Record outcomes here as work is implemented and verified; then consider optional enhancements.
