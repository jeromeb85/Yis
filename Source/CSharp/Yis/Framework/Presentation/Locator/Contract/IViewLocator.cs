using System;
using Yis.Framework.Presentation.View;

namespace Yis.Framework.Presentation.Locator.Contract
{
    public interface IViewLocator
    {
        #region Methods

        Type ResolveView(Type viewModelType);

        Type ResolveView<T>() where T : IView;

        #endregion Methods
    }
}