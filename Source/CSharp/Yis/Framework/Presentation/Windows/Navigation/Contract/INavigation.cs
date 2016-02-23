using Yis.Framework.Presentation.Windows.View;

namespace Yis.Framework.Presentation.Windows.Navigation.Contract
{
    public interface INavigation
    {
        void Show<T>(object context = null) where T : IView;

        bool? ShowModal<T>(object context = null) where T : IView;
    }
}