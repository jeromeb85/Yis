using System.Collections.Generic;

namespace Yis.Framework.Business.Contract
{
    public interface IBusinessComponentBase
    {
    }

    public interface IBusinessComponentBase<TModel>
    {
        IEnumerable<TModel> GetAll();
    }
}