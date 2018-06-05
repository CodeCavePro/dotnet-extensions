// ReSharper disable CheckNamespace

namespace System.Collections.Generic
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="T:System.Collections.Generic.IEqualityComparer`1" />
    public class LambdaComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _expression;

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaComparer{T}"/> class.
        /// </summary>
        /// <param name="lambda">The lambda.</param>
        public LambdaComparer(Func<T, T, bool> lambda)
        {
            _expression = lambda;
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
        public bool Equals(T x, T y)
        {
            return _expression(x, y);
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public int GetHashCode(T obj)
        {
            /*
             If you just return 0 for the hash the Equals comparer will kick in. 
             The underlying evaluation checks the hash and then short circuits the evaluation if it is false.
             Otherwise, it checks the Equals. If you force the hash to be true (by assuming 0 for both objects), 
             you will always fall through to the Equals check which is what we are always going for.
            */
            return 0;
        }
    }
}
