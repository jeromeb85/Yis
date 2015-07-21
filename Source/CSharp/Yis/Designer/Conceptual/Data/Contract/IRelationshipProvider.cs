using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Conceptual.Model;
using Yis.Framework.Data.Contract;

namespace Yis.Designer.Conceptual.Data.Contract
{
    public interface IRelationshipProvider : IRepository<Relationship>
    {
        IEnumerable<Relationship> GetBySourceId(Guid id);
        IEnumerable<Relationship> GetByDestinationId(Guid id);

        IEnumerable<Relationship> GetBySourceOrDestinationId(Guid id);
    }
}
