using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Yis.Framework.Core.Extension
{
    public static class DependencyObjectExt
    {
        #region Methods

        public static IEnumerable<T> GetChildren<T>(this DependencyObject parent) where T : DependencyObject
        {
            return parent.GetChildren<T>(t => true);
        }

        public static IEnumerable<T> GetChildren<T>(this DependencyObject parent, Func<T, bool> customFilter) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                T typedChild = child as T;
                if (typedChild != null)
                {
                    if (customFilter.Invoke(typedChild))
                    {
                        yield return typedChild;
                    }
                }
                foreach (var innerChild in child.GetChildren<T>(customFilter))
                {
                    yield return innerChild;
                }
            }
        }

        public static T GetParent<T>(this DependencyObject child) where T : DependencyObject
        {
            return child.GetParent<T>(t => true);
        }

        public static T GetParent<T>(this DependencyObject child, Func<T, bool> customFilter) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            if (parent != null)
            {
                T typedParent = parent as T;
                if (typedParent != null && customFilter.Invoke(typedParent))
                {
                    return typedParent;
                }
                else
                {
                    return parent.GetParent<T>();
                }
            }
            return null;
        }

        #endregion Methods
    }
}