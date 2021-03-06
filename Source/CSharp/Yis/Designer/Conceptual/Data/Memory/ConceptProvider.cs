﻿using System;
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
    public class ConceptProvider : RepositoryBase<Concept>, IConceptProvider
    {
        #region Constructors

        public ConceptProvider(IDataContext dataContext)
            : base(dataContext)
        {
        }

        #endregion Constructors

        #region Methods

        public IEnumerable<Concept> GetByDomain(Guid domainId)
        {
            return GetQuery().Where(t => t.DomainId == domainId);
        }

        public Concept GetById(Guid id)
        {
            return GetQuery().First(t => t.Id == id);
        }

        public Concept GetByName(string name)
        {
            return GetQuery().First(t => t.Name == name);
        }

        #endregion Methods
    }
}