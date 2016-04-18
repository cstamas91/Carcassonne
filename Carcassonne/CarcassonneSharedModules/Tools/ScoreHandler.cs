using CarcassonneSharedModules.Representation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneSharedModules.Tools
{
    public class ScoreHandler : IPayloadContent
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

        public event ScoringFinishedEventHandler ScoringFinished;
        public delegate void ScoringFinishedEventHandler(object sender, ScoringFinishedEventArgs e);
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

        #region IPayloadContent
        public void ReadContent(byte[] payloadContent)
        {
            using (var ms = new MemoryStream(payloadContent))
            {
                var content = new byte[sizeof(short)];
                var offset = ms.Read(content, 0, sizeof(short));

                for (short i = 0; ms.CanRead; i++)
                {
                    offset += ms.Read(content, offset, sizeof(short));
                    ScoreTable.Add(new Player(i, string.Empty), BitConverter.ToInt16(content, 0));
                }
            }
        }

        public void WriteContent(Stream contentStream)
        {

            var pairs = from items in ScoreTable
                        orderby items.Key.Number
                        select items;

            contentStream.WriteShort((short)pairs.Count());
            foreach (var pair in pairs)
                contentStream.WriteShort((short)pair.Value);
        }
        #endregion IPayloadContent
    }
}
