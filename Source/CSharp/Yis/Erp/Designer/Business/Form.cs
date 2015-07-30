using System;
using Yis.Erp.Designer.Data.Contract;
using Yis.Framework.Business;
using Yis.Framework.Core.Extension;

namespace Yis.Erp.Designer.Business
{
    /// <summary>
    /// Obhet métier client
    /// </summary>
    public class Form : BusinessObjectBase<Form, Model.Form, IFormProvider, IDesignerDataContext>
    {
        #region Constructors + Destructors

        /// <summary>
        /// Constructeur de l'objet avec un modèle
        /// </summary>
        /// <param name="model"></param>
        public Form(Model.Form model)
            : base(model)
        {
        }

        /// <summary>
        /// Création d'un nouveau client
        /// </summary>
        public Form()
            : base()
        {
            Id = Guid.NewGuid();
        }

        #endregion Constructors + Destructors

        #region Properties

        /// <summary>
        /// Décrit le client
        /// </summary>
        public string Description
        {
            get { return GetProperty(Model.Description); }
            set { SetProperty(v => Model.Description = value, Model.Description, value); }
        }

        /// <summary>
        /// Identifiant technique du <see cref="Client"/>
        /// </summary>
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

        /// <summary>
        /// Identfiant fonctionnel du <see cref="Client"/>
        /// </summary>
        public string Reference
        {
            get { return GetProperty(Model.Reference); }
            set { SetProperty(v => Model.Reference = value, Model.Reference, value); }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Retrouve un client en fonction de l'identifiant technique
        /// </summary>
        /// <param name="id">Identfiant technique du client</param>
        /// <returns><see cref="Client"/></returns>
        public static Form GetById(Guid id)
        {
            Model.Form model = Provider.GetById(id);
            Form item = null;

            if (!model.IsNull())
                item = new Form(model);

            return item;
        }

        #endregion Methods
    }
}