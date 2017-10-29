using GoudKoorts.Events;
using GoudKoorts.Models.Squares.Standable;
using GoudKoorts.Models.Standable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoudKoorts.Models.Vehicles
{
    public class Cart : Vehicle
    {
        public StandableSquare Square { get; set; }

        public Direction Direction { get; set; }

        public Cart() : base(1)
        {
            Direction = Direction.East;
        }

        public override void Move()
        {
            StandableSquare next = Square.Next(Direction);

            if (next == null)
            {
                Square.RemoveCart();

                return;
            }

            if (HasCollision(next))
            {
                return;
            }

            if (next.HandleMove(this))
            {
                IsWaiting = false;
            }
        }

        public bool HasCollision()
        {
            return HasCollision(Square.Next(Direction));
        }

        public bool HasCollision(StandableSquare next)
        {
            if (next == null)
            {
                return false;
            }
            else if (next.Cart == null)
            {
                return false;
            }
            // If the other car is not in queue they are riding very closely together.
            else if (!next.Cart.IsWaiting)
            {
                return false;
            }
            else if (next.Cart.Square is QueueableSquare && Square is QueueableSquare)
            {
                return false;
            }

            return true;
        }
    }
}
