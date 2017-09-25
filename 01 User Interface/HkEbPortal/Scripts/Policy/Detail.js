layui.use('element', function () {
    var element = layui.element;

});

layui.use('form', function () {
    var form = layui.form;
    ShowMsg();

    form.on('submit(submitDetailRadio)', function (data) {
        var array = new Array();
        for (var key in data.field) {
            if (data.field.hasOwnProperty(key) && data.field[key].length > 0) {
                array.push(JSON.parse(data.field[key]));
            }
        }
        ShowMsg();
        $.post("Detail",
            {
                data: JSON.stringify(array)
            },
            function (msg) {
                setTimeout(function () {
                    layer.closeAll();
                    layer.open({
                        type: 1,
                        skin: 'layui-layer-demo', //样式类名
                        closeBtn: 0, //不显示关闭按钮
                        shadeClose: true, //开启遮罩关闭
                        content: '<div style="padding: 20px 100px;">' + msg + '</div>',
                        btnAlign: 'c'
                    });
                }, 300);

            });
        return false;
    });
    CloseMsg();
});

function returnbackurl() {
    window.location.href = "index?memeKy=" + getUrlParam("memeKy");
}

function showInformation(pdpdId) {
    layer.open({
        type: 2,
        title: 'Information',
        skin: 'layui-layer-demo', //样式类名
        area: ['800px', '600px'],
        closeBtn: 0, //不显示关闭按钮
        shadeClose: true, //开启遮罩关闭
        content: ['Information?pdpdId=' + pdpdId, 'no'],
        btnAlign: 'c'
    });
}


layui.use('element', function () {
    var element = layui.element;


    //监听Tab切换，以改变地址hash值
    element.on('tab(tabInformation)', function () {
        location.hash = 'tabInformation=' + this.getAttribute('lay-id');
        //alert(this.getAttribute('lay-id'));
    });
});

layui.use('tree', function () {
    layui.tree({
        skin: 'shihuang',
        elem: '#TreeInfo' //传入元素选择器
        , nodes: [{ //节点
            name: '父节点1',
            spread: true
            , children: [{
                name: '子节点11'
            }, {
                name: '子节点12'
            }]
        }, {
            name: '父节点2（可以点左侧箭头，也可以双击标题）'
            , children: [{
                name: '子节点21'
                , children: [{
                    name: '子节点211'
                }]
            }]
        }]
    });
});
