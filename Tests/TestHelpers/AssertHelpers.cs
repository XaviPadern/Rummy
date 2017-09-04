using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RummyEnvironment;
using RummyEnvironment.Helpers;

namespace TestHelpers
{
    public static class AssertHelpers
    {
        public static TException AssertThrows<TException>(Action action) where TException : Exception
        {
            try
            {
                action();
            }
            catch (TException ex)
            {
                return ex;
            }
            Assert.Fail("Expected exception was not thrown");

            return null;
        }

        public static void AssertTokensInListAreValid(List<IToken> tokenList)
        {
            Assert.IsTrue(tokenList.All(token => Token.IsTokenValid(token)));
        }

        // Exactly the same elements (Guid, Color, Number) in the same order.
        public static void AssertTokenListsAreTheSame(List<IToken> list1, List<IToken> list2)
        {
            Assert.IsTrue(TokenListsHelper.AreTokenListsExactlyTheSameWithSameOrder(list1, list2));
        }

        // Exactly the same elements (Guid, Color, Number) in different order.
        public static void AssertTokenListsContainsTheSameElements(List<IToken> list1, List<IToken> list2)
        {
            Assert.IsTrue(TokenListsHelper.AreTokenListsExactlyTheSameWithDifferentOrder(list1, list2));
        }

        // Equivalent elements (Color, Number) in different order.
        public static void AssertTokenListsContainsEquivalentElements(List<IToken> list1, List<IToken> list2)
        {
            Assert.IsTrue(TokenListsHelper.AreTokenListsEquivalentWithDifferentOrder(list1, list2));
        }
    }
}
