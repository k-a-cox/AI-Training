using System;
using System.Collections.Generic;

namespace AiAssistedChess.Engine
{
    internal record MoveRecord(Move Move, ChessPiece Moved, ChessPiece Captured, Position? EnPassantTarget, bool WhiteCanCastleKingSide, bool WhiteCanCastleQueenSide, bool BlackCanCastleKingSide, bool BlackCanCastleQueenSide);

    public class BoardState
    {
        private readonly ChessPiece[,] board = new ChessPiece[8,8];
        public PieceColor CurrentPlayer { get; private set; } = PieceColor.White;

        public bool WhiteCanCastleKingSide { get; private set; }
        public bool WhiteCanCastleQueenSide { get; private set; }
        public bool BlackCanCastleKingSide { get; private set; }
        public bool BlackCanCastleQueenSide { get; private set; }

        public Position? EnPassantTarget { get; private set; }

        private readonly Stack<MoveRecord> history = new();

        public BoardState()
        {
            Reset();
        }

        public ChessPiece GetPiece(Position p) => p.IsValid ? board[p.Row, p.Col] : ChessPiece.Empty;
        private void SetPiece(Position p, ChessPiece piece)
        {
            if (!p.IsValid) return;
            board[p.Row, p.Col] = piece;
        }

        public void Reset()
        {
            // Empty
            for (int r = 0; r < 8; r++) for (int c = 0; c < 8; c++) board[r,c] = ChessPiece.Empty;

            // Setup pieces
            // White back rank
            board[7,0] = new ChessPiece(PieceType.Rook, PieceColor.White);
            board[7,1] = new ChessPiece(PieceType.Knight, PieceColor.White);
            board[7,2] = new ChessPiece(PieceType.Bishop, PieceColor.White);
            board[7,3] = new ChessPiece(PieceType.Queen, PieceColor.White);
            board[7,4] = new ChessPiece(PieceType.King, PieceColor.White);
            board[7,5] = new ChessPiece(PieceType.Bishop, PieceColor.White);
            board[7,6] = new ChessPiece(PieceType.Knight, PieceColor.White);
            board[7,7] = new ChessPiece(PieceType.Rook, PieceColor.White);
            for (int c = 0; c < 8; c++) board[6,c] = new ChessPiece(PieceType.Pawn, PieceColor.White);

            // Black
            board[0,0] = new ChessPiece(PieceType.Rook, PieceColor.Black);
            board[0,1] = new ChessPiece(PieceType.Knight, PieceColor.Black);
            board[0,2] = new ChessPiece(PieceType.Bishop, PieceColor.Black);
            board[0,3] = new ChessPiece(PieceType.Queen, PieceColor.Black);
            board[0,4] = new ChessPiece(PieceType.King, PieceColor.Black);
            board[0,5] = new ChessPiece(PieceType.Bishop, PieceColor.Black);
            board[0,6] = new ChessPiece(PieceType.Knight, PieceColor.Black);
            board[0,7] = new ChessPiece(PieceType.Rook, PieceColor.Black);
            for (int c = 0; c < 8; c++) board[1,c] = new ChessPiece(PieceType.Pawn, PieceColor.Black);

            CurrentPlayer = PieceColor.White;
            WhiteCanCastleKingSide = WhiteCanCastleQueenSide = BlackCanCastleKingSide = BlackCanCastleQueenSide = true;
            EnPassantTarget = null;
            history.Clear();
        }

        public IEnumerable<(Position pos, ChessPiece piece)> AllPieces()
        {
            for (int r = 0; r < 8; r++)
                for (int c = 0; c < 8; c++)
                {
                    var p = board[r,c];
                    if (!p.IsEmpty) yield return (new Position(r,c), p);
                }
        }

        public void MakeMove(Move move)
        {
            var from = move.From; var to = move.To;
            var moved = GetPiece(from);
            var captured = GetPiece(to);
            // Save record
            var record = new MoveRecord(move, moved, captured, EnPassantTarget, WhiteCanCastleKingSide, WhiteCanCastleQueenSide, BlackCanCastleKingSide, BlackCanCastleQueenSide);
            history.Push(record);

            // Handle en passant capture
            if (moved.Type == PieceType.Pawn && EnPassantTarget.HasValue && to == EnPassantTarget.Value && captured.IsEmpty)
            {
                // Captured pawn is behind the target
                int dir = moved.Color == PieceColor.White ? 1 : -1;
                var capPos = new Position(to.Row + dir, to.Col);
                captured = GetPiece(capPos);
                SetPiece(capPos, ChessPiece.Empty);
            }

            // Move piece
            SetPiece(to, moved);
            SetPiece(from, ChessPiece.Empty);

            // Handle promotion
            if (moved.Type == PieceType.Pawn)
            {
                if ((moved.Color == PieceColor.White && to.Row == 0) || (moved.Color == PieceColor.Black && to.Row == 7))
                {
                    // Default to queen if no promotion set
                    var promoteTo = move.Promotion ?? PieceType.Queen;
                    SetPiece(to, new ChessPiece(promoteTo, moved.Color));
                }
            }

            // Handle castling: if king moved two squares, move rook
            if (moved.Type == PieceType.King)
            {
                int diff = to.Col - from.Col;
                if (Math.Abs(diff) == 2)
                {
                    // King side or queen side
                    if (diff > 0)
                    {
                        // king side
                        var rookFrom = new Position(from.Row, 7);
                        var rookTo = new Position(from.Row, 5);
                        var rook = GetPiece(rookFrom);
                        SetPiece(rookTo, rook);
                        SetPiece(rookFrom, ChessPiece.Empty);
                    }
                    else
                    {
                        var rookFrom = new Position(from.Row, 0);
                        var rookTo = new Position(from.Row, 3);
                        var rook = GetPiece(rookFrom);
                        SetPiece(rookTo, rook);
                        SetPiece(rookFrom, ChessPiece.Empty);
                    }
                }
            }

            // Update castling rights
            if (moved.Type == PieceType.King)
            {
                if (moved.Color == PieceColor.White) { WhiteCanCastleKingSide = WhiteCanCastleQueenSide = false; }
                else { BlackCanCastleKingSide = BlackCanCastleQueenSide = false; }
            }
            if (moved.Type == PieceType.Rook)
            {
                if (from.Row == 7 && from.Col == 7) WhiteCanCastleKingSide = false;
                if (from.Row == 7 && from.Col == 0) WhiteCanCastleQueenSide = false;
                if (from.Row == 0 && from.Col == 7) BlackCanCastleKingSide = false;
                if (from.Row == 0 && from.Col == 0) BlackCanCastleQueenSide = false;
            }

            // If rook was captured, update rights
            if (captured.Type == PieceType.Rook)
            {
                if (to.Row == 7 && to.Col == 7) WhiteCanCastleKingSide = false;
                if (to.Row == 7 && to.Col == 0) WhiteCanCastleQueenSide = false;
                if (to.Row == 0 && to.Col == 7) BlackCanCastleKingSide = false;
                if (to.Row == 0 && to.Col == 0) BlackCanCastleQueenSide = false;
            }

            // Update en passant target
            EnPassantTarget = null;
            if (moved.Type == PieceType.Pawn && Math.Abs(to.Row - from.Row) == 2)
            {
                int dir = moved.Color == PieceColor.White ? -1 : 1;
                EnPassantTarget = new Position(from.Row + dir, from.Col);
            }

            // Switch player
            CurrentPlayer = CurrentPlayer == PieceColor.White ? PieceColor.Black : PieceColor.White;
        }

        public void Undo()
        {
            if (history.Count == 0) return;
            var rec = history.Pop();
            var m = rec.Move;
            SetPiece(m.From, rec.Moved);
            SetPiece(m.To, rec.Captured);

            // If en passant capture was performed, restore the captured pawn behind the to-square
            if (rec.Moved.Type == PieceType.Pawn && rec.EnPassantTarget.HasValue && m.To == rec.EnPassantTarget.Value && rec.Captured.IsEmpty)
            {
                int dir = rec.Moved.Color == PieceColor.White ? 1 : -1;
                var capPos = new Position(m.To.Row + dir, m.To.Col);
                SetPiece(capPos, new ChessPiece(PieceType.Pawn, rec.Moved.Color == PieceColor.White ? PieceColor.Black : PieceColor.White));
                SetPiece(m.To, ChessPiece.Empty);
            }

            // If castling, move rook back
            if (rec.Moved.Type == PieceType.King)
            {
                int diff = m.To.Col - m.From.Col;
                if (Math.Abs(diff) == 2)
                {
                    if (diff > 0)
                    {
                        var rookFrom = new Position(m.From.Row, 7);
                        var rookTo = new Position(m.From.Row, 5);
                        var rook = GetPiece(rookTo);
                        SetPiece(rookFrom, rook);
                        SetPiece(rookTo, ChessPiece.Empty);
                    }
                    else
                    {
                        var rookFrom = new Position(m.From.Row, 0);
                        var rookTo = new Position(m.From.Row, 3);
                        var rook = GetPiece(rookTo);
                        SetPiece(rookFrom, rook);
                        SetPiece(rookTo, ChessPiece.Empty);
                    }
                }
            }

            // Restore state
            EnPassantTarget = rec.EnPassantTarget;
            WhiteCanCastleKingSide = rec.WhiteCanCastleKingSide;
            WhiteCanCastleQueenSide = rec.WhiteCanCastleQueenSide;
            BlackCanCastleKingSide = rec.BlackCanCastleKingSide;
            BlackCanCastleQueenSide = rec.BlackCanCastleQueenSide;

            CurrentPlayer = CurrentPlayer == PieceColor.White ? PieceColor.Black : PieceColor.White;
        }

        public BoardState Clone()
        {
            var copy = new BoardState();
            // copy board
            for (int r = 0; r < 8; r++) for (int c = 0; c < 8; c++) copy.board[r,c] = board[r,c];
            copy.CurrentPlayer = CurrentPlayer;
            copy.WhiteCanCastleKingSide = WhiteCanCastleKingSide;
            copy.WhiteCanCastleQueenSide = WhiteCanCastleQueenSide;
            copy.BlackCanCastleKingSide = BlackCanCastleKingSide;
            copy.BlackCanCastleQueenSide = BlackCanCastleQueenSide;
            copy.EnPassantTarget = EnPassantTarget;
            // history not copied
            return copy;
        }
    }
}
