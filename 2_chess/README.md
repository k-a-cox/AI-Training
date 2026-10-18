# 2 Chess

## Overview

A local two-player chess game for Windows, built with C#, Windows Forms, and .NET 10. Chess rules are implemented separately from the UI in the `AiAssistedChess.Engine` namespace so they can be tested independently.

## Features

- Clickable chessboard with legal destination highlighting.
- Standard piece movement and move validation, including check detection.
- Castling, en passant, and player-selected pawn promotion.
- Checkmate and stalemate status, current-player display, undo, and restart controls.
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

## Project status

See [AI documents/remaining_work.md](AiAssistedChess/AI%20documents/remaining_work.md) for the implementation status, verification needs, and prioritized remaining work.

## Assets

The application uses the Maestro BW chess piece set from
[Kadagaden/chess-pieces](https://github.com/Kadagaden/chess-pieces), under
[`chess_maestro_bw`](https://github.com/Kadagaden/chess-pieces/tree/master/chess_maestro_bw).
The SVG files were converted to PNG for use by the application. Refer to the
upstream repository for license and attribution details.
