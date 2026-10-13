# Remaining Chess Project Work

## Current implementation status

- The chess rules are separated from the Windows Forms UI in the `Engine` namespace.
- The engine supports standard piece movement, check detection, castling, en passant, promotion, undo, and checkmate/stalemate status detection.
- The WinForms UI includes a clickable chessboard, legal-destination highlighting, current-player and game-status labels, undo/restart controls, and a pawn-promotion choice dialog.
- Five engine tests currently cover an initial pawn double move, en passant, kingside castling, Fool's Mate checkmate, and undo.
- `AiAssistedChess.csproj` copies files under `Images\**` to the output directory, and `ChessboardPanel` looks for `white_<piece>.png` and `black_<piece>.png`. Actual piece PNG availability has not been confirmed; the Unicode glyph fallback remains available.
- The pawn attack-direction change intended to fix the kingside castling test has not been verified by a test run after that change.

## Remaining work

### 1. Verify the current build and tests

- [ ] Run the engine test suite and confirm whether the kingside castling test passes after the pawn attack-direction change.
- [ ] Build the WinForms project and address any current build failures.
- [ ] Record test/build outcomes here after verification.

### 2. Strengthen chess-rule correctness and test coverage

- [ ] Add focused tests for each piece's movement, captures, and blocked paths.
- [ ] Add tests for both castling sides, castling through/into check, and castling rights after a rook or king moves or a rook is captured.
- [ ] Add tests for promotion choices and promotion undo, and for en passant expiration and undo.
- [ ] Add tests for check, checkmate, stalemate, and undo over multiple moves.
- [ ] Correct legal-move generation for a requested color: `GetAllLegalMoves(color)` currently calls `GetLegalMoves(position)`, which only generates moves for `CurrentPlayer`. Ensure status detection can assess either color correctly.
- [ ] Review board-state cloning and undo for all special-move state, including en passant targets, castling rights, captures, and promotion.

### 3. Confirm piece images and attribution

- [ ] Verify that the 12 expected PNGs exist in `Images\pieces` and are valid transparent images with the filenames expected by `ChessboardPanel`.
- [ ] If adding icons from an external collection, check its license and attribution requirements and record the source and required attribution in the `2_chess`-level README.
- [ ] Run the UI and confirm image loading works from the build output; otherwise retain the Unicode fallback without treating images as installed.

### 4. Manually exercise the application

- [ ] Play through normal moves and confirm legal-destination highlighting, turn changes, and status updates.
- [ ] Exercise castling, en passant, all promotion choices, checkmate, stalemate, undo, and restart through the UI.
- [ ] Check board rendering and interactions at the form's supported sizes, including piece contrast and selection highlights.

## Suggested order

1. Run tests and build to establish the current baseline.
2. Fix engine defects revealed by the tests and expand focused rule coverage.
3. Confirm or install the licensed piece images and attribution.
4. Manually verify complete local two-player games and UI behavior.
