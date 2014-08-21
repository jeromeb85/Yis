using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.Factory;
using Yis.Framework.Core.IoC;

namespace Yis.Framework.Core.Tracer
{
    internal class TraceFactory : IFactory<ITrace>
    {

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <remarks></remarks>
        public TraceFactory()
        {
        }

        public ITrace CreateInstance()
        {
            if (String.IsNullOrEmpty(ConfigurationManager.AppSettings["TraceTypeName"]))
                if (DependencyResolver.Resolve<ITrace>() == null)
                    return new Log4NetTrace();
                else
                    return DependencyResolver.Resolve<ITrace>();
            else
                return (new Factory<ITrace>(ConfigurationManager.AppSettings["TraceTypeName"])).CreateInstance();

            
        }
    }

}
