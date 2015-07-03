using Yis.Framework.Core.Extension;
using Yis.Framework.Core.IoC;
using Yis.Framework.Core.IoC.Contract;
using Yis.Framework.Core.Locator;
using Yis.Framework.Core.Locator.Contract;
using Yis.Framework.Core.Validation.Contract;

namespace Yis.Framework.Core.Validation
{
    internal class RuleEngineManager
    {
        #region Fields

        private static IRuleEngine _default;

        #endregion Fields

        #region Properties

        public static IRuleEngine Default
        {
            get
            {
                if (_default.IsNull())
                {
                    if (Resolver.IsRegistered<IRuleEngine>())
                    {
                        _default = Resolver.Resolve<IRuleEngine>();
                    }
                    else
                    {
                        _default = Locator.ResolveAndCreateType<IRuleEngine>();
                    }
                }
                return _default;
            }
        }

        private static IServiceLocator Locator
        {
            get { return ServiceLocatorManager.Default; }
        }

        private static IDependencyResolver Resolver
        {
            get { return DependencyResolverManager.Default; }
        }

        #endregion Properties
    }
}