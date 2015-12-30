using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Model.Representation
{
    /// <summary>
    /// Egy játékmező logikai reprezentációja, tartalmazza az absztrakt interakciós logikát.
    /// </summary>
    public class Tile
    {
        #region Declarations
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
    }
}
