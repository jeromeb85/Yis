using System;
using System.Linq;
using Yis.Designer.Conceptual.Data.Contract;
using Yis.Designer.Conceptual.Model;
using Yis.Framework.Data.Contract;
using Yis.Framework.Data.Memory;

namespace Yis.Designer.Conceptual.Data.Memory
{
    public class DomainProvider : RepositoryBase<Domain>, IDomainProvider
    {
        #region Methods

        public bool IsExists(string name)
        {
            return GetQuery().Any(t => t.Name == name);
        }

        #endregion Methods

        #region Constructors + Destructors

        public DomainProvider(IDataContext dataContext)
            : base(dataContext)
        {
        }

        #endregion Constructors + Destructors

        public Domain GetById(Guid id)
        {
            return GetQuery().First(t => t.Id == id);
        }

        public Domain GetByName(string name)
        {
            return GetQuery().First(t => t.Name == name);
        }
    }
}