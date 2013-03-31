using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BluffersDice.GameEngine;
using System.Collections.Generic;
using System.Linq;

namespace BluffersDice.Tests
{
    [TestClass]
    public class GameEngineTests
    {

        [TestMethod]
        public void PokerHandComparisonTest()
        {
            Assert.IsTrue(PokerValue.FiveOfAKind > PokerValue.FourOfAKind);
            Assert.IsTrue(PokerValue.FourOfAKind > PokerValue.LargeStraight);
            Assert.IsTrue(PokerValue.LargeStraight > PokerValue.SmallStraight);

            Assert.IsTrue(PokerValue.SmallStraight > PokerValue.FullHouse);
            Assert.IsTrue(PokerValue.FullHouse > PokerValue.ThreeOfAKind);
            Assert.IsTrue(PokerValue.ThreeOfAKind > PokerValue.TwoPair);

            Assert.IsTrue(PokerValue.TwoPair > PokerValue.Pair);
            Assert.IsTrue(PokerValue.Pair > PokerValue.Chance);

            PokerValue testval = default(PokerValue);

            Assert.IsTrue(testval == PokerValue.Chance);

        }
        [TestMethod]
        public void CompareEqualHands()
        {
            //generate 10 rolls 
            var rolls = new List<List<Die>>();
            var groupings = new List<List<DieGrouping>>();
            for (int i = 0; i < 10; i++)
            {


                List<Die> dice = new List<Die>();
                for (int ii = 0; ii < 5; ii++)
                {
                    var die = new Die();
                    die.Roll();
                    dice.Add(die);
                }

                rolls.Add(dice);
                groupings.Add(TurnValue.GetDieValueGroups(dice));
            }

            Hand hand = new Hand(groupings.First());
            var matches = hand.GetMatches();




        }

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

            var groups = Hand.GetStraightGroups(c_test_list);



            Assert.AreEqual(2, groups.Count());
            Assert.AreEqual(5, groups[0].Count());
            Assert.AreEqual(4, groups[1].Count());

        }


        [TestMethod]
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

            var h = testGroups.ToHandMatchList();

           
        }


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
            
        }


    }
}
