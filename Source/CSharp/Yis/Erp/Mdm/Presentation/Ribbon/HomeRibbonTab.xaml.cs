using System.Windows;
using Yis.Erp.Mdm.Presentation.Windows.View;
using Yis.Erp.Shell.Presentation.Windows.Contract;
using Yis.Erp.Shell.Presentation.Windows.Event;

namespace Yis.Erp.Mdm.Presentation.Windows.Ribbon
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

        private void NewFicheClient_Click(object sender, RoutedEventArgs e)
        {
            Bus.Publish<ShowView>(new ShowView(this, "Nouveau Client", new FicheClientView()));
        }

        #endregion Methods

        private void ListFicheClient_Click(object sender, RoutedEventArgs e)
        {
            Bus.Publish<ShowView>(new ShowView(this, "Fiche client", new FicheClientCollectionView(),true));
        }
    }
}