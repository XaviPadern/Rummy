using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RummyEnvironment;
using System.Drawing;
using Moq;
using TestHelpers;

namespace RummyEnvironmentTest
{
    [TestClass]
    public class RummyLadderTest
    {
        private List<IToken> dummyTokens;
        
        [TestMethod]
        public void RummyLadder_NewStructureWithValidTokenStructure_Created()
        {
            this.InitializeDummyTokensAsValidLadder();

            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            Guid outId;

            Assert.IsTrue(Guid.TryParse(ladder.Id.ToString(), out outId));
            AssertHelpers.AssertTokenListsAreTheSame(this.dummyTokens, ladder.Tokens);
        }

        [TestMethod]
        public void RummyLadder_NewStructureWithOnlySingleToken_ExceptionThrown()
        {
            this.InitializeDummyTokensAsSingleToken();

            AssertHelpers.AssertThrows<RummyException>(() =>
                new RummyLadder(this.dummyTokens));
        }

        [TestMethod]
        public void RummyLadder_NewStructureWithBrokenLadder_ExceptionThrown()
        {
            this.InitializeDummyTokensAsBrokenLadder();

            AssertHelpers.AssertThrows<RummyException>(() =>
                new RummyLadder(this.dummyTokens));
        }

        [TestMethod]
        public void RummyLadder_NewStructureWithInvalidColors_ExceptionThrown()
        {
            this.InitializeDummyTokensAsInvalidColorLadder();

            AssertHelpers.AssertThrows<RummyException>(() =>
                new RummyLadder(this.dummyTokens));
        }

        [TestMethod]
        public void RummyLadder_ModifyWithValidTokenStructure_Created()
        {
            this.InitializeDummyTokensAsValidLadder();

            RummyLadder ladder = new RummyLadder(this.dummyTokens);
            ladder.Modify(this.dummyTokens);

            Guid outId;

            Assert.IsTrue(Guid.TryParse(ladder.Id.ToString(), out outId));
            AssertHelpers.AssertTokenListsAreTheSame(this.dummyTokens, ladder.Tokens);
        }

        [TestMethod]
        public void RummyLadder_ModifyWithOnlySingleToken_ExceptionThrown()
        {
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            this.InitializeDummyTokensAsSingleToken();

            AssertHelpers.AssertThrows<RummyException>(() =>
                ladder.Modify(this.dummyTokens));
        }

        [TestMethod]
        public void RummyLadder_ModifyWithBrokenLadder_ExceptionThrown()
        {
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            this.InitializeDummyTokensAsBrokenLadder();

            AssertHelpers.AssertThrows<RummyException>(() =>
                ladder.Modify(this.dummyTokens));
        }

        [TestMethod]
        public void RummyLadder_ModifyWithInvalidColors_ExceptionThrown()
        {
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            this.InitializeDummyTokensAsInvalidColorLadder();

            AssertHelpers.AssertThrows<RummyException>(() =>
                ladder.Modify(this.dummyTokens));
        }

        private void InitializeDummyTokensAsValidLadder()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 4),
                new Token(Color.Blue, 5),
                new Token(Color.Blue, 6),
                new Token(Color.Blue, 7)
            };
        }

        private void InitializeDummyTokensAsSingleToken()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 4)
            };
        }

        private void InitializeDummyTokensAsBrokenLadder()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 4),
                new Token(Color.Blue, 5),
                new Token(Color.Blue, 7),
                new Token(Color.Blue, 8)
            };
        }

        private void InitializeDummyTokensAsInvalidColorLadder()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 4),
                new Token(Color.Blue, 5),
                new Token(Color.Yellow, 6),
                new Token(Color.Blue, 7)
            };
        }
    }
}
