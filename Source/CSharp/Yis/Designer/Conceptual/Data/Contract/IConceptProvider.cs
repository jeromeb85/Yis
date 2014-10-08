using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Conceptual.Business;
using Yis.Framework.Data.Contract;

namespace Yis.Designer.Conceptual.Data.Contract
{
    public interface IConceptProvider : IRepository<Concept>
    {
        #region Methods

        IEnumerable<Concept> GetByDomain(Guid id);

        Concept GetById(Guid id);

        #endregion Methods
    }
}