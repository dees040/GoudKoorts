using GoudKoorts.Events;
using GoudKoorts.Models.Squares.Static;
using GoudKoorts.Models.Standable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoudKoorts.Models
{
    public class Game
    {
        public Square Square { get; set; }

        public List<Cart> Carts { get; set; }

        public List<StartingSquare> StartingPoints { get; set; }

        public Dictionary<int, SwitchableSquare> Switches { get; set; }

        public int Points { get; set; }

        public int Level
        {
            get
            {
                return (int)Math.Ceiling((double)Points / 5);
            }
        }

        private Random _random = new Random();

        public Game()
        {
            Carts = new List<Cart>();
            StartingPoints = new List<StartingSquare>();
            Switches = new Dictionary<int, SwitchableSquare>();
        }

        public Square GetNorthWestSquare()
        {
            Square square = Square;

            while (square.NeighbourWest != null)
            {
                square = square.NeighbourWest;
            }

            while (square.NeighbouNorth != null)
            {
                square = square.NeighbouNorth;
            }

            return square;
        }

        public void PrepareSwitchToggle(int index)
        {
            SwitchableSquare square = Switches[index];

            if (square.Cart == null)
            {
                square.Toggle();
            }
        }

        public void CreateCarts(EventHandler stopEvent)
        {
            int chance = _random.Next(10);

            if (chance > 8 || Carts.Count == 0)
            {
                int startingPoint = _random.Next(StartingPoints.Count);

                Cart cart = new Cart();

                StandableSquare standable = StartingPoints[startingPoint].NeighbourEast as StandableSquare;
                standable.Cart = cart;
                cart.Square = standable;
                cart.CartHasCollision += stopEvent;
                cart.CartEarnedPoints += EarnedPoints;
                Carts.Add(cart);
            }
        }

        public void MoveCarts()
        {
            List<Cart> needToBeRemoved = new List<Cart>();

            foreach (Cart cart in Carts)
            {
                cart.Move();

                if (cart.Square == null)
                {
                    needToBeRemoved.Add(cart);
                }
            }

            foreach (Cart cart in needToBeRemoved)
            {
                Carts.Remove(cart);
            }
        }

        public bool HasEnded()
        {
            foreach (Cart cart in Carts)
            {
                if (cart.HasCollision())
                {
                    return true;
                }
            }

            return false;
        }

        public void EarnedPoints(object sender, EventArgs args)
        {
            Points += (args as PointsEarnedEventArgs).Points;
        }
    }
}
