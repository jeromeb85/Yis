using System.Windows;
using Yis.Erp.Shell.Presentation.Contract;
using Yis.Erp.Shell.Presentation.Event;
using Yis.Erp.Designer.Presentation.View;

namespace Yis.Erp.Designer.Presentation.Ribbon
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