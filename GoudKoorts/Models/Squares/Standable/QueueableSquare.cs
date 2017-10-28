using GoudKoorts.Models.Standable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoudKoorts.Models.Squares.Standable
{
    public class QueueableSquare : LineSquare
    {
        public QueueableSquare(Alignment alignment) : base(alignment)
        {
        }

        public override void RemoveCart()
        {
            this.Cart.InQueue = true;

            return;
        }

        public override bool HandleMove(Cart cart)
        {
            if (this.Cart != null && this.Cart.InQueue)
            {
                cart.InQueue = true;

                return false;
            }

            return base.HandleMove(cart);
        }
    }
}
