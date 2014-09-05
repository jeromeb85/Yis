using System;
using System.Reflection;

namespace Yis.Framework.Core.Weak
{
    /// <summary>
    /// Permet de stocker un délégué associé à une référence faible, donc collectable par le GC
    /// </summary>
    internal class WeakDelegate
    {
        #region Fields

        private readonly Type _delegateType;
        private readonly MethodInfo _method;
        private readonly WeakReference _targetRef;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="item">délégué pour lequel la référence est créé</param>
        public WeakDelegate(Delegate item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _targetRef = new WeakReference(item.Target);
            _method = item.Method;
            _delegateType = item.GetType();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Obtient l'instance de la classe sur lequel le délégué est invoqué
        /// </summary>
        /// <remarks>peut être null, alors soit la référence n'existe plus soit il s'agissait d'une méthode statique</remarks>
        public object Target
        {
            get
            {
                if (_targetRef == null || !_targetRef.IsAlive)
                    return null;

                return _targetRef.Target;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Retourne le délégué à invoquer
        /// </summary>
        /// <returns>le délégué à invoquer ou null si la référence faible n'a plus lieue d'être</returns>
        public Delegate GetDelegate()
        {
            // Si la référence existe encore, on crée un délégué lui étant associé
            if (Target != null)
                return Delegate.CreateDelegate(_delegateType, _targetRef.Target, _method);
            // S'il s'agit d'une méthode statique, on crée le délégué sans référence particulière
            else if (_method.IsStatic)
                return Delegate.CreateDelegate(_delegateType, null, _method);

            // En cas de référence null, on ne renvoie rien
            return null;
        }

        #endregion Methods
    }
}