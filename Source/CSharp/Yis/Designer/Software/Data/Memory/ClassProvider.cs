using System;
using System.Collections.Generic;
using System.Linq;
using Yis.Designer.Software.Data.Contract;
using Yis.Designer.Software.Model;
using Yis.Framework.Data.Contract;
using Yis.Framework.Data.Memory;

namespace Yis.Designer.Software.Data.Memory
{
    public class ClassProvider : RepositoryBase<Class>, IClassProvider
    {
        #region Constructors + Destructors

        public ClassProvider(IDataContext dataContext)
            : base(dataContext)
        {
        }

        #endregion Constructors + Destructors

        #region Methods

        public Class GetById(Guid id)
        {
            return GetQuery().First(t => t.Id == id);
        }

        public IEnumerable<Class> GetByNameSpace(Guid id)
        {
            return GetQuery().Where(t => t.NameSpaceId == id);
        }

        #endregion Methods
    }
}