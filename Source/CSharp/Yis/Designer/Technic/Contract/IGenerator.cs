using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Software.Model;

namespace Yis.Designer.Technic.Contract
{
    public interface IGenerator
    {
        void Generate(NameSpace root, string outputDirectory);
    }
}