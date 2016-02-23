using System;

namespace Yis.Framework.Presentation.Windows.Locator.Contract
{
    public interface IViewModelLocator
    {
        Type ResolveViewModel(Type viewType);
    }
}