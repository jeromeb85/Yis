using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Ribbon;

namespace Yis.Erp.Shell.Presentation.Contract
{
    public abstract class RibbonTabBase : RibbonTab, IRibbonTabExtension
    {
        protected void SendMessage(object sender, RoutedEventArgs e)
        {
            /*Messenger.Default.Send(new Message(containingPluginName, (sender as RibbonButton).Tag.ToString()));*/
            MessageBox.Show("rr");
        }
    }
}
