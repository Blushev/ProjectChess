using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChess
{
    class Pawn : ChessFigureTemplate
    {
        bool firstTurn = true; 
        public Pawn(int X, int Y, byte _Id, bool whiteOrBlack) : base(X, Y, _Id, whiteOrBlack)
        {
            template = new bool[5, 5];
            if (whiteOrBlack) template[2, 1] = true;
            else  template[2, 3] = true;
        }

        public override List<int[]> calcTurn(byte[,] Board)
        {
            List<int[]> turnList = new List<int[]>();
            int shiftX = template.GetLength(0) / 2;
            int shiftY = template.GetLength(1) / 2;
            if (white)
            {
                if (Board[1 + coordX, coordY - 1] > 16) template[1, 1] = true;
                if (Board[coordX - 1, coordY - 1] > 16) template[3, 1] = true;
                if (firstTurn) template[2, 0] = true;         
            }
            else
            {
                if (Board[1 + coordX, coordY + 1] > 16) template[1, 3] = true;
                if (Board[coordX - 1, coordY + 1] > 16) template[3, 3] = true;
                if (firstTurn) template[2, 4] = true;  
            }

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
    }
}
