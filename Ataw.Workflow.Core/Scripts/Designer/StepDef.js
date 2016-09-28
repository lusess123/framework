function ControllAction() {
    this.Order = 0;
    this.ShowKind = 1;
    this.Title = "";
    this.AreaName = "";
    this.ControllName = "";
    this.ActionName = "";
}
//function OperationConfig() {
//    this.Name = "";
//    this.DisplayName = "";
//    this.ButtonCaption = "";
//    this.PlugIn = "";
//}
function UIOperationConfig() {
    //this.OperationConfig = new OperationConfig();
    this.Name = "";
    this.DisplayName = "";
    this.ButtonCaption = "";
    this.PlugIn = "";
}
function NonUIOperationConfig() {
    //this.OperationConfig = new OperationConfig();
    this.Name = "";
    this.DisplayName = "";
    this.ButtonCaption = "";
    this.PlugIn = "";
    this.NeedPrompt = false;
}
function ConnectionConfig() {
    this.Id = ""; //两个节点的ID结合 用“_”连接
    this.Name = "";
    this.DisplayName = "";
    this.FromX = 0;
    this.FromY = 0;
    this.ToX = 0;
    this.ToY = 0;
    this.PlugName = "";
}