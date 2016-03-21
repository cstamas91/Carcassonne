﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carcassonne.Model.Tools;
using System.IO;

namespace Carcassonne.Model.Representation
{
    public class Player : IPayloadContent<Player>
    {
        #region Declaration
        private int points;

        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private short number;

        public short Number
        {
            get { return number; }
            set { number = value; }
        }

        private List<Meeple> ownedMeeples;

        //TODO: nem biztos hogy a saját figuráknak kifelé láthatónak kell lenni.
        //public List<Meeple> OwnedMeeples
        //{
        //    get { return ownedMeeples; }
        //    set { ownedMeeples = value; }
        //}

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
                meeples.Add(new Meeple(this));

            return meeples;
        }
        #endregion Private methods
        #region IPayloadContent
        public Player ReadContent()
        {
            throw new NotImplementedException();
        }

        public byte[] WriteContent()
        {
            using (var ms = new MemoryStream())
            {
                using (var sw = new StreamWriter(ms))
                {
                    sw.Write(number);

                    var content = new byte[ms.Length];
                    using (var contentStream = new MemoryStream(content))
                        ms.WriteTo(contentStream);

                    return content;
                }
            }
        }
        #endregion IPayloadContent
    }
}
