using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carcassonne.Model.Tools;
using System.IO;

namespace Carcassonne.Model.Representation
{
    #region Tile
    public struct TileSideDescriptor : IPayloadContent<TileSideDescriptor>
    {
        public TileSideDescriptor(TileSideType up, TileSideType down, TileSideType left, TileSideType right)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
        }
        public TileSideType Up, Down, Left, Right;
        #region IPayloadContent
        public TileSideDescriptor ReadContent()
        {
            throw new NotImplementedException();
        }

        public byte[] WriteContent()
        {
            using (var ms = new MemoryStream())
            {
                using (var sw = new StreamWriter(ms))
                {
                    sw.Write(Up);
                    sw.Write(Down);
                    sw.Write(Left);
                    sw.Write(Right);

                    var content = new byte[ms.Length];
                    using (var contentStream = new MemoryStream(content))
                        ms.WriteTo(contentStream);

                    return content;
                }
            }
        }
        #endregion IPayloadContent
    }

    public enum TileSideType : short
    {
        Field = 0,
        Road = 1,
        Castle = 2
    }

    public enum TileFieldType : short
    {
        Road = 0,
        Field = 1,
        Castle = 2,
        Monastery = 3
    }
    #endregion Tile
}
