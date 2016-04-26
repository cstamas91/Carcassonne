using CarcassonneSharedModules.Network;
using CarcassonneSharedModules.Tools;
using System.Collections.Generic;
using System.IO;
using CarcassonneServer.Model.Representation;

namespace CarcassonneServer.Model.GameLogic
{

    public class GameModel
    {
        private TurnHandler turnHandler;
        private ScoreHandler scoreHandler;
        private NetworkHandler networkHandler;
        private IEnumerable<Player> players;

        public ScoreHandler ScoreHandler { get { return scoreHandler; } }
        public IEnumerable<Player> Players { get { return players; } }
        /// <summary>
        /// Property a játék aktuális állapotának lekérésére.
        /// </summary>
        //public GameStateDescriptor State { get { return GameStateDescriptor.GameStateDescriptorFactory(this); } }

        public GameModel()
        {
        }                    

        private void SendState()
        {
            using (var stream = new MemoryStream())
            {
                networkHandler.Send(stream.ToArray(), MessageType.Broadcast);
            }
        }

    }
}
