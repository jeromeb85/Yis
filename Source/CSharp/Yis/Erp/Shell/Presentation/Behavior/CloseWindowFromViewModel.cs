using System.Windows;
using System.Windows.Interactivity;
using Yis.Framework.Presentation.Event;
using Yis.Framework.Presentation.ViewModel;

namespace Yis.Erp.Shell.Presentation.Behavior
{
    /// <summary>
    /// Behaviour for closing window from viewmodels.
    /// </summary>
    public class CloseWindowFromViewModel : Behavior<Window>
    {
        public IClosableViewModel ViewModel
        {
            get { return (IClosableViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel",
                typeof(IClosableViewModel),
                typeof(CloseWindowFromViewModel));

        protected override void OnAttached()
        {
            var viewModel = ViewModel ?? AssociatedObject.DataContext as IClosableViewModel;

            if (viewModel != null)
            {
                viewModel.RequestClose += OnViewModelRequestClose;
            }
        }

        private void OnViewModelRequestClose(object sender, CloseEventArgs e)
        {
            if (e.DialogResult != null)
            {
                AssociatedObject.DialogResult = e.DialogResult;
            }
            else
            {
                AssociatedObject.Close();
            }
        }
    }

}
