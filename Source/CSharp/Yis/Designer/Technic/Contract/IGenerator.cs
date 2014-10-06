using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Software.Business;

namespace Yis.Designer.Technic.Contract
{
    public interface IGenerator
    {
        #region Methods

        void Generate(NameSpace root, string outputDirectory);

        #endregion Methods
    }
}