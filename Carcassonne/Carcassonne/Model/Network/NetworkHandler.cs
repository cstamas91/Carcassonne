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
        private List<TcpClient> clients;
        private List<TcpListener> listeners;
        /// <summary>
        /// Hálózati kommunikáció kezelőjének a konstruktora.
        /// </summary>
        public NetworkHandler() 
        {
            
        }
        #endregion Declarations

        #region Events

        public event NewGameRequestReceivedHandler NewGameRequestReceived;
        public delegate void NewGameRequestReceivedHandler(object sender, NewGameRequestReceivedEventArgs e);
        public 

        #endregion Events
    }
}
