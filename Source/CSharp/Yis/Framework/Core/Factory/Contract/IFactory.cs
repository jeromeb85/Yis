using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Core.Factory.Contract
{
    public interface IFactory
    {
        object CreateInstance();
    }

    public interface IFactory<T> where T : class
    {

        T CreateInstance();
    }

}
