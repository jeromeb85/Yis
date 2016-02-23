namespace Yis.Framework.Presentation.Windows.View
{
    public interface IWindowView : IView
    {
        void Show(object context = null);

        bool? ShowModal(object context = null);
    }
}