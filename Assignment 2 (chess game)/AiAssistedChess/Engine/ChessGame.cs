using System;
using System.Collections.Generic;
using System.Linq;

namespace AiAssistedChess.Engine
{
    public enum GameStatus { Active, Check, Checkmate, Stalemate }

    public class ChessGame
    {
        private readonly BoardState state = new();

        public PieceColor CurrentPlayer => state.CurrentPlayer;

        public GameStatus Status { get; private set; } = GameStatus.Active;

        public BoardState Board => state;

        public ChessGame()
        {
            UpdateStatus();
        }

        public void Reset() { state.Reset(); UpdateStatus(); }

        public IEnumerable<Move> GetLegalMoves(Position from)
        {
            var piece = state.GetPiece(from);
            if (piece.IsEmpty || piece.Color != state.CurrentPlayer) return Enumerable.Empty<Move>();
            var moves = GeneratePseudoLegalMoves(from, piece);
            // Filter out moves that leave king in check
            var legal = new List<Move>();
            foreach (var m in moves)
            {
                var copy = state.Clone();
                copy.MakeMove(m);
                // we need to check if own king is in check in copy
                if (!IsKingInCheck(copy, piece.Color)) legal.Add(m);
            }
            return legal;
        }

        public IEnumerable<Move> GetAllLegalMoves(PieceColor color)
        {
            var moves = new List<Move>();
            foreach (var (pos, piece) in state.AllPieces())
            {
                if (piece.Color != color) continue;
                moves.AddRange(GetLegalMoves(pos));
            }
            return moves;
        }

        public bool TryMakeMove(Move move)
        {
            var legal = GetLegalMoves(move.From).Any(m => m.To == move.To && m.Promotion == move.Promotion);
            if (!legal) return false;
            state.MakeMove(move);
            UpdateStatus();
            return true;
        }

        public void Undo() { state.Undo(); UpdateStatus(); }

        private void UpdateStatus()
        {
            var color = state.CurrentPlayer;
            bool inCheck = IsKingInCheck(state, color);
            var any = GetAllLegalMoves(color).Any();
            if (inCheck && !any) Status = GameStatus.Checkmate;
            else if (!inCheck && !any) Status = GameStatus.Stalemate;
            else if (inCheck) Status = GameStatus.Check;
            else Status = GameStatus.Active;
        }

        private static bool IsKingInCheck(BoardState board, PieceColor kingColor)
        {
            // find king
            Position? kingPos = null;
            foreach (var (pos, piece) in board.AllPieces())
            {
                if (piece.Type == PieceType.King && piece.Color == kingColor) { kingPos = pos; break; }
            }
            if (!kingPos.HasValue) return false; // should not happen
            return IsSquareAttacked(board, kingPos.Value, kingColor == PieceColor.White ? PieceColor.Black : PieceColor.White);
        }

        private static bool IsSquareAttacked(BoardState board, Position square, PieceColor attacker)
        {
            // Pawns
            int pawnDir = attacker == PieceColor.White ? -1 : 1;
            var p1 = new Position(square.Row + pawnDir, square.Col - 1);
            var p2 = new Position(square.Row + pawnDir, square.Col + 1);
            if (p1.IsValid)
            {
                var pp = board.GetPiece(p1);
                if (!pp.IsEmpty && pp.Color == attacker && pp.Type == PieceType.Pawn) return true;
            }
            if (p2.IsValid)
            {
                var pp = board.GetPiece(p2);
                if (!pp.IsEmpty && pp.Color == attacker && pp.Type == PieceType.Pawn) return true;
            }

            // Knights
            int[] kdr = { -2, -1, 1, 2 };
            foreach (var dr in kdr)
                foreach (var dc in kdr)
                {
                    if (Math.Abs(dr) == Math.Abs(dc)) continue;
                    var np = new Position(square.Row + dr, square.Col + dc);
                    if (!np.IsValid) continue;
                    var p = board.GetPiece(np);
                    if (!p.IsEmpty && p.Color == attacker && p.Type == PieceType.Knight) return true;
                }

            // Sliding pieces: rook/queen (orthogonal), bishop/queen (diagonal)
            (int dr, int dc)[] directions = { (-1,0),(1,0),(0,-1),(0,1),(-1,-1),(-1,1),(1,-1),(1,1) };
            foreach (var (dr, dc) in directions)
            {
                var r = square.Row + dr; var c = square.Col + dc;
                int distance = 1;
                while (r >= 0 && r < 8 && c >= 0 && c < 8)
                {
                    var p = board.GetPiece(new Position(r,c));
                    if (!p.IsEmpty)
                    {
                        if (p.Color == attacker)
                        {
                            if (distance == 1 && p.Type == PieceType.King) return true;
                            if ((dr == 0 || dc == 0) && (p.Type == PieceType.Rook || p.Type == PieceType.Queen)) return true;
                            if ((dr != 0 && dc != 0) && (p.Type == PieceType.Bishop || p.Type == PieceType.Queen)) return true;
                        }
                        break; // blocked
                    }
                    r += dr; c += dc; distance++;
                }
            }
            return false;
        }

        private IEnumerable<Move> GeneratePseudoLegalMoves(Position from, ChessPiece piece)
        {
            var list = new List<Move>();
            int r = from.Row, c = from.Col;
            if (piece.Type == PieceType.Pawn)
            {
                int dir = piece.Color == PieceColor.White ? -1 : 1;
                var one = new Position(r + dir, c);
                if (one.IsValid && state.GetPiece(one).IsEmpty) list.Add(new Move(from, one));
                // two squares
                var two = new Position(r + 2*dir, c);
                int startRow = piece.Color == PieceColor.White ? 6 : 1;
                if (r == startRow && two.IsValid && state.GetPiece(one).IsEmpty && state.GetPiece(two).IsEmpty) list.Add(new Move(from, two));
                // captures
                var capL = new Position(r + dir, c -1);
                var capR = new Position(r + dir, c +1);
                if (capL.IsValid)
                {
                    var p = state.GetPiece(capL);
                    if (!p.IsEmpty && p.Color != piece.Color) list.Add(new Move(from, capL));
                }
                if (capR.IsValid)
                {
                    var p = state.GetPiece(capR);
                    if (!p.IsEmpty && p.Color != piece.Color) list.Add(new Move(from, capR));
                }
                // en passant
                if (state.EnPassantTarget.HasValue)
                {
                    var ep = state.EnPassantTarget.Value;
                    if (ep.Row == r + dir && Math.Abs(ep.Col - c) == 1) list.Add(new Move(from, ep));
                }
            }
            else if (piece.Type == PieceType.Knight)
            {
                int[] drs = { -2, -1, 1, 2 };
                foreach (var dr in drs)
                    foreach (var dc in drs)
                    {
                        if (Math.Abs(dr) == Math.Abs(dc)) continue;
                        var np = new Position(r + dr, c + dc);
                        if (!np.IsValid) continue;
                        var p = state.GetPiece(np);
                        if (p.IsEmpty || p.Color != piece.Color) list.Add(new Move(from, np));
                    }
            }
            else if (piece.Type == PieceType.Bishop || piece.Type == PieceType.Rook || piece.Type == PieceType.Queen)
            {
                (int dr, int dc)[] dirs;
                if (piece.Type == PieceType.Bishop) dirs = new (int,int)[] { (-1,-1),(-1,1),(1,-1),(1,1) };
                else if (piece.Type == PieceType.Rook) dirs = new (int,int)[] { (-1,0),(1,0),(0,-1),(0,1) };
                else dirs = new (int,int)[] { (-1,-1),(-1,1),(1,-1),(1,1),(-1,0),(1,0),(0,-1),(0,1) };
                foreach (var (dr,dc) in dirs)
                {
                    int rr = r+dr, cc = c+dc;
                    while (rr >=0 && rr<8 && cc>=0 && cc<8)
                    {
                        var p = state.GetPiece(new Position(rr,cc));
                        if (p.IsEmpty) list.Add(new Move(from, new Position(rr,cc)));
                        else { if (p.Color != piece.Color) list.Add(new Move(from, new Position(rr,cc))); break; }
                        rr += dr; cc += dc;
                    }
                }
            }
            else if (piece.Type == PieceType.King)
            {
                for (int dr = -1; dr <= 1; dr++) for (int dc = -1; dc <= 1; dc++)
                {
                    if (dr == 0 && dc == 0) continue;
                    var np = new Position(r+dr, c+dc);
                    if (!np.IsValid) continue;
                    var p = state.GetPiece(np);
                    if (p.IsEmpty || p.Color != piece.Color) list.Add(new Move(from, np));
                }
                // castling
                if (piece.Color == PieceColor.White)
                {
                    if (state.WhiteCanCastleKingSide && IsPathClearForCastling(7,4,7,7)) list.Add(new Move(from, new Position(7,6)));
                    if (state.WhiteCanCastleQueenSide && IsPathClearForCastling(7,4,7,0)) list.Add(new Move(from, new Position(7,2)));
                }
                else
                {
                    if (state.BlackCanCastleKingSide && IsPathClearForCastling(0,4,0,7)) list.Add(new Move(from, new Position(0,6)));
                    if (state.BlackCanCastleQueenSide && IsPathClearForCastling(0,4,0,0)) list.Add(new Move(from, new Position(0,2)));
                }
            }

            // Handle promotions: if pawn reaches last rank, add promotion options
            var results = new List<Move>();
            foreach (var m in list)
            {
                var p = state.GetPiece(from);
                if (p.Type == PieceType.Pawn && (m.To.Row == 0 || m.To.Row == 7))
                {
                    results.Add(new Move(m.From, m.To, PieceType.Queen));
                    results.Add(new Move(m.From, m.To, PieceType.Rook));
                    results.Add(new Move(m.From, m.To, PieceType.Bishop));
                    results.Add(new Move(m.From, m.To, PieceType.Knight));
                }
                else results.Add(m);
            }

            return results;
        }

        private bool IsPathClearForCastling(int kingRow, int kingCol, int rookRow, int rookCol)
        {
            // Simplified: ensure squares between king and rook are empty and not under attack
            int dir = rookCol > kingCol ? 1 : -1;
            for (int c = kingCol + dir; c != rookCol; c += dir)
                if (!state.GetPiece(new Position(kingRow, c)).IsEmpty) return false;

            // King must not be in check, and squares it passes through must not be attacked
            var kingColor = state.GetPiece(new Position(kingRow, kingCol)).Color;
            if (IsKingInCheck(state, kingColor)) return false;
            // squares king passes
            var pass1 = new Position(kingRow, kingCol + dir);
            var pass2 = new Position(kingRow, kingCol + 2*dir);
            if (IsSquareAttacked(state, pass1, Opposite(kingColor))) return false;
            if (IsSquareAttacked(state, pass2, Opposite(kingColor))) return false;
            return true;
        }

        private static PieceColor Opposite(PieceColor c) => c == PieceColor.White ? PieceColor.Black : PieceColor.White;
    }
}
