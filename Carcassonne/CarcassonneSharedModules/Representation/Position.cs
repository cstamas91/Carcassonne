using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarcassonneSharedModules.Tools;
using System.IO;

namespace CarcassonneSharedModules.Representation
{
    public class Position : IPayloadContent
    {
        public short X { get; set; }
        public short Y { get; set; }


        public Position() { }

        #region IPayloadContent
        public void ReadContent(byte[] payloadContent)
        {
            using (var ms = new MemoryStream(payloadContent))
            {
                var content = new byte[sizeof(short)];
                var offset = ms.Read(content, 0, sizeof(short));
                X = BitConverter.ToInt16(content, 0);
                offset += ms.Read(content, offset, sizeof(short));
                Y = BitConverter.ToInt16(content, 0);
            }
        }

        public void WriteContent(Stream contentStream)
        {
            contentStream.WriteShort(X);
            contentStream.WriteShort(Y);
        }
        
        #endregion IPayloadContent
    }
}
