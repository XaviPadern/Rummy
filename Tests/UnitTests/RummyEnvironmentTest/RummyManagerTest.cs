using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RummyEnvironment;
using System.Drawing;
using System.Linq;
using Moq;
using TestHelpers;

namespace RummyEnvironmentTest
{
    [TestClass]
    public class RummyManagerTest
    {
        [TestMethod]
        public void RummyManager_ConstructorStart_LakeBoardAndPlayersCreated()
        {
            RummyManager manager = new RummyManager();

            Assert.IsNotNull(manager.board);
            Assert.IsNull(manager.board.Structures);

            Assert.IsNotNull(manager.players);
            Assert.IsTrue(manager.players.All(player => player.Tokens.Count == 14));
            manager.players.ForEach(player => AssertHelpers.AssertTokensInListAreValid(player.Tokens));
            
            int remainingTokens = 104 - (manager.players.Count * 14);

            Assert.IsNotNull(manager.tokensLake);
            Assert.AreEqual(remainingTokens, manager.tokensLake.Tokens.Count);
            AssertHelpers.AssertTokensInListAreValid(manager.tokensLake.Tokens);
        }
    }
}
