using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Presentation.View;

namespace Yis.Framework.Presentation.Navigation
{
    public interface INavigation
    {
        void Show<T>(object context = null) where T : IView;
        bool? ShowModal<T>(object context = null) where T : IView;

    }
}
