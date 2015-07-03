using System;
using System.Collections.Generic;
using Yis.Designer.Software.Model;
using Yis.Framework.Data.Contract;

namespace Yis.Designer.Software.Data.Contract
{
    public interface IClassProvider : IRepository<Class>
    {
        #region Methods

        Class GetById(Guid id);

        IEnumerable<Class> GetByNameSpace(Guid id);

        #endregion Methods
    }
}