using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Model.Tools
{
    public class GameClient : TcpClient
    {
        public string GUID { get; set; }
        public string PlayerName { get; set; }

        public static GameClient GameClientFactory(string guid, string playerName)
        {
            return new GameClient(guid, playerName);
        }

        private GameClient() { }

        private GameClient(string guid, string playerName)
        {
            GUID = guid;
            PlayerName = playerName;
        }

        public void SendMessage(byte[] message)
        {
            var stream = this.GetStream();
            stream.Write(message, 0, message.Length);
        }
    }
}
