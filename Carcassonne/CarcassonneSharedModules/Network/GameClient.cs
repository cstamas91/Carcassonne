using CarcassonneSharedModules.Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneSharedModules.Network
{
    public class GameClient
    {
        private Player player;

        public Player Player
        {
            get { return player; }
            set { player = value; }
        }
        private Socket socket;

        public static GameClient GameClientFactory(Player player)
        {
            return new GameClient(player);
        }

        private GameClient() { }

        private GameClient(Player player)
        {
            this.player = player;
        }

        public void SendMessage(byte[] message)
        {
            socket.Send(message);
        }
    }
}
