#region

using System.Collections.Generic;

#endregion

// ReSharper disable CheckNamespace

namespace System.Collections
{
    /// <inheritdoc cref="HashSet{T}" />
    /// <inheritdoc cref="ICollection{T}" />
    /// <summary>
    /// Providers a functionality similar to HashSet, except it panics whenever a duplicate is added
    /// </summary>
    public class HashSetUnique<T> : HashSet<T>, ICollection<T>
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets a value indicating whether the <see cref="T:ICollection`1"></see> is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <inheritdoc />
        /// <summary>
        /// Adds an item to the <see cref="T:ICollection`1"></see>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:ICollection`1"></see>.</param>
        public new virtual void Add(T item)
        {
            this.AddOrThrow(item);
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Tries the add.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public bool TryAdd(T item)
        {
            try
            {
                Add(item);
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
    }
}
