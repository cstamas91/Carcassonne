using System.Collections.Generic;
using CarcassonneSharedModules.Tools;
using System.IO;
using System;
using System.Linq;

namespace CarcassonneServer.Model.Representation
{
    /// <summary>
    /// Egy játékmező logikai reprezentációja, tartalmazza az absztrakt interakciós logikát.
    /// </summary>
    public class Tile : IPayloadContent
    {
        #region Declarations
        private int DIRECTION_MOD_VALUE = Enum.GetValues(typeof(Direction)).Cast<int>().Max() + 1;
        private Position position;
        public Position Position
        {
            get { return position; }
            set { position = value; }
        }

        private TileRotation rotation;
        private readonly TileDescriptor sideDescriptor;
        public TileSideDescriptor Up { get { return sideDescriptor[(Direction)((((short)Direction.Up - (short)rotation) + DIRECTION_MOD_VALUE) % DIRECTION_MOD_VALUE)]; } }
        public TileSideDescriptor Right { get { return sideDescriptor[(Direction)((((short)Direction.Right - (short)rotation) + DIRECTION_MOD_VALUE) % DIRECTION_MOD_VALUE)]; } }
        public TileSideDescriptor Down { get { return sideDescriptor[(Direction)((((short)Direction.Down - (short)rotation) + DIRECTION_MOD_VALUE) % DIRECTION_MOD_VALUE)]; } }
        public TileSideDescriptor Left { get { return sideDescriptor[(Direction)((((short)Direction.Left - (short)rotation) + DIRECTION_MOD_VALUE) % DIRECTION_MOD_VALUE)]; } }

        public bool IsMonastery { get { return sideDescriptor.IsMonastery; } }

        public Tile() { }

        public Tile(TileDescriptor tileDescriptor)
        {
            this.sideDescriptor = tileDescriptor;
        }
        #endregion Declarations

        #region IPayloadContent
        public void ReadContent(byte[] payloadContent)
        {
        }
                              
        public void WriteContent(Stream contentStream)
        {
        }
        #endregion IPayloadContent

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

        private bool NeighbourTo(Tile other)
        {
            return Position | other.Position;
        }

        public void Rotate()
        {
            this.rotation = (TileRotation)((int)(this.rotation + 1) % Enum.GetValues(typeof(TileRotation)).Cast<int>().Max());
        }
    }
}
