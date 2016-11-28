using System.IO;
using System.Collections.Generic;
using CarcassonneSharedModules.Tools;
using System;
using System.Runtime.Serialization;
using System.Linq;

namespace CarcassonneServer.Model.Representation
{ 
    #region Tile
    /// <summary>
    /// Egy mező négy oldalának típusait leíró osztály.
    /// Elérhetőek statikus példányok a szabvány mezőtípúsokhoz.
    /// </summary>
    [Serializable]
    public class TileDescriptor : 
        Dictionary<Direction, TileSideDescriptor>, 
        ISerializable
    {        
        /// <summary>
        /// Indexer az egy Területhez tartozó oldalak könnyű elérésére a Terület azonosítóján keresztül.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public IEnumerable<TileSideDescriptor> this[string guid] { get { return this.Values.Where(d => d.AreaGuid == guid); } }

        private bool isMonastery;
        public bool IsMonastery { get { return isMonastery; } }

        /// <summary>
        /// Rendszerezi, hogy egy bizonyos irányból nézve, mely másik irányok érhetőek el közvetlenül az elemen belül.
        /// </summary>
        public Dictionary<Direction, IEnumerable<Direction>> AccessibleFrom
        {
            get
            {
                return accessibleFrom;
            }

            set
            {
                accessibleFrom = value;
            }
        }
        private Dictionary<Direction, IEnumerable<Direction>> accessibleFrom = new Dictionary<Direction, IEnumerable<Direction>>();

        /// <summary>
        /// Üres konstruktor a gyártáshoz.
        /// </summary>
        public TileDescriptor() { }
    }

    public class TileSideDescriptor
    {
        public string AreaGuid { get; set; }
        public AreaType Type { get { return descriptor.Type; } }
        public bool Closed { get { return descriptor.Closed; } }
        public Tile Parent { get; private set; }
        protected StaticTileSideDescriptor descriptor;

        public TileSideDescriptor(StaticTileSideDescriptor descriptor, Tile parent)
        {
            this.Parent = parent;
            this.descriptor = descriptor;
            AreaGuid = null;
        }
    }
    
    public static class TileSideDescriptorFactory
    {
        public static TileSideDescriptor Factory(StaticTileSideDescriptor singleton, Tile parent)
        {
            return new TileSideDescriptor(singleton, parent);
        }
    }

    /// <summary>
    /// A mező egy oldalát reprezentáló struktúra.
    /// Elérhetőek statikus példányok a különböző oldaltípusokhoz.
    /// </summary>
    public class StaticTileSideDescriptor
    {
        /// <summary>
        /// A terület típusa.
        /// </summary>
        public AreaType Type { get; private set; }
        /// <summary>
        /// Megmondja, hogy az adott egység zárja-e ezt a területet.
        /// </summary>
        public bool Closed { get; private set; }
        /// <summary>
        /// Oldalleíró konstruktora.
        /// </summary>
        /// <param name="type">Az oldal által definiált terület típusa.</param>
        /// <param name="closed">Az oldal területzárást deifniál-e.</param>
        private  StaticTileSideDescriptor(AreaType type, bool closed)
        {
            Type = type;
            Closed = closed;
        }

        #region Singleton type instances
        private static StaticTileSideDescriptor closedRoad = new StaticTileSideDescriptor(AreaType.Road, true);
        public static StaticTileSideDescriptor ClosedRoad { get { return closedRoad; } }

        private static StaticTileSideDescriptor openRoad = new StaticTileSideDescriptor(AreaType.Road, false);
        public static StaticTileSideDescriptor OpenRoad { get { return openRoad; } }

        private static StaticTileSideDescriptor openField = new StaticTileSideDescriptor(AreaType.Field, false);
        public static StaticTileSideDescriptor OpenField { get { return openField; } }

        private static StaticTileSideDescriptor closedField = new StaticTileSideDescriptor(AreaType.Field, true);
        public static StaticTileSideDescriptor ClosedField { get { return closedField; } }

        private static StaticTileSideDescriptor openCastle = new StaticTileSideDescriptor(AreaType.Castle, false);
        public static StaticTileSideDescriptor OpenCastle { get { return openCastle; } }

        private static StaticTileSideDescriptor closedCastle = new StaticTileSideDescriptor(AreaType.Castle, true);
        public static StaticTileSideDescriptor ClosedCastle { get { return closedCastle; } }
        #endregion Singleton type instances
    }
    /// <summary>
    /// Mező elforgatását reprezentáló felsoroló.
    /// </summary>
    public enum TileRotation
    {
        _90 = 1,
        _180,
        _270
    }
    /// <summary>
    /// A mező érintkezési pontjait definiáló felsorolás.
    /// </summary>
    public enum Direction : int
    {
        Up = 1,
        UpRight = 2,
        RightUp = 3,
        Right = 4,
        RightDown = 5,
        DownRight = 6,
        Down = 7,
        DownLeft = 8,
        LeftDown = 9,
        Left = 10,
        LeftUp = 11,
        UpLeft = 12,
    }
    /// <summary>
    /// A terület típusokat definiáló felsorolás.
    /// </summary>
    public enum AreaType : short
    {
        Field = 0,
        Road = 1,
        Castle = 2
    }
    #endregion Tile
}
