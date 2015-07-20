using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using Yis.Erp.Mdm.Business;
using Yis.Framework.Core.Extension;
using Yis.Framework.Presentation.Commanding;
using Yis.Framework.Presentation.ViewModel;

namespace Yis.Erp.Mdm.Presentation.ViewModel
{
    public class FicheClientCollectionViewModel : ViewModelCollectionBase<FicheClientViewModel>
    {
        public FicheClientCollectionViewModel()
            : base()
        {
        }

        private ICommand _chargerTout;

        public ICommand ChargerTout
        {
            get
            {
                if (_chargerTout.IsNull())
                {
                    _chargerTout = new Command(LoadAll);
                }
                return _chargerTout;
            }
        }

        private void LoadAll()
        {
            ClientCollection list = ClientCollection.GetAll();
            List.Clear();
            foreach (var item in list)
            {
                List.Add(new FicheClientViewModel(item));
            }
        }

    }
}
