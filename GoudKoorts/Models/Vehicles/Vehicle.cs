using GoudKoorts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoudKoorts.Models.Vehicles
{
    public abstract class Vehicle
    {
        private int _points;
        public event EventHandler VehicleEarnedPoints;

        public bool IsWaiting { get; set; }

        public Vehicle(int points)
        {
            _points = points;
            IsWaiting = false;
        }

        public void EarnedNewPoints()
        {
            VehicleEarnedPoints?.Invoke(this, new PointsEarnedEventArgs(_points));
        }

        public abstract void Move();
    }
}
