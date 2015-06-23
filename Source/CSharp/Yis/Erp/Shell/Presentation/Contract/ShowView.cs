using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.Messaging.Event;
using Yis.Framework.Presentation.View;


namespace Yis.Erp.Shell.Presentation.Contract
{
    public class ShowView : Message
    {
        public string Title { get; set; }
        public IView View { get; set; }

        public ShowView(object sender, string title, IView view)
            : base(sender)
        {
            Title = title;
            View = view;
        }
    }
}
