using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoudKoorts.Models.Standable
{
    abstract public class StandableSquare : Square
    {
        public Cart Cart { get; set; }

        public StandableSquare Next(Direction direction)
        {
            Square square;

            switch (direction)
            {
                case Direction.North:
                    square = NeighbourNorth;
                    break;
                case Direction.East:
                    square = NeighbourEast;
                    break;
                case Direction.South:
                    square = NeighbourSouth;
                    break;
                case Direction.West:
                    square = NeighbourWest;
                    break;
                default:
                    square = null;
                    break;
            }

            if (!(square is StandableSquare))
            {
                return null;
            }

            return (StandableSquare)square;
        }
    }
}
