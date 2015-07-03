using System;

namespace Yis.Framework.Core.Logging.Contract
{
    /// <summary>
    /// Définition pour l'implémentation d'un client de Trace Dériver de cette classe pour
    /// implémenter un logger.
    /// </summary>
    public interface ILog
    {
        #region Properties

        bool IsDebugEnabled { get; }

        /// <summary>
        /// Retourne true si le niveau "Error" est activé
        /// </summary>
        bool IsErrorEnabled { get; }

        /// <summary>
        /// La log du framework est activé
        /// </summary>
        bool IsFrameworkEnabled { get; }

        bool IsInfoEnabled { get; }

        bool IsWarningEnabled { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Log un message au niveau "Debug".
        /// </summary>
        /// <param name="message">Le message à logger.</param>
        void Debug(String message);

        /// <summary>
        /// Log un message formaté au niveau "Debug".
        /// </summary>
        /// <param name="message">Le message contenant le format à logger.</param>
        /// <param name="args">   Les arguments pour le formatage.</param>
        void Debug(String message, params object[] args);

        /// <summary>
        /// Retourne true si le niveau "Warning" est activé.
        /// </summary>
        /// <summary>
        /// Log un message au niveau "Error".
        /// </summary>
        /// <param name="message">Le message à logger.</param>
        void Error(String message);

        void Error(String message, System.Exception ex);

        /// <summary>
        /// Log un message formaté au niveau "Error".
        /// </summary>
        /// <param name="message">Le message contenant le format à logger.</param>
        /// <param name="args">   Les arguments pour le formatage.</param>
        void Error(String message, params object[] args);

        /// <summary>
        /// Retourne true si le niveau "Debug" est activé.
        /// </summary>
        /// <summary>
        /// Log un message au niveau "Info".
        /// </summary>
        /// <param name="message">Le message à logger.</param>
        void Info(String message);

        /// <summary>
        /// Log un message formaté au niveau "Info".
        /// </summary>
        /// <param name="message">Le message contenant le format à logger.</param>
        /// <param name="args">   Les arguments pour le formatage.</param>
        void Info(String message, params object[] args);

        /// <summary>
        /// Retourne true si le niveau "Info" est activé.
        /// </summary>
        /// <summary>
        /// Log un message au niveau "Warning".
        /// </summary>
        /// <param name="message">Le message à logger.</param>
        void Warning(String message);

        /// <summary>
        /// Log un message au niveau "Warning".
        /// </summary>
        /// <param name="message">Le message à logger.</param>
        void Warning(String message, System.Exception Ex);

        /// <summary>
        /// Log un message formaté au niveau "Warnin"g.
        /// </summary>
        /// <param name="message">Le message contenant le format à logger.</param>
        /// <param name="args">   Les arguments pour le formatage.</param>
        void Warning(String message, params object[] args);

        #endregion Methods
    }
}