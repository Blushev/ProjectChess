using ConsoleApplication1;
using System;
using System.Collections.Generic;

namespace ProjectChess
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[,] massiveBoard = new byte[8, 8];
            //Pawn p1 = new Pawn(1, 6, 1, true);
            //Pawn p2 = new Pawn(0, 5, 17, false);
            //Pawn p3 = new Pawn(2, 5, 18, false);
            //massiveBoard[p1.coordX, p1.coordY] = p1.id;
            //massiveBoard[p2.coordX, p2.coordY] = p2.id;
            //massiveBoard[p3.coordX, p3.coordY] = p3.id;

            Rook p1 = new Rook(1, 3, 1, true);
            Rook p2 = new Rook(7, 3, 1, true);
            Rook p3 = new Rook(1, 3, 1, true);
            Rook p4 = new Rook(1, 1, 1, true);
            massiveBoard[p1.coordX, p1.coordY] = p1.id;

            Draw(massiveBoard);
            List<int[]> p1el = p1.calcTurn(massiveBoard);
            foreach (var i in p1el) 
            {     
                try
                {
                    massiveBoard[i[0], i[1]] = 9;
                }

                catch { }

            }
            Draw(massiveBoard);
            Console.ReadKey();
        }

        private static void Draw(byte[,] Board)
        {
            Console.Clear();
            for(int i = 0; i < Board.GetLength(1); i++)
            {
                for (int j = 0; j < Board.GetLength(0); j++)
                {
                    Console.Write(Board[j,i]);
                }
                Console.WriteLine();
            }
        }
    }
}
