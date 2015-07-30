using System.Collections.ObjectModel;
using Yis.Framework.Core.IoC;
using Yis.Framework.Core.Messaging.Contract;

namespace Yis.Framework.Presentation.ViewModel
{
    public abstract class ViewModelCollectionBase : IViewModel
    {
        #region Constructors + Destructors

        public ViewModelCollectionBase()
            : base()
        {
        }

        #endregion Constructors + Destructors

        #region Fields

        private IBus _bus;

        #endregion Fields

        #region Properties

        protected IBus Bus
        {
            get
            {
                if (_bus == null)
                {
                    _bus = DependencyResolverManager.Default.Resolve<IBus>();
                }

                return _bus;
            }
        }

        #endregion Properties
    }

    public abstract class ViewModelCollectionBase<TViewModel> : ViewModelCollectionBase
    {
        #region Constructors + Destructors

        public ViewModelCollectionBase()
            : base()
        {
            List = new ObservableCollection<TViewModel>();
        }

        #endregion Constructors + Destructors

        #region Properties

        public ObservableCollection<TViewModel> List { get; set; }

        public TViewModel Selected { get; set; }

        #endregion Properties
    }
}