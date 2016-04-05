using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Carcassonne.Model.Tools;

namespace Carcassonne.Model.Network
{
    /// <summary>
    /// Hálózati kommunikációért felelős osztály.
    /// </summary>
    public class NetworkHandler
    {

        #region Declarations
        private const int BASE_CONNECTION_PORT = 1500;
        private List<GameClient> clients;
        private List<TcpListener> listeners;
        private UdpClient connectionListener;
        /// <summary>
        /// Hálózati kommunikáció kezelőjének a konstruktora.
        /// </summary>
        public NetworkHandler() 
        {
            connectionListener = new UdpClient(BASE_CONNECTION_PORT);
        }
        #endregion Declarations

        #region Events

        public event NewGameRequestReceivedHandler NewGameRequestReceived;
        public delegate void NewGameRequestReceivedHandler(object sender, NewGameRequestReceivedEventArgs e);

        #endregion Events

        #region Methods
        public void Send(byte[] message, MessageType type, string recipient = null)
        {
            switch (type)
            {
                case MessageType.Broadcast:
                    foreach (var client in clients)
                        client.SendMessage(message);
                    break;

                case MessageType.Single:
                    clients
                        .FirstOrDefault(client => client.PlayerName == recipient)
                        .SendMessage(message);
                    break;
            }
        }

        #endregion Methods
    }

    public enum MessageType : short
    {
        Single = 0,
        Broadcast = 1
    }
}
