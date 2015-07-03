using Yis.Designer.Conceptual.Business;
using Yis.Designer.Software.Business;

namespace Yis.Designer.Technic.Internal
{
    public partial class Transformator
    {
        #region Methods

        private void CreateDataContext(NameSpace nsData, NameSpace nsDomain)
        {
            NameSpace ns = nsData.Sub.GetFirstOrAddNew((i) => i.Name == "Contract");

            if (ns.IsNew)
            {
                ns.Name = "Contract";
            }

            Class cDataContext = ns.Class.GetFirstOrAddNew((i) => i.Name == "I" + nsDomain.Name + "DataContext");

            if (cDataContext.IsNew)
            {
                cDataContext.Name = "I" + nsDomain.Name + "DataContext";
                cDataContext.Import.Add("Yis.Framework.Data.Contract");
                cDataContext.Implement.Add("IDataContext");
                cDataContext.IsInterface = true;
                cDataContext.Scope = Yis.Designer.Software.Model.Scope.Public;
            }
        }

        private void TransformContract(NameSpace nsData, Concept concept)
        {
            NameSpace ns = nsData.Sub.GetFirstOrAddNew((i) => i.Name == "Contract");

            if (ns.IsNew)
            {
                ns.Name = "Contract";
            }

            Class cDataContract = ns.Class.GetFirstOrAddNew((i) => i.Name == "I" + concept.Name + "Provider");

            if (cDataContract.IsNew)
            {
                cDataContract.Name = "I" + concept.Name + "Provider";
                cDataContract.Import.Add("Yis.Framework.Data.Contract");
                cDataContract.Import.Add("System");
                cDataContract.IsInterface = true;
                cDataContract.Scope = Yis.Designer.Software.Model.Scope.Public;

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

        private void TransformData(NameSpace nsDomain, Concept concept)
        {
            NameSpace ns = nsDomain.Sub.GetFirstOrAddNew((i) => i.Name == DataNameSpace);

            if (ns.IsNew)
            {
                ns.Name = DataNameSpace;
            }

            CreateDataContext(ns, nsDomain);

            TransformContract(ns, concept);
            TransformMemory(ns, concept);
        }

        private void TransformMemory(NameSpace nsData, Concept concept)
        {
            NameSpace ns = nsData.Sub.GetFirstOrAddNew((i) => i.Name == "Memory");

            if (ns.IsNew)
            {
                ns.Name = "Memory";
            }

            Class cDataContract = ns.Class.GetFirstOrAddNew((i) => i.Name == concept.Name + "Provider");

            if (cDataContract.IsNew)
            {
                cDataContract.Name = concept.Name + "Provider";
                cDataContract.Import.Add("Yis.Framework.Data.Contract");
                cDataContract.Import.Add("System");
                cDataContract.Scope = Yis.Designer.Software.Model.Scope.Public;

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

        #endregion Methods
    }
}