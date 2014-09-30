using System;
using System.Collections.Generic;
using Yis.Designer.Software.Model;
using Yis.Framework.Data.Contract;

namespace Yis.Designer.Software.Data.Contract
{
    public interface INameSpaceProvider : IRepository<NameSpace>
    {
        #region Methods

        NameSpace GetByName(string name);

        IEnumerable<NameSpace> GetByParent(Guid id);

        bool IsExists(string name);

        #endregion Methods
    }
}