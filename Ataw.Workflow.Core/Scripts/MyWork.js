function loadList() {
    $("#list").treegrid({
        title: '我的工作实例列表',
        nowrap: false,
        width: $(".listBox").width()*0.97,
        animate: true,
        collapsible: true,
        url: "/WorkFlow/MyWork/InitData",
        idField: 'ID',
        treeField: 'Name',
        singleSelect: false,
        height: 450,
        loadMsg: "数据加载中，请稍后...",
        queryParams: { name: trim($("#Name").val()), begin: $("#BeginTime").val(), end: $("#EndTime").val() },
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
                    { field: "CreateTime", title: "创建时间", width: 180 },
                    { field: "ReceiveTime", title: "到达时间", width: 180 },
                    { field: "LastName", title: "上一步骤", width: 180 },
                    { field: "LastManualName", title: "上一人工步骤", width: 180 }
                ]],
        onClickRow: function (row) {
            $("#list").treegrid("unselect", row.ID);
        }
//        onLoadSuccess: function () {
//            var node = $('#list').treegrid('getSelected');
//            if (node) {
//                $('#list').treegrid('collapseAll', node.code);
//            } else {
//                $('#list').treegrid('collapseAll');
//            }
//        }
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
        //window.open("/WorkFlow/WorkFlowInst/Detail?id=" + row.attr("listCKMark"), "_self");
        window.open("/WorkFlow/MyWork/ProcessDetail?id=" + row.attr("listCKMark"), "_self");
    })
      
})