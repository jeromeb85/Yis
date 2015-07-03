using System;

namespace Yis.Framework.Presentation.Locator.Contract
{
    public interface IViewModelLocator
    {
        #region Methods

        Type ResolveViewModel(Type viewType);

        #endregion Methods
    }
}