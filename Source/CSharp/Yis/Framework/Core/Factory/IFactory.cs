using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Core.Factory
{
    public interface IFactory
    {

    }

    public interface IFactory<T> where T : class
    {

        T CreateInstance();
    }

}
