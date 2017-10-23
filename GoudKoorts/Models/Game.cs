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
            Switches[index].Toggle();
        }

        public void CreateCarts()
        {
            int chance = _random.Next(10);

            if (chance > 8 || Carts.Count == 0)
            {
                int startingPoint = _random.Next(StartingPoints.Count - 1);

                Cart cart = new Cart();

                StandableSquare standable = (StandableSquare)StartingPoints[startingPoint].NeighbourEast;
                standable.Cart = cart;
                cart.Square = standable;
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
    }
}
