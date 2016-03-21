using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carcassonne.Model.Tools;
using System.IO;

namespace Carcassonne.Model.Representation
{
    public class Meeple : IPayloadContent<Meeple>
    {
        #region Declaration
        private Player owner;

        public Player Owner
        {
            get { return owner; }
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
            set { position = value; }
        }

        /// <summary>
        /// Meeple ktor. Létrejötterkor megkapja, hogy melyik játékos birtokolja. Alapból egyik meeple sincs használatban.
        /// </summary>
        /// <param name="owner">Birtokló játékos</param>
        public Meeple(Player owner)
        {
            this.owner = owner;
            this.inUse = false;
        }
        #endregion Declaration

        #region IPayloadContent
        public Meeple ReadContent()
        {
            throw new NotImplementedException();
        }

        public byte[] WriteContent()
        {
            if (!inUse)
                throw new InvalidOperationException("Meeple is not in use.");

            using (var ms = new MemoryStream())
            {
                using (var sw = new StreamWriter(ms))
                {
                    sw.Write(owner.WriteContent());
                    sw.Write(position.WriteContent());

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
