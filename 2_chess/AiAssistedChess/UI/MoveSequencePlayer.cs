using System;
using System.Collections.Generic;
using System.Linq;
using AiAssistedChess.Engine;

namespace AiAssistedChess.UI
{
    public sealed class MoveSequencePlayer
    {
        private readonly ChessGame game;

        private List<Move> moves = new();

        public int CurrentIndex { get; private set; }

        public int MoveCount => moves.Count;

        public bool IsLoaded => moves.Count > 0;

        public MoveSequencePlayer(ChessGame game)
        {
            this.game = game;
        }

        public bool Load(string input, out string errorMessage)
        {
            var tokens = input.Split(new[] { ',', ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var parsedMoves = new List<Move>();

            if (!tokens.Any())
            {
                errorMessage = "Enter moves such as e2-e4, separated by spaces, commas, or new lines.";
                return false;
            }

            for (var moveIndex = 0; moveIndex < tokens.Length; moveIndex++)
            {
                var token = tokens[moveIndex];

                if (token.Length != 5 || token[2] != '-')
                {
                    errorMessage = $"Move {moveIndex + 1} ('{token}') must use coordinate format such as e2-e4.";
                    return false;
                }

                if (!TryParseSquare(token[0], token[1], out var from) || !TryParseSquare(token[3], token[4], out var to))
                {
                    errorMessage = $"Move {moveIndex + 1} ('{token}') contains an invalid square. Use files a-h and ranks 1-8.";
                    return false;
                }

                parsedMoves.Add(new Move(from, to));
            }

            game.Reset();

            moves = parsedMoves;
            CurrentIndex = 0;
            errorMessage = string.Empty;
            return true;
        }

        public bool StepForward(out string errorMessage)
        {
            if (CurrentIndex >= moves.Count)
            {
                errorMessage = "The sequence is complete.";
                return false;
            }

            if (!game.TryMakeMove(moves[CurrentIndex]))
            {
                errorMessage = $"Move {CurrentIndex + 1} ({moves[CurrentIndex].From}-{moves[CurrentIndex].To}) is illegal in this position.";
                return false;
            }

            CurrentIndex++;
            errorMessage = string.Empty;
            return true;
        }

        public bool StepBack()
        {
            if (CurrentIndex == 0)
            {
                return false;
            }

            game.Undo();
            CurrentIndex--;
            return true;
        }

        public void Clear()
        {
            game.Reset();
            moves.Clear();
            CurrentIndex = 0;
        }

        private static bool TryParseSquare(char fileCharacter, char rankCharacter, out Position position)
        {
            var file = char.ToLowerInvariant(fileCharacter);
            var rank = rankCharacter;

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
