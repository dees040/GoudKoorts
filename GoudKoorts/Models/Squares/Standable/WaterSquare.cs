using GoudKoorts.Models.Standable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoudKoorts.Models.Standable
{
    public class WaterSquare : StandableSquare
    {
        public Ship Ship { get; set; }

        public bool HasDockedShip()
        {
            return this.Ship != null && this.Ship.IsDocked;
        }

        public override bool HandleMove(Cart cart)
        {
            return true;
        }
    }
}
