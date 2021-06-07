using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChessDrawingInterface
{
    class InterfaceBoard
    {
        private TextBlock tbTurnCount = new TextBlock();
        internal ChessLogic.LogicBoard calcBoard;
        private Dictionary<byte, BoardCell> boardCellDict = new Dictionary<byte, BoardCell>();
        internal Canvas canvas;
        internal ChessLogic.Coordinate cursor;
        internal int cellSize;

        public InterfaceBoard(ChessLogic.LogicBoard board, Canvas _canvas, TextBlock turnCount)
        {
            canvas = _canvas;            
            UnselectCursor();
            tbTurnCount = turnCount;
            calcBoard = board;

            byte id = 1;
            for (int i = 0; i < board.GetBoardWidth(); i++)
            {
                for (int j = 0; j < board.GetBoardHeight(); j++)
                {
                    bool white = false;
                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0))
                        white = true;
                    ChessLogic.Coordinate cellCoord = new ChessLogic.Coordinate(i, j);
                    BoardCell cell = new BoardCell(cellCoord, white, this, id);
                    boardCellDict.Add(id, cell);
                    id++;
                }
            }
            RenderBoard();
        }

        public void RenderBoard()
        {
            cellSize = (int)(Math.Min(canvas.ActualHeight, canvas.ActualWidth) / 9);
            canvas.Children.Clear();           
            int letter = 65;
            char[] letterArray = new char[8];
            char[] ciferArray = new char[8];
            for (int i = 0; i < 8; i++)
            {                
                letterArray[i] = (char)letter; 
                letter++;
                ciferArray[i] =(char)(i + 1+'0');              
            }
            DrawAxis(letterArray,true);
            DrawAxis(ciferArray,false);
            foreach (var i in boardCellDict)
            {
                i.Value.Render();
            }
            RenderState();
        }

        private void DrawAxis (char [] _axisLetters, bool _horizOrientation)
        {             
            for (int i = 0; i < _axisLetters.Length; i++)
            {
                var border = new Border();
                border.Width = cellSize;
                border.Height = cellSize;
                var tbLetter = new TextBlock();
                tbLetter.Text = _axisLetters[i].ToString();
                tbLetter.VerticalAlignment = VerticalAlignment.Center;
                tbLetter.HorizontalAlignment = HorizontalAlignment.Center;
                tbLetter.FontSize = border.Height;

                border.Child = tbLetter;
                canvas.Children.Add(border);

                if (_horizOrientation)
                {
                    Canvas.SetTop(border, 0);
                    Canvas.SetLeft(border, (i + 1) * cellSize);
                }
                else
                {
                    Canvas.SetTop(border, (i + 1) * cellSize);
                    Canvas.SetLeft(border, 0);                    
                }                               
            } 
        }

        private void ResetCells()
        {
            foreach (var i in boardCellDict)
            {
                if (i.Value.isSelected || i.Value.isPossible)
                {
                    i.Value.isSelected = false;
                    i.Value.isPossible = false;
                    i.Value.Render();
                }
            }
        }
        public void RenderState()
        {
            var border = new Border();
            string player = " ";
            if (calcBoard.WhiteTurnFlag)
                player = "Игрок1(Белые)";

            else
                player = "Игрок2(Белые)";
            tbTurnCount.Text = "Ходит " + player + ". Номер хода: " + calcBoard.Turn;

        }
        public void CalcResult(bool whiteWins)
        {
            if (whiteWins)
                MessageBox.Show("Белые выиграли");
            else
                MessageBox.Show("Черные выиграли");
        }

        private bool NoCursor()
        {
            return (cursor.X == -1 || cursor.Y == -1);
        }

        private void UnselectCursor()
        {
            cursor = new ChessLogic.Coordinate(-1, -1);
        }

        private void SetCursor (int _x, int _y)
        {
            cursor.X = _x;
            cursor.Y = _y;
        }

        public void calcAction(BoardCell sender)
        {
            
            if (NoCursor())
            {
                if (calcBoard.ReadBoardCell(sender.cellCoord.X, sender.cellCoord.Y) > 0)
                {
                    SetCursor(sender.cellCoord.X, sender.cellCoord.Y);
                                      
                    byte id = calcBoard.ReadBoardCell(cursor.X, cursor.Y);

                    if ((calcBoard.WhiteTurnFlag && !calcBoard.GetFigure(id).White) || (!calcBoard.WhiteTurnFlag && calcBoard.GetFigure(id).White))
                    {
                        UnselectCursor();
                        return;
                    }

                    ResetCells();
                    sender.isSelected = true;
                    sender.Render();

                    List <ChessLogic.Coordinate> possCell = calcBoard.GetFigure(id).CalcTurn();

                    foreach (var c in possCell)
                    {
                        foreach (var p in boardCellDict)
                        {
                            if (p.Value.cellCoord == c)
                            {
                                boardCellDict[p.Key].isPossible = true;
                                boardCellDict[p.Key].Render();
                            }
                        }
                    }
                }
            }
            else
            {
                if (cursor == sender.cellCoord)
                {
                    sender.isSelected = false;
                    sender.Render();
                    UnselectCursor();

                    foreach (var cell in boardCellDict)
                    {
                        if (cell.Value.isPossible)
                        {
                            cell.Value.isPossible = false;
                            cell.Value.Render();
                        }                            
                    }  
                }
                else
                {
                    byte id = calcBoard.ReadBoardCell(cursor.X, cursor.Y);

                    ChessLogic.Coordinate destinationCell = new ChessLogic.Coordinate(sender.cellCoord.X, sender.cellCoord.Y);
                    if (calcBoard.GetFigure(id).Move(destinationCell))
                    {
                        UnselectCursor();
                        ResetCells();
                        calcBoard.TurnPlayer();
                        RenderState();

                        bool whiteWin = false;
                        if (calcBoard.GameCycle(out whiteWin))
                            CalcResult(whiteWin);
                    }
                    else
                        MessageBox.Show("Ход невозможен");
                }
            }
        }
    }
}
