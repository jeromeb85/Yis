using Yis.Framework.Core.Extension;
using Yis.Framework.Core.IoC;
using Yis.Framework.Core.Locator;
using Yis.Framework.Core.Logging.Contract;

namespace Yis.Framework.Core.Logging
{
    /// <summary>
    /// Classe fournissant les instances de logs
    /// </summary>
    internal static class LogManager
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