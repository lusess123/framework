using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Ataw.Framework.Core;
using Ataw.Framework.Web;
using Ataw.Workflow.Core;
using Ataw.Workflow.Core.DataAccess;
using Ataw.Workflow.Web.Bussiness;
using WorkFlowDesigner.Models;

namespace Ataw.Workflow.Web
{
    public class DesignerController : AtawBaseController
    {
        //
        // GET: /Designer/
        //已生成的节点
        private Dictionary<string, StepConfig> steps = new Dictionary<string, StepConfig>();
        private WorkflowDbContext context = new WorkflowDbContext(PlugAreaRegistration.CONN);
        private WorkFlowDef workflowDef = new WorkFlowDef();
        public ActionResult Index(string shortName = "-1")
        {
            ViewBag.shortName = shortName;
            return View();
        }
        public string AddWorkflow(string workflow)
        {

            workflow = FormatXml(HttpUtility.UrlDecode(workflow));
            Root rt = XmlUtil.ReadFromString<Root>(workflow);
            var config = Root2WorkflowConfig(rt);
            return workflowDef.AddOrEditWorkflowDefine(config);
            //rt.SaveStringBuilder(sb);
            //  return "1";
        }
        public string GetWorkflowXML(string shortName)
        {
            //string workflowXml = workflowDef.GetWorkFlowDef(shortName).WD_CONTENT;
            //WorkflowConfig workflowConfig = XmlUtil.ReadFromString<WorkflowConfig>(workflowXml);
            //workflowConfig.Steps.ToList().ForEach(a => a.Parent = workflowConfig);

            var workflowConfig = WorkflowConfig.GetByName(shortName, context);

            Root root = WorkflowConfig2Root(workflowConfig);
            return root.SaveString(Formatting.None);
        }
        public WorkflowConfig Root2WorkflowConfig(Root root)
        {
            WorkflowConfig config = new WorkflowConfig();
            config.Id = root.ID;
            config.Name = root.Name;
            config.DisplayName = root.DisplayName;
            config.Description = root.Description;
            config.Priority = root.Priority.Value<WorkflowPriority>();
            config.IsSaveContent = root.IsSave == "1" ? true : false;
            config.ContentChoice = root.ContentChoice.Value<ConfigChoice>();
            config.ContentXml = root.ManualPageXml ?? "";
            if (root.ControllActions != null)
            {
                config.ControlActions = new RegNameList<ControlActionConfig>();
                ControlActionConfig controllAction;
                foreach (ControllAction ca in root.ControllActions)
                {
                    controllAction = new ControlActionConfig();
                    controllAction.ActionName = ca.ActionName;
                    controllAction.AreaName = ca.AreaName;
                    controllAction.ControlName = ca.ControllName;
                    controllAction.Order = ca.Order;
                    controllAction.Title = ca.Title == null ? "" : ca.Title;
                    controllAction.ShowKind = ca.ShowKind.Value<ShowKind>();
                    config.ControlActions.Add(controllAction);
                }
            }
            for (int i = 0; i < root.LineList.Count; i++)
            {
                CreateLineNode(config, root, root.LineList[i]);
            }
            string gg = config.SaveString(Formatting.None);
            return config;
        }
        private string FormatXml(string rootStr)
        {
            Regex reg = new Regex(@"(?<=<[^>]*?\b[^>]+\b\s*=\s*)(?![""'\s])([^>]+?)(?<![""'\s])(?=((\s+[^>]*)|)>)");
            return reg.Replace(rootStr, "\"$1\"");
        }
        private string FormatXml(string rootStr, int start = 0)
        {

            int ide = rootStr.IndexOf("id=", start);
            if (ide > 0)
            {
                char pide = rootStr[ide + 3];
                if (pide != '"')
                {
                    int idRight = rootStr.IndexOf('>', ide);
                    int idQues = rootStr.IndexOf(' ', ide, idRight - ide);
                    int idee = idQues != -1 ? idQues : idRight;
                    //int idee = idQues > idRight ? idRight : idQues;
                    //if (idQues > idRight)
                    //{
                    //    int idLeft = rootStr.IndexOf('<', ide);
                    //    if (idLeft < idRight)
                    //    {
                    //          return FormatXml(rootStr, ide + 3);
                    //    }
                    //}
                    if (idee > 0)
                    {
                        string strId = rootStr.Substring(ide + 3, idee - ide - 3);
                        rootStr = rootStr.Replace("id=" + strId, "id=\"" + strId + "\"");
                        return FormatXml(rootStr, ide + 3);
                    }
                    else
                        return FormatXml(rootStr, ide + 3);

                }
                return FormatXml(rootStr, ide + 3);


            }
            return rootStr;
        }
        public void CreateLineNode(WorkflowConfig wfConfig, Root rt, Line line)
        {
            string lineId = line.ID;
            StepConfig fromStep = null;
            StepConfig toStep = null;
            int index;
            string nodeId;
            #region 循环查找
            for (int i = 0; i < rt.Nodes.Count; i++)
            {
                nodeId = rt.Nodes[i].ID.Value;
                index = lineId.IndexOf(nodeId);
                if (index != -1)
                {
                    if (steps.Keys.Contains(nodeId))
                    {
                        if (index == 0)
                        {
                            fromStep = steps[nodeId];
                            RouteStepConfig routeStepConfig = fromStep as RouteStepConfig;
                            if (routeStepConfig != null)
                            {
                                ConnectionConfig connectionConfig = new ConnectionConfig();
                                connectionConfig.DisplayName = line.Text;
                                connectionConfig.PlugName = line.PlugName;
                                connectionConfig.Name = line.Name;
                                routeStepConfig.Connections.Add(connectionConfig);
                            }
                        }
                        else
                        {
                            toStep = steps[nodeId];
                        }
                    }
                    else
                    {
                        StepConfig stepConfig = null;
                        #region 将nodes转化为workflowconfig
                        switch ((NodeType)rt.Nodes[i].NodeType.Value)
                        {
                            case NodeType.Begin:
                                BeginStepConfig beginStep = new BeginStepConfig(wfConfig);
                                beginStep.Id = rt.Nodes[i].ID.Value;
                                beginStep.Name = rt.Nodes[i].Name.Value;
                                beginStep.DisplayName = rt.Nodes[i].Text.Value; ;
                                beginStep.Height = rt.Nodes[i].Height.Value;
                                beginStep.Width = rt.Nodes[i].Width.Value;
                                beginStep.Left = rt.Nodes[i].X.Value;
                                beginStep.Top = rt.Nodes[i].Y.Value;
                                beginStep.CreatorRegName = rt.Nodes[i].CreatorRegName.Value;
                                steps.Add(nodeId, beginStep);
                                wfConfig.Steps.Add(beginStep);
                                stepConfig = beginStep;
                                break;
                            case NodeType.End:
                                EndStepConfig endStep = new EndStepConfig(wfConfig);
                                endStep.Id = rt.Nodes[i].ID.Value;
                                endStep.Name = rt.Nodes[i].Name.Value;
                                endStep.DisplayName = rt.Nodes[i].Text.Value; ;
                                endStep.Height = rt.Nodes[i].Height.Value;
                                endStep.Width = rt.Nodes[i].Width.Value;
                                endStep.Left = rt.Nodes[i].X.Value;
                                endStep.Top = rt.Nodes[i].Y.Value;
                                endStep.PlugRegName = rt.Nodes[i].PlugRegName.Value;
                                steps.Add(nodeId, endStep);
                                wfConfig.Steps.Add(endStep);
                                stepConfig = endStep;
                                break;
                            case NodeType.Auto:
                                AutoStepConfig autoStep = new AutoStepConfig(wfConfig);
                                autoStep.Id = rt.Nodes[i].ID.Value;
                                autoStep.Name = rt.Nodes[i].Name.Value;
                                autoStep.DisplayName = rt.Nodes[i].Text.Value; ;
                                autoStep.Height = rt.Nodes[i].Height.Value;
                                autoStep.Width = rt.Nodes[i].Width.Value;
                                autoStep.Left = rt.Nodes[i].X.Value;
                                autoStep.Top = rt.Nodes[i].Y.Value;
                                autoStep.PlugRegName = rt.Nodes[i].PlugRegName.Value;
                                steps.Add(nodeId, autoStep);
                                wfConfig.Steps.Add(autoStep);
                                stepConfig = autoStep;
                                break;
                            case NodeType.Route:
                                RouteStepConfig routeStep = new RouteStepConfig(wfConfig);
                                routeStep.Id = rt.Nodes[i].ID.Value;
                                routeStep.Name = rt.Nodes[i].Name.Value;
                                routeStep.DisplayName = rt.Nodes[i].Text.Value;
                                routeStep.Height = rt.Nodes[i].Height.Value;
                                routeStep.Width = rt.Nodes[i].Width.Value;
                                routeStep.Left = rt.Nodes[i].X.Value;
                                routeStep.Top = rt.Nodes[i].Y.Value;
                                if (index == 0)
                                {
                                    ConnectionConfig connection = new ConnectionConfig();
                                    connection.DisplayName = line.Text;
                                    connection.PlugName = line.PlugName;
                                    connection.Name = line.Name;
                                    routeStep.Connections.Add(connection);
                                }
                                steps.Add(nodeId, routeStep);
                                wfConfig.Steps.Add(routeStep);
                                stepConfig = routeStep;
                                break;
                            case NodeType.Manual:
                                ManualStepConfig manualStep = new ManualStepConfig(wfConfig);
                                manualStep.Id = rt.Nodes[i].ID.Value;
                                manualStep.Name = rt.Nodes[i].Name.Value;
                                manualStep.DisplayName = rt.Nodes[i].Text.Value;
                                manualStep.Height = rt.Nodes[i].Height.Value;
                                manualStep.Width = rt.Nodes[i].Width.Value;
                                manualStep.Left = rt.Nodes[i].X.Value;
                                manualStep.Top = rt.Nodes[i].Y.Value;
                                manualStep.ActorRegName = rt.Nodes[i].ActorRegName.Value;
                                manualStep.ContentXml = rt.Nodes[i].ManualPageXml.Value;
                                manualStep.ContentChoice = rt.Nodes[i].ContentChoice.Value.Value<ConfigChoice>();
                                if (rt.Nodes[i].ControllActions != null)
                                {
                                    manualStep.ControlActions = new Ataw.Framework.Core.RegNameList<ControlActionConfig>();
                                    ControlActionConfig controlActionConfig;
                                    for (int j = 0; j < rt.Nodes[i].ControllActions.Count; j++)
                                    {
                                        controlActionConfig = new ControlActionConfig
                                        {
                                            ShowKind = (ShowKind)rt.Nodes[i].ControllActions[j].ShowKind,
                                            Title = rt.Nodes[i].ControllActions[j].Title,
                                            Order = rt.Nodes[i].ControllActions[j].Order,
                                            AreaName = rt.Nodes[i].ControllActions[j].AreaName,
                                            ControlName = rt.Nodes[i].ControllActions[j].ControllName,
                                            ActionName = rt.Nodes[i].ControllActions[j].ActionName
                                        };
                                        manualStep.ControlActions.Add(controlActionConfig);
                                    }
                                }
                                if (rt.Nodes[i].Process != null)
                                {
                                    manualStep.Process = new ProcessConfig();
                                    if (rt.Nodes[i].Process.UIOperationConfig != null)
                                    {
                                        manualStep.Process.UIOperation = new Ataw.Workflow.Core.UIOperationConfig();
                                        manualStep.Process.UIOperation.Name = rt.Nodes[i].Process.UIOperationConfig.Name;
                                        manualStep.Process.UIOperation.DisplayName = rt.Nodes[i].Process.UIOperationConfig.DisplayName;
                                        manualStep.Process.UIOperation.ButtonCaption = rt.Nodes[i].Process.UIOperationConfig.ButtonCaption;
                                        manualStep.Process.UIOperation.PlugIn = rt.Nodes[i].Process.UIOperationConfig.Plugin;
                                    }
                                    if (rt.Nodes[i].Process.NonUIOperationConfigs != null)
                                    {
                                        manualStep.Process.NonUIOperations = new Ataw.Framework.Core.RegNameList<Ataw.Workflow.Core.NonUIOperationConfig>();
                                        Ataw.Workflow.Core.NonUIOperationConfig nonUIOperationConfig;
                                        for (int j = 0; j < rt.Nodes[i].Process.NonUIOperationConfigs.Count; j++)
                                        {
                                            nonUIOperationConfig = new Ataw.Workflow.Core.NonUIOperationConfig
                                            {
                                                Name = rt.Nodes[i].Process.NonUIOperationConfigs[j].Name,
                                                DisplayName = rt.Nodes[i].Process.NonUIOperationConfigs[j].DisplayName,
                                                ButtonCaption = rt.Nodes[i].Process.NonUIOperationConfigs[j].ButtonCaption,
                                                PlugIn = rt.Nodes[i].Process.NonUIOperationConfigs[j].Plugin,
                                                NeedPrompt = rt.Nodes[i].Process.NonUIOperationConfigs[j].NeedPrompt == "1" ? true : false
                                            };
                                            manualStep.Process.NonUIOperations.Add(nonUIOperationConfig);
                                        }
                                    }
                                }
                                steps.Add(nodeId, manualStep);
                                wfConfig.Steps.Add(manualStep);
                                stepConfig = manualStep;
                                break;
                        }
                        #endregion
                        if (index == 0)
                        {
                            fromStep = stepConfig;
                        }
                        else
                        {
                            toStep = stepConfig;
                        }
                    }
                }
            }
            #endregion
            if (fromStep != null && toStep != null)
            {
                //设置路由步骤的Connection 的下一步名称
                RouteStepConfig rsc = fromStep as RouteStepConfig;
                if (rsc != null)
                {
                    rsc.Connections[rsc.Connections.Count - 1].NextStepName = toStep.Name;
                }
                fromStep.AddNextConfig(toStep, fromStep.Left, fromStep.Top, toStep.Left, toStep.Top);
            }
        }

        public Root WorkflowConfig2Root(WorkflowConfig workflowConfig)
        {
            Root root = new Root();
            root.ID = workflowConfig.Id;
            root.Description = workflowConfig.Description;
            root.DisplayName = workflowConfig.DisplayName;
            root.IsSave = workflowConfig.IsSaveContent.ToString();
            root.Name = workflowConfig.Name;
            root.Priority = ((int)workflowConfig.Priority).ToString();
            root.ContentChoice = ((int)workflowConfig.ContentChoice).ToString();
            root.ManualPageXml = workflowConfig.ContentXml ?? "";
            if (workflowConfig.ControlActions != null)
            {
                root.ControllActions = new List<ControllAction>();
                ControllAction controllAction;
                foreach (ControlActionConfig caConfig in workflowConfig.ControlActions)
                {
                    controllAction = new ControllAction();
                    controllAction.ActionName = caConfig.ActionName;
                    controllAction.AreaName = caConfig.AreaName;
                    controllAction.ControllName = caConfig.ControlName;
                    controllAction.Order = caConfig.Order;
                    controllAction.Title = caConfig.Title == null ? "" : caConfig.Title;
                    controllAction.ShowKind = (int)caConfig.ShowKind;
                    root.ControllActions.Add(controllAction);
                }
            }
            root.Nodes = new List<Node>();
            root.LineList = new List<Line>();
            Node node;
            for (int i = 0; i < workflowConfig.Steps.Count; i++)
            {
                node = new Node();
                Line line = new Line();
                StepConfig stepConfig = workflowConfig.Steps[i];
                BeginStepConfig beginStep = workflowConfig.Steps[i] as BeginStepConfig;
                EndStepConfig endStep = workflowConfig.Steps[i] as EndStepConfig;
                AutoStepConfig autoStep = workflowConfig.Steps[i] as AutoStepConfig;
                ManualStepConfig manualStep = workflowConfig.Steps[i] as ManualStepConfig;
                RouteStepConfig routeStep = workflowConfig.Steps[i] as RouteStepConfig;
                node.ObjectIndex = new BaseIntElement { Type = "number", Value = 0 };
                node.TypeIndex = new BaseIntElement { Type = "number", Value = 0 };
                stepConfig.Id = string.IsNullOrEmpty(stepConfig.Id) ? context.GetUniId() : stepConfig.Id;
                node.ID = new BaseStringElement { Type = "string", Value = stepConfig.Id };
                node.Text = new BaseStringElement { Type = "string", Value = stepConfig.DisplayName };
                node.Name = new BaseStringElement { Type = "string", Value = stepConfig.Name };
                node.Height = new BaseIntElement { Type = "number", Value = stepConfig.Height == 0 ? 51 : stepConfig.Height };
                node.Width = new BaseIntElement { Type = "number", Value = stepConfig.Width == 0 ? 130 : stepConfig.Width };
                node.X = new BaseIntElement { Type = "number", Value = stepConfig.Left };
                node.Y = new BaseIntElement { Type = "number", Value = stepConfig.Top };
                node.ParentList = new List<Parent>();
                node.ChildList = new List<Child>();
                if (stepConfig.HasInStep)
                {
                    Parent parent;
                    ////输入节点数量 0：无输入 1：1输入 2：多输入
                    //switch (stepConfig.PrevSteps.Count())
                    //{
                    //    case 0:
                    //        node.InputType = new BaseIntElement { Type = "number", Value = 0 };
                    //        break;
                    //    case 1:
                    //        node.InputType = new BaseIntElement { Type = "number", Value = 1 };
                    //        break;
                    //    default:
                    //        node.InputType = new BaseIntElement { Type = "number", Value = 2 };
                    //        break;
                    //}
                    foreach (StepConfig sc in stepConfig.PrevSteps)
                    {
                        sc.Id = string.IsNullOrEmpty(sc.Id) ? context.GetUniId() : sc.Id;
                        parent = new Parent();
                        parent.Value = sc.Id;
                        node.ParentList.Add(parent);
                    }
                }
                if (stepConfig.HasOutStep)
                {
                    Child child;
                    //switch (stepConfig.NextStepCount)
                    //{
                    //    case 0:
                    //        node.OutputType = new BaseIntElement { Type = "number", Value = 0 };
                    //        break;
                    //    case 1:
                    //        node.OutputType = new BaseIntElement { Type = "number", Value = 1 };
                    //        break;
                    //    default:
                    //        node.OutputType = new BaseIntElement { Type = "number", Value = 2 };
                    //        break;
                    //}
                    foreach (StepConfig sc in stepConfig.NextSteps)
                    {
                        sc.Id = string.IsNullOrEmpty(sc.Id) ? context.GetUniId() : sc.Id;
                        child = new Child();
                        child.Value = sc.Id;
                        node.ChildList.Add(child);
                        line = new Line();
                        line.ID = stepConfig.Id + "_" + sc.Id;
                        if (routeStep != null)
                        {
                            for (int k = 0; k < routeStep.Connections.Count; k++)
                            {
                                if (routeStep.Connections[k].NextStepName == sc.Name)
                                {
                                    line.Name = routeStep.Connections[k].Name;
                                    line.PlugName = routeStep.Connections[k].PlugName;
                                    line.Text = routeStep.Connections[k].DisplayName;
                                    break;
                                }
                            }
                        }
                        root.LineList.Add(line);
                    }
                }
                if (beginStep != null)
                {
                    node.Icon = new BaseStringElement { Type = "string", Value = "nodeicon1" };
                    node.NodeType = new BaseIntElement { Type = "number", Value = (int)NodeType.Begin };
                    node.CreatorRegName = new BaseStringElement { Type = "string", Value = beginStep.CreatorRegName };
                    node.InputType = new BaseIntElement { Type = "number", Value = 0 };
                    node.OutputType = new BaseIntElement { Type = "number", Value = 1 };
                }
                else if (endStep != null)
                {
                    node.Icon = new BaseStringElement { Type = "string", Value = "nodeicon2" };
                    node.NodeType = new BaseIntElement { Type = "number", Value = (int)NodeType.End };
                    node.PlugRegName = new BaseStringElement { Type = "string", Value = endStep.PlugRegName };
                    node.InputType = new BaseIntElement { Type = "number", Value = 2 };
                    node.OutputType = new BaseIntElement { Type = "number", Value = 0 };
                }
                else if (routeStep != null)
                {
                    node.Icon = new BaseStringElement { Type = "string", Value = "nodeicon3" };
                    node.NodeType = new BaseIntElement { Type = "number", Value = (int)NodeType.Route };
                    node.InputType = new BaseIntElement { Type = "number", Value = 2 };
                    node.OutputType = new BaseIntElement { Type = "number", Value = 2 };
                }
                else if (autoStep != null)
                {
                    node.Icon = new BaseStringElement { Type = "string", Value = "nodeicon-auto" };
                    node.NodeType = new BaseIntElement { Type = "number", Value = (int)NodeType.Auto };
                    node.PlugRegName = new BaseStringElement { Type = "string", Value = autoStep.PlugRegName };
                    node.InputType = new BaseIntElement { Type = "number", Value = 2 };
                    node.OutputType = new BaseIntElement { Type = "number", Value = 1 };
                }
                else if (manualStep != null)
                {
                    node.Icon = new BaseStringElement { Type = "string", Value = "nodeicon5" };
                    node.NodeType = new BaseIntElement { Type = "number", Value = (int)NodeType.Manual };
                    node.ActorRegName = new BaseStringElement { Type = "string", Value = manualStep.ActorRegName };
                    node.InputType = new BaseIntElement { Type = "number", Value = 2 };
                    node.OutputType = new BaseIntElement { Type = "number", Value = 1 };
                    node.ManualPageXml = new BaseStringElement { Type = "string", Value = manualStep.ContentXml };
                    node.ContentChoice = new BaseIntElement { Type = "number", Value = (int)manualStep.ContentChoice };

                    if (manualStep.ControlActions != null)
                    {
                        node.ControllActions = new List<ControllAction>();
                        ControllAction controllAction;
                        foreach (ControlActionConfig caConfig in manualStep.ControlActions)
                        {
                            controllAction = new ControllAction();
                            controllAction.ActionName = caConfig.ActionName;
                            controllAction.AreaName = caConfig.AreaName;
                            controllAction.ControllName = caConfig.ControlName;
                            controllAction.Order = caConfig.Order;
                            controllAction.Title = caConfig.Title == null ? "" : caConfig.Title;
                            controllAction.ShowKind = (int)caConfig.ShowKind;
                            node.ControllActions.Add(controllAction);
                        }
                    }

                    if (manualStep.Process != null)
                    {
                        node.Process = new Process();
                        node.Process.UIOperationConfig = new WorkFlowDesigner.Models.UIOperationConfig();
                        if (manualStep.Process.UIOperation != null)
                        {
                            node.Process.UIOperationConfig.ButtonCaption = manualStep.Process.UIOperation.ButtonCaption;
                            node.Process.UIOperationConfig.DisplayName = manualStep.Process.UIOperation.DisplayName;
                            node.Process.UIOperationConfig.Name = manualStep.Process.UIOperation.Name;
                            node.Process.UIOperationConfig.Plugin = manualStep.Process.UIOperation.PlugIn;
                        }
                        node.Process.NonUIOperationConfigs = new List<WorkFlowDesigner.Models.NonUIOperationConfig>();
                        WorkFlowDesigner.Models.NonUIOperationConfig nonUiConfig;
                        if (manualStep.Process.NonUIOperations != null)
                        {
                            foreach (var nonUi in manualStep.Process.NonUIOperations)
                            {
                                nonUiConfig = new WorkFlowDesigner.Models.NonUIOperationConfig();
                                nonUiConfig.ButtonCaption = nonUi.ButtonCaption;
                                nonUiConfig.DisplayName = nonUi.DisplayName;
                                nonUiConfig.Name = nonUi.Name;
                                nonUiConfig.NeedPrompt = nonUi.NeedPrompt.ToString();
                                nonUiConfig.Plugin = nonUi.PlugIn;
                                node.Process.NonUIOperationConfigs.Add(nonUiConfig);
                            }
                        }
                    }
                }
                root.Nodes.Add(node);
            }
            return root;
        }
    }
}