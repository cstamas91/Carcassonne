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
    public class Tile : IPayloadContent<Tile>
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
        public Tile ReadContent()
        {
            throw new NotImplementedException();
        }
        
        public byte[] WriteContent()
        {
            using (var ms = new MemoryStream())
            {
                using (var sw = new StreamWriter(ms))
                {
                    sw.Write(position.WriteContent());
                    sw.Write(sideDescriptor.WriteContent());

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
