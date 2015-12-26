using Carcassonne.Model.Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Model.GameLogic
{

    public class TilePlayedEventArgs : EventArgs
    {

    }

    public class MeeplePlayedEventArgs : EventArgs
    {

    }

    public class ScoringFinishedEventArgs : EventArgs
    {
        private Player nextPlayer;

        public Player NextPlayer
        {
            get { return nextPlayer; }
            set { nextPlayer = value; }
        }

        public ScoringFinishedEventArgs(Player nextPlayer)
        {
            this.nextPlayer = nextPlayer;
        }
    }

    public class SkipScoringStateEventArgs : EventArgs
    {
        private Player nextPlayer;

        public Player NextPlayer
        {
            get { return nextPlayer; }
            set { nextPlayer = value; }
        }
        public SkipScoringStateEventArgs(Player nextPlayer)
        {
            this.nextPlayer = nextPlayer;
        }
    }
}
