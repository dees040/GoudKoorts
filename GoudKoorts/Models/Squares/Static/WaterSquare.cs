using GoudKoorts.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoudKoorts.Models.Static
{
    public class WaterSquare : Square
    {
        public Ship Ship { get; set; }

        public bool HasDockedShip()
        {
            return this.Ship != null && this.Ship.IsWaiting;
        }
    }
}
