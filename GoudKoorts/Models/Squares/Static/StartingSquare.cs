using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoudKoorts.Models.Squares.Static
{
    public class StartingSquare : Square
    {
        public char Index { get; set; }

        public StartingSquare(char index)
        {
            Index = index;
        }
    }
}
