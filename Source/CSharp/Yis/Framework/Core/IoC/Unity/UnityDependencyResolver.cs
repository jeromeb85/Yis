using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using Yis.Framework.Core.IoC.Contract;

namespace Yis.Framework.Core.IoC
{
    /// <summary>
    /// Résolution des dépendances avec Unity
    /// </summary>
    internal class UnityDependencyResolver : IDisposable, IDependencyResolver
    {
        #region Fields

        private readonly IUnityContainer _container;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="UnityDependencyResolver"/> class.
        /// </summary>
        public UnityDependencyResolver()
            : this(new UnityContainer())
        {
            UnityConfigurationSection configuration = ConfigurationManager.GetSection("Unity") as UnityConfigurationSection;

            if (configuration != null)
            {
                configuration.Configure(_container);
            }
        }

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="UnityDependencyResolver"/> class.
        /// </summary>
        /// <param name="container">Le conainer Unity.</param>
        public UnityDependencyResolver(IUnityContainer container)
        {
            _container = container;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Enregistre une instance spécifiée.
        /// </summary>
        /// <typeparam name="T">Type de l'instance.</typeparam>
        /// <param name="instance">L'instance.</param>
        public void Register<T>(T instance)
        {
            _container.RegisterInstance(instance);
        }

        public void Register<T>(string name, T instance)
        {
            _container.RegisterInstance(name, instance);
        }

        /// <summary>
        /// Résoudre un type spécifié.
        /// </summary>
        /// <typeparam name="T">Type à résoudre.</typeparam>
        /// <param name="type">Le Type.</param>
        /// <returns>
        /// Retourne une instance du type à résoudre.
        /// </returns>
        public T Resolve<T>(Type type)
        {
            return (T)_container.Resolve(type);
        }

        /// <summary>
        /// Résoudre le type spécifié.
        /// </summary>
        /// <typeparam name="T">Le type à résoudre</typeparam>
        /// <param name="type">Le Type.</param>
        /// <param name="name">Le nom.</param>
        /// <returns>
        /// Retourne une instance du type à résoudre.
        /// </returns>
        public T Resolve<T>(Type type, string name)
        {
            return (T)_container.Resolve(type, name);
        }

        /// <summary>
        /// Résoudre le type spécifié.
        /// </summary>
        /// <typeparam name="T">Le type à résoudre.</typeparam>
        /// <returns>
        /// Retourne une instance du type à résoudre.
        /// </returns>
        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        /// <summary>
        /// Résoudre le typé spécifié.
        /// </summary>
        /// <typeparam name="T">Le type à résoudre.</typeparam>
        /// <param name="name">Le nom.</param>
        /// <returns>
        /// Retourne une instance du type à résoudre.
        /// </returns>
        public T Resolve<T>(string name)
        {
            return _container.Resolve<T>(name);
        }

        /// <summary>
        /// Résoudre un ensemble
        /// </summary>
        /// <typeparam name="T">Le type à résoudre.</typeparam>
        /// <returns>
        /// Retourne des instances de type à résoudre.
        /// </returns>
        public IEnumerable<T> ResolveAll<T>()
        {
            IEnumerable<T> namedInstances = _container.ResolveAll<T>();
            T unnamedInstance;

            try
            {
                unnamedInstance = _container.Resolve<T>();
                //When default instance is missing
            }
            catch (ResolutionFailedException)
            {
            }

            //if (Equals(unnamedInstance, null))
            //{
            //  return namedInstances;
            //}

            //return null;
            //return new ReadOnlyCollection<T>(new List<T>(namedInstances) { unnamedInstance });

            return null;
        }

        public void Dispose()
        {
            _container.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Injection sur un type existant.
        /// </summary>
        /// <typeparam name="T">Le type de l'existant</typeparam>
        /// <param name="existing">L'existant.</param>
        public void Inject<T>(T existing)
        {
            _container.BuildUp(existing);
        }

        public bool IsRegistered<T>()
        {
            return _container.IsRegistered<T>();
        }

        #endregion Methods
    }
}