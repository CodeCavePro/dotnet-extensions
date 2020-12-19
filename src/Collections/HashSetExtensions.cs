// ReSharper disable CheckNamespace

using System.Collections.Generic;

namespace System.Collections
{
    /// <summary>
    /// A collection of extensions for <see cref="HashSet{T}"/> class.
    /// </summary>
    public static class HashSetExtensions
    {
        /// <summary>
        /// Adds the item to a HashSet in it's not already inside, otherwise throws an exception.
        /// </summary>
        /// <typeparam name="T">Type of the item.</typeparam>
        /// <param name="hashSet">The hash set.</param>
        /// <param name="item">The item to add.</param>
        /// <exception cref="InvalidOperationException">Duplicates are not allowed in this list.</exception>
        public static void AddOrThrow<T>(this HashSet<T> hashSet, T item)
        {
            if (hashSet is null)
            {
                throw new ArgumentNullException(nameof(hashSet));
            }

            if (hashSet.Add(item))
            {
                return;
            }

            throw new InvalidOperationException("Duplicates are not allowed in this list");
        }
    }
}
