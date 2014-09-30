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

            if (NameSpace.IsExsist("Yis"))
                nsRoot = NameSpace.GetByName("Yis");
            else
            {
                nsRoot = NameSpace.New();
                nsRoot.Id = Guid.NewGuid();
                nsRoot.Name = "Yis";
            }
        }

        #endregion Methods
    }
}