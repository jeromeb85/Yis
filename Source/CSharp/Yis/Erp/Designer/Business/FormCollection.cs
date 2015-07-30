using System.Collections.Generic;
using Yis.Erp.Designer.Data.Contract;
using Yis.Framework.Business;

namespace Yis.Erp.Designer.Business
{
    public class FormCollection : BusinessObjectCollectionBase<FormCollection, Form, Model.Form, IFormProvider, IDesignerDataContext>
    {
        #region Constructors

        public FormCollection(ICollection<Form> list)
            : base(list)
        {
        }

        public FormCollection(IEnumerable<Model.Form> list)
            : base(list)
        {
        }

        public FormCollection()
            : base()
        {
        }

        #endregion Constructors
    }
}