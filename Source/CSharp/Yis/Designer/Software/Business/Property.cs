using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Software.Data.Contract;
using Yis.Framework.Business;

namespace Yis.Designer.Software.Business
{
    public class Property : BusinessObjectBase<Property, Model.Property, IPropertyProvider, ISoftwareDataContext>
    {
        #region Constructors

        public Property(Model.Property model)
            : base(model)
        {
        }

        public Property()
            : base()
        {
            Id = Guid.NewGuid();
        }

        #endregion Constructors

        #region Properties

        public string Comment
        {
            get { return GetProperty(() => Model.Comment); }
            set { SetProperty(v => Model.Comment = value, Model.Comment, value); }
        }

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

        /// <summary>
        /// Signifie que si le langugage cible permet la dénormalisation ça le sera.
        /// </summary>
        public bool IsDenormalized
        {
            get { return GetProperty(() => Model.IsDenormalized); }
            set { SetProperty(v => Model.IsDenormalized = value, Model.IsDenormalized, value); }
        }

        public string Name
        {
            get { return GetProperty(() => Model.Name); }
            set { SetProperty(v => Model.Name = value, Model.Name, value); }
        }

        public Class Parent
        {
            get { return GetProperty<Class>(() => Class.GetById(Model.ClassId)); }
            set { SetProperty(v => Model.ClassId = value.Id, Model.ClassId, value.Id); }
        }

        public Model.Scope Scope
        {
            get { return GetProperty(() => Model.Scope); }
            set { SetProperty(v => Model.Scope = value, Model.Scope, value); }
        }

        public string Type
        {
            get { return GetProperty(() => Model.Type); }
            set { SetProperty(v => Model.Type = value, Model.Type, value); }
        }

        #endregion Properties
    }
}