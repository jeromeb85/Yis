using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Ribbon;
using Yis.Erp.Mdm.Presentation.Ribbon;
using Yis.Erp.Shell.Presentation.Contract;
using Yis.Framework.Core.Extension;
using Yis.Framework.Core.IoC;
using Yis.Framework.Core.Locator.Contract;
using Yis.Framework.Presentation.ViewModel;

namespace Yis.Erp.Shell.Presentation.ViewModel
{
    public class ShellViewModel : ViewModelBase
    {
        private ObservableCollection<RibbonTab> _ribbonTabCollection;

        public ObservableCollection<RibbonTab> RibbonTabCollection
        {
            get {
                if (_ribbonTabCollection == null)
                {

                    _ribbonTabCollection = new ObservableCollection<RibbonTab>();  
                    //Locator.ResolveAndCreateAllType<RibbonTab>().ForEach((i) => _ribbonTabCollection.Add(i));
                    foreach (RibbonTab item in Locator.ResolveAndCreateAllType<IRibbonTabExtension>())
                    {
                        _ribbonTabCollection.Add(item);
                    }
                                        
                }
                return _ribbonTabCollection;
            }
        }
    }
}
