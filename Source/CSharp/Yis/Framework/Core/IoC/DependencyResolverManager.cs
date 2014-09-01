using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.Factory;
using Yis.Framework.Core.Locator;

namespace Yis.Framework.Core.IoC
{
    public static class DependencyResolverManager
    {

        private static IDependencyResolver _default;
        public static IDependencyResolver Default
        {
            get
            {
                if (_default.IsNull())
                {
                    _default = ServiceLocatorManager.Default.ResolveAndCreateType<IDependencyResolver>();
                }
                return _default;
            }
        }
    }

}
