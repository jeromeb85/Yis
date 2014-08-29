using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Presentation.ViewModel;

namespace Yis.Designer.Presentation.ViewModel
{
    public class WorkSpaceCollectionViewModel : ViewModelBase
    {
        private string _testText;
        public string TestText { get { return _testText; } set { SetValue<string>(ref _testText, value); } }

        public WorkSpaceCollectionViewModel() : base()
        {
            _testText = "youpi";
        }
    }
}
