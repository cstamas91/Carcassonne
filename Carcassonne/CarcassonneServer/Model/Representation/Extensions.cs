using System;
using System.Collections.Generic;
using CarcassonneServer.Model.Representation.Area;
using System.Linq;

namespace CarcassonneServer.Model.Representation
{
    public static class Extensions
    {
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
