using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Yis.Framework.Presentation.Windows.Behavior.Contract
{
    public interface IDragDropHandler
    {
        bool CanDrop(object dropObject, object dropTarget);

        void OnDrop(object dropObject, object dropTarget);

    }
}
