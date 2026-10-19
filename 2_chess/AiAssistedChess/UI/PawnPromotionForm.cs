using System;
using System.Drawing;
using System.Windows.Forms;
using AiAssistedChess.Engine;

namespace AiAssistedChess.UI
{
    public class PawnPromotionForm : Form
    {
        public PieceType Selected { get; private set; } = PieceType.Queen;

        public PawnPromotionForm()
        {
            Text = "Promote Pawn";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(300, 60);
            var btnQ = new Button { Text = "Queen", Location = new Point(10, 10), Size = new Size(65, 40) };
            var btnR = new Button { Text = "Rook", Location = new Point(80, 10), Size = new Size(65, 40) };
            var btnB = new Button { Text = "Bishop", Location = new Point(150, 10), Size = new Size(65, 40) };
            var btnN = new Button { Text = "Knight", Location = new Point(220, 10), Size = new Size(65, 40) };
            btnQ.Click += (s,e) => { Selected = PieceType.Queen; DialogResult = DialogResult.OK; Close(); };
            btnR.Click += (s,e) => { Selected = PieceType.Rook; DialogResult = DialogResult.OK; Close(); };
            btnB.Click += (s,e) => { Selected = PieceType.Bishop; DialogResult = DialogResult.OK; Close(); };
            btnN.Click += (s,e) => { Selected = PieceType.Knight; DialogResult = DialogResult.OK; Close(); };
            Controls.AddRange(new Control[]{btnQ, btnR, btnB, btnN});
        }
    }
}
