using CDZ.Common.Helpers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CDZ.Common.Models.Builders
{
    public static class MapBuilder
    {
        public static async Task<Map> BuildAsync(this Map map)
        {
            var raw = await AssemblyHelper.GetEmbeddedResourceAsync("CDZ.Common.Assets.Text.Map.txt");
            var rowRegex = new Regex(@"\n");
            var columnRegex = new Regex(@"\t");

            var rows = rowRegex.Split(raw);
            map.Tiles = new int[rows.Length, rows.Length];

            int x = 0;
            foreach (var row in rows)
            {
                int y = 0;
                foreach (var column in columnRegex.Split(row))
                {
                    map.Tiles[x, y] = int.Parse(column);
                    y++;
                }
                x++;
            }

            return map;
        }
    }
}
