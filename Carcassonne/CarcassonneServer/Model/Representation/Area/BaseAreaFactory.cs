using System;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneServer.Model.Representation.Area
{
    public static class BaseAreaFactory
    {
        public static BaseArea MergeArea(this IEnumerable<BaseArea> collection)
        {
            return collection.Aggregate((first, next) => first.Merge(next));
        }

        public static List<BaseArea> Factory(Tile tile)
        {
            throw new NotImplementedException();
        }

        private static BaseArea Factory(AreaType tsd)
        {
            switch (tsd)
            {
                case AreaType.Castle: return new CastleArea();
                case AreaType.Field: return new FieldArea();
                default: return new RoadArea();
            }
        }
    }
}
