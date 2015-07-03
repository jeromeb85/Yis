﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yis.Framework.Presentation.Locator.Contract;
using Yis.Framework.Presentation.View;

namespace Yis.Framework.Presentation.Locator
{
    internal class ViewLocator : IViewLocator
    {
        #region Fields

        private List<Type> _cache;

        #endregion Fields

        #region Properties

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

        #endregion Properties

        #region Methods

        public Type ResolveView(Type viewModelType)
        {
            string nameView = viewModelType.Name.Replace("ViewModel", "");

            return Cache.First((t) => { return (t.Name == (nameView + "View")) || (t.Name == (nameView + "Window")); });
        }

        public Type ResolveView<T>() where T : IView
        {
            return Cache.First((t) => { return (t.GetInterfaces().Any((u) => { return u.Equals(typeof(T)); })); });
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
                        if (currentType.GetInterfaces().Any((t) => { return t == typeof(IView); }))
                        {
                            Cache.Add(currentType);
                        }
                    }
                }
            }
        }

        #endregion Methods
    }
}