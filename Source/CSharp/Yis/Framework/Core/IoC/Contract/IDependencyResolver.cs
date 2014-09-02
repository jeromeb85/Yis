using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Core.IoC
{
        /// <summary>
        /// Interface de tous les les résolveurs de dépendances.
        /// </summary>
        public interface IDependencyResolver : IDisposable
        {
            /// <summary>
            /// Enregistre une instance spécifiée.
            /// </summary>
            /// <typeparam name="T">Type de l'instance.</typeparam>
            /// <param name="instance">L'instance.</param>
            void Register<T>(T instance);
            void Register<T>(string name,T instance);

            /// <summary>
            /// Injection sur un type existant.
            /// </summary>
            /// <typeparam name="T">Le type de l'existant</typeparam>
            /// <param name="existing">L'existant.</param>

            void Inject<T>(T existing);
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


            bool IsRegistered<T>();

            /// <summary>
            /// Résoudre un ensemble
            /// </summary>
            /// <typeparam name="T">Le type à résoudre.</typeparam>
            /// <returns>Retourne des instances de type à résoudre.</returns>
            IEnumerable<T> ResolveAll<T>();
        }
    
}
