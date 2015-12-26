using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Model.Representation
{
    public class Meeple
    {
        #region Declaration
        private Player owner;

        public Player Owner
        {
            get { return owner; }
        }

        private bool inUse;

        public bool InUse
        {
            get { return inUse; }
            set { inUse = value; }
        }

        /// <summary>
        /// Meeple ktor. Létrejötterkor megkapja, hogy melyik játékos birtokolja.
        /// </summary>
        /// <param name="owner">Birtokló játékos</param>
        public Meeple(Player owner)
        {
            this.owner = owner;
        }
        #endregion Declaration
    }
}
