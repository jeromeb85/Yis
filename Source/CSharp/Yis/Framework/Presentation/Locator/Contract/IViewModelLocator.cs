using System;

namespace Yis.Framework.Presentation.Locator.Contract
{
    public interface IViewModelLocator
    {
        Type ResolveViewModel(Type viewType);
    }
}