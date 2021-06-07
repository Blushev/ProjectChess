using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    class King : Horse
    {
        public King(Coordinate _figurePoint, byte _Id, bool _white, byte[,] _parentBoard) : base(_figurePoint, _Id, _white, _parentBoard)
        {
            figureType = "King";
            turnTemplate = new bool[3, 3];
            turnTemplate[0, 0] = true;
            turnTemplate[0, 1] = true;
            turnTemplate[1, 0] = true;
            turnTemplate[2, 0] = true;
            turnTemplate[0, 2] = true;
            turnTemplate[1, 2] = true;
            turnTemplate[2, 1] = true;
            turnTemplate[2, 2] = true;
        }
    }
}
