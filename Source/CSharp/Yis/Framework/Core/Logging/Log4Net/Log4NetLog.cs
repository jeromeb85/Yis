using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Yis.Framework.Core.Logging.Log4Net
{
    /// <summary>
    /// Un client de logging <see cref="log4net"/>.
    /// </summary>
    public class Log4NetLog : Yis.Framework.Core.Logging.Contract.ILog
    {
        private readonly ILog _log4net;
        /// <summary>
        /// Initialise une nouvelle instance de <see cref="Log4NetTrace"/> classe.
        /// </summary>

        public Log4NetLog()
        {
            //Dim AssemblyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\"
            string AssemblyPath = AppDomain.CurrentDomain.BaseDirectory;

            string configFile = ConfigurationManager.AppSettings["LogFileConfName"];



            if ((string.IsNullOrEmpty(configFile)))
            {
                XmlConfigurator.Configure();
            }
            else
            {
                configFile = configFile.Replace(".\\", AssemblyPath);
                XmlConfigurator.ConfigureAndWatch(new FileInfo(configFile));
            }

            IsFrameworkEnabled = true;
            //_log4net = log4net.LogManager.GetLogger( LogManager.GetLogger("FleuryMichonLog");
        }


        #region Implémentation de Ilog

        /// <summary>
        /// Log un message au niveau "Debug".
        /// </summary>
        /// <param name="message">Le message à logger.</param>
        public void Debug(string message)
        {
            if (_log4net.IsDebugEnabled)
            {
                _log4net.Debug(message);
            }
        }

        /// <summary>
        /// Log un message formaté au niveau "Debug".
        /// </summary>
        /// <param name="message">Le message contenant le format à logger.</param>
        /// <param name="args">Les arguments pour le formatage.</param>
        public void Debug(string message, params object[] args)
        {
            if (_log4net.IsDebugEnabled)
            {
                Debug(message.FormatWith(args));
            }
        }

        /// <summary>
        /// Retourne true si le niveau "Debug" est activé.
        /// </summary>
        public bool IsDebugEnabled
        {
            get { return _log4net.IsDebugEnabled; }
        }

        /// <summary>
        /// Log un message au niveau "Info".
        /// </summary>
        /// <param name="message">Le message à logger.</param>
        public void Info(string message)
        {
            if (_log4net.IsInfoEnabled)
            {
                _log4net.Info(message);
            }
        }

        /// <summary>
        /// Log un message formaté au niveau "Info".
        /// </summary>
        /// <param name="message">Le message contenant le format à logger.</param>
        /// <param name="args">Les arguments pour le formatage.</param>
        public void Info(string message, params object[] args)
        {
            if (_log4net.IsInfoEnabled)
            {
                Info(message.FormatWith(args));
            }
        }

        /// <summary>
        /// Retourne true si le niveau "Info" est activé.
        /// </summary>
        public bool IsInfoEnabled
        {
            get { return _log4net.IsInfoEnabled; }
        }

        /// <summary>
        /// Log un message au niveau "Warning".
        /// </summary>
        /// <param name="message">Le message à logger.</param>
        public void Warning(string message)
        {
            if (_log4net.IsWarnEnabled)
            {
                _log4net.Warn(message);
            }
        }

        /// <summary>
        /// Log un message formaté au niveau "Warnin"g.
        /// </summary>
        /// <param name="message">Le message contenant le format à logger.</param>
        /// <param name="args">Les arguments pour le formatage.</param>
        public void Warning(string message, params object[] args)
        {
            if (_log4net.IsWarnEnabled)
            {
                Warning(message.FormatWith(args));
            }
        }

        /// <summary>
        /// Log un message formaté au niveau "Warnin"g.
        /// </summary>
        /// <param name="message">Le message contenant le format à logger.</param>
        /// <param name="args">Les arguments pour le formatage.</param>
        public void Warning(string message, System.Exception Ex)
        {
            if (_log4net.IsWarnEnabled)
            {
                _log4net.Warn(message, Ex);
            }
        }

        /// <summary>
        /// Retourne true si le niveau "Warning" est activé.
        /// </summary>
        public bool IsWarningEnabled
        {
            get { return _log4net.IsWarnEnabled; }
        }

        /// <summary>
        /// Log un message au niveau "Error".
        /// </summary>
        /// <param name="message">Le message à logger.</param>
        public void Error(string message)
        {
            if (_log4net.IsErrorEnabled)
            {
                _log4net.Error(message);
            }
        }

        public void Error(string message, System.Exception ex)
        {
            if (_log4net.IsErrorEnabled)
            {
                _log4net.Error(message, ex);
            }
        }

        /// <summary>
        /// Log un message formaté au niveau "Error".
        /// </summary>
        /// <param name="message">Le message contenant le format à logger.</param>
        /// <param name="args">Les arguments pour le formatage.</param>
        public void Error(string message, params object[] args)
        {
            if (_log4net.IsErrorEnabled)
            {
                Error(message.FormatWith(args));
            }
        }

        /// <summary>
        /// Retourne true si le niveau "Error" est activé
        /// </summary>
        public bool IsErrorEnabled
        {
            get { return _log4net.IsErrorEnabled; }
        }

        public bool IsFrameworkEnabled
        {
            get;
            set;
        }
        #endregion
    }

}
