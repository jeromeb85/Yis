namespace Yis.Framework.Model.Contract
{
    public interface IModel
    {
    }

    public interface IModel<TKey> : IModel
    {
        #region Properties

        TKey Id { get; set; }

        #endregion Properties
    }
}