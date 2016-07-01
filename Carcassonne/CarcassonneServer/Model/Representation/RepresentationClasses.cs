using System.IO;
using System.Collections.Generic;
using CarcassonneSharedModules.Tools;
using System;

namespace CarcassonneServer.Model.Representation
{
    #region Tile
    /// <summary>
    /// Egy mező négy oldalának típusait leíró osztály.
    /// Elérhetőek statikus példányok a szabvány mezőtípúsokhoz.
    /// </summary>
    public class TileDescriptor : 
        Dictionary<Direction, TileSideDescriptor>, 
        IPayloadContent
    {        
        private bool isMonastery;

        public bool IsMonastery { get { return isMonastery; } }
        /// <summary>
        /// Üres konstruktor a gyártáshoz.
        /// </summary>
        public TileDescriptor() { }
        /// <summary>
        /// Mező területleírójának konstruktora.
        /// </summary>
        /// <param name="up">A mező felső oldalát deifniáló leíró.</param>
        /// <param name="right">A mező jobb oldalát deifniáló leíró.</param>
        /// <param name="down">A mező alsó oldalát deifniáló leíró.</param>
        /// <param name="left">A mező bal oldalát deifniáló leíró.</param>
        public TileDescriptor(TileSideDescriptor up, TileSideDescriptor right, TileSideDescriptor down, TileSideDescriptor left, bool isMonastery = false)
        {
            this.Add(Direction.Up, up);
            this.Add(Direction.Right, right);
            this.Add(Direction.Down, down);
            this.Add(Direction.Left, left);
            this.isMonastery = isMonastery;
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

    public class TileSideDescriptor
    {
        //TODO: TileDescriptort refaktorálni, hogy TileSideDescriptorDecort használja
        public string ConstructionGuid { get; set; }
        public TileSideType Type { get { return descriptor.Type; } }
        public bool Closed { get { return descriptor.Closed; } }
        private StaticTileSideDescriptor descriptor;

        public TileSideDescriptor(StaticTileSideDescriptor descriptor)
        {
            this.descriptor = descriptor;
            ConstructionGuid = null;
        }
    }

    public static class TileSideDescriptorFactory
    {
        public static TileSideDescriptor Factory(StaticTileSideDescriptor singleton)
        {
            return new TileSideDescriptor(singleton);
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
        public TileSideType Type { get; private set; }
        /// <summary>
        /// Megmondja, hogy az adott egység zárja-e ezt a területet.
        /// </summary>
        public bool Closed { get; private set; }
        /// <summary>
        /// Oldalleíró konstruktora.
        /// </summary>
        /// <param name="type">Az oldal által definiált terület típusa.</param>
        /// <param name="closed">Az oldal területzárást deifniál-e.</param>
        public StaticTileSideDescriptor(TileSideType type, bool closed)
        {
            Type = type;
            Closed = closed;
        }

        #region Singleton type instances
        private static StaticTileSideDescriptor closedRoad = new StaticTileSideDescriptor(TileSideType.Road, true);
        public static StaticTileSideDescriptor ClosedRoad { get { return closedRoad; } }

        private static StaticTileSideDescriptor openRoad = new StaticTileSideDescriptor(TileSideType.Road, false);
        public static StaticTileSideDescriptor OpenRoad { get { return openRoad; } }

        private static StaticTileSideDescriptor openField = new StaticTileSideDescriptor(TileSideType.Field, false);
        public static StaticTileSideDescriptor OpenField { get { return openField; } }

        private static StaticTileSideDescriptor closedField = new StaticTileSideDescriptor(TileSideType.Field, true);
        public static StaticTileSideDescriptor ClosedField { get { return closedField; } }

        private static StaticTileSideDescriptor openCastle = new StaticTileSideDescriptor(TileSideType.Castle, false);
        public static StaticTileSideDescriptor OpenCastle { get { return openCastle; } }

        private static StaticTileSideDescriptor closedCastle = new StaticTileSideDescriptor(TileSideType.Castle, true);
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
    /// A mező négy oldalát definiáló felsorolás.
    /// </summary>
    public enum Direction : int
    {
        Up = 0,
        Right,
        Down,
        Left
    }
    /// <summary>
    /// A terület típusokat definiáló felsorolás.
    /// </summary>
    public enum TileSideType : short
    {
        Field = 0,
        Road = 1,
        Castle = 2
    }
    #endregion Tile
}
