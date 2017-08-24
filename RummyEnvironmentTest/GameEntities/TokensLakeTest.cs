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
        public void TokensLake_Shuffle_SameTokensInDifferentOrder()
        {
            TokensLake lake = new TokensLake();
            lake.CreateNew();
            List<string> initialIds = this.GetTokenIds(lake.Tokens);

            lake.ShuffleTokens();
            List<string> shuffledIds = this.GetTokenIds(lake.Tokens);

            Assert.AreEqual(initialIds.Count, shuffledIds.Count);
            Assert.IsTrue(initialIds.All(shuffledIds.Contains));
            Assert.IsFalse(initialIds.SequenceEqual(shuffledIds));
        }

        private List<string> GetTokenIds(List<Token> tokens)
        {
            List<string> ids = new List<string>();
            tokens.ForEach(token => ids.Add(token.Id.ToString()));
            return ids;
        }
    }
}
