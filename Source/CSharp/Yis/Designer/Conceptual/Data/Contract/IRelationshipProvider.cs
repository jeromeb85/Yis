using System;
using System.Collections.Generic;
using Yis.Designer.Conceptual.Model;
using Yis.Framework.Data.Contract;

namespace Yis.Designer.Conceptual.Data.Contract
{
    public interface IRelationshipProvider : IRepository<Relationship>
    {
        #region Methods

        IEnumerable<Relationship> GetByDestinationId(Guid id);

        IEnumerable<Relationship> GetBySourceId(Guid id);

        IEnumerable<Relationship> GetBySourceOrDestinationId(Guid id);

        #endregion Methods
    }
}