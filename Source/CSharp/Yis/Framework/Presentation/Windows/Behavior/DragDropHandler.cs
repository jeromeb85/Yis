using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Yis.Framework.Presentation.Windows.Behavior.Contract;

namespace Yis.Framework.Presentation.Windows.Behavior
{
    public class DragDropHandler : IDragDropHandler
    {
        readonly Action<object, object> _drop;
        readonly Func<object, object, bool> _canDrop;

        public DragDropHandler(Action<object, object> drop)
            : this(drop, null)
        {
        }

        public DragDropHandler(Action<object, object> drop,
                               Func<object, object, bool> canDrop)
        {
            if (drop == null)
                throw new ArgumentNullException("drop");

            _drop = drop;
            _canDrop = canDrop;
        }

        [DebuggerStepThrough]
        public bool CanDrop(object dropObject, object dropTarget)
        {
            if (_canDrop != null)
                return _canDrop(dropObject, dropTarget);

            return true;
        }

        [DebuggerStepThrough]
        public void OnDrop(object dropObject, object dropTarget)
        {
            _drop(dropObject, dropTarget);
        }
    }

}
