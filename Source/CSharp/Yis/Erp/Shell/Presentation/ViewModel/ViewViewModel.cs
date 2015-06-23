using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Presentation.View;
using Yis.Framework.Presentation.ViewModel;

namespace Yis.Erp.Shell.Presentation.ViewModel
{
    public class ViewViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public string Title { get; set; }

        public IView View { get; set; }
    }
}
