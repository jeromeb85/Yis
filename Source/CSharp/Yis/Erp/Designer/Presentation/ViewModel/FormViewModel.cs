using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using Yis.Erp.Designer.Business;
using Yis.Framework.Core.Extension;
using Yis.Framework.Presentation.Commanding;
using Yis.Framework.Presentation.ViewModel;

namespace Yis.Erp.Designer.Presentation.ViewModel
{
    public class FormViewModel : ViewModelBase
    {
        #region Constructors + Destructors

        public FormViewModel(Form model)
        {
            Item = model;
        }

        public FormViewModel()
            : base()
        {
            Item = Form.New();
            AutoValidateProperty = true;
        }

        #endregion Constructors + Destructors

        #region Fields

        private ICommand _valider;

        #endregion Fields

        #region Properties
        
        public string Description
        {
            get { return Item.Description; }
            set { SetProperty<string>(v => Item.Description = value, Item.Description, value); }
        }
        
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

        private Form Item { get; set; }

        #endregion Properties

        #region Methods

        protected override void OnRuleInialize()
        {
            base.OnRuleInialize();

            //Le champ référence ne doit pas être vide
            Validator.AddRule<FormViewModel>((t) => t.Reference, (t) =>
            {
                return  t.Reference.IsNotNullNorEmpty();
            }, "Le champ référence doit être renseigné");
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