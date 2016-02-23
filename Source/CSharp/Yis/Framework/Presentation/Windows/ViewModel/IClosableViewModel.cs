using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Presentation.Windows.Event;

namespace Yis.Framework.Presentation.Windows.ViewModel
{
    public interface IClosableViewModel
    {
        event EventHandler<CloseEventArgs> RequestClose;

        void Close();
    }
}
