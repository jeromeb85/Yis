using System;
using System.Collections.Generic;
using System.Linq;

namespace Yis.Framework.Core.Extension
{
    public static class ICollectionExt
    {
        #region Methods

        /// <summary>
        /// Ajoute tous les éléments à la collection
        /// </summary>
        /// <typeparam name="T">type d'éléments</typeparam>
        /// <param name="value">collection à modifier</param>
        /// <param name="items">éléments à ajouter à la collection</param>
        public static void AddRange<T>(this ICollection<T> value, IEnumerable<T> items)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (items == null)
                throw new ArgumentNullException("items");

            foreach (var item in items)
                value.Add(item);
        }

        /// <summary>
        /// Supprime le premier élément répondant au prédicat
        /// </summary>
        /// <typeparam name="T">type d'éléments</typeparam>
        /// <param name="value">    collection à modifier</param>
        /// <param name="predicate">prédicat de suppression</param>
        public static void RemoveFirst<T>(this ICollection<T> value, Func<T, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            try
            {
                T element = value.First(predicate);
                value.Remove(element);
            }
            catch { }
        }

        /// <summary>
        /// Supprime tous les items de la collection
        /// </summary>
        /// <typeparam name="T">type d'éléments</typeparam>
        /// <param name="value">collection à modifier</param>
        /// <param name="items">éléments à supprimer de la collection</param>
        public static void RemoveRange<T>(this ICollection<T> value, IEnumerable<T> items)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (items == null)
                throw new ArgumentNullException("items");

            foreach (var item in items)
                value.Remove(item);
        }

        /// <summary>
        /// Supprime les éléments répondant au prédicat
        /// </summary>
        /// <typeparam name="T">type d'éléments</typeparam>
        /// <param name="value">    collection à modifier</param>
        /// <param name="predicate">prédicat de suppression</param>
        public static void RemoveWhere<T>(this ICollection<T> value, Func<T, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            value.RemoveRange(value.Where(predicate).ToList());
        }

        #endregion Methods
    }
}