using System;

namespace Yis.Framework.Core.Messaging.Contract
{
    public interface IBus
    {
        #region Methods

        void Publish<TMessage>(TMessage message);

        void Publish(Object message);

        void Subscribe(Action handler);

        void Unsubscribe(Action handler);

        #endregion Methods
    }
}