using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Pawn : ChessFigureTemplate
    {

        private bool firstTurn = true;
        public Pawn(Coordinate _figurePoint, byte _Id, bool _white, byte[,] _parentBoard) : base(_figurePoint, _Id, _white, _parentBoard)
        {
            figureType = "Pawn";
            turnTemplate = new bool[5, 5];
            if (white) turnTemplate[2, 1] = true;
            else turnTemplate[2, 3] = true;
        }

        public override List<Coordinate> CalcTurn()
        {
            bool[,] boofTemplate = CopyTemplate(turnTemplate);        
            int shift = turnTemplate.GetLength(0) / 2;
            if (white)
            {
                if (firstTurn)
                {
                    turnTemplate[2, 0] = true;
                }
                ConsiderObstacles(new Coordinate(1, -1));
                ConsiderObstacles(new Coordinate(-1, -1));
            }
            else
            {
                if (firstTurn)
                {
                    turnTemplate[2, 4] = true;
                }
                ConsiderObstacles(new Coordinate(1, 1));
                ConsiderObstacles(new Coordinate(-1, 1));
            }

            var turnList = base.CalcTurn(parentBoard);
            turnTemplate = CopyTemplate(boofTemplate);
            return turnList;
        }

        protected override void ConsiderObstacles(Coordinate Vector)
        {
            int shift = turnTemplate.GetLength(0) / 2;
            Coordinate coordCheck = new Coordinate(figurePoint.x + Vector.x-shift, figurePoint.y + Vector.y-shift);

            if (!CheckBonds(coordCheck, parentBoard) && parentBoard[coordCheck.x, coordCheck.y] > 0)
            {
                if (EatingCheck(coordCheck))
                {
                    turnTemplate[shift + Vector.x, shift + Vector.y] = true;                                     
                }
            }            
        }
        public override bool Move(Coordinate newCoord)
        {
            bool result = base.Move(newCoord);
            firstTurn = false;
            return result;
        }
    }
}
