using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BluffersDice.GameEngine;
using System.Collections.Generic;
using System.Linq;

using System.Windows.Media;
using System.Windows.Controls;

namespace BluffersDice.Tests
{
    [TestClass]
    public class UnitTests
    {

        [TestMethod]
        [TestCategory("Value Comparison Tests")]
        public void PokerHandComparisonTest()
        {
            Assert.IsTrue(DiceValue.FiveOfAKind > DiceValue.FourOfAKind);
            Assert.IsTrue(DiceValue.FourOfAKind > DiceValue.LargeStraight);
            Assert.IsTrue(DiceValue.LargeStraight > DiceValue.SmallStraight);

            Assert.IsTrue(DiceValue.SmallStraight > DiceValue.FullHouse);
            Assert.IsTrue(DiceValue.FullHouse > DiceValue.ThreeOfAKind);
            Assert.IsTrue(DiceValue.ThreeOfAKind > DiceValue.TwoPair);

            Assert.IsTrue(DiceValue.TwoPair > DiceValue.Pair);
            Assert.IsTrue(DiceValue.Pair > DiceValue.Runt);

            DiceValue testval = default(DiceValue);

            Assert.IsTrue(testval == DiceValue.Runt);

        }

        [TestCategory("Algorithm")]
        [TestMethod]
        public void ConsecutiveDieDetectionWorksAsExpected()
        {
            List<DieGrouping> c_test_list = new List<DieGrouping>(){ 
                //large straight
                new DieGrouping(1,1),
                new DieGrouping(2,1),
                new DieGrouping(3,1),
                new DieGrouping(4,1),
                new DieGrouping(5,1),
                //should not return 
                new DieGrouping(7,1),
                new DieGrouping(8,1),
                new DieGrouping(9,1),
                //small straight
                new DieGrouping(3,1),
                new DieGrouping(4,1),
                new DieGrouping(5,1),
                new DieGrouping(6,1),
                //should not return 
                new DieGrouping(4,1),
            };

            var groups = new Hand(c_test_list).GetStraightGroups();

            Assert.AreEqual(2, groups.Count());
            Assert.AreEqual(5, groups[0].Count());
            Assert.AreEqual(4, groups[1].Count());

        }

        [TestMethod]
        [TestCategory("Hand Matcher")]
        public void DieGroupingToHamdmatchListTest()
        {
            List<Die> testDice = new List<Die>()
            {
                new Die(){ Value = 6, Id = 1},
                new Die(){ Value = 6, Id = 2},
                new Die(){ Value = 6, Id = 3},
                new Die(){ Value = 3, Id = 4},
                new Die(){ Value = 3, Id = 5}
            };

            var testGroups = testDice.ToDiceGroups();
            Assert.AreEqual(2, testGroups.Count);

            var h = testGroups.ToHandMatchList();
            Assert.IsTrue(h.Any());

        }

        [TestCategory("Hand Matcher")]
        [TestMethod]
        public void FiveOfaKindReturnsFiveofaKindHandMatch()
        {
            List<Die> testDice = new List<Die>()
            {
                new Die(){ Value = 6, Id = 1},
                new Die(){ Value = 6, Id = 2},
                new Die(){ Value = 6, Id = 3},
                new Die(){ Value = 6, Id = 4},
                new Die(){ Value = 6, Id = 5}
            };

            var testGroups = testDice.ToDiceGroups();
            Assert.IsTrue(testGroups.Count() == 1);

            var h = testGroups.ToHandMatchList();
            Assert.AreEqual(1, h.Count);
            Assert.AreEqual(DiceValue.FiveOfAKind, h.First().Type);

        }
        [TestCategory("Hand Matcher")]
        [TestMethod]
        public void FourOfaKindReturnsFourofaKindHandMatch()
        {
            List<Die> testDice = new List<Die>()
            {
                new Die(){ Value = 6, Id = 1},
                new Die(){ Value = 6, Id = 2},
                new Die(){ Value = 6, Id = 3},
                new Die(){ Value = 6, Id = 4},
                new Die(){ Value = 1, Id = 5}
            };

            var testGroups = testDice.ToDiceGroups();
            Assert.IsTrue(testGroups.Count() == 2);

            var h = testGroups.ToHandMatchList();
            Assert.AreEqual(1, h.Count);
            Assert.AreEqual(DiceValue.FourOfAKind, h.First().Type);

        }
        [TestCategory("Hand Matcher")]
        [TestMethod]
        public void ThreeOfaKindReturnsThreeofaKindHandMatch()
        {
            List<Die> testDice = new List<Die>()
            {
                new Die(){ Value = 6, Id = 1},
                new Die(){ Value = 6, Id = 2},
                new Die(){ Value = 6, Id = 3},
                new Die(){ Value = 2, Id = 4},
                new Die(){ Value = 1, Id = 5}
            };

            var testGroups = testDice.ToDiceGroups();
            Assert.IsTrue(testGroups.Count() == 3);

            var h = testGroups.ToHandMatchList();
            Assert.AreEqual(1, h.Count);
            Assert.AreEqual(DiceValue.ThreeOfAKind, h.First().Type);

        }
        [TestCategory("Hand Matcher")]
        [TestMethod]
        public void PairReturnsPairHandMatch()
        {
            List<Die> testDice = new List<Die>()
            {
                new Die(){ Value = 5, Id = 1},
                new Die(){ Value = 3, Id = 2},
                new Die(){ Value = 2, Id = 3},
                new Die(){ Value = 2, Id = 4},
                new Die(){ Value = 1, Id = 5}
            };

            var testGroups = testDice.ToDiceGroups();
            Assert.IsTrue(testGroups.Count() == 4);

            var h = testGroups.ToHandMatchList();
            Assert.AreEqual(1, h.Count);
            Assert.AreEqual(DiceValue.Pair, h.First().Type);

        }

        [TestMethod]
        [TestCategory("Hand Matcher")]
        public void SmallStraightReturnsSmallStraightHandMatch()
        {
            List<Die> testDice = new List<Die>()
            {
                new Die(){ Value = 1, Id = 1},
                new Die(){ Value = 2, Id = 2},
                new Die(){ Value = 3, Id = 3},
                new Die(){ Value = 4, Id = 4},
                new Die(){ Value = 6, Id = 5}
            };

            var testGroups = testDice.ToDiceGroups();
            Assert.IsTrue(testGroups.Count() == 5);

            var h = testGroups.ToHandMatchList();
            Assert.AreEqual(1, h.Count);
            Assert.AreEqual(DiceValue.SmallStraight, h.First().Type);

            testDice = new List<Die>()
            {
                new Die(){ Value = 1, Id = 1},
                new Die(){ Value = 2, Id = 2},
                new Die(){ Value = 3, Id = 3},
                new Die(){ Value = 4, Id = 4},
                new Die(){ Value = 4, Id = 5}
            };

            testGroups = testDice.ToDiceGroups();
            Assert.IsTrue(testGroups.Count() == 4);

            h = testGroups.ToHandMatchList();
            Assert.AreEqual(2, h.Count);
            Assert.AreEqual(DiceValue.SmallStraight, h.OrderByDescending(e => e.Type).First().Type);

        }

        [TestMethod]
        [TestCategory("Hand Matcher")]
        public void LargeStraightReturnsLargeStraightHandMatch()
        {
            List<Die> testDice = new List<Die>()
            {
                new Die(){ Value = 1, Id = 1},
                new Die(){ Value = 2, Id = 2},
                new Die(){ Value = 3, Id = 3},
                new Die(){ Value = 4, Id = 4},
                new Die(){ Value = 5, Id = 5}
            };

            List<Die> testDice2 = new List<Die>()
            {
                new Die(){ Value = 2, Id = 1},
                new Die(){ Value = 3, Id = 2},
                new Die(){ Value = 4, Id = 3},
                new Die(){ Value = 5, Id = 4},
                new Die(){ Value = 6, Id = 5}
            };

            var testGroups = testDice.ToDiceGroups();
            Assert.IsTrue(testGroups.Count() == 5);
            var testGroup2 = testDice2.ToDiceGroups();
            Assert.IsTrue(testGroups.Count() == 5);

            var h = testGroups.ToHandMatchList();
            Assert.AreEqual(1, h.Count);
            Assert.AreEqual(DiceValue.LargeStraight, h.First().Type);

            var h2 = testGroup2.ToHandMatchList();
            Assert.AreEqual(1, h2.Count);
            Assert.AreEqual(DiceValue.LargeStraight, h2.First().Type);

        }
        [TestCategory("Hand Matcher")]
        [TestMethod]
        public void FullHouseReturnsFullHouseHandMatch()
        {
            List<Die> testDice = new List<Die>()
            {
                new Die(){ Value = 6, Id = 1},
                new Die(){ Value = 6, Id = 2},
                new Die(){ Value = 6, Id = 3},
                new Die(){ Value = 2, Id = 4},
                new Die(){ Value = 2, Id = 5}
            };

            var testGroups = testDice.ToDiceGroups();
            Assert.IsTrue(testGroups.Count() == 2);

            var h = testGroups.ToHandMatchList();
            Assert.AreEqual(3, h.Count);

            DiceValue diceValue = h.OrderByDescending(o => o.Type).First().Type;
            Assert.AreEqual(DiceValue.FullHouse, diceValue);

        }
        [TestMethod]
        [TestCategory("Hand Matcher")]
        public void TwoPairReturns2PairHandMatch()
        {
            List<Die> testDice = new List<Die>()
            {
                new Die(){ Value = 6, Id = 1},
                new Die(){ Value = 1, Id = 2},
                new Die(){ Value = 6, Id = 3},
                new Die(){ Value = 2, Id = 4},
                new Die(){ Value = 2, Id = 5}
            };

            var testGroups = testDice.ToDiceGroups();
            Assert.IsTrue(testGroups.Count() == 3);

            var h = testGroups.ToHandMatchList().OrderByDescending(o => o.Type).ToList();
            Assert.AreEqual(2, h.Count);

            Assert.AreEqual(DiceValue.TwoPair, h.First().Type);

        }

        [TestCategory("Hand Matcher")]
        [TestMethod]
        public void NoMatchReturnsChanceHandMatch()
        {
            List<Die> testDice = new List<Die>()
            {
                new Die(){ Value = 1, Id = 1},
                new Die(){ Value = 2, Id = 2},
                new Die(){ Value = 3, Id = 3},
                new Die(){ Value = 5, Id = 4},
                new Die(){ Value = 6, Id = 5}
            };

            var testGroups = testDice.ToDiceGroups();
            Assert.IsTrue(testGroups.Count() == 5);

            var h = testGroups.ToHandMatchList();
            Assert.AreEqual(1, h.Count);

            Assert.AreEqual(DiceValue.Runt, h.OrderByDescending(o => o.Type).First().Type);

        }

        [TestMethod]
        [TestCategory("Dice Roller")]
        public void DiceRollerRollsDice()
        {
            Roll r = new Roll();
            var dice = r.RollDice();
            Assert.AreEqual(5, dice.Count());

        }

        [TestMethod]
        [TestCategory("Dice Roller")]
        public void DiceRollerRollsOnlyUnheldDice()
        {
            var r = new Roll() 
            { 
                Dice = new List<Die>() { 
                    new Die() { Value = 1, Id = 1 }, 
                    new Die() { Value = 2, Id = 2 }, 
                    new Die() { Value = 3, Id = 3 }, 
                    new Die() { Value = 4, Id = 4 }, 
                    new Die() { Value = 5, Id = 5 } 
                } 
            };

            r.Dice.ForEach(d => r.Dice.HoldToggle(d.Id));
            r.Dice.ForEach(d=> Assert.IsTrue(d.IsHeld));

            r.RollDice();

            r.Dice.ForEach(d=>Assert.AreEqual(d.Id, d.Value));

        }

        [TestMethod]
        [TestCategory("Dice Roller")]
        public void DieHoldToggleDoesToggle()
        {
            Die d = new Die();
            d.Roll();
            Assert.IsFalse(d.IsHeld);
            int d_value = d.Value;

            //turn hold on 
            d.ToggleHold();
            Assert.AreEqual(d_value, d.Value);
            Assert.IsTrue(d.IsHeld);
            d.Roll();
            Assert.AreEqual(d_value, d.Value);
            Assert.IsTrue(d.IsHeld);

        }


    }
}
