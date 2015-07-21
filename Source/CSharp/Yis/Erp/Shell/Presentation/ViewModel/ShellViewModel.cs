using System.Collections.ObjectModel;
using System.Windows.Controls.Ribbon;
using Yis.Erp.Shell.Presentation.Contract;
using Yis.Erp.Shell.Presentation.View;
using Yis.Framework.Presentation.ViewModel;

namespace Yis.Erp.Shell.Presentation.ViewModel
{
    public class ShellViewModel : ViewModelBase
    {
        public ShellViewModel() : base()
        {
            //OpenedView.Add(new ViewViewModel {Name = "ttot",Title="ee",View = new TestView()} );
            Bus.Subscribe<ShowView>(OpenView);
        }



        private ObservableCollection<RibbonTab> _ribbonTabCollection;

        public ObservableCollection<RibbonTab> RibbonTabCollection
        {
            get {
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

        private ObservableCollection<ViewViewModel> _openedView = new ObservableCollection<ViewViewModel>();
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

        private ViewViewModel _selectedView;
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

        public void OpenView(ShowView message)
        {
            ViewViewModel newView = new ViewViewModel { Name = message.Title, Title = message.Title, View = message.View };
             OpenedView.Add(newView);
                 SelectedView = newView;
        }
    }
}
