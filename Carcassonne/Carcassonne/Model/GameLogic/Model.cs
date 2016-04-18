using CarcassonneSharedModules.Network;
using CarcassonneSharedModules.Representation;
using CarcassonneSharedModules.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneServer.Model.GameLogic
{
    
    public class GameModel : IGameModel
    {
        private TurnHandler turnHandler;
        private ScoreHandler scoreHandler;
        private GameTable gameTable;
        private NetworkHandler networkHandler;
        private PayloadContentHandler payloadContentHandler;
        private IEnumerable<Player> players;

        public GameTable GameTable { get { return gameTable; } }
        public ScoreHandler ScoreHandler { get { return scoreHandler; } }
        public IEnumerable<Player> Players { get { return players; } }
        /// <summary>
        /// Property a játék aktuális állapotának lekérésére.
        /// </summary>
        public GameStateDescriptor State { get { return GameStateDescriptor.GameStateDescriptorFactory(this); } }

        public GameModel()
        {
            payloadContentHandler = new PayloadContentHandler(this);
        }

        private void SendState()
        {
            using (var stream = new MemoryStream())
            {
                State.WriteContent(stream);
                networkHandler.Send(stream.ToArray(), MessageType.Broadcast);
            }
        }

    }
}
