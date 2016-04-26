using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CarcassonneServer.Model.Representation;

namespace CarcassonneServer.Model.GameLogic
{
    /// <summary>
    /// A játék köreit kezelő osztály. Számon tartja az eltelt körök számát, a jelenlegi játékost, és a jelenlegi kör belső állapotát.
    /// </summary>
    public class TurnHandler : INotifyPropertyChanged
    {
        #region Declaration
        private TurnState currentTurnState;

        internal TurnState CurrentTurnState
        {
            get { return currentTurnState; }
            set 
            { 
                currentTurnState = value;
                NotifyPropertyChanged();
            }
        }

        private int turns;

        public int Turns
        {
            get { return turns; }
            set { turns = value; }
        }

        private Player currentPlayer;

        public Player CurrentPlayer
        {
            get { return currentPlayer; }
            set 
            {
                currentPlayer = value;
                NotifyPropertyChanged();
            }
        }

        public TurnHandler() 
        {
            this.Turns = 0;
        }
        #endregion Declaration
                                                                     
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion INotifyPropertyChanged
    }

    #region Exceptions
    public class InvalidTurnStateException : Exception 
    {
        public InvalidTurnStateException(string message) : base(message) { }
    }
    #endregion Exceptions
    /// <summary>
    /// Egy kör belső állapotát leíró felsoroló típus.
    /// </summary>
    enum TurnState
    {
        TilePlacement,
        MeeplePlacement,
        Scoring
    }

    
}
