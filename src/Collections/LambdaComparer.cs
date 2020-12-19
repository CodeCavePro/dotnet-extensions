// ReSharper disable CheckNamespace

namespace System.Collections.Generic
{
    /// <inheritdoc />
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="IEqualityComparer{T}" />
    public class LambdaComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> expression;

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaComparer{T}"/> class.
        /// </summary>
        /// <param name="lambda">The lambda.</param>
        public LambdaComparer(Func<T, T, bool> lambda)
        {
            expression = lambda;
        }

        /// <inheritdoc />
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type T to compare.</param>
        /// <param name="y">The second object of type T to compare.</param>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        public bool Equals(T x, T y) => expression(x, y);

        /// <inheritdoc />
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public int GetHashCode(T obj) => 0;
    }
}
