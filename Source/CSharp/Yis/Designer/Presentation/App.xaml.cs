using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Yis.Designer.Presentation
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            #region CaTAL CODE
            //CATEL CODE
            //Catel.Logging.LogManager.AddDebugListener();// .RegisterDebugListener(); //Mis en commebntaire

            //StyleHelper.CreateStyleForwardersForDefaultStyles();

            //// TODO: Using a custom IoC container like Unity? Register it here:
            //// Catel.IoC.ServiceLocator.Instance.RegisterExternalContainer(MyUnityContainer);

            //var serviceLocator = ServiceLocator.Default;


            //serviceLocator.RegisterType<IViewLocator, ViewLocator>();
            //var viewLocator = serviceLocator.ResolveType<IViewLocator>();
            //viewLocator.NamingConventions.Add("[UP].View.[VM]");
            //viewLocator.NamingConventions.Add("[UP].View.[VM]Window");
            //viewLocator.NamingConventions.Add("[UP].View.[VM]View");
            //viewLocator.NamingConventions.Add("[UP].Views.LogicInBehavior.[VM]");
            //viewLocator.NamingConventions.Add("[UP].Views.LogicInBehavior.[VM]View");
            //viewLocator.NamingConventions.Add("[UP].Views.LogicInBehavior.[VM]Window");
            //viewLocator.NamingConventions.Add("[UP].Views.LogicInViewBase.[VM]");
            //viewLocator.NamingConventions.Add("[UP].Views.LogicInViewBase.[VM]View");
            //viewLocator.NamingConventions.Add("[UP].Views.LogicInViewBase.[VM]Window");


            //serviceLocator.RegisterType<IViewModelLocator, ViewModelLocator>();
            //var viewModelLocator = serviceLocator.ResolveType<IViewModelLocator>();
            ////viewModelLocator.NamingConventions.Add("Yis.Designer.Presentation.ViewModel.[VW]ViewModel");
            //viewModelLocator.NamingConventions.Add("[UP].ViewModel.[VW]ViewModel");
            //viewModelLocator.NamingConventions.Add("[AS].ViewModel.[VW]ViewModel");
            

            //viewModelLocator.NamingConventions.ForEach(s => (s = new string("r"));

            //viewModelLocator.NamingConventions.Add("[AS].toto.ViewModels.[VW]ViewModel");
            //viewModelLocator.NamingConventions.Add("[UP].toto.ViewModels.[VW]ViewModel");


            // Register several different external IoC containers for demo purposes
            //IoCHelper.MefContainer = new CompositionContainer();
            //IoCHelper.UnityContainer = new UnityContainer();
            //serviceLocator.RegisterExternalContainer(IoCHelper.MefContainer);
            //serviceLocator.RegisterExternalContainer(IoCHelper.UnityContainer);

            #endregion
            


            
            base.OnStartup(e);
        }
    }
}
