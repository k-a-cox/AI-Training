# Chess Game Implementation Plan

## Overview
Build a local two-player chess game in C# using Windows Forms and .NET 10 LTS. Follow a phased approach: core chess engine first (independently testable), then Windows Forms UI.

## Phase 1: Core Chess Engine (Domain Logic)

### Purpose
Create the chess rules engine completely separate from the UI. This allows:
- Independent testing of all rules
- Easy debugging
- Clear separation of concerns
- UI becomes much simpler once engine is solid

### Files to Create

1. **ChessPiece.cs**
   - Enums: PieceType (Pawn, Rook, Knight, Bishop, Queen, King), PieceColor (White, Black)
   - Structure to represent a piece with type and color

2. **Position.cs**
   - Represents a board coordinate (0-7 for file/rank)
   - Helper methods for coordinate validation
   - ToString() for debugging

3. **Move.cs**
   - Represents a move (source, destination, optional promotion piece)
   - ToString() for debugging

4. **BoardState.cs**
   - Internal representation of the 8x8 board
   - Methods to get/set pieces at positions
   - Track castling rights
   - Track en passant target square (if any)
   - Track move history (for undo)

5. **ChessGame.cs**
   - Main orchestrator class
   - Current board state
   - Current player (white/black)
   - Game status (active, check, checkmate, stalemate, white won, black won)
   - Methods:
	 - GetLegalMoves(position) - Return all legal destinations for a piece
	 - TryMove(source, destination, promotionPiece) - Execute a move if legal
	 - Undo() - Undo last move
	 - IsInCheck(color) - Check if a color is in check
	 - DetectGameState() - Determine if game is active, checkmate, stalemate, etc.
	 - ResetGame() - Start a new game

### Move Validation Components

- **Piece Movement Rules**
  - Pawn: 1 square forward (2 on first move), capture diagonally
  - Rook: Any distance horizontally/vertically
  - Knight: L-shaped moves (2 in one direction, 1 perpendicular)
  - Bishop: Any distance diagonally
  - Queen: Any distance horizontally/vertically/diagonally
  - King: 1 square in any direction

- **Special Moves**
  - Castling: King hasn't moved, rook hasn't moved, no pieces between, king not in check, king doesn't move through check
  - En Passant: Pawn capture after opponent's two-square pawn advance
  - Pawn Promotion: When pawn reaches opposite end, let player choose piece type

- **Game State Detection**
  - Check: King is under attack
  - Checkmate: King in check and no legal moves
  - Stalemate: King not in check but no legal moves
  - Move legality: Move doesn't leave own king in check

### Testing Strategy

Create unit tests for:
- Pawn movement and capture (including en passant)
- Rook, knight, bishop, queen movement
- King movement and castling
- Check detection
- Checkmate scenarios
- Stalemate scenarios
- Undo functionality

---

## Phase 2: Windows Forms UI

### Files to Create/Modify

1. **ChessboardPanel.cs** (Custom Control)
   - 8x8 grid of squares
   - Display pieces using symbols or images
   - Show legal move destinations as highlighted squares
   - Mouse click handling to select piece and destination

2. **PawnPromotionForm.cs**
   - Small dialog that appears when pawn reaches end
   - Let player choose: Queen, Rook, Bishop, Knight
   - Return selected piece type

3. **Form1.cs** (Main Game Form)
   - Integrate ChessboardPanel
   - Status display labels (current player, game status)
   - Restart button
   - Undo button
   - Connect UI events to ChessGame logic

### UI Layout

```
[Title: Chess Game]
[Status: White to move | Check | Checkmate | Stalemate]
[Chessboard - 8x8 grid]
[Undo] [Restart] [Exit]
```

### Interaction Flow

1. Player clicks a piece on the board
2. ChessGame.GetLegalMoves() returns valid destinations
3. UI highlights legal destination squares
4. Player clicks a destination
5. If pawn promotion: show PawnPromotionForm
6. ChessGame.TryMove() executes the move
7. UI updates board display
8. Status updates
9. Next player's turn

---

## Phase 3: Integration & Polish

### Testing Tasks

- Play complete games with all standard moves
- Test castling in multiple scenarios
- Test en passant
- Test pawn promotion with all piece choices
- Test undo multiple moves in succession
- Test restart clears board and resets game state
- Verify check/checkmate/stalemate detection

### Possible Enhancements

- Piece movement animations
- Move history display
- Sound effects
- Save/load games
- Difficulty AI (future work)

---

## Project Structure

```
AiAssistedChess/
├── Program.cs
├── Form1.cs (Main game form)
├── Form1.Designer.cs
├── Form1.resx
├── ChessboardPanel.cs (Custom control)
├── PawnPromotionForm.cs
├── PawnPromotionForm.Designer.cs
├── PawnPromotionForm.resx
├── Chess Engine/
│   ├── ChessPiece.cs
│   ├── Position.cs
│   ├── Move.cs
│   ├── BoardState.cs
│   └── ChessGame.cs
├── design_plan.md
└── implementation_plan.md
```

---

## Execution Order

1. Create chess engine files (Phase 1)
2. Write unit tests for engine
3. Test and debug engine until all rules pass
4. Create UI controls (Phase 2)
5. Integrate UI with engine
6. Test complete game scenarios (Phase 3)
7. Polish and verify

This approach ensures rules are correct before building UI.
