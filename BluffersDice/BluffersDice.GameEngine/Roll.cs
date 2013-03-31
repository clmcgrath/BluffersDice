using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace BluffersDice.GameEngine
{
    [DebuggerDisplay("{Dice[0].Value},{Dice[1].Value},{Dice[2].Value},{Dice[3].Value},{Dice[4].Value}")]
    public class Roll 
    {
        public Roll(int diceNum = 5)
        {
            Dice = new List<Die>();
            for (var i = 1; i <= diceNum; i++)
            {
                Dice.Add(new Die() { Id = i });
            }
        }

        public List<Die> Dice { get; set; }

        public List<Die> RollDice()
        {
            for (var i = 0; i < Dice.Count; i++)
            {
                if (!Dice[i].IsHeld)
                {
                    Dice[i].Roll();
                }
            }

            return Dice;
        }

        public int DiceSum { get { return Dice.Sum(s => s.Value); } }

        public Roll Clone()
        {
            var roll = new Roll();
            if (roll.Dice.Any())
                roll.Dice.Clear();

            Dice.ForEach(d => roll.Dice.Add(d.Clone()));

            return roll;
        }

        public override string ToString()
        {
            var t = Dice.GetBestHand().Type;
            
            
            switch (t)
            {

                case DiceValue.Runt:
                    return "Runt";
                    
                case DiceValue.FiveOfAKind:
                    return "5 of a Kind";
                    
                case DiceValue.FourOfAKind:
                    return "4 of a Kind";
                   
                case DiceValue.FullHouse:
                    return "Full House";

                case DiceValue.LargeStraight:
                    return "Large Straight";
                
                case DiceValue.Pair:
                    return "Pair";
                
                case DiceValue.SmallStraight:
                    return "Small Straight";
                
                case DiceValue.ThreeOfAKind:
                    return "3 of a Kind";
                
                case DiceValue.TwoPair:
                    return "2 Pair";
                
                default:
                    return t.ToString();
            }

        }

        public HandMatch BestHand 
        { 
            get
            {
                return Dice.ToDiceGroups().ToHand().BestPossibleHand;
            } 
        }


    }

}
