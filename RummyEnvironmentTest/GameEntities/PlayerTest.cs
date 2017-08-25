using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RummyEnvironment;
using TestHelpers;

namespace RummyEnvironmentTest
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void Player_NewPlayer_DataIsCorrect()
        {
            const int tokensTaken = 14;
            const string playerName = "godzilla";

            TokensLake lake = new TokensLake();
            lake.CreateNew();
            List<IToken> tokensToBeTransferredToPlayer = lake.Tokens.GetRange(0, tokensTaken);

            Player player = new Player(playerName, lake.GrabTokens(tokensTaken));

            Assert.AreEqual(playerName, player.Name);
            AssertHelpers.AssertTokenListsAreTheSame(tokensToBeTransferredToPlayer, player.Tokens);
        }
    }
}
