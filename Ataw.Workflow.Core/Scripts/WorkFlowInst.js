function reloadList() {
    $("#list").datagrid("reload", { defName: trim($("#defName").val()), name: trim($("#name").val()), begin: $("#BeginTime").val(), end: $("#EndTime").val() });
}
$(function () {
    $("#search").click(function () {
        reloadList();
    })
    $("#list").datagrid({
        pageList: [15],
        title: "工作流实例列表",
        height: 450,
        width: $(".listBox").width()*0.97,
        fitColumns: true,
        collapsible: true,
        sortName: "WI_CREATE_DATE",
        sortOrder: "desc",
        idField: "WI_ID",
        loadMsg: "数据加载中，请稍后...",
        url: "/WorkFlow/WorkFlowInst/InitData",
        frozenColumns: [[
            { field: "ck", checkbox: true }
        ]],
        columns: [[
            { field: "WI_WD_NAME", title: "流程模式名", width: 150, sortable: true },
            { field: "WI_NAME", title: "名称", width: 180, sortable: true },
            { field: "WI_CURRENT_STEP_NAME", title: "当前步骤", width: 80 },
            { field: "WI_CREATE_DATE", title: "创建时间", width: 180, sortable: true },
            { field: "WI_END_DATE", title: "结束时间", width: 180, sortable: true }
        ]],
        pagination: true,
        onSortColumn: function () {
            reloadList();
        },
        onClickRow: function (rowIndex) {
            $('#list').datagrid("unselectRow", rowIndex);
        }
    });
    $("#btnDel").click(function () {
        var rows = $("#list").datagrid("getSelections");
        var ids = [];
        for (var i = 0; i < rows.length; i++) {
            ids.push(rows[i].WI_ID);
        }
        if (ids == false) {
            $.messager.alert("提示框", "未选择任何项")
            return;
        }
        $.messager.confirm("提示框", "确定删除吗？", function (r) {
            if (r) {
                $.post("/WorkFlow/WorkFlowInst/Delete", { fids: ids.toString(), callback: Math.random() }, function (res) {
                    if (res === "1") {
                        reloadList();
                        $("#list").datagrid("clearSelections");
                    } else {
                        $.messager.alert("提示框", "删除出错");
                    }
                })
            }
        })
    })
    $("#btnDetail").click(function () {
        var rows = $("#list").datagrid("getSelected");
        if (rows === null) {
            return;
        }
     
    //   window.open("/WorkFlow/WorkFlowInst/Detail?id=" + rows.WI_ID, "_self");
        window.open("/WorkFlow/ManualStep/NewIndex?wid=" + rows.WI_ID, "_self");
    })
})