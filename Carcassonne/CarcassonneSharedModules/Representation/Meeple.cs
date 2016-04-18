using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarcassonneSharedModules.Tools;
using System.IO;

namespace CarcassonneSharedModules.Representation
{
    public class Meeple : IPayloadContent
    {
        #region Declaration
        public short OwnerId
        {
            get;
            private set;
        }

        private bool inUse;

        public bool InUse
        {
            get { return inUse; }
            set { inUse = value; }
        }

        private Position position;
        public Position Position
        {
            get { return position; }
            private set { position = value; }
        }

        /// <summary>
        /// Üres ktor a PayloadContentFactoryhez.
        /// </summary>
        public Meeple() { }

        /// <summary>
        /// Meeple ktor. Létrejötterkor megkapja, hogy melyik játékos birtokolja. Alapból egyik meeple sincs használatban.
        /// </summary>
        /// <param name="owner">Birtokló játékos</param>
        public Meeple(short id)
        {
            this.OwnerId = id;
            this.InUse = false;
        }
        #endregion Declaration

        #region IPayloadContent
        public void ReadContent(byte[] payloadContent)
        {
            using (var ms = new MemoryStream(payloadContent))
            {
                var playerContent = new byte[sizeof(short)];
                ms.Read(playerContent, 0, sizeof(short));
                this.OwnerId = BitConverter.ToInt16(playerContent, 0);

                var positionContent = new byte[sizeof(short) * 2];
                ms.Read(positionContent, 0, sizeof(short) * 2);
                this.Position = PayloadContentFactory<Position>.Create(positionContent);
            }
        }

        public void WriteContent(Stream contentStream)
        {
            if (!inUse)
                throw new InvalidOperationException("Meeple is not in use.");

            contentStream.WriteShort(OwnerId);
            Position.WriteContent(contentStream);
        }
        #endregion IPayloadContent

    }
}
