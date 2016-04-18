using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarcassonneSharedModules.Tools;
using System.IO;
using System.ComponentModel;

namespace CarcassonneSharedModules.Representation
{
    public class Player : IPayloadContent
    {
        #region Declaration
        private string name;

        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        private short number;

        public short Number
        {
            get { return number; }
            private set { number = value; }
        }

        private string guid;

        public string GUID
        {
            get { return guid; }
            private set { guid = value; }
        }


        private List<Meeple> ownedMeeples;

        public Player() { }
        /// <summary>
        /// Player konstruktor. Egy játékos létrejöttekor megkapja a rendelkezésére álló figurákat, hányas számú játékos, a játékos nevét.
        /// </summary>
        /// <param name="ownedMeeples">A játékos figurái.</param>
        /// <param name="playerNumber">A játékos száma</param>
        /// <param name="name">A játékos neve</param>
        public Player(short playerNumber, string name)
        {
            this.name = name;
            this.number = playerNumber;
            this.GUID = Guid.NewGuid().ToString();
            this.ownedMeeples = GenerateMeeples().ToList();
        }
        #endregion Declaration

        #region Public methods
        /// <summary>
        /// Visszaad a játékos éppen rendelkezésre álló figurái közül egyet.
        /// </summary>
        /// <returns>Figura, ami a játékos birtokában van, és nincs még használatban.</returns>
        public Meeple GetFreeMeeple()
        {
            return ownedMeeples.First(meeple => !meeple.InUse);
        }
        #endregion Public methods

        #region Private methods
        private ICollection<Meeple> GenerateMeeples()
        {
            List<Meeple> meeples = new List<Meeple>();
            for (int i = 0; i < 15; i++)
                meeples.Add(new Meeple(Number));

            return meeples;
        }
        #endregion Private methods

        #region IPayloadContent

        public void ReadContent(byte[] payloadContent)
        {
            using (var ms = new MemoryStream(payloadContent))
            {
                var arr = new byte[sizeof(short)];
                ms.Read(arr, 0, sizeof(short));
                this.Number = BitConverter.ToInt16(arr, 0);

                using (var sr = new StreamReader(ms, Encoding.Default, false, 4, true))
                {
                    Name = sr.ReadLine();
                    GUID = sr.ReadLine();
                }
            }
            if (Name.EndsWith("\n"))
                Name = Name.Trim();
        }

        public void WriteContent(Stream contentStream)
        {
            contentStream.WriteShort((short)Number);
            contentStream.WriteString(Name);
            contentStream.WriteString(GUID);
        }
        #endregion IPayloadContent

        #region public methods
        public override string ToString()
        {
            return string.Format("{0}, {1}", Number, Name);
        }
        #endregion public methods

    }
}
