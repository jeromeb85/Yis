using Yis.Framework.Core.Extension;
using Yis.Framework.Core.IoC;
using Yis.Framework.Core.IoC.Contract;
using Yis.Framework.Core.Locator;
using Yis.Framework.Core.Locator.Contract;
using Yis.Framework.Core.Messaging.Contract;

namespace Yis.Framework.Core.Messaging
{
    internal static class BusManager
    {
        #region Fields

        private static IBus _default;

        #endregion Fields

        #region Properties

        public static IBus Default
        {
            get
            {
                if (_default.IsNull())
                {
                    if (Resolver.IsRegistered<IBus>())
                    {
                        _default = Resolver.Resolve<IBus>();
                    }
                    else
                    {
                        _default = Locator.ResolveAndCreateType<IBus>();
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