using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yis.Framework.Presentation.Windows.Locator.Contract;
using Yis.Framework.Presentation.Windows.ViewModel;

namespace Yis.Framework.Presentation.Windows.Locator
{
    internal class ViewModelLocator : IViewModelLocator
    {
        private List<Type> _cache;

        public List<Type> Cache
        {
            get
            {
                if (_cache == null)
                {
                    _cache = new List<Type>();
                    InitializeCache();
                }

                return _cache;
            }
        }

        private void InitializeCache()
        {
            Assembly[] assemblies = new Assembly[] { Assembly.GetExecutingAssembly() };
            //Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly currentAssembly in assemblies)
            {
                foreach (Type currentType in currentAssembly.GetTypes())
                {
                    if (!currentType.IsAbstract)
                    {
                        if (currentType.GetInterfaces().Any((t) => { return t == typeof(IViewModel); }))
                        {
                            Cache.Add(currentType);
                        }
                    }
                }
            }
        }

        public Type ResolveViewModel(Type viewType)
        {
            string nameView = viewType.Name.Replace("Window", "").Replace("View", "");

            return Cache.First((t) => { return t.Name == (nameView + "ViewModel"); });
        }
    }
}