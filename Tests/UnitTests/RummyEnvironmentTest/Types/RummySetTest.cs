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
    public class RummySetTest
    {
        private List<IToken> dummyTokens;

        #region Constructor

        [TestMethod]
        public void RummySet_NewStructureWithValidTokenStructure_Created()
        {
            // Set of number 4, size 3 initialized with (Blue, Black, Yellow).
            this.InitializeDummyTokensAsValidSetOf3();

            RummySet set = new RummySet(this.dummyTokens);

            Guid outId;

            Assert.IsTrue(Guid.TryParse(set.Id.ToString(), out outId));
            AssertHelpers.AssertTokenListsAreTheSame(this.dummyTokens, set.Tokens);
        }

        [TestMethod]
        public void RummySet_NewStructureWithOnlySingleToken_ExceptionThrown()
        {
            this.InitializeDummyTokensAsSingleToken();

            AssertHelpers.AssertThrows<RummyException>(() =>
                new RummySet(this.dummyTokens));
        }

        [TestMethod]
        public void RummySet_NewStructureWithOnlyTwoTokens_ExceptionThrown()
        {
            this.InitializeDummyTokensAsOnlyTwoTokens();

            AssertHelpers.AssertThrows<RummyException>(() =>
                new RummySet(this.dummyTokens));
        }

        [TestMethod]
        public void RummySet_NewStructureWithDifferentNumbers_ExceptionThrown()
        {
            this.InitializeDummyTokensAsDifferentNumbersSet();

            AssertHelpers.AssertThrows<RummyException>(() =>
                new RummySet(this.dummyTokens));
        }

        [TestMethod]
        public void RummySet_NewStructureWithRepeatedColors_ExceptionThrown()
        {
            this.InitializeDummyTokensAsRepeatedColorsSet();

            AssertHelpers.AssertThrows<RummyException>(() =>
                new RummySet(this.dummyTokens));
        }

        #endregion

        #region Modify

        [TestMethod]
        public void RummySet_ModifyWithOnlySingleToken_ExceptionThrown()
        {
            // Set of number 4, size 3 initialized with (Blue, Black, Yellow).
            this.InitializeDummyTokensAsValidSetOf3();
            RummySet set = new RummySet(this.dummyTokens);

            this.InitializeDummyTokensAsSingleToken();

            AssertHelpers.AssertThrows<RummyException>(() =>
                set.Modify(this.dummyTokens));
        }

        [TestMethod]
        public void RummySet_ModifyWithOnlyTwoTokens_ExceptionThrown()
        {
            // Set of number 4, size 3 initialized with (Blue, Black, Yellow).
            this.InitializeDummyTokensAsValidSetOf3();
            RummySet set = new RummySet(this.dummyTokens);

            this.InitializeDummyTokensAsOnlyTwoTokens();

            AssertHelpers.AssertThrows<RummyException>(() =>
                set.Modify(this.dummyTokens));
        }

        [TestMethod]
        public void RummySet_ModifyWithDifferentNumbers_ExceptionThrown()
        {
            // Set of number 4, size 3 initialized with (Blue, Black, Yellow).
            this.InitializeDummyTokensAsValidSetOf3();
            RummySet set = new RummySet(this.dummyTokens);

            this.InitializeDummyTokensAsDifferentNumbersSet();

            AssertHelpers.AssertThrows<RummyException>(() =>
                set.Modify(this.dummyTokens));
        }

        [TestMethod]
        public void RummySet_ModifyWithRepeatedColors_ExceptionThrown()
        {
            // Set of number 4, size 3 initialized with (Blue, Black, Yellow).
            this.InitializeDummyTokensAsValidSetOf3();
            RummySet set = new RummySet(this.dummyTokens);

            this.InitializeDummyTokensAsRepeatedColorsSet();

            AssertHelpers.AssertThrows<RummyException>(() =>
                set.Modify(this.dummyTokens));
        }

        [TestMethod]
        public void RummySet_ModifyWithValidTokenStructure_Created()
        {
            // Set of number 4, size 3 initialized with (Blue, Black, Yellow).
            this.InitializeDummyTokensAsValidSetOf3();

            RummySet set = new RummySet(this.dummyTokens);

            this.InitializeDummyTokensAsValidSetOf4();
            set.Modify(this.dummyTokens);

            Guid outId;

            Assert.IsTrue(Guid.TryParse(set.Id.ToString(), out outId));
            AssertHelpers.AssertTokenListsAreTheSame(this.dummyTokens, set.Tokens);
        }

        #endregion

        #region CanAdd

        [TestMethod]
        public void RummySet_CanAddMoreThanOneToken_NotPossible()
        {
            List<IToken> tokensToAdd = new List<IToken>
            {
                new Token(Color.Red, 4),
                new Token(Color.Blue, 4)
            };

            // Set of number 4, size 3 initialized with (Blue, Black, Yellow).
            this.AssertTokenCanBeAdded(tokensToAdd, false);
        }

        [TestMethod]
        public void RummySet_CanAddColorRepeated_NotPossible()
        {
            // Set of number 4, size 3 initialized with (Blue, Black, Yellow).
            this.AssertTokenCanBeAdded(new Token(Color.Blue, 4), false);
        }

        [TestMethod]
        public void RummySet_CanAddNotTheSameNumber_NotPossible()
        {
            // Set of number 4, size 3 initialized with (Blue, Black, Yellow).
            this.AssertTokenCanBeAdded(new Token(Color.Red, 5), false);
        }

        [TestMethod]
        public void RummySet_CanAddNewValidToken_IsPossible()
        {
            // Set of number 4, size 3 initialized with (Blue, Black, Yellow).
            this.AssertTokenCanBeAdded(new Token(Color.Red, 4), true);
        }
        
        #endregion
        
        #region Add

        [TestMethod]
        public void RummySet_AddMoreThanOneToken_ExceptionThrown()
        {
            // Set of number 4, size 3 initialized with (Blue, Black, Yellow).
            this.InitializeDummyTokensAsValidSetOf3();
            RummySet set = new RummySet(this.dummyTokens);

            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Red, 4),
                new Token(Color.Yellow, 4)
            };

            AssertHelpers.AssertThrows<RummyException>(() => set.Add(tokensToInsert));
        }

        [TestMethod]
        public void RummySet_AddNotPossibleBecauseDifferentNumber_ExceptionThrown()
        {
            // Set of number 4, size 3 initialized with (Blue, Black, Yellow).
            this.InitializeDummyTokensAsValidSetOf3();
            RummySet set = new RummySet(this.dummyTokens);

            List<IToken> tokensToInsert = new List<IToken>()
            {
                new Token(Color.Red, 5)
            };

            AssertHelpers.AssertThrows<RummyException>(() => set.Add(tokensToInsert));
        }

        [TestMethod]
        public void RummySet_AddNotPossibleBecauseColorExists_ExceptionThrown()
        {
            // Set of number 4, size 3 initialized with (Blue, Black, Yellow).
            this.InitializeDummyTokensAsValidSetOf3();
            RummySet set = new RummySet(this.dummyTokens);

            List<IToken> tokensToInsert = new List<IToken>()
            {
                new Token(Color.Blue, 4)
            };

            AssertHelpers.AssertThrows<RummyException>(() => set.Add(tokensToInsert));
        }

        [TestMethod]
        public void RummySet_AddNotPossibleBecauseSetIsFull_ExceptionThrown()
        {
            // Set of number 4, size 3 initialized with (Blue, Black, Yellow).
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 4),
                new Token(Color.Black, 4),
                new Token(Color.Yellow, 4),
                new Token(Color.Red, 4),
            };
            RummySet set = new RummySet(this.dummyTokens);

            List<IToken> tokensToInsert = new List<IToken>
            {
                new Token(Color.Blue, 4)
            };

            AssertHelpers.AssertThrows<RummyException>(() => set.Add(tokensToInsert));
        }

        [TestMethod]
        public void RummySet_AddValidInexistentToken_Succeeds()
        {
            // Set of number 4, size 3 initialized with (Blue, Black, Yellow).
            this.AssertTokenIsAdded(new Token(Color.Red, 4));
        }

        #endregion

        #region CanGet
        
        [TestMethod]
        public void RummySet_CanGetNotSingleToken_NotPossible()
        {
            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Blue, 4),
                new Token(Color.Black, 4)
            };

            // Set of number 4, size 4 initialized with (Blue, Black, Yellow, Red).
            this.AssertTokensCanBeGet(tokensToGet, false);
        }

        [TestMethod]
        public void RummySet_CanGetOutOfLimits_NotPossible()
        {
            // Set of number 4, size 4 initialized with (Blue, Black, Yellow, Red).
            this.AssertTokensCanBeGet(new Token(Color.Blue, 3), false);
        }

        [TestMethod]
        public void RummySet_CanGetInInitialExtreme_IsPossible()
        {
            // Set of number 4, size 4 initialized with (Blue, Black, Yellow, Red).
            this.AssertTokensCanBeGet(new Token(Color.Blue, 4), true);
        }

        [TestMethod]
        public void RummySet_CanGetInFinalExtreme_IsPossible()
        {
            // Set of number 4, size 4 initialized with (Blue, Black, Yellow, Red).
            this.AssertTokensCanBeGet(new Token(Color.Red, 4), true);
        }

        #endregion

        #region Get

        [TestMethod]
        public void RummySet_GetNotSingleToken_ExceptionThrown()
        {
            // Set of number 4, size 3 initialized with (Blue, Black, Yellow).
            this.InitializeDummyTokensAsValidSetOf3();
            RummySet set = new RummySet(this.dummyTokens);

            List<IToken> tokensToGet = new List<IToken>
            {
                new Token(Color.Blue, 4),
                new Token(Color.Black, 4)
            };

            AssertHelpers.AssertThrows<RummyException>(() => set.Get(tokensToGet));
        }

        [TestMethod]
        public void RummySet_GetNotPossible_ExceptionThrown()
        {
            // Set of number 4, size 3 initialized with (Blue, Black, Yellow).
            this.InitializeDummyTokensAsValidSetOf3();
            RummySet set = new RummySet(this.dummyTokens);

            AssertHelpers.AssertThrows<RummyException>(() => set.Get(new Token(Color.Blue, 3)));
        }

        [TestMethod]
        public void RummySet_GetFromInitialExtreme_Succeeds()
        {
            // Set of number 4, size 4 initialized with (Blue, Black, Yellow, Red).
            this.InitializeDummyTokensAsValidSetOf4();
            
            this.AssertTokenIsGet(new Token(Color.Blue, 4));         
        }

        [TestMethod]
        public void RummySet_GetFromFinalExtreme_Succeeds()
        {
            // Set of number 4, size 4 initialized with (Blue, Black, Yellow, Red).
            this.InitializeDummyTokensAsValidSetOf4();

            this.AssertTokenIsGet(new Token(Color.Red, 4));
        }

        #endregion

        private void AssertTokenCanBeAdded(object tokenToInsert, bool desiredResult)
        {
            // Set of number 4, size 3 initialized with (Blue, Black, Yellow).
            this.InitializeDummyTokensAsValidSetOf3();
            RummySet set = new RummySet(this.dummyTokens);

            IActionResult result;
            if (tokenToInsert.GetType() == typeof(Token))
            {
                result = set.CanAdd((IToken)tokenToInsert);
            }
            else
            {
                result = set.CanAdd((List<IToken>)tokenToInsert);
            }

            Assert.AreEqual(desiredResult, result.ActionIsPossible);
            Assert.IsFalse(result.NeededExtraTokens.Any());
        }
        
        private void AssertTokensCanBeGet(object tokenToGet, bool desiredResult)
        {
            // Set of number 4, size 3 initialized with (Blue, Black, Yellow).
            this.InitializeDummyTokensAsValidSetOf4();
            RummySet set = new RummySet(this.dummyTokens);

            IActionResult result;
            if (tokenToGet.GetType() == typeof(Token))
            {
                result = set.CanGet((IToken)tokenToGet);
            }
            else
            {
                result = set.CanGet((List<IToken>)tokenToGet);
            }

            Assert.AreEqual(desiredResult, result.ActionIsPossible);
            Assert.IsFalse(result.NeededExtraTokens.Any());
        }

        private void AssertTokenIsAdded(IToken tokenToInsert)
        {
            // Set of number 4, size 3 initialized with (Blue, Black, Yellow).
            this.InitializeDummyTokensAsValidSetOf3();
            List<IToken> desiredStructureTokens = (List<IToken>)this.dummyTokens.Clone();
            desiredStructureTokens.Add((IToken)tokenToInsert.Clone());

            RummySet set = new RummySet(this.dummyTokens);

            List<IOperationResult> operationResults = set.Add(tokenToInsert);

            Assert.IsNotNull(operationResults);
            Assert.AreEqual(0, operationResults.Count);
            AssertHelpers.AssertTokenListsAreTheSame(desiredStructureTokens, set.Tokens);
        }

        private void AssertTokenIsGet(IToken tokenToGet)
        {
            // Set of number 4, size 3 initialized with (Blue, Black, Yellow, Red).
            this.InitializeDummyTokensAsValidSetOf4();
            List<IToken> desiredStructureTokens = (List<IToken>)this.dummyTokens.Clone();
            desiredStructureTokens.Remove(desiredStructureTokens.Last());
            IToken desiredGetToken = (IToken)tokenToGet.Clone();

            RummySet set = new RummySet(this.dummyTokens);

            List<IOperationResult> operationResults = set.Get(tokenToGet);

            Assert.IsNotNull(operationResults);
            Assert.AreEqual(1, operationResults.Count);

            Assert.AreEqual(StructureChanges.Retrieving, operationResults.First().StructureChanges);
            AssertHelpers.AssertTokenListsAreTheSame(new List<IToken> { desiredGetToken }, operationResults.First().Tokens);

            AssertHelpers.AssertTokenListsAreTheSame(desiredStructureTokens, set.Tokens);
        }

        private void InitializeDummyTokensAsValidSetOf3()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 4),
                new Token(Color.Black, 4),
                new Token(Color.Yellow, 4)
            };
        }

        private void InitializeDummyTokensAsValidSetOf4()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 4),
                new Token(Color.Black, 4),
                new Token(Color.Yellow, 4),
                new Token(Color.Red, 4)
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

        private void InitializeDummyTokensAsDifferentNumbersSet()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 4),
                new Token(Color.Black, 5),
                new Token(Color.Yellow, 4)
            };
        }

        private void InitializeDummyTokensAsRepeatedColorsSet()
        {
            this.dummyTokens = new List<IToken>
            {
                new Token(Color.Blue, 4),
                new Token(Color.Black, 4),
                new Token(Color.Blue, 4)
            };
        }
    }
}
