using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Core.Messaging.Event
{
    public class NotificationMessage : Message
    {
        /// <summary>
        /// Obtient le message envoyé
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="sender">élément à l'origine de l'événement</param>
        /// <param name="message">message envoyé</param>
        public NotificationMessage(object sender, string message)
            : base(sender)
        {
            Message = message;
        }
    }
}