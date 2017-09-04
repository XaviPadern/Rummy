using System.Collections.Generic;
using System.Linq;

namespace RummyEnvironment.Helpers
{
    public static class TokenListsHelper
    {
        // Exactly the same elements (Guid, Color, Number) in the same order.
        public static bool AreTokenListsExactlyTheSameWithSameOrder(List<IToken> list1, List<IToken> list2)
        {
            if (!TokenListsComparisonBase(list1, list2))
            {
                return false;
            }

            IToken[] array1 = list1.ToArray();
            IToken[] array2 = list2.ToArray();

            for (int i = 0; i < array1.Length; i++)
            {
                if (!array1[i].IsEqual(array2[i]))
                {
                    return false;
                }
            }
            return true;
        }

        // Exactly the same elements (Guid, Color, Number) in different order.
        public static bool AreTokenListsExactlyTheSameWithDifferentOrder(List<IToken> list1, List<IToken> list2)
        {
            if (!TokenListsComparisonBase(list1, list2))
            {
                return false;
            }
            return list1.All(s => list2.Count(t => t.IsEqual(s)) == 1);
        }

        // Equivalent elements (Color, Number) in the same order.
        public static bool AreTokenListsEquivalentWithSameOrder(List<IToken> list1, List<IToken> list2)
        {
            if (!TokenListsComparisonBase(list1, list2))
            {
                return false;
            }

            IToken[] array1 = list1.ToArray();
            IToken[] array2 = list2.ToArray();

            for (int i = 0; i < array1.Length; i++)
            {
                if (!array1[i].IsEquivalent(array2[i]))
                {
                    return false;
                }
            }
            return true;
        }

        // Equivalent elements (Color, Number) in different order.
        public static bool AreTokenListsEquivalentWithDifferentOrder(List<IToken> list1, List<IToken> list2)
        {
            if (!TokenListsComparisonBase(list1, list2))
            {
                return false;
            }
            return list1.All(s => list2.Count(t => t.IsEquivalent(s)) == 1);
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
