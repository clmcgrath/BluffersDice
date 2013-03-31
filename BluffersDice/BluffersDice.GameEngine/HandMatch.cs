using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace BluffersDice.GameEngine
{
    [DebuggerDisplay("{Type} DiceGroups: {Dice.Count}")]
    public class HandMatch
    {
        public HandMatch(DiceValue type, List<DieGrouping> dice)
        {
            Type = type;
            Dice = dice;
        }

        public List<DieGrouping> Dice { get; set; }
        public DiceValue Type { get; set; }
    }
}
