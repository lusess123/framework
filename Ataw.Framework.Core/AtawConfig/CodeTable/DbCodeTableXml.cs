using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ataw.Framework.Core
{
   public class DbCodeTableXml: FileXmlConfigBase, IReadXmlCallback
    {
       public DbCodeTableXml()
       {
           DbCodeTables = new RegNameList<DbCodeTable>();
       }
       public RegNameList<DbCodeTable> DbCodeTables { get; set; }

       public void OnReadXml()
       {
          // throw new NotImplementedException();
           foreach (var item in DbCodeTables)
           {
               item.OnReadXml();
           }
       }

       public static DbCodeTableXml ReadConfig(string fileName)
       {
           string fpath = Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH,
               "CodeTable", fileName);

           return XmlUtil.ReadFromFile<DbCodeTableXml>(fpath);
       }
       public static IList<DbCodeTableXml> ReadConfigFromDir()
       {
           string fpath = Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH,
               "CodeTable");
           string[] codeTableXmls= Directory.GetFiles(fpath, "*.xml", SearchOption.AllDirectories);
           IList<DbCodeTableXml> dbCodeTableXml = new List<DbCodeTableXml>();
           foreach (var codeTableXml in codeTableXmls)
           {
               dbCodeTableXml.Add(XmlUtil.ReadFromFile<DbCodeTableXml>(codeTableXml));
           }
           return dbCodeTableXml;
       }
    }
}
