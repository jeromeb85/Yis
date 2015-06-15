using System;
using System.Windows;
using System.Windows.Controls.Ribbon;
using Yis.Framework.Core.IoC;
using Yis.Framework.Presentation.Locator.Contract;

namespace Yis.Framework.Presentation.View
{
    public abstract class WindowBase :  RibbonWindow, IWindowView
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

        protected WindowBase()
        {
        }

        protected WindowBase(bool searchViewModel)
        {
            DataContext = Activator.CreateInstance(Locator.ResolveViewModel(this.GetType()));
        }

        public void Show(object context = null)
        {
            if (context != null)
            {
                DataContext = context;
            }
            else
            {
                DataContext = Activator.CreateInstance(Locator.ResolveViewModel(this.GetType()));
            }

            base.Show();
        }

        public bool? ShowModal(object context = null)
        {
            if (context != null)
            {
                DataContext = context;
            }
            else
            {
                DataContext = Activator.CreateInstance(Locator.ResolveViewModel(this.GetType()));
            }

            return base.ShowDialog();
        }
    }
}