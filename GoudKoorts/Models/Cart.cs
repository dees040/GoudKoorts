using GoudKoorts.Models.Squares.Standable;
using GoudKoorts.Models.Standable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoudKoorts.Models
{
    public class Cart
    {
        public StandableSquare Square { get; set; }

        public Direction Direction { get; set; }

        public bool InQueue { get; set; }

        public event EventHandler SomethingHappened;

        public Cart()
        {
            Direction = Direction.East;
            InQueue = false;
        }

        public void Move()
        {
            StandableSquare next = Square.Next(Direction);

            if (next == null)
            {
                if (Square is QueueableSquare)
                {
                    InQueue = true;
                    return;
                }

                Square.Cart = null;
                Square = null;

                return;
            }

            if (HasCollision(next))
            {
                SomethingHappened?.Invoke(this, EventArgs.Empty);

                return;
            }

            // If the next statement is true, then he is on the queuebale sqaures.
            if (InQueue && next.Cart != null && next.Cart.InQueue)
            {
                return;
            }

            if (next is CornerSquare)
            {
                if (next is SwitchableSquare)
                {
                    // Check if switch is open.
                    SwitchableSquare switchable = (SwitchableSquare)next;

                    if (! switchable.AccessableFrom(Direction))
                    {
                        InQueue = true;

                        return;
                    }
                }

                CornerSquare corner = (CornerSquare)next;
                Direction = corner.GetOutgoingDirection(Direction);
            }

            UpdateLocation(next);
        }

        public bool HasCollision(StandableSquare next)
        {
            if (next.Cart == null)
            {
                return false;
            }

            // If the other car is not in queue they are riding very closely together.
            if (!next.Cart.InQueue)
            {
                return false;
            }

            if (next.Cart.Square is QueueableSquare && Square is QueueableSquare)
            {
                InQueue = true;

                return false;
            }

            return true;
        }

        private void UpdateLocation(Square next)
        {
            StandableSquare standable = (StandableSquare)next;

            Square.Cart = null;
            Square = standable;
            Square.Cart = this;
            this.InQueue = false;
        }
    }
}
