using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Presentation.View;

namespace Yis.Framework.Presentation.Locator.Contract
{
    public interface IViewLocator
    {
        Type ResolveView(Type viewModelType);
        Type ResolveView<T>() where T : IView; 
    }
}
