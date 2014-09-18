using Yis.Framework.Core.IoC;
using Yis.Framework.Core.Locator;
using Yis.Framework.Core.Locator.Contract;
using Yis.Framework.Core.Logging;
using Yis.Framework.Core.Logging.Contract;
using Yis.Framework.Core.Messaging;
using Yis.Framework.Core.Messaging.Contract;
using Yis.Framework.Core.Serialization;
using Yis.Framework.Core.Serialization.Contract;
using Yis.Framework.Core.Shell.Contract;
using Yis.Framework.Presentation.Locator.Contract;
using Yis.Framework.Presentation.Navigation.Contract;

namespace Yis.Framework.Core.Shell
{
    internal class ShellFramework : IShell
    {
        #region Methods

        public void Initialize()
        {
            DependencyResolverManager.Default.Register<ILog>(LogManager.Default);
            DependencyResolverManager.Default.Register<IServiceLocator>(ServiceLocatorManager.Default);
            DependencyResolverManager.Default.Register<IBus>(BusManager.Default);
            DependencyResolverManager.Default.Register<ISerializer>(SerializerManager.Default);

            DependencyResolverManager.Default.Register<IViewModelLocator>(ServiceLocatorManager.Default.ResolveAndCreateType<IViewModelLocator>());
            DependencyResolverManager.Default.Register<IViewLocator>(ServiceLocatorManager.Default.ResolveAndCreateType<IViewLocator>());
            DependencyResolverManager.Default.Register<INavigation>(ServiceLocatorManager.Default.ResolveAndCreateType<INavigation>());
        }

        #endregion Methods
    }
}