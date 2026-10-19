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
            return GetLegalMoves(from, piece.Color, state);
        }

        private IEnumerable<Move> GetLegalMoves(Position from, PieceColor color, BoardState board)
        {
            var piece = board.GetPiece(from);
            if (piece.IsEmpty || piece.Color != color) return Enumerable.Empty<Move>();

            var moves = GeneratePseudoLegalMoves(from, piece, board);
            // Filter out moves that leave king in check
            var legal = new List<Move>();
            foreach (var m in moves)
            {
                var copy = board.Clone();
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
                moves.AddRange(GetLegalMoves(pos, color, state));
            }
            return moves;
        }

        public bool TryMakeMove(Move move)
        {
            if (Status == GameStatus.Checkmate || Status == GameStatus.Stalemate) return false;

            var legal = GetLegalMoves(move.From).Any(m => m.To == move.To && m.Promotion == move.Promotion);
            if (!legal) return false;
            state.MakeMove(move);
            UpdateStatus();
            return true;
        }

        public void Undo() { state.Undo(); UpdateStatus(); }

        internal void SetPositionForTesting(IEnumerable<(Position Position, ChessPiece Piece)> pieces, PieceColor currentPlayer)
        {
            state.SetPositionForTesting(pieces, currentPlayer);
            UpdateStatus();
        }

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
            // For a given attacker color, a pawn that attacks 'square' must be one rank
            // behind it from the pawn's perspective. For white pawns that means the pawn
            // is at square.Row + 1; for black pawns it is at square.Row - 1.
            int pawnDir = attacker == PieceColor.White ? 1 : -1;
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

        private IEnumerable<Move> GeneratePseudoLegalMoves(Position from, ChessPiece piece, BoardState board)
        {
            var moves = new List<Move>();
            int row = from.Row, col = from.Col;
            if (piece.Type == PieceType.Pawn)
            {
                int forwardDir = piece.Color == PieceColor.White ? -1 : 1;
                var oneStep = new Position(row + forwardDir, col);
                if (oneStep.IsValid && board.GetPiece(oneStep).IsEmpty) moves.Add(new Move(from, oneStep));
                // two squares
                var twoStep = new Position(row + 2 * forwardDir, col);
                int startingRow = piece.Color == PieceColor.White ? 6 : 1;
                if (row == startingRow && twoStep.IsValid && board.GetPiece(oneStep).IsEmpty && board.GetPiece(twoStep).IsEmpty) moves.Add(new Move(from, twoStep));
                // captures
                var captureLeft = new Position(row + forwardDir, col - 1);
                var captureRight = new Position(row + forwardDir, col + 1);
                if (captureLeft.IsValid)
                {
                    var targetPiece = board.GetPiece(captureLeft);
                    if (!targetPiece.IsEmpty && targetPiece.Color != piece.Color) moves.Add(new Move(from, captureLeft));
                }
                if (captureRight.IsValid)
                {
                    var targetPiece = board.GetPiece(captureRight);
                    if (!targetPiece.IsEmpty && targetPiece.Color != piece.Color) moves.Add(new Move(from, captureRight));
                }
                // en passant
                if (board.EnPassantTarget.HasValue)
                {
                    var ep = board.EnPassantTarget.Value;
                    if (ep.Row == row + forwardDir && Math.Abs(ep.Col - col) == 1) moves.Add(new Move(from, ep));
                }
            }
            else if (piece.Type == PieceType.Knight)
            {
                int[] deltaRows = { -2, -1, 1, 2 };
                foreach (var deltaRow in deltaRows)
                    foreach (var deltaCol in deltaRows)
                    {
                        if (Math.Abs(deltaRow) == Math.Abs(deltaCol)) continue;
                        var knightTarget = new Position(row + deltaRow, col + deltaCol);
                        if (!knightTarget.IsValid) continue;
                        var targetPiece = board.GetPiece(knightTarget);
                        if (targetPiece.IsEmpty || targetPiece.Color != piece.Color) moves.Add(new Move(from, knightTarget));
                    }
            }
            else if (piece.Type == PieceType.Bishop || piece.Type == PieceType.Rook || piece.Type == PieceType.Queen)
            {
                (int dRow, int dCol)[] directions;
                if (piece.Type == PieceType.Bishop) directions = new (int,int)[] { (-1,-1),(-1,1),(1,-1),(1,1) };
                else if (piece.Type == PieceType.Rook) directions = new (int,int)[] { (-1,0),(1,0),(0,-1),(0,1) };
                else directions = new (int,int)[] { (-1,-1),(-1,1),(1,-1),(1,1),(-1,0),(1,0),(0,-1),(0,1) };
                foreach (var (dRow,dCol) in directions)
                {
                    int nextRow = row + dRow, nextCol = col + dCol;
                    while (nextRow >= 0 && nextRow < 8 && nextCol >= 0 && nextCol < 8)
                    {
                        var encountered = board.GetPiece(new Position(nextRow,nextCol));
                        if (encountered.IsEmpty) moves.Add(new Move(from, new Position(nextRow,nextCol)));
                        else { if (encountered.Color != piece.Color) moves.Add(new Move(from, new Position(nextRow,nextCol))); break; }
                        nextRow += dRow; nextCol += dCol;
                    }
                }
            }
            else if (piece.Type == PieceType.King)
            {
                for (int dRow = -1; dRow <= 1; dRow++) for (int dCol = -1; dCol <= 1; dCol++)
                {
                    if (dRow == 0 && dCol == 0) continue;
                    var kingTarget = new Position(row + dRow, col + dCol);
                    if (!kingTarget.IsValid) continue;
                    var targetPiece = board.GetPiece(kingTarget);
                    if (targetPiece.IsEmpty || targetPiece.Color != piece.Color) moves.Add(new Move(from, kingTarget));
                }
                // castling
                if (piece.Color == PieceColor.White)
                {
                    if (board.WhiteCanCastleKingSide && IsPathClearForCastling(board, 7,4,7,7)) moves.Add(new Move(from, new Position(7,6)));
                    if (board.WhiteCanCastleQueenSide && IsPathClearForCastling(board, 7,4,7,0)) moves.Add(new Move(from, new Position(7,2)));
                }
                else
                {
                    if (board.BlackCanCastleKingSide && IsPathClearForCastling(board, 0,4,0,7)) moves.Add(new Move(from, new Position(0,6)));
                    if (board.BlackCanCastleQueenSide && IsPathClearForCastling(board, 0,4,0,0)) moves.Add(new Move(from, new Position(0,2)));
                }
            }

            // Handle promotions: if pawn reaches last rank, add promotion options
            var results = new List<Move>();
            foreach (var m in moves)
            {
                var currentPiece = board.GetPiece(from);
                if (currentPiece.Type == PieceType.Pawn && (m.To.Row == 0 || m.To.Row == 7))
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

        private bool IsPathClearForCastling(BoardState board, int kingRow, int kingCol, int rookRow, int rookCol)
        {
            // Simplified: ensure squares between king and rook are empty and not under attack
            int dir = rookCol > kingCol ? 1 : -1;
            for (int c = kingCol + dir; c != rookCol; c += dir)
                if (!board.GetPiece(new Position(kingRow, c)).IsEmpty) return false;

            // King must not be in check, and squares it passes through must not be attacked
            var kingColor = board.GetPiece(new Position(kingRow, kingCol)).Color;
            if (IsKingInCheck(board, kingColor)) return false;
            // squares king passes
            var pass1 = new Position(kingRow, kingCol + dir);
            var pass2 = new Position(kingRow, kingCol + 2*dir);
            if (IsSquareAttacked(board, pass1, Opposite(kingColor))) return false;
            if (IsSquareAttacked(board, pass2, Opposite(kingColor))) return false;
            return true;
        }

        private static PieceColor Opposite(PieceColor c) => c == PieceColor.White ? PieceColor.Black : PieceColor.White;
    }
}
