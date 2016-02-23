using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Presentation.Windows.Event
{
    public class CloseEventArgs : EventArgs
    {
        public bool? DialogResult { get; set; }
    }
}
