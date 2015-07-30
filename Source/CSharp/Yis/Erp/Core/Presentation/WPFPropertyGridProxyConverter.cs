using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace Yis.Erp.Core.Presentation
{
    public class WPFPropertyGridProxyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (value is TextBox)
            //    return new TextBoxProxy(value as TextBox);

            //if (value is Label)
            //    return new LabelProxy(value as Label);

            //if (value is CheckBox)
            //    return new CheckBoxProxy(value as CheckBox);

            //if (value is DatePicker)
            //    return new DatePickerProxy(value as DatePicker);

            //if (value is ComboBox)
            //    return new ComboBoxProxy(value as ComboBox);

            //if (value is Expander)
            //    return new ExpanderProxy(value as Expander);

            //if (value is RadioButton)
            //    return new RadioButtonProxy(value as RadioButton);

            //if (value is ListView)
            //    return new ListViewProxy(value as ListView);

            //if (value is GroupBox)
            //    return new GroupBoxProxy(value as GroupBox);

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}