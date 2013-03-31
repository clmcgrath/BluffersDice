using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BluffersDice.GameEngine.Tests
{
    [TestClass]
    public class GameStateTests
    {

        [TestMethod]
        [TestCategory("Game State")]
        public void GameStateInitialization()
        {
            Assert.IsNotNull(GameState.Caller);
            Assert.IsNotNull(GameState.Players);
            Assert.IsNotNull(GameState.Players.Player1);
            Assert.IsNotNull(GameState.Players.Player2);
            Assert.IsTrue(GameState.Players.Player1.Id == 1);
            Assert.IsTrue(GameState.Players.Player2.Id == 2);
            


        }
    }
}
