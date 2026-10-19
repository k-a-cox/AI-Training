using System;
using System.Linq;
using Xunit;
using AiAssistedChess.Engine;

namespace AiAssistedChess.Tests
{
    public class ChessEngineTests
    {
        [Fact]
        public void PawnInitialMoveAllowsTwo()
        {
            var g = new ChessGame();
            var from = new Position(6, 4); // e2
            var to = new Position(4, 4);   // e4
            Assert.True(g.TryMakeMove(new Move(from, to)));
        }

        [Fact]
        public void GetAllLegalMovesReturnsMovesForNonCurrentPlayer()
        {
            var game = new ChessGame();

            var blackMoves = game.GetAllLegalMoves(PieceColor.Black).ToList();

            Assert.NotEmpty(blackMoves);
            Assert.All(blackMoves, move => Assert.Equal(PieceColor.Black, game.Board.GetPiece(move.From).Color));
            Assert.Equal(PieceColor.White, game.CurrentPlayer);
        }

        [Fact]
        public void EnPassantCapture()
        {
            var g = new ChessGame();
            // White: e2->e4
            Assert.True(g.TryMakeMove(new Move(new Position(6,4), new Position(4,4))));
            // Black: a7->a6 (random)
            Assert.True(g.TryMakeMove(new Move(new Position(1,0), new Position(2,0))));
            // White: e4->e5
            Assert.True(g.TryMakeMove(new Move(new Position(4,4), new Position(3,4))));
            // Black: d7->d5
            Assert.True(g.TryMakeMove(new Move(new Position(1,3), new Position(3,3))));
            // White: e5xd6 en passant
            Assert.True(g.TryMakeMove(new Move(new Position(3,4), new Position(2,3))));
        }

        [Fact]
        public void EnPassantExpiresAfterOneReply()
        {
            var game = new ChessGame();

            Assert.True(game.TryMakeMove(new Move(new Position(6, 4), new Position(4, 4))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 0), new Position(2, 0))));
            Assert.True(game.TryMakeMove(new Move(new Position(4, 4), new Position(3, 4))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 3), new Position(3, 3))));
            Assert.True(game.TryMakeMove(new Move(new Position(6, 0), new Position(5, 0))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 7), new Position(2, 7))));

            Assert.False(game.TryMakeMove(new Move(new Position(3, 4), new Position(2, 3))));
        }

        [Fact]
        public void UndoEnPassantRestoresBothPawns()
        {
            var game = new ChessGame();

            Assert.True(game.TryMakeMove(new Move(new Position(6, 4), new Position(4, 4))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 0), new Position(2, 0))));
            Assert.True(game.TryMakeMove(new Move(new Position(4, 4), new Position(3, 4))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 3), new Position(3, 3))));
            Assert.True(game.TryMakeMove(new Move(new Position(3, 4), new Position(2, 3))));

            game.Undo();

            Assert.Equal(PieceType.Pawn, game.Board.GetPiece(new Position(3, 4)).Type);
            Assert.Equal(PieceColor.White, game.Board.GetPiece(new Position(3, 4)).Color);
            Assert.Equal(PieceType.Pawn, game.Board.GetPiece(new Position(3, 3)).Type);
            Assert.Equal(PieceColor.Black, game.Board.GetPiece(new Position(3, 3)).Color);
            Assert.True(game.Board.GetPiece(new Position(2, 3)).IsEmpty);
            Assert.Equal(PieceColor.White, game.CurrentPlayer);
        }

        [Fact]
        public void CastlingIsRejectedWhenTransitSquareIsAttacked()
        {
            var game = new ChessGame();

            Assert.True(game.TryMakeMove(new Move(new Position(6, 4), new Position(4, 4))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 3), new Position(2, 3))));
            Assert.True(game.TryMakeMove(new Move(new Position(6, 6), new Position(4, 6))));
            Assert.True(game.TryMakeMove(new Move(new Position(0, 2), new Position(3, 5))));
            Assert.True(game.TryMakeMove(new Move(new Position(4, 6), new Position(3, 6))));
            Assert.True(game.TryMakeMove(new Move(new Position(3, 5), new Position(5, 7))));
            Assert.True(game.TryMakeMove(new Move(new Position(7, 6), new Position(5, 5))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 0), new Position(2, 0))));
            Assert.True(game.TryMakeMove(new Move(new Position(7, 5), new Position(4, 2))));

            Assert.DoesNotContain(game.GetLegalMoves(new Position(7, 4)), move => move.To == new Position(7, 6));
            Assert.False(game.TryMakeMove(new Move(new Position(7, 4), new Position(7, 6))));
        }

        [Fact]
        public void CastlingIsRejectedWhenKingIsInCheck()
        {
            var game = new ChessGame();

            Assert.True(game.TryMakeMove(new Move(new Position(6, 5), new Position(4, 5))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 4), new Position(3, 4))));
            Assert.True(game.TryMakeMove(new Move(new Position(7, 6), new Position(5, 5))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 0), new Position(2, 0))));
            Assert.True(game.TryMakeMove(new Move(new Position(6, 4), new Position(4, 4))));
            Assert.True(game.TryMakeMove(new Move(new Position(2, 0), new Position(3, 0))));
            Assert.True(game.TryMakeMove(new Move(new Position(7, 5), new Position(4, 2))));
            Assert.True(game.TryMakeMove(new Move(new Position(0, 3), new Position(4, 7))));

            Assert.Equal(GameStatus.Check, game.Status);
            Assert.DoesNotContain(game.GetLegalMoves(new Position(7, 4)), move => move.To == new Position(7, 6));
        }

        [Fact]
        public void CastlingIsRejectedWhenDestinationSquareIsAttacked()
        {
            var game = new ChessGame();

            Assert.True(game.TryMakeMove(new Move(new Position(6, 4), new Position(4, 4))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 4), new Position(3, 4))));
            Assert.True(game.TryMakeMove(new Move(new Position(7, 1), new Position(5, 2))));
            Assert.True(game.TryMakeMove(new Move(new Position(0, 1), new Position(2, 2))));
            Assert.True(game.TryMakeMove(new Move(new Position(7, 5), new Position(4, 2))));
            Assert.True(game.TryMakeMove(new Move(new Position(0, 5), new Position(3, 2))));
            Assert.True(game.TryMakeMove(new Move(new Position(6, 3), new Position(5, 3))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 3), new Position(2, 3))));
            Assert.True(game.TryMakeMove(new Move(new Position(7, 6), new Position(5, 7))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 0), new Position(2, 0))));
            Assert.True(game.TryMakeMove(new Move(new Position(6, 5), new Position(5, 5))));
            Assert.True(game.TryMakeMove(new Move(new Position(2, 0), new Position(3, 0))));

            Assert.DoesNotContain(game.GetLegalMoves(new Position(7, 4)), move => move.To == new Position(7, 6));
            Assert.False(game.TryMakeMove(new Move(new Position(7, 4), new Position(7, 6))));
        }

        [Fact]
        public void CastlingRightsAreClearedAndRestoredByUndo()
        {
            var board = new BoardState();

            board.MakeMove(new Move(new Position(7, 4), new Position(6, 4)));

            Assert.False(board.WhiteCanCastleKingSide);
            Assert.False(board.WhiteCanCastleQueenSide);

            board.Undo();

            Assert.True(board.WhiteCanCastleKingSide);
            Assert.True(board.WhiteCanCastleQueenSide);

            board.MakeMove(new Move(new Position(7, 7), new Position(6, 7)));

            Assert.False(board.WhiteCanCastleKingSide);
            Assert.True(board.WhiteCanCastleQueenSide);

            board.Undo();

            Assert.True(board.WhiteCanCastleKingSide);
            Assert.True(board.WhiteCanCastleQueenSide);
        }

        [Fact]
        public void CapturingRookOnHomeSquareClearsAndUndoRestoresBothCastlingRights()
        {
            var board = new BoardState();

            board.MakeMove(new Move(new Position(0, 0), new Position(7, 0)));

            Assert.False(board.BlackCanCastleQueenSide);
            Assert.False(board.WhiteCanCastleQueenSide);

            board.Undo();

            Assert.True(board.BlackCanCastleQueenSide);
            Assert.True(board.WhiteCanCastleQueenSide);
            Assert.Equal(PieceType.Rook, board.GetPiece(new Position(0, 0)).Type);
            Assert.Equal(PieceType.Rook, board.GetPiece(new Position(7, 0)).Type);
        }

        [Fact]
        public void CastlingKingSide()
        {
            var g = new ChessGame();

            // White: e2->e4, clearing the bishop's diagonal
            Assert.True(g.TryMakeMove(new Move(new Position(6, 4), new Position(4, 4))));

            // Black: a7->a6
            Assert.True(g.TryMakeMove(new Move(new Position(1, 0), new Position(2, 0))));

            // White: g1->f3
            Assert.True(g.TryMakeMove(new Move(new Position(7, 6), new Position(5, 5))));

            // Black: a6->a5
            Assert.True(g.TryMakeMove(new Move(new Position(2, 0), new Position(3, 0))));

            // White: f1->c4, clearing the king's path
            Assert.True(g.TryMakeMove(new Move(new Position(7, 5), new Position(4, 2))));

            // Black: h7->h6
            Assert.True(g.TryMakeMove(new Move(new Position(1, 7), new Position(2, 7))));

            // White castle king side: e1->g1
            Assert.True(g.TryMakeMove(new Move(new Position(7, 4), new Position(7, 6))));

            Assert.Equal(PieceType.King, g.Board.GetPiece(new Position(7, 6)).Type);
            Assert.Equal(PieceType.Rook, g.Board.GetPiece(new Position(7, 5)).Type);
        }

        [Fact]
        public void WhiteCanCastleQueenSide()
        {
            var game = new ChessGame();

            Assert.True(game.TryMakeMove(new Move(new Position(6, 3), new Position(4, 3))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 4), new Position(3, 4))));
            Assert.True(game.TryMakeMove(new Move(new Position(7, 1), new Position(5, 2))));
            Assert.True(game.TryMakeMove(new Move(new Position(0, 6), new Position(2, 5))));
            Assert.True(game.TryMakeMove(new Move(new Position(7, 2), new Position(4, 5))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 0), new Position(2, 0))));
            Assert.True(game.TryMakeMove(new Move(new Position(7, 3), new Position(6, 3))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 7), new Position(2, 7))));

            Assert.True(game.TryMakeMove(new Move(new Position(7, 4), new Position(7, 2))));

            Assert.Equal(PieceType.King, game.Board.GetPiece(new Position(7, 2)).Type);
            Assert.Equal(PieceType.Rook, game.Board.GetPiece(new Position(7, 3)).Type);
        }

        [Fact]
        public void BlackCanCastleKingSide()
        {
            var game = new ChessGame();

            Assert.True(game.TryMakeMove(new Move(new Position(6, 4), new Position(4, 4))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 4), new Position(3, 4))));
            Assert.True(game.TryMakeMove(new Move(new Position(7, 6), new Position(5, 5))));
            Assert.True(game.TryMakeMove(new Move(new Position(0, 6), new Position(2, 5))));
            Assert.True(game.TryMakeMove(new Move(new Position(7, 5), new Position(4, 2))));
            Assert.True(game.TryMakeMove(new Move(new Position(0, 5), new Position(3, 2))));
            Assert.True(game.TryMakeMove(new Move(new Position(6, 3), new Position(5, 3))));

            Assert.True(game.TryMakeMove(new Move(new Position(0, 4), new Position(0, 6))));

            Assert.Equal(PieceType.King, game.Board.GetPiece(new Position(0, 6)).Type);
            Assert.Equal(PieceType.Rook, game.Board.GetPiece(new Position(0, 5)).Type);
        }

        [Fact]
        public void BlackCanCastleQueenSide()
        {
            var game = new ChessGame();

            Assert.True(game.TryMakeMove(new Move(new Position(6, 4), new Position(4, 4))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 3), new Position(3, 3))));
            Assert.True(game.TryMakeMove(new Move(new Position(7, 6), new Position(5, 5))));
            Assert.True(game.TryMakeMove(new Move(new Position(0, 1), new Position(2, 2))));
            Assert.True(game.TryMakeMove(new Move(new Position(6, 3), new Position(5, 3))));
            Assert.True(game.TryMakeMove(new Move(new Position(0, 2), new Position(3, 5))));
            Assert.True(game.TryMakeMove(new Move(new Position(7, 5), new Position(6, 4))));
            Assert.True(game.TryMakeMove(new Move(new Position(0, 3), new Position(1, 3))));
            Assert.True(game.TryMakeMove(new Move(new Position(6, 7), new Position(5, 7))));

            Assert.True(game.TryMakeMove(new Move(new Position(0, 4), new Position(0, 2))));

            Assert.Equal(PieceType.King, game.Board.GetPiece(new Position(0, 2)).Type);
            Assert.Equal(PieceType.Rook, game.Board.GetPiece(new Position(0, 3)).Type);
        }

        [Fact]
        public void Foolsmate_CheckmateDetected()
        {
            var g = new ChessGame();
            // 1. f3
            Assert.True(g.TryMakeMove(new Move(new Position(6,5), new Position(5,5))));
            // ... e5
            Assert.True(g.TryMakeMove(new Move(new Position(1,4), new Position(3,4))));
            // 2. g4
            Assert.True(g.TryMakeMove(new Move(new Position(6,6), new Position(4,6))));
            // ... Qh4#
            Assert.True(g.TryMakeMove(new Move(new Position(0,3), new Position(4,7))));
            Assert.Equal(GameStatus.Checkmate, g.Status);
        }

        [Fact]
        public void UndoRestoresState()
        {
            var g = new ChessGame();
            var from = new Position(6,4); // e2
            var to = new Position(4,4);   // e4
            Assert.True(g.TryMakeMove(new Move(from, to)));
            g.Undo();
            var p = g.Board.GetPiece(from);
            Assert.False(p.IsEmpty);
            Assert.Equal(PieceType.Pawn, p.Type);
            Assert.Equal(PieceColor.White, p.Color);
        }
    }
}
