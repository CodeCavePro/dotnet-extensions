// ReSharper disable CheckNamespace

using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace System
{
    /// <summary>
    /// A collection of extensions for <see cref="string"/> class.
    /// </summary>
    public static class StringContains
    {
        /// <summary>
        /// Determines whether [haystack contains] [the specified needle].
        /// </summary>
        /// <param name="haystack">The haystack.</param>
        /// <param name="needle">The needle.</param>
        /// <param name="options">The compare options.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified needle]; otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains(this string haystack, string needle, CompareOptions options, CultureInfo culture = null) => (culture ?? CultureInfo.InvariantCulture).CompareInfo.IndexOf(haystack, needle, options) >= 0;

        /// <summary>
        /// Determines whether [haystack contains] [the specified needle].
        /// </summary>
        /// <param name="haystack">The haystack.</param>
        /// <param name="needle">The needle.</param>
        /// <param name="comparison">The comparer to use.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified needle]; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// haystack
        /// or
        /// needle.
        /// </exception>
        public static bool Contains(this IEnumerable<string> haystack, string needle, StringComparison comparison)
        {
            if (haystack == null)
            {
                throw new ArgumentNullException(nameof(haystack));
            }

            if (needle == null)
            {
                throw new ArgumentNullException(nameof(needle));
            }

            return haystack.Any(str => needle.Equals(str, comparison));
        }

        /// <summary>
        /// Returns the index of the first occurrence of value in the current instance.
        /// The search starts at startIndex and runs thorough the next count characters.
        /// </summary>
        /// <param name="haystack">The haystack.</param>
        /// <param name="needle">The needle.</param>
        /// <param name="comparison">The comparer to use.</param>
        /// <param name="startIndex">The starting index.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// haystack
        /// or
        /// startIndex.
        /// </exception>
        /// <exception cref="ArgumentException">needle.</exception>
        public static int IndexOf(this IEnumerable<string> haystack, string needle, StringComparison comparison, int startIndex = 0)
        {
            if (haystack == null)
            {
                throw new ArgumentNullException(nameof(haystack));
            }

            if (needle is null)
            {
                throw new ArgumentNullException(nameof(needle));
            }

            var haystackArray = (haystack is ICollection<string> haystackCollection)
                ? haystackCollection
                : haystack.ToArray();

            if (haystackArray.Count <= startIndex)
            {
                throw new ArgumentNullException(nameof(startIndex));
            }

            for (var i = startIndex; i < haystackArray.Count; i++)
            {
                if (haystackArray.ElementAt(i).Equals(needle, comparison))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
