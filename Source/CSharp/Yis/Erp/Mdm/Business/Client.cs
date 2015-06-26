using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Erp.Mdm.Data.Contract;
using Yis.Framework.Core.Extension;
using Yis.Framework.Business;

namespace Yis.Erp.Mdm.Business
{
    public class Client : BusinessObjectBase<Client, Model.Client, IClientProvider, IMdmDataContext>
    {
                #region Constructors

        public Client(Model.Client model)
            : base(model)
        {
        }

        public Client()
            : base()
        {
            Id = Guid.NewGuid();
        }

        #endregion Constructors

        public Guid Id
        {
            get { return GetProperty(Model.Id); }
            set
            {
                if (!IsNew)
                    throw new Exception("Impossible d'affecter un Id si pas isNew");

                SetProperty(v => Model.Id = value, Model.Id, value);
            }
        }

        public string Reference
        {
            get { return GetProperty(Model.Reference); }
            set { SetProperty(v => Model.Reference = value, Model.Reference, value); }
        }

        public string Description
        {
            get { return GetProperty(Model.Description); }
            set { SetProperty(v => Model.Description = value, Model.Description, value); }
        }

        public static Client GetById(Guid id)
        {
            Model.Client model = Provider.GetById(id);
            Client item = null;

            if (!model.IsNull())
                item = new Client(model);

            return item;
        }

    }
}
