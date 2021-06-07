using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class LogicBoard
    {
        private bool endGameFlag;
        public bool EndGameFlag
        {
            get
            {
                return endGameFlag;
            }
        }
        private bool whiteWinFlag;
        public bool WhiteWinFlag
        {
            get
            {
                return whiteWinFlag;
            }
        }
        private bool whiteTurnFlag;
        public bool WhiteTurnFlag
        {
            get
            {
                return whiteTurnFlag;
            }
        }

        private byte[,] boardField;
        private Dictionary<byte, ChessFigureTemplate> figureDictionary = new Dictionary<byte, ChessFigureTemplate>();
        private int turn; 
        public int Turn
        {
            get
            {
                return turn;
            }
        }

        public LogicBoard()
        {
            GameInitialize();
        }

        public byte ReadBoardCell(int _x, int _y)
        {
            return boardField[_x, _y];
        }
        public int GetBoardWidth()
        {
            return boardField.GetLength(0);
        }
        public int GetBoardHeight()
        {
            return boardField.GetLength(1);
        }

        public ChessFigureTemplate GetFigure(byte _Id)
        {
            return figureDictionary[_Id];
        }

        public void GameInitialize()
        {
            endGameFlag = false;
            whiteTurnFlag = true;
            turn = 1;
            byte id = 1;
            boardField = new byte[8, 8];
            // СТАВИМ БЕЛЫЕ
            for (int i = 0; i < 8; i++)
            {
                FigureGeneration("Pawn", ref id, new Coordinate(i, 6), true);
                
            }
            FigureGeneration("Rook", ref id, new Coordinate(0, 7), true);
            FigureGeneration("Rook", ref id, new Coordinate(7, 7), true);

            FigureGeneration("Elephant", ref id, new Coordinate(1, 7), true);
            FigureGeneration("Elephant", ref id, new Coordinate(6, 7), true);

            FigureGeneration("Horse", ref id, new Coordinate(2, 7), true);
            FigureGeneration("Horse", ref id, new Coordinate(5, 7), true);

            FigureGeneration("Queen", ref id, new Coordinate(3, 7), true);
            FigureGeneration("King", ref id, new Coordinate(4, 7), true);

            // СТАВИМ ЧЕРНЫЕ
            for (int i = 0; i < 8; i++)
            {
                FigureGeneration("Pawn", ref id, new Coordinate(i, 1), false);

            }
            FigureGeneration("Rook", ref id, new Coordinate(0, 0), false);
            FigureGeneration("Rook", ref id, new Coordinate(7, 0), false);

            FigureGeneration("Elephant", ref id, new Coordinate(1, 0), false);
            FigureGeneration("Elephant", ref id, new Coordinate(6, 0), false);

            FigureGeneration("Horse", ref id, new Coordinate(2, 0), false);
            FigureGeneration("Horse", ref id, new Coordinate(5, 0), false);

            FigureGeneration("Queen", ref id, new Coordinate(3, 0), false);
            FigureGeneration("King", ref id, new Coordinate(4, 0), false);
        }

        private void FigureGeneration(string _type, ref byte _id, Coordinate figureCoord, bool _white)
        {
            ChessFigureTemplate figure = null;
            switch (_type)
            {
                case "Pawn":
                    {
                        figure = new Pawn(figureCoord, _id, _white, boardField);
                        break;
                    }
                case "Rook":
                    {
                        figure = new Rook(figureCoord, _id, _white, boardField);
                        break;
                    }
                case "Elephant":
                    {
                        figure = new Elephant(figureCoord, _id, _white, boardField);
                        break;
                    }
                case "Horse":
                    {
                        figure = new Horse(figureCoord, _id, _white, boardField);
                        break;
                    }
                case "Queen":
                    {
                        figure = new Queen(figureCoord, _id, _white, boardField);
                        break;
                    }
                case "King":
                    {
                        figure = new King(figureCoord, _id, _white, boardField);
                        break;
                    }
            }
            PlaceFigure(figure);
            figureDictionary.Add(_id,figure);
            _id++;
        }

        private void PlaceFigure(ChessFigureTemplate figure)
        {
            boardField[figure.GetFigureCoord().x, figure.GetFigureCoord().y] = figure.Id;
        }

        public void TurnPlayer()
        {
            if (whiteTurnFlag)
                whiteTurnFlag = false;

            else
                whiteTurnFlag = true;
            turn++;
        }
        //=====================================
        public bool GameCycle(out bool whiteWin)
        {
            bool whiteKingExists = false;
            bool blackKingExists = false;

            foreach (var i in boardField)
            {
                if (i == 16)
                    whiteKingExists = true;
                if (i == 32)
                    blackKingExists = true;
            }
            if (whiteKingExists && blackKingExists)
                endGameFlag = false;
            else
            {
                endGameFlag = true;
            }
            whiteWin = false;
            if (endGameFlag)
            {
                whiteWinFlag = whiteKingExists;
                whiteWin = whiteWinFlag;
            }
            return endGameFlag;
        }
    }
}
