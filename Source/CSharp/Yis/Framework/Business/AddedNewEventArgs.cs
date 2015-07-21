using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Business
{
    /// <summary>
    /// Object containing information about a
    /// newly added object.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the object that was added.
    /// </typeparam>
    public class AddedNewEventArgs<T> : EventArgs
    {
        #region Fields

        private T _newObject;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Creates a new instance of the object.
        /// </summary>
        public AddedNewEventArgs()
        {
        }

        /// <summary>
        /// Creates a new instance of the object.
        /// </summary>
        /// <param name="newObject">
        /// The newly added object.
        /// </param>
        public AddedNewEventArgs(T newObject)
        {
            NewObject = newObject;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a reference to the newly added
        /// object.
        /// </summary>
        public T NewObject
        {
            get { return _newObject; }
            protected set { _newObject = value; }
        }

        #endregion Properties
    }
}