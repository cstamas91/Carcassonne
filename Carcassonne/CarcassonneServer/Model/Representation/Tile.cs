﻿using CarcassonneSharedModules.Tools;
using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CarcassonneServer.Model.Representation
{
    /// <summary>
    /// Egy játékmező logikai reprezentációja, tartalmazza az absztrakt interakciós logikát.
    /// </summary>
    public class Tile : Position, IPayloadContent
    {
        #region Declarations
        private int DIRECTION_MOD_VALUE = Enum.GetValues(typeof(Direction)).Cast<int>().Max() + 1;
        private TileRotation rotation;
        private List<SubArea> areas;
        public List<SubArea> Areas
        {
            get
            {
                return areas;
            }
        }

        private Direction RotationAdjustedDirection(Direction direction)
        {
            return (Direction)((((short)direction - (short)rotation) + DIRECTION_MOD_VALUE) % DIRECTION_MOD_VALUE);
        }

        public SubArea this[Direction direction]
        {
            get
            {
                IEnumerable<SubArea> areasWithDirection = areas.Where(area => area[direction]);

                if (areasWithDirection.Count() != 1)
                    throw new InvalidStateException("Egy irány egyetlen alterülethez tartozhat egy mezőn belül.");

                return areasWithDirection.FirstOrDefault();
            }
        }
        public bool IsMonastery { get { return sideDescriptor.IsMonastery; } }

        public Tile() { }
        public Tile(IList<SubArea> subAreas, Position position)
            : base (position.X, position.Y)
        {
            areas = subAreas.ToList();
        }
        #endregion Declarations

        public void Rotate()
        {
            this.rotation = (TileRotation)((int)(this.rotation + 1) % Enum.GetValues(typeof(TileRotation)).Cast<int>().Max());
        }

        #region IPayloadContent
        override public void ReadContent(byte[] payloadContent)
        {
        }

        override public void WriteContent(Stream contentStream)
        {
        }
        #endregion IPayloadContent

        public bool CanBeAdjacentWith(Tile other)
        {
            return this | other && FacingSidesAreCompatible(other);
        }

        private bool FacingSidesAreCompatible(Tile other)
        {
            foreach (Direction facingMinorDirection in AdjacentDirection(other).MinorDirections())
                if (this[facingMinorDirection].AreaType != other[facingMinorDirection.Opposite()].AreaType)
                    return false;

            return true;
        }

        #region Deprecated
        protected readonly TileDescriptor sideDescriptor;
        public IEnumerable<TileSideDescriptor> Sides
        {
            get
            {
                return sideDescriptor.Values;
            }
        }
        public Tile(TileDescriptor tileDescriptor)
        {
            this.sideDescriptor = tileDescriptor;
        }
        public Tile(TileDescriptor tileDescriptor, Position pos)
            : base(pos.X, pos.Y)
        {
            this.sideDescriptor = tileDescriptor;
        }

        /// <summary>
        /// Operátor túltöltés szomszédsági kapcsolat eldöntéséhez.
        /// </summary>
        /// <param name="lhs">Bal Tile</param>
        /// <param name="rhs">Jobb Tile</param>
        /// <returns>Igazat, ha Bal és Jobb szomszédok, egyébként hamisat.</returns>
        public static bool operator |(Tile lhs, Tile rhs)
        {
            return lhs.NeighbourTo(rhs);
        }
        #endregion Deprecated
    }
}
