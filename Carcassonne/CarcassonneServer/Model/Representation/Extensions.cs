using System;
using System.Collections.Generic;
using CarcassonneServer.Model.Representation.Construction;
using System.Linq;

namespace CarcassonneServer.Model.Representation
{
    public static class Extensions
    {
        public static void SetMeeple(this IEnumerable<BaseConstruction> constructions, Meeple meeple, Func<BaseConstruction, Meeple, bool> selector)
        {
            BaseConstruction construction = null;
            foreach (var item in constructions)
            {
                if (selector(item, meeple))
                    construction = item;
            }

            if (construction != null)
                construction.AddMeeple(meeple);
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
        /// <summary>
        /// Visszaad egy asszociatív tömböt, amiben minden oldaltípushoz egy listában szerepel, hogy a vizsgált mező mely irányú oldal olyan típusú.
        /// </summary>
        /// <param name="tile">A kérdéses mező.</param>
        /// <returns>Egy dictionary ami oldaltípushoz irány listát rendel.</returns>
        public static Dictionary<TileSideType, List<Direction>> GetDirectionsForAreaType(this Tile tile)
        {
            var dict = new Dictionary<TileSideType, List<Direction>>();

            foreach (TileSideType areaType in Enum.GetValues(typeof(TileSideType)))
                dict.Add(areaType,
                    new List<Direction>(
                        from Direction direction in Enum.GetValues(typeof(Direction))
                        where tile[direction].Type == areaType
                        select direction));

            return dict;
        }
    }
}
