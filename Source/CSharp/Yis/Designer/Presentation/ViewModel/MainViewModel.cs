using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yis.Designer.Presentation.Command;
using Yis.Framework.Presentation.ViewModel;

namespace Yis.Designer.Presentation.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        public MainViewModel()
            : base()
        {
            _testText = "ttttt";
        }

        public string Title { get { return "toto"; } }


        private string _testText;
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "La longueur doit être comprise entre 5 et 50")]
        public string TestText { get { return _testText; } set { SetValue<string>(ref _testText, value); } }

        private RelayCommand _commandTest;
        public ICommand CommandTest
        {
            get
            {
                if (_commandTest == null)
                {
                    _commandTest = new RelayCommand(ProcedureTest);
                }
                return _commandTest;
            }
        }

        private void ProcedureTest()
        {
            //TestText = "dd";
            Validate();
        }
    }
}
