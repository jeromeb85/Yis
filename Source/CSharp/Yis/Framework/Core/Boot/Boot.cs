using Yis.Framework.Core.Locator;
using Yis.Framework.Core.Locator.Contract;
using Yis.Framework.Core.Logging;
using Yis.Framework.Core.Logging.Contract;
using Yis.Framework.Core.Shell.Contract;

/// <summary>
/// Gère le démarrage de tous l'environnement
/// </summary>
public static class Boot
{
    #region Propriétés privées

    private static ILog Log
    {
        get { return LogManager.Default; }
    }

    private static IServiceLocator Locator
    {
        get { return ServiceLocatorManager.Default; }
    }

    #endregion Propriétés privées

    /// <summary>
    /// Démarrage
    /// </summary>
    public static void Start()
    {
        Log.Debug("Starting");

        //Initialize tous les Shell trouvés
        foreach (IShell item in Locator.ResolveAndCreateAllType<IShell>())
        {
            item.Initialize();
        }

        Log.Debug("Started");
    }
}