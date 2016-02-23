using Yis.Framework.Presentation.Windows.View;
using Yis.Framework.Presentation.Windows.ViewModel;

namespace Yis.Erp.Shell.Presentation.Windows.ViewModel
{
    public class ViewViewModel : ViewModelBase
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public IView View { get; set; }
    }
}