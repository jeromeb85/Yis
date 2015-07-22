using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls.Ribbon;
using Yis.Erp.Shell.Presentation.Contract;
using Yis.Erp.Shell.Presentation.Event;
using Yis.Framework.Presentation.ViewModel;
using Yis.Framework.Core.Extension;

namespace Yis.Erp.Shell.Presentation.ViewModel
{
    public class ShellViewModel : ViewModelBase
    {
        #region Constructors + Destructors

        public ShellViewModel()
            : base()
        {
            //OpenedView.Add(new ViewViewModel {Name = "ttot",Title="ee",View = new TestView()} );
            Bus.Subscribe<ShowView>(OpenView);
        }

        #endregion Constructors + Destructors

        #region Fields

        private ObservableCollection<ViewViewModel> _openedView = new ObservableCollection<ViewViewModel>();
        private ObservableCollection<RibbonTab> _ribbonTabCollection;

        private ViewViewModel _selectedView;

        #endregion Fields

        #region Properties

        public ObservableCollection<ViewViewModel> OpenedView
        {
            get
            {
                return _openedView;
            }
            set
            {
                _openedView = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<RibbonTab> RibbonTabCollection
        {
            get
            {
                if (_ribbonTabCollection == null)
                {
                    _ribbonTabCollection = new ObservableCollection<RibbonTab>();
                    //Locator.ResolveAndCreateAllType<RibbonTab>().ForEach((i) => _ribbonTabCollection.Add(i));
                    foreach (RibbonTab item in Locator.ResolveAndCreateAllType<IRibbonTabExtension>())
                    {
                        _ribbonTabCollection.Add(item);
                    }
                }
                return _ribbonTabCollection;
            }
        }

        public ViewViewModel SelectedView
        {
            set
            {
                if (value != null)
                {
                    //IPlugin selectedPlugin = AllPlugins
                    //    .Where(plugin => plugin.Value.ApplicationCollection
                    //        .Any(application => application.ApplicationName == value.ApplicationName))
                    //    .First().Value;

                    //if (SelectedPlugin != selectedPlugin)
                    //    SelectedPlugin = selectedPlugin;

                    _selectedView = value;
                    RaisePropertyChanged();
                }
            }
            get { return _selectedView; }
        }

        #endregion Properties

        #region Methods

        public void OpenView(ShowView message)
        {
            ViewViewModel newView = null;

            if (message.UniqueInstance)
            {
                if (OpenedView.Any((p) => p.Name == message.Title))
                {
                    SelectedView = OpenedView.First((p) => p.Name == message.Title);
                }
                   
            }

            if (newView.Equals(null))
            {
                newView = new ViewViewModel { Name = message.Title, Title = message.Title, View = message.View };
                OpenedView.Add(newView);
                SelectedView = newView;
            }
        }

        #endregion Methods
    }
}