layui.use(['form', 'element'], function () {
    $("#linkInsurant").addClass("navbarCheckIn");
    var form = layui.form,
        element = layui.element;

    //var index = ShowLoading();
    //setTimeout(function () { layer.close(index) }, 500);

});

function ShowBenefitInfo(plplky, memeky) {
    layer.open({
        type: 2,
        title: 'Benefit Info',
        skin: 'layui-layer-demo', //样式类名
        area: ['900px', '500px'],
        closeBtn: 1,
        shadeClose: true, //开启遮罩关闭
        content: ['BenefitDetail?plplKy=' + plplky + '&memeKy=' + memeky],
        btnAlign: 'c'
    });
}

function ShowDetail(fmacky) {
    layer.open({
        type: 2,
        title: 'Points Transaction Records',
        skin: 'layui-layer-demo', //样式类名
        area: ['900px', '500px'],
        closeBtn: 1,
        shadeClose: true, //开启遮罩关闭
        content: 'BillingInfomationDetail?fmacKy=' + fmacky,
        btnAlign: 'c'
    });
}