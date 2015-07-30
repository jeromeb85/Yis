using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Yis.Erp.Core.Presentation
{
    /// <summary>
    /// http://www.codeproject.com/Articles/27158/XamlWriter-and-Bindings-Serialization
    /// http://social.msdn.microsoft.com/Forums/en-US/wpf/thread/98637d07-f0c6-42cc-8cfe-b9c713914d3c
    /// </summary>
    internal class BindingConvertor : ExpressionConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(MarkupExtension))
                return true;
            else return false;
        }

        public override object ConvertTo(ITypeDescriptorContext context,
                                         System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(MarkupExtension))
            {
                BindingExpression bindingExpression = value as BindingExpression;
                if (bindingExpression == null)
                {
                    throw new Exception();
                }
                return bindingExpression.ParentBinding;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    internal class MultiBindingConvertor : ExpressionConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(MarkupExtension))
                return true;
            else return false;
        }

        public override object ConvertTo(ITypeDescriptorContext context,
                                         System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(MarkupExtension))
            {
                MultiBindingExpression bindingExpression = value as MultiBindingExpression;
                if (bindingExpression == null)
                {
                    throw new Exception();
                }
                return bindingExpression.ParentMultiBinding;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    internal static class EditorHelper
    {
        public static void Register<T, TC>()
        {
            Attribute[] attr = new Attribute[1];
            TypeConverterAttribute vConv = new TypeConverterAttribute(typeof(TC));
            attr[0] = vConv;
            TypeDescriptor.AddAttributes(typeof(T), attr);
            //Register ItemsControl provider to serialize ComboBox ItemsSource binding!!!
            ItemsControlTypeDescriptionProvider.Register();
            SelectedValueTypeDescriptionProvider.Register();
            SelectedItemTypeDescriptionProvider.Register();
            SelectedIndexTypeDescriptionProvider.Register();
            //Item Template Selector for ListBox Relationships Participants collection
            //Templates
            ListBoxItemTemplateSelectorTypeDescriptionProvider.Register();
        }
    }
}