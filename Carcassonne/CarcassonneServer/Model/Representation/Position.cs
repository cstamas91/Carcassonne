using System;
using CarcassonneSharedModules.Tools;
using System.IO;

namespace CarcassonneServer.Model.Representation
{
    public class Position
    {
        public short X { get; set; }
        public short Y { get; set; }


        public Position() { }
        public Position(short x, short y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Operátor túltöltés szomszédsági kapcsolat eldöntéséhez.
        /// </summary>
        /// <param name="lhs">Bal pozíció</param>
        /// <param name="rhs">Jobb pozíció</param>
        /// <returns>Igazat, ha Bal és Jobb szomszédok, egyébként hamisat.</returns>
        public static bool operator |(Position lhs, Position rhs)
        {
            return lhs.NeighbourTo(rhs);
        }

        virtual protected bool NeighbourTo(Position other)
        {
            if (Math.Abs(this.X - other.X) == 1 || Math.Abs(this.Y - other.Y) == 1)
                return true;

            return false;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            Position other = obj as Position;

            if (other == null)
                return false;

            return other.X == X && other.Y == Y;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Direction AdjacentDirection(Position other)
        {
            if(!(this | other))
                throw new InvalidOperationException();

            if (X + 1 == other.X)
                return Direction.Up;
            else if (X - 1 == other.X)
                return Direction.Down;
            else if (Y + 1 == other.Y)
                return Direction.Right;
            else
                return Direction.Left;
        }
    }
}
