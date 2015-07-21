using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Conceptual.Model;
using Yis.Designer.Conceptual.Data.Contract;
using Yis.Framework.Data.Contract;
using Yis.Framework.Data.Memory;

namespace Yis.Designer.Conceptual.Data.Memory
{
    public class RelationshipProvider : RepositoryBase<Yis.Designer.Conceptual.Model.Relationship>, IRelationshipProvider
    {
        public RelationshipProvider(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public IEnumerable<Relationship> GetBySourceId(Guid id)
        {
            return GetQuery().Where(t => t.IdConceptPointSource == id);
        }

        public IEnumerable<Relationship> GetByDestinationId(Guid id)
        {
            return GetQuery().Where(t => t.IdConceptPointDestination == id);
        }

        public IEnumerable<Relationship> GetBySourceOrDestinationId(Guid id)
        {
            return GetQuery().Where(t => t.IdConceptPointDestination == id || t.IdConceptPointSource == id);
        }
    }
}
