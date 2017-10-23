using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoudKoorts.Models.Standable
{
    public class LineSquare : StandableSquare
    {
        public Alignment Alignment { get; set; }

        public LineSquare(Alignment alignment)
        {
            Alignment = alignment;
        }
    }
}
