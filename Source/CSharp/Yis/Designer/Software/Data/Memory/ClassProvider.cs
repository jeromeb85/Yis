using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Software.Data.Contract;
using Yis.Designer.Software.Model;
using Yis.Framework.Data.Contract;
using Yis.Framework.Data.Memory;

namespace Yis.Designer.Software.Data.Memory
{
    public class ClassProvider : RepositoryBase<Class>, IClassProvider
    {
        #region Constructors

        public ClassProvider(IDataContext dataContext)
            : base(dataContext)
        {
        }

        #endregion Constructors

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