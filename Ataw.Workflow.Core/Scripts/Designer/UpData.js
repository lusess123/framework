function WorkflowUpData() {
    $("#dvdvPaintArea_temp input[name='name']").keyup(function () {
        workflowChange("", $(this).val(), "name");
        workflow.name = $(this).val();
    })
    $("#dvdvPaintArea_temp input[name='displayName']").keyup(function () {
        workflowChange("", $(this).val(), "displayname");
        workflow.displayname = $(this).val();
    })
    $("#dvdvPaintArea_temp select[name='priority']").change(function () {
        workflowChange("", $(this).val(), "priority");
        workflow.priority = $(this).val();
    })
    $("#dvdvPaintArea_temp select[name='isSave']").change(function () {
        workflowChange("", $(this).val(), "issave");
        workflow.issave = $(this).val();
    })
    $("#dvdvPaintArea_temp textarea[name='description']").keyup(function () {
        workflowChange("", $(this).val(), "description");
        workflow.description = $(this).val();
    })
    $("#dvdvPaintArea_temp select[name='contentChoice']").change(function () {
        workflowChange("", $(this).val(), "contentchoice");
        workflow.contentchoice = $(this).val();
    })
    $("#dvdvPaintArea_temp input[name='manualPageXml']").keyup(function () {
        workflowChange("", $(this).val(), "manualpagexml");
        workflow.manualpagexml = $(this).val();
    })
    $("#dvdvPaintArea_temp input[name='controllActions']").change(function () {
        workflowChange("", $(this).val(), "controllactions");
        workflow.controllactions = $(this).val() == "" ? null : $.parseJSON($(this).val());
    })
}
function BeginUpData(stepId) {
    var dvName = "dvbegin_temp" + stepId;
    var selectNode = getSelectNode(stepId);
    $("#" + dvName + " input[name='name']").keyup(function () {
        workflowChange(stepId, $(this).val(), "name");
        selectNode.name = $(this).val();
    })
    $("#" + dvName + " input[name='displayName']").keyup(function () {
        workflowChange(stepId, $(this).val(), "text");
        selectNode.text = $(this).val();
        var testDv = $("#" + selectNode.id).find(".tt");
        testDv.text(selectNode.text);
    })
    $("#" + dvName + " input[name='creatorRegName']").keyup(function () {
        workflowChange(stepId, $(this).val(), "creatorregname");
        selectNode.creatorregname = $(this).val();
    })
}
function EndUpData(stepId) {
    var dvName = "dvend_temp" + stepId;
    var selectNode = getSelectNode(stepId);
    $("#" + dvName + " input[name='name']").keyup(function () {
        workflowChange(stepId, $(this).val(), "name");
        selectNode.name = $(this).val();
    })
    $("#" + dvName + " input[name='displayName']").keyup(function () {
        workflowChange(stepId, $(this).val(), "text");
        selectNode.text = $(this).val();
        var testDv = $("#" + selectNode.id).find(".tt");
        testDv.text(selectNode.text);
    })
    $("#" + dvName + " input[name='plugRegName']").keyup(function () {
        workflowChange(stepId, $(this).val(), "plugregname");
        selectNode.plugregname = $(this).val();
    })
}
function ManualUpData(stepId) {
    var dvName = "dvmanual_temp" + stepId;
    var selectNode = getSelectNode(stepId);
    $("#" + dvName + " input[name='name']").keyup(function () {
        workflowChange(stepId, $(this).val(), "name");
        selectNode.name = $(this).val();
    })
    $("#" + dvName + " input[name='displayName']").keyup(function () {
        workflowChange(stepId, $(this).val(), "text");
        selectNode.text = $(this).val();
        var testDv = $("#" + selectNode.id).find(".tt");
        testDv.text(selectNode.text);
    })
    $("#" + dvName + " input[name='actorRegName']").keyup(function () {
        workflowChange(stepId, $(this).val(), "actorregname");
        selectNode.actorregname = $(this).val();
    })
    $("#" + dvName + " input[name='manualPageXml']").keyup(function () {
        workflowChange(stepId, $(this).val(), "manualpagexml");
        selectNode.manualpagexml = $(this).val();
    })
    $("#" + dvName + " input[name='controllActions']").change(function () {
        workflowChange(stepId, $(this).val(), "controllactions");
        selectNode.controllactions = $(this).val() == "" ? null : $.parseJSON($(this).val());
    })
    $("#" + dvName + " input[name='process']").change(function () {
        workflowChange(stepId, $(this).val(), "process");
        selectNode.process = $(this).val() == "" ? null : $.parseJSON($(this).val());
    })
    $("#" + dvName + " select[name='contentchoice']").change(function () {
        workflowChange(stepId, $(this).val(), "contentchoice");
        selectNode.contentchoice = $(this).val();
    })
}
function AutoUpData(stepId) {
    var dvName = "dvauto_temp" + stepId;
    var selectNode = getSelectNode(stepId);
    $("#" + dvName + " input[name='name']").keyup(function () {
        workflowChange(stepId, $(this).val(), "name");
        selectNode.name = $(this).val();
    })
    $("#" + dvName + " input[name='displayName']").keyup(function () {
        workflowChange(stepId, $(this).val(), "text");
        selectNode.text = $(this).val();
        var testDv = $("#" + selectNode.id).find(".tt");
        testDv.text(selectNode.text);
    })
    $("#" + dvName + " input[name='plugRegName']").keyup(function () {
        workflowChange(stepId, $(this).val(), "plugregname");
        selectNode.plugregname = $(this).val();
    })
}


function RouteUpData(stepId) {
    var dvName = "dvroute_temp" + stepId;
    var selectNode = getSelectNode(stepId);
    $("#" + dvName + " input[name='name']").keyup(function () {
        workflowChange(stepId, $(this).val(), "name");
        selectNode.name = $(this).val();
    })
    $("#" + dvName + " input[name='displayName']").keyup(function () {
        workflowChange(stepId, $(this).val(), "text");
        selectNode.text = $(this).val();
        var testDv = $("#" + selectNode.id).find(".tt");
        testDv.text(selectNode.text);
    })
}
function ConnectionUpData(stepId) {
    var dvName = "dvconnection_temp" + stepId;
    var selectNode = getSelectNode(stepId);
    $("#" + dvName + " input[name='name']").keyup(function () {
        workflowChange(stepId, $(this).val(), "name");
        selectNode.name = $(this).val();
    })
    $("#" + dvName + " input[name='plugName']").keyup(function () {
        workflowChange(stepId, $(this).val(), "plugname");
        selectNode.plugname = $(this).val();
    })
    $("#" + dvName + " input[name='displayName']").keyup(function () {
        workflowChange(stepId, $(this).val(), "text");
        selectNode.text = $(this).val();
        //        if (selectNode.nodetype == 100) {
        if ($.browser.msie) {
            if ($("#" + selectNode.id + ">div").length == 0) { $("#" + selectNode.id).append("<div style='position:absolute;left:50%;top:50%;overflow:visible;cursor:default;'></div>") }
            $("#" + selectNode.id + ">div").html(selectNode.text);
        }
        else {
            if ($("#" + selectNode.id + ">div").length == 0) { $("#" + selectNode.id).append("<div></div>") }
            $("#" + selectNode.id + ">div").html(selectNode.text);
        }
        //        } 
        //        else {
        //            var testDv = $("#" + selectNode.id).find(".tt");
        //            testDv.text(selectNode.text);
        //        }
    })
}

function getSelectNode(id) {

    //var node = workflow.config.nowselect;
    //    if (node == null) { return }
    //    node = workflow.tool.tojQuery(node);
    //    var id = node.attr("id");
    if (id.indexOf("_") < 0)/*元素节点*/
    {
        return workflow.nodelist.get(id);
    }
    else/*连线节点*/
    {
        return workflow.linelist.get(id);
    }
}
function getBaseNode(id) {
    if (id.indexOf("_") < 0)/*元素节点*/
    {
        return baseWorkflow.nodelist.get(id);
    }
    else/*连线节点*/
    {
        return baseWorkflow.linelist.get(id);
    }
}
function workflowChange(id, text, attr) {
    if (workflow.id == "") {
        return;
    }
    var baseNode = id == "" ? baseWorkflow : getBaseNode(id);
    if (baseNode == null) { return; } //当新增节点或线条
    var currentNode = id == "" ? workflow : getSelectNode(id);
    if (currentNode == null) { return; } //当删除节点或线条
    var baseNodeAttrValue = (typeof baseNode[attr]) != "object" ? baseNode[attr] : $.toJSON(baseNode[attr]);
    var currentNodeAttrValue = (typeof currentNode[attr]) != "object" ? currentNode[attr] : $.toJSON(currentNode[attr]);
    //当字段改变changeCount就+1（多次改变只加+1次）
    if (baseNodeAttrValue == currentNodeAttrValue) {
        if (baseNodeAttrValue != text) {
            workflow.changeCount += 1;
        }
    }
    //当字段还原changeCount就-1（多次相等只减一次）
    else if (baseNodeAttrValue == text) {
        workflow.changeCount -= 1;
    }
}