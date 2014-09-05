using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Presentation.ViewModel
{
    public abstract class ViewModelCollectionBase<TViewModel>
    {
        public ObservableCollection<TViewModel> List { get; set; }
    }
}