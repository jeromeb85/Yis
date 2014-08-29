using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Presentation.Locator
{
    public interface IViewModelLocator
    {
        Type ResolveViewModel(Type viewType);
    }
}
