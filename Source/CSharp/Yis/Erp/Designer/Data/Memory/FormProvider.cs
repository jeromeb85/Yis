using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Erp.Designer.Data.Contract;
using Yis.Framework.Data.Contract;
using Yis.Framework.Data.Memory;

namespace Yis.Erp.Designer.Data.Memory
{
    public class ClientProvider  : RepositoryBase<Model.Form>, IFormProvider
    {

        public ClientProvider(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public Model.Form GetById(Guid id)
        {
            return GetQuery().First(t => t.Id == id);
        }

    }
}
