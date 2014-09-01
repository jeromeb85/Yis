
using Yis.Framework.Core.IoC;
using Yis.Framework.Core.Logging;
using Yis.Framework.Presentation.Locator;
using Yis.Framework.Presentation.Navigation;
using Yis.Framework.Core.Locator;
using Yis.Framework.Core.Locator.Contract;
using Yis.Framework.Presentation.Locator.Contract;

/// <summary>
/// Gère le démarrage de tous l'environnement
/// </summary>
public static class Boot
{
    #region Propriétés privées
    #endregion

    /// <summary>
    /// Démarrage
    /// </summary>
    public static void Start()
    {
        DependencyResolverManager.Default.Register<IViewModelLocator>(ServiceLocatorManager.Default.ResolveAndCreateType<IViewModelLocator>());
        DependencyResolverManager.Default.Register<IViewLocator>(ServiceLocatorManager.Default.ResolveAndCreateType<IViewLocator>());
        DependencyResolverManager.Default.Register<INavigation>(ServiceLocatorManager.Default.ResolveAndCreateType<INavigation>());
    }
}

