﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Core.Locator.Contract
{
    public interface IServiceLocator
    {
        TInterface ResolveAndCreateType<TInterface>(object[] param = null);
        IEnumerable<Type> ResolveType<TInterface>();
        IEnumerable<TInterface> ResolveAndCreateAllType<TInterface>(object[] param = null);
    }
}