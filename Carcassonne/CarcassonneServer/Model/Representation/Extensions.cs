using System;
using System.Collections.Generic;
using CarcassonneServer.Model.Representation.Area;
using System.Linq;

namespace CarcassonneServer.Model.Representation
{
    public static class Extensions
    {
        public static void SetMeeple(this IEnumerable<BaseArea> areas, Meeple meeple, Func<BaseArea, Meeple, bool> selector)
        {
            BaseArea area = null;
            foreach (var item in areas)
            {
                if (selector(item, meeple))
                    area = item;
            }

            if (area != null)
                area.AddMeeple(meeple);
        }
        public static ConnectingPoint Opposite(this ConnectingPoint direction)
        {
            switch (direction)
            {
                case ConnectingPoint.Down: return ConnectingPoint.Up;
                case ConnectingPoint.Up: return ConnectingPoint.Down;
                case ConnectingPoint.Left: return ConnectingPoint.Right;
                default: return ConnectingPoint.Left;
            }
        }
        /// <summary>
        /// Visszaad egy asszociatív tömböt, amiben minden oldaltípushoz egy listában szerepel, hogy a vizsgált mező mely irányú oldala olyan típusú.
        /// </summary>
        /// <param name="tile">A kérdéses mező.</param>
        /// <returns>Egy dictionary ami oldaltípushoz irány listát rendel.</returns>
        public static Dictionary<AreaType, List<ConnectingPoint>> GetDirectionsForAreaType(this Tile tile)
        {
            var dict = new Dictionary<AreaType, List<ConnectingPoint>>();

            foreach (AreaType areaType in Enum.GetValues(typeof(AreaType)))
                dict.Add(areaType,
                    new List<ConnectingPoint>(
                        from ConnectingPoint direction in Enum.GetValues(typeof(ConnectingPoint))
                        where tile[direction].Type == areaType
                        select direction));

            return dict;
        }

        public static int NumberOfNeighborsTo(this IEnumerable<Tile> tiles, Tile other)
        {
            return tiles.Count(tile => tile | other);
        }
        /// <summary>
        /// Visszaad egy felsorolást azokból a "tiles"-ban szereplő mezőkből, melyek szomszédosak az "other" mezővel.
        /// </summary>
        /// <param name="tiles">Mezők kollekciója.</param>
        /// <param name="other">A vizsgált mező.</param>
        /// <returns></returns>
        public static IEnumerable<Tile> GetNeighboringTiles(this ICollection<Tile> tiles, Tile other)
        {
            return from Tile tile in tiles
                   where tile | other
                   select tile;
        }
    }
}
