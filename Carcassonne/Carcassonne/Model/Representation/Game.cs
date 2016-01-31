using Carcassonne.Model.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Model.Representation
{
    public class Game
    {
        #region Declarations
        private string guid;

        public string Guid
        {
            get { return guid; }
            set { guid = value; }
        }


        private int numberOfPlayers;

        public int NumberOfPlayers
        {
            get { return numberOfPlayers; }
            set { numberOfPlayers = value; }
        }

        public int CurrentPlayers
        {
            get { return players.Count; }
        }

        private List<Player> players;
        private GameTable table;
        private TurnHandler turnHandler;
        private ScoreHandler scoreHandler;

        public Game(int numberOfPlayers, string guid)
        {
            this.numberOfPlayers = numberOfPlayers;
            this.guid = guid;
            this.table = new GameTable();
            
        }
        #endregion Declarations

        #region Public methods
        public void AddPlayer(Player player)
        {
            this.players.Add(player);
        }

        public void InitializeGame()
        {
            this.turnHandler = new TurnHandler();
            this.scoreHandler = new ScoreHandler(players);
        }
        #endregion Public methods
    }
}
