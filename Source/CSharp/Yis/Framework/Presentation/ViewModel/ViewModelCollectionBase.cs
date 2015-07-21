using System.Collections.ObjectModel;

namespace Yis.Framework.Presentation.ViewModel
{
    public abstract class ViewModelCollectionBase<TViewModel>
    {
        #region Properties

        public ObservableCollection<TViewModel> List { get; set; }

        #endregion Properties
    }
}