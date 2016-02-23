using System;
using Yis.Framework.Presentation.Windows.View;

namespace Yis.Framework.Presentation.Windows.Locator.Contract
{
    public interface IViewLocator
    {
        Type ResolveView(Type viewModelType);

        Type ResolveView<T>() where T : IView;
    }
}