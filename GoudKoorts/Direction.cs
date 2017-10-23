using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoudKoorts
{
    public enum Direction
    {
        /// <summary>
        /// TODO: consider renaming. Maybe cardinal directions? Because top and bottom
        /// might be unclear, we are watching from above the 'map' (outputview). So top 
        /// and bottom could be above or underneath the map.
        /// </summary>
        North,
        East,
        South,
        West,
        Empty
    }
}
