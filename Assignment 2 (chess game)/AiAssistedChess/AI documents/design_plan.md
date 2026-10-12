# Chess Game Design Plan

## Problem and proposed approach

Create a graphical chess game for two people sharing one computer. Implement the standard chess rules, make legal moves easy to select and understand, and show turn and game status clearly. Keep the rule engine separate from the graphical interface so it can be tested independently.

**Suggested language: C#**, using **Windows Forms** for the desktop interface. Windows Forms is a beginner-friendly option for a Windows desktop application and supports a chessboard UI with buttons or custom-drawn controls, mouse input, and status displays.

## Scope and decisions

- Desktop graphical application; local two-player, taking turns on the same board.
- Standard chess movement and game rules: check, checkmate, stalemate, castling, en passant, and pawn promotion.
- Select a piece, show its legal destination squares, and select a destination to move.
- Show the current player, check/game-over state, and provide restart and undo controls.
- No computer opponent, online play, clocks, or account features in the initial version.
- Store board state and rules in classes separate from the Windows Forms UI.

## Todos

1. Set up the C# Windows Forms project and define the board-state representation.
2. Implement and test legal chess moves and game-state detection.
3. Build the Windows Forms board, interaction, and status display using the rules classes.
4. Integrate controls and verify complete local two-player games and special moves.

## Notes and considerations

- Keep pieces represented by structured values (color and piece type), not by screen coordinates.
- Use a clear coordinate conversion layer between board squares and pixels.
- Treat promotion as a player choice; present a small choice UI rather than silently selecting a piece.
- Test rules independently with focused positions before relying on full-game manual testing.
- Keep chess rules testable independently from the Windows Forms event handlers.
