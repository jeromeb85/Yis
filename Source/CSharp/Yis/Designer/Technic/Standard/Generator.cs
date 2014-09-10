using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Technic.Contract;
using Yis.Framework.Core.Extension;

namespace Yis.Designer.Technic.Standard
{
    public class Generator : IGenerator
    {
        #region Fields

        private CodeCompileUnit _compileUnit;

        #endregion Fields

        #region Properties

        public CodeCompileUnit CompileUnit
        {
            get
            {
                if (_compileUnit.IsNull())
                {
                    _compileUnit = new CodeCompileUnit();
                }
                return _compileUnit;
            }
        }

        #endregion Properties

        #region Methods

        public void Generate(Software.Model.NameSpace root, string outputDirectory)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}