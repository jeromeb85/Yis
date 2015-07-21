using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.Weak;

namespace Yis.Framework.Core.Messaging.Internal
{
    /// <summary>
    /// Définit un abonnement à un événement du bus d'événements
    /// </summary>
    internal class Subscription
    {
        #region Attributs

        private WeakDelegate _callback;
        private WeakDelegate _filter;
        private ThreadingStrategy _threadingStrategy;

        #endregion Attributs

        #region Constructeurs

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="callback">callback à invoquer</param>
        /// <param name="filter">filtre permettant de savoir si le callback doit être invoqué</param>
        /// <param name="threadingStrategy">stratégie de threading d'exécution du callback</param>
        public Subscription(Delegate callback, Delegate filter, ThreadingStrategy threadingStrategy)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            _callback = new WeakDelegate(callback);

            if (filter != null)
                _filter = new WeakDelegate(filter);

            _threadingStrategy = threadingStrategy;
        }

        #endregion Constructeurs

        #region Propriétés

        /// <summary>
        /// Obtient l'instance cible du callback
        /// </summary>
        public object Target
        {
            get { return _callback.Target; }
        }

        /// <summary>
        /// Obtient le délégué du callback
        /// </summary>
        public Delegate Action
        {
            get { return _callback.GetDelegate(); }
        }

        /// <summary>
        /// Obtient le délégué du filtre
        /// </summary>
        public Delegate Filter
        {
            get { return (_filter != null) ? _filter.GetDelegate() : null; }
        }

        /// <summary>
        /// Obtient la stratégie de threading
        /// </summary>
        public ThreadingStrategy ThreadingStrategy
        {
            get { return _threadingStrategy; }
        }

        #endregion Propriétés
    }
}