using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace

namespace System.Collections
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Determines whether the specified dictionary contains the given key.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="haystack">The dictionary to look into.</param>
        /// <param name="key">The key to look for.</param>
        /// <param name="comparer">The comparer to use.</param>
        /// <returns>
        ///   <c>true</c> if the specified key contains key; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsKey<TValue>(this IDictionary<string, TValue> haystack, string key, StringComparison comparer)
        {
            return haystack.Keys.Contains(key, comparer);
        }

        /// <summary>
        /// Merges the specified dictionary with given one and replaces the original content.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="haystack">The dictionary to look into.</param>
        /// <param name="other">The other.</param>
        public static Dictionary<TKey, TValue> Contact<TKey, TValue>(this Dictionary<TKey, TValue> haystack, Dictionary<TKey, TValue> other)
        {
            return haystack.ContactLeft<Dictionary<TKey, TValue>, TKey, TValue>(other);

        }

        /// <summary>
        /// Merges the specified dictionary with given one and returns it as a result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="haystack">The dictionary to look into.</param>
        /// <param name="others">The other dictionaries to merge with.</param>
        /// <returns></returns>
        public static TResult Contact<TResult, TKey, TValue>(this TResult haystack, params TResult[] others)
            where TResult : IDictionary<TKey, TValue>, new()
        {
            return haystack.ContactLeft<TResult, TKey, TValue>(others);
        }

        /// <summary>
        /// Merges the left.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="haystack">The dictionary to look into.</param>
        /// <param name="others">The other dictionaries to merge with.</param>
        /// <returns></returns>
        public static TResult ContactLeft<TResult, TKey, TValue>(this TResult haystack, params TResult[] others)
            where TResult : IDictionary<TKey, TValue>, new()
        {
            var result = new TResult();
            if (others == null)
                return result;

            foreach (var p in (new List<TResult> { haystack }).Concat(others.Where(o => o != null)).SelectMany(src => src))
            {
                result[p.Key] = p.Value;
            }
            return result;
        }

        /// <summary>
        /// Merges the left dictionary in a smart way (you can specify a method to solve conflicts).
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="haystack">The dictionary to look into.</param>
        /// <param name="mergeBehavior">The merge behavior.</param>
        /// <param name="others">The other dictionaries to merge with.</param>
        /// <returns></returns>
        public static TResult ContactLeft<TResult, TKey, TValue>(this TResult haystack, Func<TKey, TValue, TValue, TValue> mergeBehavior, params TResult[] others)
            where TResult : IDictionary<TKey, TValue>, new()
        {
            var result = new TResult();
            var haystacks = new List<TResult> { haystack }
                .Concat(others);

            foreach (var kv in haystacks.SelectMany(src => src))
            {
                result.TryGetValue(kv.Key, out var previousValue);
                result[kv.Key] = mergeBehavior(kv.Key, kv.Value, previousValue);
            }

            return result;
        }

        /// <summary>
        /// Merges the sum.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="haystack">The dictionary to look into.</param>
        /// <param name="others">The other dictionaries to merge with.</param>
        /// <returns></returns>
        public static TResult MergeSum<TResult, TKey>(this TResult haystack, params TResult[] others)
            where TResult : Dictionary<TKey, int>, IDictionary<TKey, int>
        {
            var dictionaries = new List<IDictionary<TKey, int>> { haystack }
                .Concat(others);

            return dictionaries.SelectMany(dict => dict)
                .ToLookup(pair => pair.Key, pair => pair.Value)
                .ToDictionary(group => @group.Key, group => @group.Sum()) as TResult;
        }
    }
}
