// ReSharper disable CheckNamespace

using System.Linq;

namespace System
{
    /// <summary>
    /// A collection of extensions for <see cref="string"/> class.
    /// </summary>
    public static class StringCase
    {
        /// <summary>
        /// Convert a given string to sentence case.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Input cannot be null or whitespace.</exception>
        public static string ToSentenceCase(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException($"'{nameof(input)}' cannot be null or whitespace", nameof(input));
            }

            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        /// <summary>
        /// Converts string to constant case.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Input cannot be null or whitespace.</exception>
        public static string ToConstantCase(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException($"'{nameof(input)}' cannot be null or whitespace", nameof(input));
            }

            const string separators = @"- []{}<>~`+=,.;:/?|!@#$%^&*()";

            input = separators.Aggregate(input, (current, separator) => current.Replace(separator, '_'));
            return input.ToUpperInvariant();
        }
    }
}
