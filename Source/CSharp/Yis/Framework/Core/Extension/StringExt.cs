using System;
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
        /// Resolve an absolute or relative href to a site-specific uri.
        /// </summary>
        /// <param name="href">The link.</param>
        /// <param name="siteUrl">The sites Uri.</param>
        /// <returns>An absolute uri where host is the same as the sites.</returns>
        public static Uri ToAbsoluteUri(this string href, Uri siteUrl)
        {
            var uri = new Uri(href, UriKind.RelativeOrAbsolute);
            if (uri.IsAbsoluteUri)
                return uri;

            return new Uri(siteUrl, href.Trim('/'));
            // TODO: return new Uri(string.Format("{0}://{1}/{2}", siteUrl.Scheme, siteUrl.Authority.TrimEnd('/'), href.Trim('/')));
        }

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