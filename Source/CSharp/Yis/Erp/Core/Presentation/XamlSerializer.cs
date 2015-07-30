using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Xml;
using System.IO;

namespace Yis.Erp.Core.Presentation
{
    public class XamlSerializer
    {
        public XamlSerializer()
        {

        }

        public string SerializeControlToXaml(FrameworkElement control)
        {
            StringBuilder outstr = new StringBuilder();

            //this code need for right XML fomating 
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            XamlDesignerSerializationManager dsm =
                new XamlDesignerSerializationManager(XmlWriter.Create(outstr, settings));
            //this string need for turning on expression saving mode 
            dsm.XamlWriterMode = XamlWriterMode.Expression;
            System.Windows.Markup.XamlWriter.Save(control, dsm);

            string xaml = outstr.ToString();
            return xaml;
        }

        public object DeserializeXaml(string xaml)
        {
            StringReader stringReader = new StringReader(xaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            return System.Windows.Markup.XamlReader.Load(xmlReader);
        }
    }
}
