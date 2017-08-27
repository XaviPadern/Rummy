using System;
using System.Collections.Generic;

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
    }
}
