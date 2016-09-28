window.onerror = function (msg, url, line) {
    var str = "You have found an error as below: \n\n";
    str += "Err: " + msg + " on line: " + line;
    alert(str);
    return true;
}
$.ajaxSetup({ cache: false });
var result = [];
$(document).ready(function () {
    workflow.init(xmlobject);
    $(".attributecontentleft div").click(function () {
        $(".attributecontentleft div").removeClass("attractive");
        $(".attributecontentright div").hide();
        var id = $(this).attr("cid");
        $(this).addClass("attractive");
        $("#" + id).show();

    });
});

function setgrid(obj) { if ($("#paintarea").attr("grid") == 1) { /*$(obj).removeClass("menuitemactive");*/$("#paintarea").removeClass("grid"); $("#paintarea").attr("grid", "0"); } else { /*$(obj).addClass("menuitemactive");*/$("#paintarea").addClass("grid"); $("#paintarea").attr("grid", "1"); } }
function save() {
    var xml = workflow.getXml();
    //    $.ajax({
    //        url: "/Designer/AddWorkflow?workflow=" + encodeURI(xml),
    //        type: 'post'
    //    });
    if (xml) {
        $.post("/Workflow/Designer/AddWorkflow", { workflow: encodeURI(xml) }, function (res) {
            if (res == "-1") {
                $.messager.alert("提示框", "该名称的流程已存在，请换名");
            }
            if (res == "-2") {
                $.messager.alert("提示框", "保存失败");
            }
            else {
                $.messager.alert("提示框", "保存成功");
                workflow.id = res;
                workflow.changeCount = 0;
                baseWorkflow.id = workflow.id;
                baseWorkflow.name = workflow.name;
                baseWorkflow.priority = workflow.priority;
                baseWorkflow.issave = workflow.issave;
                baseWorkflow.displayname = workflow.displayname;
                baseWorkflow.description = workflow.description;
                baseWorkflow.contentchoice = workflow.contentchoice;
                baseWorkflow.manualpagexml = workflow.manualpagexml;
                if (workflow.controllactions != null) {
                    baseWorkflow.controllactions = [];
                    clone.deepCopy(baseWorkflow.controllactions, workflow.controllactions);
                } else {
                    baseWorkflow.controllactions = null;
                }
                //                $.extend(true, baseWorkflow.nodelist.list, nodelist);
                //                $.extend(true, baseWorkflow.linelist.list, linelist);
                baseWorkflow.nodelist.list = [];
                baseWorkflow.linelist.list = [];
                clone.deepCopy(baseWorkflow.nodelist.list, workflow.nodelist.list);
                clone.deepCopy(baseWorkflow.linelist.list, workflow.linelist.list);
            }
        })
    }
    // if (xml) console.log(xmlToJson(createXml(xml)));
    //SaveWorkflow();
}
function fabu() {
    alert(workflow.nodelist.getByIndex(0));
};
function daoru() {
    $("#designerWindow").window("open");
    //var xml = '<root  name="sss" displayname="dddd" priority="0" issave="0" description=""><node><objectindex type="number">0</objectindex><typeindex type="number">0</typeindex><id type="string">446aa15d57e00907ab0512ad6d157857</id><icon type="string">nodeicon1</icon><nodetype type="number">1</nodetype><text type="string">QQ</text><nodetext type="string">开始</nodetext><x type="number">345</x><y type="number">25</y><width type="number">130</width><height type="number">51</height><inputtype type="number">0</inputtype><outputtype type="number">1</outputtype><zindex type="number">101</zindex><name type="string">QQ</name><creatorregname type="string">QQ</creatorregname><parentlist></parentlist><childlist><childid>f36abde1ac243ae19fac32398fdae4e3</childid></childlist></node><node><objectindex type="number">0</objectindex><typeindex type="number">0</typeindex><id type="string">e427e005f969c831fb21bb6c8b5debab</id><icon type="string">nodeicon2</icon><nodetype type="number">2</nodetype><text type="string">RR</text><nodetext type="string">结束</nodetext><x type="number">345</x><y type="number">210</y><width type="number">130</width><height type="number">51</height><inputtype type="number">2</inputtype><outputtype type="number">0</outputtype><zindex type="number">102</zindex><name type="string">RR</name><plugregname type="string">RR</plugregname><parentlist><parentid>8b3e7a104bb28859f5b1cc7b926e1f2f</parentid><parentid>5780bf8e67f534037d0340a6cc567eac</parentid></parentlist><childlist></childlist></node><node><objectindex type="number">0</objectindex><typeindex type="number">0</typeindex><id type="string">f36abde1ac243ae19fac32398fdae4e3</id><icon type="string">nodeicon3</icon><nodetype type="number">21</nodetype><text type="string">WW</text><nodetext type="string">路由</nodetext><x type="number">333</x><y type="number">97</y><width type="number">130</width><height type="number">51</height><inputtype type="number">2</inputtype><outputtype type="number">2</outputtype><zindex type="number">103</zindex><name type="string">WW</name><parentlist><parentid>446aa15d57e00907ab0512ad6d157857</parentid></parentlist><childlist><childid>8b3e7a104bb28859f5b1cc7b926e1f2f</childid><childid>5780bf8e67f534037d0340a6cc567eac</childid></childlist></node><node><objectindex type="number">0</objectindex><typeindex type="number">0</typeindex><id type="string">5780bf8e67f534037d0340a6cc567eac</id><icon type="string">nodeicon-auto</icon><nodetype type="number">22</nodetype><text type="string">TT</text><nodetext type="string">自动</nodetext><x type="number">566</x><y type="number">152</y><width type="number">130</width><height type="number">51</height><inputtype type="number">2</inputtype><outputtype type="number">1</outputtype><zindex type="number">104</zindex><name type="string">TT</name><plugregname type="string">TT</plugregname><parentlist><parentid>f36abde1ac243ae19fac32398fdae4e3</parentid></parentlist><childlist><childid>e427e005f969c831fb21bb6c8b5debab</childid></childlist></node><node><objectindex type="number">0</objectindex><typeindex type="number">0</typeindex><id type="string">8b3e7a104bb28859f5b1cc7b926e1f2f</id><icon type="string">nodeicon5</icon><nodetype type="number">20</nodetype><text type="string">EE</text><nodetext type="string">人工</nodetext><x type="number">219</x><y type="number">171</y><width type="number">130</width><height type="number">51</height><inputtype type="number">2</inputtype><outputtype type="number">1</outputtype><zindex type="number">105</zindex><name type="string">EE</name><actorregname type="string">EE</actorregname><controllactions><controllaction actionname="2" controlname="2" areaname="2" order="2"></controllaction><controllaction actionname="1" controlname="1" areaname="1" order="1"></controllaction></controllactions><process><uioperationconfig plugin="1" buttoncaption="1" displayname="1" name="1"></uioperationconfig><nonuioperationconfigs><nonuioperationconfig needprompt="false" plugin="3" buttoncaption="3" displayname="3" name="3"></nonuioperationconfig><nonuioperationconfig needprompt="false" plugin="2" buttoncaption="2" displayname="2" name="2"></nonuioperationconfig></nonuioperationconfigs></process><parentlist><parentid>f36abde1ac243ae19fac32398fdae4e3</parentid></parentlist><childlist><childid>e427e005f969c831fb21bb6c8b5debab</childid></childlist></node><linelist><line id="446aa15d57e00907ab0512ad6d157857_f36abde1ac243ae19fac32398fdae4e3"></line><line id="f36abde1ac243ae19fac32398fdae4e3_8b3e7a104bb28859f5b1cc7b926e1f2f"></line><line id="f36abde1ac243ae19fac32398fdae4e3_5780bf8e67f534037d0340a6cc567eac"></line><line id="8b3e7a104bb28859f5b1cc7b926e1f2f_e427e005f969c831fb21bb6c8b5debab"></line><line id="5780bf8e67f534037d0340a6cc567eac_e427e005f969c831fb21bb6c8b5debab"></line></linelist></root>';
    //var xml = '<root><node><id type="string">6d79a3fe2693c6a3fd69a0da63086801</id><icon type="string">nodeicon1</icon><nodetype type="number">1</nodetype><text type="string">新建节点</text><nodetext type="string">开始</nodetext><x type="number">109</x><y type="number">139</y><width type="number">130</width><height type="number">51</height><inputtype type="number">0</inputtype><outputtype type="number">1</outputtype><zindex type="number">101</zindex><parentlist></parentlist><childlist><childid>a4e41f2c5731441bbd31eb23794dc112</childid></childlist></node><node><id type="string">a4e41f2c5731441bbd31eb23794dc112</id><icon type="string">nodeicon2</icon><nodetype type="number">2</nodetype><text type="string">新建节点</text><nodetext type="string">结束</nodetext><x type="number">543</x><y type="number">200</y><width type="number">130</width><height type="number">51</height><inputtype type="number">2</inputtype><outputtype type="number">0</outputtype><zindex type="number">102</zindex><parentlist><parentid>6d79a3fe2693c6a3fd69a0da63086801</parentid></parentlist><childlist></childlist></node><linelist><line id="6d79a3fe2693c6a3fd69a0da63086801_a4e41f2c5731441bbd31eb23794dc112">dddd</line></linelist></root>';
    //workflow.parseXml(createXml(xml));
    //    $.ajax({
    //        url: "test.xml",
    //        success: function (xmlDoc) {
    //            workflow.parseXml(xmlDoc)
    //        },
    //        dataType: "xml"
    //    });
}
function rebuild() {
    var isChange;
    if (workflow.id == "") {
        isChange = workflow.nodelist.list.length > 0 || workflow.linelist.list.length > 0;
    } else {
        isChange = workflow.changeCount > 0 || !baseWorkflow.compareWorkflow();
    }
    //if ((workflow.nodelist.list.length > 0 || workflow.linelist.list.length > 0) && workflow.id == "" || workflow.changeCount > 0 || !baseWorkflow.compareWorkflow()) {
    if (isChange) {
        var messager = workflow.id == "" ? "工作区域有新增流程,是否保存并重建" : "工作区域流程有修改,是否保存并重建";
        $.messager.confirm("提示框", messager, function (r) {
            if (r) {
                var xml = workflow.getXml();
                if (xml == undefined) {
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
                        window.open("/Workflow/Designer/index", "_self");
                    }
                })
            }
        })
    }
    else {
        window.open("/Workflow/Designer/index", "_self");
    }
}
function shanchu() { workflow.delnode(); }
//检测是否修改字段
var baseWorkflow = {
    id: "",
    name: "",
    displayname: "",
    priority: "",
    issave: "1",
    description: "",
    manualpagexml: "",
    controllactions: null,
    contentchoice: "1",
    nodelist: {
        get: function (id) {
            for (var i = 0; i < this.list.length; i++) {
                var node = this.list[i];
                if (node.id == id) { return node; }
            }
            return null
        },
        list: []
    },
    linelist: {
        list: [],
        get: function (id) {
            for (var i = 0; i < this.list.length; i++) {
                var node = this.list[i];
                if (node.id == id)
                { return node; }
            }
            return null
        }
    },
    //比较某个节点或线条是否删除或增添
    compareWorkflow: function () {
        var baseNodeLength = this.nodelist.list.length, workflowNodeLength = workflow.nodelist.list.length,
        baseLineLength = this.linelist.list.length, workflowLineLength = workflow.linelist.list.length;
        if (baseNodeLength != workflowNodeLength) { return false; }
        if (baseLineLength != workflowLineLength) { return false; }
        for (var j = 0; j < baseNodeLength; j++) {
            for (var k = 0; k < workflowNodeLength; k++) {
                if (this.nodelist.list[j].id == workflow.nodelist.list[k].id) {
                    break;
                }
            }
            //若baseWorkflow.nodelist中某个节点在workflow.nodelist中不存在，返回false
            if (k > workflowNodeLength - 1) {
                return false;
            }
        }
        //因为线条id是由两个节点id组合得到，所以不能判断id，而要判断整个属性
        for (var j = 0; j < baseLineLength; j++) {
            var workflowLine = workflow.linelist.get(this.linelist.list[j].id);
            if (workflowLine != null) {
                if (workflowLine.text !== this.linelist.list[j].text) { return false; }
                if (workflowLine.name !== this.linelist.list[j].name) { return false; }
                if (workflowLine.plugname !== this.linelist.list[j].plugname) { return false; }
            } else {
                return false;
            }
        }
        return true;
    }
};
var workflow = {
    changeCount: 0, //判断是否修改 0：未修改，!0：修改
    id: "",
    name: "",
    displayname: "",
    priority: "0",
    issave: "1",
    description: "",
    manualpagexml: "",
    controllactions: null,
    contentchoice: "1",
    init: function (xmlobject) {
        this.xmlobject = xmlobject;
        this.xmlobject.nodelist = this.nodelist;
        this.xmlobject.linelist = this.linelist;
        this.resize();
        var thisobj = this;
        $(window).resize(function () { thisobj.resize(); });
        this.config.browser = $.browser;
        this.paintpanelclick(); //画布点击事件
        this.changetool(); //更换工具
        this.settool(0); //设置默认工具
        this.config.demonode = $("#demonode");
        $("#paintarea").append("<div class='resizediv' id='resizel'></div>");
        $("#paintarea").append("<div class='resizediv' id='resizet'></div>");
        $("#paintarea").append("<div class='resizediv' id='resizeb'></div>");
        $("#paintarea").append("<div class='resizediv' id='resizer'></div>");
        $("#paintarea").append("<div class='resizediv' id='resizelt'></div>");
        $("#paintarea").append("<div class='resizediv' id='resizelb'></div>");
        $("#paintarea").append("<div class='resizediv' id='resizert'></div>");
        $("#paintarea").append("<div class='resizediv' id='resizerb'></div>");

        $("#paintarea").append("<div class='resizediv linediv' id='linestart'></div>");
        $("#paintarea").append("<div class='resizediv linediv' id='lineend'></div>");
        if (this.config.browser.msie) {
            $("#paintarea").append('<v:line id="drawbaseline" class="vline" from="0,0" to="150,150" ></v:line>'); //添加ie画线
        }
        else {
            $("#paintarea").append('<div id="drawbaseline" style="display:none" class="ffline"><a></a><div></div></div>'); //添非ie画线
        }
        var wflow = this;
        AtawWfConfig_Fun();
        $("#paintarea").click(function (e) {
            wflow.hidemovediv();
        });

        $(document).mouseup(function (e) {
            $("#drawbaseline").hide();
            if (wflow.config.moveobj.ismove) {
                var obj = wflow.config.moveobj.obj;
                wflow.config.moveobj.ismove = false;
                wflow.resetdeawline();
            }
            return false;
        });


        $(document).mousemove(function (e) {
            if (!wflow.config.moveobj.ismove) { return }
            var mousepos = wflow.tool.mousePosition(e);
            if (wflow.config.moveobj.tag == 1) {
                mousepos.x = mousepos.x + $("#paintarea").scrollLeft() - wflow.config.paintpos.left;
                mousepos.y = mousepos.y + $("#paintarea").scrollTop() - wflow.config.paintpos.top;
            }
            wflow.config.moveobj.pos = { x: mousepos.x, y: mousepos.y };
            if (wflow.config.moveobj.tag == 1) {
                wflow.config.moveobj.obj.css("left", mousepos.x - wflow.config.moveobj.offsetpos.x + "px");
                wflow.config.moveobj.obj.css("top", mousepos.y - wflow.config.moveobj.offsetpos.y + "px");
                wflow.showmovediv(wflow.config.moveobj.obj);
                wflow.resetlineposition(wflow.config.moveobj.obj);
            }
        });

        document.getElementById("paintarea").onselectstart = new Function('event.returnValue=false;');

        $("#paintarea").mousemove(function (e) {
            if (wflow.config.tooltype.type == "line" && wflow.config.drawline.drawtag && wflow.config.browser.msie) {
                var mousepos = wflow.tool.mouseOnPanelPosition(e);
                if (wflow.config.drawline.from.x < mousepos.x) { mousepos.x = mousepos.x - 2; } else { mousepos.x = mousepos.x + 2; }
                if (wflow.config.drawline.from.y < mousepos.y) { mousepos.y = mousepos.y - 2; } else { mousepos.y = mousepos.y + 2; }
                $("#drawbaseline").attr("to", mousepos.x + "," + (mousepos.y));
                return false;
            }
            else if (wflow.config.tooltype.type == "line" && wflow.config.drawline.drawtag) {
                var mousepos = wflow.tool.mouseOnPanelPosition(e);
                if (wflow.config.drawline.from.x < mousepos.x) { mousepos.x = mousepos.x - 2; } else { mousepos.x = mousepos.x + 2; }
                if (wflow.config.drawline.from.y < mousepos.y) { mousepos.y = mousepos.y - 2; } else { mousepos.y = mousepos.y + 2; }
                wflow.setNoIElinePos($("#drawbaseline"), wflow.config.drawline.from, mousepos);
                return false;
            }
            //wflow.config.browser.msie
        })


        $(document).keyup(function (e) {
            e = e || window.event;
            if (e.keyCode == 46) {
                if (wflow.config.nowselect != null) {
                    wflow.delnode();
                }
            }
        });
    },
    resize: function () {
        var ww = $(window).width();
        var wh = $(window).height();
        var woffset = 4;
        var hoffset = 4;
        //leftmenu  dragdiv  paintarea
        $("#leftmenu").css("left", woffset + "px");
        woffset = woffset + $("#leftmenu").width();
        $("#dragdiv").css("left", woffset + "px");
        woffset = woffset + 5;
        $("#paintarea").css("left", woffset + "px");
        $("#paintarea").width(ww - woffset - 5);

        $(".setheight").height(wh - $("#attribute").height() - 40);
        hoffset = $("#paintarea").height() + 35;
        $("#dragy").css("top", hoffset + "px");
        hoffset = hoffset + 5;
        $("#attribute").css("top", hoffset + "px");
        $("#attribute").width(ww - 8);
        //$("#attribute").height(wh-hoffset-4);
        $("#attributecontentright").width($("#attribute").width() - 170);

        this.config.paintpos = $("#paintarea").offset(); //设置画布位置
    },
    clear: function () {
        this.changeCount = 0;
        this.id = "";
        this.name = "";
        this.displayname = "";
        this.priority = "0";
        this.issave = "1";
        this.description = "";
        this.config.moveobj.obj = null;
        this.config.nowselect = null;
        this.config.moveobj.tag = 0;
        this.config.moveobj.ismove = false;
        this.hidemovediv();
        $("#paintarea>.node").remove();
        $("#paintarea>.newline").remove();
        $("#paintarea>.ffline").remove();
        $("#attribute").html("");
        this.nodelist.list = [];
        this.linelist.list = [];
    },
    //节点列表
    nodelist: {
        list: new Array(),
        add: function (node) { this.list[this.list.length] = node; },
        remove: function (id) { for (var i = 0; i < this.list.length; i++) { var node = this.list[i]; if (node.id == id) { this.list.splice(i, 1) } } },
        size: function () { return this.list.length },
        get: function (id) { for (var i = 0; i < this.list.length; i++) { var node = this.list[i]; if (node.id == id) { return node; } } return null },
        getByElement: function (eleobj) { eleobj = workflow.tool.tojQuery(eleobj); var id = eleobj.attr("id"); return this.get(id); },
        getByIndex: function (index) { if (this.list.length > index && index >= 0) { return this.list[index] } return null }
    },
    //连接线列表
    linelist: {
        list: new Array(),
        add: function (node) { this.list[this.list.length] = node; },
        remove: function (id) { for (var i = 0; i < this.list.length; i++) { var node = this.list[i]; if (node.id == id) { this.list.splice(i, 1) } } },
        size: function () { return this.list.length },
        get: function (id) { for (var i = 0; i < this.list.length; i++) { var node = this.list[i]; if (node.id == id) { return node; } } return null },
        getByElement: function (eleobj) { eleobj = workflow.tool.tojQuery(eleobj); var id = eleobj.attr("id"); return this.get(id); },
        getByIndex: function (index) { if (this.list.length > index && index >= 0) { return this.list[index] } return null }
    },
    //节点对象
    nodeobject: {
        basenode: function () {
            //自定义
            this.ObjectIndex = 0;
            this.TypeIndex = 0;
            this.id = "";
            this.icon = "";
            this.nodetype = 1;
            this.text = "新建节点";
            this.nodetext = "";
            this.x = 0;
            this.y = 0;
            this.width = 130;
            this.height = 51;
            this.inputlist = new Array();
            this.outputlist = new Array();
            this.inputtype = 0; /*输入节点数量 0：无输入 1：1输入 2：多输入*/
            this.outputtype = 1;
            this.zindex = 100;
            this.name = "";
            /*删除输入节点*/
            this.removeInput = function (id) { for (var i = 0; i < this.inputlist.length; i++) { if (this.inputlist[i] == id) { this.inputlist.splice(i, 1); return; } } },
            /*删除输出节点*/
			this.removeOutput = function (id) { for (var i = 0; i < this.outputlist.length; i++) { if (this.outputlist[i] == id) { this.outputlist.splice(i, 1); return; } } },
            /*删除所有线*/
			this.removeAllLine = function () {
			    for (var i = 0; i < this.inputlist.length; i++) {
			        var lineid = this.inputlist[i] + "_" + this.id;
			        DelLine_Fun(lineid); $("#" + lineid).remove(); var parobj = workflow.nodelist.get(this.inputlist[i]); parobj.removeOutput(this.id); workflow.linelist.remove(lineid);
			    } for (var i = 0; i < this.outputlist.length; i++) { var lineid = this.id + "_" + this.outputlist[i]; DelLine_Fun(lineid); $("#" + lineid).remove(); var subobj = workflow.nodelist.get(this.outputlist[i]); subobj.removeInput(this.id); workflow.linelist.remove(lineid); }
			}
        },
        1: function () {
            this.icon = "nodeicon1";
            this.nodetype = 1;
            this.nodetext = "开始";
            this.inputtype = 0; /*输入节点数量 0：无输入 1：1输入 2：多输入*/
            this.outputtype = 1;
            this.creatorregname = "";
        },
        2: function () {
            this.icon = "nodeicon2";
            this.nodetype = 2;
            this.nodetext = "结束";
            this.inputtype = 2; /*输入节点数量 0：无输入 1：1输入 2：多输入*/
            this.outputtype = 0;
            this.plugregname = "";
        },
        3: function () {
            this.icon = "nodeicon3";
            this.nodetype = 3;
            this.nodetext = "分支";
            this.inputtype = 1; /*输入节点数量 0：无输入 1：1输入 2：多输入*/
            this.outputtype = 2;
        },
        4: function () {
            this.icon = "nodeicon4";
            this.nodetype = 4;
            this.nodetext = "合并";
            this.inputtype = 2; /*输入节点数量 0：无输入 1：1输入 2：多输入*/
            this.outputtype = 1;
        },
        5: function () {
            this.icon = "nodeicon5";
            this.nodetype = 5;
            this.nodetext = "任务";
            this.inputtype = 1; /*输入节点数量 0：无输入 1：1输入 2：多输入*/
            this.outputtype = 1;
        },
        //-----------------自定义节点
        20: function () {
            this.icon = "nodeicon5";
            this.nodetype = 20;
            this.nodetext = "人工";
            this.inputtype = 2; /*输入节点数量 0：无输入 1：1输入 2：多输入*/
            this.outputtype = 1;
            this.actorregname = "";
            this.controllactions = null;
            this.process = null;
            this.manualpagexml = "";
            this.contentchoice = 0
        },
        21: function () {
            this.icon = "nodeicon3";
            this.nodetype = 21;
            this.nodetext = "路由";
            this.inputtype = 2; /*输入节点数量 0：无输入 1：1输入 2：多输入*/
            this.outputtype = 2;
        },
        22: function () {
            this.icon = "nodeicon-auto";
            this.nodetype = 22;
            this.nodetext = "自动";
            this.inputtype = 2; /*输入节点数量 0：无输入 1：1输入 2：多输入*/
            this.outputtype = 1;
            this.plugregname = ""
        },
        100: function () {
            this.id = "";
            this.nodetype = 100;
            this.text = "";
            this.zindex = 100;
            this.name = "";
            this.plugname = "";
        },
        getNode: function (key) {
            var node = new this.basenode();
            var snode = new this[key]();
            for (var k in snode) {
                node[k] = snode[k];
            }
            return node;
        }
    },
    config: {//变量
        paintpos: { left: 0, top: 0 }, //画布位置
        browser: {}, //浏览器信息
        tooltype: 0, //工具类型 0：选择
        demonode: null, //元素样例
        zindex: 100, //z-index
        moveobj: { obj: null, ismove: false, pos: { x: 0, y: 0 }, offsetpos: { x: 0, y: 0 }, tag: 0 }, //当前移动对象
        drawline: { start: null, end: null, drawtag: false, from: { x: 0, y: 0} },
        nowselect: null,
        a: 0
    },
    tool: {//工具
        //获得event对象
        getevent: function (e) { return e || window.event; },
        //鼠标位置
        mousePosition: function (ev) { ev = this.getevent(ev); if (ev.pageX || ev.pageY) { return { x: ev.pageX, y: ev.pageY }; } return { x: ev.clientX + document.body.scrollLeft - document.body.clientLeft, y: ev.clientY + document.body.scrollTop - document.body.clientTop} },
        //获取事件源
        getResource: function (event) { event = this.getevent(event); var obj = event.srcElement ? event.srcElement : event.target; return obj; },
        //获取鼠标在画布上的位置
        mouseOnPanelPosition: function (e) { var sl = $("#paintarea").scrollLeft(); var st = $("#paintarea").scrollTop(); var mousepos = workflow.tool.mousePosition(e); var panelpos = workflow.config.paintpos; mousepos.x = mousepos.x - panelpos.left + sl; mousepos.y = mousepos.y - panelpos.top + st; return mousepos; },
        //获取新随机id
        newname: function () { var myDate = new Date(); var tm = myDate.getYear() + "-" + myDate.getMonth() + "-" + myDate.getDate() + "-" + myDate.getDay() + myDate.getTime() + "-" + myDate.getHours() + "-" + myDate.getMinutes() + "-" + myDate.getSeconds() + "-" + myDate.getMilliseconds() + "-" + Math.random(); return $.md5(tm); },
        //转化成jquery对象
        tojQuery: function (obj) { if (!(obj instanceof jQuery)) { obj = $(obj) } return obj; },
        //获得offset位置
        offset: function (obj) { obj = this.tojQuery(obj); var objpos = obj.offset(); var parentobj = obj.offsetParent(); var oleft = objpos.left + parentobj.scrollLeft(); var otop = objpos.top + parentobj.scrollTop(); return { left: oleft, top: otop }; },
        getobjpos: function (obj) { obj = this.tojQuery(obj); if (obj.length <= 0) { return { left: 0, top: 0} } var left = obj.css("left"); left = left.replace("px", ""); left = parseInt(left); var top = obj.css("top"); top = top.replace("px", ""); top = parseInt(top); return { left: left, top: top} },
        a: function () { }
    },
    changetool: function ()//更换工具
    {
        var wf = this;
        $(".tool").click(function () {
            wf.config.tooltype = { type: $(this).attr("type"), value: $(this).attr("value") };
            $(".tool").removeClass("menuitemactive");
            $(this).addClass("menuitemactive");
        });
    },
    //设置工具
    settool: function (value) {
        $(".tool").removeClass("menuitemactive");
        $(".tool[value=" + value + "]").addClass("menuitemactive");
        this.config.tooltype = { type: $(".tool[value=" + value + "]").attr("type"), value: $(".tool[value=" + value + "]").attr("value") };
    },
    paintpanelclick: function ()//画布点击事件
    {
        var wf = this;
        $("#paintarea").click(function (e) {
            var tooltype = wf.config.tooltype; //工具类型
            e = workflow.tool.getevent(e);
            if (tooltype.type == "tool")//添加节点元素
            {
                //nodeobject//nodelist
                var nodetype = tooltype.value;
                //var nodeobj = new wf.nodeobject[nodetype];
                var nodeobj = wf.nodeobject.getNode(nodetype); //把基础属性加到某type类型的属性上，并返回一个对象

                wf.config.zindex = wf.config.zindex + 1;
                var zindex = wf.config.zindex;
                var newid = wf.tool.newname(); //随机生成新id
                var mousePosition = workflow.tool.mouseOnPanelPosition(e);
                var node = wf.config.demonode.clone();
                node.css("left", mousePosition.x);
                node.css("top", mousePosition.y);
                node.css("z-index", zindex);
                node.attr("id", newid);
                node.height(nodeobj.height);
                node.width(nodeobj.width);
                $(">.c>.icon>.t", node).html("&lt;&lt;" + nodeobj.nodetext + "&gt;&gt;&nbsp;");
                $(">.c>.icon>.tt", node).html(nodeobj.text);
                $(">.c>.icon", node).width(nodeobj.width - 50);
                $(">.c>.icon", node).addClass(nodeobj.icon);
                $(">.c>.icon", node).addClass("nodeicon");
                nodeobj.id = newid;
                nodeobj.x = mousePosition.x;
                nodeobj.zindex = zindex;
                nodeobj.y = mousePosition.y;
                wf.nodelist.add(nodeobj);
                node.bind("click", wf.nodeclick);
                node.bind("mousedown", wf.nodemousedown);
                node.bind("mouseup", wf.nodemouseup);
                $(this).append(node);

                wf.settool(0); //设置工具
                setTimeout(function () { $(node).trigger("click") }, 100); //初始化属性
            }
            else {
                AtawWfConfig_Fun();
            }
        });
    },
    //节点点击事件
    nodeclick: function (e) {
        if (workflow.config.tooltype.type == "select") {
            workflow.config.moveobj.obj = $(this);
            workflow.config.nowselect = $(this);
            workflow.showmovediv($(this)); /*显示移动div*/
            var node = workflow.nodelist.getByElement(this);
            /*设置属性*/
            workflow.editAttribute();
            return false;
        }

        return true;
    },
    //鼠标点击节点
    nodemousedown: function (e) {
        var wflow = workflow;
        wflow.hidemovediv();
        if (wflow.config.tooltype.type == "select") {
            var mousepos = wflow.tool.mousePosition(e);
            mousepos.x = mousepos.x + $("#paintarea").scrollLeft();
            mousepos.y = mousepos.y + $("#paintarea").scrollTop();
            var thispos = wflow.tool.offset(this);

            var offsetpos = { x: (mousepos.x - thispos.left), y: (mousepos.y - thispos.top) };
            wflow.config.moveobj.offsetpos = offsetpos;
            wflow.config.moveobj.tag = 1;
            wflow.config.moveobj.ismove = true;
            wflow.showmovediv($(this)); //显示移动div
            wflow.config.moveobj.obj = $(this);
            wflow.config.nowselect = $(this);
        }
        else if (wflow.config.tooltype.type == "line") {
            wflow.config.drawline.start = null;
            wflow.config.drawline.end = null;
            wflow.config.drawline.start = $(this);

            var mousepos = wflow.tool.mouseOnPanelPosition(e);
            if (wflow.config.browser.msie) {
                $("#drawbaseline").attr("from", mousepos.x + "," + mousepos.y);
                $("#drawbaseline").attr("to", mousepos.x + "," + mousepos.y);
            }
            else {
                wflow.setNoIElinePos($("#drawbaseline"), mousepos, mousepos);
            }
            wflow.config.drawline.drawtag = true;
            wflow.config.drawline.from.x = mousepos.x;
            wflow.config.drawline.from.y = mousepos.y;
            $("#drawbaseline").show();
        }
        return false;
    },
    //鼠标弹起事件
    nodemouseup: function (e) {

        var wflow = workflow;
        if (wflow.config.tooltype.type == "line" && wflow.config.drawline.drawtag) {
            wflow.config.drawline.end = $(this);
            var eanbleline = wflow.eanbleline();
            if (eanbleline) { wflow.drawline(wflow.config.drawline.start, wflow.config.drawline.end); }
            wflow.config.drawline.drawtag = false;
            $("#drawbaseline").hide();
        }
        else {
            var mousePosition = workflow.tool.mouseOnPanelPosition(e);
            var nodeObj = workflow.nodelist.get($(this).attr("id"));
            nodeObj.x = mousePosition.x;
            nodeObj.y = mousePosition.y;
            //alert("other")
        }
        //return false;
    },
    //非ie设置线的位置
    setNoIElinePos: function setpos(obj, startpos, endpos) {
        obj = this.tool.tojQuery(obj);
        var x1 = startpos.x;
        var y1 = startpos.y;
        var x2 = endpos.x;
        var y2 = endpos.y;
        var minx = Math.min(x1, x2);
        var miny = Math.min(y1, y2);

        var xoff = x2 - x1;
        var yoff = y2 - y1;
        var ay = Math.abs(parseInt(yoff / 2)) - 3 + miny;
        obj.css("top", ay + "px");
        var width = Math.pow((xoff * xoff + yoff * yoff), 0.5); //线长度
        var cos = xoff / width;
        var rad = Math.acos(cos);
        var deg = 180 / (Math.PI / rad);
        // alert("yoff"+ yoff + "  "+ "deg: " +  deg);
        if (yoff < 0) deg = -deg;
        // alert("后： yoff" + "rotate(" + deg + "deg);");
        var ax = xoff / 2;
        ax = x1 - width / 2 + ax;
        // var g = "rotate(" + deg + "deg);"
        // obj.css("-webkit-transform", g)
        //alert(obj.css());
        var isChrome = window.navigator.userAgent.indexOf("Chrome") !== -1;
        if (isChrome) {
            degree = -1 * deg;
            var cosa = Math.cos(degree * Math.PI / 180), sina = Math.sin(degree * Math.PI / 180);
            //alert(Math.sin(Math.PI));        //最恐怖的地方
            if (degree == 90 || degree == -90)
                cosa = 0;
            if (degree == 180)
                sina = 0;
            var newMatrix = { M11: cosa, M12: (-1 * sina), M21: sina, M22: cosa };
            obj.css("-webkit-transform", "matrix(" + newMatrix.M11 + "," + newMatrix.M12 + ","
                + newMatrix.M21 + "," + newMatrix.M22 + ",0,0)");
        }
        else {
            obj.css("-webkit-transform", "rotate(" + deg + "deg)");
        }

        obj.css("-moz-transform", "rotate(" + deg + "deg)");
        obj.width(width);
        obj.css("left", ax + "px");
    },
    /**
    * 画连接线
    * start：开始节点
    * end：结束节点
    * isimport：是否导入，导入时须将此参数设置非空
    */
    drawline: function (start, end, isimport) {
        start = this.tool.tojQuery(start);
        end = this.tool.tojQuery(end);
        var lineid = start.attr("id") + "_" + end.attr("id");

        if (this.config.browser.msie && start.attr("id") != end.attr("id")) {

            var line = document.createElement("v:line");
            var jiantou = document.createElement("v:stroke");
            jiantou.setAttribute("EndArrow", "Classic");
            line.appendChild(jiantou);
            line.setAttribute("id", lineid);
            line.className = "newline";
            $(line).bind("click", this.lineclick);
            var startid = start.attr("id");
            var endid = end.attr("id");
            var fromobj = this.nodelist.get(startid);
            var toobj = this.nodelist.get(endid);
            if (!isimport) {
                var lineobj = new this.nodeobject[100];
                lineobj.id = lineid;
                fromobj.outputlist[fromobj.outputlist.length] = endid;
                toobj.inputlist[toobj.inputlist.length] = startid;
                this.linelist.add(lineobj);
            }
            else {
                var nowline = this.linelist.get(lineid);
                if (nowline) {
                    if ($(">div", line).length == 0) { $(line).append("<div style='position:absolute;left:50%;top:50%;overflow:visible;cursor:default;'></div>") }
                    $(">div", line).html(nowline.text);
                }
            }
            document.getElementById("paintarea").appendChild(line);
            this.setlineposition(start.attr("id"), end.attr("id"));
        }
        else if (start.attr("id") != end.attr("id")) {
            var line = document.createElement("div");
            var jiantou = document.createElement("a");
            line.appendChild(jiantou);
            line.setAttribute("id", lineid);
            line.className = "ffline";
            $(line).bind("click", this.lineclick);

            var startid = start.attr("id");
            var endid = end.attr("id");
            var fromobj = this.nodelist.get(startid);
            var toobj = this.nodelist.get(endid);

            if (!isimport) {
                var lineobj = new this.nodeobject[100];
                lineobj.id = lineid;
                fromobj.outputlist[fromobj.outputlist.length] = endid;
                toobj.inputlist[toobj.inputlist.length] = startid;
                this.linelist.add(lineobj);
            }
            else {
                var nowline = this.linelist.get(lineid);
                if (nowline) {
                    if ($(">div", line).length == 0) { $(line).append("<div></div>") }
                    $(">div", line).html(nowline.text);
                }
            }


            document.getElementById("paintarea").appendChild(line);
            this.setlineposition(start.attr("id"), end.attr("id"));
            //            fromX = $(line).attr("x1");
            //            fromY = $(line).attr("y1");
            //            toX = $(line).attr("x2");
            //            toY = $(line).attr("y2");
        }
        this.resetdeawline
        var lineObject = this.linelist.get(lineid);
        SetNode_Fun(lineObject);
    },
    /*线点击*/
    lineclick: function () {
        var wflow = workflow;
        if (wflow.config.tooltype.type == "select") {
            if (wflow.config.browser.msie) {
                wflow.hidemovediv();
                wflow.config.nowselect = $(this);
                var from = $(this).attr("from");
                var to = $(this).attr("to");
                $("#linestart").css("left", parseInt(from.x - 2) + "pt");
                $("#linestart").css("top", parseInt(from.y - 2) + "pt");
                $("#lineend").css("left", parseInt(to.x - 2) + "pt");
                $("#lineend").css("top", parseInt(to.y - 2) + "pt");
                $("#lineend").show();
                $("#linestart").show();
            }
            else {
                wflow.hidemovediv();
                wflow.config.nowselect = $(this);
                $(this).css("border", "1px dashed red");
            }
            wflow.editAttribute();
            return false;
        }
    },
    /*重置drawline参数*/
    resetdeawline: function () {
        this.config.drawline.start = null;
        this.config.drawline.end = null;
        this.config.drawline.drawtag = false;
        this.config.drawline.from = { x: 0, y: 0 };
    },
    /*检查是否可以连接*/
    eanbleline: function () {
        var start = this.config.drawline.start;
        var end = this.config.drawline.end;
        var startid = start.attr("id");
        var endid = end.attr("id");
        start = this.nodelist.get(startid);
        end = this.nodelist.get(endid);

        if (start == null || end == null || (startid == endid)) { return false }
        if (start.outputtype == 0 || (start.outputtype == 1 && start.outputlist.length >= 1)) { return false }
        if (end.inputtype == 0 || (end.inputtype == 1 && end.inputlist.length >= 1)) { return false }
        return true;
    },
    /*重新设置连线*/
    resetlineposition: function (nodeobj) {
        nodeobj = this.tool.tojQuery(nodeobj);
        var nodeid = nodeobj.attr("id");
        var node = this.nodelist.get(nodeid);
        if (node != null) {
            var left = nodeobj.css("left"); left = left.replace("px", "");
            var top = nodeobj.css("top"); top = top.replace("px", "");
            var attr = left + "," + top;
            var inputlist = node.inputlist;
            var outputlist = node.outputlist;
            /**/
            for (var i = 0; i < inputlist.length; i++) {
                var lineid = inputlist[i] + "_" + nodeid;
                //if(this.config.browser.msie){$("#"+lineid).attr("to",attr);}
                this.setlineposition(inputlist[i], nodeid);
            }
            for (var i = 0; i < outputlist.length; i++) {
                var lineid = nodeid + "_" + outputlist[i];
                //if(this.config.browser.msie){$("#"+lineid).attr("from",attr);}
                this.setlineposition(nodeid, outputlist[i]);
            }
        }
    },
    /*设置线条位置*/
    setlineposition: function (startid, endid) {
        var lineid = startid + "_" + endid;
        var start = $("#" + startid);
        var end = $("#" + endid);
        var line = $("#" + lineid);
        if (start.length <= 0 || end.length <= 0 || line.length <= 0) { return }
        var startpos = this.tool.getobjpos(start);
        var endpos = this.tool.getobjpos(end);
        var startwidth = start.width();
        var startheight = start.height();
        var endwidth = end.width();
        var endheight = end.height();

        var x1 = 0; var y1 = 0; var x2 = 0; var y2 = 0;
        if (startpos.left + startwidth < endpos.left || startpos.left > endpos.left + endwidth) {
            x1 = startpos.left + startwidth < endpos.left ? startpos.left + startwidth : startpos.left;
            x2 = startpos.left + startwidth < endpos.left ? endpos.left : endpos.left + endwidth;
        }
        else {
            var leftoffset = startpos.left > endpos.left ? startpos.left : endpos.left;
            x1 = Math.abs(startpos.left - endpos.left - endwidth) > (endwidth + startwidth) / 2 ? endpos.left - startpos.left - startwidth : startpos.left - (endpos.left + endwidth);
            x1 = Math.abs(x1) / 2 + leftoffset;
            x2 = x1;
        }
        if (startpos.top + startheight < endpos.top || startpos.top > endpos.top + endheight) {
            y1 = startpos.top + startheight < endpos.top ? startpos.top + startheight : startpos.top;
            y2 = startpos.top + startheight < endpos.top ? endpos.top : endpos.top + endheight;
        }
        else {
            var topoffset = startpos.top > endpos.top ? startpos.top : endpos.top;
            y1 = Math.abs(startpos.top - endpos.top - endheight) > (endheight + startheight) / 2 ? endpos.top - startpos.top - startheight : startpos.top - (endpos.top + endheight);
            y1 = Math.abs(y1) / 2 + topoffset;
            y2 = y1;
        }
        if (this.config.browser.msie) {
            line.attr("from", x1 + "," + y1);
            line.attr("to", x2 + "," + y2);
        }
        else {
            line.attr("x1", x1);
            line.attr("y1", y1);
            line.attr("x2", x2);
            line.attr("y2", y2);
            this.setNoIElinePos(line, { x: x1, y: y1 }, { x: x2, y: y2 });
        }
    },
    //删除节点
    delnode: function () {//this.config.nowselect
        var node = this.config.nowselect;

        if (node == null) return;
        var id = node.attr("id");
        if (id.indexOf("_") < 0) {
            var nodeobj = this.nodelist.get(id);
            nodeobj.removeAllLine();
            this.nodelist.remove(id); /*删除节点*/

            node = this.tool.tojQuery(node);
            node.remove();
            this.config.moveobj.obj = null;
            this.config.nowselect = null;
            this.config.moveobj.tag = 0;
            this.config.moveobj.ismove = false;
            this.hidemovediv();
            //删除属性
            DelNode_Fun(nodeobj);
        }
        else {
            var nodeidlist = id.split("_");
            if (nodeidlist.length < 2) { return }
            var start = this.nodelist.get(nodeidlist[0]);
            var end = this.nodelist.get(nodeidlist[1]);
            if (start != null) { start.removeOutput(nodeidlist[1]) }
            if (end != null) { end.removeInput(nodeidlist[0]) }
            this.linelist.remove(id);
            this.hidemovediv();
            node.remove();
            //删除连接线
            DelLine_Fun(id)
        }

    },
    /*设置节点属性*/
    setnodeattr: function (attrlist) {
        var node = this.config.nowselect;
        node = this.tool.tojQuery(node);
        if (node.length <= 0) { return }
        var id = node.attr("id");
        var nodeobj = null;
        if (id.indexOf("_") >= 0) { nodeobj = this.linelist.get(id); } else { nodeobj = this.nodelist.get(id); }
        if (nodeobj == null) { return }

        if (id.indexOf("_") < 0) {
            for (var i = 0; i < attrlist.length; i++) {
                var attr = attrlist[i];
                nodeobj[attr.name] = attr.value;
                if (attr.name == "text") { $(">.c>.icon>.tt", node).html(attr.value); }
            }
        }
        else//线属性
        {
            if (this.config.browser.msie) {
                var linenodediv = $("#" + id + " div");
                if (linenodediv.length > 0) {
                    linenodediv.html(attrlist[0].value);
                }
                else {
                    nodeobj.text = attrlist[0].value;
                    $("#" + id).append("<div style='position:absolute;left:50%;top:50%;overflow:visible;cursor:default;'>" + attrlist[0].value + "</div>");
                }
            }
            else {
                var linenodediv = $("#" + id + " div");
                if (linenodediv.length > 0) {
                    linenodediv.html(attrlist[0].value);
                }
                else {
                    nodeobj.text = attrlist[0].value;
                    $("#" + id).append("<div style='position:absolute;left:50%;top:50%;overflow:visible;cursor:default;'>" + attrlist[0].value + "</div>");
                }
            }
        }
    },
    //显示移动div
    showmovediv: function (obj) {
        $("#linestart").hide(); $("#lineend").hide();
        $(".node").css("cursor", "default"); obj = this.tool.tojQuery(obj); obj.css("cursor", "move"); var paintpost = this.config.paintpos; var objpos = workflow.tool.offset(obj); var oleft = objpos.left; var otop = objpos.top;
        var owidth = obj.width(); var owidth2 = parseInt(owidth / 2); var oheight = obj.height(); var oheight2 = parseInt(oheight / 2); var os = 2; var resize = 4; var l = oleft - paintpost.left; var t = otop - paintpost.top;
        $("#resizelt").css("left", l - (resize + os) + "px"); $("#resizelt").css("top", t - (resize + os) + "px"); $("#resizel").css("left", l - (resize + os) + "px"); $("#resizel").css("top", t - resize / 2 + oheight2 + "px"); $("#resizeb").css("left", l + (owidth2 - resize / 2) + "px"); $("#resizeb").css("top", t + oheight + "px"); $("#resizet").css("left", l + (owidth2 - resize / 2) + "px"); $("#resizet").css("top", t - (resize + os) + "px"); $("#resizelb").css("left", l - (resize + os) + "px"); $("#resizelb").css("top", t + oheight + "px"); $("#resizer").css("left", l + owidth - 4 + "px"); $("#resizer").css("top", t - resize / 2 + oheight2 + "px"); $("#resizert").css("left", l + owidth - 4 + "px"); $("#resizert").css("top", t - (resize + os) + "px"); $("#resizerb").css("left", l + owidth - 4 + "px"); $("#resizerb").css("top", t + oheight + "px");
        $("#resizelt").show(); $("#resizel").show(); $("#resizelb").show(); $("#resizet").show(); $("#resizeb").show(); $("#resizer").show(); $("#resizert").show(); $("#resizerb").show();
    },
    //隐藏移动div
    hidemovediv: function () {
        this.config.moveobj.obj = null;
        this.config.nowselect = null;
        $(".ffline").css("border", "none");
        $(".node").css("cursor", "default");
        $("#resizelt").hide(); $("#resizel").hide();
        $("#resizelb").hide();
        $("#resizet").hide();
        $("#resizeb").hide();
        $("#resizer").hide();
        $("#resizert").hide();
        $("#resizerb").hide();
        $("#linestart").hide();
        $("#lineend").hide();
    },
    /*编辑属性*/
    editAttribute: function () {
        var node = this.config.nowselect;
        if (node == null) { return }
        node = this.tool.tojQuery(node);
        var id = node.attr("id");
        if (id.indexOf("_") < 0)/*元素节点*/
        {
            var nodeobj = this.nodelist.get(id);
            //$("#attrtext").val(nodeobj.text);
            //            alert("节点文本：  " + nodeobj.text);
            //设置结点属性
            SetNode_Fun(nodeobj);
            //nodeobj.text = "haha123";
        }
        else/*连线节点*/
        {
            var nodeobj = this.linelist.get(id);
            SetNode_Fun(nodeobj);
            //$("#attrtext").val(nodeobj.text);
            //            alert("连线节点：  " + nodeobj.text);
        }
    },
    //XML生成、解析
    xmlobject: null,
    getXml: function () {
        if (this.xmlobject == null || this.xmlobject.checkNode == null) {
            return;
        }
        var res = this.xmlobject.checkNode();
        if (res) {
            return this.xmlobject.createXml();
        }
    },
    //导入xml
    parseXml: function (xml) {
        if (this.xmlobject != null && this.xmlobject.parseXml != null) {
            var listobj = this.xmlobject.parseXml(xml);
            if (listobj != null && listobj.nodelist != null && listobj.linelist != null) {
                var nodelist = listobj.nodelist;
                var linelist = listobj.linelist;
                this.nodelist.list = nodelist;
                this.linelist.list = linelist;

                //未完待续
                //绘制节点
                for (var i = 0; i < this.nodelist.list.length; i++) {
                    var nodeobj = this.nodelist.list[i];
                    var node = this.config.demonode.clone();
                    node.css("left", nodeobj.x);
                    node.css("top", nodeobj.y);
                    node.css("z-index", nodeobj.zindex);
                    node.attr("id", nodeobj.id);
                    node.height(nodeobj.height);
                    node.width(nodeobj.width);
                    $(">.c>.icon>.t", node).html("&lt;&lt;" + nodeobj.nodetext + "&gt;&gt;&nbsp;");
                    $(">.c>.icon>.tt", node).html(nodeobj.text);
                    $(">.c>.icon", node).width(nodeobj.width - 50);
                    $(">.c>.icon", node).addClass(nodeobj.icon);
                    $(">.c>.icon", node).addClass("nodeicon");

                    node.bind("click", this.nodeclick);
                    node.bind("mousedown", this.nodemousedown);
                    node.bind("mouseup", this.nodemouseup);
                    $("#paintarea").append(node);
                    SetNode_Fun(nodeobj);
                }

                for (var i = 0; i < this.nodelist.list.length; i++) {
                    var start = this.nodelist.list[i];

                    //drawline
                    for (var j = 0; j < start.outputlist.length; j++) {
                        var endid = start.outputlist[j];
                        var end = $("#" + endid);
                        this.drawline(start, end, 1);
                    }

                }
                AtawWfConfig_Fun();
                iniWorkflowAttr();
            }
        }
    }
}

//保存属性
function saveattribute1() {
    var val = $("#attribute1 #attrtext").val();
    var attrlist = new Array();
    attrlist[attrlist.length] = { name: "text", value: val };
    workflow.setnodeattr(attrlist);

}

//显示隐藏属性栏,tag=1:显示
function hideattribute(tag) { if (tag == 1) { $("#attributecontent").show(); $("#attribute").css("height", "auto"); } else { $("#attribute").css("height", "auto"); $("#attributecontent").hide(); } workflow.resize(); }


var clone = {
    is: function (obj, type) {
        var toString = Object.prototype.toString, undefined;
        return (type === "Null" && obj === null) ||
          (type === "Undefined" && obj === undefined) ||
          toString.call(obj).slice(8, -1) === type;
    },
    deepCopy: function (result, source) {
        for (var key in source) {
            var copy = source[key];
            if (source === copy) continue; //如window.window === window，会陷入死循环
            if (clone.is(copy, "Object")) {
                result[key] = arguments.callee(result[key] || {}, copy);
            } else if (clone.is(copy, "Array")) {
                result[key] = arguments.callee(result[key] || [], copy);
            } else {
                result[key] = copy;
            }
        }
        return result;
    }
}


var xmlobject = {
    nodelist: null, //节点列表
    linelist: null, //连线列表
    createXml: function () {
        var border = document.createElement("div");
        var root = document.createElement("root");
        //        root.name = workflow.name;
        //        root.displayname = workflow.displayname;
        if (workflow.id != "") {
            root.setAttribute("id", workflow.id);
        }
        if (workflow.name != "") {
            root.setAttribute("name", workflow.name);
        }
        if (workflow.displayname != "") {
            root.setAttribute("displayname", workflow.displayname);
        }
        if (workflow.priority != "") {
            root.setAttribute("priority", workflow.priority);
        }
        if (workflow.issave != "") {
            root.setAttribute("issave", workflow.issave);
        }
        if (workflow.description != "") {
            root.setAttribute("description", workflow.description);
        }
        if (workflow.contentchoice != "") {
            root.setAttribute("contentchoice", workflow.contentchoice);
        }
        if (workflow.manualpagexml != "") {
            root.setAttribute("manualpagexml", workflow.manualpagexml);
        }
        if (workflow.controllactions != null && workflow.controllactions != false) {
            var wControllActionNode = document.createElement("controllactions");
            var controlaction;
            for (var i = 0; i < workflow.controllactions.length; i++) {
                controlaction = document.createElement("controllaction");
                for (wcanAttr in workflow.controllactions[i]) {
                    if (workflow.controllactions[i][wcanAttr] != "") {
                        controlaction.setAttribute(wcanAttr.toLowerCase(), workflow.controllactions[i][wcanAttr]);
                    }
                }
                wControllActionNode.appendChild(controlaction);
            }
            root.appendChild(wControllActionNode);
        }
        border.appendChild(root);
        for (var i = 0; i < this.nodelist.size(); i++) {
            var node = this.nodelist.getByIndex(i);
            var data = document.createElement("node");
            for (var key in node) {
                var type = typeof node[key];
                type = type.toLowerCase();
                if (type == "string" || type == "number") {
                    var child = document.createElement(key);
                    child.setAttribute("type", type);
                    child.innerHTML = node[key];
                    data.appendChild(child);
                }
                if (key == "controllactions" && type == "object" && node.nodetype == "20") {
                    var controllActions = document.createElement("controllactions");
                    if (node[key] != null) {
                        var controlaction;
                        for (var j = 0; j < node[key].length; j++) {
                            controlaction = document.createElement("controllaction");
                            for (var ca in node[key][j]) {
                                if (node[key][j][ca] != "") {
                                    controlaction.setAttribute(ca.toLowerCase(), node[key][j][ca]);
                                }
                            }
                            controllActions.appendChild(controlaction);
                        }

                    }
                    data.appendChild(controllActions);
                }
                if (key == "process" && type == "object" && node.nodetype == "20") {
                    var process = document.createElement("process");
                    if (node[key] != null) {
                        var uiOperationConfig = document.createElement("uioperationconfig");
                        var nonuiOperationConfigs = document.createElement("nonuioperationconfigs");
                        for (var pr in node[key]) {
                            if (pr == "UIOperationConfig") {
                                for (var uipr in node[key][pr]) {
                                    if (node[key][pr][uipr] != "") {
                                        uiOperationConfig.setAttribute(uipr.toLowerCase(), node[key][pr][uipr]);
                                    }
                                }
                            } else if (pr == "NonUIOperationConfigs") {
                                for (var j = 0; j < node[key][pr].length; j++) {
                                    var nonuiOperationConfig = document.createElement("nonuioperationconfig");
                                    for (var nonuipr in node[key][pr][j]) {
                                        if (node[key][pr][j][nonuipr] != "") {
                                            nonuiOperationConfig.setAttribute(nonuipr.toLowerCase(), node[key][pr][j][nonuipr]);
                                        }
                                    }
                                    nonuiOperationConfigs.appendChild(nonuiOperationConfig);
                                }
                            }
                        }
                        process.appendChild(uiOperationConfig);
                        process.appendChild(nonuiOperationConfigs);
                        data.appendChild(process);
                    }
                }
            }
            var parentlist = document.createElement("parentlist");
            var childlist = document.createElement("childlist");
            for (var j = 0; j < node.inputlist.length; j++) {
                var parentid = document.createElement("parentid");
                parentid.innerHTML = node.inputlist[j];
                parentlist.appendChild(parentid);
            }
            for (var j = 0; j < node.outputlist.length; j++) {
                var childid = document.createElement("childid");
                childid.innerHTML = node.outputlist[j];
                childlist.appendChild(childid);
            }
            data.appendChild(parentlist);
            data.appendChild(childlist);
            root.appendChild(data);
        }
        var linelist = document.createElement("linelist");
        root.appendChild(linelist);
        for (var i = 0; i < this.linelist.size(); i++) {
            var lineobj = this.linelist.getByIndex(i);
            var line = document.createElement("line");
            line.innerHTML = lineobj.text;
            line.setAttribute("id", lineobj.id);
            if (lineobj.plugname != "") {
                line.setAttribute("plugname", lineobj.plugname);
            }
            if (lineobj.name != "") {
                line.setAttribute("name", lineobj.name);
            }
            linelist.appendChild(line);
        }
        return "<?xml version=\"1.0\" encoding=\"utf-16\"?>" + border.innerHTML;
    },
    /**
    * 解析xml
    * xml：xml字符串
    * return {nodelist:节点列表,linelist:连线列表}
    */
    parseXml: function (xmlobj) {
        var nodelist = new Array();
        var linelist = new Array();


        var roots = xmlobj.getElementsByTagName("root");
        if (roots.length > 0) {
            var root = roots[0];
            workflow.changeCount = 0;
            workflow.id = root.getAttribute("id");
            workflow.name = root.getAttribute("name");
            workflow.displayname = root.getAttribute("displayname");
            workflow.priority = root.getAttribute("priority");
            workflow.issave = root.getAttribute("issave");
            workflow.description = root.getAttribute("description");
            workflow.contentchoice = root.getAttribute("contentchoice");
            workflow.manualpagexml = root.getAttribute("manualpagexml");
            var wControllActionNodes = root.childNodes;
            var wControllActionNode = null;
            for (var i = 0; i < wControllActionNodes.length; i++) {
                if (wControllActionNodes[i].tagName == "controllactions") {
                    wControllActionNode = wControllActionNodes[i];
                    break;
                }
            }
            if (wControllActionNode != null) {
                workflow.controllactions = [];
                var controllactionChildren = wControllActionNode.childNodes;
                var controllAction;
                for (var i = 0; i < controllactionChildren.length; i++) {
                    controllAction = new ControllAction();
                    controllAction.Order = controllactionChildren[i].getAttribute("order");
                    controllAction.Title = controllactionChildren[i].getAttribute("title");
                    controllAction.ShowKind = controllactionChildren[i].getAttribute("showkind");
                    controllAction.AreaName = controllactionChildren[i].getAttribute("areaname");
                    controllAction.ControllName = controllactionChildren[i].getAttribute("controllname");
                    controllAction.ActionName = controllactionChildren[i].getAttribute("actionname");
                    workflow.controllactions.push(controllAction);
                }
                baseWorkflow.controllactions = [];
                clone.deepCopy(baseWorkflow.controllactions, workflow.controllactions);
            }
            baseWorkflow.id = workflow.id;
            baseWorkflow.name = workflow.name;
            baseWorkflow.priority = workflow.priority;
            baseWorkflow.issave = workflow.issave;
            baseWorkflow.displayname = workflow.displayname;
            baseWorkflow.description = workflow.description;
            baseWorkflow.contentchoice = workflow.contentchoice;
            baseWorkflow.manualpagexml = workflow.manualpagexml;

            var nodes = root.getElementsByTagName("node");
            for (var i = 0; i < nodes.length; i++) {
                var node = nodes[i];
                var nodetype = node.getElementsByTagName("nodetype");
                if (nodetype.length == 1) {
                    nodetype = nodetype[0];
                    nodetype = nodetype.text || nodetype.textContent;
                    var nodeobj = workflow.nodeobject.getNode(nodetype);
                    var nodeattrs = node.childNodes;
                    //var nodeattrs = node.children;
                    for (var j = 0; j < nodeattrs.length; j++) {
                        var attr = nodeattrs[j];
                        var value = attr.text || attr.textContent || ""; //当为空时 ||""
                        var type = attr.getAttribute("type");
                        var name = attr.tagName;
                        if (type) {
                            if (type == "number" && !isNaN(value)) { value = parseInt(value) }
                            nodeobj[name] = value;
                        }
                    }
                    var controllactions = node.getElementsByTagName("controllactions");
                    if (controllactions.length == 1) {
                        controllactions = controllactions[0]
                        nodeobj.controllactions = [];
                        var controllactionChildren = controllactions.childNodes;
                        var controllAction;
                        for (var k = 0; k < controllactionChildren.length; k++) {
                            controllAction = new ControllAction();
                            controllAction.Order = controllactionChildren[k].getAttribute("order");
                            controllAction.Title = controllactionChildren[k].getAttribute("title");
                            controllAction.ShowKind = controllactionChildren[k].getAttribute("showkind");
                            controllAction.AreaName = controllactionChildren[k].getAttribute("areaname");
                            controllAction.ControllName = controllactionChildren[k].getAttribute("controllname");
                            controllAction.ActionName = controllactionChildren[k].getAttribute("actionname");
                            nodeobj.controllactions.push(controllAction);
                        }
                    }
                    var processNode = node.getElementsByTagName("process");
                    if (processNode.length == 1) {
                        processNode = processNode[0];
                        nodeobj.process = {};
                        var uiOperationConfig = processNode.getElementsByTagName("uioperationconfig");
                        if (uiOperationConfig.length == 1) {
                            uiOperationConfig = uiOperationConfig[0];
                            var uioc = new UIOperationConfig();
                            uioc.Name = uiOperationConfig.getAttribute("name");
                            uioc.DisplayName = uiOperationConfig.getAttribute("displayname");
                            uioc.ButtonCaption = uiOperationConfig.getAttribute("buttoncaption");
                            uioc.PlugIn = uiOperationConfig.getAttribute("plugin");
                            nodeobj.process.UIOperationConfig = uioc;
                        }
                        var nonuiOperationConfigs = processNode.getElementsByTagName("nonuioperationconfigs");
                        if (nonuiOperationConfigs.length == 1) {
                            nonuiOperationConfigs = nonuiOperationConfigs[0];
                            var nonuiOperationConfigChildren = nonuiOperationConfigs.childNodes;
                            var NonUIOperationConfigs = [];
                            var nuioc;
                            for (var k = 0; k < nonuiOperationConfigChildren.length; k++) {
                                nuioc = new NonUIOperationConfig();
                                nuioc.Name = nonuiOperationConfigChildren[k].getAttribute("name");
                                nuioc.DisplayName = nonuiOperationConfigChildren[k].getAttribute("displayname");
                                nuioc.ButtonCaption = nonuiOperationConfigChildren[k].getAttribute("buttoncaption");
                                nuioc.PlugIn = nonuiOperationConfigChildren[k].getAttribute("plugin");
                                nuioc.NeedPrompt = nonuiOperationConfigChildren[k].getAttribute("needprompt");
                                NonUIOperationConfigs.push(nuioc);
                            }
                            nodeobj.process.NonUIOperationConfigs = NonUIOperationConfigs;
                        }

                    }
                    var parentlist = node.getElementsByTagName("parentlist");
                    if (parentlist.length > 0) {
                        parentlist = parentlist[0];
                        var parentids = parentlist.childNodes;
                        for (var j = 0; j < parentids.length; j++) {
                            var parentid = parentids[j];
                            parentid = parentid.text || parentid.textContent;
                            nodeobj.inputlist[nodeobj.inputlist.length] = parentid;
                        }
                    }

                    var childlist = node.getElementsByTagName("childlist");
                    if (childlist.length > 0) {//childid
                        childlist = childlist[0];
                        var childids = childlist.childNodes;
                        for (var j = 0; j < childids.length; j++) {
                            var childid = childids[j];
                            childid = childid.text || childid.textContent;
                            nodeobj.outputlist[nodeobj.outputlist.length] = childid;
                        }
                    }
                    nodelist[nodelist.length] = nodeobj;
                }
            }

            var linelists = root.getElementsByTagName("linelist");
            if (linelists.length > 0) {
                linelists = linelists[0];
                var lines = linelists.getElementsByTagName("line");
                for (var i = 0; i < lines.length; i++) {
                    var line = lines[i];
                    var lineobj = new workflow.nodeobject[100];
                    lineobj.id = line.getAttribute("id");
                    lineobj.text = line.text || line.textContent || "";
                    lineobj.plugname = line.getAttribute("plugname") || "";
                    lineobj.name = line.getAttribute("name") || "";
                    linelist[linelist.length] = lineobj;
                }
            }
        }
        //        $.extend(true, baseWorkflow.nodelist.list, nodelist);
        //        $.extend(true, baseWorkflow.linelist.list, linelist);
        baseWorkflow.nodelist.list = [];
        baseWorkflow.linelist.list = [];
        clone.deepCopy(baseWorkflow.nodelist.list, nodelist);
        clone.deepCopy(baseWorkflow.linelist.list, linelist);
        return { linelist: linelist, nodelist: nodelist };

        var xmlobj = $(xml);
        $(">node", xmlobj).each(function (i) {

            var nodetype = $(">nodetype", this).html();
            nodetype = $.trim(nodetype);
            if (workflow.nodeobject.getNode(nodetype) != null) {
                var node = workflow.nodeobject.getNode(nodetype);
                $(">*", this).each(function () {
                    var type = $(this).attr("type");
                    if (type) {
                        type = $.trim(type);
                        type = type == "number" ? "number" : "string";
                        var value = $(this).html().toLowerCase(); value = $.trim(value);
                        var name = $(this).attr("tagName").toLowerCase(); name = $.trim(name);
                        if (type == "number" && !isNaN(value)) { value = parseInt(value) }
                        node[name] = value;
                    }
                });
                $(">parentlist>parentid", this).each(function () {
                    node.inputlist[node.inputlist.length] = $(this).html();
                });
                $(">childlist>childid", this).each(function () {
                    node.outputlist[node.outputlist.length] = $(this).html();
                });
                nodelist[nodelist.length] = node;
            }
        });

        $(">linelist>line", xmlobj).each(function () {
            var line = new workflow.nodeobject[100];
            line.id = $(this).attr("id");
            line.text = $(this).html();
            linelist[linelist.length] = line;
        });

        return { linelist: linelist, nodelist: nodelist }
    },
    //检查
    baseFieldValidate: function (node, shortNames) {
        var nodeName;
        switch (node.nodetype) {
            case 1:
                nodeName = "开始节点";
                break;
            case 2:
                nodeName = "结束节点";
                break;
            case 22:
                nodeName = "自动节点";
                break;
            case 20:
                nodeName = "人工节点";
                break;
            case 21:
                nodeName = "路由节点";
                break;
            default:
                nodeName = "";
        }
        if (node.name == "") {
            alert(nodeName + "-" + node.text + "\n名称不能为空");
            return false;
        }
        for (var j = 0; j < shortNames.length; j++) {
            if (shortNames[j] == node.name) {
                alert(nodeName + "-" + node.text + "\n和其他节点名重复");
                return false;
            }
        }

        if (node.text == "") {
            alert(nodeName + "\n节点显示名不能为空");
            return false;
        }
        return true;
    },
    checkNode: function () {
        var start = 0; /*输入节点数量*/
        var end = 0; /*输出节点数量*/
        var inputnull = 0; /*空输入数量*/
        var outputnull = 0; /*空输出数量*/
        var othererror = 0; /*输入类型和输入节点数量不匹配数量*/
        var shortNames = [];
        if (workflow.name == "") {
            alert("工作流名称不能为空");
            return false;
        }
        if (workflow.displayname == "") {
            alert("工作流显示名不能为空");
            return false;
        }
        if (workflow.description == "") {
            alert("工作流描述不能为空");
            return false;
        }
        if (workflow.manualpagexml == "" && (workflow.controllactions == null || workflow.controllactions == false)) {
            alert("工作流manualpagexml和controllactions不能同时为空");
            return false;
        }
        for (var i = 0; i < this.nodelist.size(); i++) {
            var node = this.nodelist.getByIndex(i);
            if (node != null) {
                if (!this.baseFieldValidate(node, shortNames)) {
                    return false;
                }
                /*计算开始、结束节点数量*/
                if (node.nodetype == 1) {
                    if (node.creatorregname == "") {
                        alert("开始节点-" + node.text + "\n创建者插件不能为空");
                        return false;
                    }
                    start++;
                } else if (node.nodetype == 2) {
                    end++;
                } else if (node.nodetype == 22) {
                    if (node.plugregname == "") {
                        alert("自动节点-" + node.text + "\n插件不能为空");
                        return false;
                    }
                } else if (node.nodetype == 20) {
                    if ((node.controllactions == null || node.controllactions == false) && node.manualpagexml == "") {
                        alert("人工节点-" + node.text + "\nmanualpagexml和controllactions不能都为空");
                        return;
                    }
                    if (node.actorregname == "") {
                        alert("人工节点-" + node.text + "\n插件不能为空");
                        return false;
                    }
                } else if (node.nodetype == 21) {
                    var line;
                    var lineNames = [];
                    for (var k = 0; k < this.linelist.size(); k++) {
                        line = this.linelist.getByIndex(k);
                        if (line.id.indexOf(node.id) == 0) {
                            if (line.name == "") {
                                alert("路由步骤-" + node.text + "\n连接到下一节点的连接线名称不能为空");
                                return false;
                            }
                            for (var j = 0; j < lineNames.length; j++) {
                                if (lineNames[j] == line.name) {
                                    alert("路由节点-" + node.text + "\n和其他路由连接线名称不能重复");
                                    return false;
                                }
                            }

                            if (line.text == "") {
                                alert("路由步骤-" + node.text + "\n连接到下一节点的连接线显示名称不能为空");
                                return false;
                            }
                            if (line.plugname == "") {
                                alert("路由步骤-" + node.text + "\n连接到下一节点的连接线插件名称不能为空");
                                return false;
                            }
                            lineNames.push(line.name);
                        }
                    }
                }
                /*没有输入节点的数量*/
                if (node.inputtype > 0 && node.inputlist.length == 0) { inputnull++; }
                /*没有输出节点的数量*/
                if (node.outputtype > 0 && node.outputlist.length == 0) { outputnull++; }
                /*输入、输出节点类型和节点数量不匹配*/
                if (node.inputtype == 1 && node.inputlist.length != 1) { othererror++ } else if (node.inputtype == 0 && node.inputlist.length != 0) { othererror++ } if (node.outputtype == 1 && node.outputlist.length != 1) { othererror++ } else if (node.outputtype == 0 && node.outputlist.length != 0) { othererror++ }
                shortNames.push(node.name);
            }
        }
        if (start == 1 && end >= 1 && inputnull == 0 && outputnull == 0 && othererror == 0) {
            return true;
        }
        var str = "错误信息";
        str += "\n输入节点数量：" + start;
        str += "\n输出节点数量：" + end;
        str += "\n空输入数量：" + inputnull;
        str += "\n空输出数量：" + outputnull;
        str += "\n输入类型和输入、输出数量不匹配：" + othererror;
        alert(str);
        return false;
    }
}

function createXml(str) {
    if (document.all) {
        var xmlDom = new ActiveXObject("Microsoft.XMLDOM")
        xmlDom.loadXML(str)
        return xmlDom
    }
    else
        return new DOMParser().parseFromString(str, "text/xml")
}
