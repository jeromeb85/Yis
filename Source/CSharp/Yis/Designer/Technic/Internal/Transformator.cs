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
    public class Transformator : ITransformator
    {
        #region Fields

        private const string IdNameProperty = "Id";
        private const string IdTypeProperty = "Guid";
        private const string ModelNameSpace = "Model";
        private const string RootNameSpace = "Yis.Designer.Sample";

        #endregion Fields

        #region Methods

        public void Transform(Domain domain)
        {
            NameSpace nsRoot = CreateRootNameSpace(RootNameSpace);

            NameSpace nsDomain = nsRoot.Sub.GetFirstOrAddNew((i) => i.Name == domain.Name);

            if (nsDomain.IsNew)
            {
                nsDomain.Name = domain.Name;
            }

            domain.Concept.ForEach((i) => Transform(nsDomain, i));

            nsRoot.Save();
        }

        private NameSpace CreateRootNameSpace(string nameSpace)
        {
            ArgumentHelper.IsNotNullOrEmpty("nameSpace", nameSpace);

            NameSpace ns = null;
            NameSpace nsRoot = null;

            foreach (var item in nameSpace.Split('.'))
            {
                if (ns.IsNull())
                {
                    if (NameSpace.IsExists(item))
                    {
                        ns = NameSpace.GetByName(item);
                    }
                    else
                    {
                        ns = NameSpace.New();
                        ns.Name = item;
                    }

                    nsRoot = ns;
                }
                else
                {
                    if (ns.Sub.Any((i) => i.Name == item))
                    {
                        ns = ns.Sub.First((i) => i.Name == item);
                    }
                    else
                    {
                        ns = ns.Sub.AddNew();
                        ns.Name = item;
                    }
                }
            }

            nsRoot.Save();

            return ns;
        }

        private void Transform(NameSpace nsDomain, Concept concept)
        {
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
                    cModelProp.IsDenormalized = true;
                }

                concept.Attribute.ForEach((i) => Transform(cModel, i));
            }
        }

        private void Transform(Class cModel, Yis.Designer.Conceptual.Business.Attribute attribute)
        {
            Property cModelProp = cModel.Property.GetFirstOrAddNew((i) => i.Name == attribute.Name);
            cModelProp.IsDenormalized = true;
            cModelProp.Name = attribute.Name;
            cModelProp.Type = attribute.Type;
        }

        #endregion Methods
    }
}