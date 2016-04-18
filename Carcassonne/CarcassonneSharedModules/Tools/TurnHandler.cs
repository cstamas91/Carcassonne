using CarcassonneSharedModules.Representation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneSharedModules.Tools

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


        #region Event handlers
        /// <summary>
        /// Kezeli a mezőlerakási szegmens véget érését. Ez a handler a játékmező egy eventjét figyeli.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TilePlayedHandler(object sender, TilePlayedEventArgs e) 
        {
            if (this.CurrentTurnState != TurnState.TilePlacement)
                throw new InvalidTurnStateException(CurrentTurnState.ToString());

            this.CurrentTurnState = TurnState.MeeplePlacement;
        }
        /// <summary>
        /// Kezelei a figura lerakási szegmens végét. A játékmező egy eventjét figyeli.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MeeplePlayedHandler(object sender, MeeplePlayedEventArgs e) 
        {
            if (this.CurrentTurnState != TurnState.MeeplePlacement)
                throw new InvalidTurnStateException(CurrentTurnState.ToString());

            this.CurrentTurnState = TurnState.Scoring;
            if (MeeplePlayed != null)
                MeeplePlayed(this, new MeeplePlayedEventArgs());
        }
        /// <summary>
        /// Kezeli a pontozási szegmens végét. Az átölelő model egy eventjét figyeli.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ScoringFinishedHandler(object sender, ScoringFinishedEventArgs e) 
        {
            if (this.CurrentTurnState != TurnState.Scoring)
                throw new InvalidTurnStateException(CurrentTurnState.ToString());

            this.Turns++;
            this.CurrentTurnState = TurnState.TilePlacement;
            this.CurrentPlayer = e.NextPlayer;
        }
        /// <summary>
        /// Kezeli a pontozási szegmens végét. Az átölelő model egy eventjét figyeli.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SkipScoringStateHandler(object sender, SkipScoringStateEventArgs e)
        {
            if (this.CurrentTurnState != TurnState.MeeplePlacement)
                throw new InvalidTurnStateException(CurrentTurnState.ToString());

            this.Turns++;
            this.CurrentTurnState = TurnState.TilePlacement;
            this.CurrentPlayer = e.NextPlayer;
        }

        #endregion Event handlers

        #region Events
        public event MeeplePlayedEventHandler MeeplePlayed;
        public delegate void MeeplePlayedEventHandler(object sender, MeeplePlayedEventArgs e);
        #endregion Events


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
