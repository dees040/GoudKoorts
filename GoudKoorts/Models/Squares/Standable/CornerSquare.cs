using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoudKoorts.Models.Standable
{
    public class CornerSquare : StandableSquare
    {
        public Direction Beginning { get; set; }
        public Direction Ending { get; set; }

        public CornerSquare(Direction beginning, Direction ending)
        {
            Beginning = beginning;
            Ending = ending;
        }

        public Direction GetOutgoingDirection(Direction direction)
        {
            if (Beginning == Helper.Inverse(direction))
            {
                return Ending;
            }

            return Beginning;
        }
    }
}
