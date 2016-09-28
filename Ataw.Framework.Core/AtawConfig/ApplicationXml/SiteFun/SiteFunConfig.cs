using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Ataw.Framework.Core
{
    public class SiteFunConfig : FileXmlConfigBase, IReadXmlCallback
    {
        public SiteFunConfig()
        {
            Children = new List<FunConfig>();
            fDictList = new Dictionary<string, FunConfig>();
        }

        private Dictionary<string, FunConfig> fDictList;

        [XmlElement("Fun")]
        public List<FunConfig> Children { get; set; }

        public static SiteFunConfig ReadConfig(string binPath)
        {
            string fpath = Path.Combine(binPath, AtawApplicationConfig.REAL_PATH, "Fun.xml");

            return XmlUtil.ReadFromFile<SiteFunConfig>(fpath);
        }

        public FunConfig GetFunByName(string name)
        {
            FunConfig res = null;
            fDictList.TryGetValue(name,out res);
            return res;
        }

        void IReadXmlCallback.OnReadXml()
        {
            if (Children != null && Children.Count > 0)
            {
                AddDict(Children);
            }
        }

        private void AddDict(List<FunConfig> funs)
        {
            foreach (FunConfig f in funs)
            {
                if (!f.Name.IsEmpty())
                {
                    fDictList.Add(f.Name, f);
                }
                if (f.Children != null && f.Children.Count > 0)
                {
                    AddDict(f.Children);
                }
            }
        }
    }
}
