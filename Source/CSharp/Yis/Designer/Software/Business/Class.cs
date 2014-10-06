using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Software.Data.Contract;
using Yis.Framework.Business;

namespace Yis.Designer.Software.Business
{
    public class Class : BusinessObjectBase<Class, Model.Class, IClassProvider, ISoftwareDataContext>
    {
        #region Constructors

        public Class(Model.Class model)
            : base(model)
        {
        }

        public Class()
            : base()
        {
            Id = Guid.NewGuid();
        }

        #endregion Constructors

        #region Properties

        public Guid Id
        {
            get { return GetProperty(() => Model.Id); }
            set
            {
                if (!IsNew)
                    throw new Exception("Impossible d'affecter un Id si pas isNew");

                SetProperty(v => Model.Id = value, Model.Id, value);
            }
        }

        public string Name
        {
            get { return GetProperty(() => Model.Name); }
            set { SetProperty(v => Model.Name = value, Model.Name, value); }
        }

        public NameSpace Parent
        {
            get { return GetProperty<NameSpace>(() => NameSpace.GetById(Model.NameSpaceId)); }
            set { SetProperty(v => Model.NameSpaceId = value.Id, Model.NameSpaceId, value.Id); }
        }

        #endregion Properties
    }
}