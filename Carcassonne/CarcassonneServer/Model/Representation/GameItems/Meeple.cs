using System;
using CarcassonneSharedModules.Tools;
using System.IO;

namespace CarcassonneServer.Model.Representation
{
    public class Meeple : Position
    {
        #region Declaration
        public Player Owner { get; set; }

        private bool inUse;

        public bool InUse
        {
            get { return inUse; }
            set { inUse = value; }
        }

        private string areaGuid;
        public string AreaGuid
        {
            get { return areaGuid;} 
            private set { this.areaGuid = value; }
        }


        /// <summary>
        /// Üres ktor a PayloadContentFactoryhez.
        /// </summary>
        public Meeple() { }

        /// <summary>
        /// Meeple ktor. Létrejötterkor megkapja, hogy melyik játékos birtokolja. Alapból egyik meeple sincs használatban.
        /// </summary>
        /// <param name="owner">Birtokló játékos</param>
        public Meeple(Player player)
        {
            this.Owner = player;
            this.InUse = false;
        }
        #endregion Declaration
    }
}
