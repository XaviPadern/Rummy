using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RummyEnvironment;
using System.Linq;

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

            List<string> ids = this.GetTokenIds(lake.Tokens);

            Assert.IsTrue(ids.Count == ids.Distinct().Count());
        }

        [TestMethod]
        public void TokensLake_ShuffleTokensList_SameTokensInDifferentOrder()
        {
            // Pending: Use a better equality assertion (with Token.IsEqual()). Not just the IDs.
            TokensLake lake = new TokensLake();
            lake.CreateNew();
            List<string> initialIds = this.GetTokenIds(lake.Tokens);

            lake.ShuffleTokensList();
            List<string> shuffledIds = this.GetTokenIds(lake.Tokens);

            Assert.AreEqual(initialIds.Count, shuffledIds.Count);
            Assert.IsTrue(initialIds.All(shuffledIds.Contains));
            Assert.IsFalse(initialIds.SequenceEqual(shuffledIds));
        }

        [TestMethod]
        public void TokensLake_GrabTokens_NewListIsCreatedAndRemovedFromTheOriginalOne()
        {
            // Pending: Use a better equality assertion (with Token.IsEqual()). Not just the IDs.
            const int TokensTaken = 14;

            TokensLake lake = new TokensLake();
            lake.CreateNew();
            List<string> initialIds = this.GetTokenIds(lake.Tokens);

            List<string> newListIds = this.GetTokenIds(lake.GrabTokens(TokensTaken));
            List<string> croppedListIds = this.GetTokenIds(lake.Tokens);

            newListIds.AddRange(croppedListIds);

            Assert.AreEqual(initialIds.Count, newListIds.Count);
            Assert.IsTrue(initialIds.SequenceEqual(newListIds));
        }

        

        private List<string> GetTokenIds(List<Token> tokens)
        {
            List<string> ids = new List<string>();
            tokens.ForEach(token => ids.Add(token.Id.ToString()));
            return ids;
        }
    }
}
