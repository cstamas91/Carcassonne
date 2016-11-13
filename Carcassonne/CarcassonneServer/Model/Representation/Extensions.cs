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
        public static Direction Opposite(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Down: return Direction.Up;
                case Direction.Up: return Direction.Down;
                case Direction.Left: return Direction.Right;
                default: return Direction.Left;
            }
        }

        public static List<Direction> MinorDirections(this Direction majorDirection)
        {
            switch (majorDirection)
            {
                case Direction.Down:
                    return new List<Direction>() { Direction.Down, Direction.DownLeft, Direction.DownRight };
                case Direction.Left:
                    return new List<Direction>() { Direction.Left, Direction.LeftDown, Direction.LeftUp };
                case Direction.Right:
                    return new List<Direction>() { Direction.Right, Direction.RightDown, Direction.RightUp };
                case Direction.Up:
                    return new List<Direction>() { Direction.Up, Direction.UpLeft, Direction.UpRight };
                default:
                    throw new ArgumentException(string.Format("{0} nem fő irány.", majorDirection.ToString()));
            }
        }

        /// <summary>
        /// Visszaad egy asszociatív tömböt, amiben minden oldaltípushoz egy listában szerepel, hogy a vizsgált mező mely irányú oldala olyan típusú.
        /// </summary>
        /// <param name="tile">A kérdéses mező.</param>
        /// <returns>Egy dictionary ami oldaltípushoz irány listát rendel.</returns>
        public static Dictionary<AreaType, List<Direction>> GetDirectionsForAreaType(this Tile tile)
        {
            var dict = new Dictionary<AreaType, List<Direction>>();

            foreach (AreaType areaType in Enum.GetValues(typeof(AreaType)))
                dict.Add(areaType,
                    new List<Direction>(
                        from Direction direction in Enum.GetValues(typeof(Direction))
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
