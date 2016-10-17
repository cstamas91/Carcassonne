using CarcassonneSharedModules.Tools;
using System.Collections.Generic;
using System.IO;
using CarcassonneServer.Model.Representation.Area;
using System.Linq;

namespace CarcassonneServer.Model.Representation
{
    public class GameTable : IPayloadContent
    {
        private List<BaseArea> areas = new List<BaseArea>();
        public IEnumerable<BaseArea> Areas { get { return areas.AsEnumerable(); } }
        private Tile lastAddedTile;
        private Meeple lastAddedMeeple;
        public GameTable() { }

        public void SetTile(Tile tile)
        {
            var neighboringAreas = from area in areas
                                           where area | tile
                                           select area;

            if (neighboringAreas.Count() != 0)
            {
                foreach (var item in neighboringAreas)
                    item.AddSubArea(tile);
            }
            else
                areas.AddRange(BaseAreaFactory.Factory(tile));
        }

        public void SetMeeple(Meeple meeple)
        {
            areas.SetMeeple(meeple, (c, m) => c.GUID == m.AreaGuid);
        }

        #region IPayloadContent
        public void ReadContent(byte[] payloadContent)
        {

        }

        public void WriteContent(Stream contentStream)
        {

        }
        #endregion IPayloadContent
    }
}
