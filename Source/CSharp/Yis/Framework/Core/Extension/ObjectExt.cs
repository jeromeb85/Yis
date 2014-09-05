namespace Yis.Framework.Core.Extension
{
    /// <summary>
    /// Extension de la classe Object
    /// </summary>
    public static class ObjectExt
    {
        #region Methods

        /// <summary>
        /// Test si un Object est null
        /// </summary>
        /// <param name="source">Object à tester</param>
        /// <returns>Renvoie True si l'Object est Null</returns>
        public static bool IsNull(this object source)
        {
            return source == null;
        }

        #endregion Methods
    }
}