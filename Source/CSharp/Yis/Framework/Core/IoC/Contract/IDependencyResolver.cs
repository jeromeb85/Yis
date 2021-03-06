﻿using System;
using System.Collections.Generic;

namespace Yis.Framework.Core.IoC.Contract
{
    /// <summary>
    /// Interface de tous les les résolveurs de dépendances.
    /// </summary>
    public interface IDependencyResolver : IDisposable
    {
        #region Methods

        void Inject<T>(T existing);

        bool IsRegistered<T>();

        /// <summary>
        /// Enregistre une instance spécifiée.
        /// </summary>
        /// <typeparam name="T">Type de l'instance.</typeparam>
        /// <param name="instance">L'instance.</param>
        void Register<T>(T instance);

        void Register<T>(string name, T instance);

        /// <summary>
        /// Injection sur un type existant.
        /// </summary>
        /// <typeparam name="T">Le type de l'existant</typeparam>
        /// <param name="existing">L'existant.</param>
        /// <summary>
        /// Résoudre un type spécifié.
        /// </summary>
        /// <typeparam name="T">Type à résoudre.</typeparam>
        /// <param name="type">Le Type.</param>
        /// <returns>Retourne une instance du type à résoudre.</returns>
        T Resolve<T>(Type type);

        /// <summary>
        /// Résoudre le type spécifié.
        /// </summary>
        /// <typeparam name="T">Le type à résoudre</typeparam>
        /// <param name="type">Le Type.</param>
        /// <param name="name">Le nom.</param>
        /// <returns>Retourne une instance du type à résoudre.</returns>
        T Resolve<T>(Type type, string name);

        /// <summary>
        /// Résoudre le type spécifié.
        /// </summary>
        /// <typeparam name="T">Le type à résoudre.</typeparam>
        /// <returns>Retourne une instance du type à résoudre.</returns>
        T Resolve<T>();

        /// <summary>
        /// Résoudre le typé spécifié.
        /// </summary>
        /// <typeparam name="T">Le type à résoudre.</typeparam>
        /// <param name="name">Le nom.</param>
        /// <returns>Retourne une instance du type à résoudre.</returns>
        T Resolve<T>(string name);

        /// <summary>
        /// Résoudre un ensemble
        /// </summary>
        /// <typeparam name="T">Le type à résoudre.</typeparam>
        /// <returns>Retourne des instances de type à résoudre.</returns>
        IEnumerable<T> ResolveAll<T>();

        #endregion Methods
    }
}