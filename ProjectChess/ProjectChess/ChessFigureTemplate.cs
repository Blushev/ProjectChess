using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChess
{
    class ChessFigureTemplate
    {
        public int coordX, coordY;
        public byte id;
        public bool[,] template;
        protected bool white;
        public ChessFigureTemplate(int X, int Y, byte _Id, bool whiteOrBlack)
        {
            coordX = X;
            coordY = Y;
            id = _Id;
            white = whiteOrBlack;

        }

        public virtual List<int[]> calcTurn(byte[,] Board)
        {
            List<int[]> turnList = new List<int[]>();
            int shiftX = template.GetLength(0) / 2;
            int shiftY = template.GetLength(1) / 2;
            for (int y = 0; y < template.GetLength(1); y++)
            {
                for (int x = 0; x < template.GetLength(0); x++)
                {
                    if (template[x, y] == true)
                    {
                        int xt = x + coordX - shiftX;
                        int yt = coordY + y - shiftY;

                        int[] turnCoord = new int[2] { xt, yt };
                        turnList.Add(turnCoord);
                    }
                }
            }
            for (int i = 0; i < template.GetLength(1); i++)
            {
                for (int j = 0; j < template.GetLength(0); j++)
                {
                    if (white)
                    {
                        try
                        {
                            if (Board[j + coordX - shiftX, i + coordY - shiftY] < 17 && Board[j + coordX - shiftX, i + coordY - shiftY] > 0)
                            {
                                template[j, i] = false;
                                if (i - shiftY > 0)
                                {
                                    for (int i1 = 0; i1 < i; i1++)
                                    {
                                        if (j - shiftX >= 0)
                                        {
                                            for (int j1 = j; j1 < template.GetLength(0); j1++)
                                            {
                                                template[j1, i1] = false;
                                            }
                                        }
                                        else
                                        {
                                            for (int j1 = 0; j1 <= j; j1++)
                                            {
                                                template[j1, i1] = false;
                                            }
                                        }
                                    }
                                }

                                else
                                {
                                    for (int i1 = i; i1 < template.GetLength(1); i1++)
                                    {
                                        if (j - shiftX >= 0)
                                        {
                                            for (int j1 = j; j1 < template.GetLength(0); j1++)
                                            {
                                                template[j1, i1] = false;
                                            }
                                        }
                                        else
                                        {
                                            for (int j1 = 0; j1 <= j; j1++)
                                            {
                                                template[j1, i1] = false;
                                            }
                                        }
                                    }
                                }

                            }
                        }

                        catch { }
                        
                    }
                }
            }
            turnList.Clear();
            for (int y = 0; y < template.GetLength(1); y++)
            {
                for (int x = 0; x < template.GetLength(0); x++)
                {
                    if (template[x, y] == true)
                    {
                        int xt = x + coordX - shiftX;
                        int yt = coordY + y - shiftY;

                        int[] turnCoord = new int[2] { xt, yt };
                        turnList.Add(turnCoord);
                    }
                }
            }
            return turnList;
        }

        public void Move(byte[,] Board, int newCoordX, int newCoordY)
        {
            List<int[]>possiblemooves = calcTurn(Board);
            bool possible = false;
            foreach(var i in possiblemooves)
            {
                if(i[0] == newCoordX && i[1] == newCoordY)
                {
                    possible = true;
                    break;
                }
            }
            if (possible) 
            { 
            Board[coordX, coordY] = 0;
            coordX = newCoordX;
            coordY = newCoordY;
            Board[coordX, coordY] = id;
            }
        }
    }
}
