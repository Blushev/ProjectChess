using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public abstract class ChessFigureTemplate
    {
        protected Coordinate figurePoint;
        public Coordinate GetFigureCoord()
        {
            return figurePoint;
        }
        protected byte id;
        public byte Id
        {
            get
            {
                return id;
            }
        }
        protected bool[,] turnTemplate;

        protected bool white;
        public bool White
        {
            get
            {
                return white;
            }
        }

        protected string figureType; 
        public string FigureType
        {
            get
            {
                return figureType;
            }
        }

        protected byte[,] parentBoard;


        public ChessFigureTemplate(Coordinate _figurePoint, byte _Id, bool _white, byte[,] _parentBoard)
        {
            figurePoint = _figurePoint;
            id = _Id;
            white = _white;
            parentBoard = _parentBoard;
        }

        public static bool CheckBonds(Coordinate _p, byte[,] board)
        {
            bool cross = false;
            if ((_p.x >= board.GetLength(0))||(_p.y >= board.GetLength(1)))
                cross = true;
            if ((_p.x < 0)||(_p.y < 0))
                cross = true;
            return cross;
        }

      

        public static bool CheckBonds(Coordinate _p, int _boardWidth, int _boardHeight) 
        {
            bool cross = false;
            if (_p.x >= _boardWidth || _p.y >= _boardHeight)
                cross = true;
            if (_p.x < 0 || _p.y < 0)
                cross = true;
            return cross;
        }              

        public virtual List<Coordinate> CalcTurn()
        {
            List<Coordinate> turnPointsList = new List<Coordinate>();
            int shift = turnTemplate.GetLength(0) / 2;            
            for (int y = 0; y < turnTemplate.GetLength(1); y++)
            {
                for (int x = 0; x < turnTemplate.GetLength(0); x++)
                {
                    if (turnTemplate[x, y] == true)
                    {
                        int xBoard = x + figurePoint.x - shift;
                        int yBoard = y + figurePoint.y - shift;
                        Coordinate coordTurn = new Coordinate(xBoard, yBoard);                      

                        if (!CheckBonds(coordTurn, parentBoard))
                            turnPointsList.Add(coordTurn);
                    }
                }
            }
            return turnPointsList;
        }

        public virtual List<Coordinate> CalcTurn(byte[,] Board)
        {
            List<Coordinate> turnPointsList = new List<Coordinate>();            
            int shift = turnTemplate.GetLength(0) / 2;     
            for (int y = 0; y < turnTemplate.GetLength(1); y++)
            {
                for (int x = 0; x < turnTemplate.GetLength(0); x++)
                {
                    if (turnTemplate[x, y] == true)
                    {
                        int xBoard = x + figurePoint.x - shift;
                        int yBoard = y + figurePoint.y - shift;
                        Coordinate coordTurn = new Coordinate(xBoard, yBoard);

                        if (!CheckBonds(coordTurn, Board))
                            turnPointsList.Add(coordTurn);
                    }
                }
            }
            return turnPointsList;
        }

        public static bool [,] CopyTemplate (bool [,] original)
        {
            bool[,] copy = new bool[original.GetLength(0), original.GetLength(1)];

            for (int i = 0; i < copy.GetLength(0); i++)
            {
                for (int j = 0; j < copy.GetLength(1); j++)
                {
                    copy[i, j] = original[i, j];
                }

            }
            return copy;
        }

        public virtual bool Move(Coordinate newCoord)
        {
            List<Coordinate> possibleMovesList = CalcTurn();
            bool possible = false;
            foreach (var i in possibleMovesList)
            {
                if (i==newCoord)
                {
                    possible = true;
                    break;
                }
            }

            if (possible)
            {
                parentBoard[figurePoint.x, figurePoint.y] = 0;
                figurePoint = newCoord;                
                parentBoard[figurePoint.x, figurePoint.y] = id;
            }
            return possible;
        } 

        protected virtual void ConsiderObstacles (Coordinate Vector)
        {
            int shift = turnTemplate.GetLength(0) / 2;
            for (int i = -shift; i < shift; i++)
            {
                if (i == 0) { }
                else
                {                    
                    Coordinate coordCheck = new Coordinate(figurePoint.x+i*Vector.x, i * Vector.y+ figurePoint.y);
                    if (!CheckBonds(coordCheck, parentBoard) && turnTemplate[i * Vector.x+shift, i* Vector.y+shift] && parentBoard[coordCheck.x, coordCheck.y] > 0)
                    {
                        if (i < 0)
                        {
                            for (int j = -shift; j <= i; j++)
                            {
                                Coordinate templCoordCheck = new Coordinate(j * Vector.x + shift, j * Vector.y + shift);
                                if (!CheckBonds(templCoordCheck, turnTemplate.GetLength(0),turnTemplate.GetLength(1)))                   
                                    turnTemplate[j * Vector.x+shift, j * Vector.y+shift] = false;
                            }
                        }
                        else if (i > 0) 
                        {
                            for (int j = i; j < shift; j++)
                            {
                                Coordinate templCoordCheck = new Coordinate(j * Vector.x + shift, j * Vector.y + shift);
                                if (!CheckBonds(templCoordCheck, turnTemplate.GetLength(0), turnTemplate.GetLength(1)))
                                    turnTemplate[j * Vector.x + shift, j * Vector.y + shift] = false;
                            }
                        }                      

                        if (EatingCheck(coordCheck))
                        {
                            Coordinate templCoordCheck = new Coordinate(i * Vector.x + shift, i * Vector.y + shift);
                            if (!CheckBonds(templCoordCheck, turnTemplate.GetLength(0), turnTemplate.GetLength(1)))
                                turnTemplate[i * Vector.x + shift, i * Vector.y + shift] = true;
                        }   
                    }                    
                }
            }
        }

        protected bool EatingCheck (Coordinate _neighborCoord)
        {          
            bool whiteNeighbor = true;
            if (parentBoard[_neighborCoord.x, _neighborCoord.y] > 16)
                whiteNeighbor = false;
            return (!((white && whiteNeighbor) || (!white && !whiteNeighbor)));
        }            
    }
}
