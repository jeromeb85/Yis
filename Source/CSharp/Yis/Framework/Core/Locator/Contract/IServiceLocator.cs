using System;
using System.Collections.Generic;

namespace Yis.Framework.Core.Locator.Contract
{
    public interface IServiceLocator
    {
        object ResolveAndCreateType(Type typeInterface, object[] param = null);

        TInterface ResolveAndCreateType<TInterface>(object[] param = null);

        IEnumerable<Type> ResolveType<TInterface>();

        IEnumerable<TInterface> ResolveAndCreateAllType<TInterface>(object[] param = null);
    }
}