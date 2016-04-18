using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneSharedModules.Network
{
    public class NewGameRequestReceivedEventArgs : EventArgs 
    {
        public string GameHandle { get; set; }
        public string GameGuid { get; set; }
        public string PlayerName { get; set; }
        public int NumberOfPlayers { get; set; }

        public NewGameRequestReceivedEventArgs(string handle, string guid, string playerName, int numberOfPlayers)
        {
            this.GameGuid = guid;
            this.GameHandle = handle;
            this.PlayerName = playerName;
            this.NumberOfPlayers = numberOfPlayers;
        }
    }
}
