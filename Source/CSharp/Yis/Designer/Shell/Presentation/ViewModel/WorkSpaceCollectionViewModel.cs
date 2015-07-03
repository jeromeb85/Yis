using Yis.Framework.Presentation.ViewModel;

namespace Yis.Designer.Presentation.ViewModel
{
    public class WorkSpaceCollectionViewModel : ViewModelBase
    {
        #region Constructors + Destructors

        public WorkSpaceCollectionViewModel()
            : base()
        {
            _testText = "youpi";
        }

        #endregion Constructors + Destructors

        #region Fields

        private string _testText;

        #endregion Fields

        #region Properties

        public string TestText { get { return _testText; } set { SetProperty<string>(ref _testText, value); } }

        #endregion Properties
    }
}