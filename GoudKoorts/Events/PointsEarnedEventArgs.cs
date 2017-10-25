using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoudKoorts.Events
{
    public class PointsEarnedEventArgs : EventArgs
    {
        private readonly int _points;

        public PointsEarnedEventArgs(int points)
        {
            _points = points;
        }

        public int Points
        {
            get { return _points; }
        }
    }
}
