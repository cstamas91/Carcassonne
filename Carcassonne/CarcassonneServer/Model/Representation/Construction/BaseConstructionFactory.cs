using System;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneServer.Model.Representation.Construction
{
    public static class BaseConstructionFactory
    {
        public static BaseConstruction MergeConstructions(this IEnumerable<BaseConstruction> collection)
        {
            return collection.Aggregate((first, next) => first.Merge(next));
        }

        public static List<BaseConstruction> Factory(ref Tile tile)
        {
            List<BaseConstruction> constructions = new List<BaseConstruction>();

            foreach (Direction item in Enum.GetValues(typeof(Direction)))
            {
                var construction = Factory(tile[item].Type);
                construction.AddElement(ref tile);
                constructions.Add(construction);
            }

            List<BaseConstruction> finalConstructions = null;

            if (tile.IsMonastery)
            {
                FieldConstruction mergedFields = (from construction in constructions
                                                  where construction.AreaType == TileSideType.Field
                                                  select construction).MergeConstructions() as FieldConstruction;

                IEnumerable<RoadConstruction> road = from construction in constructions
                                                     where construction.AreaType == TileSideType.Road
                                                     select construction as RoadConstruction;

                if (road.Count() != 1)
                    throw new InvalidOperationException();

                finalConstructions = new List<BaseConstruction>() { mergedFields, road.First() };
            }                                                                                

            return finalConstructions;
        }

        private static BaseConstruction Factory(TileSideType tsd)
        {
            switch (tsd)
            {
                case TileSideType.Castle: return new CastleConstruction();
                case TileSideType.Field: return new FieldConstruction();
                default: return new RoadConstruction();
            }
        }
    }
}
