function AtawWfConfig_Fun() {
    // if($("#dvPaintArea").length)
    ShowTab(null, "dvPaintArea_temp", "dvdvPaintArea_temp", WorkflowUpData);
}
function iniWorkflowAttr() {
    $("#dvdvPaintArea_temp input[name='hdnId']").val(workflow.id);
    $("#dvdvPaintArea_temp input[name='name']").val(workflow.name);
    $("#dvdvPaintArea_temp input[name='displayName']").val(workflow.displayname);
    $("#dvdvPaintArea_temp textarea[name='description']").val(workflow.description);
    $("#dvdvPaintArea_temp select[name='priority'] option[value='" + workflow.priority + "']").attr("selected", true);
    $("#dvdvPaintArea_temp select[name='isSave'] option[value='" + workflow.issave + "']").attr("selected", true);
    $("#dvdvPaintArea_temp input[name='manualPageXml']").val(workflow.manualpagexml);
    $("#dvdvPaintArea_temp select[name='contentChoice'] option[value='" + workflow.contentchoice + "']").attr("selected", true);
    $("#dvdvPaintArea_temp input[name='controllActions']").val(workflow.controllactions == null ? "" : $.toJSON(workflow.controllactions));
}
function HideAllTab() {
    $("#attribute").children().each(
    function (i) {
        $(this).hide();
    }
    );
}
//---------------------实用方法
function ShowTab(nodeobj, tempName, dvName, LoadFun) {
    var dv = $("#" + dvName);
    if (dv.length == 0) {
        var g = $("#" + tempName).parent().html();
        HideAllTab();
        var g = $(g);
        g.attr("id", dvName)
        $("#attribute").append(g);
        g.show();
        if (nodeobj !== null) {
            $("#" + dvName + " input[name='displayName']").val(nodeobj.text);
            $("#" + dvName + " input[name='name']").val(nodeobj.name);
            switch (nodeobj.nodetype) {
                case 1:
                    $("#" + dvName + " input[name='creatorRegName']").val(nodeobj.creatorregname);
                    break;
                case 2:
                    $("#" + dvName + " input[name='plugRegName']").val(nodeobj.plugregname);
                    break;
                case 20:
                    $("#" + dvName + " input[name='manualPageXml']").val(nodeobj.manualpagexml);
                    $("#" + dvName + " input[name='controllActions']").val(nodeobj.controllactions == null ? "" : $.toJSON(nodeobj.controllactions));
                    $("#" + dvName + " input[name='process']").val(nodeobj.process == null ? "" : $.toJSON(nodeobj.process));
                    $("#" + dvName + " input[name='actorRegName']").val(nodeobj.actorregname);
                    $("#" + dvName + " select[name='contentchoice'] option[value='" + nodeobj.contentchoice + "']").attr("selected", true);
                    break;
                case 22:
                    $("#" + dvName + " input[name='plugRegName']").val(nodeobj.plugregname);
                    break;
                case 21:
                    break;
                case 100:
                    $("#" + dvName + " input[name='plugName']").val(nodeobj.plugname);
                    break;
            }
            LoadFun(nodeobj);
        }
        else {
            LoadFun()
        }
    }
    else {
        var isHide = dv.is(":hidden");
        if (isHide) {
            HideAllTab();
            dv.show();
        }

    }
}
//-----------------------------------
//删除节点
function DelNode_Fun(nodeobj) {
    var attrNodes = $("#attribute").children();
    for (var i = 0; i < attrNodes.length; i++) {
        if ($(attrNodes[i]).attr("id").indexOf(nodeobj.id) != -1) {
            //$("#attribute:eq(" + i + ")").remove()
            $("#" + $(attrNodes[i]).attr("id")).remove();
        }
    }
}
//删除连接线
function DelLine_Fun(lineId) {
    var attrNodes = $("#attribute").children();
    for (var i = 0; i < attrNodes.length; i++) {
        if ($(attrNodes[i]).attr("id").indexOf(lineId) != -1) {
            $("#" + $(attrNodes[i]).attr("id")).remove();
        }
    }
}
function SetNode_Fun(nodeobj) {
    var tempName = "";
    var fun = null;
    switch (nodeobj.nodetype) {
        case 1:
            tempName = "begin_temp";
            fun = BeginUpData;
            break;
        case 2:
            tempName = "end_temp";
            fun = EndUpData;
            break;
        case 20:
            tempName = "manual_temp";
            fun = ManualUpData;
            break;
        case 22:
            tempName = "auto_temp";
            fun = AutoUpData;
            break;
        case 21:
            tempName = "route_temp";
            fun = RouteUpData;
            break;
        case 100:
            tempName = "connection_temp";
            fun = ConnectionUpData;
            break;
        default:
            alert("错误，未定义模板");
            break;
    }
    ShowTab(nodeobj, tempName, "dv" + tempName + nodeobj.id,
    function (nodeobj) {
        //加入同步事件
        fun(nodeobj.id);
    }
     );

}