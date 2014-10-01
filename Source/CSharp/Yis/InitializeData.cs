using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Software.Business;

namespace Yis
{
    public static class InitializeData
    {
        #region Methods

        public static void Run()
        {
            NameSpace nsRoot;
            NameSpace nsSample;

            //Vérifier ou créer Yis
            if (NameSpace.IsExists("Yis"))
                nsRoot = NameSpace.GetByName("Yis");
            else
            {
                nsRoot = NameSpace.New();
                nsRoot.Id = Guid.NewGuid();
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
                nsSample.Id = Guid.NewGuid();
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
                nsSample.Id = Guid.NewGuid();
                nsSample.Name = "Sample";
            }

            nsRoot.Save();
        }

        #endregion Methods
    }
}