using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ataw.Framework.Core
{
      [Serializable]
    public class DocViewerConfig : FileXmlConfigBase
    {
          public string DocViewerPath { get; set; }
          public string ExtractTextCmd { get; set; }
          public string Pdf2SwfCmd { get; set; }

          public static DocViewerConfig ReadConfig(string binPath)
          {
              string fpath = Path.Combine(binPath, AtawApplicationConfig.REAL_PATH, "DocViwer.xml");

              return XmlUtil.ReadFromFile<DocViewerConfig>(fpath);
          }
    }
}
