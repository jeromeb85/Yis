using System.Windows;
using System.Windows.Controls.Ribbon;
using Yis.Framework.Core.IoC;
using Yis.Framework.Core.Messaging.Contract;

namespace Yis.Erp.Shell.Presentation.Contract
{
    public abstract class RibbonTabBase : RibbonTab, IRibbonTabExtension
    {
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

        #region Methods

        protected void SendMessage(object sender, RoutedEventArgs e)
        {
            /*Messenger.Default.Send(new Message(containingPluginName, (sender as RibbonButton).Tag.ToString()));*/
            /*MessageBox.Show("rr");*/
        }

        #endregion Methods
    }
}