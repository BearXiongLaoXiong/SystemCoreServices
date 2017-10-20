layui.use(['form','element'], function () {
    var form = layui.form;
    var element = layui.element;


    //监听Tab切换，以改变地址hash值
    element.on('tab(tabInformation)', function () {
        location.hash = 'tabInformation=' + this.getAttribute('lay-id');
        //alert(this.getAttribute('lay-id'));
    });

    ShowMsg();

    form.on('submit(submitDetailRadio)', function (data) {
        var array = new Array();
        for (var key in data.field) {
            if (data.field.hasOwnProperty(key) && data.field[key].length > 0) {
                array.push(JSON.parse(data.field[key]));
            }
        }
        ShowMsg();
        $.post("Detail",{ data: JSON.stringify(array) }, function (dat,status) {
            if (status === "success") {
                    returnbackurl();
                } else {
                    layer.msg(dat);
                }
                //setTimeout(function () {
                //    layer.closeAll();
                //    layer.open({
                //        type: 1,
                //        skin: 'layui-layer-demo', //样式类名
                //        closeBtn: 0, //不显示关闭按钮
                //        shadeClose: true, //开启遮罩关闭
                //        content: '<div style="padding: 20px 100px;">' + msg + '</div>',
                //        btnAlign: 'c'
                //    });
                //}, 300);
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
        closeBtn: 1, 
        shadeClose: true, //开启遮罩关闭
        content: 'Information?pdpdId=' + pdpdId,
        btnAlign: 'c'
    });
}


