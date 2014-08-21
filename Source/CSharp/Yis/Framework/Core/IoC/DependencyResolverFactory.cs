using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.Factory;

namespace Yis.Framework.Core.IoC
{
    internal class DependencyResolverFactory : IFactory<IDependencyResolver>
    {
        public DependencyResolverFactory()            
        {          
        }

        public IDependencyResolver CreateInstance()
        {
            if (String.IsNullOrEmpty(ConfigurationManager.AppSettings["IoCTypeName"]))
                return new UnityDependencyResolver();
            else
                return (new Factory<IDependencyResolver>(ConfigurationManager.AppSettings["IoCTypeName"])).CreateInstance();

            
        }
    }

}
