using System;
using System.Collections.Generic;
using System.Linq;
using AiAssistedChess.Engine;

namespace AiAssistedChess.UI
{
    internal sealed class CustomBoardPosition
    {
        public IReadOnlyList<(Position Position, ChessPiece Piece)> Pieces { get; }

        public PieceColor CurrentPlayer { get; }

        public CustomBoardPosition(IReadOnlyList<(Position Position, ChessPiece Piece)> pieces, PieceColor currentPlayer)
        {
            Pieces = pieces;
            CurrentPlayer = currentPlayer;
        }
    }

    internal static class CustomBoardPositionParser
    {
        public static bool TryParse(string input, out CustomBoardPosition? position, out string errorMessage)
        {
            var sections = input.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var pieces = new List<(Position Position, ChessPiece Piece)>();
            var occupiedSquares = new HashSet<Position>();
            var kingCounts = new Dictionary<PieceColor, int>
            {
                [PieceColor.White] = 0,
                [PieceColor.Black] = 0
            };
            PieceColor? currentPlayer = null;

            foreach (var section in sections)
            {
                if (section.StartsWith("turn=", StringComparison.OrdinalIgnoreCase))
                {
                    if (currentPlayer.HasValue)
                    {
                        position = null;
                        errorMessage = "Specify the side to move only once.";
                        return false;
                    }

                    var turnValue = section.Substring("turn=".Length).Trim();
                    if (turnValue.Equals("white", StringComparison.OrdinalIgnoreCase))
                    {
                        currentPlayer = PieceColor.White;
                    }
                    else if (turnValue.Equals("black", StringComparison.OrdinalIgnoreCase))
                    {
                        currentPlayer = PieceColor.Black;
                    }
                    else
                    {
                        position = null;
                        errorMessage = "Side to move must be 'white' or 'black'.";
                        return false;
                    }

                    continue;
                }

                var colorSeparator = section.IndexOf(':');
                if (colorSeparator <= 0)
                {
                    position = null;
                    errorMessage = $"Invalid section '{section}'. Use white:piece=square or black:piece=square.";
                    return false;
                }

                var colorName = section.Substring(0, colorSeparator).Trim();
                PieceColor color;
                if (colorName.Equals("white", StringComparison.OrdinalIgnoreCase))
                {
                    color = PieceColor.White;
                }
                else if (colorName.Equals("black", StringComparison.OrdinalIgnoreCase))
                {
                    color = PieceColor.Black;
                }
                else
                {
                    position = null;
                    errorMessage = $"Unknown piece color '{colorName}'. Use white or black.";
                    return false;
                }

                var pieceEntries = section.Substring(colorSeparator + 1).Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (pieceEntries.Length == 0)
                {
                    position = null;
                    errorMessage = $"Add at least one piece after '{colorName}:'.";
                    return false;
                }

                foreach (var pieceEntry in pieceEntries)
                {
                    var assignmentSeparator = pieceEntry.IndexOf('=');
                    if (assignmentSeparator <= 0 || assignmentSeparator == pieceEntry.Length - 1)
                    {
                        position = null;
                        errorMessage = $"Invalid piece entry '{pieceEntry}'. Use piece=square, such as king=e1.";
                        return false;
                    }

                    var pieceName = pieceEntry.Substring(0, assignmentSeparator).Trim();
                    var squareName = pieceEntry.Substring(assignmentSeparator + 1).Trim();
                    if (!TryParsePieceType(pieceName, out var pieceType))
                    {
                        position = null;
                        errorMessage = $"Unknown piece type '{pieceName}'. Use king, queen, rook, bishop, knight, or pawn.";
                        return false;
                    }

                    if (!TryParseSquare(squareName, out var square))
                    {
                        position = null;
                        errorMessage = $"Invalid square '{squareName}'. Use a file from a-h and a rank from 1-8.";
                        return false;
                    }

                    if (!occupiedSquares.Add(square))
                    {
                        position = null;
                        errorMessage = $"Square '{squareName}' is listed more than once.";
                        return false;
                    }

                    if (pieceType == PieceType.King)
                    {
                        kingCounts[color]++;
                    }

                    pieces.Add((square, new ChessPiece(pieceType, color)));
                }
            }

            if (!currentPlayer.HasValue)
            {
                position = null;
                errorMessage = "Specify who moves next with turn=white or turn=black.";
                return false;
            }

            if (kingCounts[PieceColor.White] != 1 || kingCounts[PieceColor.Black] != 1)
            {
                position = null;
                errorMessage = "A position must contain exactly one white king and one black king.";
                return false;
            }

            position = new CustomBoardPosition(pieces, currentPlayer.Value);
            errorMessage = string.Empty;
            return true;
        }

        private static bool TryParsePieceType(string value, out PieceType pieceType)
        {
            switch (value.Trim().ToLowerInvariant())
            {
                case "king":
                    pieceType = PieceType.King;
                    return true;
                case "queen":
                    pieceType = PieceType.Queen;
                    return true;
                case "rook":
                    pieceType = PieceType.Rook;
                    return true;
                case "bishop":
                    pieceType = PieceType.Bishop;
                    return true;
                case "knight":
                    pieceType = PieceType.Knight;
                    return true;
                case "pawn":
                    pieceType = PieceType.Pawn;
                    return true;
                default:
                    pieceType = PieceType.None;
                    return false;
            }
        }

        private static bool TryParseSquare(string value, out Position position)
        {
            if (value.Length != 2)
            {
                position = default;
                return false;
            }

            var file = char.ToLowerInvariant(value[0]);
            var rank = value[1];
            if (file < 'a' || file > 'h' || rank < '1' || rank > '8')
            {
                position = default;
                return false;
            }

            position = new Position(8 - (rank - '0'), file - 'a');
            return true;
        }
    }
}
