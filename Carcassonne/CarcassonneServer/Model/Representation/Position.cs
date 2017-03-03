using System;
using System.Collections.Generic;

namespace CarcassonneServer.Model.Representation
{
    public class Position
    {
        private const int MAX_X = short.MaxValue;
        private const int MAX_Y = short.MaxValue - 1;
        public bool IsBounded
        {
            get
            {
                return X == 0 || X == MAX_X || Y == 0 || Y == MAX_Y;
            }
        }
        /// <summary>
        /// Vertikális tengelyen való elmozdulást mutatja.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Horizontális tengelyen való elmozdulást mutatja.
        /// </summary>
        public int Y { get; set; }


        public Position() { }
        /// <summary>
        /// Pozíció konstruktora.
        /// </summary>
        /// <param name="x">Vertikálist tengely mentén való elmozdulás.</param>
        /// <param name="y">Horizontális tengely mentén való elmozdulás.</param>
        public Position(int x, int y)
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
            return X * MAX_X + Y;
        }

        public Direction GetDirection(Position other)
        {
            if(!(this | other))
                throw new InvalidOperationException();

            if (X + 1 == other.X)
                return Representation.Direction.Up;
            else if (X - 1 == other.X)
                return Representation.Direction.Down;
            else if (Y + 1 == other.Y)
                return Representation.Direction.Right;
            else
                return Representation.Direction.Left;
        }

        public Position GetPosition(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                case Direction.DownRight:
                case Direction.DownLeft:
                    if (X - 1 < 0)
                        throw new OutOfBoundsException(string.Format("{0} -> {1}", X, direction.ToString()), this, direction);
                    return new Position((short)(X - 1), Y);
                case Direction.Left:
                case Direction.LeftDown:
                case Direction.LeftUp:
                    if (Y - 1 < 0)
                        throw new OutOfBoundsException(string.Format("{0} -> {1}", Y, direction.ToString()), this, direction);
                    return new Position(X, (short)(Y - 1));
                case Direction.Up:
                case Direction.UpLeft:
                case Direction.UpRight:
                    if (X == short.MaxValue)
                        throw new OutOfBoundsException(string.Format("{0} -> {1}", X, direction.ToString()), this, direction);
                    return new Position((short)(X + 1), Y);
                case Direction.Right:
                case Direction.RightDown:
                case Direction.RightUp:
                    if (Y == short.MaxValue - 1)
                        throw new OutOfBoundsException(string.Format("{0} -> {1}", Y, direction.ToString()), this, direction);
                    return new Position(X, (short)(Y + 1));
                default:
                    throw new ArgumentException(string.Format("Nincs ilyen irány: {0}", direction));
            }
        }

        public IEnumerable<Position> GetAdjacentPositions()
        {
            foreach (Direction direction in new List<Direction>() { Direction.Up, Direction.Down, Direction.Left, Direction.Right })
                yield return GetPosition(direction);
        }

        public override string ToString()
        {
            return string.Format("X: {0}, Y: {1}", X, Y);
        }
    }
}
