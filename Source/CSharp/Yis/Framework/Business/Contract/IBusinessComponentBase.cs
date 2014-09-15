using System.Collections.Generic;

namespace Yis.Framework.Business.Contract
{
    public interface IBusinessComponentBase
    {
    }

    public interface IBusinessComponentBase<TModel>
    {
        #region Methods

        TModel Create();

        IEnumerable<TModel> GetAll();

        #endregion Methods
    }
}