namespace Yis.Framework.Presentation.View
{
    public interface IWindowView : IView
    {
        #region Methods

        void Show(object context = null);

        bool? ShowModal(object context = null);

        #endregion Methods
    }
}