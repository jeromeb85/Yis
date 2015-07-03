using Yis.Framework.Presentation.ViewModel;

namespace Yis.Designer.Presentation.ViewModel
{
    public class WorkSpaceViewModel : ViewModelBase
    {
        #region Fields

        private string _name;

        #endregion Fields

        #region Properties

        public string Name { get { return _name; } set { SetProperty<string>(ref _name, value); } }

        #endregion Properties
    }
}