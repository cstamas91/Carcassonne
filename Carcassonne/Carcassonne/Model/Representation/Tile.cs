using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carcassonne.Model.Tools;
using System.IO;

namespace Carcassonne.Model.Representation
{
    /// <summary>
    /// Egy játékmező logikai reprezentációja, tartalmazza az absztrakt interakciós logikát.
    /// </summary>
    public class Tile : IPayloadContent
    {
        #region Declarations
        private Position position;

        public Position Position
        {
            get { return position; }
            set { position = value; }
        }

        private TileSideDescriptor sideDescriptor;

        public TileSideDescriptor SideDescriptor
        {
            get { return sideDescriptor; }
            set { sideDescriptor = value; }
        }

        public Tile() { }

        private Dictionary<TileFieldType, Meeple> meeples;

        public Dictionary<TileFieldType, Meeple> Meeples
        {
            get { return meeples; }
            set { meeples = value; }
        }

        private bool isMonastery;

        public bool IsMonastery
        {
            get { return isMonastery; }
            set { isMonastery = value; }
        }

        #endregion Declarations
        #region IPayloadContent
        public void ReadContent(byte[] payloadContent)
        {
            using (var ms = new MemoryStream(payloadContent)){
                var content = new byte[sizeof(short) * 2];
                var offset = ms.Read(content, 0, sizeof(short) * 2);
                this.Position = PayloadContentFactory<Position>.Create(content);

                var sideContent = new byte[sizeof(short) * 4];
                offset += ms.Read(sideContent, 0, sizeof(short) * 4);
                this.SideDescriptor = PayloadContentFactory<TileSideDescriptor>.Create(sideContent);
            }
        }


        public void WriteContent(Stream contentStream)
        {
            Position.WriteContent(contentStream);
            SideDescriptor.WriteContent(contentStream);
        }
        #endregion IPayloadContent


    }
}
