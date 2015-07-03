namespace Yis.Framework.Core.Messaging.Contract
{
    public interface IMessage
    {
    }

    public interface IMessage<TResult> : IMessage
    {
        #region Properties

        TResult Result { get; set; }

        #endregion Properties
    }
}