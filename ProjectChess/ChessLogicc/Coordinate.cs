using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Coordinate
    {
        internal int x;
        public int X
        {
            set
            {
                x = value;
            }
            get
            {
                return x;
            }
        }

        internal int y;
        public int Y
        {
            set
            {
                y = value;
            }
            get
            {
                return y;
            }
        }
        public Coordinate (int _x, int _y)
        {
            x = _x;
            y = _y;
        }
       

        public static bool operator == (Coordinate coord1, Coordinate coord2)
        {
            return coord1.x == coord2.x && coord1.y == coord2.y;
        }
        public static bool operator !=(Coordinate coord1, Coordinate coord2)
        {
            return coord1.x != coord2.x || coord1.y != coord2.y;
        }

    }
}
