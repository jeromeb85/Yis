using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.Factory;
using Yis.Framework.Core.IoC;
using Yis.Framework.Core.Locator;
using Yis.Framework.Core.Locator.Contract;
using Yis.Framework.Core.Logging.Contract;

namespace Yis.Framework.Core.Logging
{
    /// <summary>
    /// Classe fournissant les instances de logs
    /// </summary>
    public static class LogManager
    {
        
        private static ILog _default;
        public static ILog Default
        {
            get 
            {
                if (_default.IsNull())
                {
                    if (DependencyResolverManager.Default.IsRegistered<ILog>())
                    {
                        _default = DependencyResolverManager.Default.Resolve<ILog>();
                    }
                    else
                    {
                        _default = ServiceLocatorManager.Default.ResolveAndCreateType<ILog>();
                    }
                }
                return _default;
            }
        }
    }

}
