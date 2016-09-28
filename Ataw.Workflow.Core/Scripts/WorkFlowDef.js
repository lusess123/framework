function reloadList() {
    $("#list").datagrid("load", { shortName: trim($("#shortName").val()), name: trim($("#name").val()), begin: $("#BeginTime").val(), end: $("#EndTime").val() });
}
$(function () {
    $("#search").click(function () {
        reloadList();
    });
    $("#list").datagrid({
        pageList: [15],
        title: "工作流模式列表",
        height: 450,
        width: $(".listBox").width()*0.97,
        fitColumns: true,
        collapsible: true,
        sortName: "WD_CREATE_DATE",
        sortOrder: "desc",
        idField: "WD_SHORT_NAME",
        loadMsg: "数据加载中，请稍后...",
        url: "/WorkFlow/WorkFlowDef/InitData",
        frozenColumns: [[
            { field: "ck", checkbox: true }
        ]],
        columns: [[
            { field: "WD_SHORT_NAME", title: "标识名", width: 100, sortable: true },
            { field: "WD_NAME", title: "名称", width: 180, sortable: true },
            { field: "WD_IS_USED", title: "状态", width: 60, sortable: true,
                formatter: function (value) {
                    if (value === 1) {
                        return "启用中";
                    } else if (value === 0) {
                        return "禁用中"
                    } else {
                        return "";
                    }
                }
            },
            { field: "WD_VERSION", title: "版本号", width: 80 },
            { field: "WD_CREATE_DATE", title: "创建时间", width: 180, sortable: true }
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
        var ids = getSelections()
        if (ids == false) {
            $.messager.alert("提示框", "未选择任何项")
            return;
        }
        $.messager.confirm("提示框", "确定删除吗？", function (r) {
            if (r) {
                $.post("/WorkFlow/WorkFlowDef/Delete", { fids: ids.toString(), callback: Math.random() }, function (res) {
                    if (res === "1") {
                        reloadList();
                        $("#list").datagrid("clearSelections");
                    } else {
                        $.messager.alert("提示框", "删除出错");
                    }
                })
            }
        })
    });
    $("#btnDetail").click(function () {
        var rows = $("#list").datagrid("getSelected");
        if (rows === null) {
            return;
        }
        window.open("/WorkFlow/Designer/Index?shortName=" + rows.WD_SHORT_NAME, "_self");
        //window.open("/WorkFlow/WorkFlowDef/Detail?shortName=" + rows.WD_SHORT_NAME, "_self");
    })
    $("#btnEnable").click(function () {
        var ids = getSelections()
        if (ids == false) {
            $.messager.alert("提示框", "未选择任何项")
            return;
        }
        $.get("/WorkFlow/WorkFlowDef/EnableOrDisable", { shortNames: ids.toString(), enable: 1, callback: Math.random() }, function (res) {
            if (res) {
                reloadList();
            } else {
                $.messager.alert("提示框", "操作失败");
            }
        })
    })
    $("#btnDisable").click(function () {
        var ids = getSelections()
        if (ids == false) {
            $.messager.alert("提示框", "未选择任何项")
            return;
        }
        $.get("/WorkFlow/WorkFlowDef/EnableOrDisable", { shortNames: ids.toString(), enable: 0, callback: Math.random() }, function (res) {
            if (res) {
                reloadList();
            } else {
                $.messager.alert("提示框", "操作失败");
            }
        })
    })
})
function getSelections() {
    var rows = $("#list").datagrid("getSelections");
    var ids = [];
    for (var i = 0; i < rows.length; i++) {
        ids.push(rows[i].WD_SHORT_NAME);
    }
    return ids;
}