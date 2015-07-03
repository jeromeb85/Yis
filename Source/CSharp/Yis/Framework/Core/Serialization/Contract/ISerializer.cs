using System.IO;

namespace Yis.Framework.Core.Serialization.Contract
{
    /// <summary>
    /// </summary>
    public interface ISerializer
    {
        #region Methods

        /// <summary>
        /// Des the serialize.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        T DeSerialize<T>(string file);

        /// <summary>
        /// DeSerialize.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        T DeSerialize<T>(Stream stream);

        /// <summary>
        /// Serializes the specified obj.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"> The obj.</param>
        /// <param name="file">The file.</param>
        void Serialize<T>(T obj, string file);

        /// <summary>
        /// Serializes the specified obj.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">   The obj.</param>
        /// <param name="stream">The stream.</param>
        void Serialize<T>(T obj, Stream stream);

        #endregion Methods
    }
}