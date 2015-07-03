using System;
using System.Linq;
using Yis.Erp.Mdm.Data.Contract;
using Yis.Framework.Data.Contract;
using Yis.Framework.Data.Memory;

namespace Yis.Erp.Mdm.Data.Memory
{
    public class ClientProvider : RepositoryBase<Model.Client>, IClientProvider
    {
        #region Constructors + Destructors

        public ClientProvider(IDataContext dataContext)
            : base(dataContext)
        {
        }

        #endregion Constructors + Destructors

        #region Methods

        public Model.Client GetById(Guid id)
        {
            return GetQuery().First(t => t.Id == id);
        }

        #endregion Methods
    }
}