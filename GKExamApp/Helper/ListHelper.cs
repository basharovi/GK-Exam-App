using System;
using System.Collections.Generic;
using System.Linq;

namespace GKExamApp.Helper
{
    public static class EnumerableHelper<E>
    {
        private static Random random;

        static EnumerableHelper()
        {
            random = new Random();
        }

        public static T Random<T>(IEnumerable<T> input)
        {
            return input.ElementAt(random.Next(input.Count()));
        }

    }

    public static class EnumerableExtensions
    {
        public static T NextRandom<T>(this IEnumerable<T> input)
        {
            return EnumerableHelper<T>.Random(input);
        }
    }
}
