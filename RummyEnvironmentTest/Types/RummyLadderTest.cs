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
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
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
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
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

        #region CanAdd_OnlyOneToken

        [TestMethod]
        public void RummyLadder_CanAddOnlyOneTokenNotTheSameColor_NotPossibleNoExtraTokens()
        {
            IActionResult desiredResult = new ActionResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(new Token(Color.Black, 3), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddOnlyOneTokenInInitialExtreme_IsPossibleNoExtraTokens()
        {
            IActionResult desiredResult = new ActionResult(true, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(new Token(Color.Blue, 3), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddOnlyOneTokenInFinalExtreme_IsPossibleNoExtraTokens()
        {
            IActionResult desiredResult = new ActionResult(true, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(new Token(Color.Blue, 10), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddOnlyOneTokenInInitialExtremeNeedsExtraTokens_IsPossibleWithExtraTokens()
        {
            List<IToken> desiredExtraTokens = new List<IToken>
            {
                new Token(Color.Blue, 2),
                new Token(Color.Blue, 3)
            };
            IActionResult desiredResult = new ActionResult(true, desiredExtraTokens);

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(new Token(Color.Blue, 1), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddOnlyOneTokenInFinalExtremeNeedsExtraTokens_IsPossibleWithExtraTokens()
        {
            List<IToken> desiredExtraTokens = new List<IToken>
            {
                new Token(Color.Blue, 10),
                new Token(Color.Blue, 11)
            };
            IActionResult desiredResult = new ActionResult(true, desiredExtraTokens);

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(new Token(Color.Blue, 12), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddOnlyOneTokenInTheMiddleWrongPosition1_NotPossibleNoExtraTokens()
        {
            IActionResult desiredResult = new ActionResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(new Token(Color.Blue, 4), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddOnlyOneTokenInTheMiddleWrongPosition2_NotPossibleNoExtraTokens()
        {
            IActionResult desiredResult = new ActionResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(new Token(Color.Blue, 5), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddOnlyOneTokenInTheMiddleWrongPosition3_NotPossibleNoExtraTokens()
        {
            IActionResult desiredResult = new ActionResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(new Token(Color.Blue, 8), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddOnlyOneTokenInTheMiddleWrongPosition4_NotPossibleNoExtraTokens()
        {
            IActionResult desiredResult = new ActionResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(new Token(Color.Blue, 9), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddOnlyOneTokenInTheMiddleRightPosition_PossibleWithSplittingNoExtraTokens()
        {
            IActionResult desiredResult = new ActionResult(true, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(new Token(Color.Blue, 6), desiredResult);
        }

        #endregion
        
        #region CanAdd_TokenList

        [TestMethod]
        public void RummyLadder_CanAddTokenListInvalidSize1_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            List<IToken> tokensToInsert = new List<IToken>();

            AssertHelpers.AssertThrows<RummyException>(() => ladder.CanAdd(tokensToInsert));
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInvalidSize2_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            List<IToken> tokensToInsert = new List<IToken>
                {
                    new Token(Color.Blue, 1), new Token(Color.Blue, 2), new Token(Color.Blue, 3),
                    new Token(Color.Blue, 4), new Token(Color.Blue, 5), new Token(Color.Blue, 6),
                    new Token(Color.Blue, 7), new Token(Color.Blue, 8), new Token(Color.Blue, 9)
                };

            AssertHelpers.AssertThrows<RummyException>(() => ladder.CanAdd(tokensToInsert));
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInvalidColors_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 1),
                new Token(Color.Blue, 2),
                new Token(Color.Yellow, 3)
            };

            AssertHelpers.AssertThrows<RummyException>(() => ladder.CanAdd(tokensToInsert));
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListNotCorrelative_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 10),
                new Token(Color.Blue, 11),
                new Token(Color.Blue, 13)
            };

            AssertHelpers.AssertThrows<RummyException>(() => ladder.CanAdd(tokensToInsert));
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListNotTheSameColor_NotPossibleNoExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Black, 1),
                new Token(Color.Black, 2),
                new Token(Color.Black, 3)
            };

            IActionResult desiredResult = new ActionResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInInitialExtremeBadSequence_NotPossibleNoExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 2),
                new Token(Color.Blue, 3),
                new Token(Color.Blue, 4)
            };

            IActionResult desiredResult = new ActionResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInFinalExtremeBadSequence_NotPossibleNoExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 8),
                new Token(Color.Blue, 9),
                new Token(Color.Blue, 10)
            };

            IActionResult desiredResult = new ActionResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInInitialExtremeValidWithoutExtraTokens_PossibleNoExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 1),
                new Token(Color.Blue, 2),
                new Token(Color.Blue, 3)
            };

            IActionResult desiredResult = new ActionResult(true, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInFinalExtremeValidWithoutExtraTokens_PossibleNoExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 10),
                new Token(Color.Blue, 11),
                new Token(Color.Blue, 12)
            };

            IActionResult desiredResult = new ActionResult(true, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInInitialExtremeValidWithExtraTokensNeeded_PossibleWithExtraTokens()
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

            IActionResult desiredResult = new ActionResult(true, desiredExtraTokens);

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInFinalExtremeValidWithExtraTokensNeeded_PossibleWithExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 11),
                new Token(Color.Blue, 12),
                new Token(Color.Blue, 13)
            };
            List<IToken> desiredExtraTokens = new List<IToken>
            {
                new Token(Color.Blue, 10)
            };

            IActionResult desiredResult = new ActionResult(true, desiredExtraTokens);

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInTheMiddleExceedsMaximumSize_NotPossibleNoExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 6),
                new Token(Color.Blue, 7),
                new Token(Color.Blue, 8)
            };

            IActionResult desiredResult = new ActionResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInTheMiddleInvalidPosition1_NotPossibleNoExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 4),
                new Token(Color.Blue, 5)
            };

            IActionResult desiredResult = new ActionResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInTheMiddleInvalidPosition2_NotPossibleNoExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 8),
                new Token(Color.Blue, 9)
            };

            IActionResult desiredResult = new ActionResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInTheMiddleValidPosition1_PossibleNoExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 5),
                new Token(Color.Blue, 6)
            };

            IActionResult desiredResult = new ActionResult(true, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInTheMiddleValidPosition2_PossibleNoExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 6),
                new Token(Color.Blue, 7)
            };

            IActionResult desiredResult = new ActionResult(true, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInTheMiddleValidPosition3_PossibleNoExtraTokens()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 7),
                new Token(Color.Blue, 8)
            };

            IActionResult desiredResult = new ActionResult(true, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, desiredResult);
        }

        #endregion

        #region Add_OnlyOneToken

        [TestMethod]
        public void RummyLadder_AddOnlyOneTokenNotPossible_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            List<IToken> tokensToInsert = new List<IToken>()
            {
                new Token(Color.Black, 3)
            };

            AssertHelpers.AssertThrows<RummyException>(() => ladder.Add(tokensToInsert));
        }

        [TestMethod]
        public void RummyLadder_AddOnlyOneTokenInvalidExtraTokens_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
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

            AssertHelpers.AssertThrows<RummyException>(() => ladder.Add(tokensToInsert, extraTokens));
        }

        [TestMethod]
        public void RummyLadder_AddOnlyOneTokenInInitialExtremeWithoutExtraTokens_Succeeds()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensAreAdded(new Token(Color.Blue, 3), false);
        }

        [TestMethod]
        public void RummyLadder_AddOnlyOneTokenInFinalExtremeWithoutExtraTokens_Succeeds()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensAreAdded(new Token(Color.Blue, 10), false);
        }

        [TestMethod]
        public void RummyLadder_AddOnlyOneTokenInInitialExtremeWithExtraTokens_Succeeds()
        {
            List<IToken> extraTokens = new List<IToken>()
            {
                new Token(Color.Blue, 2),
                new Token(Color.Blue, 3)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensAreAdded(new Token(Color.Blue, 1), extraTokens, false);
        }

        [TestMethod]
        public void RummyLadder_AddOnlyOneTokenInFinalExtremeWithExtraTokens_Succeeds()
        {
            List<IToken> extraTokens = new List<IToken>()
            {
                new Token(Color.Blue, 10),
                new Token(Color.Blue, 11)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensAreAdded(new Token(Color.Blue, 12), extraTokens, false);
        }

        [TestMethod]
        public void RummyLadder_AddOnlyOneTokenInTheMiddle_Succeeds()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensAreAdded(new Token(Color.Blue, 6), true);
        }

        #endregion

        #region Add_TokenList

        [TestMethod]
        public void RummyLadder_AddTokenListInvalidTokens_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            List<IToken> tokensToInsert = new List<IToken>()
            {
                new Token(Color.Blue, 2),
                new Token(Color.Black, 3)
            };

            AssertHelpers.AssertThrows<RummyException>(() => ladder.Add(tokensToInsert));
        }

        [TestMethod]
        public void RummyLadder_AddTokenListInvalidExtraTokens_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
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

            AssertHelpers.AssertThrows<RummyException>(() => ladder.Add(tokensToInsert, extraTokens));
        }

        [TestMethod]
        public void RummyLadder_AddTokenListInInitialExtremeWithoutExtraTokens_Succeeds()
        {
            List<IToken> tokensToInsert = new List<IToken>()
            {
                new Token(Color.Blue, 2),
                new Token(Color.Blue, 3)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensAreAdded(tokensToInsert, false);
        }

        [TestMethod]
        public void RummyLadder_AddTokenListInFinalExtremeWithoutExtraTokens_Succeeds()
        {
            List<IToken> tokensToInsert = new List<IToken>()
            {
                new Token(Color.Blue, 10),
                new Token(Color.Blue, 11)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensAreAdded(tokensToInsert, false);
        }

        [TestMethod]
        public void RummyLadder_AddTokenListInInitialExtremeWithExtraTokens_Succeeds()
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

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensAreAdded(tokensToInsert, extraTokens, false);
        }

        [TestMethod]
        public void RummyLadder_AddTokenListInFinalExtremeWithExtraTokens_Succeeds()
        {
            List<IToken> tokensToInsert = new List<IToken>()
            {
                new Token(Color.Blue, 12),
                new Token(Color.Blue, 13)
            };

            List<IToken> extraTokens = new List<IToken>()
            {
                new Token(Color.Blue, 10),
                new Token(Color.Blue, 11)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensAreAdded(tokensToInsert, extraTokens, false);
        }

        [TestMethod]
        public void RummyLadder_AddTokenListInTheMiddle_Succeeds()
        {
            List<IToken> tokensToInsert = new List<IToken>()
            {
                new Token(Color.Blue, 5),
                new Token(Color.Blue, 6)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensAreAdded(tokensToInsert, true);
        }

        #endregion

        #region CanGet_OnlyOneToken

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenNotTheSameColor_NotPossibleNoExtraTokens()
        {
            IActionResult desiredResult = new ActionResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeGet(new Token(Color.Black, 4), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenOutOfLimits_NotPossibleNoExtraTokens()
        {
            IActionResult desiredResult = new ActionResult(false, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeGet(new Token(Color.Blue, 3), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInInitialExtreme1_IsPossibleNoExtraTokens()
        {
            IActionResult desiredResult = new ActionResult(true, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeGet(new Token(Color.Blue, 4), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInInitialExtreme2_IsPossibleNoExtraTokens()
        {
            IActionResult desiredResult = new ActionResult(true, new List<IToken>());

            List<IToken> structureTokens = new List<IToken>
            {
                new Token(Color.Blue, 1),
                new Token(Color.Blue, 2),
                new Token(Color.Blue, 3),
                new Token(Color.Blue, 4)
            };

            this.AssertTokensCanBeGet(structureTokens, new Token(Color.Blue, 1), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInFinalExtreme1_IsPossibleNoExtraTokens()
        {
            IActionResult desiredResult = new ActionResult(true, new List<IToken>());

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeGet(new Token(Color.Blue, 9), desiredResult);
        }
        
        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInFinalExtreme2_IsPossibleNoExtraTokens()
        {
            IActionResult desiredResult = new ActionResult(true, new List<IToken>());

            List<IToken> structureTokens = new List<IToken>
            {
                new Token(Color.Blue, 10),
                new Token(Color.Blue, 11),
                new Token(Color.Blue, 12),
                new Token(Color.Blue, 13)
            };

            this.AssertTokensCanBeGet(structureTokens, new Token(Color.Blue, 13), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInInitialExtremeNotEnoughSpace1_NotPossibleNoExtraTokens()
        {
            IActionResult desiredResult = new ActionResult(false, new List<IToken>());

            List<IToken> structureTokens = new List<IToken>
            {
                new Token(Color.Blue, 1),
                new Token(Color.Blue, 2),
                new Token(Color.Blue, 3),
                new Token(Color.Blue, 4),
                new Token(Color.Blue, 5),
                new Token(Color.Blue, 6)
            };

            this.AssertTokensCanBeGet(structureTokens, new Token(Color.Blue, 2), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInInitialExtremeNotEnoughSpace2_NotPossibleNoExtraTokens()
        {
            IActionResult desiredResult = new ActionResult(false, new List<IToken>());

            List<IToken> structureTokens = new List<IToken>
            {
                new Token(Color.Blue, 1),
                new Token(Color.Blue, 2),
                new Token(Color.Blue, 3),
                new Token(Color.Blue, 4),
                new Token(Color.Blue, 5),
                new Token(Color.Blue, 6)
            };

            this.AssertTokensCanBeGet(structureTokens, new Token(Color.Blue, 3), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInFinalExtremeNotEnoughSpace1_NotPossibleNoExtraTokens()
        {
            IActionResult desiredResult = new ActionResult(false, new List<IToken>());

            List<IToken> structureTokens = new List<IToken>
            {
                new Token(Color.Blue, 8),
                new Token(Color.Blue, 9),
                new Token(Color.Blue, 10),
                new Token(Color.Blue, 11),
                new Token(Color.Blue, 12),
                new Token(Color.Blue, 13)
            };

            this.AssertTokensCanBeGet(structureTokens, new Token(Color.Blue, 12), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInFinalExtremeNotEnoughSpace2_NotPossibleNoExtraTokens()
        {
            IActionResult desiredResult = new ActionResult(false, new List<IToken>());

            List<IToken> structureTokens = new List<IToken>
            {
                new Token(Color.Blue, 8),
                new Token(Color.Blue, 9),
                new Token(Color.Blue, 10),
                new Token(Color.Blue, 11),
                new Token(Color.Blue, 12),
                new Token(Color.Blue, 13)
            };

            this.AssertTokensCanBeGet(structureTokens, new Token(Color.Blue, 11), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInInitialExtremeNeedsExtraTokens1_IsPossibleWithExtraTokens()
        {
            List<IToken> desiredExtraTokens = new List<IToken>
            {
                new Token(Color.Blue, 2),
                new Token(Color.Blue, 3)
            };
            IActionResult desiredResult = new ActionResult(true, desiredExtraTokens);

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeGet(new Token(Color.Blue, 5), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInInitialExtremeNeedsExtraTokens2_IsPossibleWithExtraTokens()
        {
            List<IToken> desiredExtraTokens = new List<IToken>
            {
                new Token(Color.Blue, 3)
            };
            IActionResult desiredResult = new ActionResult(true, desiredExtraTokens);

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeGet(new Token(Color.Blue, 6), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInFinalExtremeNeedsExtraTokens1_IsPossibleWithExtraTokens()
        {
            List<IToken> desiredExtraTokens = new List<IToken>
            {
                new Token(Color.Blue, 10),
                new Token(Color.Blue, 11)
            };
            IActionResult desiredResult = new ActionResult(true, desiredExtraTokens);

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeGet(new Token(Color.Blue, 8), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInFinalExtremeNeedsExtraTokens2_IsPossibleWithExtraTokens()
        {
            List<IToken> desiredExtraTokens = new List<IToken>
            {
                new Token(Color.Blue, 10)
            };
            IActionResult desiredResult = new ActionResult(true, desiredExtraTokens);

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeGet(new Token(Color.Blue, 7), desiredResult);
        }

		#endregion

		#region CanGet_TokenList

		[TestMethod]
		public void RummyLadder_GetTokenListInvalidColors_ExceptionThrown()
		{
			// Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
			this.InitializeDummyTokensAsValidLadder();
			RummyLadder ladder = new RummyLadder(this.dummyTokens);

			List<IToken> tokensToGet = new List<IToken>
			{
				new Token(Color.Blue, 4), new Token(Color.Yellow, 5), new Token(Color.Blue, 6)
			};

			AssertHelpers.AssertThrows<RummyException>(() => ladder.CanGet(tokensToGet));
		}

		[TestMethod]
		public void RummyLadder_GetTokenListInvalidSequence_ExceptionThrown()
		{
			// Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
			this.InitializeDummyTokensAsValidLadder();
			RummyLadder ladder = new RummyLadder(this.dummyTokens);

			List<IToken> tokensToGet = new List<IToken>
			{
				new Token(Color.Blue, 4), new Token(Color.Blue, 6), new Token(Color.Blue, 7)
			};

			AssertHelpers.AssertThrows<RummyException>(() => ladder.CanGet(tokensToGet));
		}

		[TestMethod]
		public void RummyLadder_GetTokenListNotTheSameColor_NotPossibleNoExtraTokens()
		{
			IActionResult desiredResult = new ActionResult(false, new List<IToken>());

			List<IToken> tokensToGet = new List<IToken>
			{
				new Token(Color.Black, 4), new Token(Color.Black, 5), new Token(Color.Black, 6)
			};

			// Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
			this.AssertTokensCanBeGet(tokensToGet, desiredResult);
		}

		[TestMethod]
		public void RummyLadder_GetTokenListOutOfLimits_NotPossibleNoExtraTokens()
		{
			IActionResult desiredResult = new ActionResult(false, new List<IToken>());

			List<IToken> tokensToGet = new List<IToken>
			{
				new Token(Color.Blue, 1), new Token(Color.Blue, 2), new Token(Color.Blue, 3)
			};

			// Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
			this.AssertTokensCanBeGet(tokensToGet, desiredResult);
		}

		[TestMethod]
		public void RummyLadder_GetTokenListInInitialExtreme1_IsPossibleNoExtraTokens()
		{
			IActionResult desiredResult = new ActionResult(true, new List<IToken>());

			List<IToken> tokensToGet = new List<IToken>
			{
				new Token(Color.Blue, 4), new Token(Color.Blue, 5),	new Token(Color.Blue, 6)
			};

			// Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
			this.AssertTokensCanBeGet(tokensToGet, desiredResult);
		}

		[TestMethod]
		public void RummyLadder_GetTokenListInInitialExtreme2_IsPossibleNoExtraTokens()
		{
			IActionResult desiredResult = new ActionResult(true, new List<IToken>());

			List<IToken> structureTokens = new List<IToken>
			{
				new Token(Color.Blue, 1), new Token(Color.Blue, 2),	new Token(Color.Blue, 3),
				new Token(Color.Blue, 4), new Token(Color.Blue, 5), new Token(Color.Blue, 6)
			};

			List<IToken> tokensToGet = new List<IToken>
			{
				new Token(Color.Blue, 1), new Token(Color.Blue, 2), new Token(Color.Blue, 3)
			};

            this.AssertTokensCanBeGet(structureTokens, tokensToGet, desiredResult);
		}

		[TestMethod]
		public void RummyLadder_GetTokenListInFinalExtreme1_IsPossibleNoExtraTokens()
		{
			IActionResult desiredResult = new ActionResult(true, new List<IToken>());

			List<IToken> tokensToGet = new List<IToken>
			{
				new Token(Color.Blue, 7), new Token(Color.Blue, 8),	new Token(Color.Blue, 9)
			};

			// Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
			this.AssertTokensCanBeGet(tokensToGet, desiredResult);
		}

		[TestMethod]
		public void RummyLadder_GetTokenListInInFinalExtreme2_IsPossibleNoExtraTokens()
		{
			IActionResult desiredResult = new ActionResult(true, new List<IToken>());

			List<IToken> structureTokens = new List<IToken>
			{
				new Token(Color.Blue, 8), new Token(Color.Blue, 9), new Token(Color.Blue, 10),
				new Token(Color.Blue, 11), new Token(Color.Blue, 12), new Token(Color.Blue, 13)
			};

			List<IToken> tokensToGet = new List<IToken>
			{
				new Token(Color.Blue, 11), new Token(Color.Blue, 12), new Token(Color.Blue, 13)
			};

			this.AssertTokensCanBeGet(structureTokens, tokensToGet, desiredResult);
		}

		[TestMethod]
		public void RummyLadder_GetTokenListInInitialExtremeNotEnoughSpace1_NotPossibleNoExtraTokens()
		{
			IActionResult desiredResult = new ActionResult(false, new List<IToken>());

            List<IToken> structureTokens = new List<IToken>
			{
				new Token(Color.Blue, 1), new Token(Color.Blue, 2),	new Token(Color.Blue, 3),
				new Token(Color.Blue, 4), new Token(Color.Blue, 5),	new Token(Color.Blue, 6)
			};

			List<IToken> tokensToGet = new List<IToken>
			{
				new Token(Color.Blue, 2), new Token(Color.Blue, 3)
			};

			this.AssertTokensCanBeGet(structureTokens, tokensToGet, desiredResult);
		}

		[TestMethod]
		public void RummyLadder_GetTokenListInInitialExtremeNotEnoughSpace2_NotPossibleNoExtraTokens()
		{
			IActionResult desiredResult = new ActionResult(false, new List<IToken>());

			List<IToken> structureTokens = new List<IToken>
			{
				new Token(Color.Blue, 1), new Token(Color.Blue, 2), new Token(Color.Blue, 3),
				new Token(Color.Blue, 4), new Token(Color.Blue, 5), new Token(Color.Blue, 6)
			};

			List<IToken> tokensToGet = new List<IToken>
			{
				new Token(Color.Blue, 3), new Token(Color.Blue, 4)
			};

			this.AssertTokensCanBeGet(structureTokens, tokensToGet, desiredResult);
		}

		[TestMethod]
		public void RummyLadder_GetTokenListInFinalExtremeNotEnoughSpace1_NotPossibleNoExtraTokens()
		{
			IActionResult desiredResult = new ActionResult(false, new List<IToken>());

			List<IToken> structureTokens = new List<IToken>
			{
				new Token(Color.Blue, 8), new Token(Color.Blue, 9),	new Token(Color.Blue, 10),
				new Token(Color.Blue, 11), new Token(Color.Blue, 12), new Token(Color.Blue, 13)
			};

			List<IToken> tokensToGet = new List<IToken>
			{
				new Token(Color.Blue, 11), new Token(Color.Blue, 12)
			};

			this.AssertTokensCanBeGet(structureTokens, tokensToGet, desiredResult);
		}

		[TestMethod]
		public void RummyLadder_GetTokenListInFinalExtremeNotEnoughSpace2_NotPossibleNoExtraTokens()
		{
			IActionResult desiredResult = new ActionResult(false, new List<IToken>());

			List<IToken> structureTokens = new List<IToken>
			{
				new Token(Color.Blue, 8), new Token(Color.Blue, 9), new Token(Color.Blue, 10),
				new Token(Color.Blue, 11), new Token(Color.Blue, 12), new Token(Color.Blue, 13)
			};

			List<IToken> tokensToGet = new List<IToken>
			{
				new Token(Color.Blue, 10), new Token(Color.Blue, 11)
			};

			this.AssertTokensCanBeGet(structureTokens, tokensToGet, desiredResult);
		}

		[TestMethod]
		public void RummyLadder_GetTokenListInInitialExtremeNeedsExtraTokens1_IsPossibleWithExtraTokens()
		{
			List<IToken> structureTokens = new List<IToken>
			{
				new Token(Color.Blue, 4), new Token(Color.Blue, 5), new Token(Color.Blue, 6),
				new Token(Color.Blue, 7), new Token(Color.Blue, 8), new Token(Color.Blue, 9),
                new Token(Color.Blue, 10), new Token(Color.Blue, 11), new Token(Color.Blue, 12)
			};

			List<IToken> tokensToGet = new List<IToken>
			{
                new Token(Color.Blue, 5), new Token(Color.Blue, 6), new Token(Color.Blue, 7)
            };

			List<IToken> desiredExtraTokens = new List<IToken>
			{
				new Token(Color.Blue, 2),
				new Token(Color.Blue, 3)
			};

			IActionResult desiredResult = new ActionResult(true, desiredExtraTokens);

			this.AssertTokensCanBeGet(structureTokens, tokensToGet, desiredResult);
		}

		[TestMethod]
		public void RummyLadder_GetTokenListInInitialExtremeNeedsExtraTokens2_IsPossibleWithExtraTokens()
		{
			List<IToken> structureTokens = new List<IToken>
			{
				new Token(Color.Blue, 4), new Token(Color.Blue, 5), new Token(Color.Blue, 6),
				new Token(Color.Blue, 7), new Token(Color.Blue, 8), new Token(Color.Blue, 9),
				new Token(Color.Blue, 10), new Token(Color.Blue, 11), new Token(Color.Blue, 12)
			};

			List<IToken> tokensToGet = new List<IToken>
			{
                new Token(Color.Blue, 6), new Token(Color.Blue, 7), new Token(Color.Blue, 8)
			};

			List<IToken> desiredExtraTokens = new List<IToken>
			{
				new Token(Color.Blue, 3)
			};
                                                    
			IActionResult desiredResult = new ActionResult(true, desiredExtraTokens);

			this.AssertTokensCanBeGet(structureTokens, tokensToGet, desiredResult);
		}

		[TestMethod]
		public void RummyLadder_CanGetTokenListInFinalExtremeNeedsExtraTokens1_IsPossibleWithExtraTokens()
		{
			List<IToken> structureTokens = new List<IToken>
			{
				new Token(Color.Blue, 4), new Token(Color.Blue, 5), new Token(Color.Blue, 6),
				new Token(Color.Blue, 7), new Token(Color.Blue, 8), new Token(Color.Blue, 9),
				new Token(Color.Blue, 10), new Token(Color.Blue, 11)
			};

			List<IToken> tokensToGet = new List<IToken>
			{
                new Token(Color.Blue, 8), new Token(Color.Blue, 9), new Token(Color.Blue, 10)
			};

			List<IToken> desiredExtraTokens = new List<IToken>
			{
				new Token(Color.Blue, 12),
                new Token(Color.Blue, 13)
			};
                                                    
			IActionResult desiredResult = new ActionResult(true, desiredExtraTokens);

			this.AssertTokensCanBeGet(structureTokens, tokensToGet, desiredResult);
		}

		[TestMethod]
		public void RummyLadder_CanGetTokenListInFinalExtremeNeedsExtraTokens2_IsPossibleWithExtraTokens()
		{
			List<IToken> structureTokens = new List<IToken>
			{
				new Token(Color.Blue, 4), new Token(Color.Blue, 5), new Token(Color.Blue, 6),
				new Token(Color.Blue, 7), new Token(Color.Blue, 8), new Token(Color.Blue, 9),
				new Token(Color.Blue, 10), new Token(Color.Blue, 11)
			};

			List<IToken> tokensToGet = new List<IToken>
			{
                new Token(Color.Blue, 7), new Token(Color.Blue, 8), new Token(Color.Blue, 9)
			};

			List<IToken> desiredExtraTokens = new List<IToken>
			{
				new Token(Color.Blue, 12)
			};
                                                    
			IActionResult desiredResult = new ActionResult(true, desiredExtraTokens);

			this.AssertTokensCanBeGet(structureTokens, tokensToGet, desiredResult);
		}

		[TestMethod]
		public void RummyLadder_CanGetTokenListInMiddleNeedsExtraTokens1_IsPossibleWithExtraTokens()
		{
			List<IToken> tokensToGet = new List<IToken>
			{
                new Token(Color.Blue, 5), new Token(Color.Blue, 6), new Token(Color.Blue, 7), new Token(Color.Blue, 8)
			};

			List<IToken> desiredExtraTokens = new List<IToken>
			{
				new Token(Color.Blue, 2), new Token(Color.Blue, 3), new Token(Color.Blue, 10), new Token(Color.Blue, 11)
			};

			IActionResult desiredResult = new ActionResult(true, desiredExtraTokens);

		    // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
			this.AssertTokensCanBeGet(tokensToGet, desiredResult);
		}

		[TestMethod]
		public void RummyLadder_CanGetTokenListInMiddleNeedsExtraTokens2_IsPossibleWithExtraTokens()
		{
			List<IToken> tokensToGet = new List<IToken>
			{
				new Token(Color.Blue, 6), new Token(Color.Blue, 7)
			};

			List<IToken> desiredExtraTokens = new List<IToken>
			{
				new Token(Color.Blue, 3), new Token(Color.Blue, 10)
			};

			IActionResult desiredResult = new ActionResult(true, desiredExtraTokens);

		    // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
		    this.AssertTokensCanBeGet(tokensToGet, desiredResult);
        }
                                                    
		#endregion

		private void AssertTokensCanBeAdded(object tokenToInsert, IActionResult desiredResult)
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

            IActionResult result = ladder.CanAdd(tokensToInsert);

            Assert.AreEqual(desiredResult.ActionIsPossible, result.ActionIsPossible);
            AssertHelpers.AssertTokenListsContainsEquivalentElements(desiredResult.NeededExtraTokens, result.NeededExtraTokens);
        }
        
        private void AssertTokensCanBeGet(object tokenToGet, IActionResult desiredResult)
        {
            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.InitializeDummyTokensAsValidLadder();

            this.AssertTokensCanBeGet(this.dummyTokens, tokenToGet, desiredResult);
        }

        private void AssertTokensCanBeGet(List<IToken> structureTokens, object tokenToGet, IActionResult desiredResult)
        {
            this.dummyTokens = structureTokens;
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            List<IToken> tokensToGet;

            if (tokenToGet.GetType() == typeof(Token))
            {
                tokensToGet = new List<IToken> { (IToken)tokenToGet };
            }
            else
            {
                tokensToGet = (List<IToken>)tokenToGet;
            }

            IActionResult result = ladder.CanGet(tokensToGet);

            Assert.AreEqual(desiredResult.ActionIsPossible, result.ActionIsPossible);
            AssertHelpers.AssertTokenListsContainsEquivalentElements(desiredResult.NeededExtraTokens, result.NeededExtraTokens);
        }

        private void AssertTokensAreAdded(object tokenToInsert, bool creationOperationIsExpected)
        {
            this.AssertTokensAreAdded(tokenToInsert, new List<IToken>(), creationOperationIsExpected);
        }

        private void AssertTokensAreAdded(object tokenToInsert, List<IToken> extraTokens, bool creationOperationIsExpected)
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

            List<IOperationResult> operationResults = ladder.Add(tokensToInsert, extraTokens);
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
                new Token(Color.Blue, 8),
                new Token(Color.Blue, 9)
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
                new Token(Color.Blue, 8),
                new Token(Color.Blue, 9)
            };
        }

        private void InitializeDummyTokensAsInvalidColorLadder()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 4),
                new Token(Color.Blue, 5),
                new Token(Color.Yellow, 6),
                new Token(Color.Blue, 7),
                new Token(Color.Blue, 8),
                new Token(Color.Blue, 9)
            };
        }
    }
}
