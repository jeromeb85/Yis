using System;
using System.Collections.Generic;
using Yis.Designer.Conceptual.Model;
using Yis.Framework.Data.Contract;

namespace Yis.Designer.Conceptual.Data.Contract
{
    public interface IConceptProvider : IRepository<Concept>
    {
        #region Methods

        IEnumerable<Concept> GetByDomain(Guid id);

        Concept GetById(Guid id);

        Concept GetByName(string name);

        #endregion Methods
    }
}