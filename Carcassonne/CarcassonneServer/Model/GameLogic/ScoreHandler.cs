using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CarcassonneServer.Model.Representation;
using CarcassonneSharedModules.Tools;
using CarcassonneServer.Model.Representation.GameItems;

namespace CarcassonneServer.Model.GameLogic
{
    public class ScoreHandler
    {
        #region Declaration
        private Dictionary<Player, int> scoreTable;

        public Dictionary<Player, int> ScoreTable
        {
            get { return scoreTable; }
            set { scoreTable = value; }
        }

        public ScoreHandler() { }

        /// <summary>
        /// Pontozáskezelő konstruktor. Megkapja a résztvevő játékosokat, és inicializálja a ponttáblát.
        /// </summary>
        /// <param name="players">Résztvevő játékosok.</param>
        public ScoreHandler(ICollection<Player> players)
        {
            this.scoreTable = new Dictionary<Player, int>();
            foreach (Player player in players)
                scoreTable.Add(player, 0);
        }

        #endregion Declaration

        #region Public methods
        /// <summary>
        /// Elvégzi egy kör végi pontszámolást a jelenlegi játékállapot (a játéktábla) és a jelenlegi játékos alapján.
        /// </summary>
        /// <param name="table">A játéktábla reprezentációja.</param>
        /// <param name="currentPlayer">A jelenlegi játékos.</param>
        public void CalculateScores(GameTable table, Player currentPlayer)
        {
            /* TODO: a jelenlegi kör letett mezője és meepleje alapján eldöntjük hogy honnan kell kiindulni a számolásnak. 
             * (Ha gráfreprezentációjú lesz a tábla) Az adott nodeból kiindulva meg kell vizsgálni, hogy létrejött-e olyan terület, ami pontozást igényel.
             */
        }

        #endregion Public methods
    }
}
