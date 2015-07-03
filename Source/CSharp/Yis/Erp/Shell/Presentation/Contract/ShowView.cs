using Yis.Framework.Core.Messaging.Event;
using Yis.Framework.Presentation.View;

namespace Yis.Erp.Shell.Presentation.Contract
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

        #endregion Constructors + Destructors

        #region Properties

        public string Title { get; set; }

        public IView View { get; set; }

        #endregion Properties
    }
}