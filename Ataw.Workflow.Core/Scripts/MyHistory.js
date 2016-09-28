function loadList() {

    var flowType = $("#hdnflowType").val();
    var title = "";
    if (flowType == "Launch") {
        flowType = "2";
        title = "我发起的历史流程";
    }
    else {
        flowType = "1";
        title = "我参与的历史流程";
    }
  
    $("#list").treegrid({
        title: title,
        nowrap: false,
        animate: true,
        width: $(".listBox").width() * 0.97,
        height: 450,
        collapsible: true,
        url: "/WorkFlow/MyHistory/InitData",
        idField: 'ID',
        treeField: 'Name',
        singleSelect: false,
        loadMsg: "数据加载中，请稍后...",
        queryParams: { name: trim($("#Name").val()), begin: $("#BeginTime").val(), end: $("#EndTime").val(), flowType: flowType },
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
                    { field: "EndStatus", title: "结束状态", width: 150,
                        formatter: function (value, data) {
                            switch (value) {
                                case 1:
                                    return "正常结束";
                                case 2:
                                    return "移除";
                                case 3:
                                    return "终止";
                                case 4:
                                    return "异常";
                                case 5:
                                    return "超出重试次数";
                                case 6:
                                    return "回退到开始";
                                default:
                                    return "";

                            }
                        }
                    },
                    { field: "EndUser", title: "结束人", width: 150 },
                    { field: "CreateTime", title: "创建时间", width: 180 },
                    { field: "EndTime", title: "结束时间", width: 180 }
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
    autoHighet();
    $("#btnDetail").click(function () {
        //默认选择第一个
        var row = $("input:checked[listCKMark]").filter(":first");
        if (!(row.length > 0)) {
            $.messager.alert("提示框", "未选择任何项");
            return;
        }
        location.href = "/WorkFlow/MyHistory/Detail?id=" + row.attr("listCKMark");
       // window.open("/WorkFlow/MyHistory/Detail?id=" + row.attr("listCKMark"), "_self");
        // window.open("/WorkFlow/ManualStep/ReadOnlyIndex?wid=" + row.attr("listCKMark"), "_self");
    })
})