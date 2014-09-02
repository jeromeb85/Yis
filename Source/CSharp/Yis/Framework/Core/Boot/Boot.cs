
using Yis.Framework.Core.Locator;
using Yis.Framework.Core.IoC;
using Yis.Framework.Core.Logging;
using Yis.Framework.Core.Locator.Contract;
using Yis.Framework.Presentation.Locator.Contract;
using Yis.Framework.Presentation.Navigation.Contract;
using Yis.Framework.Core.Logging.Contract;
using Yis.Framework.Core.Shell.Contract;
using System.Collections.Generic;

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


    #endregion

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

