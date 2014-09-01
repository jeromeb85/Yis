using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.Factory.Contract;

namespace Yis.Framework.Core.Factory
{

    public class Factory : IFactory
    {
        private string _typeName;
        private Type _type;


        protected string TypeName
        {
            get
            {
                if (String.IsNullOrEmpty(_typeName))
                {
                    _typeName = _type.Name;
                }

                return _typeName;
            }
        }

        protected Type Type
        {
            get 
            {
                if (_type == null)
                {
                    _type = GetTypeByName(_typeName);
                }

                return _type;
            }
        }

        public Factory(string typeName)
        {
            _typeName = typeName;            
        }

        public Factory(Type type)
        {
            _type = type;
        }


        private Type GetTypeByName(string name)
        {

            Type result = null;
            Assembly a = null;

            //a = Assembly.GetAssembly(typeof(T));
            //result = a.GetType(_typeName, false, true);

            if (result == null)
            {
                a = Assembly.GetExecutingAssembly();
                result = a.GetType(_typeName, false, true);
            }

            if (result == null)
            {
                result = Type.GetType(_typeName, false, true);
            }


            return result;

        }

        public object CreateInstance()
        {
            object _newInstance = null;

            if (_newInstance == null)
            {

                _newInstance = Activator.CreateInstance(Type);
            }

            //Traitement d'exception
            if (_newInstance == null)
            {
                throw new Exception("Instance introuvable");
            }

            return _newInstance;

        }

    }

    public class Factory<T> : Factory, IFactory<T> where T : class
    {
        /// <summary>
        /// Initialise une nouvelle instance de  classe.
        /// </summary>
        /// 
        public Factory(string name) : base(name)
        {

        }

        public Factory() : base(typeof(T))
        {

        }

        // <summary>
        /// Création d'une instance de résolution de dépendance.
        /// </summary>
        /// <returns>
        /// Instance de la résolution de dépendance.
        /// </returns>
        public T CreateInstance()
        {
            T _newInstance = null;
            _newInstance = CreateInstance() as T;
            return _newInstance;
        }
    }

}
