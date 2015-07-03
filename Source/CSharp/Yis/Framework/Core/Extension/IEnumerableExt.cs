using System;
using System.Collections.Generic;
using System.Linq;

namespace Yis.Framework.Core.Extension
{
    /// <summary>
    /// Extension pour le type IEnumerable
    /// </summary>
    public static class IEnumerableExt
    {
        #region Methods

        /// <summary>
        /// Met à plat un arbre
        /// </summary>
        /// <typeparam name="T">type d'objet</typeparam>
        /// <param name="value">           enumerable à mettre à plat</param>
        /// <param name="childrenSelector">méthode récupérant les enfants</param>
        /// <returns>la liste mise à plat</returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> value, Func<T, IEnumerable<T>> childrenSelector)
        {
            var result = value;

            if (value != null)
            {
                foreach (T element in value)
                {
                    var children = childrenSelector(element).Flatten(childrenSelector);

                    if (children != null)
                        result = result.Concat(children);
                }
            }

            return result;
        }

        /// <summary>
        /// Applique un délégué à chacun des éléments
        /// </summary>
        /// <typeparam name="T">type d'éléments</typeparam>
        /// <param name="value"> IEnumerable étendu</param>
        /// <param name="action">délégué à appliquer sur tous les éléments</param>
        public static void ForEach<T>(this IEnumerable<T> value, Action<T> action)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (action == null)
                throw new ArgumentNullException("action");

            foreach (var item in value)
                action(item);
        }

        /// <summary>
        /// Applique un délégué aux éléments répondant au prédicat
        /// </summary>
        /// <typeparam name="T">type d'éléments</typeparam>
        /// <param name="value">    IEnumerable étendu</param>
        /// <param name="action">   délégué à appliquer sur tous les éléments</param>
        /// <param name="predicate">prédicat de recherche</param>
        public static void ForEach<T>(this IEnumerable<T> value, Action<T> action, Func<T, bool> predicate)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (action == null)
                throw new ArgumentNullException("action");

            value.Where(predicate).ForEach(action);
        }

        #endregion Methods
    }
}