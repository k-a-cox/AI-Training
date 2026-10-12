using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AiAssistedChess.Engine;

namespace AiAssistedChess.UI
{
    public class ChessboardPanel : Control
    {
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public ChessGame? Game { get; set; }

        private Position? selected;
        private List<Position> legal = new();

        public event Action? GameChanged;

        public ChessboardPanel()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            this.Width = 400;
            this.Height = 400;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            int squareSize = Math.Min(Width, Height) / 8;
            for (int r = 0; r < 8; r++)
            for (int c = 0; c < 8; c++)
            {
                var rect = new Rectangle(c * squareSize, r * squareSize, squareSize, squareSize);
                bool light = (r + c) % 2 == 0;
                using var brush = new SolidBrush(light ? Color.Beige : Color.Sienna);
                g.FillRectangle(brush, rect);

                var pos = new Position(r, c);
                if (selected.HasValue && selected.Value == pos)
                {
                    using var hb = new SolidBrush(Color.FromArgb(120, Color.CornflowerBlue));
                    g.FillRectangle(hb, rect);
                }
                else if (legal.Contains(pos))
                {
                    using var hb = new SolidBrush(Color.FromArgb(120, Color.LightGreen));
                    g.FillRectangle(hb, rect);
                }

                // draw piece
                if (Game != null)
                {
                    var piece = Game.Board.GetPiece(pos);
                    if (!piece.IsEmpty)
                    {
                        string text = piece.ToString();
                        using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                        var font = new Font(FontFamily.GenericSansSerif, squareSize * 0.32f, FontStyle.Bold);
                        var fore = piece.Color == PieceColor.White ? Brushes.White : Brushes.Black;
                        var outline = piece.Color == PieceColor.White ? Brushes.Black : Brushes.White;
                        // draw outline for readability
                        var textRect = new RectangleF(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);
                        g.DrawString(text, font, outline, textRect, sf);
                        g.DrawString(text, font, fore, textRect, sf);
                    }
                }

                g.DrawRectangle(Pens.Black, rect);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (Game == null) return;
            int squareSize = Math.Min(Width, Height) / 8;
            int col = e.X / squareSize;
            int row = e.Y / squareSize;
            if (col < 0 || col > 7 || row < 0 || row > 7) return;
            var pos = new Position(row, col);

            if (!selected.HasValue)
            {
                var piece = Game.Board.GetPiece(pos);
                if (!piece.IsEmpty && piece.Color == Game.CurrentPlayer)
                {
                    selected = pos;
                    legal = Game.GetLegalMoves(pos).Select(m => m.To).ToList();
                    Invalidate();
                }
                return;
            }

            // if clicked selected, deselect
            if (selected.Value == pos)
            {
                selected = null; legal.Clear(); Invalidate(); return;
            }

            // if clicked a legal destination
            if (legal.Contains(pos))
            {
                var moves = Game.GetLegalMoves(selected.Value).Where(m => m.To == pos).ToList();
                Move? chosen = null;
                if (moves.Count == 1) chosen = moves[0];
                else
                {
                    // promotions or ambiguous; if promotions available ask user
                    var promoMoves = moves.Where(m => m.Promotion.HasValue).ToList();
                    if (promoMoves.Count > 0)
                    {
                        using var dlg = new PawnPromotionForm();
                        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            var pt = dlg.Selected;
                            chosen = promoMoves.FirstOrDefault(m => m.Promotion == pt);
                        }
                    }
                    else chosen = moves[0];
                }

                if (chosen != null)
                {
                    var success = Game.TryMakeMove(chosen);
                    selected = null; legal.Clear();
                    Invalidate();
                    GameChanged?.Invoke();
                    if (!success) System.Media.SystemSounds.Beep.Play();
                }
                return;
            }

            // clicked elsewhere: if same color piece, select it
            var clickedPiece = Game.Board.GetPiece(pos);
            if (!clickedPiece.IsEmpty && clickedPiece.Color == Game.CurrentPlayer)
            {
                selected = pos;
                legal = Game.GetLegalMoves(pos).Select(m => m.To).ToList();
                Invalidate();
            }
            else
            {
                selected = null; legal.Clear(); Invalidate();
            }
        }
    }
}
