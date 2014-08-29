using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Presentation.View
{
    public interface IWindowView : IView
    {
        void Show(object context = null);
        bool? ShowModal(object context = null);
    }
}
