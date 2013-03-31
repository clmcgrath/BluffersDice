using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BluffersDice.Tests.dll")]
namespace BluffersDice.GameEngine
{
    public class TurnValue
    {
        public TurnValue(ICollection<Die> turnDice)
        {
            DiceValue = turnDice.ToList();
        }
        public DiceValue BestPossiblePokerHand { get; set; }
        public List<Die> DiceValue { get; set; }
        public Hand Value
        {
            get
            {
                var hand = new Hand(GetDieValueGroups(DiceValue));
                return hand;
            }
        }
        public static List<DieGrouping> GetDieValueGroups(ICollection<Die> t1)
        {
            var groups = t1.GroupBy(g => g.Value);
            return groups.Select(o => new DieGrouping(o.First().Value, o.Count())).OrderBy(o => o.DieValue).ToList();
        }
        public static TurnValue GetWinningHand(TurnValue t1, TurnValue t2)
        {
            if (t1.BestPossiblePokerHand != t2.BestPossiblePokerHand)
            {
                return t1.BestPossiblePokerHand > t2.BestPossiblePokerHand ? t1 : t2;
            }

            return null;
        }
    }
}
