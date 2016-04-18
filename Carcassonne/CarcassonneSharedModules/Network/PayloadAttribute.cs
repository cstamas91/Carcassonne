using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneSharedModules.Network
{
    public class PayloadAttribute : Attribute
    {
        public int Index { get; set; }
        public PropertyType ContentType { get; set; }
        public int Size { get; set; }
        public string Name { get; set; }
        public Type BaseType { get; set; }
        public PayloadAttribute(int ind, int size, PropertyType contentType, string name, Type baseType = null)
        {
            Index = ind;
            Size = size;
            ContentType = contentType;
            Name = name;
            BaseType = BaseType;
        }
    }

    public enum PropertyType
    {
        Value = 0,
        Collection = 1
    }
}
