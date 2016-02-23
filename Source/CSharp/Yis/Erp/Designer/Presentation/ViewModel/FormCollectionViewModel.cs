using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using Yis.Erp.Designer.Business;
using Yis.Framework.Core.Extension;
using Yis.Framework.Presentation.Windows.Commanding;
using Yis.Framework.Presentation.Windows.ViewModel;

namespace Yis.Erp.Designer.Presentation.Windows.ViewModel
{
    public class FormCollectionViewModel : ViewModelCollectionBase<FormViewModel>
    {
        public FormCollectionViewModel()
            : base()
        {
            LoadAll();
        }

        private ICommand _actualiser;

        public ICommand Actualiser
        {
            get
            {
                if (_actualiser.IsNull())
                {
                    _actualiser = new Command(LoadAll);
                }
                return _actualiser;
            }
        }

        private ICommand _ajouter;

        public ICommand Ajouter
        {
            get
            {
                if (_ajouter.IsNull())
                {
                    _ajouter = new Command(Add);
                }
                return _ajouter;
            }
        }

        private void LoadAll()
        {
            FormCollection list = FormCollection.GetAll();
            List.Clear();
            foreach (var item in list)
            {
                List.Add(new FormViewModel(item));
            }
        }

        private void Add()
        {
            FormViewModel item = new FormViewModel();
            List.Add(item);
            Selected = item;
        }

    }
}
