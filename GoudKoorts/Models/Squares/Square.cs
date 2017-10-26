using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoudKoorts.Models
{
    abstract public class Square
    {
        public Square NeighbourNorth { get; set; }
        public Square NeighbourEast { get; set; }
        public Square NeighbourSouth { get; set; }
        public Square NeighbourWest { get; set; }
    }
}
