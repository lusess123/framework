
using System.Collections.Generic;
using System.Xml.Serialization;
namespace Ataw.Framework.Core
{
    public class QueryObj : IReadXmlCallback
    {
        [XmlAttribute]
        public string Logic
        {
            get;
            set;
        }
        [XmlAttribute]
        public string Operator
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }
        public string Value
        {
            get;
            set;
        }
        [XmlIgnore]
        public int ChildrenLength
        {
            get;
            set;
        }
        public virtual List<QueryObj> ObjList
        {
            get;
            set;
        }


        void IReadXmlCallback.OnReadXml()
        {
            //throw new System.NotImplementedException();
            SetObjListLength(this);

        }

        private static void SetObjListLength(QueryObj qobj)
        {
            if (qobj.ObjList != null)
                qobj.ChildrenLength = qobj.ObjList.Count;
            if (qobj.ChildrenLength > 0)
            {
                foreach (QueryObj q in qobj.ObjList)
                {
                    SetObjListLength(q);
                }
            }
        }
    }
}
