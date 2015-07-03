using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using Yis.Designer.Presentation.View;
using Yis.Framework.Presentation.Commanding;
using Yis.Framework.Presentation.ViewModel;

namespace Yis.Designer.Presentation.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Constructors + Destructors

        public MainViewModel()
            : base()
        {
            _testText = "ttttt";
        }

        #endregion Constructors + Destructors

        #region Fields

        private ICommand _commandTest;

        private string _testText;

        #endregion Fields

        #region Properties

        public ICommand CommandTest
        {
            get
            {
                if (_commandTest == null)
                {
                    _commandTest = new Command(ProcedureTest);
                }
                return _commandTest;
            }
        }

        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "La longueur doit être comprise entre 5 et 50")]
        public string TestText { get { return _testText; } set { SetProperty<string>(ref _testText, value); } }

        public string Title { get { return "toto"; } }

        #endregion Properties

        #region Methods

        protected override void OnRuleInialize()
        {
            base.OnRuleInialize();

            Validator.AddRule<MainViewModel>((t) => t.TestText, (t) => { return t.TestText == "tototo"; }, "Tu dois metre tototo");
        }

        private void ProcedureTest()
        {
            //TestText = "dd";
            //Validate();
            WorkSpaceCollectionViewModel ws = new WorkSpaceCollectionViewModel();
            ws.TestText = "tutu";

            Navigation.Show<IWorkSpaceCollectionView>(ws);
        }

        #endregion Methods
    }
}