using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDZ.Common.Models
{
    [Serializable]
    public class Battle
    {
        public Knight GoldKnight { get; set; }
        public List<Knight> BronzeKnights { get; set; }

        public double CalculateEstimatedTime()
        {
            return GoldKnight is null ? 0 : GoldKnight.Cosmo / BronzeKnights.Sum(x => x.Cosmo);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var knight in BronzeKnights)
            {
                builder.AppendLine(knight.Name);
            }

            return builder.ToString();
        }
    }
}
