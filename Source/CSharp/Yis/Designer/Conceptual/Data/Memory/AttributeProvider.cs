using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Conceptual.Data.Contract;
using Yis.Designer.Conceptual.Model;
using Yis.Framework.Data.Contract;
using Yis.Framework.Data.Memory;

namespace Yis.Designer.Conceptual.Data.Memory
{
    public class AttributeProvider : RepositoryBase<Yis.Designer.Conceptual.Model.Attribute>, IAttributeProvider
    {
        #region Constructors

        public AttributeProvider(IDataContext dataContext)
            : base(dataContext)
        {
        }

        #endregion Constructors

        #region Methods

        public IEnumerable<Model.Attribute> GetByConcept(Guid idConcept)
        {
            return GetQuery().Where(t => t.ConceptId == idConcept);
        }

        public Model.Attribute GetById(Guid id)
        {
            return GetQuery().First(t => t.Id == id);
        }

        public Model.Attribute GetByName(string name)
        {
            return GetQuery().First(t => t.Name == name);
        }

        #endregion Methods
    }
}