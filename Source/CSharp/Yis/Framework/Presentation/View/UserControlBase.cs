using System;
using System.Windows.Controls;
using Yis.Framework.Core.IoC;
using Yis.Framework.Presentation.Locator.Contract;

namespace Yis.Framework.Presentation.View
{
    public class UserControlBase : UserControl, IView
    {
           private IViewModelLocator _locator;

        protected IViewModelLocator Locator
        {
            get
            {
                if (_locator == null)
                {
                    _locator = DependencyResolverManager.Default.Resolve<IViewModelLocator>();
                }

                return _locator;
            }
        }

        protected UserControlBase()
        {
        }

        protected UserControlBase(bool searchViewModel)
        {
            DataContext = Activator.CreateInstance(Locator.ResolveViewModel(this.GetType()));
        }
    }
}