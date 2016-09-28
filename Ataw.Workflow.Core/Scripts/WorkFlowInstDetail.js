$(function () {
    $("#list").datagrid({
      // title: "步骤列表",
        height: "auto",
        width:$(".listBox")*0.98,
        fitColumns: true,
        collapsible: true,
        sortName: "SI_INDEX",
        sortOrder: "desc",
        idField: "SI_ID",
        loadMsg: "数据加载中，请稍后...",
        url: "/WorkFlow/WorkFlowInst/GetSteps?instId=" + $("#list").attr("instId"),
        columns: [[
                    { field: "SI_LAST_STEP_NAME", title: "源步骤", width: 100, sortable: true },
                    { field: "SI_CURRENT_STEP_NAME", title: "发送步骤", width: 180, sortable: true },
                    { field: "SI_STEP_TYPE", title: "发送步骤类型", width: 180, sortable: true,
                        formatter: function (value) {
                            switch (value) {
                                case 0:
                                    return "未定义";
                                case 1:
                                    return "开始";
                                case 2:
                                    return "结束";
                                case 3:
                                    return "人工";
                                case 4:
                                    return "路由";
                                case 5:
                                    return "自动";
                                case 6:
                                    return "聚合";
                                default:
                                    return "";
                            }
                        }
                    },
                    { field: "SI_RECEIVE_ID", title: "接收人", width: 120, sortable: true },
                    { field: "SI_RECEIVE_DATE", title: "接收时间", width: 100, sortable: true },
                    { field: "SI_PROCESS_ID", title: "处理人", width: 100, sortable: true },
                    { field: "SI_PROCESS_DATE", title: "处理时间", width: 100, sortable: true },
                    { field: "SI_FLOW_TYPE", title: "流转方式", width: 80, sortable: true,
                        formatter: function (value) {
                            if (value === 0) {
                                return "流转";
                            } else if (value === 1) {
                                return "回退";
                            } else {
                                return "";
                            }
                        }
                    }

        ]],
        onSortColumn: function () {
            $("#list").datagrid("reload");
        },
        onClickRow: function (rowIndex) {
            $('#list').datagrid("unselectRow", rowIndex);
        }
    });

    $("#approveList").datagrid({
        //title: "审核历史",
        height: "auto",
        width:$(".listBox")*0.98,
        fitColumns: true,
        collapsible: true,
        sortName: "AH_CREATE_DATE",
        sortOrder: "desc",
        idField: "AH_ID",
        url: "/WorkFlow/WorkFlowInst/GetApproveHistory?instId=" + $("#approveList").attr("instId"),
        columns: [[
            { field: "AH_APPROVE", title: "操作", width: 100, sortable: true,
                formatter: function (value) {
                    if (value === 0) {
                        return "拒绝";
                    } else if (value === 1) {
                        return "同意";
                    } else {
                        return "";
                    }
                }
            },
            { field: "AH_NOTE", title: "流转意见", width: 180 },
            { field: "AH_STEP_DISPLAY_NAME", title: "流转环节", width: 180, sortable: true },
            { field: "AH_OPERATOR", title: "审核人", width: 180, sortable: true },
            { field: "AH_CREATE_DATE", title: "审核时间", width: 100, sortable: true }

        ]],
        onSortColumn: function () {
            $("#approveList").datagrid("reload");
        },
        onClickRow: function (rowIndex) {
            $('#approveList').datagrid("unselectRow", rowIndex);
        }
    });
})