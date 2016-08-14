using CarcassonneSharedModules.Tools;
using System.Collections.Generic;
using System.IO;
using CarcassonneServer.Model.Representation.Construction;
using System.Linq;

namespace CarcassonneServer.Model.Representation
{
    public class GameTable : IPayloadContent
    {
        private List<BaseConstruction> constructions = new List<BaseConstruction>();
        public IEnumerable<BaseConstruction> Constructions { get { return constructions.AsEnumerable(); } }
        private Tile lastAddedTile;
        private Meeple lastAddedMeeple;
        public GameTable() { }

        public void SetTile(ref Tile tile)
        {
            Tile tileNonref = tile;

            var neighboringConstructions = from construction in constructions
                                           where construction | tileNonref
                                           select construction;

            if (neighboringConstructions.Count() != 0)
            {
                foreach (var item in neighboringConstructions)
                    item.AddElement(ref tile);
            }
            else
                constructions.AddRange(BaseConstructionFactory.Factory(ref tile));
        }

        public void SetMeeple(Meeple meeple)
        {
            constructions.SetMeeple(meeple, (c, m) => c.GUID == m.ConstructionGuid);
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
