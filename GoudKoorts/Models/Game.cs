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

        public Ship Ship { get; set; }

        public List<StartingSquare> StartingPoints { get; set; }

        public Dictionary<int, SwitchableSquare> Switches { get; set; }

        public int Points { get; set; }

        private Random _random = new Random();

        public Game()
        {
            Points = 0;
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

            while (square.NeighbourNorth != null)
            {
                square = square.NeighbourNorth;
            }

            return square;
        }

        public Square GetNorthEastSquare()
        {
            Square square = Square;

            while (square.NeighbourEast != null)
            {
                square = square.NeighbourEast;
            }

            while (square.NeighbourNorth != null)
            {
                square = square.NeighbourNorth;
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
            int chance = _random.Next(11);

            if (chance > CalculateChance() || Carts.Count == 0)
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

        public void CreateShip()
        {
            if (Ship == null)
            {
                int chance = _random.Next(10);

                if (chance > CalculateChance())
                {
                    WaterSquare beginSquare = GetNorthEastSquare() as WaterSquare;
                    Ship = new Ship(beginSquare);
                }
            }
        }

        public int CalculateChance()
        {
            int chance = 10 - Points / 10;

            return chance < 3 ? 3 : chance;
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

        public void MoveShip()
        {
            if (Ship != null)
            {
                Ship.Move();

                if (Ship.Squares.All(s => s == null))
                {
                    Ship = null;
                    Points += 10;
                }
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
