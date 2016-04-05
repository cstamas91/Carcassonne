using Carcassonne.Model.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Model.Representation
{
    public class GameTable : INotifyPropertyChanged, IPayloadContent
    {
        public short Dimension { get; set; }
        public ICollection<Tile> Tiles { get; set; }
        public ICollection<Meeple> Meeples { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public GameTable() { }

        #region IPayloadContent
        public void ReadContent(byte[] payloadContent)
        {
            using (var ms = new MemoryStream(payloadContent))
            {
                var content = new byte[sizeof(short)];
                ms.Read(content, 0, sizeof(short));
                Dimension = BitConverter.ToInt16(content, 0);

                ms.Read(content, 0, sizeof(short));
                var tileCount = BitConverter.ToInt16(content, 0);
                Tiles = new List<Tile>();
                var tileContent = new byte[sizeof(short) * 10];
                for (int i = 0; i < tileCount; i++)
                {
                    ms.Read(tileContent, 0, sizeof(short) * 10);
                    Tiles.Add(PayloadContentFactory<Tile>.Create(tileContent));
                }

                ms.Read(content, 0, sizeof(short));
                var meepleCount = BitConverter.ToInt16(content, 0);
                Meeples = new List<Meeple>();
                var meepleContent = new byte[sizeof(short) * 3];
                for(int i = 0; i < meepleCount; i++)
                {
                    ms.Read(meepleContent, 0, sizeof(short) * 3);
                    Meeples.Add(PayloadContentFactory<Meeple>.Create(meepleContent));
                }
            }
        }

        public void WriteContent(Stream contentStream)
        {
            contentStream.WriteShort(Dimension);

            contentStream.WriteShort((short)Tiles.Count);
            foreach (var tile in Tiles)
                tile.WriteContent(contentStream);

            contentStream.WriteShort((short)Meeples.Count);
            foreach (var meeple in Meeples)
                meeple.WriteContent(contentStream);
        }
        #endregion IPayloadContent



    }
}
