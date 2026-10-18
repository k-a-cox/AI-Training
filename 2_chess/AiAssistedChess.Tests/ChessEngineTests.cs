using System;
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
