using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketIOClient;
using System.Threading.Tasks;

namespace Ataw.Framework.Core
{
   public class NodeServerPusher
    {
       public NodeRequest request { get; set; }

       public string Url { get; set; }

       private void InternalSend()
       {
           Client socket = new Client(Url);
           if (request.NodeCmd.IsEmpty())
           {
               request.NodeCmd = "server";
           }
           request.IsNotify = true;
           using (socket)
           {
               bool isConnect = false;
               socket.Error += (a, b) =>
               {
                   isConnect = true;
               };

               socket.Message += (a, b) =>
               {
                   //isConnect = true;
                 var bmm =   b.Message.MessageText;

               };
               socket.SocketConnectionClosed += (a, b) =>
               {
                   isConnect = true;
               };
               socket.HeartBeatTimerEvent += (a, b) =>
               {
                   isConnect = true;
               };
               socket.Opened += (a, b) =>
               {
                   socket.Emit("client_open", request);

                  // socket.
                  // isConnect = true;
               };
               //---------------------------

               socket.On("server_endnode", (data) =>
               {

                   isConnect = true;
               });

               socket.Connect();
               while (!isConnect)
               {

               }
               socket.Close();
           }
       }

       public void Send() {
           var _node = AtawAppContext.Current.ApplicationXml.AppSettings["NodeJsUrl"];
           if (_node != null)
           {
               if (!_node.Value.IsEmpty())
               {
                   Url = _node.Value;
                   AtawDebug.AssertNotNullOrEmpty(Url, " NodeJsUrl 节点配置不能为空 ", this);
                   Task task = new Task(InternalSend);
                   task.Start();
                   //task.Wait();
               }
           }
           //task.Wait();

       }

       public static void Route(IEnumerable<string> idList, string controlName, string actionName, NodeRequest req = null)
       {
           var puser = new NodeServerPusher();
           if (req != null)
               puser.request = req;
           else
               puser.request = new NodeRequest()
               {
                   ClientList = new ClientList()
               };
           if (idList != null)
               puser.request.ClientList = new ClientList(idList);
           puser.request.JsCmd = "route";
           puser.request.RouteData = new RouteDefine() { 
                ControllerName = controlName ,
                 ActionName = actionName
           };
           puser.Send();
       }


       public static void Notify(IEnumerable<string> idList,  NodeRequest req = null)
       {
          // if (request == null)
          // NodeRequest request = new NodeRequest();
           var puser = new NodeServerPusher();
           if (req != null)
               puser.request = req;
           else
               puser.request = new NodeRequest()
               {
                   ClientList = new ClientList()
               };
         if (idList != null)
             puser.request.ClientList = new ClientList(idList);
         puser.request.JsCmd = "notify";
         puser.Send();
           //.Send();
       }

    }
}
