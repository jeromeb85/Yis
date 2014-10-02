using System;
using System.Collections.Generic;
using System.Linq;
using Yis.Designer.Software.Data.Contract;
using Yis.Designer.Software.Model;
using Yis.Framework.Data.Contract;
using Yis.Framework.Data.Memory;

namespace Yis.Designer.Software.Data.Memory
{
    public class NameSpaceProvider : RepositoryBase<NameSpace>, INameSpaceProvider
    {
        #region Methods

        public bool IsExists(string name)
        {
            return GetQuery().Any(t => t.Name == name);
        }

        #endregion Methods

        #region Constructors

        public NameSpaceProvider(IDataContext dataContext)
            : base(dataContext)
        {
        }

        #endregion Constructors

        public NameSpace GetById(Guid id)
        {
            return GetQuery().First(t => t.Id == id);
        }

        public NameSpace GetByName(string name)
        {
            return GetQuery().First(t => t.Name == name);
        }

        public IEnumerable<NameSpace> GetChildByParent(Guid id)
        {
            return GetQuery().Where(t => t.ParentNameSpaceId == id);
        }
    }
}