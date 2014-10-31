using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Conceptual.Business;
using Yis.Designer.Software.Business;

namespace Yis
{
    public static class InitializeData
    {
        #region Methods

        public static void Run()
        {
            Domain dAccess = CreateDomainAccess();
            dAccess.Save();

            //NameSpace nsRoot = CreateNameSpaceSample();
            //NameSpace nsModel = CreateNameSpaceModel(nsRoot);
        }

        private static Domain CreateDomainAccess()
        {
            Domain Access;
            Concept newConcept;
            Yis.Designer.Conceptual.Business.Attribute attr;

            //Vérifier ou créer Yis
            if (Domain.IsExists("Access"))
                Access = Domain.GetByName("Access");
            else
            {
                Access = Domain.New();
                Access.Name = "Access";
            }

            newConcept = Access.Concept.GetFirstOrAddNew(t => (t.Name == "User"));

            if (newConcept.IsNew)
            {
                newConcept.Name = "User";

                attr = newConcept.Attribute.GetFirstOrAddNew(t => (t.Name == "Login"));
                attr.Name = "Login";
                attr.Type = "String";
            }

            newConcept = Access.Concept.GetFirstOrAddNew(t => (t.Name == "Group"));

            if (newConcept.IsNew)
            {
                newConcept.Name = "Group";

                attr = newConcept.Attribute.GetFirstOrAddNew(t => (t.Name == "Name"));
                attr.Name = "Name";
                attr.Type = "String";
            }



            return Access;
        }

        private static NameSpace CreateNameSpaceModel(NameSpace root)
        {
            NameSpace nsModel;
            Class cUser;
            Property pProp;

            //Vérifier ou créer Designer
            if (root.Sub.Any(t => (t.Name == "Model")))
            {
                nsModel = root.Sub.First(t => (t.Name == "Model"));
            }
            else
            {
                nsModel = root.Sub.AddNew();
                nsModel.Name = "Model";
            }

            if (nsModel.Class.Any(t => (t.Name == "User")))
            {
                cUser = nsModel.Class.First(t => (t.Name == "User"));
            }
            else
            {
                cUser = nsModel.Class.AddNew();
                cUser.Name = "User";
                cUser.Import.Add("Yis.Framework.Model");
                cUser.Import.Add("System");
                cUser.BaseType = "ModelBase";
                cUser.Scope = Yis.Designer.Software.Model.Scope.Public;
            }

            if (cUser.Property.Any(t => (t.Name == "Id")))
            {
                pProp = cUser.Property.First(t => (t.Name == "Id"));
            }
            else
            {
                pProp = cUser.Property.AddNew();
                pProp.Name = "Id";
                pProp.Type = "Guid";
                pProp.Comment = "Identifiant technique d'un utilisateur";

            }

            if (cUser.Property.Any(t => (t.Name == "Login")))
            {
                pProp = cUser.Property.First(t => (t.Name == "Login"));
            }
            else
            {
                pProp = cUser.Property.AddNew();
                pProp.Name = "Login";
                pProp.Type = "string";
                pProp.Comment = "Identifiant de l'utilisateur";

            }

            nsModel.Save();

            return nsModel;
        }

        private static NameSpace CreateNameSpaceSample()
        {
            NameSpace nsRoot;
            NameSpace nsSample;

            //Vérifier ou créer Yis
            if (NameSpace.IsExists("Yis"))
                nsRoot = NameSpace.GetByName("Yis");
            else
            {
                nsRoot = NameSpace.New();
                nsRoot.Name = "Yis";
            }

            //Vérifier ou créer Designer
            if (nsRoot.Sub.Any(t => (t.Name == "Designer")))
            {
                nsSample = nsRoot.Sub.First(t => (t.Name == "Designer"));
            }
            else
            {
                nsSample = nsRoot.Sub.AddNew();
                nsSample.Name = "Designer";
            }

            //Vérifier ou créer Sample
            if (nsSample.Sub.Any(t => (t.Name == "Sample")))
            {
                nsSample = nsSample.Sub.First(t => (t.Name == "Sample"));
            }
            else
            {
                nsSample = nsSample.Sub.AddNew();
                nsSample.Name = "Sample";
            }

            nsRoot.Save();

            return nsSample;
        }

        #endregion Methods
    }
}