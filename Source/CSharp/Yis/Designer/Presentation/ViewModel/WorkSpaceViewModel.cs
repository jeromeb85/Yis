using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Presentation.ViewModel;

namespace Yis.Designer.Presentation.ViewModel
{
    public class WorkSpaceViewModel : ViewModelBase
    {
        private string _name;
        public string Name { get { return _name; } set { SetValue<string>(ref _name, value); } }
    }
}
