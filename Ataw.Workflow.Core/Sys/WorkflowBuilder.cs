using Ataw.Framework.Core;
using Ataw.Workflow.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Workflow.Core.Sys
{
   public class WorkflowBuilder:IWorflowBuilder
    {

      // public 

       public Workflow StartWorkFlow(string key, string defineName, string creatorId)
        {
           // throw new NotImplementedException();
            WorkflowDbContext source = new WorkflowDbContext(AtawAppContext.Current.DefaultConnString);
            WorkflowConfig.ConnString = AtawAppContext.Current.DefaultConnString;
            RegNameList<KeyValueItem> param = new RegNameList<KeyValueItem>();
            //if (!string.IsNullOrEmpty(paramList))
            //{
            //    List<KeyValueItem> list = new List<KeyValueItem>();
            //    list = FastToObject<List<KeyValueItem>>(paramList);
            //    list.ForEach(a => param.Add(a));
            //}
            param.Add(new KeyValueItem()
            {
                Key = "MainKey",
                Value = key
            });
          var wf =   Workflow.CreateWorkflow(source, defineName, param,
                creatorId, null);
          wf.Run();
         // return wf.WorkflowId;
          return wf;
        }
    }
}
