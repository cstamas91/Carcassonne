using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carcassonne.Model.Tools;
using System.IO;

namespace Carcassonne.Model.Representation
{
    public class Position : IPayloadContent<Position>
    {
        public short X { get; set; }
        public short Y { get; set; }
        #region IPayloadContent
        public Position ReadContent()
        {
            throw new NotImplementedException();
        }

        public byte[] WriteContent()
        {
            using (var ms = new MemoryStream())
            {
                using (var sw = new StreamWriter(ms))
                {
                    sw.Write(X);
                    sw.Write(Y);

                    var content = new byte[ms.Length];
                    using (var contentStream = new MemoryStream(content))
                        ms.WriteTo(contentStream);

                    return content;
                }
            }
        }
        #endregion IPayloadContent
    }
}
