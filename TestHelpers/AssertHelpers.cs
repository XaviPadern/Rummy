using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RummyEnvironment;

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

        public static void AssertTokenListsAreTheSame(List<IToken> list1, List<IToken> list2)
        {
            Assert.IsTrue(TokenListsComparisonBase(list1, list2));
            
            IToken[] array1 = list1.ToArray();
            IToken[] array2 = list2.ToArray();

            for (int i = 0; i < array1.Length; i++)
            {
                Assert.IsTrue(array1[i].IsEqual(array2[i]));
            }
        }

        public static void AssertTokenListsContainsTheSameElements(List<IToken> list1, List<IToken> list2)
        {
            Assert.IsTrue(TokenListsComparisonBase(list1, list2));
            Assert.IsTrue(list1.All(s => list2.Count(t => t.IsEqual(s)) == 1));
        }

        private static bool TokenListsComparisonBase(List<IToken> list1, List<IToken> list2)
        {
            if ((list1 == null) != (list2 == null))
            {
                return false;
            }

            if (list1 == null)
            {
                return true;
            }

            return list1.Count == list2.Count;
        }
    }
}
