#region

using System.Linq;

#endregion

// ReSharper disable CheckNamespace

namespace System.Collections.Generic
{
    /// <summary>
    /// Extensions for <see cref="IEnumerable"/> class
    /// </summary>
    /// ReSharper disable once InconsistentNaming
    public static class IEnumerableExtensions
    {
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

        /// <summary>
        /// Shuffles (arranges in random order) items of an enumeration.
        /// </summary>
        /// <typeparam name="T">Type of items inside the enumeration</typeparam>
        /// <param name="enumeration">The enumeration.</param>
        /// <returns></returns>
        public static IEnumerable<T> OrderByRand<T>(this IEnumerable<T> enumeration)
        {
            return enumeration?.OrderBy(s => Guid.NewGuid());
        }
    }
}
