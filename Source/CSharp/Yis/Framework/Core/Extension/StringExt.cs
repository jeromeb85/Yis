using System.Globalization;

namespace Yis.Framework.Core.Extension
{
    /// <summary>
    /// Extension de String
    /// </summary>
    public static class StringExt
    {
        #region Methods

        /// <summary>
        ///     Appel string.Format(CultureInfo.CurrentCulture, value, args);
        /// </summary>
        /// <param name = "value">La valeur.</param>
        /// <param name = "args">Tableau d'arguments.</param>
        /// <returns></returns>
        public static string FormatWith(this string value, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, value, args);
        }

        /// <summary>
        /// Teste si une String est Null ou vide
        /// </summary>
        /// <param name="value">String à tester</param>
        /// <returns>Retourne True si la String est null ou vide</returns>
        public static bool IsNotNullNorEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        #endregion Methods
    }
}