﻿using System;
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
            NameSpace nsRoot = CreateNameSpaceSample();
            NameSpace nsModel = CreateNameSpaceModel(nsRoot);
        }

        private static NameSpace CreateNameSpaceModel(NameSpace root)
        {
            NameSpace nsModel;
            Class cUser;

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