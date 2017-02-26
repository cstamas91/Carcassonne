using System;
using System.Collections.Generic;
using CarcassonneServer.Model.Representation.Area;
using System.Linq;

namespace CarcassonneServer.Model.Representation
{
    public static class Extensions
    {
        public static Direction RotationAdjustedDirection(this Direction direction, TileRotation Rotation)
        {
            int DIRECTION_MOD_VALUE = Enum.GetValues(typeof(Direction)).Length;
            int rotationEnumCount = Enum.GetValues(typeof(TileRotation)).Length;
            int rotationDisplaceValue = DIRECTION_MOD_VALUE / rotationEnumCount;

            return (Direction)(
                (((short)direction + 
                ((short)rotationDisplaceValue * (short)Rotation)))
                % DIRECTION_MOD_VALUE);
        }

        public static Direction GetTileDirectionFromAreaDirection(this Direction areaDirection, TileRotation rotation)
        {
            int DIRECTION_MOD_VALUE = Enum.GetValues(typeof(Direction)).Length;
            int rotationEnumCount = Enum.GetValues(typeof(TileRotation)).Length;
            int rotationDisplaceValue = DIRECTION_MOD_VALUE / rotationEnumCount;

            var initialResult = ((short)areaDirection - ((short)rotationDisplaceValue * (short)rotation));
            if (initialResult < 0)
                initialResult += DIRECTION_MOD_VALUE;

            var retValue = (Direction)(initialResult % DIRECTION_MOD_VALUE); 
            return retValue;
        }

        public static Direction Opposite(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Down: return Direction.Up;
                case Direction.DownLeft: return Direction.UpLeft;
                case Direction.DownRight: return Direction.UpRight;
                case Direction.Up: return Direction.Down;
                case Direction.UpLeft: return Direction.DownLeft;
                case Direction.UpRight: return Direction.DownRight;
                case Direction.Left: return Direction.Right;
                case Direction.LeftUp: return Direction.RightUp;
                case Direction.LeftDown: return Direction.RightDown;
                case Direction.Right: return Direction.Left;
                case Direction.RightUp: return Direction.LeftUp;
                case Direction.RightDown: return Direction.LeftDown;
                default: throw new ArgumentException("Nem maradt több irány.");
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

        public static IEnumerable<Y> MostOf<T, Y>(this IEnumerable<T> collection, Func<T, Y> selector)
        {
            Dictionary<Y, int> counts = new Dictionary<Y, int>();
            counts = collection.GroupBy(selector).ToDictionary(g => g.Key, g => g.Count());
            int max = counts.Values.Max();

            return counts.Keys.Where(item => counts[item] == max);
        }

        public static IEnumerable<T> GetEnumValues<T>()
            where T: struct
        {
            return Enum.GetValues(typeof(T)) as IEnumerable<T>;
        }
    }
}
