using System;
using System.Collections.Generic;
using CarcassonneServer.Model.Representation.Construction;

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
    }
}
