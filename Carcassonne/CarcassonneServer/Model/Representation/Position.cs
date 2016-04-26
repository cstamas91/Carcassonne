using System;
using CarcassonneSharedModules.Tools;
using System.IO;

namespace CarcassonneServer.Model.Representation
{
    public class Position : IPayloadContent
    {
        public short X { get; set; }
        public short Y { get; set; }


        public Position() { }
        public Position(short x, short y)
        {
            X = x;
            Y = y;
        }

        #region IPayloadContent
        public void ReadContent(byte[] payloadContent)
        {
            using (var ms = new MemoryStream(payloadContent))
            {
                var content = new byte[sizeof(short)];
                var offset = ms.Read(content, 0, sizeof(short));
                X = BitConverter.ToInt16(content, 0);
                offset += ms.Read(content, offset, sizeof(short));
                Y = BitConverter.ToInt16(content, 0);
            }
        }

        public void WriteContent(Stream contentStream)
        {
            contentStream.WriteShort(X);
            contentStream.WriteShort(Y);
        }

        #endregion IPayloadContent
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

        private bool NeighbourTo(Position other)
        {
            if (Math.Abs(this.X - other.X) == 1 || Math.Abs(this.Y - other.Y) == 1)
                return true;

            return false;
        }
    }
}
