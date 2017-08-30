using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RummyEnvironment;
using System.Drawing;
using System.Linq;
using TestHelpers;

namespace RummyEnvironmentTest
{
    [TestClass]
    public class RummyLadderTest
    {
        private List<IToken> dummyTokens;

        #region Constructor

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
        public void RummyLadder_NewStructureWithOnlyTwoTokens_ExceptionThrown()
        {
            this.InitializeDummyTokensAsOnlyTwoTokens();

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

        #endregion

        #region Modify

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
        public void RummyLadder_ModifyWithOnlyTwoTokens_ExceptionThrown()
        {
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            this.InitializeDummyTokensAsOnlyTwoTokens();

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

        #endregion

        #region CanAddInStructure_OnlyOneToken

        [TestMethod]
        public void RummyLadder_CanAddInStructureOnlyOneTokenNotTheSameColor_NotPossibleNoExtraTokens()
        {
            IAddingResult desiredResult = new AddingResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                new Token(Color.Black, 3), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureOnlyOneTokenInInitialExtreme_IsPossibleNoExtraTokens()
        {
            IAddingResult desiredResult = new AddingResult(true, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                new Token(Color.Blue, 3), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureOnlyOneTokenInFinalExtreme_IsPossibleNoExtraTokens()
        {
            IAddingResult desiredResult = new AddingResult(true, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                new Token(Color.Blue, 9), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureOnlyOneTokenInInitialExtremeNeedsExtraTokens_IsPossibleWithExtraTokens()
        {
            List<IToken> desiredExtraTokens = new List<IToken>
            {
                new Token(Color.Blue, 2),
                new Token(Color.Blue, 3)
            };
            IAddingResult desiredResult = new AddingResult(true, desiredExtraTokens);

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                new Token(Color.Blue, 1), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureOnlyOneTokenInFinalExtremeNeedsExtraTokens_IsPossibleWithExtraTokens()
        {
            List<IToken> desiredExtraTokens = new List<IToken>
            {
                new Token(Color.Blue, 9),
                new Token(Color.Blue, 10)
            };
            IAddingResult desiredResult = new AddingResult(true, desiredExtraTokens);

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                new Token(Color.Blue, 11), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureOnlyOneTokenInTheMiddleWrongPosition1_NotPossibleNoExtraTokens()
        {
            IAddingResult desiredResult = new AddingResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                new Token(Color.Blue, 4), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureOnlyOneTokenInTheMiddleWrongPosition2_NotPossibleNoExtraTokens()
        {
            IAddingResult desiredResult = new AddingResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                new Token(Color.Blue, 5), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureOnlyOneTokenInTheMiddleWrongPosition3_NotPossibleNoExtraTokens()
        {
            IAddingResult desiredResult = new AddingResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                new Token(Color.Blue, 7), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureOnlyOneTokenInTheMiddleWrongPosition4_NotPossibleNoExtraTokens()
        {
            IAddingResult desiredResult = new AddingResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                new Token(Color.Blue, 8), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureOnlyOneTokenInTheMiddleRightPosition_PossibleWithSplittingNoExtraTokens()
        {
            IAddingResult desiredResult = new AddingResult(true, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                new Token(Color.Blue, 6), desiredResult);
        }

        #endregion
        
        #region CanAddInStructure_TokenList

        [TestMethod]
        public void RummyLadder_CanAddInStructureTokenListInvalidSize1_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            List<IToken> tokensToInsert = new List<IToken>();

            AssertHelpers.AssertThrows<RummyException>(() =>
                ladder.CanAddInStructure(tokensToInsert));
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureTokenListInvalidSize2_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            List<IToken> tokensToInsert = new List<IToken>
                {
                    new Token(Color.Blue, 1), new Token(Color.Blue, 2), new Token(Color.Blue, 3),
                    new Token(Color.Blue, 4), new Token(Color.Blue, 5), new Token(Color.Blue, 6),
                    new Token(Color.Blue, 7), new Token(Color.Blue, 8), new Token(Color.Blue, 9)
                };

            AssertHelpers.AssertThrows<RummyException>(() =>
                ladder.CanAddInStructure(tokensToInsert));
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureTokenListInvalidColors_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 1),
                new Token(Color.Blue, 2),
                new Token(Color.Yellow, 3)
            };

            AssertHelpers.AssertThrows<RummyException>(() =>
                ladder.CanAddInStructure(tokensToInsert));
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureTokenListNotCorrelative_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 9),
                new Token(Color.Blue, 10),
                new Token(Color.Blue, 12)
            };

            AssertHelpers.AssertThrows<RummyException>(() =>
                ladder.CanAddInStructure(tokensToInsert));
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureTokenListNotTheSameColor_NotPossibleNoExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Black, 1),
                new Token(Color.Black, 2),
                new Token(Color.Black, 3)
            };

            IAddingResult desiredResult = new AddingResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureTokenListInInitialExtremeBadSequence_NotPossibleNoExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 2),
                new Token(Color.Blue, 3),
                new Token(Color.Blue, 4)
            };

            IAddingResult desiredResult = new AddingResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureTokenListInFinalExtremeBadSequence_NotPossibleNoExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 7),
                new Token(Color.Blue, 8),
                new Token(Color.Blue, 9)
            };

            IAddingResult desiredResult = new AddingResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureTokenListInInitialExtremeValidWithoutExtraTokens_PossibleNoExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 1),
                new Token(Color.Blue, 2),
                new Token(Color.Blue, 3)
            };

            IAddingResult desiredResult = new AddingResult(true, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureTokenListInFinalExtremeValidWithoutExtraTokens_PossibleNoExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 9),
                new Token(Color.Blue, 10),
                new Token(Color.Blue, 11)
            };

            IAddingResult desiredResult = new AddingResult(true, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureTokenListInInitialExtremeValidWithExtraTokensNeeded_PossibleWithExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 1),
                new Token(Color.Blue, 2),
            };
            List<IToken> desiredExtraTokens = new List<IToken>
            {
                new Token(Color.Blue, 3)
            };

            IAddingResult desiredResult = new AddingResult(true, desiredExtraTokens);

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureTokenListInFinalExtremeValidWithExtraTokensNeeded_PossibleWithExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 10),
                new Token(Color.Blue, 11),
                new Token(Color.Blue, 12)
            };
            List<IToken> desiredExtraTokens = new List<IToken>
            {
                new Token(Color.Blue, 9)
            };

            IAddingResult desiredResult = new AddingResult(true, desiredExtraTokens);

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureTokenListInTheMiddleExceedsMaximumSize_NotPossibleNoExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 5),
                new Token(Color.Blue, 6),
                new Token(Color.Blue, 7)
            };

            IAddingResult desiredResult = new AddingResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureTokenListInTheMiddleInvalidPosition1_NotPossibleNoExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 4),
                new Token(Color.Blue, 5)
            };

            IAddingResult desiredResult = new AddingResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureTokenListInTheMiddleInvalidPosition2_NotPossibleNoExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 7),
                new Token(Color.Blue, 8)
            };

            IAddingResult desiredResult = new AddingResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureTokenListInTheMiddleValidPosition1_PossibleNoExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 5),
                new Token(Color.Blue, 6)
            };

            IAddingResult desiredResult = new AddingResult(true, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddInStructureTokenListInTheMiddleValidPosition2_PossibleNoExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 6),
                new Token(Color.Blue, 7)
            };

            IAddingResult desiredResult = new AddingResult(true, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensCanBeAddedInStructure(
                tokensToInsert, desiredResult);
        }

        #endregion

        #region AddInStructure_OnlyOneToken

        [TestMethod]
        public void RummyLadder_AddInStructureOnlyOneTokenNotPossible_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            List<IToken> tokensToInsert = new List<IToken>()
            {
                new Token(Color.Black, 3)
            };

            AssertHelpers.AssertThrows<RummyException>(() =>
                ladder.AddInStructure(tokensToInsert));
        }

        [TestMethod]
        public void RummyLadder_AddInStructureOnlyOneTokenInvalidExtraTokens_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            List<IToken> tokensToInsert = new List<IToken>()
            {
                new Token(Color.Blue, 2)
            };

            List<IToken> extraTokens = new List<IToken>()
            {
                new Token(Color.Black, 3)
            };

            AssertHelpers.AssertThrows<RummyException>(() =>
                ladder.AddInStructure(tokensToInsert, extraTokens));
        }

        [TestMethod]
        public void RummyLadder_AddInStructureOnlyOneTokenInInitialExtremeWithoutExtraTokens_Succeeds()
        {
            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensAreAddedInStructure(new Token(Color.Blue, 3), false);
        }

        [TestMethod]
        public void RummyLadder_AddInStructureOnlyOneTokenInFinalExtremeWithoutExtraTokens_Succeeds()
        {
            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensAreAddedInStructure(new Token(Color.Blue, 9), false);
        }

        [TestMethod]
        public void RummyLadder_AddInStructureOnlyOneTokenInInitialExtremeWithExtraTokens_Succeeds()
        {
            List<IToken> extraTokens = new List<IToken>()
            {
                new Token(Color.Blue, 2),
                new Token(Color.Blue, 3)
            };

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensAreAddedInStructure(new Token(Color.Blue, 1), extraTokens, false);
        }

        [TestMethod]
        public void RummyLadder_AddInStructureOnlyOneTokenInFinalExtremeWithExtraTokens_Succeeds()
        {
            List<IToken> extraTokens = new List<IToken>()
            {
                new Token(Color.Blue, 9),
                new Token(Color.Blue, 10)
            };

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensAreAddedInStructure(new Token(Color.Blue, 11), extraTokens, false);
        }

        [TestMethod]
        public void RummyLadder_AddInStructureOnlyOneTokenInTheMiddle_Succeeds()
        {
            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensAreAddedInStructure(new Token(Color.Blue, 6), true);
        }

        #endregion

        #region AddInStructure_TokenList

        [TestMethod]
        public void RummyLadder_AddInStructureTokenListNotPossible_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            List<IToken> tokensToInsert = new List<IToken>()
            {
                new Token(Color.Blue, 2),
                new Token(Color.Black, 3)
            };

            AssertHelpers.AssertThrows<RummyException>(() =>
                ladder.AddInStructure(tokensToInsert));
        }

        [TestMethod]
        public void RummyLadder_AddInStructureTokenListInvalidExtraTokens_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            List<IToken> tokensToInsert = new List<IToken>()
            {
                new Token(Color.Blue, 1),
                new Token(Color.Blue, 2)
            };

            List<IToken> extraTokens = new List<IToken>()
            {
                new Token(Color.Black, 3)
            };

            AssertHelpers.AssertThrows<RummyException>(() =>
                ladder.AddInStructure(tokensToInsert, extraTokens));
        }

        [TestMethod]
        public void RummyLadder_AddInStructureTokenListInInitialExtremeWithoutExtraTokens_Succeeds()
        {
            List<IToken> tokensToInsert = new List<IToken>()
            {
                new Token(Color.Blue, 2),
                new Token(Color.Blue, 3)
            };

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensAreAddedInStructure(tokensToInsert, false);
        }

        [TestMethod]
        public void RummyLadder_AddInStructureTokenListInFinalExtremeWithoutExtraTokens_Succeeds()
        {
            List<IToken> tokensToInsert = new List<IToken>()
            {
                new Token(Color.Blue, 9),
                new Token(Color.Blue, 10)
            };

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensAreAddedInStructure(tokensToInsert, false);
        }

        [TestMethod]
        public void RummyLadder_AddInStructureTokenListInInitialExtremeWithExtraTokens_Succeeds()
        {
            List<IToken> tokensToInsert = new List<IToken>()
            {
                new Token(Color.Blue, 1),
                new Token(Color.Blue, 2)
            };

            List<IToken> extraTokens = new List<IToken>()
            {
                new Token(Color.Blue, 3)
            };

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensAreAddedInStructure(tokensToInsert, extraTokens, false);
        }

        [TestMethod]
        public void RummyLadder_AddInStructureTokenListInFinalExtremeWithExtraTokens_Succeeds()
        {
            List<IToken> tokensToInsert = new List<IToken>()
            {
                new Token(Color.Blue, 11),
                new Token(Color.Blue, 12)
            };

            List<IToken> extraTokens = new List<IToken>()
            {
                new Token(Color.Blue, 9),
                new Token(Color.Blue, 10)
            };

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensAreAddedInStructure(tokensToInsert, extraTokens, false);
        }

        [TestMethod]
        public void RummyLadder_AddInStructureTokenListInTheMiddle_Succeeds()
        {
            List<IToken> tokensToInsert = new List<IToken>()
            {
                new Token(Color.Blue, 5),
                new Token(Color.Blue, 6)
            };

            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.AssertTokensAreAddedInStructure(tokensToInsert, true);
        }

        #endregion

        private void AssertTokensCanBeAddedInStructure(object tokenToInsert, IAddingResult desiredResult)
        {
            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            List<IToken> tokensToInsert;

            if (tokenToInsert.GetType() == typeof(Token))
            {
                tokensToInsert = new List<IToken> { (IToken)tokenToInsert };
            }
            else
            {
                tokensToInsert = (List<IToken>)tokenToInsert;
            }

            IAddingResult result = ladder.CanAddInStructure(tokensToInsert);

            Assert.AreEqual(desiredResult.AdditionIsPossible, result.AdditionIsPossible);
            AssertHelpers.AssertTokenListsContainsEquivalentElements(desiredResult.NeededExtraTokens, result.NeededExtraTokens);
        }

        private void AssertTokensAreAddedInStructure(object tokenToInsert, bool creationOperationIsExpected)
        {
            this.AssertTokensAreAddedInStructure(tokenToInsert, new List<IToken>(), creationOperationIsExpected);
        }

        private void AssertTokensAreAddedInStructure(object tokenToInsert, List<IToken> extraTokens, bool creationOperationIsExpected)
        {
            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            List<IToken> tokensToInsert;

            if (tokenToInsert.GetType() == typeof(Token))
            {
                tokensToInsert = new List<IToken> {(IToken)tokenToInsert};
            }
            else
            {
                tokensToInsert = (List<IToken>)tokenToInsert;
            }

            List<IOperationResult> operationResults = ladder.AddInStructure(tokensToInsert, extraTokens);
            Assert.IsNotNull(operationResults);
            
            List<IToken> allTokensFromOperations = new List<IToken>();
            
            Assert.AreEqual(StructureChanges.Modified, operationResults.First().StructureChanges);
            Assert.AreEqual(ladder.Id, operationResults.First().StructureId);
            allTokensFromOperations.AddRange(operationResults.First().Tokens);

            if (creationOperationIsExpected)
            {
                Assert.AreEqual(StructureChanges.Created, operationResults.Last().StructureChanges);
                Assert.AreNotEqual(ladder.Id, operationResults.Last().StructureId);
                allTokensFromOperations.AddRange(operationResults.Last().Tokens);
            }
            
            // All the initial tokens have to be present in the operations:
            Assert.IsTrue(ladder.Tokens.All(s => allTokensFromOperations.Count(t => t.IsEqual(s)) == 1));

            // All the tokens to insert have to be present in the operations:
            Assert.IsTrue(tokensToInsert.All(s => allTokensFromOperations.Count(t => t.IsEqual(s)) == 1));

            // All the extra tokens have to be present in the operations:
            Assert.IsTrue(extraTokens.All(s => allTokensFromOperations.Count(t => t.IsEqual(s)) == 1));
        }

        private void InitializeDummyTokensAsValidLadder()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 4),
                new Token(Color.Blue, 5),
                new Token(Color.Blue, 6),
                new Token(Color.Blue, 7),
                new Token(Color.Blue, 8)
            };
        }
        
        private void InitializeDummyTokensAsOnlyTwoTokens()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 4),
                new Token(Color.Blue, 5)
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
