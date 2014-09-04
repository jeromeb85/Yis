using Yis.Framework.Core.Locator.Contract;

namespace Yis.Framework.Core.Locator
{
    public static class ServiceLocatorManager
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