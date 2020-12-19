// ReSharper disable CheckNamespace

namespace System
{
    /// <summary>
    /// A collection of extensions for <see cref="string"/> class.
    /// </summary>
    public static class StringFractions
    {
        /// <summary>
        /// Convert fraction to double.
        /// </summary>
        /// <param name="fraction">The fraction.</param>
        /// <returns>The resulting double value.</returns>
        /// <exception cref="FormatException">
        /// Not a valid fraction.
        /// or
        /// Not a valid fraction.
        /// </exception>
        public static double FractionToDouble(this string fraction)
        {
            if (string.IsNullOrWhiteSpace(fraction))
            {
                return 0.0;
            }

            if (double.TryParse(fraction, out var result))
            {
                return result;
            }

            var split = fraction.Split(' ', '/');
            var has2Or3Parts = split.Length == 2 || split.Length == 3;
            var a = 0;
            var b = 0;
            var partsAreInt = split.Length >= 2 &&
                              int.TryParse(split[0], out a) && int.TryParse(split[1], out b);

            if (!has2Or3Parts || !partsAreInt)
            {
                throw new FormatException($"'{fraction}' is not a valid fraction.");
            }

            if (split.Length == 2)
            {
                return (double)a / b;
            }

            if (!int.TryParse(split[2], out var c))
            {
                throw new FormatException($"'{fraction}' is not a valid fraction.");
            }

            return a + ((double)b / c);
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
    }
}
