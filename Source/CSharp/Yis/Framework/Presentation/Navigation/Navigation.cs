using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;
using Yis.Framework.Core.IoC;
using Yis.Framework.Presentation.Locator;
using Yis.Framework.Core.Fluent;
using Yis.Framework.Core.Caching;
using Yis.Framework.Presentation.View;

namespace Yis.Framework.Presentation.Navigation
{
    public class Navigation : INavigation
    {
        IViewLocator _locator;

        private static readonly ICacheStorage<string, PropertyInfo> _availableProperties = new CacheStorage<string, PropertyInfo>();

        protected IViewLocator Locator
        {
            get
            {
                if (_locator == null)
                {
                    _locator = DependencyResolver.Resolve<IViewLocator>();
                }

                return _locator;
            }
        }




        public void Show<T>(object context = null) where T : View.IView
        {
            Type viewtype = Locator.ResolveView<T>();
            IWindowView window = CreateWindow(viewtype);
            window.Show(context);

            //var showMethodInfo = window.GetType().GetMethod("Show");
            //window.Dispatcher.Invoke(() => showMethodInfo.Invoke(window, null));
        }

        public bool? ShowModal<T>(object context = null) where T : View.IView
        {
            Type viewtype = Locator.ResolveView<T>();
            IWindowView window = CreateWindow(viewtype);
            return window.ShowModal(context);
            //var showDialogMethodInfo = window.GetType().GetMethod("ShowDialog");

            //if (showDialogMethodInfo != null)
            //{
            //    // Child window does not have a ShowDialog, so not null is allowed
            //    bool? result = null;
            //    window.Dispatcher.Invoke(() => result = showDialogMethodInfo.Invoke(window, null) as bool?);
            //    // return result;
            //}
        }


        protected virtual IWindowView CreateWindow(Type viewType)
        {
            FrameworkElement window = null;

            //if (context != null)
            //{
            //    var injectionConstructor = viewType.GetConstructor(new[] { context.GetType() });

            //    if (injectionConstructor != null)
            //    {
            //        window = (FrameworkElement)injectionConstructor.Invoke(new[] { context });
            //    }
            //}


            //if (window == null)
            //{
                var defaultConstructor = viewType.GetConstructor(new Type[0]);
                window = (FrameworkElement) defaultConstructor.Invoke(null);

                //if (context != null)
                //{
                //    window.DataContext = context;
                //}
            //}



            var activeWindow = GetActiveWindowForApplication(Application.Current);

            if (window != activeWindow)
            {
                TrySetPropertyValue(window, "Owner", activeWindow, true);
            }


            return (IWindowView)window;
        }

        /*Objet extension  / Propertyhelper*/
        private static bool TrySetPropertyValue(object obj, string property, object value, bool throwOnError)
        {
            Argument.IsNotNull("obj", obj);
            Argument.IsNotNullOrWhitespace("property", property);

            var propertyInfo = GetPropertyInfo(obj, property);
            if (propertyInfo == null)
            {

                if (throwOnError)
                {
                    throw new Exception(property);
                }

                return false;
            }

            if (!propertyInfo.CanWrite)
            {
                if (throwOnError)
                {
                    throw new Exception(property);
                }

                return false;
            }


            var setMethod = propertyInfo.GetSetMethod(true);
            if (setMethod == null)
            {
                if (throwOnError)
                {
                    throw new Exception(property);
                }

                return false;
            }

            setMethod.Invoke(obj, new[] { value });

            return true;
        }

        /*Objet extension  / Propertyhelper*/
        private static PropertyInfo GetPropertyInfo(object obj, string property)
        {
            string cacheKey = string.Format("{0}_{1}", obj.GetType().FullName, property);
            return _availableProperties.GetFromCacheOrFetch(cacheKey, () => obj.GetType().GetProperty(property));
        }

        /*Application Extension*/
        private static System.Windows.Window GetActiveWindowForApplication(System.Windows.Application application)
        {
            System.Windows.Window activeWindow = null;

            if (application != null && application.Windows.Count > 0)
            {
                var windowList = new List<System.Windows.Window>(application.Windows.Cast<System.Windows.Window>());
                activeWindow = windowList.FirstOrDefault(cur => cur.IsActive);
                if (activeWindow == null && windowList.Count == 1 && windowList[0].Topmost)
                {
                    activeWindow = windowList[0];
                }
            }

            return activeWindow;
        }



        //public static FrameworkElement ConstructViewWithViewModel(Type viewType, object dataContext)
        //{
        //    Argument.IsNotNull("viewType", viewType);

        //    Log.Debug("Constructing view for view type '{0}'", viewType.Name);

        //    FrameworkElement view;

        //    // First, try to constructor directly with the data context
        //    if (dataContext != null)
        //    {
        //        var injectionConstructor = viewType.GetConstructorEx(new[] { dataContext.GetType() });
        //        if (injectionConstructor != null)
        //        {
        //            view = (FrameworkElement)injectionConstructor.Invoke(new[] { dataContext });

        //            Log.Debug("Constructed view using injection constructor");

        //            return view;
        //        }
        //    }

        //    Log.Debug("No constructor with data (of type '{0}') injection found, trying default constructor", ObjectToStringHelper.ToTypeString(dataContext));

        //    // Try default constructor
        //    var defaultConstructor = viewType.GetConstructorEx(new Type[0]);
        //    if (defaultConstructor == null)
        //    {
        //        Log.Error("View '{0}' does not have an injection or default constructor thus cannot be constructed", viewType.Name);
        //        return null;
        //    }

        //    try
        //    {
        //        view = (FrameworkElement)defaultConstructor.Invoke(null);
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = string.Format("Failed to construct view '{0}' with both injection and empty constructor", viewType.Name);
        //        Log.Error(ex, error);
        //        throw new InvalidOperationException(error, ex);
        //    }

        //    view.DataContext = dataContext;

        //    Log.Debug("Constructed view using default constructor and setting DataContext afterwards");

        //    return view;
        //}

    }
}
