using System.Data;
using System.Text;
using System.Xml.Linq;
using Ataw.Framework.Core;
using System.Xml.Serialization;

namespace Ataw.Workflow.Core
{
    public class OperationConfig : XmlConfigBase, IRegName
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string DisplayName { get; set; }

        [XmlAttribute]
        public string ButtonCaption { get; set; }

        [XmlAttribute]
        public string PlugIn { get; set; }

        public virtual OperationType OperationType
        {
            get
            {
                return OperationType.NonUI;
            }
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(DisplayName) && !string.IsNullOrEmpty(Name))
            {
                string name = string.IsNullOrEmpty(DisplayName) ? Name : DisplayName;
                return string.Format(ObjectUtil.SysCulture, "操作{{{0}}}", name);
            }
            else
                return "无设置";
        }

        [XmlIgnore]
        public string RegName
        {
            get { return Name; }
        }
    }
}
