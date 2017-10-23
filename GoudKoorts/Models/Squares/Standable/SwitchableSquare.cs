using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoudKoorts.Models.Standable
{
    public class SwitchableSquare : CornerSquare
    {
        public SwitchableSquare(Direction beginning, Direction ending) : base(beginning, ending)
        {

        }

        public void Toggle()
        {
            if (Beginning == Direction.North)
            {
                Beginning = Direction.South;
            }
            else if (Beginning == Direction.South)
            {
                Beginning = Direction.North;
            }
            else if (Ending == Direction.North)
            {
                Ending = Direction.South;
            }
            else if (Ending == Direction.South)
            {
                Ending = Direction.North;
            }
        }

        public bool AccessableFrom(Direction going)
        {
            return Beginning == Helper.Inverse(going);
        }
    }
}
