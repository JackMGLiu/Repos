﻿var typeDataKey = "typeDataKey";
layui.use(['tree', 'layer', 'jsGrid', 'ejq', 'laypage'], function () {
    /***变量***/
    var layer = layui.layer,
        $ = layui.jquery,
        jsGrid = layui.jsGrid,
        laypage = layui.laypage;
    var $sizeDemo = $(".site-demo");
    var $gridTable = $(".gridtable");
    var $buttonBox = $(".site-demo-button");
    var btnAdd = $("#btnAdd");
    var btnDel = $("#btnDel");
    var btnAddType = $("#btnAddType");
    var btnDelType = $("#btnDelType");
    var btnEditType = $("#btnEditType");
    var btnSelect = $("#btnSelect");
    var hidTypeId = $("#hidTypeId");
    var hidTypeName = $("#hidTypeName");
    var lblTypeName = $("#lblTypeName");
    var lblPageCount = $("#lblPageCount");
    var typeSavediv = $("#typeSavediv");
    var hidLevel = $("#hidLevel");
    var hidTypeSort = $("#hidTypeSort");
    /***事件***/
    btnAdd.click(function() {
        var addUrl = layui.ejq.action.url("PageDCAdd", null, "DocContent", "AdminSite");
        if (hidTypeId.val() != "") {
            if (hidLevel.val() != 2) {
                layer.msg('不能在一级分类和二级分类下面添加文章！');
                return;
            }
            addUrl += "?typeId=" + hidTypeId.val() + "&typeName=" + encodeURI(hidTypeName.val());
            window.open(addUrl);
        } else {
            layer.msg('请选择分类！');
        }
    });
    btnDel.click(function() {
        //询问框
        layer.confirm('您是否要删除？',
            {
                btn: ['确定', '取消'] //按钮
            },
            function() {
                var ccb = $("input:checkbox:checked");
                if (ccb.size() == 0) {
                    layer.msg('请选择一项！');
                    return;
                }
                $.ajax({
                    url: $.action.url("DC_Delete", null, "DocContent", "AdminSite"),
                    data: { ids: ccb.attrToArray("value") },
                    cache: false,
                    traditional: true,
                    success: function(msg) {
                        bindGird();
                        layer.msg('删除成功！', { icon: 1 });
                    },
                    error: function(msg) {
                        console.log(msg);
                        layer.msg('删除失败！', { icon: 1 });
                    }
                })
            },
            function() {

            });
    });
    btnSelect.click(function() {
        var cbs = $("input:checkbox");
        var isChecked = cbs.prop("checked");
        cbs.prop("checked", !isChecked)
    });
    btnAddType.click(function() {
        if (hidTypeId.val() == "") {
            layer.msg('请选分类！');
            return;
        }
        if (hidLevel.val() == 2) {
            layer.msg('最多只能添加3级分类！');
            return;
        }
        layer.open({
            type: 1,
            title: '添加【' + hidTypeName.val() + '】的子分类',
            area: ['550px', '300px'],
            shade: 0.5,
            content: typeSavediv.html(),
            btn: ['添加', '关闭'] //只是为了演示
            ,
            yes: function() {
                $.ajax({
                    type: "post",
                    url: $.action.url("DcType_Submit", null, "DocContent", "AdminSite"),
                    data: {
                        ParentId: hidTypeId.val(),
                        TypeName: $(".layui-layer-content [name=Name]").val(),
                        Sort: $(".layui-layer-content [name=Sort]").val(),
                        MasterId: $(".layui-layer-content [name=MasterId]").val()
                    },
                    cache: false,
                    success: function(msg) {
                        layer.msg('添加成功！', { icon: 1 });
                        bindTree();
                    }
                });
                layer.closeAll();
            },
            btn2: function() {
                layer.closeAll();
            },
            zIndex: layer.zIndex //重点1
            ,
            success: function(layero) {
                layer.setTop(layero); //重点2
            }
        });
    });
    btnDelType.click(function() {
        if (hidTypeId.val() == "") {
            layer.msg('请选分类！');
            return;
        }
        if (hidLevel.val() == 0) {
            layer.msg('不能删除一级分类！');
            return;
        }
        //询问框
        layer.confirm('您是否要删除:' + hidTypeName.val() + '？',
            {
                btn: ['确定', '取消'] //按钮
            },
            function() {
                $.ajax({
                    url: $.action.url("DCType_Delete", null, "DocContent", "AdminSite"),
                    data: { id: hidTypeId.val() },
                    cache: false,
                    traditional: true,
                    success: function(msg) {
                        bindTree();
                        layer.msg('删除成功！', { icon: 1 });
                        hidTypeId.val("");
                        hidTypeName.val("");
                        lblTypeName.empty();
                    },
                    error: function(msg) {
                        console.log(msg);
                        layer.msg('删除失败！', { icon: 1 });
                    }
                })
            },
            function() {

            });
    });
    btnEditType.click(function() {
        if (hidTypeId.val() == "") {
            layer.msg('请选择分类！');
            return;
        }
        layer.open({
            type: 1,
            title: '修改【' + hidTypeName.val() + '】',
            area: ['550px', '300px'],
            shade: 0.5,
            content: typeSavediv.html(),
            btn: ['保存修改', '关闭'] //只是为了演示
            ,
            yes: function() {
                $.ajax({
                    type: "post",
                    url: $.action.url("DcType_Submit", null, "DocContent", "AdminSite"),
                    data: {
                        id: hidTypeId.val(),
                        TypeName: $(".layui-layer-content [name=Name]").val(),
                        Sort: $(".layui-layer-content [name=Sort]").val(),
                        MasterId: $(".layui-layer-content [name=MasterId]").val()
                    },
                    cache: false,
                    success: function(msg) {
                        layer.msg('修改成功！', { icon: 1 });
                        bindTree();
                    }
                });
                layer.closeAll();
            },
            btn2: function() {
                layer.closeAll();
            },
            zIndex: layer.zIndex //重点1
            ,
            success: function(layero) {
                layer.setTop(layero); //重点2
                var treeItem = $("body").data(typeDataKey);
                $(".layui-layer-content [name=Name]").val(hidTypeName.val());
                $(".layui-layer-content [name=Sort]").val(hidTypeSort.val());
                $(".layui-layer-content [name=MasterId]").val(treeItem.masterId);
            }
        });
    });

    /***初始化***/
    bindGird();
    bindTree();

    /***公用方法***/
    function bindTree() {
        $("#treediv").html("");
        $.ajax({
            url: $.action.url("Dc_GetTypeList", null, "DocContent", "AdminSite"),
            cache: false,
            success: function (msg) {
                layui.tree({
                    elem: '#treediv',
                    target: '_blank',
                    click: function (item) { //点击节点回调
                        hidTypeId.val(item.id);
                        hidTypeName.val(item.name)
                        hidLevel.val(item.level);
                        hidTypeSort.val(item.sort);
                        $("body").data(typeDataKey, item);
                        lblTypeName.html("<span>当前分类:</span>" + item.name);
                        bindGird();
                    }, nodes: msg.ResultInfo
                });
                $("[data-spread=true] cite,[data-spread=true] i").click(function () {
                    $(".tree-current").removeClass("tree-current");
                    $(this).parent().parent().addClass("tree-current");
                })
            }
        });
    }
    function bindGird(obj) {
        var listGird = new jsGrid({
            id: "listGrid",
            width: "100%",
            doubleHead: true,
            height: $(window).height() - $buttonBox.height() - 150,
            "class": "gridtable",
            columns: [
                {
                    title: "标题",
                    indexname: "Title",
                    textalign: 'center',
                    width: "50",
                    formatter:
                    function (val, rowdata, rowindex, td, trL, trR) {
                        return '<input name="ids" value="' + rowdata.Id + '" type="checkbox" />';
                    }
                },
                {
                    title: "编号",
                    indexname: "Id",
                    textalign: 'left',
                    width: 100,
                },
                {
                    title: "标题",
                    indexname: "Title",
                    textalign: 'left',
                    width: 500,
                    formatter:
                    function (val, rowdata, rowindex, td, trL, trR) {
                        return '<a target="_blank" href="' + $.action.url("PageDCAdd", null, "DocContent", "AdminSite") + "?id=" + rowdata.Id + '&typeName=' + encodeURI(rowdata.TypeName) + '">' + val + '</a>';
                    }
                },
                {
                    title: "预览",
                    indexname: "TypeId",
                    textalign: 'left',
                    width: 500,
                    formatter:
                    function (val, rowdata, rowindex, td, trL, trR) {
                        return '<a target="_blank" href="' + $.action.url("Doc", null, "Home") + "?typeId=" + val + '&typeName=' + encodeURI(rowdata.TypeName) + '">预览 </a>';
                    }
                },
                {
                    title: "创建人",
                    indexname: "Creator",
                    textalign: 'center',
                    width: 100
                },
                {
                    title: "创建时间",
                    indexname: "CreateTime",
                    textalign: 'center',
                    width: 100
                }]
        })
        $.ajax({
            url: $.action.url("Dc_GetList", null, "DocContent", "AdminSite"),
            data: { pageIndex: obj == null ? 1 : obj.curr, typeId: hidTypeId.val() },
            cache: false,
            success: function (msg) {
                var datas = msg.ResultInfo.DocList;
                listGird.RenderTo('listdiv');
                listGird.dataStore = datas;
                listGird.ShowData();
                lblPageCount.text(msg.ResultInfo.PageCount);
                laypage({
                    cont: 'pagediv',
                    pages: msg.ResultInfo.Pages,
                    curr: msg.ResultInfo.PageIndex,
                    prev: '<em><</em>',
                    next: '<em>></em>',
                    jump: function (obj, first) {
                        if (!first) {
                            bindGird(obj);
                        }
                    }
                });
                if (msg.ResultInfo.Pages <= 1) {
                    $(".page-count").hide();
                } else {
                    $(".page-count").show();
                }
            },
            error: function (msg) {
                console.log(msg);
            }
        });
    }
});