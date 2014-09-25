using Yis.Framework.Business.Contract;

namespace Yis.Framework.Model.Contract
{
    public interface IModel : IBusinessObject
    {
    }

    public interface IModel<TKey> : IModel
    {
        #region Properties

        TKey Id { get; set; }

        #endregion Properties
    }
}