using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RummyEnvironment;
using System.Linq;
using TestHelpers;

namespace RummyEnvironmentTest
{
    [TestClass]
    public class TokensLakeTest
    {
        [TestMethod]
        public void TokensLake_NewLakeCreated_AllTokenIdsAreDifferent()
        {
            TokensLake lake = new TokensLake();
            lake.CreateNew();

            Assert.IsTrue(lake.Tokens.All(token => lake.Tokens.Count(s => s.IsEqual(token)) == 1));
        }

        [TestMethod]
        public void TokensLake_InvalidLake_Fails()
        {
            TokensLake lake = new TokensLake();
            lake.CreateNew();
            lake.Tokens[3] = lake.Tokens[14];

            Assert.IsFalse(lake.Tokens.All(token => lake.Tokens.Count(s => s.IsEqual(token)) == 1));
        }

        [TestMethod]
        public void TokensLake_ShuffleTokensList_SameTokensInDifferentOrder()
        {
            TokensLake lake = new TokensLake();
            lake.CreateNew();
            List<IToken> initialTokens = lake.Tokens.GetRange(0, lake.Tokens.Count);
            lake.ShuffleTokensList();
            List<IToken> shuffledTokens = lake.Tokens;

            AssertHelpers.AssertTokenListsContainsTheSameElements(initialTokens, shuffledTokens);
        }

        [TestMethod]
        public void TokensLake_GrabTokens_NewListIsCreatedAndRemovedFromTheOriginalOne()
        {
            const int TokensTaken = 14;

            TokensLake lake = new TokensLake();
            lake.CreateNew();
            List<IToken> initialTokens = lake.Tokens.GetRange(0, lake.Tokens.Count);

            List<IToken> newTokensList = lake.GrabTokens(TokensTaken);
            newTokensList.AddRange(lake.Tokens);

            AssertHelpers.AssertTokenListsAreTheSame(initialTokens, newTokensList);
        }
    }
}
