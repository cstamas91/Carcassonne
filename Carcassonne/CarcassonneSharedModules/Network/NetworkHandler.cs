using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace CarcassonneSharedModules.Network
{
    /// <summary>
    /// Hálózati kommunikációért felelős osztály.
    /// </summary>
    public class NetworkHandler : IDisposable
    {

        #region Declarations
        private const int BASE_CONNECTION_PORT = 1500;
        //private List<GameClient> clients;
        //private List<TcpListener> listeners;
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
        /// <summary>
        /// A kapott byte[] típusú üzenetet elküldi a címzetteknek.
        /// </summary>
        /// <param name="message">Küldendő üzenet</param>
        /// <param name="type">A küldés típusa, broadcast vagy single.</param>
        /// <param name="recipientGUID">Ha a küldés nem broadcast, akkor a címzett GUIDja.</param>
        public void Send(byte[] message, MessageType type, string recipientGUID = null)
        {
            switch (type)
            {
                case MessageType.Broadcast:
                    SendBroadcast(message);
                    break;
                case MessageType.Single:
                    SendSingle(message, recipientGUID);
                    break;
            }
        }

        private void SendSingle(byte[] message, string recipientGUID)
        {
        }

        private void SendBroadcast(byte[] message)
        {
        }

        public void Dispose()
        {
            connectionListener.Close();
        }

        #endregion Methods
    }

    public enum MessageType : short
    {
        Single = 0,
        Broadcast = 1
    }
}
