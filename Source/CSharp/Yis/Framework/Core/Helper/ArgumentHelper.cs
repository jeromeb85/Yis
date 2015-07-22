using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Yis.Framework.Core.Extension;
using Yis.Framework.Core.IoC;
using Yis.Framework.Core.IoC.Contract;
using Yis.Framework.Core.Logging.Contract;

namespace Yis.Framework.Core.Helper
{
    /// <summary>
    /// Argument validator class to help validating arguments that are passed into a method.
    /// <para />
    /// This class automatically adds thrown exceptions to the log file.
    /// </summary>
    public static class ArgumentHelper
    {
        #region Fields

        private static ILog _log;

        #endregion Fields

        #region Properties

        private static ILog Log
        {
            get
            {
                if (_log.IsNull())
                {
                    _log = Resolver.Resolve<ILog>();
                }
                return _log;
            }
        }

        private static IDependencyResolver Resolver
        {
            get { return DependencyResolverManager.Default; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Checks whether the specified <paramref name="instance" /> implements the specified <paramref name="interfaceType" />.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="instance">The instance to check.</param>
        /// <param name="interfaceType">The type of the interface to check for.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="instance" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The <paramref name="instance" /> does not implement the <paramref name="interfaceType" />.</exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void ImplementsInterface(string paramName, object instance, Type interfaceType)
        {
            ArgumentHelper.IsNotNull("instance", instance);

            ImplementsInterface(paramName, instance.GetType(), interfaceType);
        }

        /// <summary>
        /// Checks whether the specified <paramref name="instance" /> implements the specified <typeparamref name="TInterface" />.
        /// </summary>
        /// <typeparam name="TInterface">The type of the T interface.</typeparam>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="instance">The instance to check.</param>
        /// <exception cref="ArgumentException">The <paramref name="paramName" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="instance" /> is <c>null</c>.</exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void ImplementsInterface<TInterface>(string paramName, object instance)
            where TInterface : class
        {
            var interfaceType = typeof(TInterface);

            ImplementsInterface(paramName, instance, interfaceType);
        }

        /// <summary>
        /// Checks whether the specified <paramref name="instance" /> implements at least one of the specified <paramref name="interfaceTypes" />.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="instance">The instance to check.</param>
        /// <param name="interfaceTypes">The types of the interfaces to check for.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="instance" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The <paramref name="interfaceTypes" /> is <c>null</c> or an empty array.</exception>
        /// <exception cref="ArgumentException">The <paramref name="instance" /> does not implement at least one of the <paramref name="interfaceTypes" />.</exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void ImplementsOneOfTheInterfaces(string paramName, object instance, Type[] interfaceTypes)
        {
            ArgumentHelper.IsNotNull("instance", instance);

            ImplementsOneOfTheInterfaces(paramName, instance.GetType(), interfaceTypes);
        }

        /// <summary>
        /// Checks whether the specified <paramref name="instance" /> inherits from the <paramref name="baseType" />.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="instance">The instance.</param>
        /// <param name="baseType">The base type.</param>
        /// <exception cref="ArgumentException">The <paramref name="paramName" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="instance" /> is <c>null</c>.</exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void InheritsFrom(string paramName, object instance, Type baseType)
        {
            IsNotNull("instance", instance);

            InheritsFrom(paramName, instance.GetType(), baseType);
        }

        /// <summary>
        /// Checks whether the specified <paramref name="instance" /> inherits from the specified <typeparamref name="TBase" />.
        /// </summary>
        /// <typeparam name="TBase">The base type.</typeparam>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="instance">The instance.</param>
        /// <exception cref="ArgumentException">The <paramref name="paramName" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="instance" /> is <c>null</c>.</exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void InheritsFrom<TBase>(string paramName, object instance)
            where TBase : class
        {
            var baseType = typeof(TBase);

            InheritsFrom(paramName, instance, baseType);
        }

        /// <summary>
        /// Determines whether the specified argument match with a given pattern.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="paramValue">The param value.</param>
        /// <param name="pattern">The pattern.</param>
        /// <param name="regexOptions">The regular expression options.</param>
        /// <exception cref="ArgumentException">The <paramref name="paramName" /> is <c>null</c> or whitespace.</exception>
        /// <exception cref="ArgumentException">The <paramref name="paramValue" /> is <c>null</c> or whitespace.</exception>
        /// <exception cref="ArgumentException">The <paramref name="pattern" /> is <c>null</c> or whitespace.</exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void IsMatch(string paramName, string paramValue, string pattern, RegexOptions regexOptions = RegexOptions.None)
        {
            ArgumentHelper.IsNotNull("paramValue", paramValue);
            ArgumentHelper.IsNotNull("pattern", pattern);

            if (!Regex.IsMatch(paramValue, pattern, regexOptions))
            {
                string error = string.Format("Argument '{0}' doesn't match with pattern '{1}'", paramName, pattern);
                throw new ArgumentException(error);
            }
        }

        /// Determines whether the specified argument has a maximum value.
        /// </summary>
        /// <typeparam name="T">Type of the argument.</typeparam>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">Value of the parameter.</param>
        /// <param name="maximumValue">The maximum value.</param>
        /// <param name="validation">The validation function to call for validation.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException">The <paramref name="validation" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="paramValue" /> is out of range.</exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void IsMaximum<T>(string paramName, T paramValue, T maximumValue, Func<T, T, bool> validation)
        {
            if (!validation(paramValue, maximumValue))
            {
                string error = string.Format("Argument '{0}' should be at maximum {1}", paramName, maximumValue);
                throw new ArgumentOutOfRangeException(paramName, error);
            }
        }

        /// <summary>
        /// Determines whether the specified argument has a maximum value.
        /// </summary>
        /// <param name="paramName">
        /// Name of the parameter.
        /// </param>
        /// <param name="paramValue">
        /// Value of the parameter.
        /// </param>
        /// <param name="maximumValue">
        /// The maximum value.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <paramref name="paramValue"/> is out of range.
        /// </exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void IsMaximum(string paramName, int paramValue, int maximumValue)
        {
            IsMaximum(paramName, paramValue, maximumValue, (innerParamValue, innerMaximumValue) => innerParamValue <= maximumValue);
        }

        /// <summary>
        /// Determines whether the specified argument has a minimum value.
        /// </summary>
        /// <typeparam name="T">Type of the argument.</typeparam>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">Value of the parameter.</param>
        /// <param name="minimumValue">The minimum value.</param>
        /// <param name="validation">The validation function to call for validation.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException">The <paramref name="validation" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="paramValue" /> is out of range.</exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void IsMinimal<T>(string paramName, T paramValue, T minimumValue, Func<T, T, bool> validation)
        {
            IsNotNull("validation", validation);

            if (!validation(paramValue, minimumValue))
            {
                string error = string.Format("Argument '{0}' should be minimal {1}", paramName, minimumValue);
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        /// <summary>
        /// Determines whether the specified argument has a minimum value.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">Value of the parameter.</param>
        /// <param name="minimumValue">The minimum value.</param>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="paramValue" /> is out of range.</exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void IsMinimal(string paramName, int paramValue, int minimumValue)
        {
            IsMinimal(paramName, paramValue, minimumValue, (internalParamValue, internalMinimumValue) => internalParamValue >= internalMinimumValue);
        }

        /// <summary>
        /// Determines whether the specified argument doesn't match with a given pattern.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="paramValue">The para value.</param>
        /// <param name="pattern">The pattern.</param>
        /// <param name="regexOptions">The regular expression options.</param>
        /// <exception cref="System.ArgumentException">The <paramref name="paramName" /> is <c>null</c> or whitespace.</exception>
        /// <exception cref="System.ArgumentException">The <paramref name="paramValue" /> is <c>null</c> or whitespace.</exception>
        /// <exception cref="System.ArgumentException">The <paramref name="pattern" /> is <c>null</c> or whitespace.</exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void IsNotMatch(string paramName, string paramValue, string pattern, RegexOptions regexOptions = RegexOptions.None)
        {
            ArgumentHelper.IsNotNull("paramValue", paramValue);
            ArgumentHelper.IsNotNull("pattern", pattern);

            if (Regex.IsMatch(paramValue, pattern, regexOptions))
            {
                string error = string.Format("Argument '{0}' matches with pattern '{1}'", paramName, pattern);
                throw new ArgumentException(error);
            }
        }

        /// <summary>
        /// Determines whether the specified argument is not <c>null</c>.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">Value of the parameter.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="ArgumentNullException">If <paramref name="paramValue" /> is <c>null</c>.</exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void IsNotNull(string paramName, object paramValue)
        {
            if (paramValue == null)
            {
                Log.Error("L'argument '{0}' ne peut être null", paramName);
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Determines whether the specified argument is not <c>null</c> or empty.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">Value of the parameter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="ArgumentException">If <paramref name="paramValue" /> is <c>null</c> or empty.</exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void IsNotNullOrEmpty(string paramName, string paramValue)
        {
            if (string.IsNullOrEmpty(paramValue))
            {
                string error = string.Format("Argument '{0}' cannot be null or empty", paramName);
                throw new ArgumentException(error, paramName);
            }
        }

        /// <summary>
        /// Determines whether the specified argument is not <c>null</c> or empty.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">Value of the parameter.</param>
        /// <exception cref="ArgumentException">If <paramref name="paramValue" /> is <c>null</c> or empty.</exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void IsNotNullOrEmpty(string paramName, Guid paramValue)
        {
            IsNotNullOrEmpty(paramName, (Guid?)paramValue);
        }

        /// <summary>
        /// Determines whether the specified argument is not <c>null</c> or empty.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">Value of the parameter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="ArgumentException">If <paramref name="paramValue" /> is <c>null</c> or empty.</exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void IsNotNullOrEmpty(string paramName, Guid? paramValue)
        {
            if (!paramValue.HasValue || paramValue.Value == Guid.Empty)
            {
                string error = string.Format("Argument '{0}' cannot be null or Guid.Empty", paramName);
                throw new ArgumentException(error, paramName);
            }
        }

        /// <summary>
        /// Determines whether the specified argument is not <c>null</c> or an empty array (.Length == 0).
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">Value of the parameter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="ArgumentException">If <paramref name="paramValue" /> is <c>null</c> or an empty array.</exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void IsNotNullOrEmptyArray(string paramName, Array paramValue)
        {
            if ((paramValue == null) || (paramValue.Length == 0))
            {
                string error = string.Format("Argument '{0}' cannot be null or an empty array", paramName);
                throw new ArgumentException(error, paramName);
            }
        }

        /// <summary>
        /// Determines whether the specified argument is not <c>null</c> or a whitespace.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">Value of the parameter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="ArgumentException">If <paramref name="paramValue" /> is <c>null</c> or a whitespace.</exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void IsNotNullOrWhitespace(string paramName, string paramValue)
        {
            if (string.IsNullOrEmpty(paramValue) || (string.CompareOrdinal(paramValue.Trim(), string.Empty) == 0))
            {
                string error = string.Format("Argument '{0}' cannot be null or whitespace", paramName);
                throw new ArgumentException(error, paramName);
            }
        }

        /// <summary>
        /// Determines whether the specified argument is not out of range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">Value of the parameter.</param>
        /// <param name="minimumValue">The minimum value.</param>
        /// <param name="maximumValue">The maximum value.</param>
        /// <param name="validation">The validation function to call for validation.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException">The <paramref name="validation" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="paramValue" /> is out of range.</exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void IsNotOutOfRange<T>(string paramName, T paramValue, T minimumValue, T maximumValue, Func<T, T, T, bool> validation)
        {
            IsNotNull("validation", validation);

            if (!validation(paramValue, minimumValue, maximumValue))
            {
                string error = string.Format("Argument '{0}' should be between {1} and {2}", paramName, minimumValue, maximumValue);
                throw new ArgumentOutOfRangeException(paramName, error);
            }
        }

        /// <summary>
        /// Determines whether the specified argument is not out of range.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">Value of the parameter.</param>
        /// <param name="minimumValue">The minimum value.</param>
        /// <param name="maximumValue">The maximum value.</param>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="paramValue" /> is out of range.</exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void IsNotOutOfRange(string paramName, int paramValue, int minimumValue, int maximumValue)
        {
            IsNotOutOfRange(paramName, paramValue, minimumValue, maximumValue, (innerParamValue, innerMinimalValue, innerMaximumValue) => innerParamValue >= innerMinimalValue && innerParamValue <= innerMaximumValue);
        }

        /// <summary>
        /// Checks whether the specified <paramref name="instance" /> is of the specified <paramref name="requiredType" />.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="instance">The instance to check.</param>
        /// <param name="requiredType">The type to check for.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="instance" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="instance" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The <paramref name="instance" /> is not of type <paramref name="requiredType" />.</exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void IsOfType(string paramName, object instance, Type requiredType)
        {
            ArgumentHelper.IsNotNull("instance", instance);

            IsOfType(paramName, instance.GetType(), requiredType);
        }

        /// <summary>
        /// Checks whether the passed in boolean check is <c>true</c>. If not, this method will throw a <see cref="NotSupportedException" />.
        /// </summary>
        /// <param name="isSupported">if set to <c>true</c>, the action is supported; otherwise <c>false</c>.</param>
        /// <param name="errorFormat">The error format.</param>
        /// <param name="args">The arguments for the string format.</param>
        /// <exception cref="NotSupportedException">The <paramref name="isSupported" /> is <c>false</c>.</exception>
        /// <exception cref="ArgumentException">The <paramref name="errorFormat" /> is <c>null</c> or whitespace.</exception>
        public static void IsSupported(bool isSupported, string errorFormat, params object[] args)
        {
            ArgumentHelper.IsNotNullOrWhitespace("errorFormat", errorFormat);

            if (!isSupported)
            {
                string error = string.Format(errorFormat, args);
                throw new NotSupportedException(error);
            }
        }

        /// <summary>
        /// Determines whether the specified argument is valid.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">The parameter value.</param>
        /// <param name="validation">The validation function.</param>
        /// <exception cref="ArgumentException">If the <paramref name="validation" /> code returns <c>false</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="paramName" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="paramValue" /> is <c>null</c>.</exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void IsValid<T>(string paramName, T paramValue, Func<bool> validation)
        {
            ArgumentHelper.IsNotNull("validation", validation);

            IsValid(paramName, paramValue, validation.Invoke());
        }

        /// <summary>
        /// Determines whether the specified argument is valid.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">The parameter value.</param>
        /// <param name="validation">The validation function.</param>
        /// <exception cref="ArgumentException">If the <paramref name="validation" /> code returns <c>false</c>.</exception>
        /// <exception cref="System.ArgumentNullException">The <paramref name="paramName" /> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentNullException">The <paramref name="paramValue" /> is <c>null</c>.</exception>
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void IsValid<T>(string paramName, T paramValue, bool validation)
        {
            ArgumentHelper.IsNotNull("paramValue", paramValue);

            if (!validation)
            {
                string error = string.Format("Argument '{0}' is not valid", paramName);
                throw new ArgumentException(error);
            }
        }

        #endregion Methods
    }
}