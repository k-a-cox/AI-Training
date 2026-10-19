using System;
using System.Windows.Forms;
using AiAssistedChess.Engine;
using AiAssistedChess.UI;

namespace AiAssistedChess
{
    public partial class Form1 : Form
    {
        private readonly ChessGame game = new ChessGame();

        private readonly MoveSequencePlayer moveSequencePlayer;

        public Form1()
        {
            InitializeComponent();

            moveSequencePlayer = new MoveSequencePlayer(game);
            chessBoardPanel.Game = game;
            chessBoardPanel.GameChanged += OnGameChanged;
            UpdateStatusLabels();
        }

        private void ButtonLoadPosition_Click(object? sender, EventArgs e)
        {
            if (!CustomBoardPositionParser.TryParse(textBoxCustomPosition.Text, out var customPosition, out var errorMessage))
            {
                labelPositionStatus.Text = errorMessage;
                return;
            }

            if (!game.LoadPosition(customPosition!.Pieces, customPosition.CurrentPlayer))
            {
                labelPositionStatus.Text = "Position is invalid: use a valid board with exactly one king per color and no duplicate squares.";
                return;
            }
            moveSequencePlayer.Discard();
            chessBoardPanel.SetInputEnabled(true);
            chessBoardPanel.ClearSelection();

            labelPositionStatus.Text = "Custom position loaded. Manual moves and Undo are enabled.";
            labelSequenceStatus.Text = "Move replay cleared because a custom position was loaded.";

            UpdateSequenceControls();
            UpdateStatusLabels();
        }

        private void OnGameChanged()
        {
            UpdateStatusLabels();
        }

        private void UpdateStatusLabels()
        {
            labelCurrentPlayer.Text = $"Current: {game.CurrentPlayer}";
            labelStatus.Text = $"Status: {game.Status}";
        }

        private void ButtonUndo_Click(object? sender, EventArgs e)
        {
            if (moveSequencePlayer.IsLoaded) return;

            game.Undo();
            chessBoardPanel.Invalidate();
            UpdateStatusLabels();
        }

        private void ButtonRestart_Click(object? sender, EventArgs e)
        {
            moveSequencePlayer.Clear();
            chessBoardPanel.SetInputEnabled(true);
            chessBoardPanel.Invalidate();

            labelSequenceStatus.Text = "Game restarted. Paste a coordinate move sequence to replay it.";

            UpdateSequenceControls();
            UpdateStatusLabels();
        }

        private void ButtonLoadSequence_Click(object? sender, EventArgs e)
        {
            if (!moveSequencePlayer.Load(textBoxMoveSequence.Text, out var errorMessage))
            {
                labelSequenceStatus.Text = errorMessage;
                return;
            }

            chessBoardPanel.SetInputEnabled(false);
            chessBoardPanel.Invalidate();

            labelSequenceStatus.Text = $"Loaded {moveSequencePlayer.MoveCount} moves. Select Next to begin.";

            UpdateSequenceControls();
            UpdateStatusLabels();
        }

        private void ButtonNextMove_Click(object? sender, EventArgs e)
        {
            if (!moveSequencePlayer.StepForward(out var errorMessage))
            {
                labelSequenceStatus.Text = errorMessage;
                return;
            }

            chessBoardPanel.Invalidate();

            labelSequenceStatus.Text = $"Move {moveSequencePlayer.CurrentIndex} of {moveSequencePlayer.MoveCount} played.";

            UpdateSequenceControls();
            UpdateStatusLabels();
        }

        private void ButtonPreviousMove_Click(object? sender, EventArgs e)
        {
            if (!moveSequencePlayer.StepBack()) return;

            chessBoardPanel.Invalidate();

            labelSequenceStatus.Text = $"Back at move {moveSequencePlayer.CurrentIndex} of {moveSequencePlayer.MoveCount}.";

            UpdateSequenceControls();
            UpdateStatusLabels();
        }

        private void ButtonClearSequence_Click(object? sender, EventArgs e)
        {
            moveSequencePlayer.Clear();
            chessBoardPanel.SetInputEnabled(true);
            chessBoardPanel.ClearSelection();

            labelSequenceStatus.Text = "Replay cleared. The board has been reset; you can now play moves manually.";

            UpdateSequenceControls();
            UpdateStatusLabels();
        }

        private void ButtonSwapToManualGame_Click(object? sender, EventArgs e)
        {
            moveSequencePlayer.Discard();
            chessBoardPanel.SetInputEnabled(true);
            chessBoardPanel.ClearSelection();

            labelSequenceStatus.Text = "Switched to manual play at the current position.";

            UpdateSequenceControls();
            UpdateStatusLabels();
        }

        private void UpdateSequenceControls()
        {
            buttonUndo.Enabled = !moveSequencePlayer.IsLoaded;
            buttonPreviousMove.Enabled = moveSequencePlayer.CurrentIndex > 0;
            buttonNextMove.Enabled = moveSequencePlayer.IsLoaded && moveSequencePlayer.CurrentIndex < moveSequencePlayer.MoveCount;
            buttonClearSequence.Enabled = moveSequencePlayer.IsLoaded;
            buttonSwapToManualGame.Enabled = moveSequencePlayer.IsLoaded;
        }
    }
}
