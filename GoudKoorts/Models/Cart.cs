using GoudKoorts.Events;
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

        public event EventHandler CartHasCollision;
        public event EventHandler CartEarnedPoints;

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
                Square.RemoveCart();

                return;
            }

            if (HasCollision(next))
            {
                CartHasCollision?.Invoke(this, EventArgs.Empty);

                return;
            }

            if (next.HandleMove(this))
            {
                InQueue = false;
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
            else if (!next.Cart.InQueue)
            {
                return false;
            }
            else if (next.Cart.Square is QueueableSquare && Square is QueueableSquare)
            {
                return false;
            }

            return true;
        }

        public void EarnedNewPoint()
        {
            CartEarnedPoints?.Invoke(this, new PointsEarnedEventArgs(1));
        }
    }
}
