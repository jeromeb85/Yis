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
            Client = model;
        }

        public FicheClientViewModel()
            : base()
        {
            Client = Client.New();
            AutoValidateProperty = true;
        }

        #endregion Constructors + Destructors

        #region Fields

        private ICommand _valider;

        #endregion Fields

        #region Properties

        [StringLength(50, MinimumLength = 5, ErrorMessage = "La longueur doit être comprise entre 5 et 50")]
        public string Description
        {
            get { return Client.Description; }
            set { SetProperty<string>(v => Client.Description = value, Client.Description, value); }
        }

        [Required]
        public string Reference
        {
            get { return Client.Reference; }
            set { SetProperty<string>(v => Client.Reference = value, Client.Reference, value); }
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

        private Client Client { get; set; }

        #endregion Properties

        #region Methods

        protected override void OnRuleInialize()
        {
            base.OnRuleInialize();

            Validator.AddRule<FicheClientViewModel>((t) => t.Description, (t) =>
            {
                return
                    t.Description == "tototo";
            }, "Tu dois metre tototo");
        }

        private void Sauvegarder()
        {
            if (Validate())
            {
                Client.Save();
            }
        }

        #endregion Methods
    }
}