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

        private static TileDescriptor castleInner = new TileDescriptor(TileSideDescriptor.OpenCastle, TileSideDescriptor.OpenCastle, TileSideDescriptor.OpenCastle, TileSideDescriptor.OpenCastle);
        public static TileDescriptor CastleInner { get { return castleInner; } }
        
        private static TileDescriptor monastery = new TileDescriptor(TileSideDescriptor.OpenField, TileSideDescriptor.OpenField, TileSideDescriptor.OpenField, TileSideDescriptor.OpenField, true);
        public static TileDescriptor Monastery { get { return monastery; } }
                  
        private static readonly TileDescriptor curvyRoad = new TileDescriptor(TileSideDescriptor.OpenRoad, TileSideDescriptor.OpenRoad, TileSideDescriptor.ClosedField, TileSideDescriptor.ClosedField);
        public static TileDescriptor CurvyRoad { get { return curvyRoad; } }

        private static readonly TileDescriptor allField = new TileDescriptor(TileSideDescriptor.OpenField, TileSideDescriptor.OpenField, TileSideDescriptor.OpenField, TileSideDescriptor.OpenField);
        public static TileDescriptor AllField { get { return allField; } }
    }
    /// <summary>
    /// A mező egy oldalát reprezentáló struktúra.
    /// Elérhetőek statikus példányok a különböző oldaltípusokhoz.
    /// </summary>
    public struct TileSideDescriptor
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
        public TileSideDescriptor(TileSideType type, bool closed)
        {
            Type = type;
            Closed = closed;
        }

        private static TileSideDescriptor closedRoad = new TileSideDescriptor(TileSideType.Road, true);
        public static TileSideDescriptor ClosedRoad { get { return closedRoad; } }

        private static TileSideDescriptor openRoad = new TileSideDescriptor(TileSideType.Road, false);
        public static TileSideDescriptor OpenRoad { get { return openRoad; } }

        private static TileSideDescriptor openField = new TileSideDescriptor(TileSideType.Field, false);
        public static TileSideDescriptor OpenField { get { return openField; } }

        private static TileSideDescriptor closedField = new TileSideDescriptor(TileSideType.Field, true);
        public static TileSideDescriptor ClosedField { get { return closedField; } }

        private static TileSideDescriptor openCastle = new TileSideDescriptor(TileSideType.Castle, false);
        public static TileSideDescriptor OpenCastle { get { return openCastle; } }

        private static TileSideDescriptor closedCastle = new TileSideDescriptor(TileSideType.Castle, true);
        public static TileSideDescriptor ClosedCastle { get { return closedCastle; } }
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
