using System;
using System.Windows.Forms;
using AiAssistedChess.Engine;
using AiAssistedChess.UI;

namespace AiAssistedChess
{
    public partial class Form1 : Form
    {
        private readonly ChessGame game = new ChessGame();

        public Form1()
        {
            InitializeComponent();
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
            game.Undo();
            chessBoardPanel.Invalidate();
            UpdateStatusLabels();
        }

        private void ButtonRestart_Click(object? sender, EventArgs e)
        {
            game.Reset();
            chessBoardPanel.Invalidate();
            UpdateStatusLabels();
        }
    }
}
