using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace Yis.Framework.Core.Extension
//{
    /// <summary>
    ///     Extensions valable pour l'ensemble du Framework
    /// </summary>
    public static class ExtString
    {

        /// <summary>
        ///     Calls string.Format(CultureInfo.CurrentCulture, value, args);
        ///     where the value is the template and args contains the array of arguments
        /// </summary>
        /// <param name = "value">The value.</param>
        /// <param name = "args">The args.</param>
        /// <returns></returns>
                //[System.Runtime.CompilerServices.Extension()]
        public static string FormatWith(this string value, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, value, args);
        }



    }

//}
