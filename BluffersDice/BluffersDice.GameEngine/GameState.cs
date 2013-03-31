using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace BluffersDice.GameEngine
{
    public static class GameState
    {
        public static event EventHandler RoundChanged;
        private static int callerNumber;
        public static Players Players { get; set; }
        public static ObservableCollection<Round> Rounds { get; set; }
        static GameState()
        {
            
            Players = new Players();
            
            Players.Player1.PointsChanged += PlayerPointsChanged;
            Players.Player2.PointsChanged += PlayerPointsChanged;
            Rounds = new ObservableCollection<Round>();
            StartNewRound();
            CallerNumber = 1;
            
        }

        private static void PlayerPointsChanged(object sender, EventArgs e)
        {
            Player player = (Player)sender;
            if (player.Id == 1)
            {

            }
            else if (player.Id == 2)
            { 
            
            }
        }

        public static event EventHandler ActivePlayerChanged;

        public static Player  Caller 
        { 
            get {return CallerNumber == 1 ? Players.Player1 : Players.Player2;} 
        }  
        
   
        public static Player Challenger
        { 
            get { return CallerNumber == 1 ? GameState.Players.Player2 : GameState.Players.Player1; } 
        }


        public static int CallerNumber
        {
            get
            {
                return callerNumber;
            }
            private set
            {
                callerNumber = value;
                if (ActivePlayerChanged != null)
                ActivePlayerChanged(null , new EventArgs());
            }
        }
        public static bool GameCompleted { get; set; }
        
        public static int Pot 
        { 
            get 
            { 
                return 20 - (Players.Player1.Points + Players.Player2.Points); 
            } 
        }

        public static void ChangeActivePlayer()
        {
            
            CallerNumber = CallerNumber == 1 ? 2 : 1;


        }

        public static Round CurrentRound { get; set; }
        public static int RoundNumber 
        {
            get 
            {
                return Rounds.Count;
            } 
        }
        public static void StartNewRound()
        {
            
            if (CurrentRound != null)
                Rounds.Add(CurrentRound);
            
            Caller.LastTurn = null;
            Challenger.LastTurn = null;

            CurrentRound = new Round();
            if (RoundChanged != null)
            RoundChanged(CurrentRound, null);
            
        }
    }
}
