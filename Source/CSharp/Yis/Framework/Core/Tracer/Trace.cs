using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.Factory;

namespace Yis.Framework.Core.Tracer
{
    /// <summary>
    /// Classe outil servant à écrire les Traces.
    /// </summary>
    public sealed class Trace
    {


        private static ITrace _trace;
        /// <summary>
        /// Initialise la classe <see cref="Trace"/>.
        /// </summary>
        static Trace()
        {
            Initialize();
        }

        /// <summary>
        /// Initialiser avec une Factory définit
        /// </summary>
        /// <param name="factory">Factory</param>
        public static void InitializeWith(IFactory<ITrace> factory)
        {
            _trace = factory.CreateInstance();
        }

        /// <summary>
        /// Initialiser
        /// </summary>
        public static void Initialize()
        {
            InitializeWith(new TraceFactory());
        }


        #region "Implémentation de ITrace (Façon Proxy)"

        /// <summary>
        /// Log un message au niveau "Debug".
        /// </summary>
        /// <param name="message">Le message à logger.</param>
        public static void Debug(string message)
        {
            if (IsDebugEnabled)
            {
                _trace.Debug(message);
            }
        }

        /// <summary>
        /// Log un message formaté au niveau "Debug".
        /// </summary>
        /// <param name="message">Le message contenant le format à logger.</param>
        /// <param name="args">Les arguments pour le formatage.</param>
        public static void Debug(string message, params object[] args)
        {
            if (IsDebugEnabled)
            {
                _trace.Debug(message, args);
            }
        }

        /// <summary>
        /// Retourne true si le niveau "Debug" est activé.
        /// </summary>
        public static bool IsDebugEnabled
        {
            get { return _trace.IsDebugEnabled; }
        }

        /// <summary>
        /// Log un message au niveau "Info".
        /// </summary>
        /// <param name="message">Le message à logger.</param>
        public static void Info(string message)
        {
            if (IsInfoEnabled)
            {
                _trace.Info(message);
            }
        }

        /// <summary>
        /// Log un message formaté au niveau "Info".
        /// </summary>
        /// <param name="message">Le message contenant le format à logger.</param>
        /// <param name="args">Les arguments pour le formatage.</param>
        public static void Info(string message, params object[] args)
        {
            if (IsInfoEnabled)
            {
                _trace.Info(message, args);
            }
        }

        /// <summary>
        /// Retourne true si le niveau "Info" est activé.
        /// </summary>
        public static bool IsInfoEnabled
        {
            get { return _trace.IsInfoEnabled; }
        }

        /// <summary>
        /// Log un message au niveau "Warning".
        /// </summary>
        /// <param name="message">Le message à logger.</param>
        public static void Warning(string message)
        {
            if (IsWarningEnabled)
            {
                _trace.Warning(message);
            }
        }

        public static void Warning(string message, System.Exception ex)
        {
            if (IsWarningEnabled)
            {
                _trace.Warning(message, ex);
            }
        }

        /// <summary>
        /// Log un message formaté au niveau "Warnin"g.
        /// </summary>
        /// <param name="message">Le message contenant le format à logger.</param>
        /// <param name="args">Les arguments pour le formatage.</param>
        public static void Warning(string message, params object[] args)
        {
            if (IsWarningEnabled)
            {
                _trace.Warning(message, args);
            }
        }

        /// <summary>
        /// Retourne true si le niveau "Warning" est activé.
        /// </summary>
        public static bool IsWarningEnabled
        {
            get { return _trace.IsWarningEnabled; }
        }

        /// <summary>
        /// Log un message au niveau "Error".
        /// </summary>
        /// <param name="message">Le message à logger.</param>

        public static void Error(string message)
        {
            if (IsErrorEnabled)
            {
                _trace.Error(message);
            }
        }


        public static void Error(string message, System.Exception ex)
        {
            if (IsErrorEnabled)
            {
                _trace.Error(message, ex);
            }
        }

        /// <summary>
        /// Log un message formaté au niveau "Error".
        /// </summary>
        /// <param name="message">Le message contenant le format à logger.</param>
        /// <param name="args">Les arguments pour le formatage.</param>
        public static void Error(string message, params object[] args)
        {
            if (IsErrorEnabled)
            {
                _trace.Error(message, args);
            }
        }

        /// <summary>
        /// Retourne true si le niveau "Error" est activé
        /// </summary>
        public static bool IsErrorEnabled
        {
            get { return _trace.IsErrorEnabled; }
        }

        #endregion
    }

}
