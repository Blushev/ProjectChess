using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ChessDrawingInterface
{
    class BoardCell
    {
        private InterfaceBoard board;

        internal byte id {get;}
        internal bool white { get;}
        internal bool isSelected { get; set; }
        internal ChessLogic.Coordinate cellCoord;
        internal bool isPossible { get; set; }

        public BoardCell(ChessLogic.Coordinate _cellCoord, bool _white, InterfaceBoard _board, byte _id)
        {
            id = _id;
            cellCoord = _cellCoord;
            white = _white;
            board = _board;
            isSelected = false; 
            isPossible = false;
        }
        public void Render()
        {   
            int cellSize = board.cellSize;
            Rectangle rec = new Rectangle();
            rec.Width = cellSize;
            rec.Height = cellSize;
            if (white)
                rec.Fill = Brushes.White;
            else
                rec.Fill = Brushes.Gray;

            if (isSelected)
                rec.Fill = Brushes.LightGray;

            if (isPossible)
                rec.Fill = Brushes.Aquamarine;

            rec.MouseLeftButtonUp += Clicked;

            board.canvas.Children.Add(rec);
            Canvas.SetTop(rec, (cellCoord.Y + 1) * cellSize); 
            Canvas.SetLeft(rec, (cellCoord.X + 1) * cellSize);

            var border = new Border();
            border.Width = cellSize/2;
            border.Height = cellSize/2;

            var tbfigureName = new TextBlock();
            tbfigureName.Text = "";
            tbfigureName.VerticalAlignment = VerticalAlignment.Center;
            tbfigureName.HorizontalAlignment = HorizontalAlignment.Center;
            tbfigureName.FontSize = border.Height;

            if (board.calcBoard.ReadBoardCell(cellCoord.X,cellCoord.Y) > 0)
            {
                if (board.calcBoard.GetFigure(board.calcBoard.ReadBoardCell(cellCoord.X,cellCoord.Y)).White)
                    tbfigureName.Foreground = Brushes.Wheat;

                else
                    tbfigureName.Foreground = Brushes.Black;

                switch (board.calcBoard.GetFigure(board.calcBoard.ReadBoardCell(cellCoord.X, cellCoord.Y)).FigureType)
                {
                    case "Pawn":
                        {
                            tbfigureName.Text = "♙";
                            break;
                        }
                    case "Rook":
                        {
                            tbfigureName.Text = "♖";
                            break;
                        }
                    case "Elephant":
                        {
                            tbfigureName.Text = "♗";
                            break;
                        }
                    case "King":
                        {
                            tbfigureName.Text = "♔";
                            break;
                        }
                    case "Queen":
                        {
                            tbfigureName.Text = "♕";
                            break;
                        }
                    case "Horse":
                        {
                            tbfigureName.Text = "♘";
                            break;
                        }
                }
            }            
            border.Child = tbfigureName;
            board.canvas.Children.Add(border);
            Canvas.SetTop(border, (cellCoord.Y + 1) * cellSize+ border.Height/2);
            Canvas.SetLeft(border, (cellCoord.X + 1) * cellSize + border.Width / 2);
        }


        internal void Clicked(object sender, MouseButtonEventArgs e)
        {
            board.calcAction(this);
        }
    }
}
