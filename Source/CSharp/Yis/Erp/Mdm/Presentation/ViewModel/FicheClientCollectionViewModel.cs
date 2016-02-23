using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using Yis.Erp.Mdm.Business;
using Yis.Framework.Core.Extension;
using Yis.Framework.Presentation.Windows.Commanding;
using Yis.Framework.Presentation.Windows.ViewModel;

namespace Yis.Erp.Mdm.Presentation.Windows.ViewModel
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
