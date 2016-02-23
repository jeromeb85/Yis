using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Ribbon;
using Yis.Framework.Core.IoC;
using Yis.Framework.Core.Messaging.Contract;

namespace Yis.Erp.Shell.Presentation.Windows.Contract
{
    public abstract class RibbonTabBase : RibbonTab, IRibbonTabExtension
    {

        private IBus _bus;

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


        protected void SendMessage(object sender, RoutedEventArgs e)
        {
            /*Messenger.Default.Send(new Message(containingPluginName, (sender as RibbonButton).Tag.ToString()));*/
            /*MessageBox.Show("rr");*/
        }
    }
}
