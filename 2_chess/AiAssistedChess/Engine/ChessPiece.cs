using System;

namespace AiAssistedChess.Engine
{
    public enum PieceType { None, Pawn, Knight, Bishop, Rook, Queen, King }
    public enum PieceColor { None, White, Black }

    public readonly struct ChessPiece
    {
        public PieceType Type { get; }
        public PieceColor Color { get; }

        public ChessPiece(PieceType type, PieceColor color)
        {
            Type = type;
            Color = color;
        }

        public static ChessPiece Empty => new ChessPiece(PieceType.None, PieceColor.None);

        public bool IsEmpty => Type == PieceType.None;

        public override string ToString()
        {
            if (IsEmpty) return ".";
            char c = Type switch
            {
                PieceType.Pawn => 'P',
                PieceType.Knight => 'N',
                PieceType.Bishop => 'B',
                PieceType.Rook => 'R',
                PieceType.Queen => 'Q',
                PieceType.King => 'K',
                _ => '?'
            };
            return (Color == PieceColor.White ? 'W' : 'B') + c.ToString();
        }
    }
}
