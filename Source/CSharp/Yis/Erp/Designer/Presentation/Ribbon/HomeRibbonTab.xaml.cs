using System.Windows;
using Yis.Erp.Shell.Presentation.Windows.Contract;
using Yis.Erp.Shell.Presentation.Windows.Event;
using Yis.Erp.Designer.Presentation.Windows.View;

namespace Yis.Erp.Designer.Presentation.Windows.Ribbon
{
    /// <summary>
    /// Logique d'interaction pour HomeRibbonTab.xaml
    /// </summary>
    public partial class HomeRibbonTab : RibbonTabBase
    {
        #region Constructors + Destructors

        public HomeRibbonTab()
        {
            InitializeComponent();
        }

        #endregion Constructors + Destructors

        #region Methods

        private void FormDesigner_Click(object sender, RoutedEventArgs e)
        {
            Bus.Publish<ShowView>(new ShowView(this, "Form", new FormCollectionView()));
        }

        #endregion Methods


    }
}