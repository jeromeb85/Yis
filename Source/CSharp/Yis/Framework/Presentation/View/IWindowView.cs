namespace Yis.Framework.Presentation.View
{
    public interface IWindowView : IView
    {
        void Show(object context = null);

        bool? ShowModal(object context = null);
    }
}