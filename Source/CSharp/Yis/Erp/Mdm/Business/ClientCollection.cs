using System.Collections.Generic;
using Yis.Erp.Mdm.Data.Contract;
using Yis.Framework.Business;

namespace Yis.Erp.Mdm.Business
{
    public class ClientCollection : BusinessObjectCollectionBase<ClientCollection, Client, Model.Client, IClientProvider, IMdmDataContext>
    {
        #region Constructors + Destructors

        public ClientCollection(ICollection<Client> list)
            : base(list)
        {
        }

        public ClientCollection(IEnumerable<Model.Client> list)
            : base(list)
        {
        }

        public ClientCollection()
            : base()
        {
        }

        #endregion Constructors + Destructors
    }
}