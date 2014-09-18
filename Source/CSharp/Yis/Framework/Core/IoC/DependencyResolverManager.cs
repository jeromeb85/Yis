using Yis.Framework.Core.Extension;
using Yis.Framework.Core.IoC.Contract;
using Yis.Framework.Core.Locator;

namespace Yis.Framework.Core.IoC
{
    public static class DependencyResolverManager
    {
        #region Fields

        private static IDependencyResolver _default;

        #endregion Fields

        #region Properties

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

        #endregion Properties
    }
}