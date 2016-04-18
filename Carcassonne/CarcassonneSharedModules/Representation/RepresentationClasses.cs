using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarcassonneSharedModules.Tools;
using System.IO;

namespace CarcassonneSharedModules.Representation
{
    #region Tile
    public struct TileSideDescriptor : IPayloadContent
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
        public void ReadContent(byte[] payloadContent)
        {
            using (var ms = new MemoryStream(payloadContent))
            {
                var content = new byte[sizeof(short)];
                ms.Read(content, 0, sizeof(short));
                this.Up = (TileSideType)BitConverter.ToInt16(content, 0);

                ms.Read(content, 0, sizeof(short));
                this.Right = (TileSideType)BitConverter.ToInt16(content, 0);

                ms.Read(content, 0, sizeof(short));
                this.Down = (TileSideType)BitConverter.ToInt16(content, 0);

                ms.Read(content, 0, sizeof(short));
                this.Left = (TileSideType)BitConverter.ToInt16(content, 0);
            }
            
        }

        public void WriteContent(Stream contentStream)
        {
            contentStream.WriteShort((short)Up);
            contentStream.WriteShort((short)Down);
            contentStream.WriteShort((short)Left);
            contentStream.WriteShort((short)Right);
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
