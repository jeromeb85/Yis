using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Input;
using Yis.Erp.Designer.Business;
using Yis.Framework.Core.Extension;
using Yis.Framework.Presentation.Behavior;
using Yis.Framework.Presentation.Behavior.Contract;
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

        IDragDropHandler _dragDropAction;
        public IDragDropHandler DragDropAction
        {
            get
            {
                if (_dragDropAction == null)
                    _dragDropAction = new DragDropHandler(OnDrop, CanDrop);
                return _dragDropAction;
            }
        }

        public bool CanDrop(object dropObject, object dropTarget)
        {
            //if (!(dropTarget is IList))
            //    return false;
            //return !(dropTarget as IList).Contains(dropObject.GetData(typeof(BaseGroup)));
            return true;
        }

        public void OnDrop(object dropObject, object dropTarget)
        {

            MessageBox.Show(dropObject.GetType().ToString());
            //if (dropTarget is IList)
            //{
            //    if (dropObject.GetDataPresent(typeof(BaseGroup)))
            //    {
            //        (dropTarget as IList).Add(dropObject.GetData(typeof(BaseGroup)));
            //    }
            //}
        }

    }
}