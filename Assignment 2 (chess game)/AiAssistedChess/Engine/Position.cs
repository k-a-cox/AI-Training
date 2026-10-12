using System;

namespace AiAssistedChess.Engine
{
    public readonly struct Position : IEquatable<Position>
    {
        public int Row { get; }
        public int Col { get; }

        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public bool IsValid => Row >= 0 && Row < 8 && Col >= 0 && Col < 8;

        public override string ToString()
        {
            // Algebraic like e4
            char file = (char)('a' + Col);
            int rank = 8 - Row;
            return $"{file}{rank}";
        }

        public bool Equals(Position other) => Row == other.Row && Col == other.Col;
        public override bool Equals(object? obj) => obj is Position p && Equals(p);
        public override int GetHashCode() => HashCode.Combine(Row, Col);
        public static bool operator ==(Position a, Position b) => a.Equals(b);
        public static bool operator !=(Position a, Position b) => !a.Equals(b);
    }
}
