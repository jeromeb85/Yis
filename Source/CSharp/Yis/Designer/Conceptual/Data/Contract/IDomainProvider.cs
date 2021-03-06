﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Conceptual.Model;
using Yis.Framework.Data.Contract;

namespace Yis.Designer.Conceptual.Data.Contract
{
    public interface IDomainProvider : IRepository<Domain>
    {
        #region Methods

        Domain GetById(Guid id);

        Domain GetByName(string name);

        bool IsExists(string name);

        #endregion Methods
    }
}