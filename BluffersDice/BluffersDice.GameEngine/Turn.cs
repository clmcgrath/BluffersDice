using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace BluffersDice.GameEngine
{
    public class Turn
    {
        public Turn()
        {
            Id = ++TurnIdTracker;
            Roll = new Roll();
            Rolls = new ObservableCollection<Roll>();
            Challenger = GameState.Challenger;
            CurrentPlayer = GameState.Caller;
        }

        private static int TurnIdTracker { get; set; }

        public Roll Actual { get; set; }
        public Player Challenger { get; set; }
        public Player CurrentPlayer { get; set; }
        public HandMatch ClaimedHandValue
        {
            get
            {
                if (Claim != null)
                    return Claim.Dice.ToDiceGroups().ToHand().BestPossibleHand;
                return null;
            }
        }
        public HandMatch ActualHandValue 
        {
            get
            {
                if (Actual != null)
                    return Actual.Dice.ToDiceGroups().ToHand().BestPossibleHand;
                return null;
            }
        }
        public int Id { get; set; }
        public Roll Roll { get; set; }
        public ObservableCollection<Roll> Rolls { get; set; }

        public Turn Clone()
        {
            var rolls = new ObservableCollection<Roll>();
            rolls.ToList().ForEach(i => new Roll() { Dice = i.Dice.Select(d => d.Clone()).ToList()
            });
            return new Turn()
            {
                 Id = Id,
                 Rolls = new ObservableCollection<Roll>(rolls)
            };
        }
        public void RollDice()
        {
            Roll.RollDice();
        }

        public Roll Claim { get; set; }
    }
}
