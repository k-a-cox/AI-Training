# Chess Game Design Record

## Purpose

Build a local two-player chess game for people sharing one Windows computer. Keep chess rules separate from the graphical interface so the engine can be tested independently.

## Technology and scope

- C# with Windows Forms, targeting .NET 10 on Windows.
- A separate `AiAssistedChess.Engine` namespace for board state, move validation, and game status.
- Local, alternating two-player play; no computer opponent, online play, clocks, or accounts.
- Standard chess movement and rules, including check, checkmate, stalemate, castling, en passant, and promotion.
- A graphical board with legal-destination highlighting, current-player and status indicators, undo, and restart.

## Design decisions

- Represent pieces by color and type, and board squares as positions—not screen coordinates.
- Keep board-to-screen coordinate conversion in the UI control.
- Filter pseudo-legal moves so a move cannot leave the moving side's king in check.
- Require a player choice for pawn promotion among queen, rook, bishop, and knight.
- Test the engine independently from Windows Forms event handling.

## Implemented additions

Features added beyond the initial scope:

- Load custom board positions using the established text format. Positions require exactly one king per color and a side to move; invalid squares, duplicate squares, pawns on promotion ranks, adjacent kings, and positions where the inactive side is in check are rejected. Custom positions disable castling rights because the format does not encode them.
- Load coordinate move sequences and step forward/backward through them. A replay can be discarded while preserving the current position to continue manually.
- Display board coordinates along all four edges and render PNG piece images, with Unicode glyphs as a fallback.
- Prevent capturing the opposing king; checkmate ends the game.

## Current status

The engine, Windows Forms interface, automated coverage, step 2.5 manual engine-test review, and required UI verification have been completed. See [the project status and future-work checklist](remaining_work.md) for verified results and optional enhancements.
