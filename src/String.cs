#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#endregion

// ReSharper disable CheckNamespace

namespace System
{
    /// <summary>
    /// A collection of extensions for <see cref="string"/> class
    /// </summary>
    public static class StringExtensions
    {
        #region Fraction

        /// <summary>
        /// Convert fraction to double.
        /// </summary>
        /// <param name="fraction">The fraction.</param>
        /// <returns>The resulting double value</returns>
        /// <exception cref="FormatException">
        /// Not a valid fraction.
        /// or
        /// Not a valid fraction.
        /// </exception>
        public static double FractionToDouble(this string fraction)
        {
            if (string.IsNullOrWhiteSpace(fraction))
                return 0.0;

            if (double.TryParse(fraction, out var result))
            {
                return result;
            }

            var split = fraction.Split(' ', '/');
            var has2Or3Parts = split.Length == 2 || split.Length == 3;
            // ReSharper disable once InlineOutVariableDeclaration
            int a = 0, // ReSharper disable once InlineOutVariableDeclaration
                b = 0;
            var partsAreInt = split.Length >= 2 &&
                              int.TryParse(split[0], out a) && int.TryParse(split[1], out b);

            if (!has2Or3Parts || !partsAreInt)
            {
                throw new FormatException("Not a valid fraction.");
            }

            if (split.Length == 2)
            {
                return (double)a / b;
            }

            if (!int.TryParse(split[2], out var c))
            {
                throw new FormatException("Not a valid fraction.");
            }
            return a + (double)b / c;
        }

        /// <summary>
        /// Tries to convert fraction to double.
        /// </summary>
        /// <param name="fraction">The fraction.</param>
        /// <param name="result">The resulting double value.</param>
        /// <returns></returns>
        public static bool TryFractionToDouble(this string fraction, out double result)
        {
            try
            {
                result = fraction.FractionToDouble();
                return true;
            }
            catch (FormatException)
            {
                result = 0.0;
                return false;
            }
        }

        #endregion Fraction

        #region Case

        /// <summary>
        /// Convert a given string to sentence case.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string ToSentenceCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) throw new ArgumentException();

            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        /// <summary>
        /// Converts string to constant case.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string ToConstantCase(this string input)
        {
            const string separators = @"- []{}<>~`+=,.;:/?|!@#$%^&*()";
            if (string.IsNullOrEmpty(input)) throw new ArgumentException();

            input = separators.Aggregate(input, (current, separator) => current.Replace(separator, '_'));
            return input.ToUpperInvariant();
        }

        #endregion Case

        #region Contains

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
        public static bool Contains(this string haystack, string needle, CompareOptions options, CultureInfo culture = null)
        {
            return (culture ?? CultureInfo.InvariantCulture).CompareInfo.IndexOf(haystack, needle, options) >= 0;
        }

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
        /// needle
        /// </exception>
        public static bool Contains(this IEnumerable<string> haystack, string needle, StringComparison comparison)
        {
            if (null == haystack) throw new ArgumentNullException(nameof(haystack));
            if (null == needle) throw new ArgumentNullException(nameof(needle));

            return haystack.ToList().Any(str => needle.Equals(str, comparison));
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
        /// startIndex
        /// </exception>
        /// <exception cref="ArgumentException">needle</exception>
        public static int IndexOf(this IEnumerable<string> haystack, string needle, StringComparison comparison, int startIndex = 0)
        {
            if (null == haystack) throw new ArgumentNullException(nameof(haystack));
            if (null == needle) throw new ArgumentException(nameof(needle));

            if (!(haystack is IList<string> enumerable))
                return -1;

            if (enumerable.Count <= startIndex)
                throw new ArgumentNullException(nameof(startIndex));

            for (var i = startIndex; i < enumerable.Count(); i++)
            {
                if (enumerable[i].Equals(needle, comparison))
                    return i;
            }

            return -1;
        }

        #endregion Contains
    }
}
