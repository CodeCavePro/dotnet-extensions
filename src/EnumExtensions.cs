using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#if NETSTANDARD1_6
using System.Reflection;
#endif

// ReSharper disable CheckNamespace
namespace System
{
    /// <summary>
    /// Extensions for <see cref="Enum"/> class.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the description of a given enumeration value.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enumeration.</typeparam>
        /// <param name="enumerationValue">The enumeration value.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">enumerationValue - enumerationValue.</exception>
        public static string GetDescription<TEnum>(this TEnum enumerationValue)
            where TEnum : struct
        {
#if NETSTANDARD1_6
            var field = enumerationValue.GetType().GetRuntimeField(enumerationValue.ToString());
            var attributes = field.GetCustomAttributes(false)?.ToArray();
#else
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException(
                    $"{nameof(enumerationValue)} must be an enumeration",
                    nameof(enumerationValue));
            }

            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (!memberInfo.Any())
            {
                return enumerationValue.ToString();
            }

            var attributes = memberInfo.ElementAt(0)?.GetCustomAttributes(typeof(DescriptionAttribute), false);
#endif

            return attributes != null && attributes.Any()
                ? ((DescriptionAttribute)attributes.ElementAt(0)).Description
                : enumerationValue.ToString();
        }

        /// <summary>
        /// Gets the enumeration value from a description string.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enumeration.</typeparam>
        /// <param name="description">The description.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns></returns>
        public static TEnum GetEnumFromDescription<TEnum>(
            this string description,
#if NETSTANDARD1_6
            StringComparison comparison = StringComparison.OrdinalIgnoreCase)
#else
            StringComparison comparison = StringComparison.InvariantCulture)
#endif
            where TEnum : struct
        {
            var array = Enum.GetValues(typeof(TEnum));
            var list = new List<TEnum>(array.Length);
            list.AddRange(array.Cast<object>().Select((t, i) => (TEnum)array.GetValue(i)));
            return list.FirstOrDefault(e => e.GetDescription().Equals(description, comparison));
        }

        /// <summary>
        /// Parses the enumeration string representation.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enumeration.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">TEnum.</exception>
        public static TEnum ParseEnum<TEnum>(this string value)
            where TEnum : struct, IConvertible => string.IsNullOrWhiteSpace(value)
                ? default
                : (TEnum)Enum.Parse(typeof(TEnum), value, true);
    }
}
