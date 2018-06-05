#region

using System.Collections.Generic;

#endregion

// ReSharper disable CheckNamespace

namespace System.Collections
{
    /// <summary>
    /// A collection of extensions for <see cref="T:System.Collections.HashSet`1"/> class
    /// </summary>
    public static class HashSetExtensions
    {
        /// <summary>
        /// Adds the item to a HashSet in it's not already inside, otherwise throws an exception.
        /// </summary>
        /// <typeparam name="T">Type of the item</typeparam>
        /// <param name="hashSet">The hash set.</param>
        /// <param name="item">The item to add.</param>
        /// <exception cref="InvalidOperationException">Duplicates are not allowed in this list</exception>
        public static void AddOrThrow<T>(this HashSet<T> hashSet, T item)
        {
            if (!hashSet.Add(item))
            {
                throw new InvalidOperationException("Duplicates are not allowed in this list");
            }
        }
    }
}
