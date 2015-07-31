using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;
using Yis.Framework.Presentation.Behavior.Contract;

namespace Yis.Framework.Presentation.Behavior
{
    public class DragDropBehavior : Behavior<FrameworkElement>
    {

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseMove += new MouseEventHandler(AssociatedObject_OnMouseMove);
            this.AssociatedObject.Drop += new DragEventHandler(AssociatedObject_Drop);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseMove -= new MouseEventHandler(AssociatedObject_OnMouseMove);
            this.AssociatedObject.Drop -= new DragEventHandler(AssociatedObject_Drop);
        }

        #region DragDropHandler DependencyProperty

        /// <summary>
        /// attached property that handles drag and drop
        /// </summary>
        public static readonly DependencyProperty DragDropHandlerProperty =
           DependencyProperty.RegisterAttached("DragDropHandler",
           typeof(IDragDropHandler),
           typeof(DragDropBehavior),
           new PropertyMetadata());


        public IDragDropHandler DragDropHandler
        {
            get { return (IDragDropHandler)GetValue(DragDropHandlerProperty); }
            set { SetValue(DragDropHandlerProperty, value); }
        }

        #endregion


        #region IsDragSource DependencyProperty

        /// <summary>
        /// attached property that defines if the source is a drag source
        /// </summary>
        public static readonly DependencyProperty IsDragSourceProperty =
           DependencyProperty.RegisterAttached("IsDragSource",
           typeof(bool?),
           typeof(DragDropBehavior),
           new PropertyMetadata());



        public bool IsDragSource
        {
            get { return (bool)GetValue(IsDragSourceProperty); }
            set { SetValue(IsDragSourceProperty, value); }
        }


        #endregion


        /// <summary>
        /// eventhandler for the MouseMoveEvent
        /// </summary>
        private void AssociatedObject_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (IsDragSource)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (sender is Selector)
                    {
                        var lst = (Selector)sender;
                        var selectedItem = lst.SelectedItem;
                        DragDrop.DoDragDrop(this.AssociatedObject, selectedItem, DragDropEffects.Copy);
                    }
                    else
                    {
                       // DragDrop.DoDragDrop(this.AssociatedObject, this.AssociatedObject.DataContext, DragDropEffects.Copy);
                    }
                }
            }

        }


        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {

            if (DragDropHandler == null)
                return;

            if (DragDropHandler.CanDrop(e.Data, this.AssociatedObject.DataContext))
                DragDropHandler.OnDrop(e.Data, this.AssociatedObject.DataContext);
            else
                e.Handled = true;
        }
    }
}
