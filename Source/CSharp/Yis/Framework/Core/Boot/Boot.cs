
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

    private static IEnumerable<IShell> _shell;
    private static IEnumerable<IShell> Shell
    {
        get 
        {
            if (_shell.IsNull())
            {
                _shell = Locator.ResolveAndCreateAllType<IShell>();
            }
            return _shell;
        }
    }
        

    #endregion

    /// <summary>
    /// Démarrage
    /// </summary>
    public static void Start()
    {
        Log.Debug("Starting");

        //Initialize tous les Shell trouvés
        foreach (IShell item in Shell)
        {
            item.Initialize();
        }


        Log.Debug("Started");
    }
}

