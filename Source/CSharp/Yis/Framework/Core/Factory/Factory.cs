using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Core.Factory
{
    public class Factory<T> : IFactory<T> where T : class
    {


        private string _typeName;

        /// <summary>
        /// Initialise une nouvelle instance de  classe.
        /// </summary>
        public Factory(string typeName)
        {
            _typeName = typeName;
        }




        /// <summary>
        /// Création d'une instance de résolution de dépendance.
        /// </summary>
        /// <returns>
        /// Instance de la résolution de dépendance.
        /// </returns>
        public T CreateInstance()
        {
            T _newInstance = null;
            Type _typeType = null;
            Assembly a = null;


            //Recherche selon un chemin

            if (_newInstance == null)
            {
                a = Assembly.GetAssembly(typeof(T));
                _typeType = a.GetType(_typeName, false, true);

                if (_typeType == null)
                {
                    a = Assembly.GetExecutingAssembly();
                    _typeType = a.GetType(_typeName, false, true);
                }

                if (_typeType == null)
                {
                    _typeType = Type.GetType(_typeName, false, true);
                }

                _newInstance = Activator.CreateInstance(_typeType) as T;
            }

            //Traitement d'exception
            if (_newInstance == null)
            {
                throw new Exception("Instance introuvable");
            }

            return _newInstance;
        }
    }

}
