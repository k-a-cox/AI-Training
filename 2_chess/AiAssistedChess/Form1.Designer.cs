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
        private System.Windows.Forms.Label labelSequenceStatus;
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
            this.ClientSize = new System.Drawing.Size(1100, 650);
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
            this.labelSequenceStatus = new System.Windows.Forms.Label();

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
            this.buttonUndo.Location = new System.Drawing.Point(630, 140);
            this.buttonUndo.Size = new System.Drawing.Size(100, 30);
            this.buttonUndo.Text = "Undo";
            this.buttonUndo.Click += new System.EventHandler(this.ButtonUndo_Click);

            // buttonRestart
            this.buttonRestart.Location = new System.Drawing.Point(740, 140);
            this.buttonRestart.Size = new System.Drawing.Size(100, 30);
            this.buttonRestart.Text = "Restart";
            this.buttonRestart.Click += new System.EventHandler(this.ButtonRestart_Click);

            // textBoxMoveSequence
            this.textBoxMoveSequence.Location = new System.Drawing.Point(630, 230);
            this.textBoxMoveSequence.Multiline = true;
            this.textBoxMoveSequence.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMoveSequence.Size = new System.Drawing.Size(430, 180);
            this.textBoxMoveSequence.PlaceholderText = "e2-e4, e7-e5, g1-f3";

            // buttonLoadSequence
            this.buttonLoadSequence.Location = new System.Drawing.Point(630, 425);
            this.buttonLoadSequence.Size = new System.Drawing.Size(100, 32);
            this.buttonLoadSequence.Text = "Load moves";
            this.buttonLoadSequence.Click += new System.EventHandler(this.ButtonLoadSequence_Click);

            // buttonPreviousMove
            this.buttonPreviousMove.Location = new System.Drawing.Point(740, 425);
            this.buttonPreviousMove.Size = new System.Drawing.Size(90, 32);
            this.buttonPreviousMove.Text = "Back";
            this.buttonPreviousMove.Enabled = false;
            this.buttonPreviousMove.Click += new System.EventHandler(this.ButtonPreviousMove_Click);

            // buttonNextMove
            this.buttonNextMove.Location = new System.Drawing.Point(840, 425);
            this.buttonNextMove.Size = new System.Drawing.Size(90, 32);
            this.buttonNextMove.Text = "Next";
            this.buttonNextMove.Enabled = false;
            this.buttonNextMove.Click += new System.EventHandler(this.ButtonNextMove_Click);

            // buttonClearSequence
            this.buttonClearSequence.Location = new System.Drawing.Point(940, 425);
            this.buttonClearSequence.Size = new System.Drawing.Size(120, 32);
            this.buttonClearSequence.Text = "Clear replay";
            this.buttonClearSequence.Enabled = false;
            this.buttonClearSequence.Click += new System.EventHandler(this.ButtonClearSequence_Click);

            // labelSequenceStatus
            this.labelSequenceStatus.Location = new System.Drawing.Point(630, 475);
            this.labelSequenceStatus.Size = new System.Drawing.Size(430, 100);
            this.labelSequenceStatus.AutoSize = false;
            this.labelSequenceStatus.Text = "Paste coordinate moves like e2-e4, e7-e5. Separate moves with spaces, commas, or new lines.";

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
            this.Controls.Add(this.labelSequenceStatus);
        }

        #endregion
    }
}
