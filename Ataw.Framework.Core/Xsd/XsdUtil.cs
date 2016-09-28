using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Ataw.Framework.Core
{
   public class XsdUtil
    {
       public static List<XsdItem> CreateItems(XmlSchemaObjectCollection objs) {
           List<XsdItem> list = new List<XsdItem>();
           foreach (XmlSchemaObject a in objs) {
              list.Add(   new XsdItem(a));

           }
           return list;
       }

       public static XmlSchema CreateSchema(string fileName)
       {


           string xsd = fileName;

           FileStream fs = new FileStream(xsd, FileMode.Open);
           using (fs)
           {
               XmlSchema schema = XmlSchema.Read(fs, null);
               return schema;
           }
           
       
       }

       public static void  EditDoc(string rootFileName,string node)
       {
           XmlSchemaSet schemaSet = new XmlSchemaSet();
           //schemaSet.ValidationEventHandler += new ValidationEventHandler(ShowCompileError);

           XmlSchema xml =   CreateSchema(rootFileName);
           schemaSet.Add(xml);
           schemaSet.Compile();



       }

       

       public static string[] GetXsdPath()
       {
           string fpath = Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH,
              "XmlSchema");
           string[] res = Directory.GetFiles(fpath, "*.xsd", SearchOption.AllDirectories);
           return res;
       }

    }

  
}
