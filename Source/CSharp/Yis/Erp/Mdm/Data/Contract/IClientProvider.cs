using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Data.Contract;

namespace Yis.Erp.Mdm.Data.Contract
{
    public interface IClientProvider : IRepository<Model.Client>
    {
        Model.Client GetById(Guid id);
    }
}
