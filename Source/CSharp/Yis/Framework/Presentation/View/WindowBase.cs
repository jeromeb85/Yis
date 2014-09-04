using System.Windows;
using Yis.Framework.Core.Factory;
using Yis.Framework.Core.IoC;
using Yis.Framework.Presentation.Locator.Contract;

namespace Yis.Framework.Presentation.View
{
    public abstract class WindowBase : Window/* RibbonWindow,*/, IWindowView
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
            DataContext = (new Factory(Locator.ResolveViewModel(this.GetType()))).CreateInstance();
        }

        public void Show(object context = null)
        {
            if (context != null)
            {
                DataContext = context;
            }
            else
            {
                DataContext = (new Factory(Locator.ResolveViewModel(this.GetType()))).CreateInstance();
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
                DataContext = (new Factory(Locator.ResolveViewModel(this.GetType()))).CreateInstance();
            }

            return base.ShowDialog();
        }
    }
}