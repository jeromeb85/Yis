using System;
using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Yis.Framework.Core.Extension;

namespace Yis.Erp.Shell.Presentation.Windows.Behavior
{
    public class CloseTabBehavior : Behavior<Button>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Click += OnClick;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= OnClick;

        }

        private static void OnClick(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = (sender as Button).GetParent<TabItem>();

            object selectedItem = tabItem.DataContext;

            TabControl tabControl = (sender as Button).GetParent<TabControl>();

            IList list = (IList) tabControl.ItemsSource;

            int index = list.IndexOf(selectedItem);

            if (list.Count == 1)
                tabControl.SelectedItem = null;
            else if (index < list.Count - 1 )
                tabControl.SelectedItem = list[++index];
            else if (index == list.Count - 1)
                tabControl.SelectedItem = list[--index];

            try
            {
                list.Remove(selectedItem);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
