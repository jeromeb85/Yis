using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.Helper;
using Yis.Framework.Core.Messaging.Contract;

namespace Yis.Framework.Core.Messaging.Event
{
    /// <summary>
    /// Classe de base des Message du bus d'événements
    /// </summary>
    public class Message : IMessage
    {
        #region Constructors

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="sender">élément à l'origine de l'événement</param>
        protected Message(object sender)
        {
            ArgumentHelper.IsNotNull("sender", sender);

            Sender = sender;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Obtient l'élément à l'origine de l'événement
        /// </summary>
        public object Sender { get; internal set; }

        #endregion Properties
    }

    /// <summary>
    /// Classe de base des événements modaux du bus d'événements.
    /// Par modal, on entend un événement qui attend une réponse
    /// </summary>
    public abstract class Message<TResult> : Message, IMessage<TResult>
    {
        #region Constructors

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="sender">élément à l'origine de l'événement</param>
        protected Message(object sender)
            : this(sender, default(TResult))
        {
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="sender">élément à l'origine de l'événement</param>
        /// <param name="defaultResult">résultat par défaut de l'événement</param>
        protected Message(object sender, TResult defaultResult)
            : base(sender)
        {
            Result = defaultResult;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Obtient ou définit le résultat de l'événement modal
        /// </summary>
        public TResult Result { get; set; }

        #endregion Properties
    }
}