using System;
using Yis.Framework.Presentation.View;

namespace Yis.Framework.Presentation.Locator.Contract
{
    public interface IViewLocator
    {
        Type ResolveView(Type viewModelType);

        Type ResolveView<T>() where T : IView;
    }
}