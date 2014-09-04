using System.Globalization;

/// <summary>
/// Extension de String
/// </summary>
public static class StringExt
{
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
}