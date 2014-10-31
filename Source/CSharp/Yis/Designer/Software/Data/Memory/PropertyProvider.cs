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
    public class PropertyProvider : RepositoryBase<Property>, IPropertyProvider
    {
        #region Constructors

        public PropertyProvider(IDataContext dataContext)
            : base(dataContext)
        {
        }

        #endregion Constructors

        #region Methods

        public IEnumerable<Property> GetByOwner(Guid id)
        {
            return GetQuery().Where(t => t.OwnerId == id);
        }

        #endregion Methods
    }
}