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
