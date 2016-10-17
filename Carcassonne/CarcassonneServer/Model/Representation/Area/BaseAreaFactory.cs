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
            List<BaseArea> areas = new List<BaseArea>();

            foreach (ConnectingPoint item in Enum.GetValues(typeof(ConnectingPoint)))
            {
                var area = Factory(tile[item].Type);
                area.AddTile(tile);
                areas.Add(area);
            }

            List<BaseArea> finalArea = null;

            if (tile.IsMonastery)
            {
                FieldArea mergedFields = (from area in areas
                                                  where area.AreaType == AreaType.Field
                                                  select area).MergeArea() as FieldArea;

                IEnumerable<RoadArea> road = from area in areas
                                                     where area.AreaType == AreaType.Road
                                                     select area as RoadArea;

                if (road.Count() != 1)
                    throw new InvalidOperationException();

                finalArea = new List<BaseArea>() { mergedFields, road.First() };
            }                                                                                

            return finalArea;
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
