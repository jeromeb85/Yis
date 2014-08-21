using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.Factory;

namespace Yis.Framework.Core.IoC
{
    public sealed class DependencyResolver
    {

        private static IDependencyResolver _resolver;
        private DependencyResolver()
        {
            Initialize();
        }

        /// <summary>
        /// Initialiser IOC avec
        /// </summary>
        /// <param name="factory">Factory</param>
        public static void InitializeWith(IFactory<IDependencyResolver> factory)
        {
            _resolver = factory.CreateInstance();
        }

        /// <summary>
        /// Initialiser
        /// </summary>
        public static void Initialize()
        {
            InitializeWith(new DependencyResolverFactory());
        }

        static DependencyResolver()
        {
            Initialize();
        }

        public static void Reset()
        {
            if (_resolver != null)
            {
                _resolver.Dispose();
            }
        }

        public static void Register<T>(T instance)
        {
            _resolver.Register(instance);
        }

        public static void Inject<T>(T existing)
        {
            _resolver.Inject(existing);
        }

        public static T Resolve<T>(Type type)
        {
            return _resolver.Resolve<T>(type);
        }

        public static T Resolve<T>(Type type, string name)
        {
            return _resolver.Resolve<T>(type, name);
        }

        public static T Resolve<T>()
        {
            return _resolver.Resolve<T>();
        }

        public static T Resolve<T>(string name)
        {
            return _resolver.Resolve<T>(name);
        }

        public static IEnumerable<T> ResolveAll<T>()
        {
            return _resolver.ResolveAll<T>();
        }
    }

}
