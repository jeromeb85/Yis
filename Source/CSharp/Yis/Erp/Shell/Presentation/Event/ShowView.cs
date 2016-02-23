using Yis.Framework.Core.Messaging.Event;
using Yis.Framework.Presentation.Windows.View;

namespace Yis.Erp.Shell.Presentation.Windows.Event
{
    public class ShowView : Message
    {
        #region Constructors + Destructors

        public ShowView(object sender, string title, IView view)
            : base(sender)
        {
            Title = title;
            View = view;
        }

        public ShowView(object sender, string title, IView view, bool uniqueInstance)
            : this(sender, title, view)
        {
            UniqueInstance = uniqueInstance;
        }

        #endregion Constructors + Destructors

        #region Properties

        public string Title { get; set; }

        public IView View { get; set; }

        public bool UniqueInstance { get; set; }

        #endregion Properties
    }
}