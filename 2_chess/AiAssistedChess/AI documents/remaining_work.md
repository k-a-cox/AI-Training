# Remaining Chess Project Work

## Project status

### Implemented

- A .NET 10 Windows Forms application with a separate `Engine` namespace for chess rules and board state.
- Engine representation for pieces, positions, moves, an 8x8 board, current player, move history, castling rights, and en-passant target state.
- Pseudo-legal movement generation for pawns, knights, bishops, rooks, queens, and kings, followed by filtering moves that leave the moving side's king in check.
- Special-move implementation for castling, en passant, and promotion to queen, rook, bishop, or knight.
- Check, checkmate, and stalemate status calculation, plus reset and undo operations.
- Clickable board UI with legal-destination highlights, selection, piece images, and a Unicode fallback. Piece PNGs have been confirmed working; the project copies `Images\**` into build output.
- Promotion-choice dialog, current-player and game-status labels, undo button, and restart button.
- The `2_chess`-level README already records the piece-image source: the Maestro BW set from Kadagaden/chess-pieces.

### Existing automated tests

The xUnit project currently has forty-eight tests:

- Initial pawn double move and pawn movement restrictions.
- Blocked initial rook/bishop/queen movement and knight destinations at the board edge.
- Bishop, queen, and rook movement after paths are opened; king step legality.
- Pawn, queen, and knight captures.
- En-passant capture.
- White kingside castling.
- Fool's Mate checkmate detection.
- Undo restoring a pawn to its original square.
- Legal move enumeration for a non-current player.
- En-passant expiration after one reply.
- Undo restoring both pawns and the turn after en passant.
- Rejecting kingside castling when its transit square is attacked.
- White queenside castling and black kingside/queenside castling.
- Rejecting castling when the king is in check or the destination square is attacked.
- Clearing castling rights after king/rook movement and a rook capture on its home square; undo restores the rights.
- Check detection for pawn, knight, bishop, rook, and queen attacks, each with legal responses available.
- A blocked rook attack line does not put the king in check.
- Scholar's Mate checkmate detection and rejection of further moves after checkmate.
- Two mirrored corner stalemate positions, no legal moves for the side to move, and rejection of further moves after stalemate.
- Pawn promotion to queen, rook, bishop, and knight; promotion capture and undo; invalid promotion types leave the position unchanged.

The original castling test setup was corrected to move e2 to e4 before developing the f1 bishop. The latest test run passed all forty-eight tests.

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
- [ ] Add check-detection tests for attacks by pawns, knights, kings, and sliding pieces, including blocked attack lines. Automated tests cover pawn/knight/bishop/rook/queen checks and blocked rook lines; king attack still needs a focused test position.
- [x] Fix color-specific legal-move enumeration. `GetAllLegalMoves(color)` now evaluates moves for the requested side without changing `CurrentPlayer`; a regression test verifies querying Black's moves while White is to move.
- [x] Test checkmate and stalemate with more than one position each, including positions where the side to move has no legal moves. Checkmate is covered for Fool's Mate and Scholar's Mate; two mirrored stalemate positions are covered.
- [x] Test castling for both sides and colors; reject castling through attacked transit squares, while in check, and onto an attacked destination.
- [x] Test castling rights after a king/rook moves, after a rook is captured on its original square, and after undo.
- [x] Test promotion to all four legal piece types, promotion captures, invalid promotion types, and undo after promotion.
- [x] Test en-passant availability expires after exactly one reply and undo restores the en-passant capture.
- [ ] Test additional invalid en-passant attempts and en-passant undo state after other move sequences.
- [ ] Test undo over multiple plies and verify restoration of captures, turn, castling rights, en-passant state, and game status.
- [x] Prevent further moves after checkmate or stalemate; post-checkmate rejection is covered by a regression test.
- [ ] Review `BoardState.Clone()` and move-history behavior to ensure simulations cannot mutate live game state and undo restores all special-move state.

### 3. Verify and polish the UI

- [ ] Manually play legal and illegal moves; verify selection, destination highlighting, turn changes, and status updates.
- [x] Manually replay pasted coordinate castling test sequences using Load, Next, and Back; verified the sequences work on the visible board.
- [x] Manually replay legal check sequences for pawn, knight, bishop, rook, and queen attacks; each sequence reaches check while legal moves remain.
- [ ] Manually verify the custom board-position GUI: load a valid position, reject invalid input without changing the board, then move/undo/restart.
- [ ] Verify illegal sequence moves stop at the failing move and manual play resumes after clearing/restarting.
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

## Suggested order

1. Add coverage for remaining castling sides/colors, promotion, and terminal-state edge cases.
2. Complete movement, check-detection, and multi-ply undo coverage.
3. Manually verify standard play, special moves, terminal states, and UI interactions.
4. Record outcomes here as work is implemented and verified; then consider optional enhancements.
