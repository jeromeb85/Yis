using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Conceptual.Business;
using Yis.Designer.Software.Business;
using Yis.Designer.Technic.Contract;
using Yis.Framework.Core.Extension;
using Yis.Framework.Core.Helper;
namespace Yis.Designer.Technic.Internal
{
    public partial class Transformator
    {
        private void TransformBusiness(NameSpace nsDomain, Concept concept)
        {
            /*Transformation Model*/
            NameSpace nsModel = nsDomain.Sub.GetFirstOrAddNew((i) => i.Name == BusinessNameSpace);

            if (nsModel.IsNew)
            {
                nsModel.Name = BusinessNameSpace;
            }

            Class cBusiness = nsModel.Class.GetFirstOrAddNew((i) => i.Name == concept.Name);

            if (cBusiness.IsNew)
            {
                cBusiness.Name = concept.Name;
                cBusiness.Import.Add("System");
                cBusiness.Import.Add("Yis.Framework.Business");
                cBusiness.Import.Add("Yis.Framework.Core.Extension");
                cBusiness.Import.Add(nsDomain.FullName + ".Data.Contract");                
                cBusiness.BaseType = "BusinessObjectBase<" + concept.Name + ", Model." + concept.Name + ", I" + concept.Name + "Provider, I" + nsDomain.Name + "DataContext>";
                cBusiness.Scope = Yis.Designer.Software.Model.Scope.Public;

                Property cModelProp = cBusiness.Property.GetFirstOrAddNew((i) => i.Name == IdNameProperty);

                if (cModelProp.IsNew)
                {
                    cModelProp.Name = IdNameProperty;
                    cModelProp.Type = IdTypeProperty;
                    cModelProp.Comment = "Identifiant technique";
                    cModelProp.SetCode = "ddd";
            //    if (!IsNew)
            //        throw new Exception("Impossible d'affecter un Id si pas isNew");

            //    SetProperty(v => Model.Id = value, Model.Id, value);
            //}";
                    cModelProp.GetCode = "return GetProperty(() => Model.Id);";
                }

                //concept.Attribute.ForEach((i) => TransformModel(cModel, i));
            }

            Class cBusinessCollection = nsModel.Class.GetFirstOrAddNew((i) => i.Name == concept.Name+"Collection");

            if (cBusinessCollection.IsNew)
            {
                cBusinessCollection.Name = concept.Name + "Collection";
                cBusinessCollection.Import.Add("Yis.Framework.Business");
                cBusinessCollection.Import.Add("Yis.Framework.Core.Extension");
                cBusinessCollection.Import.Add("System");
                cBusinessCollection.BaseType = "toto";
                cBusinessCollection.Scope = Yis.Designer.Software.Model.Scope.Public;

                //Property cModelProp = cModel.Property.GetFirstOrAddNew((i) => i.Name == IdNameProperty);

                //if (cModelProp.IsNew)
                //{
                //    cModelProp.Name = IdNameProperty;
                //    cModelProp.Type = IdTypeProperty;
                //    cModelProp.Comment = "Identifiant technique";
                //    cModelProp.IsDenormalized = true;
                //}

                //concept.Attribute.ForEach((i) => TransformModel(cModel, i));
            }
        }
    }
}
