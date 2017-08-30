using System;
using System.Collections.Generic;
using System.Linq;

namespace RummyEnvironment.Helpers
{
    public static class ExtensionMethods
    {
        private static Random range = new Random();

        // Extension method based on the Fisher-Yates shuffle.
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = range.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
