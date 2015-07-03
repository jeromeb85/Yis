using System;
using System.Collections.Generic;
using Yis.Framework.Data.Contract;

namespace Yis.Designer.Conceptual.Data.Contract
{
    public interface IAttributeProvider : IRepository<Model.Attribute>
    {
        #region Methods

        IEnumerable<Model.Attribute> GetByConcept(Guid id);

        Model.Attribute GetById(Guid id);

        Model.Attribute GetByName(string name);

        #endregion Methods
    }
}