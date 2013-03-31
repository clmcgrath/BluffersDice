using System;
using System.Collections.Generic;
using System.Linq;

namespace BluffersDice.GameEngine
{
    public class Hand
    {
        public Hand(List<DieGrouping> groupings)
        {
            Groupings = groupings;
        }
        public List<DieGrouping> Groupings { get; set; }


        public HandMatch BestPossibleHand
        {
            get
            {
                var matches = PossibleMatches.OrderByDescending(o => o.Type).ThenBy(o => o.Dice.Sum(s => (s.DieValue * s.Count)));

                return matches.Any() ? matches.First() : null;
            }
        }
        public List<HandMatch> PossibleMatches
        {
            get
            {
                return GetMatches();
            }
        }
        public List<HandMatch> GetMatches()
        {
            var s_st_groups = GetStraightGroups().Where(q => q.Count == 4).OrderBy(p => p.Sum(g => g.DieValue));
            var l_st_groups = GetStraightGroups().Where(q => q.Count >= 5).OrderBy(p => p.Sum(g => g.DieValue));

            var possible_matches = new List<HandMatch>();
            possible_matches.Add(new HandMatch(DiceValue.FiveOfAKind,    Groupings.Where(g => g.Count == 5).ToList()));
            possible_matches.Add(new HandMatch(DiceValue.FourOfAKind,    Groupings.Where(g => g.Count == 4).ToList()));
            possible_matches.Add(new HandMatch(DiceValue.ThreeOfAKind,   Groupings.Where(g => g.Count == 3).ToList()));
            possible_matches.Add(new HandMatch(DiceValue.Pair,           Groupings.Where(g => g.Count == 2).ToList()));
            possible_matches.Add(new HandMatch(DiceValue.TwoPair,
                    Groupings.Where(g => g.Count == 2).ToList().Count() > 1
                        ? Groupings.Where(g => g.Count == 2).ToList() :
                        new List<DieGrouping>())
            );

            possible_matches.Add(new HandMatch(DiceValue.SmallStraight,  s_st_groups.Any() ? s_st_groups.First() : new List<DieGrouping>()));
            possible_matches.Add(new HandMatch(DiceValue.LargeStraight,  l_st_groups.Any() ? l_st_groups.First() : new List<DieGrouping>()));

            var fh_chk = possible_matches.Where(q => (q.Type == DiceValue.Pair || q.Type == DiceValue.ThreeOfAKind) && q.Dice.Any());
            var fullhouse = new HandMatch(DiceValue.FullHouse, new List<DieGrouping>());
            var fh_types = fh_chk.Select(q => q.Type).Distinct();
            if (fh_types.Contains(DiceValue.ThreeOfAKind) && fh_types.Contains(DiceValue.Pair))
            {
                var pairs = fh_chk.Where(q => q.Type == DiceValue.Pair).ToList();
                var trips = fh_chk.Where(q => q.Type == DiceValue.ThreeOfAKind).ToList();
                pairs.ForEach(i => fullhouse.Dice.AddRange(i.Dice));
                trips.ForEach(i => fullhouse.Dice.AddRange(i.Dice));
            }
            possible_matches.Add(fullhouse);
            var coverageAssertion = possible_matches.Select(o => o.Type).ToList();
            if (coverageAssertion.Count != 8)
            {
                throw new InvalidOperationException("Hand Coverage Assertion Failed");
            }
            var actual_matches = possible_matches.Where(m => m.Dice.Any()).ToList();
            if (!actual_matches.Any())
            {
                actual_matches.Add(new HandMatch(DiceValue.Runt, Groupings));
            }
            return actual_matches;
        }

        public List<List<DieGrouping>> GetStraightGroups()
        {
            var dice = new List<DieGrouping>(Groupings.Select(obj => obj.Clone() as DieGrouping));
            var c_groups = new List<List<DieGrouping>>();
            var group = new List<DieGrouping>();

            while (dice.Count() > 0)
            {
                var d = dice.First();

                if (group.Count() == 0)
                {
                    group.Add(d);
                    dice.Remove(d);
                    if (dice.Count() == 0)
                    {
                        continue;
                    }

                    d = dice.First();
                }
                var n = group.Last();

                if (d.DieValue == n.DieValue + 1)
                {
                    group.Add(d);
                    dice.Remove(d);
                }
                else
                {
                    if (group.Count() > 3)
                    {
                        c_groups.Add(new List<DieGrouping>(group));
                    }
                    group.Clear();
                }
            }

            if (group.Count() > 3)
            {
                c_groups.Add(group);
            }

            return c_groups;
        }
    }
}
