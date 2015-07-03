using Yis.Framework.Core.Extension;
using Yis.Framework.Core.IoC;
using Yis.Framework.Core.IoC.Contract;
using Yis.Framework.Core.Locator;
using Yis.Framework.Core.Locator.Contract;
using Yis.Framework.Core.Serialization.Contract;

namespace Yis.Framework.Core.Serialization
{
    internal static class SerializerManager
    {
        #region Fields

        private static ISerializer _default;

        #endregion Fields

        #region Properties

        public static ISerializer Default
        {
            get
            {
                if (_default.IsNull())
                {
                    if (Resolver.IsRegistered<ISerializer>())
                    {
                        _default = Resolver.Resolve<ISerializer>();
                    }
                    else
                    {
                        _default = Locator.ResolveAndCreateType<IXmlSerializer>();
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