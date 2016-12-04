using CarcassonneSharedModules.Tools;
using System.Collections.Generic;
using System.IO;
using CarcassonneServer.Model.Representation.Area;
using System.Linq;
using System;

namespace CarcassonneServer.Model.Representation
{
    public class GameTable
    {
        private List<BaseArea> areas = new List<BaseArea>();
        public IEnumerable<BaseArea> Areas { get { return areas.AsEnumerable(); } }
        private Tile lastAddedTile;
        private Meeple lastAddedMeeple;
        public GameTable() { }

        public void SetTile(Tile tile)
        {
            throw new NotImplementedException();
        }

        public void SetMeeple(Meeple meeple)
        {
            throw new NotImplementedException();
        }
    }
}
