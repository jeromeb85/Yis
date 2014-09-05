using Yis.Framework.Core.Extension;
using Yis.Framework.Core.Locator.Contract;
using Yis.Framework.Core.Locator.Internal;

namespace Yis.Framework.Core.Locator
{
    internal static class ServiceLocatorManager
    {
        private static IServiceLocator _default;

        public static IServiceLocator Default
        {
            get
            {
                if (_default.IsNull())
                {
                    _default = new ServiceLocator();
                }
                return _default;
            }
        }
    }
}