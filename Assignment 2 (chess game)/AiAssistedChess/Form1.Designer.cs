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
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.chessBoardPanel = new AiAssistedChess.UI.ChessboardPanel();
            this.labelCurrentPlayer = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonUndo = new System.Windows.Forms.Button();
            this.buttonRestart = new System.Windows.Forms.Button();

            // chessBoardPanel
            this.chessBoardPanel.Location = new System.Drawing.Point(20, 20);
            this.chessBoardPanel.Size = new System.Drawing.Size(480, 480);

            // labelCurrentPlayer
            this.labelCurrentPlayer.AutoSize = true;
            this.labelCurrentPlayer.Location = new System.Drawing.Point(520, 40);
            this.labelCurrentPlayer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelCurrentPlayer.Text = "Current: " ;

            // labelStatus
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(520, 80);
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelStatus.Text = "Status: ";

            // buttonUndo
            this.buttonUndo.Location = new System.Drawing.Point(520, 140);
            this.buttonUndo.Size = new System.Drawing.Size(100, 30);
            this.buttonUndo.Text = "Undo";
            this.buttonUndo.Click += new System.EventHandler(this.ButtonUndo_Click);

            // buttonRestart
            this.buttonRestart.Location = new System.Drawing.Point(520, 180);
            this.buttonRestart.Size = new System.Drawing.Size(100, 30);
            this.buttonRestart.Text = "Restart";
            this.buttonRestart.Click += new System.EventHandler(this.ButtonRestart_Click);

            // Form1
            this.Controls.Add(this.chessBoardPanel);
            this.Controls.Add(this.labelCurrentPlayer);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonUndo);
            this.Controls.Add(this.buttonRestart);
        }

        #endregion
    }
}
