using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
   public class NodeRequest
    {
       public NodeRequest()
       {
           UserId = AtawAppContext.Current.UserId;
          
       }

       public string UserId { get;set;}
       public string NodeCmd { get; set; }
       public ClientList ClientList { get; set; }
       public string JsCmd { get; set; }
       public RouteDefine RouteData { get; set; }
       public object Data { get; set; }
       public bool IsNotify { get; set; }  
      // public string 
    }


    public class ClientList
    {
        public List<ClientInfo> IdList { get; set; }
        

        public ClientList()
        {
            IdList = new List<ClientInfo>();
        }

        public ClientList(IEnumerable<string> ids):this()
        {
            foreach (string id in ids)
                IdList.Add(new ClientInfo() {  Id = id });
        }
    }

    public class ClientInfo
    {
        public string Id { get; set; }
        public string JsCmd { get; set; }
       // public RouteDefine RouteData { get; set; }
        public object Data { get; set; }
    }
}
