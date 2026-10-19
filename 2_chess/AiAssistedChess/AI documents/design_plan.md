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

## Impromptu additions

- Load custom board positions using the established text syntax. Position loading validates the kings and rejects pawns on promotion ranks, adjacent kings, or a checked king that is not the side to move; custom positions disable castling rights.
- Replay coordinate move sequences with Load moves, Next, Back, and Clear replay controls. Swap to manual game preserves the current board position while discarding the remaining replay moves.
- Display board coordinates along all four edges and use chess-piece images with a Unicode fallback.
- Prevent moves from capturing the opposing king; the game ends by checkmate instead.
