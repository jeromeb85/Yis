using System;
using Yis.Framework.Data.Contract;

namespace Yis.Erp.Mdm.Data.Contract
{
    public interface IClientProvider : IRepository<Model.Client>
    {
        #region Methods

        Model.Client GetById(Guid id);

        #endregion Methods
    }
}