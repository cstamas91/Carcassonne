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
    }
}
