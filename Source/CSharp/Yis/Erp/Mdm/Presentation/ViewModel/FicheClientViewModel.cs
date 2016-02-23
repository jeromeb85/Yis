using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using Yis.Erp.Mdm.Business;
using Yis.Framework.Core.Extension;
using Yis.Framework.Presentation.Windows.Commanding;
using Yis.Framework.Presentation.Windows.ViewModel;

namespace Yis.Erp.Mdm.Presentation.Windows.ViewModel
{
    public class FicheClientViewModel : ViewModelBase
    {
        #region Constructors + Destructors

        public FicheClientViewModel(Client model)
        {
            Item = model;
        }

        public FicheClientViewModel()
            : base()
        {
            Item = Client.New();
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
            get { return Item.Description; }
            set { SetProperty<string>(v => Item.Description = value, Item.Description, value); }
        }

        [Required]
        public string Reference
        {
            get { return Item.Reference; }
            set { SetProperty<string>(v => Item.Reference = value, Item.Reference, value); }
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

        private Client Item { get; set; }

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
                Item.Save();
            }
        }

        #endregion Methods
    }
}