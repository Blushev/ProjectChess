using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Elephant : ChessFigureTemplate
    {
        public Elephant(Coordinate _figurePoint, byte _Id, bool _white, byte[,] _parentBoard) : base(_figurePoint, _Id, _white, _parentBoard)
        {
            figureType = "Elephant";
            turnTemplate = new bool[16, 16];
            int shift = turnTemplate.GetLength(0) / 2;
            for (int i = 0; i < turnTemplate.GetLength(0); i++)
            {
                for (int j = 0; j < turnTemplate.GetLength(1); j++)
                {                    
                    if ((i == j)||(i == turnTemplate.GetLength(1)-j))
                    {
                        turnTemplate[i, j] = true;
                    }
                }
            }
            turnTemplate[shift, shift] = false;
        }

        public override List<Coordinate> CalcTurn()
        {
            
            int shift = turnTemplate.GetLength(0) / 2;
            bool[,] boofTemplate = CopyTemplate(turnTemplate);


            ConsiderObstacles(new Coordinate(1, 1));
            ConsiderObstacles(new Coordinate(1, -1));

            var turnList = base.CalcTurn(parentBoard);
            turnTemplate = CopyTemplate(boofTemplate);
            return turnList;
        }
    }
}

