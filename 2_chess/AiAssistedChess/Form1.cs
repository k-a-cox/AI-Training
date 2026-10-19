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
            chessBoardPanel.Invalidate();

            labelSequenceStatus.Text = "Replay cleared. You can now play moves manually.";

            UpdateSequenceControls();
            UpdateStatusLabels();
        }

        private void UpdateSequenceControls()
        {
            buttonUndo.Enabled = !moveSequencePlayer.IsLoaded;
            buttonPreviousMove.Enabled = moveSequencePlayer.CurrentIndex > 0;
            buttonNextMove.Enabled = moveSequencePlayer.IsLoaded && moveSequencePlayer.CurrentIndex < moveSequencePlayer.MoveCount;
            buttonClearSequence.Enabled = moveSequencePlayer.IsLoaded;
        }
    }
}
