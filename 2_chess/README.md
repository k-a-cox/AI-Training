# 2 Chess

## Overview

A local two-player chess game for Windows, built with C#, Windows Forms, and .NET 10. Chess rules are implemented separately from the UI in the `AiAssistedChess.Engine` namespace so they can be tested independently.

## Features

- Clickable chessboard with legal destination highlighting.
- Standard piece movement and move validation, including check detection.
- Castling, en passant, and player-selected pawn promotion.
- Checkmate and stalemate detection; kings are not captured. The UI displays the current player and game status, with undo and restart controls.
- Replay coordinate move sequences with Load moves, Next, Back, and Clear replay controls; swap to manual play without resetting the current position.
- PNG chess-piece images with a Unicode glyph fallback.

## Run the application

From the repository root in PowerShell, build and run the Windows Forms application:

```powershell
dotnet run --project .\2_chess\AiAssistedChess\AiAssistedChess.csproj
```

The application targets `net10.0-windows` and requires Windows with the .NET 10 SDK installed.

## Run the tests

Run the chess-engine test project from the repository root:

```powershell
dotnet test .\2_chess\AiAssistedChess.Tests\AiAssistedChess.Tests.csproj
```

## Replay a move sequence

Paste coordinate moves into the move-sequence box, separated by spaces, commas, or new lines, then select **Load moves**. Use **Next** and **Back** to step through the game. **Swap to manual game** discards the remaining replay moves while preserving the current board position. **Clear replay** discards the sequence and resets the board.

```text
e2-e4, e7-e5, g1-f3
```

## Load a custom board position

In the application, paste a temporary position description into the custom-position box and select **Load position**. List each occupied square by color and piece; all squares not listed are empty. The position must include exactly one king per color and specify which side moves next.

```text
white:king=c6,queen=b6; black:king=a8; turn=black
```

Use `white` or `black` before the colon, and `king`, `queen`, `rook`, `bishop`, `knight`, or `pawn` before `=`. Separate pieces with commas and sections with semicolons. Loading a custom position clears move-sequence replay history and enables manual moves and Undo. Custom positions must not contain pawns on a promotion rank, adjacent kings, or a checked king that is not the side to move. Loading a custom position disables castling rights because the format does not encode them. **Restart** returns to the normal starting position. This syntax is temporary and may change as the UI is polished.

## Project status

See [AI documents/remaining_work.md](AiAssistedChess/AI%20documents/remaining_work.md) for the implementation status, verification needs, and prioritized remaining work.

## AI Hand holdiness

I will prompt GPT 6 Luna for the next steps, but let the model generate the development plan and implement changes. I'll sanity check the generated changes before asking GPT to make the commits for me.

## Assets

The application uses the Maestro BW chess piece set from
[Kadagaden/chess-pieces](https://github.com/Kadagaden/chess-pieces), under
[`chess_maestro_bw`](https://github.com/Kadagaden/chess-pieces/tree/master/chess_maestro_bw).
The SVG files were converted to PNG for use by the application. Refer to the
upstream repository for license and attribution details.
