using System;
using System.Collections.Generic;
using Ataw.Framework.Core;
using Ataw.Framework.Web;
using Ataw.Workflow.Core;
using Ataw.Workflow.Core.DataAccess;

namespace Ataw.Workflow.Web
{
    public class WorkflowController : AtawBaseController
    {
        //
        // GET: /Workflow/
        private readonly string connStr = PlugAreaRegistration.CONN;

        public string Creator(string workflowDefine, string key, string paramList)
        {
            try
            {
                WorkflowDbContext source = new WorkflowDbContext(connStr);
                WorkflowConfig.ConnString = connStr;
                RegNameList<KeyValueItem> param = new RegNameList<KeyValueItem>();
                if (!string.IsNullOrEmpty(paramList))
                {
                    List<KeyValueItem> list = new List<KeyValueItem>();
                    list = FastToObject<List<KeyValueItem>>(paramList);
                    list.ForEach(a => param.Add(a));
                }
                param.Add(new KeyValueItem()
                {
                    Key = "MainKey",
                    Value = key
                });
                var wf = Ataw.Workflow.Core.Workflow.CreateWorkflow(source, workflowDefine, param,
                    GlobalVariable.UserId.ToString(), null);
                return "OK" + wf.GetWorkflowUrl();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
