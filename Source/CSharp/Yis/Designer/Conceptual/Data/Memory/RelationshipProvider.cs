using System;
using System.Collections.Generic;
using System.Linq;
using Yis.Designer.Conceptual.Data.Contract;
using Yis.Designer.Conceptual.Model;
using Yis.Framework.Data.Contract;
using Yis.Framework.Data.Memory;

namespace Yis.Designer.Conceptual.Data.Memory
{
    public class RelationshipProvider : RepositoryBase<Yis.Designer.Conceptual.Model.Relationship>, IRelationshipProvider
    {
        #region Constructors + Destructors

        public RelationshipProvider(IDataContext dataContext)
            : base(dataContext)
        {
        }

        #endregion Constructors + Destructors

        #region Methods

        public IEnumerable<Relationship> GetByDestinationId(Guid id)
        {
            return GetQuery().Where(t => t.IdConceptPointDestination == id);
        }

        public IEnumerable<Relationship> GetBySourceId(Guid id)
        {
            return GetQuery().Where(t => t.IdConceptPointSource == id);
        }

        public IEnumerable<Relationship> GetBySourceOrDestinationId(Guid id)
        {
            return GetQuery().Where(t => t.IdConceptPointDestination == id || t.IdConceptPointSource == id);
        }

        #endregion Methods
    }
}