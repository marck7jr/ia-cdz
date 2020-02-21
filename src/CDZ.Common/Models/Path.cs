using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CDZ.Common.Models
{
    public class Path
    {
        public Point Key { get; set; }
        public List<Point> Points { get; set; }
        public List<double> CalculatedTimes { get; set; }

        public double GetTotalTime()
        {
            return CalculatedTimes.Sum();
        }
    }
}
