using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RummyEnvironment;
using System.Drawing;
using System.Linq;
using RummyEnvironment.Helpers;
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
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            this.InitializeDummyTokensAsSingleToken();

            AssertHelpers.AssertThrows<RummyException>(() =>
                ladder.Modify(this.dummyTokens));
        }

        [TestMethod]
        public void RummyLadder_ModifyWithOnlyTwoTokens_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            this.InitializeDummyTokensAsOnlyTwoTokens();

            AssertHelpers.AssertThrows<RummyException>(() =>
                ladder.Modify(this.dummyTokens));
        }

        [TestMethod]
        public void RummyLadder_ModifyWithBrokenLadder_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            this.InitializeDummyTokensAsBrokenLadder();

            AssertHelpers.AssertThrows<RummyException>(() =>
                ladder.Modify(this.dummyTokens));
        }

        [TestMethod]
        public void RummyLadder_ModifyWithInvalidColors_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            this.InitializeDummyTokensAsInvalidColorLadder();

            AssertHelpers.AssertThrows<RummyException>(() =>
                ladder.Modify(this.dummyTokens));
        }

        #endregion

        #region CanAdd_OnlyOneToken

        [TestMethod]
        public void RummyLadder_CanAddOnlyOneTokenNotTheSameColor_NotPossible()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(new Token(Color.Black, 3), false);
        }

        [TestMethod]
        public void RummyLadder_CanAddOnlyOneTokenInInitialExtreme_IsPossible()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(new Token(Color.Blue, 3), true);
        }

        [TestMethod]
        public void RummyLadder_CanAddOnlyOneTokenInFinalExtreme_IsPossible()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(new Token(Color.Blue, 10), true);
        }

        [TestMethod]
        public void RummyLadder_CanAddOnlyOneTokenOutOfLimitsInitialExtreme_NotPossible()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(new Token(Color.Blue, 2), false);
        }

        [TestMethod]
        public void RummyLadder_CanAddOnlyOneTokenOutOfLimitsFinalExtreme_NotPossible()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(new Token(Color.Blue, 11), false);
        }

        [TestMethod]
        public void RummyLadder_CanAddOnlyOneTokenNotValidPositionInitialExtreme1_NotPossible()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(new Token(Color.Blue, 4), false);
        }

        [TestMethod]
        public void RummyLadder_CanAddOnlyOneTokenNotValidPositionInitialExtreme2_NotPossible()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(new Token(Color.Blue, 5), false);
        }

        [TestMethod]
        public void RummyLadder_CanAddOnlyOneTokenNotValidPositionFinalExtreme1_NotPossible()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(new Token(Color.Blue, 9), false);
        }

        [TestMethod]
        public void RummyLadder_CanAddOnlyOneTokenNotValidPositionFinalExtreme2_NotPossible()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(new Token(Color.Blue, 8), false);
        }

        [TestMethod]
        public void RummyLadder_CanAddOnlyOneTokenNotValidPositionMiddle1_NotPossible()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(new Token(Color.Blue, 6), false);
        }

        [TestMethod]
        public void RummyLadder_CanAddOnlyOneTokenNotValidPositionMiddle2_NotPossible()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(new Token(Color.Blue, 7), false);
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
        public void RummyLadder_CanAddTokenListNotTheSameColor_NotPossible()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Black, 1),
                new Token(Color.Black, 2),
                new Token(Color.Black, 3)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, false);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInInitialExtremeOutOfLimits_NotPossible()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 1),
                new Token(Color.Blue, 2)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, false);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInFinalExtremeOutOfLimits_NotPossible()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 11),
                new Token(Color.Blue, 12)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, false);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInInitialExtremeBadSequence1_NotPossible()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 3),
                new Token(Color.Blue, 4)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, false);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInInitialExtremeBadSequence2_NotPossible()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 4),
                new Token(Color.Blue, 5)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, false);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInInitialExtremeBadSequence3_NotPossible()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 4),
                new Token(Color.Blue, 5),
                new Token(Color.Blue, 6)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, false);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInInitialExtremeBadSequence4_NotPossible()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 3),
                new Token(Color.Blue, 4),
                new Token(Color.Blue, 5),
                new Token(Color.Blue, 6)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, false);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInFinalExtremeBadSequence1_NotPossible()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 9),
                new Token(Color.Blue, 10)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, false);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInFinalExtremeBadSequence2_NotPossible()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 8),
                new Token(Color.Blue, 9)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, false);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInFinalExtremeBadSequence3_NotPossible()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 7),
                new Token(Color.Blue, 8),
                new Token(Color.Blue, 9)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, false);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInFinalExtremeBadSequence4_NotPossible()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 7),
                new Token(Color.Blue, 8),
                new Token(Color.Blue, 9),
                new Token(Color.Blue, 10)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, false);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInMiddle1_NotPossible()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 5),
                new Token(Color.Blue, 6)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, false);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInMiddle2_NotPossible()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 7),
                new Token(Color.Blue, 8)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, false);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInMiddle3_NotPossible()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 5),
                new Token(Color.Blue, 6),
                new Token(Color.Blue, 7),
                new Token(Color.Blue, 8)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, false);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInInitialExtremeValid_IsPossible()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 1),
                new Token(Color.Blue, 2),
                new Token(Color.Blue, 3)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, true);
        }

        [TestMethod]
        public void RummyLadder_CanAddTokenListInFinalExtremeValid_IsPossible()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 10),
                new Token(Color.Blue, 11),
                new Token(Color.Blue, 12)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeAdded(tokensToInsert, true);
        }
        
        #endregion

        #region Add_OnlyOneToken

        [TestMethod]
        public void RummyLadder_AddOnlyOneTokenNotPossible_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            AssertHelpers.AssertThrows<RummyException>(() => ladder.Add(new Token(Color.Black, 3)));
        }
        
        [TestMethod]
        public void RummyLadder_AddOnlyOneTokenInInitialExtreme_Succeeds()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensAreAdded(new Token(Color.Blue, 3), false);
        }

        [TestMethod]
        public void RummyLadder_AddOnlyOneTokenInFinalExtreme_Succeeds()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensAreAdded(new Token(Color.Blue, 10), false);
        }

        #endregion

        #region Add_TokenList

        [TestMethod]
        public void RummyLadder_AddTokenListNotPossible_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 2), new Token(Color.Black, 3)
            };

            AssertHelpers.AssertThrows<RummyException>(() => ladder.Add(tokensToInsert));
        }
        
        [TestMethod]
        public void RummyLadder_AddTokenListInInitialExtreme_Succeeds()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 2), new Token(Color.Blue, 3)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensAreAdded(tokensToInsert, false);
        }

        [TestMethod]
        public void RummyLadder_AddTokenListInFinalExtreme_Succeeds()
        {
            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 10), new Token(Color.Blue, 11)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensAreAdded(tokensToInsert, false);
        }

        #endregion

        #region CanGet_OnlyOneToken

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenNotTheSameColor_NotPossibleNoSpareTokens()
        {
            IActionResult desiredResult = new ActionResult(false);

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeGet(new Token(Color.Black, 4), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenOutOfLimits_NotPossibleNoSpareTokens()
        {
            IActionResult desiredResult = new ActionResult(false);

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeGet(new Token(Color.Blue, 3), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInInitialExtreme1_IsPossibleNoSpareTokens()
        {
            IActionResult desiredResult = new ActionResult(true);

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeGet(new Token(Color.Blue, 4), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInInitialExtreme2_IsPossibleNoSpareTokens()
        {
            IActionResult desiredResult = new ActionResult(true);

            List<IToken> structureTokens = new List<IToken>
            {
                new Token(Color.Blue, 1), new Token(Color.Blue, 2),
                new Token(Color.Blue, 3), new Token(Color.Blue, 4)
            };

            this.AssertTokensCanBeGet(structureTokens, new Token(Color.Blue, 1), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInFinalExtreme1_IsPossibleNoSpareTokens()
        {
            IActionResult desiredResult = new ActionResult(true);

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeGet(new Token(Color.Blue, 9), desiredResult);
        }
        
        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInFinalExtreme2_IsPossibleNoSpareTokens()
        {
            IActionResult desiredResult = new ActionResult(true);

            List<IToken> structureTokens = new List<IToken>
            {
                new Token(Color.Blue, 10), new Token(Color.Blue, 11),
                new Token(Color.Blue, 12), new Token(Color.Blue, 13)
            };

            this.AssertTokensCanBeGet(structureTokens, new Token(Color.Blue, 13), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInInitialExtreme1_IsPossibleWithSpareTokens()
        {
            List<IToken> spareTokens = new List<IToken>
            {
                new Token(Color.Blue, 1)
            };

            IActionResult desiredResult = new ActionResult(true, spareTokens);

            List<IToken> structureTokens = new List<IToken>
            {
                new Token(Color.Blue, 1), new Token(Color.Blue, 2), new Token(Color.Blue, 3),
                new Token(Color.Blue, 4), new Token(Color.Blue, 5), new Token(Color.Blue, 6)
            };
            
            this.AssertTokensCanBeGet(structureTokens, new Token(Color.Blue, 2), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInInitialExtreme2_IsPossibleWithSpareTokens()
        {
            List<IToken> spareTokens = new List<IToken>
            {
                new Token(Color.Blue, 1),
                new Token(Color.Blue, 2)
            };
            
            IActionResult desiredResult = new ActionResult(true, spareTokens);

            List<IToken> structureTokens = new List<IToken>
            {
                new Token(Color.Blue, 1), new Token(Color.Blue, 2), new Token(Color.Blue, 3),
                new Token(Color.Blue, 4), new Token(Color.Blue, 5), new Token(Color.Blue, 6)
            };

            this.AssertTokensCanBeGet(structureTokens, new Token(Color.Blue, 3), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInFinalExtreme1_IsPossibleWithSpareTokens()
        {
            List<IToken> spareTokens = new List<IToken>
            {
                new Token(Color.Blue, 6)
            };

            IActionResult desiredResult = new ActionResult(true, spareTokens);

            List<IToken> structureTokens = new List<IToken>
            {
                new Token(Color.Blue, 1), new Token(Color.Blue, 2), new Token(Color.Blue, 3),
                new Token(Color.Blue, 4), new Token(Color.Blue, 5), new Token(Color.Blue, 6)
            };

            this.AssertTokensCanBeGet(structureTokens, new Token(Color.Blue, 5), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInFinalExtreme2_IsPossibleWithSpareTokens()
        {
            List<IToken> spareTokens = new List<IToken>
            {
                new Token(Color.Blue, 5),
                new Token(Color.Blue, 6)
            };

            IActionResult desiredResult = new ActionResult(true, spareTokens);

            List<IToken> structureTokens = new List<IToken>
            {
                new Token(Color.Blue, 1), new Token(Color.Blue, 2), new Token(Color.Blue, 3),
                new Token(Color.Blue, 4), new Token(Color.Blue, 5), new Token(Color.Blue, 6)
            };

            this.AssertTokensCanBeGet(structureTokens, new Token(Color.Blue, 4), desiredResult);
        }
        
        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInMiddle1_NotPossible()
        {
            IActionResult desiredResult = new ActionResult(false);

            List<IToken> structureTokens = new List<IToken>
            {
                new Token(Color.Blue, 5), new Token(Color.Blue, 6), new Token(Color.Blue, 7),
                new Token(Color.Blue, 8), new Token(Color.Blue, 9)
            };

            this.AssertTokensCanBeGet(structureTokens, new Token(Color.Blue, 7), desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetOnlyOneTokenInMiddle2_IsPossibleWithSpareTokens()
        {
            List<IToken> desiredSpareTokens = new List<IToken>
            {
                new Token(Color.Blue, 9),
                new Token(Color.Blue, 10),
                new Token(Color.Blue, 11)
            };

            IActionResult desiredResult = new ActionResult(true, desiredSpareTokens);

            List<IToken> structureTokens = new List<IToken>
            {
                new Token(Color.Blue, 5), new Token(Color.Blue, 6), new Token(Color.Blue, 7),
                new Token(Color.Blue, 8), new Token(Color.Blue, 9), new Token(Color.Blue, 10),
                new Token(Color.Blue, 11)
            };

            this.AssertTokensCanBeGet(structureTokens, new Token(Color.Blue, 8), desiredResult);
        }

        #endregion

        #region CanGet_TokenList

        [TestMethod]
		public void RummyLadder_CanGetTokenListInvalidColors_ExceptionThrown()
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
		public void RummyLadder_CanGetTokenListInvalidSequence_ExceptionThrown()
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
		public void RummyLadder_CanGetTokenListNotTheSameColor_NotPossible()
		{
			IActionResult desiredResult = new ActionResult(false);

			List<IToken> tokensToGet = new List<IToken>
			{
				new Token(Color.Black, 4), new Token(Color.Black, 5), new Token(Color.Black, 6)
			};

			// Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
			this.AssertTokensCanBeGet(tokensToGet, desiredResult);
		}

        [TestMethod]
        public void RummyLadder_CanGetTokenListOutOfLimitsInInitialExtreme1_NotPossible()
        {
            IActionResult desiredResult = new ActionResult(false);

            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Blue, 1), new Token(Color.Blue, 2), new Token(Color.Blue, 3)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeGet(tokensToGet, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetTokenListOutOfLimitsInInitialExtreme2_NotPossible()
        {
            IActionResult desiredResult = new ActionResult(false);

            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Blue, 2), new Token(Color.Blue, 3), new Token(Color.Blue, 4)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeGet(tokensToGet, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetTokenListOutOfLimitsInFinalExtreme1_NotPossible()
        {
            IActionResult desiredResult = new ActionResult(false);

            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Blue, 10), new Token(Color.Blue, 11), new Token(Color.Blue, 12)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeGet(tokensToGet, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetTokenListOutOfLimitsInFinalExtreme2_NotPossible()
        {
            IActionResult desiredResult = new ActionResult(false);

            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Blue, 9), new Token(Color.Blue, 10), new Token(Color.Blue, 11)
            };

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeGet(tokensToGet, desiredResult);
        }
        
        [TestMethod]
		public void RummyLadder_CanGetTokenListInInitialExtreme1_IsPossibleNoSpareTokens()
		{
			IActionResult desiredResult = new ActionResult(true);

			List<IToken> tokensToGet = new List<IToken>
			{
				new Token(Color.Blue, 4), new Token(Color.Blue, 5),	new Token(Color.Blue, 6)
			};

			// Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
			this.AssertTokensCanBeGet(tokensToGet, desiredResult);
		}

		[TestMethod]
		public void RummyLadder_CanGetTokenListInInitialExtreme2_IsPossibleNoSpareTokens()
		{
			IActionResult desiredResult = new ActionResult(true);

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
		public void RummyLadder_CanGetTokenListInFinalExtreme1_IsPossibleNoSpareTokens()
		{
			IActionResult desiredResult = new ActionResult(true);

			List<IToken> tokensToGet = new List<IToken>
			{
				new Token(Color.Blue, 7), new Token(Color.Blue, 8),	new Token(Color.Blue, 9)
			};

			// Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
			this.AssertTokensCanBeGet(tokensToGet, desiredResult);
		}

		[TestMethod]
		public void RummyLadder_CanGetTokenListInInFinalExtreme2_IsPossibleNoSpareTokens()
		{
			IActionResult desiredResult = new ActionResult(true);

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
		public void RummyLadder_CanGetTokenListInInitialExtremeWithSpareTokens1_IsPossibleWithSpareTokens()
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

			List<IToken> desiredSpareTokens = new List<IToken>
			{
				new Token(Color.Blue, 4)
			};

			IActionResult desiredResult = new ActionResult(true, desiredSpareTokens);

            this.AssertTokensCanBeGet(structureTokens, tokensToGet, desiredResult);
        }

		[TestMethod]
		public void RummyLadder_CanGetTokenListInInitialExtremeWithSpareTokens2_IsPossibleWithSpareTokens()
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


		    List<IToken> desiredSpareTokens = new List<IToken>
		    {
		        new Token(Color.Blue, 4),
                new Token(Color.Blue, 5)
		    };

            IActionResult desiredResult = new ActionResult(true, desiredSpareTokens);

			this.AssertTokensCanBeGet(structureTokens, tokensToGet, desiredResult);
		}

		[TestMethod]
		public void RummyLadder_CanGetTokenListInFinalExtremeWithSpareTokens1_IsPossibleWithSpareTokens()
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

			List<IToken> desiredSpareTokens = new List<IToken>
			{
				new Token(Color.Blue, 11)
			};
                                                    
			IActionResult desiredResult = new ActionResult(true, desiredSpareTokens);

			this.AssertTokensCanBeGet(structureTokens, tokensToGet, desiredResult);
		}

		[TestMethod]
		public void RummyLadder_CanGetTokenListInFinalExtremeWithSpareTokens2_IsPossibleWithSpareTokens()
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

			List<IToken> desiredSpareTokens = new List<IToken>
			{
			    new Token(Color.Blue, 10),
			    new Token(Color.Blue, 11)
			};
                                                    
			IActionResult desiredResult = new ActionResult(true, desiredSpareTokens);

			this.AssertTokensCanBeGet(structureTokens, tokensToGet, desiredResult);
		}

        [TestMethod]
        public void RummyLadder_CanGetTokenListInInMiddle1_IsPossibleWithSpareTokens()
        {
            List<IToken> structureTokens = new List<IToken>
            {
                new Token(Color.Blue, 5), new Token(Color.Blue, 6), new Token(Color.Blue, 7),
                new Token(Color.Blue, 8), new Token(Color.Blue, 9), new Token(Color.Blue, 10),
                new Token(Color.Blue, 11), new Token(Color.Blue, 12), new Token(Color.Blue, 13)
            };

            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Blue, 8), new Token(Color.Blue, 9), new Token(Color.Blue, 10)
            };

            List<IToken> desiredSpareTokens = new List<IToken>
            {
                new Token(Color.Blue, 11),
                new Token(Color.Blue, 12),
                new Token(Color.Blue, 13)
            };

            IActionResult desiredResult = new ActionResult(true, desiredSpareTokens);

            this.AssertTokensCanBeGet(structureTokens, tokensToGet, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetTokenListInInMiddle2_IsPossibleWithSpareTokens()
        {
            List<IToken> structureTokens = new List<IToken>
            {
                new Token(Color.Blue, 5), new Token(Color.Blue, 6), new Token(Color.Blue, 7),
                new Token(Color.Blue, 8), new Token(Color.Blue, 9), new Token(Color.Blue, 10),
                new Token(Color.Blue, 11), new Token(Color.Blue, 12)
            };

            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Blue, 8), new Token(Color.Blue, 9)
            };

            List<IToken> desiredSpareTokens = new List<IToken>
            {
                new Token(Color.Blue, 10),
                new Token(Color.Blue, 11),
                new Token(Color.Blue, 12)
            };

            IActionResult desiredResult = new ActionResult(true, desiredSpareTokens);
            this.AssertTokensCanBeGet(structureTokens, tokensToGet, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetTokenListInMiddleNotPossible1_NotPossibleNoSpareTokens()
        {
            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Blue, 5), new Token(Color.Blue, 6),
                new Token(Color.Blue, 7), new Token(Color.Blue, 8)
            };

            IActionResult desiredResult = new ActionResult(false);

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeGet(tokensToGet, desiredResult);
        }

        [TestMethod]
        public void RummyLadder_CanGetTokenListInMiddleNotPossible2_NotPossibleNoSpareTokens()
        {
            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Blue, 6), new Token(Color.Blue, 7)
            };

            IActionResult desiredResult = new ActionResult(false);

            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.AssertTokensCanBeGet(tokensToGet, desiredResult);
        }

        #endregion

        #region Get_OnlyOneToken

        [TestMethod]
        public void RummyLadder_GetOnlyOneTokenNotPossible_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            List<IToken> tokensToGet = new List<IToken>()
            {
                new Token(Color.Black, 4)
            };

            AssertHelpers.AssertThrows<RummyException>(() => ladder.Get(tokensToGet));
        }
        
        [TestMethod]
        public void RummyLadder_GetOnlyOneTokenInInitialExtreme1_SucceedsWithoutSpareTokens()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            
            this.AssertTokensAreGet(new Token(Color.Blue, 4));         
        }

        [TestMethod]
        public void RummyLadder_GetOnlyOneTokenInInitialExtreme2_SucceedsWithoutSpareTokens()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 1),
                new Token(Color.Blue, 2),
                new Token(Color.Blue, 3),
                new Token(Color.Blue, 4)
            };

            this.AssertTokensAreGet(new Token(Color.Blue, 1));
        }

        [TestMethod]
        public void RummyLadder_GetOnlyOneTokenInFinalExtreme1_SucceedsWithoutSpareTokens()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();

            this.AssertTokensAreGet(new Token(Color.Blue, 9));
        }
        
        [TestMethod]
        public void RummyLadder_GetOnlyOneTokenInFinalExtreme2_SucceedsWithoutSpareTokens()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 10),
                new Token(Color.Blue, 11),
                new Token(Color.Blue, 12),
                new Token(Color.Blue, 13)
            };

            this.AssertTokensAreGet(new Token(Color.Blue, 13));
        }
        
        [TestMethod]
        public void RummyLadder_GetOnlyOneTokenInInitialExtremeWithSpareTokens1_Succeeds()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();

            List<IToken> expectedSpareTokens = new List<IToken>
            {
                new Token(Color.Blue, 4)
            };

            this.AssertTokensAreGet(new Token(Color.Blue, 5), expectedSpareTokens);
        }

        [TestMethod]
        public void RummyLadder_GetOnlyOneTokenInInitialExtremeWithSpareTokens2_Succeeds()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();

            List<IToken> expectedSpareTokens = new List<IToken>
            {
                new Token(Color.Blue, 4),
                new Token(Color.Blue, 5)
            };

            this.AssertTokensAreGet(new Token(Color.Blue, 6), expectedSpareTokens);
        }

        [TestMethod]
        public void RummyLadder_GetOnlyOneTokenInFinalExtremeWithSpareTokens1_Succeeds()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();

            List<IToken> expectedSpareTokens = new List<IToken>
            {
                new Token(Color.Blue, 9)
            };

            this.AssertTokensAreGet(new Token(Color.Blue, 8), expectedSpareTokens);
        }

        [TestMethod]
        public void RummyLadder_GetOnlyOneTokenInFinalExtremeWithSpareTokens2_Succeeds()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();

            List<IToken> expectedSpareTokens = new List<IToken>
            {
                new Token(Color.Blue, 8),
                new Token(Color.Blue, 9)
            };

            this.AssertTokensAreGet(new Token(Color.Blue, 7), expectedSpareTokens);
        }

        [TestMethod]
        public void RummyLadder_GetOnlyOneTokenInMiddle_SucceedsWithSpareTokens()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 7),
                new Token(Color.Blue, 8),
                new Token(Color.Blue, 9),
                new Token(Color.Blue, 10),
                new Token(Color.Blue, 11),
                new Token(Color.Blue, 12),
                new Token(Color.Blue, 13)
            };

            List<IToken> expectedSpareTokens = new List<IToken>
            {
                new Token(Color.Blue, 11),
                new Token(Color.Blue, 12),
                new Token(Color.Blue, 13)
            };

            this.AssertTokensAreGet(new Token(Color.Blue, 10), expectedSpareTokens);
        }

        #endregion

        #region Get_TokenList

        [TestMethod]
        public void RummyLadder_GetTokenListInvalidTokens_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Blue, 4), new Token(Color.Black, 5)
            };

            AssertHelpers.AssertThrows<RummyException>(() => ladder.Get(tokensToGet));
        }
        
        [TestMethod]
        public void RummyLadder_GetTokenListNotPossible_ExceptionThrown()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();
            RummyLadder ladder = new RummyLadder(this.dummyTokens);

            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Black, 4), new Token(Color.Black, 5)
            };

            AssertHelpers.AssertThrows<RummyException>(() => ladder.Get(tokensToGet));
        }

        [TestMethod]
        public void RummyLadder_GetTokenListInInitialExtreme1_SucceedWithoutSpareTokens()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();

            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Blue, 4), new Token(Color.Blue, 5), new Token(Color.Blue, 6)
            };

            this.AssertTokensAreGet(tokensToGet);
        }

        [TestMethod]
        public void RummyLadder_GetTokenListInInitialExtreme2_SucceedWithoutSpareTokens()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 1), new Token(Color.Blue, 2), new Token(Color.Blue, 3),
                new Token(Color.Blue, 4), new Token(Color.Blue, 5), new Token(Color.Blue, 6)
            } ;

            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Blue, 1), new Token(Color.Blue, 2), new Token(Color.Blue, 3)
            };

            this.AssertTokensAreGet(tokensToGet);
        }

        [TestMethod]
        public void RummyLadder_GetTokenListInFinalExtreme1_SucceedWithoutSpareTokens()
        {
            // Ladder initialized with 4, 5, 6, 7, 8, 9 (Blue).
            this.InitializeDummyTokensAsValidLadder();

            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Blue, 7), new Token(Color.Blue, 8), new Token(Color.Blue, 9)
            };

            this.AssertTokensAreGet(tokensToGet);
        }

        [TestMethod]
        public void RummyLadder_GetTokenListInFinalExtreme2_SucceedWithoutSpareTokens()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 8), new Token(Color.Blue, 9), new Token(Color.Blue, 10),
                new Token(Color.Blue, 11), new Token(Color.Blue, 12), new Token(Color.Blue, 13)
            };

            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Blue, 11), new Token(Color.Blue, 12), new Token(Color.Blue, 13)
            };

            this.AssertTokensAreGet(tokensToGet);
        }

        [TestMethod]
        public void RummyLadder_GetTokenListInInitialExtreme1_SucceedWithSpareTokens()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 4), new Token(Color.Blue, 5), new Token(Color.Blue, 6),
                new Token(Color.Blue, 7), new Token(Color.Blue, 8), new Token(Color.Blue, 9),
                new Token(Color.Blue, 10), new Token(Color.Blue, 11), new Token(Color.Blue, 12)
            };

            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Blue, 5), new Token(Color.Blue, 6), new Token(Color.Blue, 7)
            };

            List<IToken> expectedSpareTokens = new List<IToken>
            {
                new Token(Color.Blue, 4)
            };

            this.AssertTokensAreGet(tokensToGet, expectedSpareTokens);
        }

        [TestMethod]
        public void RummyLadder_GetTokenListInInitialExtreme2_SucceedWithSpareTokens()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 4), new Token(Color.Blue, 5), new Token(Color.Blue, 6),
                new Token(Color.Blue, 7), new Token(Color.Blue, 8), new Token(Color.Blue, 9),
                new Token(Color.Blue, 10), new Token(Color.Blue, 11), new Token(Color.Blue, 12)
            };

            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Blue, 6), new Token(Color.Blue, 7), new Token(Color.Blue, 8)
            };

            List<IToken> expectedSpareTokens = new List<IToken>
            {
                new Token(Color.Blue, 4),
                new Token(Color.Blue, 5)
            };

            this.AssertTokensAreGet(tokensToGet, expectedSpareTokens);
        }

        [TestMethod]
        public void RummyLadder_GetTokenListInFinalExtreme1_SucceedWithSpareTokens()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 4), new Token(Color.Blue, 5), new Token(Color.Blue, 6),
                new Token(Color.Blue, 7), new Token(Color.Blue, 8), new Token(Color.Blue, 9),
                new Token(Color.Blue, 10), new Token(Color.Blue, 11), new Token(Color.Blue, 12)
            };

            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Blue, 9), new Token(Color.Blue, 10), new Token(Color.Blue, 11)
            };

            List<IToken> expectedSpareTokens = new List<IToken>
            {
                new Token(Color.Blue, 12)
            };

            this.AssertTokensAreGet(tokensToGet, expectedSpareTokens);
        }

        [TestMethod]
        public void RummyLadder_GetTokenListInFinalExtreme2_SucceedWithSpareTokens()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 4), new Token(Color.Blue, 5), new Token(Color.Blue, 6),
                new Token(Color.Blue, 7), new Token(Color.Blue, 8), new Token(Color.Blue, 9),
                new Token(Color.Blue, 10), new Token(Color.Blue, 11), new Token(Color.Blue, 12)
            };

            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Blue, 8), new Token(Color.Blue, 9), new Token(Color.Blue, 10)
            };

            List<IToken> expectedSpareTokens = new List<IToken>
            {
                new Token(Color.Blue, 11),
                new Token(Color.Blue, 12)
            };

            this.AssertTokensAreGet(tokensToGet, expectedSpareTokens);
        }

        [TestMethod]
        public void RummyLadder_GetTokenListInMiddle1_SucceedWithSpareTokens()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 4), new Token(Color.Blue, 5), new Token(Color.Blue, 6),
                new Token(Color.Blue, 7), new Token(Color.Blue, 8), new Token(Color.Blue, 9),
                new Token(Color.Blue, 10), new Token(Color.Blue, 11), new Token(Color.Blue, 12)
            };

            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Blue, 7), new Token(Color.Blue, 8), new Token(Color.Blue, 9)
            };

            List<IToken> expectedSpareTokens = new List<IToken>
            {
                new Token(Color.Blue, 10),
                new Token(Color.Blue, 11),
                new Token(Color.Blue, 12)
            };

            this.AssertTokensAreGet(tokensToGet, expectedSpareTokens);
        }

        [TestMethod]
        public void RummyLadder_GetTokenListInMiddle2_SucceedWithSpareTokens()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 4), new Token(Color.Blue, 5), new Token(Color.Blue, 6),
                new Token(Color.Blue, 7), new Token(Color.Blue, 8), new Token(Color.Blue, 9),
                new Token(Color.Blue, 10), new Token(Color.Blue, 11)
            };

            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Blue, 7), new Token(Color.Blue, 8)
            };

            List<IToken> expectedSpareTokens = new List<IToken>
            {
                new Token(Color.Blue, 9),
                new Token(Color.Blue, 10),
                new Token(Color.Blue, 11)
            };

            this.AssertTokensAreGet(tokensToGet, expectedSpareTokens);
        }

        #endregion

        private void AssertTokensCanBeAdded(object tokenToInsert, bool desiredResult)
        {
            // Ladder initialized with 4, 5, 6, 7, 8 (Blue).
            this.InitializeDummyTokensAsValidLadder();

            this.AssertTokensCanBeAdded(this.dummyTokens, tokenToInsert, desiredResult);
        }

        private void AssertTokensCanBeAdded(List<IToken> structureTokens, object tokenToInsert, bool desiredResult)
        {
            RummyLadder ladder = new RummyLadder(structureTokens);

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

            Assert.AreEqual(desiredResult, result.ActionIsPossible);
            Assert.AreEqual(0, result.SpareTokens.Count);
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
            AssertHelpers.AssertTokenListsContainsEquivalentElements(desiredResult.SpareTokens, result.SpareTokens);
        }

        private void AssertTokensAreAdded(object tokenToInsert, bool creationOperationIsExpected)
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

            List<IToken> referenceTokens = ladder.Tokens.Clone().ToList();
            referenceTokens.AddRange(tokensToInsert);

            List<IOperationResult> operationResults = ladder.Add(tokensToInsert);

            List<IToken> tokensToAssert = ladder.Tokens.Clone().ToList();

            if (creationOperationIsExpected)
            {
                Assert.AreEqual(1, operationResults.Count);
                Assert.AreEqual(StructureChanges.Created, operationResults.First().StructureChanges);
                tokensToAssert.AddRange(operationResults.First().Tokens);
            }

            AssertHelpers.AssertTokenListsContainsTheSameElements(tokensToAssert, referenceTokens);
        }

        private void AssertTokensAreGet(object tokenToGet)
        {
            this.AssertTokensAreGet(tokenToGet, new List<IToken>());
        }

        private void AssertTokensAreGet(object tokenToGet, List<IToken> expectedSpareTokens)
        {
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

            List<IToken> expectedModifiedTokens = ladder.Tokens.Clone().ToList();
            List<IToken> realExpectedGetTokens = new List<IToken>();
            List<IToken> realExpectedSpareTokens = new List<IToken>();
            
            foreach (IToken token in tokensToGet)
            {
                IToken tokenInStructure = expectedModifiedTokens.First(tok => tok.IsEquivalent(token));
                realExpectedGetTokens.Add(tokenInStructure);
                expectedModifiedTokens.Remove(tokenInStructure);
            }

            foreach (IToken token in expectedSpareTokens)
            {
                IToken tokenInStructure = expectedModifiedTokens.First(tok => tok.IsEquivalent(token));
                realExpectedSpareTokens.Add(tokenInStructure);
                expectedModifiedTokens.Remove(tokenInStructure);
            }

            // Execute Get().
            List<IOperationResult> operationResults = ladder.Get(tokensToGet);

            Assert.IsNotNull(operationResults);
            Assert.IsTrue(operationResults.Count > 0);
            Assert.AreEqual(StructureChanges.Retrieving, operationResults.First().StructureChanges);

            AssertHelpers.AssertTokenListsContainsTheSameElements(realExpectedGetTokens, operationResults.First().Tokens);
            AssertHelpers.AssertTokenListsContainsTheSameElements(expectedModifiedTokens, ladder.Tokens);

            if (expectedSpareTokens.Count > 0)
            {
                Assert.AreEqual(2, operationResults.Count);
                Assert.AreEqual(StructureChanges.SpareTokens, operationResults.Last().StructureChanges);
                AssertHelpers.AssertTokenListsContainsTheSameElements(realExpectedSpareTokens, operationResults.Last().Tokens);
            }
            else
            {
                Assert.AreEqual(1, operationResults.Count);
            }
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
