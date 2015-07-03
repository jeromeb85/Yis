namespace Yis.Framework.Core.Messaging.Event
{
    public class NotificationMessage : Message
    {
        #region Constructors + Destructors

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="sender"> élément à l'origine de l'événement</param>
        /// <param name="message">message envoyé</param>
        public NotificationMessage(object sender, string message)
            : base(sender)
        {
            Message = message;
        }

        #endregion Constructors + Destructors

        #region Properties

        /// <summary>
        /// Obtient le message envoyé
        /// </summary>
        public string Message { get; private set; }

        #endregion Properties
    }
}