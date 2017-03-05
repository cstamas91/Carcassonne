using CarcassonneSharedModules.Tools;
using System;
using System.Linq;
using System.Collections.Generic;
using CarcassonneServer.Model.Representation.SubAreas;

namespace CarcassonneServer.Model.Representation
{
    /// <summary>
    /// Egy játékmező logikai reprezentációja, tartalmazza az absztrakt interakciós logikát.
    /// </summary>
    public class Tile : Position
    {
        #region Declarations
        private int DIRECTION_MOD_VALUE = Enum.GetValues(typeof(Direction)).Cast<int>().Max() + 1;
        private TileRotation rotation;
        public TileRotation Rotation { get { return rotation; } set { rotation = value; } }
        private List<ISubArea> areas;
        /// <summary>
        /// A mezőn lévő alterületek listája.
        /// </summary>
        virtual public IEnumerable<ISubArea> Areas => areas;

        /// <summary>
        /// Segédfüggvény, ami egy abszolút irányból relatív irányt csinál.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        private Direction RotationAdjustedDirection(Direction direction)
        {
            return direction.RotationAdjustedDirection(Rotation);
        }

        /// <summary>
        /// Indexer, ami egy irány változóra visszaadja a mezőn belül ahhoz az irányhoz tartozó alterületet.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public ISubArea this[Direction direction]
        {
            get
            {
                IEnumerable<ISubArea> areasWithDirection = areas.Where(area => area[RotationAdjustedDirection(direction)]);

                if (areasWithDirection.Count() != 1)
                    throw new InvalidStateException("Egy irány egyetlen alterülethez tartozhat egy mezőn belül.");

                return areasWithDirection.FirstOrDefault();
            }
        }
        protected bool isMonastery;
        public bool IsMonastery => isMonastery;
        
        public Tile(List<ISubArea> subAreas) :base(-1, -1)
        {
            areas = subAreas;
            areas.ForEach(area => area.Parent = this);
            isMonastery = false;
        }
        #endregion Declarations
        /// <summary>
        /// Elforgatja a mezőt. A mező belső reprezentációjában van egy "rotation" tag, ami a mező aktuális elfordulását írja le, relatív a mező alapállapotához.
        /// </summary>
        public void Rotate()
        {
            Rotation = Rotation.GetNext();
        }  
        /// <summary>
        /// Kiértékeli, hogy a kapott mező emellé rakható-e.
        /// </summary>
        /// <param name="other">A másik játékmező.</param>
        /// <returns>A másik játékmező lerkaható-e emellé.</returns>
        public bool IsValidAdjacent(Tile other)
        {
            return this | other && IsSideTypeCompatible(other);
        }
        /// <summary>
        /// Kiértékeli, hogy a másik mező ezzel szemben lévő oldalán a terület típusok egyeznek.
        /// </summary>
        /// <param name="other">A másik játékmező.</param>
        /// <returns>Terület típusok szerint lerakható-e a mező emellé.</returns>
        private bool IsSideTypeCompatible(Tile other)
        {
            foreach (Direction facingMinorDirection in GetDirection(other).MinorDirections())
                if (this[facingMinorDirection].AreaType != other[facingMinorDirection.Opposite()].AreaType)
                    return false;
            return true;
        }
    }
}
