function reloadList() {
    $("#list").datagrid("load", { name: $("#name").val(), begin: $("#BeginTime").val(), end: $("#EndTime").val() });
}
$(function () {
    $("#search").click(function () {
        reloadList();
    });
    $("#list").datagrid({
        pageList: [10, 15, 20, 25, 30, 35],
        width: 680,
        height: "auto",
        sortName: "WD_SHORT_NAME",
        sortOrder: "desc",
        idField: "WD_SHORT_NAME",
        url: "/WorkFlow/WorkFlowDef/InitData",
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
        onClickRow: function (rowIndex, rowData) {
            $('#list').datagrid("unselectRow", rowIndex);
            var isChange;
            if (workflow.id == "") {
                isChange = workflow.nodelist.list.length > 0 || workflow.linelist.list.length > 0;
            } else {
                isChange = workflow.changeCount > 0 || !baseWorkflow.compareWorkflow();
            }
            //if ((workflow.nodelist.list.length > 0 || workflow.linelist.list.length > 0) && workflow.id == "" || workflow.changeCount > 0 || !baseWorkflow.compareWorkflow()) {
            if (isChange) {
                var messager = workflow.id == "" ? "工作区域有新增流程,是否保存并导入" : "工作区域流程有修改,是否保存并导入";
                $.messager.confirm("提示框", messager, function (r) {
                    if (r) {
                        var xml = workflow.getXml();
                        if (xml == undefined) {
                            //                            $.messager.alert("提示框", "保存工作流xml格式错误,请完整工作流");
                            return;
                        }
                        $.post("/Workflow/Designer/AddWorkflow", { workflow: encodeURI(xml) }, function (res) {
                            if (res == "-1") {
                                $.messager.alert("提示框", "该名称的流程已存在，请换名");
                            }
                            if (res == "-2") {
                                $.messager.alert("提示框", "保存失败");
                            }
                            else {
                                workflow.clear();
                                $.get("/Workflow/Designer/GetWorkflowXML", { shortName: rowData.WD_SHORT_NAME, callback: Math.random() }, function (res) {
                                    $("#designerWindow").window("close");
                                    //var testXml = '<root  name="sss" displayname="dddd" priority="0" issave="0" description=""><node><objectindex type="number">0</objectindex><typeindex type="number">0</typeindex><id type="string">446aa15d57e00907ab0512ad6d157857</id><icon type="string">nodeicon1</icon><nodetype type="number">1</nodetype><text type="string">QQ</text><nodetext type="string">开始</nodetext><x type="number">345</x><y type="number">25</y><width type="number">130</width><height type="number">51</height><inputtype type="number">0</inputtype><outputtype type="number">1</outputtype><zindex type="number">101</zindex><name type="string">QQ</name><creatorregname type="string">QQ</creatorregname><parentlist></parentlist><childlist><childid>f36abde1ac243ae19fac32398fdae4e3</childid></childlist></node><node><objectindex type="number">0</objectindex><typeindex type="number">0</typeindex><id type="string">e427e005f969c831fb21bb6c8b5debab</id><icon type="string">nodeicon2</icon><nodetype type="number">2</nodetype><text type="string">RR</text><nodetext type="string">结束</nodetext><x type="number">345</x><y type="number">210</y><width type="number">130</width><height type="number">51</height><inputtype type="number">2</inputtype><outputtype type="number">0</outputtype><zindex type="number">102</zindex><name type="string">RR</name><plugregname type="string">RR</plugregname><parentlist><parentid>8b3e7a104bb28859f5b1cc7b926e1f2f</parentid><parentid>5780bf8e67f534037d0340a6cc567eac</parentid></parentlist><childlist></childlist></node><node><objectindex type="number">0</objectindex><typeindex type="number">0</typeindex><id type="string">f36abde1ac243ae19fac32398fdae4e3</id><icon type="string">nodeicon3</icon><nodetype type="number">21</nodetype><text type="string">WW</text><nodetext type="string">路由</nodetext><x type="number">333</x><y type="number">97</y><width type="number">130</width><height type="number">51</height><inputtype type="number">2</inputtype><outputtype type="number">2</outputtype><zindex type="number">103</zindex><name type="string">WW</name><parentlist><parentid>446aa15d57e00907ab0512ad6d157857</parentid></parentlist><childlist><childid>8b3e7a104bb28859f5b1cc7b926e1f2f</childid><childid>5780bf8e67f534037d0340a6cc567eac</childid></childlist></node><node><objectindex type="number">0</objectindex><typeindex type="number">0</typeindex><id type="string">5780bf8e67f534037d0340a6cc567eac</id><icon type="string">nodeicon-auto</icon><nodetype type="number">22</nodetype><text type="string">TT</text><nodetext type="string">自动</nodetext><x type="number">566</x><y type="number">152</y><width type="number">130</width><height type="number">51</height><inputtype type="number">2</inputtype><outputtype type="number">1</outputtype><zindex type="number">104</zindex><name type="string">TT</name><plugregname type="string">TT</plugregname><parentlist><parentid>f36abde1ac243ae19fac32398fdae4e3</parentid></parentlist><childlist><childid>e427e005f969c831fb21bb6c8b5debab</childid></childlist></node><node><objectindex type="number">0</objectindex><typeindex type="number">0</typeindex><id type="string">8b3e7a104bb28859f5b1cc7b926e1f2f</id><icon type="string">nodeicon5</icon><nodetype type="number">20</nodetype><text type="string">EE</text><nodetext type="string">人工</nodetext><x type="number">219</x><y type="number">171</y><width type="number">130</width><height type="number">51</height><inputtype type="number">2</inputtype><outputtype type="number">1</outputtype><zindex type="number">105</zindex><name type="string">EE</name><actorregname type="string">EE</actorregname><controllactions><controllaction actionname="2" controlname="2" areaname="2" order="2"></controllaction><controllaction actionname="1" controlname="1" areaname="1" order="1"></controllaction></controllactions><process><uioperationconfig plugin="1" buttoncaption="1" displayname="1" name="1"></uioperationconfig><nonuioperationconfigs><nonuioperationconfig needprompt="false" plugin="3" buttoncaption="3" displayname="3" name="3"></nonuioperationconfig><nonuioperationconfig needprompt="false" plugin="2" buttoncaption="2" displayname="2" name="2"></nonuioperationconfig></nonuioperationconfigs></process><parentlist><parentid>f36abde1ac243ae19fac32398fdae4e3</parentid></parentlist><childlist><childid>e427e005f969c831fb21bb6c8b5debab</childid></childlist></node><linelist><line id="446aa15d57e00907ab0512ad6d157857_f36abde1ac243ae19fac32398fdae4e3"></line><line id="f36abde1ac243ae19fac32398fdae4e3_8b3e7a104bb28859f5b1cc7b926e1f2f"></line><line id="f36abde1ac243ae19fac32398fdae4e3_5780bf8e67f534037d0340a6cc567eac"></line><line id="8b3e7a104bb28859f5b1cc7b926e1f2f_e427e005f969c831fb21bb6c8b5debab"></line><line id="5780bf8e67f534037d0340a6cc567eac_e427e005f969c831fb21bb6c8b5debab"></line></linelist></root>';
                                    workflow.parseXml(createXml(res));
                                });
                            }
                        })
                    }
                })
            }
            else {
                workflow.clear();
                $.get("/Workflow/Designer/GetWorkflowXML", { shortName: rowData.WD_SHORT_NAME }, function (res) {
                    $("#designerWindow").window("close");
                    //var testXml = '<root  name="sss" displayname="dddd" priority="0" issave="0" description=""><node><objectindex type="number">0</objectindex><typeindex type="number">0</typeindex><id type="string">446aa15d57e00907ab0512ad6d157857</id><icon type="string">nodeicon1</icon><nodetype type="number">1</nodetype><text type="string">QQ</text><nodetext type="string">开始</nodetext><x type="number">345</x><y type="number">25</y><width type="number">130</width><height type="number">51</height><inputtype type="number">0</inputtype><outputtype type="number">1</outputtype><zindex type="number">101</zindex><name type="string">QQ</name><creatorregname type="string">QQ</creatorregname><parentlist></parentlist><childlist><childid>f36abde1ac243ae19fac32398fdae4e3</childid></childlist></node><node><objectindex type="number">0</objectindex><typeindex type="number">0</typeindex><id type="string">e427e005f969c831fb21bb6c8b5debab</id><icon type="string">nodeicon2</icon><nodetype type="number">2</nodetype><text type="string">RR</text><nodetext type="string">结束</nodetext><x type="number">345</x><y type="number">210</y><width type="number">130</width><height type="number">51</height><inputtype type="number">2</inputtype><outputtype type="number">0</outputtype><zindex type="number">102</zindex><name type="string">RR</name><plugregname type="string">RR</plugregname><parentlist><parentid>8b3e7a104bb28859f5b1cc7b926e1f2f</parentid><parentid>5780bf8e67f534037d0340a6cc567eac</parentid></parentlist><childlist></childlist></node><node><objectindex type="number">0</objectindex><typeindex type="number">0</typeindex><id type="string">f36abde1ac243ae19fac32398fdae4e3</id><icon type="string">nodeicon3</icon><nodetype type="number">21</nodetype><text type="string">WW</text><nodetext type="string">路由</nodetext><x type="number">333</x><y type="number">97</y><width type="number">130</width><height type="number">51</height><inputtype type="number">2</inputtype><outputtype type="number">2</outputtype><zindex type="number">103</zindex><name type="string">WW</name><parentlist><parentid>446aa15d57e00907ab0512ad6d157857</parentid></parentlist><childlist><childid>8b3e7a104bb28859f5b1cc7b926e1f2f</childid><childid>5780bf8e67f534037d0340a6cc567eac</childid></childlist></node><node><objectindex type="number">0</objectindex><typeindex type="number">0</typeindex><id type="string">5780bf8e67f534037d0340a6cc567eac</id><icon type="string">nodeicon-auto</icon><nodetype type="number">22</nodetype><text type="string">TT</text><nodetext type="string">自动</nodetext><x type="number">566</x><y type="number">152</y><width type="number">130</width><height type="number">51</height><inputtype type="number">2</inputtype><outputtype type="number">1</outputtype><zindex type="number">104</zindex><name type="string">TT</name><plugregname type="string">TT</plugregname><parentlist><parentid>f36abde1ac243ae19fac32398fdae4e3</parentid></parentlist><childlist><childid>e427e005f969c831fb21bb6c8b5debab</childid></childlist></node><node><objectindex type="number">0</objectindex><typeindex type="number">0</typeindex><id type="string">8b3e7a104bb28859f5b1cc7b926e1f2f</id><icon type="string">nodeicon5</icon><nodetype type="number">20</nodetype><text type="string">EE</text><nodetext type="string">人工</nodetext><x type="number">219</x><y type="number">171</y><width type="number">130</width><height type="number">51</height><inputtype type="number">2</inputtype><outputtype type="number">1</outputtype><zindex type="number">105</zindex><name type="string">EE</name><actorregname type="string">EE</actorregname><controllactions><controllaction actionname="2" controlname="2" areaname="2" order="2"></controllaction><controllaction actionname="1" controlname="1" areaname="1" order="1"></controllaction></controllactions><process><uioperationconfig plugin="1" buttoncaption="1" displayname="1" name="1"></uioperationconfig><nonuioperationconfigs><nonuioperationconfig needprompt="false" plugin="3" buttoncaption="3" displayname="3" name="3"></nonuioperationconfig><nonuioperationconfig needprompt="false" plugin="2" buttoncaption="2" displayname="2" name="2"></nonuioperationconfig></nonuioperationconfigs></process><parentlist><parentid>f36abde1ac243ae19fac32398fdae4e3</parentid></parentlist><childlist><childid>e427e005f969c831fb21bb6c8b5debab</childid></childlist></node><linelist><line id="446aa15d57e00907ab0512ad6d157857_f36abde1ac243ae19fac32398fdae4e3"></line><line id="f36abde1ac243ae19fac32398fdae4e3_8b3e7a104bb28859f5b1cc7b926e1f2f"></line><line id="f36abde1ac243ae19fac32398fdae4e3_5780bf8e67f534037d0340a6cc567eac"></line><line id="8b3e7a104bb28859f5b1cc7b926e1f2f_e427e005f969c831fb21bb6c8b5debab"></line><line id="5780bf8e67f534037d0340a6cc567eac_e427e005f969c831fb21bb6c8b5debab"></line></linelist></root>';
                    workflow.parseXml(createXml(res));
                });
            }
        },
        onLoadSuccess: function (data) {
            $(".datagrid-view2 tr").css("cursor", "pointer");
        }
    });
})