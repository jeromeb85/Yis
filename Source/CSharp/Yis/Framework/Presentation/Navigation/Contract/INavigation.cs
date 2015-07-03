using Yis.Framework.Presentation.View;

namespace Yis.Framework.Presentation.Navigation.Contract
{
    public interface INavigation
    {
        #region Methods

        void Show<T>(object context = null) where T : IView;

        bool? ShowModal<T>(object context = null) where T : IView;

        #endregion Methods
    }
}