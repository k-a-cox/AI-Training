using System;
using System.Linq;
using Xunit;
using AiAssistedChess.Engine;
using AiAssistedChess.UI;

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
        public void InitialPositionHasBlockedSlidingPiecesAndEdgeKnightMoves()
        {
            var game = new ChessGame();

            Assert.Empty(game.GetLegalMoves(new Position(7, 0)));
            Assert.Empty(game.GetLegalMoves(new Position(7, 2)));
            Assert.Empty(game.GetLegalMoves(new Position(7, 3)));
            Assert.Equal(new[] { new Position(5, 0), new Position(5, 2) }, game.GetLegalMoves(new Position(7, 1)).Select(move => move.To).OrderBy(position => position.Col));
        }

        [Fact]
        public void PawnCannotMoveSidewaysOrCaptureAnEmptySquare()
        {
            var game = new ChessGame();
            var pawnMoves = game.GetLegalMoves(new Position(6, 0)).Select(move => move.To).ToList();

            Assert.Contains(new Position(5, 0), pawnMoves);
            Assert.Contains(new Position(4, 0), pawnMoves);
            Assert.DoesNotContain(new Position(6, 1), pawnMoves);
            Assert.DoesNotContain(new Position(5, 1), pawnMoves);
            Assert.DoesNotContain(new Position(7, 0), pawnMoves);
        }

        [Fact]
        public void BishopCanMoveDiagonallyAfterPathIsCleared()
        {
            var game = new ChessGame();

            Assert.True(game.TryMakeMove(new Move(new Position(6, 4), new Position(4, 4))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 0), new Position(2, 0))));

            var bishopDestinations = game.GetLegalMoves(new Position(7, 5)).Select(move => move.To).ToList();

            Assert.Contains(new Position(6, 4), bishopDestinations);
            Assert.Contains(new Position(5, 3), bishopDestinations);
            Assert.Contains(new Position(4, 2), bishopDestinations);
            Assert.Contains(new Position(3, 1), bishopDestinations);
            Assert.Contains(new Position(2, 0), bishopDestinations);
        }

        [Fact]
        public void QueenCanMoveAlongClearedFile()
        {
            var game = new ChessGame();

            Assert.True(game.TryMakeMove(new Move(new Position(6, 3), new Position(4, 3))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 0), new Position(2, 0))));

            var queenDestinations = game.GetLegalMoves(new Position(7, 3)).Select(move => move.To).ToList();

            Assert.Contains(new Position(6, 3), queenDestinations);
            Assert.Contains(new Position(5, 3), queenDestinations);
            Assert.DoesNotContain(new Position(4, 3), queenDestinations);
        }

        [Fact]
        public void RookCanMoveAlongFileAfterItsPawnMoves()
        {
            var game = new ChessGame();

            Assert.True(game.TryMakeMove(new Move(new Position(6, 0), new Position(4, 0))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 7), new Position(2, 7))));

            var rookDestinations = game.GetLegalMoves(new Position(7, 0)).Select(move => move.To).ToList();

            Assert.Contains(new Position(6, 0), rookDestinations);
            Assert.Contains(new Position(5, 0), rookDestinations);
            Assert.DoesNotContain(new Position(4, 0), rookDestinations);
        }

        [Fact]
        public void KingCanStepIntoSquareClearedByItsPawn()
        {
            var game = new ChessGame();

            Assert.True(game.TryMakeMove(new Move(new Position(6, 4), new Position(4, 4))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 0), new Position(2, 0))));

            var kingDestinations = game.GetLegalMoves(new Position(7, 4)).Select(move => move.To).ToList();

            Assert.Contains(new Position(6, 4), kingDestinations);
            Assert.DoesNotContain(new Position(6, 3), kingDestinations);
            Assert.DoesNotContain(new Position(6, 5), kingDestinations);
        }

        [Fact]
        public void PawnAndQueenCanCaptureOnAnOpenFile()
        {
            var game = new ChessGame();

            Assert.True(game.TryMakeMove(new Move(new Position(6, 4), new Position(4, 4))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 3), new Position(3, 3))));
            Assert.True(game.TryMakeMove(new Move(new Position(4, 4), new Position(3, 3))));
            Assert.True(game.TryMakeMove(new Move(new Position(0, 3), new Position(3, 3))));

            Assert.Equal(PieceType.Queen, game.Board.GetPiece(new Position(3, 3)).Type);
            Assert.Equal(PieceColor.Black, game.Board.GetPiece(new Position(3, 3)).Color);
            Assert.True(game.Board.GetPiece(new Position(4, 4)).IsEmpty);
            Assert.True(game.TryMakeMove(new Move(new Position(7, 1), new Position(5, 2))));
            Assert.True(game.TryMakeMove(new Move(new Position(1, 0), new Position(2, 0))));
            Assert.True(game.TryMakeMove(new Move(new Position(5, 2), new Position(3, 3))));
            Assert.Equal(PieceType.Knight, game.Board.GetPiece(new Position(3, 3)).Type);
            Assert.True(game.Board.GetPiece(new Position(0, 3)).IsEmpty);
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
        public void UndoRestoresEnPassantTargetAfterAnInterveningMove()
        {
            var game = new ChessGame();

            PlayMoves(game, "e2-e4", "a7-a6", "e4-e5", "d7-d5", "a2-a3");

            Assert.Null(game.Board.EnPassantTarget);
            Assert.Equal(PieceColor.Black, game.CurrentPlayer);

            game.Undo();

            var enPassantTarget = new Position(2, 3);
            var enPassantMove = new Move(new Position(3, 4), enPassantTarget);

            Assert.Equal(PieceColor.White, game.CurrentPlayer);
            Assert.Equal(enPassantTarget, game.Board.EnPassantTarget);
            Assert.Contains(game.GetLegalMoves(new Position(3, 4)), move => move.To == enPassantTarget);
            Assert.True(game.TryMakeMove(enPassantMove));
            Assert.True(game.Board.GetPiece(new Position(3, 3)).IsEmpty);
            Assert.Equal(new ChessPiece(PieceType.Pawn, PieceColor.White), game.Board.GetPiece(enPassantTarget));
        }

        [Fact]
        public void LegalMoveSimulationDoesNotMutateLiveBoardOrUndoHistory()
        {
            var game = new ChessGame();

            PlayMoves(game, "e2-e4", "a7-a6", "e4-e5", "d7-d5");

            var enPassantTarget = new Position(2, 3);
            var whitePawnPosition = new Position(3, 4);
            var blackPawnPosition = new Position(3, 3);

            Assert.Contains(game.GetLegalMoves(whitePawnPosition), move => move.To == enPassantTarget);
            Assert.NotEmpty(game.GetAllLegalMoves(PieceColor.White));
            Assert.NotEmpty(game.GetAllLegalMoves(PieceColor.Black));

            Assert.Equal(PieceColor.White, game.CurrentPlayer);
            Assert.Equal(GameStatus.Active, game.Status);
            Assert.Equal(enPassantTarget, game.Board.EnPassantTarget);
            Assert.Equal(new ChessPiece(PieceType.Pawn, PieceColor.White), game.Board.GetPiece(whitePawnPosition));
            Assert.Equal(new ChessPiece(PieceType.Pawn, PieceColor.Black), game.Board.GetPiece(blackPawnPosition));
            Assert.True(game.Board.GetPiece(enPassantTarget).IsEmpty);

            game.Undo();

            Assert.Equal(PieceColor.Black, game.CurrentPlayer);
            Assert.Null(game.Board.EnPassantTarget);
            Assert.Equal(new ChessPiece(PieceType.Pawn, PieceColor.Black), game.Board.GetPiece(new Position(1, 3)));
            Assert.Equal(new ChessPiece(PieceType.Pawn, PieceColor.White), game.Board.GetPiece(whitePawnPosition));
            Assert.True(game.Board.GetPiece(blackPawnPosition).IsEmpty);
        }

        [Fact]
        public void BoardStateCloneHasIndependentBoardAndNoCopiedUndoHistory()
        {
            var board = new BoardState();

            board.MakeMove(new Move(new Position(6, 4), new Position(4, 4)));

            var clonedBoard = board.Clone();

            clonedBoard.Undo();

            Assert.Equal(PieceColor.Black, clonedBoard.CurrentPlayer);
            Assert.Equal(new Position(5, 4), clonedBoard.EnPassantTarget);
            Assert.Equal(new ChessPiece(PieceType.Pawn, PieceColor.White), clonedBoard.GetPiece(new Position(4, 4)));
            Assert.Equal(PieceColor.Black, board.CurrentPlayer);
            Assert.Equal(new Position(5, 4), board.EnPassantTarget);

            clonedBoard.MakeMove(new Move(new Position(1, 3), new Position(3, 3)));

            Assert.Equal(new ChessPiece(PieceType.Pawn, PieceColor.Black), clonedBoard.GetPiece(new Position(3, 3)));
            Assert.True(board.GetPiece(new Position(3, 3)).IsEmpty);

            board.Undo();

            Assert.Equal(PieceColor.White, board.CurrentPlayer);
            Assert.Null(board.EnPassantTarget);
            Assert.Equal(new ChessPiece(PieceType.Pawn, PieceColor.White), board.GetPiece(new Position(6, 4)));
            Assert.True(board.GetPiece(new Position(4, 4)).IsEmpty);
            Assert.Equal(new ChessPiece(PieceType.Pawn, PieceColor.White), clonedBoard.GetPiece(new Position(4, 4)));
        }

        [Fact]
        public void WhiteEnPassantIsRejectedWhenItExposesKingToRookCheck()
        {
            var game = MakeBoard(
                PieceColor.Black,
                (new Position(7, 4), new ChessPiece(PieceType.King, PieceColor.White)),
                (new Position(0, 0), new ChessPiece(PieceType.King, PieceColor.Black)),
                (new Position(0, 4), new ChessPiece(PieceType.Rook, PieceColor.Black)),
                (new Position(3, 4), new ChessPiece(PieceType.Pawn, PieceColor.White)),
                (new Position(1, 3), new ChessPiece(PieceType.Pawn, PieceColor.Black)));

            Assert.True(game.TryMakeMove(new Move(new Position(1, 3), new Position(3, 3))));

            var enPassantTarget = new Position(2, 3);
            var attemptedMove = new Move(new Position(3, 4), enPassantTarget);

            Assert.Equal(enPassantTarget, game.Board.EnPassantTarget);
            Assert.DoesNotContain(game.GetLegalMoves(new Position(3, 4)), move => move.To == enPassantTarget);
            Assert.False(game.TryMakeMove(attemptedMove));

            Assert.Equal(PieceColor.White, game.CurrentPlayer);
            Assert.Equal(GameStatus.Active, game.Status);
            Assert.Equal(enPassantTarget, game.Board.EnPassantTarget);
            Assert.Equal(new ChessPiece(PieceType.Pawn, PieceColor.White), game.Board.GetPiece(new Position(3, 4)));
            Assert.Equal(new ChessPiece(PieceType.Pawn, PieceColor.Black), game.Board.GetPiece(new Position(3, 3)));
            Assert.True(game.Board.GetPiece(enPassantTarget).IsEmpty);
        }

        [Fact]
        public void BlackEnPassantIsRejectedWhenItExposesKingToRookCheck()
        {
            var game = MakeBoard(
                PieceColor.White,
                (new Position(7, 0), new ChessPiece(PieceType.King, PieceColor.White)),
                (new Position(7, 4), new ChessPiece(PieceType.Rook, PieceColor.White)),
                (new Position(0, 4), new ChessPiece(PieceType.King, PieceColor.Black)),
                (new Position(4, 4), new ChessPiece(PieceType.Pawn, PieceColor.Black)),
                (new Position(6, 3), new ChessPiece(PieceType.Pawn, PieceColor.White)));

            Assert.True(game.TryMakeMove(new Move(new Position(6, 3), new Position(4, 3))));

            var enPassantTarget = new Position(5, 3);
            var attemptedMove = new Move(new Position(4, 4), enPassantTarget);

            Assert.Equal(enPassantTarget, game.Board.EnPassantTarget);
            Assert.DoesNotContain(game.GetLegalMoves(new Position(4, 4)), move => move.To == enPassantTarget);
            Assert.False(game.TryMakeMove(attemptedMove));

            Assert.Equal(PieceColor.Black, game.CurrentPlayer);
            Assert.Equal(GameStatus.Active, game.Status);
            Assert.Equal(enPassantTarget, game.Board.EnPassantTarget);
            Assert.Equal(new ChessPiece(PieceType.Pawn, PieceColor.Black), game.Board.GetPiece(new Position(4, 4)));
            Assert.Equal(new ChessPiece(PieceType.Pawn, PieceColor.White), game.Board.GetPiece(new Position(4, 3)));
            Assert.True(game.Board.GetPiece(enPassantTarget).IsEmpty);
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
        public void UndoAcrossMultipleMovesRestoresKingAndCastlingRights()
        {
            var game = new ChessGame();

            PlayMoves(game, "e2-e4", "a7-a6", "e1-e2", "a6-a5");

            Assert.Equal(new ChessPiece(PieceType.King, PieceColor.White), game.Board.GetPiece(new Position(6, 4)));
            Assert.False(game.Board.WhiteCanCastleKingSide);
            Assert.False(game.Board.WhiteCanCastleQueenSide);

            game.Undo();

            Assert.Equal(PieceColor.Black, game.CurrentPlayer);
            Assert.Equal(new ChessPiece(PieceType.King, PieceColor.White), game.Board.GetPiece(new Position(6, 4)));
            Assert.False(game.Board.WhiteCanCastleKingSide);
            Assert.False(game.Board.WhiteCanCastleQueenSide);

            game.Undo();

            Assert.Equal(PieceColor.White, game.CurrentPlayer);
            Assert.Equal(GameStatus.Active, game.Status);
            Assert.Equal(new ChessPiece(PieceType.King, PieceColor.White), game.Board.GetPiece(new Position(7, 4)));
            Assert.True(game.Board.GetPiece(new Position(6, 4)).IsEmpty);
            Assert.True(game.Board.WhiteCanCastleKingSide);
            Assert.True(game.Board.WhiteCanCastleQueenSide);
        }

        [Fact]
        public void PawnAttackSetsCheckStatusWhileLegalResponsesRemain()
        {
            var game = new ChessGame();

            PlayMoves(game, "d2-d4", "e7-e5", "a2-a3", "e5-d4", "a3-a4", "d4-d3", "h2-h3", "d3-d2");

            AssertCheckWithLegalResponses(game);
        }

        [Fact]
        public void KnightAttackSetsCheckStatusWhileLegalResponsesRemain()
        {
            var game = new ChessGame();

            PlayMoves(game, "d2-d4", "b8-c6", "a2-a3", "c6-e5", "a3-a4", "e5-d3");

            AssertCheckWithLegalResponses(game);
        }

        [Fact]
        public void BishopAttackSetsCheckStatusWhileLegalResponsesRemain()
        {
            var game = new ChessGame();

            PlayMoves(game, "d2-d4", "e7-e6", "a2-a3", "f8-b4");

            AssertCheckWithLegalResponses(game);
        }

        [Fact]
        public void RookAttackSetsCheckStatusWhileLegalResponsesRemain()
        {
            var game = new ChessGame();

            PlayMoves(game, "e2-e4", "e7-e5", "d2-d4", "e5-d4", "e4-e5", "d7-d6", "e5-d6", "a7-a6", "g1-f3", "a6-a5", "f1-c4", "h7-h6", "e1-g1", "h6-h5", "f1-e1");

            AssertCheckWithLegalResponses(game);
        }

        [Fact]
        public void QueenAttackSetsCheckStatusWhileLegalResponsesRemain()
        {
            var game = new ChessGame();

            PlayMoves(game, "f2-f4", "e7-e5", "g1-f3", "a7-a6", "e2-e4", "a6-a5", "f1-c4", "d8-h4");

            AssertCheckWithLegalResponses(game);
        }

        [Fact]
        public void KingAttackSetsCheckStatusWhileLegalResponsesRemain()
        {
            var game = MakeBoard(
                PieceColor.White,
                (new Position(4, 4), new ChessPiece(PieceType.King, PieceColor.White)),
                (new Position(3, 4), new ChessPiece(PieceType.King, PieceColor.Black)));

            AssertCheckWithLegalResponses(game);
        }

        [Fact]
        public void PieceBetweenRookAndKingPreventsCheckStatus()
        {
            var game = new ChessGame();

            PlayMoves(game, "e2-e4", "e7-e5", "g1-f3", "a7-a6", "f1-c4", "a6-a5", "e1-g1", "h7-h6", "f1-e1", "b7-b6");

            Assert.Equal(GameStatus.Active, game.Status);
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
        public void ScholarsMate_CheckmateDetectedAndFurtherMovesRejected()
        {
            var game = new ChessGame();

            PlayMoves(game, "e2-e4", "e7-e5", "f1-c4", "b8-c6", "d1-h5", "g8-f6", "h5-f7");

            Assert.Equal(GameStatus.Checkmate, game.Status);
            Assert.False(game.TryMakeMove(new Move(new Position(0, 1), new Position(2, 2))));
        }

        [Fact]
        public void StalemateDetectedWithBlackKingInCorner()
        {
            var game = MakeBoard(
                PieceColor.Black,
                (new Position(0, 0), new ChessPiece(PieceType.King, PieceColor.Black)),
                (new Position(2, 2), new ChessPiece(PieceType.King, PieceColor.White)),
                (new Position(2, 1), new ChessPiece(PieceType.Queen, PieceColor.White)));

            Assert.Equal(GameStatus.Stalemate, game.Status);
            Assert.Empty(game.GetAllLegalMoves(PieceColor.Black));
            Assert.False(game.TryMakeMove(new Move(new Position(0, 0), new Position(0, 1))));
        }

        [Fact]
        public void StalemateDetectedInMirroredCorner()
        {
            var game = MakeBoard(
                PieceColor.Black,
                (new Position(0, 7), new ChessPiece(PieceType.King, PieceColor.Black)),
                (new Position(2, 5), new ChessPiece(PieceType.King, PieceColor.White)),
                (new Position(2, 6), new ChessPiece(PieceType.Queen, PieceColor.White)));

            Assert.Equal(GameStatus.Stalemate, game.Status);
            Assert.Empty(game.GetAllLegalMoves(PieceColor.Black));
            Assert.False(game.TryMakeMove(new Move(new Position(0, 7), new Position(0, 6))));
        }

        [Fact]
        public void LoadPositionSetsCustomBoardAndSideToMove()
        {
            var game = new ChessGame();
            var pieces = new[]
            {
                (new Position(0, 0), new ChessPiece(PieceType.King, PieceColor.Black)),
                (new Position(2, 2), new ChessPiece(PieceType.King, PieceColor.White)),
                (new Position(2, 1), new ChessPiece(PieceType.Queen, PieceColor.White))
            };

            Assert.True(game.LoadPosition(pieces, PieceColor.Black));

            Assert.Equal(PieceColor.Black, game.CurrentPlayer);
            Assert.Equal(GameStatus.Stalemate, game.Status);
            Assert.Equal(PieceType.King, game.Board.GetPiece(new Position(0, 0)).Type);
            Assert.True(game.Board.GetPiece(new Position(7, 4)).IsEmpty);
        }

        [Fact]
        public void LoadPositionRejectsInvalidPositionWithoutChangingBoard()
        {
            var game = new ChessGame();
            var invalidPieces = new[]
            {
                (new Position(0, 0), new ChessPiece(PieceType.King, PieceColor.Black)),
                (new Position(2, 2), new ChessPiece(PieceType.King, PieceColor.White)),
                (new Position(2, 2), new ChessPiece(PieceType.Queen, PieceColor.White))
            };

            Assert.False(game.LoadPosition(invalidPieces, PieceColor.Black));

            Assert.Equal(PieceColor.White, game.CurrentPlayer);
            Assert.Equal(PieceType.King, game.Board.GetPiece(new Position(7, 4)).Type);
            Assert.Equal(PieceType.King, game.Board.GetPiece(new Position(0, 4)).Type);
        }

        [Fact]
        public void CustomPositionParserAcceptsGenericPieceAndTurnSyntax()
        {
            Assert.True(CustomBoardPositionParser.TryParse(
                "white:king=c6,queen=b6; black:king=a8; turn=black",
                out var position,
                out var errorMessage));

            Assert.Empty(errorMessage);
            Assert.NotNull(position);
            Assert.Equal(PieceColor.Black, position!.CurrentPlayer);
            Assert.Equal(3, position.Pieces.Count);
        }

        [Theory]
        [InlineData("white:king=c6,queen=b6; black:king=a8; turn=black; turn=white")]
        [InlineData("white:king=c6,queen=b6; black:king=a8,rook=b6; turn=black")]
        [InlineData("white:king=c6,queen=z9; black:king=a8; turn=black")]
        [InlineData("white:king=c6,dragon=b6; black:king=a8; turn=black")]
        [InlineData("white:king=c6,queen=b6; black:king=a8; turn=red")]
        public void CustomPositionParserRejectsInvalidPositionSyntax(string input)
        {
            Assert.False(CustomBoardPositionParser.TryParse(input, out _, out var errorMessage));

            Assert.NotEmpty(errorMessage);
        }

        [Theory]
        [InlineData(PieceType.Queen)]
        [InlineData(PieceType.Rook)]
        [InlineData(PieceType.Bishop)]
        [InlineData(PieceType.Knight)]
        public void PawnPromotesToEachLegalPieceType(PieceType promotionType)
        {
            var game = MakeBoard(
                PieceColor.White,
                (new Position(7, 7), new ChessPiece(PieceType.King, PieceColor.White)),
                (new Position(0, 7), new ChessPiece(PieceType.King, PieceColor.Black)),
                (new Position(1, 0), new ChessPiece(PieceType.Pawn, PieceColor.White)));

            Assert.True(game.TryMakeMove(new Move(new Position(1, 0), new Position(0, 0), promotionType)));

            Assert.Equal(promotionType, game.Board.GetPiece(new Position(0, 0)).Type);
            Assert.Equal(PieceColor.White, game.Board.GetPiece(new Position(0, 0)).Color);
            Assert.True(game.Board.GetPiece(new Position(1, 0)).IsEmpty);
        }

        [Fact]
        public void PawnCanPromoteByCaptureAndUndoRestoresPawnAndCapturedPiece()
        {
            var game = MakeBoard(
                PieceColor.White,
                (new Position(7, 7), new ChessPiece(PieceType.King, PieceColor.White)),
                (new Position(0, 7), new ChessPiece(PieceType.King, PieceColor.Black)),
                (new Position(1, 0), new ChessPiece(PieceType.Pawn, PieceColor.White)),
                (new Position(0, 1), new ChessPiece(PieceType.Rook, PieceColor.Black)));

            Assert.True(game.TryMakeMove(new Move(new Position(1, 0), new Position(0, 1), PieceType.Knight)));

            Assert.Equal(PieceType.Knight, game.Board.GetPiece(new Position(0, 1)).Type);
            Assert.Equal(PieceColor.White, game.Board.GetPiece(new Position(0, 1)).Color);

            game.Undo();

            Assert.Equal(PieceType.Pawn, game.Board.GetPiece(new Position(1, 0)).Type);
            Assert.Equal(PieceColor.White, game.Board.GetPiece(new Position(1, 0)).Color);
            Assert.Equal(PieceType.Rook, game.Board.GetPiece(new Position(0, 1)).Type);
            Assert.Equal(PieceColor.Black, game.Board.GetPiece(new Position(0, 1)).Color);
            Assert.Equal(PieceColor.White, game.CurrentPlayer);
        }

        [Theory]
        [InlineData(PieceType.Pawn)]
        [InlineData(PieceType.King)]
        [InlineData(PieceType.None)]
        public void PawnRejectsInvalidPromotionType(PieceType promotionType)
        {
            var game = MakeBoard(
                PieceColor.White,
                (new Position(7, 7), new ChessPiece(PieceType.King, PieceColor.White)),
                (new Position(0, 7), new ChessPiece(PieceType.King, PieceColor.Black)),
                (new Position(1, 0), new ChessPiece(PieceType.Pawn, PieceColor.White)));

            Assert.False(game.TryMakeMove(new Move(new Position(1, 0), new Position(0, 0), promotionType)));

            Assert.Equal(PieceType.Pawn, game.Board.GetPiece(new Position(1, 0)).Type);
            Assert.True(game.Board.GetPiece(new Position(0, 0)).IsEmpty);
            Assert.Equal(PieceColor.White, game.CurrentPlayer);
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

        [Fact]
        public void UndoAcrossSeveralMovesRestoresEnPassantCaptureAndState()
        {
            var game = new ChessGame();

            PlayMoves(game, "e2-e4", "a7-a6", "e4-e5", "d7-d5", "e5-d6");

            Assert.Equal(new ChessPiece(PieceType.Pawn, PieceColor.White), game.Board.GetPiece(new Position(2, 3)));
            Assert.True(game.Board.GetPiece(new Position(3, 3)).IsEmpty);
            Assert.Null(game.Board.EnPassantTarget);
            Assert.Equal(PieceColor.Black, game.CurrentPlayer);
            Assert.Equal(GameStatus.Active, game.Status);

            game.Undo();

            Assert.Equal(PieceColor.White, game.CurrentPlayer);
            Assert.Equal(GameStatus.Active, game.Status);
            Assert.Equal(new Position(2, 3), game.Board.EnPassantTarget);
            Assert.Equal(new ChessPiece(PieceType.Pawn, PieceColor.Black), game.Board.GetPiece(new Position(3, 3)));
            Assert.Equal(new ChessPiece(PieceType.Pawn, PieceColor.White), game.Board.GetPiece(new Position(3, 4)));
            Assert.Contains(game.GetLegalMoves(new Position(3, 4)), move => move.To == new Position(2, 3));

            game.Undo();

            Assert.Equal(PieceColor.Black, game.CurrentPlayer);
            Assert.Null(game.Board.EnPassantTarget);
            Assert.Equal(new ChessPiece(PieceType.Pawn, PieceColor.Black), game.Board.GetPiece(new Position(1, 3)));
            Assert.Equal(new ChessPiece(PieceType.Pawn, PieceColor.White), game.Board.GetPiece(new Position(3, 4)));

            game.Undo();

            Assert.Equal(PieceColor.White, game.CurrentPlayer);
            Assert.Equal(GameStatus.Active, game.Status);
            Assert.Null(game.Board.EnPassantTarget);
            Assert.Equal(new ChessPiece(PieceType.Pawn, PieceColor.White), game.Board.GetPiece(new Position(4, 4)));
            Assert.Equal(new ChessPiece(PieceType.Pawn, PieceColor.Black), game.Board.GetPiece(new Position(1, 3)));
        }

        private static void PlayMoves(ChessGame game, params string[] coordinateMoves)
        {
            foreach (var coordinateMove in coordinateMoves)
            {
                var squares = coordinateMove.Split('-');

                var from = ParseSquare(squares[0]);
                var to = ParseSquare(squares[1]);

                Assert.True(game.TryMakeMove(new Move(from, to)), $"Move {coordinateMove} should be legal.");
            }
        }

        private static ChessGame MakeBoard(PieceColor currentPlayer, params (Position Position, ChessPiece Piece)[] pieces)
        {
            var game = new ChessGame();

            game.SetPositionForTesting(pieces, currentPlayer);

            return game;
        }

        private static Position ParseSquare(string algebraicSquare)
        {
            var file = char.ToLowerInvariant(algebraicSquare[0]) - 'a';
            var rank = algebraicSquare[1] - '0';

            return new Position(8 - rank, file);
        }

        private static void AssertCheckWithLegalResponses(ChessGame game)
        {
            Assert.Equal(GameStatus.Check, game.Status);
            Assert.NotEmpty(game.GetAllLegalMoves(game.CurrentPlayer));
        }

    }
}
