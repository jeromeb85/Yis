using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Yis.Erp.Designer.Presentation.Behaviors
{
    public class ControlHasBindingBehavior
    {
        #region DependencyProperty Collapsed

        /// <summary>
        /// Registers a dependency property as backing store for the HasBinding property
        /// Very important to set default value to 'false'
        /// </summary>
        public static readonly DependencyProperty HasBindingProperty =
            DependencyProperty.RegisterAttached("HasBinding", typeof(bool), typeof(ControlHasBindingBehavior),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets the Collapsed.
        /// </summary>
        /// <value>The Collapsed.</value>
        public static bool GetHasBinding(DependencyObject d)
        {
            return (bool)d.GetValue(HasBindingProperty);
        }

        public static void SetHasBinding(DependencyObject d, bool value)
        {
            d.SetValue(HasBindingProperty, value);
        }

        public static bool HasBinding { get; set; }

        #endregion
    }
}
