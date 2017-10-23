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
    }
}
