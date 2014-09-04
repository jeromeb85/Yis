using Yis.Framework.Presentation.ViewModel;

namespace Yis.Designer.Presentation.ViewModel
{
    public class WorkSpaceViewModel : ViewModelBase
    {
        private string _name;

        public string Name { get { return _name; } set { SetValue<string>(ref _name, value); } }
    }
}