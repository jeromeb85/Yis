using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Erp.Mdm.Data.Contract;
using Yis.Framework.Data.Contract;
using Yis.Framework.Data.Memory;

namespace Yis.Erp.Mdm.Data.Memory
{
    public class ClientProvider  : RepositoryBase<Model.Client>, IClientProvider
    {

        public ClientProvider(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public Model.Client GetById(Guid id)
        {
            return GetQuery().First(t => t.Id == id);
        }

    }
}
