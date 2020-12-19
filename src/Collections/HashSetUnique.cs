// ReSharper disable CheckNamespace

using System.Collections.Generic;

namespace System.Collections
{
    /// <inheritdoc cref="HashSet{T}" />
    /// <inheritdoc cref="ICollection{T}" />
    /// <summary>
    /// Providers a functionality similar to HashSet, except it panics whenever a duplicate is added.
    /// </summary>
    [Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3925:\"ISerializable\" should be implemented correctly", Justification = "Not my problem :)")]
    public class HashSetUnique<T> : HashSet<T>, ICollection<T>
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets a value indicating whether the <see cref="ICollection{T}"></see> is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <inheritdoc />
        /// <summary>
        /// Adds an item to the <see cref="ICollection{T}"></see>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="ICollection{T}"></see>.</param>
        public new virtual void Add(T item)
        {
            this.AddOrThrow(item);
        }

        /// <summary>
        /// Tries the add.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>[true if item was added successfully].</returns>
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

        /// <inheritdoc />
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
