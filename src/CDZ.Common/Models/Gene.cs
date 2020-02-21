using System;
using System.Collections.Generic;
using System.Linq;

namespace CDZ.Common.Models
{
    [Serializable]
    public class Gene
    {
        public Gene()
        {
            Battles = new List<Battle>();
        }

        public List<Battle> Battles { get; set; }

        public double EstimatedTotalTime => CalculateEstimatedTotalTime();

        private double CalculateEstimatedTotalTime()
        {
            return Battles.Sum(x => x.CalculateEstimatedTime());
        }

        public override string ToString()
        {
            return EstimatedTotalTime.ToString();
        }
    }
}
