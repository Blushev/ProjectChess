using ProjectChess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Rook : ChessFigureTemplate
    {
        public Rook(int X, int Y, byte _Id, bool whiteOrBlack) : base(X, Y, _Id, whiteOrBlack)
        {
            template = new bool[16, 16];
            for (int i = 0; i < template.GetLength(1); i++)
            {
                if (i == 8) { }
                else
                {
                    template[8, i] = true;
                }
            }
            for (int i = 0; i < template.GetLength(1); i++)
            {
                if (i == 8) { }
                else
                {
                    template[i, 8] = true;
                }

            }
        }

        public override List<int[]> calcTurn(byte[,] Board)
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

                    try
                    {
                        if (Board[j + coordX - shiftX, i + coordY - shiftY] != 0)
                        {

                            template[j, i] = false;


                            if (i - shiftY > 0)
                            {
                                for (int i1 = i; i1 < template.GetLength(1); i1++)
                                {
                                    template[j, i1] = false;
                                }
                                continue;
                            }
                            if (i - shiftY < 0)
                            {
                                for (int i1 = 0; i1 < i; i1++)
                                {
                                    template[j, i1] = false;
                                }
                                continue;
                            }

                            if (j - shiftX > 0)
                            {
                                for (int j1 = j; j1 < template.GetLength(0); j1++)
                                {
                                    template[j1, i] = false;
                                }
                            }

                            if (j - shiftX < 0)
                            {
                                for (int j1 = 0; j1 < j; j1++)
                                {
                                    template[j1, i] = false;
                                }
                            }

                            bool whiteNeigh = true;
                            if (Board[j + coordX - shiftX, i + coordY - shiftY] > 16)
                                whiteNeigh = false;

                            if (white && whiteNeigh || !white && !whiteNeigh)
                            {
                                template[j, i] = false;
                            }
                            else
                            {
                                template[j, i] = true;//Можем есть противника
                            }
                        }

                    }

                    catch { }



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

            }
            return turnList;
        }
    }
}

