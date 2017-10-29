using GoudKoorts.Models.Standable;
using GoudKoorts.Models.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoudKoorts.Models.Vehicles
{
    public class Ship : Vehicle
    {
        public WaterSquare[] Squares { get; set; }

        public int Loads { get; set; }

        public Ship(WaterSquare BeginSquare) : base(10)
        {
            this.Squares = new WaterSquare[5];
            this.Squares[0] = BeginSquare;
            this.Squares[0].Ship = this;
            this.Loads = 0;
        }

        public override void Move()
        {
            //check if middle square is north of Dock
            if (Squares[2] != null && Squares[2].NeighbourSouth is DockSquare)
            {
                if (Loads < 8)
                {
                    IsWaiting = true;
                    return;
                }
                IsWaiting = false;
            }
            
            for (int i = 0; i < 5; i++)
            {
                if (Squares[i] == null && i - 1 >= 0 && Squares[i-1] != null)
                {
                    Squares[i] = Squares[i - 1].NeighbourEast as WaterSquare;
                    Squares[i].Ship = this;
                    return;
                }
                else
                {
                    if (Squares[i] != null)
                    {
                        Squares[i].Ship = null;
                        Squares[i] = Squares[i].NeighbourWest as WaterSquare;
                        if (Squares[i] != null)
                        {
                            Squares[i].Ship = this;
                        }
                    }
                }
            }
        }

        public void AddLoad()
        {
            Loads++;
        }
    }
}
