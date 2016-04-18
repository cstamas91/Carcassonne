using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarcassonneSharedModules.Network;
using CarcassonneSharedModules.Representation;

namespace CarcassonneSharedModules.Network
{
    public class GameServer
    {
        #region Declarations
        private Dictionary<string, Game> games;
        private NetworkHandler networkHandler;

        public GameServer()
        {
            games = new Dictionary<string, Game>();
            networkHandler = new NetworkHandler();

            networkHandler.NewGameRequestReceived += NetworkHandler_NewGameRequestHandler;
        }
        #endregion Declarations

        #region handlers
        public void NetworkHandler_NewGameRequestHandler(object sender, NewGameRequestReceivedEventArgs e) 
        {
            var newGame = new Game(e.NumberOfPlayers, e.GameGuid);
            newGame.AddPlayer(new Player(0, e.PlayerName));
            
            games.Add(newGame.Guid, newGame);
        }
        #endregion handlers

        #region Private methods
        #endregion Private methods
    }
}
