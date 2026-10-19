using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AiAssistedChess.Engine;

namespace AiAssistedChess.UI
{
    public class ChessboardPanel : Control
    {
        private const int CoordinateGutterSize = 30;

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public ChessGame? Game { get; set; }

        private bool isInputEnabled = true;

        public void SetInputEnabled(bool isEnabled)
        {
            isInputEnabled = isEnabled;
        }

        private Position? selected;
        private List<Position> legal = new();
        private readonly Dictionary<string, Image> pieceImages = new();

        public event Action? GameChanged;

        public ChessboardPanel()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            this.Width = 400;
            this.Height = 400;
            LoadPieceImages();
        }

        private void LoadPieceImages()
        {
            try
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                var imagesDir = Path.Combine(baseDir, "Images", "pieces");
                if (!Directory.Exists(imagesDir)) return;
                var pieceTypes = new[] { "pawn", "knight", "bishop", "rook", "queen", "king" };
                var colors = new[] { "white", "black" };
                foreach (var colorName in colors)
                foreach (var pieceTypeName in pieceTypes)
                {
                    var fileName = Path.Combine(imagesDir, $"{colorName}_{pieceTypeName}.png");
                    if (File.Exists(fileName))
                    {
                        try { pieceImages[$"{colorName}_{pieceTypeName}"] = Image.FromFile(fileName); } catch { }
                    }
                }
            }
            catch { /* ignore image load errors */ }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var graphics = e.Graphics;
            graphics.Clear(BackColor);
            int squareSize = Math.Min(Width - CoordinateGutterSize, Height - CoordinateGutterSize) / 8;
            var boardOriginX = CoordinateGutterSize;
            var boardOriginY = CoordinateGutterSize;
            using var annotationFont = new Font(Font.FontFamily, 9, FontStyle.Regular);
            using var annotationBrush = new SolidBrush(Color.DimGray);
            using var annotationFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

            for (int rowIndex = 0; rowIndex < 8; rowIndex++)
            for (int colIndex = 0; colIndex < 8; colIndex++)
            {
                var rect = new Rectangle(boardOriginX + colIndex * squareSize, boardOriginY + rowIndex * squareSize, squareSize, squareSize);
                bool isLightSquare = (rowIndex + colIndex) % 2 == 0;
                using var tileBrush = new SolidBrush(isLightSquare ? Color.Beige : Color.Sienna);
                graphics.FillRectangle(tileBrush, rect);

                var pos = new Position(rowIndex, colIndex);
                if (selected.HasValue && selected.Value == pos)
                {
                    using var highlightBrush = new SolidBrush(Color.FromArgb(120, Color.CornflowerBlue));
                    graphics.FillRectangle(highlightBrush, rect);
                }
                else if (legal.Contains(pos))
                {
                    using var highlightBrush = new SolidBrush(Color.FromArgb(120, Color.LightGreen));
                    graphics.FillRectangle(highlightBrush, rect);
                }

                // draw piece (image fallback to unicode glyphs)
                if (Game != null)
                {
                    var piece = Game.Board.GetPiece(pos);
                    if (!piece.IsEmpty)
                    {
                        var key = (piece.Color == PieceColor.White ? "white_" : "black_") + (piece.Type.ToString().ToLower());
                        if (pieceImages.TryGetValue(key, out var pieceImage) && pieceImage != null)
                        {
                            // draw image centered and scaled (no drop shadow for image assets)
                            var imageRect = new RectangleF(rect.X + squareSize * 0.05f, rect.Y + squareSize * 0.05f, squareSize * 0.9f, squareSize * 0.9f);

                            graphics.DrawImage(pieceImage, imageRect);
                        }
                        else
                        {
                            // fallback: draw unicode chess characters with a drop shadow for better contrast
                            string glyph = piece.Color == PieceColor.White ? piece.Type switch
                            {
                                PieceType.King => "♔",
                                PieceType.Queen => "♕",
                                PieceType.Rook => "♖",
                                PieceType.Bishop => "♗",
                                PieceType.Knight => "♘",
                                PieceType.Pawn => "♙",
                                _ => "?"
                            } : piece.Type switch
                            {
                                PieceType.King => "♚",
                                PieceType.Queen => "♛",
                                PieceType.Rook => "♜",
                                PieceType.Bishop => "♝",
                                PieceType.Knight => "♞",
                                PieceType.Pawn => "♟",
                                _ => "?"
                            };

                            using var stringFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

                            var glyphFont = new Font(FontFamily.GenericSansSerif, squareSize * 0.6f, FontStyle.Regular);

                            // draw shadow behind the glyph to improve contrast on light tiles
                            var shadowOffsetPixels = Math.Max(1f, squareSize * 0.04f);
                            var shadowLayoutRect = new RectangleF(rect.X + shadowOffsetPixels, rect.Y + shadowOffsetPixels, rect.Width, rect.Height);

                            using var glyphShadowBrush = new SolidBrush(Color.FromArgb(140, 0, 0, 0));

                            graphics.DrawString(glyph, glyphFont, glyphShadowBrush, shadowLayoutRect, stringFormat);

                            // draw main glyph (white stays white, black stays black)
                            var glyphBrush = piece.Color == PieceColor.White ? Brushes.White : Brushes.Black;

                            graphics.DrawString(glyph, glyphFont, glyphBrush, rect, stringFormat);
                        }
                    }
                }

                graphics.DrawRectangle(Pens.Black, rect);
            }

            for (int index = 0; index < 8; index++)
            {
                var fileLabelRect = new RectangleF(boardOriginX + index * squareSize, 0, squareSize, CoordinateGutterSize);
                var rankLabelRect = new RectangleF(0, boardOriginY + index * squareSize, CoordinateGutterSize, squareSize);

                graphics.DrawString(((char)('a' + index)).ToString(), annotationFont, annotationBrush, fileLabelRect, annotationFormat);
                graphics.DrawString((8 - index).ToString(), annotationFont, annotationBrush, rankLabelRect, annotationFormat);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (Game == null || !isInputEnabled) return;
            int squareSize = Math.Min(Width - CoordinateGutterSize, Height - CoordinateGutterSize) / 8;
            int clickedCol = (e.X - CoordinateGutterSize) / squareSize;
            int clickedRow = (e.Y - CoordinateGutterSize) / squareSize;
            if (clickedCol < 0 || clickedCol > 7 || clickedRow < 0 || clickedRow > 7) return;
            var pos = new Position(clickedRow, clickedCol);

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
                            var selectedPromotion = dlg.Selected;
                            chosen = promoMoves.FirstOrDefault(m => m.Promotion == selectedPromotion);
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
