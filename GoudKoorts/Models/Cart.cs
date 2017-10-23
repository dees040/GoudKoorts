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

        public Cart()
        {
            Direction = Direction.East;
            InQueue = false;
        }

        public void Move()
        {
            StandableSquare next = GetNext();

            if (next == null)
            {
                Square.Cart = null;
                Square = null;

                return;
            }

            if (HasCollision(next))
            {
                // End game
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

            return next.Cart.InQueue;
        }

        private void UpdateLocation(Square next)
        {
            StandableSquare standable = (StandableSquare)next;

            Square.Cart = null;
            Square = standable;
            Square.Cart = this;
            this.InQueue = false;
        }

        private StandableSquare GetNext()
        {
            Square square;

            switch (Direction)
            {
                case Direction.North:
                    square = Square.NeighbouNorth;
                    break;
                case Direction.East:
                    square = Square.NeighbourEast;
                    break;
                case Direction.South:
                    square = Square.NeighbourSouth;
                    break;
                case Direction.West:
                    square = Square.NeighbourWest;
                    break;
                default:
                    square = null;
                    break;
            }

            if (square != null && !(square is StandableSquare))
            {
                throw new Exception("Something went wrong in the path");
            }

            return (StandableSquare)square;
        }
    }
}
