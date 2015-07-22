using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using Yis.Erp.Mdm.Business;
using Yis.Framework.Core.Extension;
using Yis.Framework.Presentation.Commanding;
using Yis.Framework.Presentation.ViewModel;

namespace Yis.Erp.Mdm.Presentation.ViewModel
{
    public class FicheClientViewModel : ViewModelBase
    {
        #region Constructors + Destructors

        public FicheClientViewModel(Client model)
        {
            client = model;
        }

        public FicheClientViewModel()
            : base()
        {
            client = new Client();
            AutoValidateProperty = false;
        }

        #endregion Constructors + Destructors

        #region Fields

        private ICommand _valider;

        #endregion Fields

        #region Properties

        [StringLength(50, MinimumLength = 5, ErrorMessage = "La longueur doit être comprise entre 5 et 50")]
        public string Description
        {
            get { return client.Description; }
            set { SetProperty<string>(v => client.Description = value, client.Description, value); }
        }

        [Required]
        public string Reference
        {
            get { return client.Reference; }
            set { SetProperty<string>(v => client.Reference = value, client.Reference, value); }
        }

        public ICommand Valider
        {
            get
            {
                if (_valider.IsNull())
                {
                    _valider = new Command(Sauvegarder);
                }
                return _valider;
            }
        }

        //public string Reference { get { return client.Reference; } set { SetProperty<string>(v => client.Reference = value, client.Reference, value); } }
        private Client client { get; set; }

        #endregion Properties

        #region Methods

        protected override void OnRuleInialize()
        {
            base.OnRuleInialize();

             Validator.AddRule<FicheClientViewModel>((t) => t.Description, (t) => { return
             t.Description == "tototo"; }, "Tu dois metre tototo");
        }

        private void Sauvegarder()
        {
            client.Save
        }

        #endregion Methods
    }
}