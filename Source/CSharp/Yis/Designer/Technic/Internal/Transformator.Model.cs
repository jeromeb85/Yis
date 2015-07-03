using Yis.Designer.Conceptual.Business;
using Yis.Designer.Software.Business;
using Yis.Framework.Core.Extension;

namespace Yis.Designer.Technic.Internal
{
    public partial class Transformator
    {
        #region Methods

        private void TransformModel(NameSpace nsDomain, Concept concept)
        {
            /*Transformation Model*/
            NameSpace nsModel = nsDomain.Sub.GetFirstOrAddNew((i) => i.Name == ModelNameSpace);

            if (nsModel.IsNew)
            {
                nsModel.Name = ModelNameSpace;
            }

            Class cModel = nsModel.Class.GetFirstOrAddNew((i) => i.Name == concept.Name);

            if (cModel.IsNew)
            {
                cModel.Name = concept.Name;
                cModel.Import.Add("Yis.Framework.Model");
                cModel.Import.Add("System");
                cModel.BaseType = "ModelBase";
                cModel.Scope = Yis.Designer.Software.Model.Scope.Public;

                Property cModelProp = cModel.Property.GetFirstOrAddNew((i) => i.Name == IdNameProperty);

                if (cModelProp.IsNew)
                {
                    cModelProp.Name = IdNameProperty;
                    cModelProp.Type = IdTypeProperty;
                    cModelProp.Comment = "Identifiant technique";
                }

                concept.Attribute.ForEach((i) => TransformModel(cModel, i));
            }
        }

        private void TransformModel(Class cModel, Yis.Designer.Conceptual.Business.Attribute attribute)
        {
            Property cModelProp = cModel.Property.GetFirstOrAddNew((i) => i.Name == attribute.Name);
            cModelProp.Name = attribute.Name;
            cModelProp.Type = attribute.Type;
        }

        #endregion Methods
    }
}