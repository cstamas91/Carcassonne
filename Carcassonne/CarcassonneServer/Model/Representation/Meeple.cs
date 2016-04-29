using System;
using CarcassonneSharedModules.Tools;
using System.IO;

namespace CarcassonneServer.Model.Representation
{
    public class Meeple : Position, IPayloadContent
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

        private string constructionGuid;
        public string ConstructionGuid
        {
            get { return constructionGuid;} 
            private set { this.constructionGuid = value; }
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
        public override void ReadContent(byte[] payloadContent)
        {
            base.ReadContent(payloadContent);
        }

        public override void WriteContent(Stream contentStream)
        {
            base.WriteContent(contentStream);
        }
        #endregion IPayloadContent

    }
}
