# Chess Application Architecture and Implementation

## Overview

The application is a local two-player chess game for Windows, built with C#, Windows Forms, and .NET 10. The chess engine is separated from the user interface and covered by an xUnit test project.

## Project structure

```text
AiAssistedChess/
├── Engine/
│   ├── BoardState.cs       # Board contents, special-move state, and undo history
│   ├── ChessGame.cs        # Legal moves, game status, loading positions, and undo
│   ├── ChessPiece.cs       # Piece types, colors, and piece values
│   ├── Move.cs             # Move source, destination, and optional promotion
│   └── Position.cs         # Board row and column
├── UI/
│   ├── ChessboardPanel.cs          # Board rendering, selection, and mouse input
│   ├── CustomBoardPositionParser.cs # Custom-position text parser
│   ├── MoveSequencePlayer.cs       # Coordinate move-sequence replay
│   └── PawnPromotionForm.cs        # Promotion-choice dialog
├── Images/pieces/          # White and black piece PNGs copied to build output
├── Form1.cs                # Main form actions and status/control updates
├── Form1.Designer.cs       # Windows Forms layout and event wiring
└── Program.cs              # Application entry point

AiAssistedChess.Tests/
└── ChessEngineTests.cs     # xUnit engine and regression tests
```

## Engine responsibilities

- `BoardState` stores pieces, current player, castling rights, the en-passant target, and move history. Its low-level `MakeMove` applies a move to the board; `ChessGame.TryMakeMove` enforces legality before changing a game.
- `ChessGame.GetLegalMoves` generates legal moves for a piece. `GetAllLegalMoves(color)` evaluates moves for the specified color without changing whose turn it is.
- `ChessGame.TryMakeMove` rejects illegal moves, including moves that capture the opposing king, and updates check, checkmate, or stalemate status after legal moves.
- `ChessGame.Undo` restores the prior board, turn, status, and relevant special-move state.
- `ChessGame.LoadPosition` validates custom positions and disables castling rights for loaded positions.

## User interface responsibilities

- `ChessboardPanel` draws the board, coordinates, pieces, and selection/legal-move overlays; it translates clicks into board positions and opens the promotion dialog when needed.
- `Form1` wires board changes to status updates and handles undo, restart, custom-position loading, and replay controls.
- `MoveSequencePlayer` parses coordinate moves and steps through them using `ChessGame.TryMakeMove` and `Undo`. Illegal replay moves stop at the failing move; **Swap to manual game** preserves the current board while discarding the remaining replay.
- `CustomBoardPositionParser` parses the documented text format. The format does not represent castling rights.

## Verification

Run the automated tests from the repository root in PowerShell:

```powershell
dotnet test .\2_chess\AiAssistedChess.Tests\AiAssistedChess.Tests.csproj --nologo --verbosity minimal
```

The latest verified run passed all 83 tests. Manual engine-test and required UI reviews are recorded in [remaining_work.md](remaining_work.md).

## Future implementation candidates

Optional work—not required to consider the application complete—is tracked in [remaining_work.md](remaining_work.md). Current candidates include a move history with replay navigation, a board that scales to available screen space, last-move highlighting, animations, sound, save/load, and additional draw rules.
