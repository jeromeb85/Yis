using System;
using System.Collections.Generic;
using System.Linq;
using Yis.Framework.Core.Helper;

namespace Yis.Framework.Core.Extension
{
    /// <summary>
    /// Extensions for the <see cref="Dictionary{TKey,TValue}"/> class.
    /// </summary>
    public static class IDictionaryExt
    {
        #region Methods

        /// <summary>
        /// Adds the specified value using the key if the value is not <c>null</c> or whitespace.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">       The key.</param>
        /// <param name="value">     The value to check and to add.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="dictionary"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="key"/> is <c>null</c>.</exception>
        public static void AddItemIfNotEmpty<TKey>(this IDictionary<TKey, string> dictionary, TKey key, string value)
        {
            ArgumentHelper.IsNotNull("dictionary", dictionary);
            ArgumentHelper.IsNotNull("key", key);

            if (!string.IsNullOrEmpty(value))
            {
                dictionary[key] = value;
            }
        }

        /// <summary>
        /// Adds all items from the source into the target dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="target">           The target.</param>
        /// <param name="source">           The source.</param>
        /// <param name="overwriteExisting">
        /// if set to <c>true</c>, existing items in the target dictionary will be overwritten.
        /// </param>
        /// <exception cref="ArgumentNullException">The <paramref name="target"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="source"/> is <c>null</c>.</exception>
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> target, IDictionary<TKey, TValue> source, bool overwriteExisting = true)
        {
            ArgumentHelper.IsNotNull("target", target);
            ArgumentHelper.IsNotNull("source", source);

            foreach (var keyValuePair in source)
            {
                if (!overwriteExisting)
                {
                    if (target.ContainsKey(keyValuePair.Key))
                    {
                        continue;
                    }
                }

                target[keyValuePair.Key] = keyValuePair.Value;
            }
        }

        public static bool ContainsKeyImplementing<T>(this IDictionary<Type, IList<object>> dictionary)
        {
            return dictionary.Keys.Any(typeof(T).IsAssignableFrom);
        }

        public static IEnumerable<T> GetInstancesImplementing<T>(this IDictionary<Type, IList<object>> dictionary)
        {
            var keys = dictionary.Keys.Where(typeof(T).IsAssignableFrom);

            return dictionary.Where(x => keys.Contains(x.Key)).SelectMany(x => x.Value).Cast<T>();
        }

        #endregion Methods
    }
}