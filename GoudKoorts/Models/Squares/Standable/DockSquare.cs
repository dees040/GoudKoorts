using GoudKoorts.Models.Static;
using GoudKoorts.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoudKoorts.Models.Standable
{
    public class DockSquare : StandableSquare
    {
        public override bool HandleMove(Cart cart)
        {
            WaterSquare waterSquare = NeighbourNorth as WaterSquare;

            if (waterSquare.HasDockedShip())
            {
                waterSquare.Ship.AddLoad();
                cart.EarnedNewPoints();
            }

            cart.Square.Cart = null;
            cart.Square = this;
            this.Cart = cart;

            return true;
        }
    }
}
