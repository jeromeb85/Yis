using System;

namespace Yis.Framework.Business
{
    /// <summary>
    /// Objet événelebt contenant toutes les informations concernant l'ajout d'un nouvel objet.
    /// </summary>
    /// <typeparam name="T">Type d'objet ajouté</typeparam>
    public class AddedNewEventArgs<T> : EventArgs
    {
        #region Constructors + Destructors

        /// <summary>
        /// Création d'une nouvelle instance
        /// </summary>
        public AddedNewEventArgs()
        {
        }

        /// <summary>
        /// Création d'une nouvelle instance de l'objet
        /// </summary>
        /// <param name="newObject">The newly added object.</param>
        public AddedNewEventArgs(T newObject)
        {
            NewObject = newObject;
        }

        #endregion Constructors + Destructors

        #region Fields

        private T _newObject;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Retourne la référence vers le nouvel objet
        /// </summary>
        public T NewObject
        {
            get { return _newObject; }
            protected set { _newObject = value; }
        }

        #endregion Properties
    }
}