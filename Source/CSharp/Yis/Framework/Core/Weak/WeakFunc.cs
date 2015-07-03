using System;
using System.Reflection;

namespace Yis.Framework.Core.Weak
{
    internal class WeakFunc<TResult>
    {
        #region Constructors + Destructors

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="item">délégué pour lequel la référence est créé</param>
        public WeakFunc(Func<TResult> item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _targetRef = new WeakReference(item.Target);
            _method = item.Method;
            _delegateType = item.GetType();
            _func = item;
        }

        #endregion Constructors + Destructors

        #region Fields

        private readonly Type _delegateType;
        private readonly Func<TResult> _func;
        private readonly MethodInfo _method;
        private readonly WeakReference _targetRef;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Obtient l'instance de la classe sur lequel le délégué est invoqué
        /// </summary>
        /// <remarks>
        /// peut être null, alors soit la référence n'existe plus soit il s'agissait d'une méthode statique
        /// </remarks>
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
        public TResult Execute()
        {
            // Si la référence existe encore, on crée un délégué lui étant associé
            if ((Target != null) && (_func != null))
                return _func();

            return default(TResult);
        }

        #endregion Methods
    }
}