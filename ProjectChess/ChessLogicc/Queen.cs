using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    class Queen : Elephant
    {
        public Queen(Coordinate _figurePoint, byte _Id, bool _white, byte[,] _parentBoard) : base(_figurePoint, _Id, _white, _parentBoard)
        {
            figureType = "Queen";
            int shift = turnTemplate.GetLength(0) / 2;
            for (int i = 0; i < turnTemplate.GetLength(1); i++)
            {
                if (i == shift) { }
                else
                {
                    turnTemplate[shift, i] = true; 
                    turnTemplate[i, shift] = true; 
                }
            }

        }
        public override List<Coordinate> CalcTurn()
        {
            int shift = turnTemplate.GetLength(0) / 2;
            bool[,] boofTemplate = CopyTemplate(turnTemplate);          

            ConsiderObstacles(new Coordinate(1, 0));
            ConsiderObstacles(new Coordinate(0, 1));
            ConsiderObstacles(new Coordinate(1, 1));
            ConsiderObstacles(new Coordinate(1, -1));

            var turnList = base.CalcTurn(parentBoard);
            turnTemplate = CopyTemplate(boofTemplate);
            return turnList;
        }
    }
}
