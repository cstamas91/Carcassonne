using CarcassonneSharedModules.Tools;
using System.IO;
using System;
using System.Linq;

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
        private readonly TileDescriptor sideDescriptor;

        private Direction RotationAdjustedDirection(Direction direction)
        {
            return (Direction)((((short)direction - (short)rotation) + DIRECTION_MOD_VALUE) % DIRECTION_MOD_VALUE);
        }
        public TileSideDescriptor Up { get { return sideDescriptor[RotationAdjustedDirection(Direction.Up)]; } }
        public TileSideDescriptor Right { get { return sideDescriptor[RotationAdjustedDirection(Direction.Right)]; } }
        public TileSideDescriptor Down { get { return sideDescriptor[RotationAdjustedDirection(Direction.Down)]; } }
        public TileSideDescriptor Left { get { return sideDescriptor[RotationAdjustedDirection(Direction.Left)]; } }

        public TileSideDescriptor this[Direction direction] { get { return sideDescriptor[RotationAdjustedDirection(direction)]; } } 
        public bool IsMonastery { get { return sideDescriptor.IsMonastery; } }

        public Tile() { }

        public Tile(TileDescriptor tileDescriptor)
        {
            this.sideDescriptor = tileDescriptor;
        }

        public Tile(TileDescriptor tileDescriptor, Position pos)
            :base(pos.X, pos.Y)
        {
            this.sideDescriptor = tileDescriptor;
        }
        #endregion Declarations

        #region IPayloadContent
        override public void ReadContent(byte[] payloadContent)
        {
        }
                              
        override public void WriteContent(Stream contentStream)
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

        public void Rotate()
        {
            this.rotation = (TileRotation)((int)(this.rotation + 1) % Enum.GetValues(typeof(TileRotation)).Cast<int>().Max());
        }
    }
}
