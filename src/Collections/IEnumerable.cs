#region

using System.Linq;

#endregion

// ReSharper disable CheckNamespace

namespace System.Collections.Generic
{
    /// <summary>
    /// Extensions for <see cref="IEnumerable"/> class
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Shuffles the specified seed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="seed">The seed.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random seed)
        {
            if (source == null)
                throw new ArgumentNullException();

            var elements = source.ToArray();
            for (var i = elements.Length - 1; i >= 0; i--)
            {
                // Swap element "i" with a random earlier element it (or itself)
                // ... except we don't really need to swap it fully, as we can
                // return it immediately, and afterwards it's irrelevant.
                var swapIndex = seed.Next(i + 1);
                yield return elements[swapIndex];
                elements[swapIndex] = elements[i];
            }
        }

        /// <summary>
        /// Returns all items in the first collection except the ones in the second collection that match the lambda condition
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="listA">The first list</param>
        /// <param name="listB">The second list</param>
        /// <param name="lambda">The filter expression</param>
        /// <returns>The filtered list</returns>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> listA, IEnumerable<T> listB, Func<T, T, bool> lambda)
        {
            return listA.Except(listB, new LambdaComparer<T>(lambda));
        }

        /// <summary>
        /// Returns all items in the first collection that intersect the ones in the second collection that match the lambda condition
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="listA">The first list</param>
        /// <param name="listB">The second list</param>
        /// <param name="lambda">The filter expression</param>
        /// <returns>The filtered list</returns>
        public static IEnumerable<T> Intersect<T>(this IEnumerable<T> listA, IEnumerable<T> listB,
            Func<T, T, bool> lambda)
        {
            return listA.Intersect(listB, new LambdaComparer<T>(lambda));
        }
    }
}
