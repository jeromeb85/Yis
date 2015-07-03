using System;

namespace Yis.Framework.Core.Messaging.Contract
{
    /// <summary>
    /// Définit le contrat d'un bus d'événements
    /// </summary>
    public interface IBus
    {
        #region Methods

        /// <summary>
        /// Publie un événement sur le bus
        /// </summary>
        /// <typeparam name="TEvent">type d'événement à publier</typeparam>
        /// <param name="item">événement à publier</param>
        void Publish<TMessage>(TMessage message) where TMessage : IMessage;

        /// <summary> Publie un événement modal sur le bus </summary> <typeparam
        /// name="TModalEvent<TResult>">type d'événement modal à publier</typeparam> <param
        /// name="item">événement modal à publier</param> <returns>le résultat de l'événement modal</returns>
        TResult Publish<TMessage, TResult>(TMessage message) where TMessage : IMessage<TResult>;

        /// <summary>
        /// S'abonne à un événement publié sur le bus
        /// </summary>
        /// <typeparam name="TEvent">type d'événement pour lequel l'abonnement est fait</typeparam>
        /// <param name="callback">handler utilisé lors de la levée de l'événement</param>
        /// <remarks>
        /// Attention en cas d'utilisation de lambda à ce que ce ne soit pas un champs statique
        /// (=&gt; la lambda doit faire référence à la classe qui la contient
        /// </remarks>
        void Subscribe<TMessage>(Action<TMessage> callback) where TMessage : IMessage;

        /// <summary>
        /// S'abonne à un événement publié sur le bus
        /// </summary>
        /// <typeparam name="TEvent">type d'événement pour lequel l'abonnement est fait</typeparam>
        /// <param name="callback">handler utilisé lors de la levée de l'événement</param>
        /// <param name="filter">  
        /// filtre permettant de savoir si le handler doit être exécuté ou non
        /// </param>
        /// <returns>l'instance du bus pour une écriture fluide</returns>
        /// <remarks>
        /// Attention en cas d'utilisation de lambda à ce que ce ne soit pas un champs statique
        /// (=&gt; la lambda doit faire référence à la classe qui la contient
        /// </remarks>
        void Subscribe<TMessage>(Action<TMessage> callback, Predicate<TMessage> filter) where TMessage : IMessage;

        /// <summary>
        /// S'abonne à un événement publié sur le bus
        /// </summary>
        /// <typeparam name="TEvent">type d'événement pour lequel l'abonnement est fait</typeparam>
        /// <param name="callback">handler utilisé lors de la levée de l'événement</param>
        /// <param name="strategy">stratégie de threading lors de l'exécution du callback</param>
        /// <remarks>
        /// Attention en cas d'utilisation de lambda à ce que ce ne soit pas un champs statique
        /// (=&gt; la lambda doit faire référence à la classe qui la contient
        /// </remarks>
        void Subscribe<TMessage>(Action<TMessage> callback, ThreadingStrategy strategy) where TMessage : IMessage;

        /// <summary>
        /// S'abonne à un événement publié sur le bus
        /// </summary>
        /// <typeparam name="TEvent">type d'événement pour lequel l'abonnement est fait</typeparam>
        /// <param name="callback">handler utilisé lors de la levée de l'événement</param>
        /// <param name="filter">  
        /// filtre permettant de savoir si le handler doit être exécuté ou non
        /// </param>
        /// <param name="strategy">stratégie de threading lors de l'exécution du callback</param>
        /// <remarks>
        /// Attention en cas d'utilisation de lambda à ce que ce ne soit pas un champs statique
        /// (=&gt; la lambda doit faire référence à la classe qui la contient
        /// </remarks>
        void Subscribe<TMessage>(Action<TMessage> callback, Predicate<TMessage> filter, ThreadingStrategy strategy) where TMessage : IMessage;

        /// <summary>
        /// Permet de se désabonner de tous les événements du bus
        /// </summary>
        /// <param name="target">cible à supprimer des listes d'invocation</param>
        void Unsubscribe(object target);

        /// <summary>
        /// Permet de se désabonner d'un événement particulier du bus
        /// </summary>
        /// <typeparam name="TEvent">type d'événement pour lequel l'abonnement est défait</typeparam>
        /// <param name="callback">callback à supprimer de la liste d'invocations du bus</param>
        void Unsubscribe<TMessage>(Action<TMessage> callback) where TMessage : IMessage;

        #endregion Methods
    }
}