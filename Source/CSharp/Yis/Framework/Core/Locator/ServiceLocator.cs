using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yis.Framework.Core.Locator.Contract;

namespace Yis.Framework.Core.Locator
{
    internal class ServiceLocator : IServiceLocator
    {
        private List<Assembly> _catalogAssembly;

        protected List<Assembly> CatalogAssembly
        {
            get
            {
                if (_catalogAssembly == null)
                {
                    _catalogAssembly = new List<Assembly>();
                }
                return _catalogAssembly;
            }
        }

        private Dictionary<Type, List<Type>> _catalog;

        protected Dictionary<Type, List<Type>> Catalog
        {
            get
            {
                if (_catalog == null)
                {
                    _catalog = new Dictionary<Type, List<Type>>();
                }
                return _catalog;
            }
        }

        public ServiceLocator()
        {
            Build();
        }

        public IEnumerable<Type> ResolveType(Type typeInterface)
        {
            List<Type> typeClasse = new List<Type>();

            if (Catalog.ContainsKey(typeInterface))
            {
                typeClasse.AddRange(Catalog[typeInterface]);
            }

            return typeClasse;
        }

        public IEnumerable<Type> ResolveType<TInterface>()
        {
            return ResolveType(typeof(TInterface));
        }

        public object ResolveAndCreateType(Type typeInterface, object[] param = null)
        {
            if (ResolveType(typeInterface).Count() == 0)
            {
                throw new Exception("Aucune classe trouvée pour l'interface : " + typeInterface.Name);
            }

            if (ResolveType(typeInterface).Count() > 1)
            {
                throw new Exception("Plusieurs classes trouvées pour l'interface : " + typeInterface.Name);
            }

            return Activator.CreateInstance(ResolveType(typeInterface).First(), param);
        }

        public TInterface ResolveAndCreateType<TInterface>(object[] param = null)
        {
            return (TInterface)ResolveAndCreateType(typeof(TInterface));
        }

        public IEnumerable<TInterface> ResolveAndCreateAllType<TInterface>(object[] param = null)
        {
            List<TInterface> list = new List<TInterface>();

            foreach (Type type in ResolveType<TInterface>())
            {
                list.Add((TInterface)Activator.CreateInstance(type, param));
            }

            return list;
        }

        public void Build()
        {
            DiscoverAssemblies();
            BuildCatalog();
        }

        private void DiscoverAssemblies()
        {
            //Définir les règles de détection et chargement des assemblies
            CatalogAssembly.Add(Assembly.GetExecutingAssembly());
        }

        private void BuildCatalog()
        {
            foreach (Assembly currentAssembly in CatalogAssembly)
            {
                foreach (Type currentType in currentAssembly.GetTypes())
                {
                    if ((!currentType.IsAbstract) && (!currentType.IsInterface))
                    {
                        foreach (Type typeInterface in currentType.GetInterfaces())
                        {
                            AddCatalog(typeInterface, currentType);
                        }
                    }
                }
            }
        }

        private void AddCatalog(Type typeInterface, Type typeClasse)
        {
            if (!Catalog.ContainsKey(typeInterface))
            {
                Catalog.Add(typeInterface, new List<Type>());
            }

            Catalog[typeInterface].Add(typeClasse);
        }
    }
}