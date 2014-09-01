using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
