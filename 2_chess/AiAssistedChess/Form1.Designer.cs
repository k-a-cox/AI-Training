namespace AiAssistedChess
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private AiAssistedChess.UI.ChessboardPanel chessBoardPanel;
        private System.Windows.Forms.Label labelCurrentPlayer;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonUndo;
        private System.Windows.Forms.Button buttonRestart;
        private System.Windows.Forms.TextBox textBoxMoveSequence;
        private System.Windows.Forms.Button buttonLoadSequence;
        private System.Windows.Forms.Button buttonPreviousMove;
        private System.Windows.Forms.Button buttonNextMove;
        private System.Windows.Forms.Button buttonClearSequence;
        private System.Windows.Forms.Button buttonSwapToManualGame;
        private System.Windows.Forms.Label labelSequenceStatus;
        private System.Windows.Forms.TextBox textBoxCustomPosition;
        private System.Windows.Forms.Button buttonLoadPosition;
        private System.Windows.Forms.Label labelPositionStatus;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Text = "Ai Assisted Chess";
            this.ClientSize = new System.Drawing.Size(1100, 660);
            this.chessBoardPanel = new AiAssistedChess.UI.ChessboardPanel();
            this.labelCurrentPlayer = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonUndo = new System.Windows.Forms.Button();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.textBoxMoveSequence = new System.Windows.Forms.TextBox();
            this.buttonLoadSequence = new System.Windows.Forms.Button();
            this.buttonPreviousMove = new System.Windows.Forms.Button();
            this.buttonNextMove = new System.Windows.Forms.Button();
            this.buttonClearSequence = new System.Windows.Forms.Button();
            this.buttonSwapToManualGame = new System.Windows.Forms.Button();
            this.labelSequenceStatus = new System.Windows.Forms.Label();
            this.textBoxCustomPosition = new System.Windows.Forms.TextBox();
            this.buttonLoadPosition = new System.Windows.Forms.Button();
            this.labelPositionStatus = new System.Windows.Forms.Label();

            // chessBoardPanel
            this.chessBoardPanel.Location = new System.Drawing.Point(20, 30);
            this.chessBoardPanel.Size = new System.Drawing.Size(580, 580);

            // labelCurrentPlayer
            this.labelCurrentPlayer.AutoSize = true;
            this.labelCurrentPlayer.Location = new System.Drawing.Point(630, 40);
            this.labelCurrentPlayer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelCurrentPlayer.Text = "Current: " ;

            // labelStatus
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(630, 80);
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelStatus.Text = "Status: ";

            // buttonUndo
            this.buttonUndo.Location = new System.Drawing.Point(770, 40);
            this.buttonUndo.Size = new System.Drawing.Size(100, 30);
            this.buttonUndo.Text = "Undo";
            this.buttonUndo.Click += new System.EventHandler(this.ButtonUndo_Click);

            // buttonRestart
            this.buttonRestart.Location = new System.Drawing.Point(880, 40);
            this.buttonRestart.Size = new System.Drawing.Size(100, 30);
            this.buttonRestart.Text = "Restart";
            this.buttonRestart.Click += new System.EventHandler(this.ButtonRestart_Click);

            // textBoxMoveSequence
            this.textBoxMoveSequence.Location = new System.Drawing.Point(630, 130);
            this.textBoxMoveSequence.Multiline = true;
            this.textBoxMoveSequence.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMoveSequence.Size = new System.Drawing.Size(430, 180);
            this.textBoxMoveSequence.PlaceholderText = "e2-e4, e7-e5, g1-f3";

            // buttonLoadSequence
            this.buttonLoadSequence.Location = new System.Drawing.Point(630, 325);
            this.buttonLoadSequence.Size = new System.Drawing.Size(100, 32);
            this.buttonLoadSequence.Text = "Load moves";
            this.buttonLoadSequence.Click += new System.EventHandler(this.ButtonLoadSequence_Click);

            // buttonPreviousMove
            this.buttonPreviousMove.Location = new System.Drawing.Point(740, 325);
            this.buttonPreviousMove.Size = new System.Drawing.Size(90, 32);
            this.buttonPreviousMove.Text = "Back";
            this.buttonPreviousMove.Enabled = false;
            this.buttonPreviousMove.Click += new System.EventHandler(this.ButtonPreviousMove_Click);

            // buttonNextMove
            this.buttonNextMove.Location = new System.Drawing.Point(840, 325);
            this.buttonNextMove.Size = new System.Drawing.Size(90, 32);
            this.buttonNextMove.Text = "Next";
            this.buttonNextMove.Enabled = false;
            this.buttonNextMove.Click += new System.EventHandler(this.ButtonNextMove_Click);

            // buttonClearSequence
            this.buttonClearSequence.Location = new System.Drawing.Point(940, 325);
            this.buttonClearSequence.Size = new System.Drawing.Size(120, 32);
            this.buttonClearSequence.Text = "Clear replay";
            this.buttonClearSequence.Enabled = false;
            this.buttonClearSequence.Click += new System.EventHandler(this.ButtonClearSequence_Click);

            // buttonSwapToManualGame
            this.buttonSwapToManualGame.Location = new System.Drawing.Point(630, 420);
            this.buttonSwapToManualGame.Size = new System.Drawing.Size(180, 30);
            this.buttonSwapToManualGame.Text = "Swap to manual game";
            this.buttonSwapToManualGame.Enabled = false;
            this.buttonSwapToManualGame.Click += new System.EventHandler(this.ButtonSwapToManualGame_Click);

            // labelSequenceStatus
            this.labelSequenceStatus.Location = new System.Drawing.Point(630, 375);
            this.labelSequenceStatus.Size = new System.Drawing.Size(430, 45);
            this.labelSequenceStatus.AutoSize = false;
            this.labelSequenceStatus.Text = "Paste coordinate moves like e2-e4, e7-e5. Separate moves with spaces, commas, or new lines.";

            // textBoxCustomPosition
            this.textBoxCustomPosition.Location = new System.Drawing.Point(630, 455);
            this.textBoxCustomPosition.Multiline = true;
            this.textBoxCustomPosition.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxCustomPosition.Size = new System.Drawing.Size(430, 110);
            this.textBoxCustomPosition.PlaceholderText = "white:king=c6,queen=b6; black:king=a8; turn=black";

            // buttonLoadPosition
            this.buttonLoadPosition.Location = new System.Drawing.Point(630, 575);
            this.buttonLoadPosition.Size = new System.Drawing.Size(120, 32);
            this.buttonLoadPosition.Text = "Load position";
            this.buttonLoadPosition.Click += new System.EventHandler(this.ButtonLoadPosition_Click);

            // labelPositionStatus
            this.labelPositionStatus.Location = new System.Drawing.Point(760, 575);
            this.labelPositionStatus.Size = new System.Drawing.Size(300, 60);
            this.labelPositionStatus.AutoSize = false;
            this.labelPositionStatus.Text = "List pieces by color and square; omitted squares are empty. Restart restores the normal board.";

            // Form1
            this.Controls.Add(this.chessBoardPanel);
            this.Controls.Add(this.labelCurrentPlayer);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonUndo);
            this.Controls.Add(this.buttonRestart);
            this.Controls.Add(this.textBoxMoveSequence);
            this.Controls.Add(this.buttonLoadSequence);
            this.Controls.Add(this.buttonPreviousMove);
            this.Controls.Add(this.buttonNextMove);
            this.Controls.Add(this.buttonClearSequence);
            this.Controls.Add(this.buttonSwapToManualGame);
            this.Controls.Add(this.labelSequenceStatus);
            this.Controls.Add(this.textBoxCustomPosition);
            this.Controls.Add(this.buttonLoadPosition);
            this.Controls.Add(this.labelPositionStatus);
        }

        #endregion
    }
}
