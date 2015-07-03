using Yis.Framework.Presentation.View;
using Yis.Framework.Presentation.ViewModel;

namespace Yis.Erp.Shell.Presentation.ViewModel
{
    public class ViewViewModel : ViewModelBase
    {
        #region Properties

        public string Name { get; set; }

        public string Title { get; set; }

        public IView View { get; set; }

        #endregion Properties
    }
}