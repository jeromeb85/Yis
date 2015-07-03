using System;
using System.Collections.Generic;
using System.Linq;
using Yis.Framework.Core.Extension;
using Yis.Framework.Core.Helper;
using Yis.Framework.Core.Messaging.Contract;

namespace Yis.Framework.Core.Messaging.Internal
{
    /// <summary>
    /// Définit un bus d'événements
    /// </summary>
    /// <remarks>toutes les références sont des références faibles</remarks>
    internal class Bus : IBus
    {
        #region Fields

        private readonly Dictionary<Type, List<Subscription>> _subscribees = new Dictionary<Type, List<Subscription>>();

        #endregion Fields

        #region Methods

        /// <summary>
        /// Publie un événement sur le bus
        /// </summary>
        /// <typeparam name="TMessage">type d'événement à publier</typeparam>
        /// <param name="message">événement à publier</param>
        public void Publish<TMessage>(TMessage message) where TMessage : IMessage
        {
            ArgumentHelper.IsNotNull("item", message);

            // Récupère les callbacks
            var actions = GetCallbacks<TMessage>(message);

            // Les exécutent s'il y en a
            if (actions != null)
                actions.ForEach(a => a(message));
        }

        /// <summary> Publie un événement modal sur le bus et récupère un résultat depuis le dernier
        /// abonné chronologiquement </summary> <typeparam name="TModalEvent<TResult>">type
        /// d'événement modal à publier</typeparam> <param name="item">événement modal à
        /// publier</param> <returns>le résultat de l'événement modal</returns>
        public TResult Publish<TMessage, TResult>(TMessage message) where TMessage : IMessage<TResult>
        {
            ArgumentHelper.IsNotNull("item", message);

            // Récupère le callback
            var action = GetCallback<TMessage, TResult>(message);

            // L'exécute s'il y en a un
            if (action != null)
                action(message);

            // Retourne le résultat
            return message.Result;
        }

        /// <summary>
        /// S'abonne à un événement publié sur le bus
        /// </summary>
        /// <typeparam name="TMessage">type d'événement pour lequel l'abonnement est fait</typeparam>
        /// <param name="callback">handler utilisé lors de la levée de l'événement</param>
        /// <remarks>
        /// Attention en cas d'utilisation de lambda à ce que ce ne soit pas un champs statique
        /// (=&gt; la lambda doit faire référence à la classe qui la contient
        /// </remarks>
        public void Subscribe<TMessage>(Action<TMessage> callback) where TMessage : IMessage
        {
            Subscribe<TMessage>(callback, null, ThreadingStrategy.Publisher);
        }

        /// <summary>
        /// S'abonne à un événement publié sur le bus
        /// </summary>
        /// <typeparam name="TMessage">type d'événement pour lequel l'abonnement est fait</typeparam>
        /// <param name="callback">handler utilisé lors de la levée de l'événement</param>
        /// <param name="filter">  
        /// filtre permettant de savoir si le handler doit être exécuté ou non
        /// </param>
        /// <remarks>
        /// Attention en cas d'utilisation de lambda à ce que ce ne soit pas un champs statique
        /// (=&gt; la lambda doit faire référence à la classe qui la contient
        /// </remarks>
        public void Subscribe<TMessage>(Action<TMessage> callback, Predicate<TMessage> filter) where TMessage : IMessage
        {
            Subscribe<TMessage>(callback, filter, ThreadingStrategy.Publisher);
        }

        /// <summary>
        /// S'abonne à un événement publié sur le bus
        /// </summary>
        /// <typeparam name="TMessage">type d'événement pour lequel l'abonnement est fait</typeparam>
        /// <param name="callback">handler utilisé lors de la levée de l'événement</param>
        /// <param name="strategy">stratégie de threading lors de l'exécution du callback</param>
        /// <remarks>
        /// Attention en cas d'utilisation de lambda à ce que ce ne soit pas un champs statique
        /// (=&gt; la lambda doit faire référence à la classe qui la contient
        /// </remarks>
        public void Subscribe<TMessage>(Action<TMessage> callback, ThreadingStrategy strategy) where TMessage : IMessage
        {
            Subscribe<TMessage>(callback, null, strategy);
        }

        /// <summary>
        /// S'abonne à un événement publié sur le bus
        /// </summary>
        /// <typeparam name="TMessage">type d'événement pour lequel l'abonnement est fait</typeparam>
        /// <param name="callback">handler utilisé lors de la levée de l'événement</param>
        /// <param name="filter">  
        /// filtre permettant de savoir si le handler doit être exécuté ou non
        /// </param>
        /// <param name="strategy">stratégie de threading lors de l'exécution du callback</param>
        /// <remarks>
        /// Attention en cas d'utilisation de lambda à ce que ce ne soit pas un champs statique
        /// (=&gt; la lambda doit faire référence à la classe qui la contient
        /// </remarks>
        public void Subscribe<TMessage>(Action<TMessage> callback, Predicate<TMessage> filter, ThreadingStrategy strategy) where TMessage : IMessage
        {
            ArgumentHelper.IsNotNull("callback", callback);
            ArgumentHelper.IsNotNull("callback.Target", callback.Target);

            lock (_subscribees)
            {
                Type eventType = typeof(TMessage);

                if (!_subscribees.ContainsKey(eventType))
                    _subscribees[eventType] = new List<Subscription>(1);

                _subscribees[eventType].Add(new Subscription(callback, filter, strategy));
            }
        }

        /// <summary>
        /// Permet de se désabonner de tous les événements du bus
        /// </summary>
        /// <param name="target">cible à supprimer des listes d'invocation</param>
        public void Unsubscribe(object target)
        {
            ArgumentHelper.IsNotNull("target", target);

            lock (_subscribees)
            {
                // Supprime toutes les actions référençant la cible
                _subscribees.ForEach(kv => kv.Value.RemoveWhere(s => s.Target != null && s.Target.Equals(target)));

                // Supprime les événements pour lesquels il n'y a plus d'actions;
                _subscribees.RemoveWhere(kv => kv.Value.Count == 0);
            }
        }

        /// <summary>
        /// Permet de se désabonner d'un événement particulier du bus
        /// </summary>
        /// <typeparam name="TEvent">type d'événement pour lequel l'abonnement est défait</typeparam>
        /// <param name="callback">callback à supprimer de la liste d'invocations du bus</param>
        public void Unsubscribe<TMessage>(Action<TMessage> callback) where TMessage : IMessage
        {
            ArgumentHelper.IsNotNull("callback", callback);

            lock (_subscribees)
            {
                List<Subscription> subscriptions;

                if (_subscribees.TryGetValue(typeof(TMessage), out subscriptions))
                {
                    // Supprime le callback
                    subscriptions.RemoveFirst(s => (Action<TMessage>)s.Action == callback);

                    // Supprime l'événement s'il n'y a plus d'actions
                    if (subscriptions.Count == 0)
                        _subscribees.Remove(typeof(TMessage));
                }
            }
        }

        /// <summary>
        /// Retourne le premier callback à invoquer en fonction de la stratégie de threading
        /// </summary>
        /// <typeparam name="TModalEvent">type de l'événement modal</typeparam>
        /// <param name="message">instance de l'événement modal</param>
        /// <returns>le premier callback à invoquer</returns>
        private Action<TMessage> GetCallback<TMessage, TResult>(TMessage message)
        {
            ArgumentHelper.IsNotNull("item", message);

            Type eventType = typeof(TMessage);
            List<Action<TMessage>> actions;

            lock (_subscribees)
            {
                // On coupe court s'il n'y a aucune souscription
                if (!_subscribees.ContainsKey(eventType))
                    return null;

                List<Subscription> subscriptions = _subscribees[eventType];
                actions = new List<Action<TMessage>>(subscriptions.Count);

                for (int i = subscriptions.Count - 1; i > -1; --i)
                {
                    Subscription sub = subscriptions[i];

                    Delegate action = sub.Action;
                    // Teste pour savoir si le délégué d'action est utilisable => la référence est
                    // encore en vie
                    if (action != null)
                    {
                        Delegate filter = sub.Filter;

                        // Teste l'existence d'un filtre, et s'il existe on l'exécute pour savoir si
                        // le callback doit être appelé
                        if (filter == null || ((Predicate<TMessage>)filter)(message))
                        {
                            if (sub.ThreadingStrategy == ThreadingStrategy.Publisher)
                                actions.Add(new Action<TMessage>(evt => action.DynamicInvoke(evt)));
                            else if (sub.ThreadingStrategy == ThreadingStrategy.Background)
                                actions.Add(new Action<TMessage>(evt => System.Threading.ThreadPool.QueueUserWorkItem(o => action.DynamicInvoke(o), evt)));
                            else if (sub.ThreadingStrategy == ThreadingStrategy.UI)
                                actions.Add(new Action<TMessage>(evt => System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(action, evt)));
                        }
                    }
                    else
                        // La référence est morte, on supprime la souscription
                        subscriptions.RemoveAt(i);
                }

                // S'il n'y a plus de souscription, on supprime l'événement
                if (subscriptions.Count == 0)
                    _subscribees.Remove(eventType);
            }

            // On renvoit le premier de la liste qui n'est autre que le dernier abonné chronologiquement
            return actions.FirstOrDefault();
        }

        /// <summary>
        /// Retourne la liste des callbacks à invoquer en fonction de la stratégie de threading
        /// </summary>
        /// <typeparam name="TEvent">type de l'événement</typeparam>
        /// <param name="item">instance de l'événement</param>
        /// <returns>la liste des callbacks à invoquer</returns>
        private List<Action<TMessage>> GetCallbacks<TMessage>(TMessage item)
        {
            ArgumentHelper.IsNotNull("item", item);

            Type eventType = typeof(TMessage);
            List<Action<TMessage>> actions;

            lock (_subscribees)
            {
                // On coupe court s'il n'y a aucune souscription
                if (!_subscribees.ContainsKey(eventType))
                    return null;

                List<Subscription> subscriptions = _subscribees[eventType];
                actions = new List<Action<TMessage>>(subscriptions.Count);

                for (int i = subscriptions.Count - 1; i > -1; --i)
                {
                    Subscription sub = subscriptions[i];

                    Delegate action = sub.Action;
                    // Teste pour savoir si le délégué d'action est utilisable => la référence est
                    // encore en vie
                    if (action != null)
                    {
                        Delegate filter = sub.Filter;

                        // Teste l'existence d'un filtre, et s'il existe on l'exécute pour savoir si
                        // le callback doit être appelé
                        if (filter == null || ((Predicate<TMessage>)filter)(item))
                        {
                            if (sub.ThreadingStrategy == ThreadingStrategy.Publisher)
                                actions.Add(new Action<TMessage>(evt => action.DynamicInvoke(evt)));
                            else if (sub.ThreadingStrategy == ThreadingStrategy.Background)
                                actions.Add(new Action<TMessage>(evt => System.Threading.ThreadPool.QueueUserWorkItem(o => action.DynamicInvoke(o), evt)));
                            else if (sub.ThreadingStrategy == ThreadingStrategy.UI)
                                actions.Add(new Action<TMessage>(evt => System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(action, evt)));
                        }
                    }
                    else
                        // La référence est morte, on supprime la souscription
                        subscriptions.RemoveAt(i);
                }

                // S'il n'y a plus de souscription, on supprime l'événement
                if (subscriptions.Count == 0)
                    _subscribees.Remove(eventType);
            }

            // Retourne les éléments pour qu'ils soient appelés dans leur ordre d'ajout initial
            actions.Reverse();

            return actions;
        }

        #endregion Methods
    }
}