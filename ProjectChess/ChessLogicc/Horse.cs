using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    class Horse : ChessFigureTemplate
    {
        public Horse(Coordinate _figurePoint, byte _Id, bool _white, byte[,] _parentBoard) : base(_figurePoint, _Id, _white, _parentBoard)
        {
            figureType = "Horse";
            turnTemplate = new bool[5, 5];
            turnTemplate[1, 0] = true;
            turnTemplate[0, 1] = true;
            turnTemplate[1, 4] = true;
            turnTemplate[0, 3] = true;
            turnTemplate[3, 0] = true;
            turnTemplate[4, 1] = true;
            turnTemplate[4, 3] = true;
            turnTemplate[3, 4] = true;

        }
        public override List<Coordinate> CalcTurn()
        {
            bool[,] boofTemplate = CopyTemplate(turnTemplate);
            
            int shift = turnTemplate.GetLength(0) / 2;
            
            for (int i = 0; i < turnTemplate.GetLength(0); i++)
            {
                for (int j = 0; j < turnTemplate.GetLength(1); j++)
                {
                    Coordinate coordCheck = new Coordinate(i+figurePoint.x-shift, j + figurePoint.y-shift);

                    if (!ChessFigureTemplate.CheckBonds(coordCheck, parentBoard) && (turnTemplate[i,j])&& parentBoard[coordCheck.x, coordCheck.y] > 0)
                    {                      

                        if (EatingCheck(coordCheck))
                        {
                           turnTemplate[i, j] = true;
                        }
                        else
                        {
                            turnTemplate[i, j] = false;
                        }
                    }
                }
            }

            var turnList = base.CalcTurn(parentBoard);
            turnTemplate = CopyTemplate(boofTemplate);
            return turnList;
        }
    }
}
