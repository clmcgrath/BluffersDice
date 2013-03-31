using System;
using System.Collections.Generic;
using System.Linq;


namespace BluffersDice.GameEngine
{
    public static class DieGroupingExtensions
    {
        public static HandMatch GetBestHand(this ICollection<Die> dice)
        {
            return dice.ToDiceGroups().ToHand().BestPossibleHand;
        }
        public static List<DieGrouping> ToDiceGroups(this ICollection<Die> dice)
        {
            return TurnValue.GetDieValueGroups(dice);
        }
        public static Hand ToHand(this ICollection<DieGrouping> dice)
        {
            return new Hand(dice.ToList());
        }
        public static List<HandMatch> ToHandMatchList(this ICollection<DieGrouping> groupings)
        {
            var h = groupings.ToHand();
            h.GetMatches();
            return h.GetMatches();
        }
    }
}
