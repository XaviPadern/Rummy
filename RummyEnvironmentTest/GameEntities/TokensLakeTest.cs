using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RummyEnvironment;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using TestHelpers;

namespace RummyEnvironmentTest
{
    [TestClass]
    public class TokensLakeTest
    {
        [TestMethod]
        public void TokensLake_LakeInitialized_TokensAreTheOriginalOnes()
        {
            List<IToken> initialTokens = new EditableList<IToken>()
            {
                new Token(Color.Yellow, 4),
                new Token(Color.Blue, 7)
            };

            TokensLake lake = new TokensLake(initialTokens);

            AssertHelpers.AssertTokenListsAreTheSame(initialTokens, lake.Tokens);
        }

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
        public void TokensLake_ShuffleNullTokensList_NothingChanges()
        {
            TokensLake lake = new TokensLake(null);
            lake.ShuffleTokensList();

            Assert.IsNull(lake.Tokens);
        }

        [TestMethod]
        public void TokensLake_ShuffleOneTokenList_NothingChanges()
        {
            List<IToken> initialTokens = new EditableList<IToken>()
            {
                new Token(Color.Yellow, 4)
            };

            TokensLake lake = new TokensLake(initialTokens);
            lake.ShuffleTokensList();

            AssertHelpers.AssertTokenListsAreTheSame(initialTokens, lake.Tokens);
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

        [TestMethod]
        public void TokensLake_GrabToken_TokenIsretrievedAndRemovedFromTheOriginalList()
        {
            TokensLake lake = new TokensLake();
            lake.CreateNew();
            List<IToken> initialTokens = lake.Tokens.GetRange(0, lake.Tokens.Count);

            List<IToken> newTokensList = lake.GrabToken();
            newTokensList.AddRange(lake.Tokens);

            AssertHelpers.AssertTokenListsAreTheSame(initialTokens, newTokensList);
        }
    }
}
