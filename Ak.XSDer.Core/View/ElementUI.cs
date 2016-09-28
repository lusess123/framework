using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ak.XSDer.Core.View
{

    public interface IRootElement
    {
         string GroupName { get; set; }
    }

    public class BaseElement
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public string DisplayName { get; set; }
    }

    public class BaseElementUI:BaseElement
    {
        public string ControlType { get; set; }
    }

    public class SimpleElementUI : BaseElementUI, IRootElement
    {
        public List<AttributeElementUI> AttributeElementUIList { get; set; }
    }

    public class AttributeElementUI : BaseElementUI
    {
       
    }

   

    public class ElementGroupInfo
    {
        public string Name { get; set; }
        public string ElementType { get; set; }

        public List<string> List { get; set; }
    }
   
    public class CompositeElementUI : BaseElement, IRootElement
    {
        public List<AttributeElementUI> AttributeElementUIList { get; set; }
        public List<CompositeElementUI> CompositeElementUIList { get; set; }
        public List<SimpleElementUI> SimpleElementUIList { get; set; }
        public Dictionary<string, ElementGroupInfo> ElementGroupInfoDict { get; set; }
       // public Dictionary<string, ElementInfo> ElementInfoDict { get; set; }
        public string GroupName { get; set; }
    }

    public enum ControlType
    {
        None = 0,
        String = 1,
        Date = 2,
        Combo = 3

    }

    public class Page
    {
        public IRootElement RootElement { get; set; }
    }



}
