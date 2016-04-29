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
    }
}
