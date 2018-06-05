// ReSharper disable once CheckNamespace

namespace System
{
    public static class DoubleExtensions
    {
        #region Weight unit conversions

        /// <summary>
        /// Converts kilograms to pounds.
        /// </summary>
        /// <param name="kilograms">The kilograms to convert.</param>
        /// <returns></returns>
        public static double KilogramsToPounds(this double kilograms) { return kilograms * 2.2046226218D; }

        /// <summary>
        /// Converts pounds to kilograms.
        /// </summary>
        /// <param name="pounds">The pounds to convert.</param>
        /// <returns></returns>
        public static double PoundsToKg(this double pounds) { return pounds * 0.45359237D; }

        #endregion

        #region Length unit conversions

        /// <summary>
        /// Converts millimeters to inches.
        /// </summary>
        /// <param name="millimeters">The millimeters to convert.</param>
        /// <returns></returns>
        public static double MillimetersToInches(this double millimeters) { return millimeters * 0.03937007874D; }

        /// <summary>
        /// Converts inches to millimeters.
        /// </summary>
        /// <param name="inches">The inches to convert.</param>
        /// <returns></returns>
        public static double InchesToMillimeters(this double inches) { return inches * 25.4D; }

        /// <summary>
        /// Converts millimeters to feet.
        /// </summary>
        /// <param name="millimeters">The millimeters.</param>
        /// <returns></returns>
        public static double MillimetersToFeet(this double millimeters) { return millimeters / 304.8D; }

        /// <summary>
        /// Converts feet to millimeters.
        /// </summary>
        /// <param name="feet">The amount of feet to convert.</param>
        /// <returns></returns>
        public static double FeetToMillimeters(this double feet) { return feet * 304.8D; }

        #endregion

        #region Angle unit conversions

        /// <summary>
        /// Converts decimal angle degrees to radiants.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        /// <returns></returns>
        public static double DegreeToRadiants(this double degrees) { return degrees * (Math.PI / 180); }

        /// <summary>
        /// Converts radiants to decimal angle degrees.
        /// </summary>
        /// <param name="radiants">The radiants.</param>
        /// <returns></returns>
        public static double RadiansToDegree(this double radiants) { return radiants * (180 / Math.PI); }

        #endregion
    }
}