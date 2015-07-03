namespace Yis.Framework.Business.Contract
{
    /// <summary>
    /// Interface identifiant un <see cref="BusinessObject"/> comme sauvegardable
    /// </summary>
    public interface ISavableBusinessObject
    {
        #region Methods

        /// <summary>
        /// Sauvegarde le BusinessObject
        /// </summary>
        void Save();

        #endregion Methods
    }
}