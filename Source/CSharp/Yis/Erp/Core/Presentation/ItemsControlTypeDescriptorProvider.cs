using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace Yis.Erp.Core.Presentation
{
    public class ItemsControlTypeDescriptionProvider : TypeDescriptionProvider
    {
        private static readonly TypeDescriptionProvider defaultTypeProvider = TypeDescriptor.GetProvider(typeof(ItemsControl));

        public ItemsControlTypeDescriptionProvider()
            : base(defaultTypeProvider)
        {
        }

        public static void Register()
        {
            TypeDescriptor.AddProvider(new ItemsControlTypeDescriptionProvider(), typeof(ItemsControl));
        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            ICustomTypeDescriptor defaultDescriptor = base.GetTypeDescriptor(objectType, instance);
            return instance == null ? defaultDescriptor : new ItemsControlCustomTypeDescriptor(defaultDescriptor);
        }
    }

    internal class ItemsControlCustomTypeDescriptor : CustomTypeDescriptor
    {
        public ItemsControlCustomTypeDescriptor(ICustomTypeDescriptor parent)
            : base(parent)
        {
        }

        public override PropertyDescriptorCollection GetProperties()
        {
            PropertyDescriptorCollection pdc = new PropertyDescriptorCollection(base.GetProperties().Cast<PropertyDescriptor>().ToArray());
            return ConvertProperties(pdc);
        }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            PropertyDescriptorCollection pdc = new PropertyDescriptorCollection(base.GetProperties(attributes).Cast<PropertyDescriptor>().ToArray());
            return ConvertProperties(pdc);
        }

        private PropertyDescriptorCollection ConvertProperties(PropertyDescriptorCollection pdc)
        {
            PropertyDescriptor pd = pdc.Find("ItemsSource", false);
            if (pd != null)
            {
                PropertyDescriptor pdNew = TypeDescriptor.CreateProperty(typeof(ItemsControl), pd, new Attribute[]
                                                                                                       {
                                                                                                           new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible),
                                                                                                           new DefaultValueAttribute("")
                                                                                                       });
                pdc.Add(pdNew);
                pdc.Remove(pd);
            }
            return pdc;
        }
    }

    public class SelectedValueTypeDescriptionProvider : TypeDescriptionProvider
    {
        private static readonly TypeDescriptionProvider defaultTypeProvider = TypeDescriptor.GetProvider(typeof(ComboBox));

        public SelectedValueTypeDescriptionProvider()
            : base(defaultTypeProvider)
        {
        }

        public static void Register()
        {
            TypeDescriptor.AddProvider(new SelectedValueTypeDescriptionProvider(), typeof(ComboBox));
        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            ICustomTypeDescriptor defaultDescriptor = base.GetTypeDescriptor(objectType, instance);
            return instance == null ? defaultDescriptor : new SelectedValueCustomTypeDescriptor(defaultDescriptor);
        }
    }

    internal class SelectedValueCustomTypeDescriptor : CustomTypeDescriptor
    {
        public SelectedValueCustomTypeDescriptor(ICustomTypeDescriptor parent)
            : base(parent)
        {
        }

        public override PropertyDescriptorCollection GetProperties()
        {
            PropertyDescriptorCollection pdc = new PropertyDescriptorCollection(base.GetProperties().Cast<PropertyDescriptor>().ToArray());
            return ConvertProperties(pdc);
        }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            PropertyDescriptorCollection pdc = new PropertyDescriptorCollection(base.GetProperties(attributes).Cast<PropertyDescriptor>().ToArray());
            return ConvertProperties(pdc);
        }

        private PropertyDescriptorCollection ConvertProperties(PropertyDescriptorCollection pdc)
        {
            PropertyDescriptor pd = pdc.Find("SelectedValue", false);
            if (pd != null)
            {
                PropertyDescriptor pdNew = TypeDescriptor.CreateProperty(typeof(ComboBox), pd, new Attribute[]
                                                                                                       {
                                                                                                           new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible),
                                                                                                           new DefaultValueAttribute("")
                                                                                                       });
                pdc.Add(pdNew);
                pdc.Remove(pd);
            }
            return pdc;
        }
    }

    public class SelectedItemTypeDescriptionProvider : TypeDescriptionProvider
    {
        private static readonly TypeDescriptionProvider defaultTypeProvider = TypeDescriptor.GetProvider(typeof(ListView));

        public SelectedItemTypeDescriptionProvider()
            : base(defaultTypeProvider)
        {
        }

        public static void Register()
        {
            TypeDescriptor.AddProvider(new SelectedItemTypeDescriptionProvider(), typeof(ListView));
        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            ICustomTypeDescriptor defaultDescriptor = base.GetTypeDescriptor(objectType, instance);
            return instance == null ? defaultDescriptor : new SelectedItemCustomTypeDescriptor(defaultDescriptor);
        }
    }

    internal class SelectedItemCustomTypeDescriptor : CustomTypeDescriptor
    {
        public SelectedItemCustomTypeDescriptor(ICustomTypeDescriptor parent)
            : base(parent)
        {
        }

        public override PropertyDescriptorCollection GetProperties()
        {
            PropertyDescriptorCollection pdc = new PropertyDescriptorCollection(base.GetProperties().Cast<PropertyDescriptor>().ToArray());
            return ConvertProperties(pdc);
        }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            PropertyDescriptorCollection pdc = new PropertyDescriptorCollection(base.GetProperties(attributes).Cast<PropertyDescriptor>().ToArray());
            return ConvertProperties(pdc);
        }

        private PropertyDescriptorCollection ConvertProperties(PropertyDescriptorCollection pdc)
        {
            PropertyDescriptor pd = pdc.Find("SelectedItem", false);
            if (pd != null)
            {
                PropertyDescriptor pdNew = TypeDescriptor.CreateProperty(typeof(ListView), pd, new Attribute[]
                                                                                                       {
                                                                                                           new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible),
                                                                                                           new DefaultValueAttribute("")
                                                                                                       });
                pdc.Add(pdNew);
                pdc.Remove(pd);
            }
            return pdc;
        }
    }

    public class ListBoxItemTemplateSelectorTypeDescriptionProvider : TypeDescriptionProvider
    {
        private static readonly TypeDescriptionProvider defaultTypeProvider = TypeDescriptor.GetProvider(typeof(ListBox));

        public ListBoxItemTemplateSelectorTypeDescriptionProvider()
            : base(defaultTypeProvider)
        {
        }

        public static void Register()
        {
            TypeDescriptor.AddProvider(new ListBoxItemTemplateSelectorTypeDescriptionProvider(), typeof(ListBox));
        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            ICustomTypeDescriptor defaultDescriptor = base.GetTypeDescriptor(objectType, instance);
            return instance == null ? defaultDescriptor : new ListBoxItemTemplateSelectorCustomTypeDescriptor(defaultDescriptor);
        }
    }

    internal class ListBoxItemTemplateSelectorCustomTypeDescriptor : CustomTypeDescriptor
    {
        public ListBoxItemTemplateSelectorCustomTypeDescriptor(ICustomTypeDescriptor parent)
            : base(parent)
        {
        }

        public override PropertyDescriptorCollection GetProperties()
        {
            PropertyDescriptorCollection pdc = new PropertyDescriptorCollection(base.GetProperties().Cast<PropertyDescriptor>().ToArray());
            return ConvertProperties(pdc);
        }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            PropertyDescriptorCollection pdc = new PropertyDescriptorCollection(base.GetProperties(attributes).Cast<PropertyDescriptor>().ToArray());
            return ConvertProperties(pdc);
        }

        private PropertyDescriptorCollection ConvertProperties(PropertyDescriptorCollection pdc)
        {
            PropertyDescriptor pd = pdc.Find("ItemTemplateSelector", false);
            if (pd != null)
            {
                PropertyDescriptor pdNew = TypeDescriptor.CreateProperty(typeof(ListBox), pd, new Attribute[]
                                                                                                       {
                                                                                                           new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible),
                                                                                                           new DefaultValueAttribute("")
                                                                                                       });
                pdc.Add(pdNew);
                pdc.Remove(pd);
            }
            return pdc;
        }
    }
    #region Selected Index
    public class SelectedIndexTypeDescriptionProvider : TypeDescriptionProvider
    {
        private static readonly TypeDescriptionProvider defaultTypeProvider = TypeDescriptor.GetProvider(typeof(ComboBox));

        public SelectedIndexTypeDescriptionProvider()
            : base(defaultTypeProvider)
        {
        }

        public static void Register()
        {
            TypeDescriptor.AddProvider(new SelectedIndexTypeDescriptionProvider(), typeof(ComboBox));
        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            ICustomTypeDescriptor defaultDescriptor = base.GetTypeDescriptor(objectType, instance);
            return instance == null ? defaultDescriptor : new SelectedIndexCustomTypeDescriptor(defaultDescriptor);
        }
    }

    internal class SelectedIndexCustomTypeDescriptor : CustomTypeDescriptor
    {
        public SelectedIndexCustomTypeDescriptor(ICustomTypeDescriptor parent)
            : base(parent)
        {
        }

        public override PropertyDescriptorCollection GetProperties()
        {
            PropertyDescriptorCollection pdc = new PropertyDescriptorCollection(base.GetProperties().Cast<PropertyDescriptor>().ToArray());
            return ConvertProperties(pdc);
        }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            PropertyDescriptorCollection pdc = new PropertyDescriptorCollection(base.GetProperties(attributes).Cast<PropertyDescriptor>().ToArray());
            return ConvertProperties(pdc);
        }

        private PropertyDescriptorCollection ConvertProperties(PropertyDescriptorCollection pdc)
        {
            PropertyDescriptor pd = pdc.Find("SelectedIndex", false);
            if (pd != null)
            {
                PropertyDescriptor pdNew = TypeDescriptor.CreateProperty(typeof(ComboBox), pd, new Attribute[]
                                                                                                       {
                                                                                                           new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible),
                                                                                                           new DefaultValueAttribute("")
                                                                                                       });
                pdc.Add(pdNew);
                pdc.Remove(pd);
            }
            return pdc;
        }
    }
    #endregion
}
