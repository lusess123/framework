function loadList() {
    $("#list").treegrid({
        title: "我参与的流程",
        nowrap: false,
        width: $(".listBox").width() * 0.97,
        animate: false,
        collapsible: false,
        url: "/WorkFlow/MyParticipation/InitData",
        idField: 'ID',
        treeField: 'Name',
        singleSelect: false,
        fitColumns: true,
        height: 450,
        loadMsg: "数据加载中，请稍后...",
        queryParams: { name: trim($("#Name").val()), begin: $("#BeginTime").val(), end: $("#EndTime").val()},
        frozenColumns: [[
                    { field: 'ck',
                        formatter: function (value, data) {
                            if (data._parentId !== "") {
                                return "<input type=\"checkbox\" listCKMark=\"" + data.ID + "\"/>";
                            } else {
                                return "";
                            }
                        }
                    },
                    { field: "Name", title: "", width: 270 }
				]],
        columns: [[
                    { field: "CurrentStep", title: "当前步骤", width: 150 },
                    { field: "CurrentProcessUserID", title: "当前处理人", width: 150 },
                    { field: "CreateTime", title: "创建时间", width: 180 },
                    { field: "EndTime", title: "结束时间", width: 180 },
                     { field: "ReceiveTime", title: "到达时间", width: 180 },
                    { field: "LastName", title: "上一步骤", width: 180 },
                    { field: "LastManualName", title: "上一人工步骤", width: 180 }
                ]],
        onClickRow: function (row) {
            $("#list").treegrid("unselect", row.ID);
        }
    });
}
$(function () {
    $("#search").click(function () {
        loadList();
    })
    loadList();
    $("#btnDetail").click(function () {
        //默认选择第一个
        var row = $("input:checked[listCKMark]").filter(":first");
        if (!(row.length > 0)) {
            $.messager.alert("提示框", "未选择任何项");
            return;
        }
        location.href = "/WorkFlow/MyParticipation/Detail?id=" + row.attr("listCKMark");

        //        $.post("/WorkFlow/ManualStep/IsSelfProcess?wid=" + row.attr("listCKMark"), function (data) {
        //            if (data == "true") {
        //                window.open("/WorkFlow/ManualStep/NewIndex?wid=" + row.attr("listCKMark"), "_self");
        //            }
        //            else if (data == "false") {
        //                window.open("/WorkFlow/ManualStep/ReadOnlyIndex?wid=" + row.attr("listCKMark"), "_self");
        //            }
        //        })
        //window.open("/WorkFlow/WorkFlowInst/Detail?id=" + row.attr("listCKMark"), "_self");

    })
})