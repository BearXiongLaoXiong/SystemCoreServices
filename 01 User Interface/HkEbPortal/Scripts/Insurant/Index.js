layui.use(['form', 'element'], function () {
    $("#linkInsurant").addClass("navbarCheckIn");
    var form = layui.form,
        element = layui.element;
    
    ShowLoading();
    //element.tabChange('memberInfoTable', "222"); 
    CloseDelayLoading();
    //element.on('tab(docDemoTabBrief)', function (data) {
    //    console.log(data);
    //    return false;
    //});
    //layer.confirm(il8nMessage("pop-up.user.login.firstloginconfirm"), {
    //    btn: ["Confirmed", "Amendment required"], //按钮
    //    title: ' ',
    //    offset: '190px',
    //    closeBtn: 0,
    //    shade: false
    //}, function () {
    //    window.location.href = "../User/ChangePassword";
    //}, function () {
    //    window.location.href = "../Insurant/Index";
    //});
    
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

//function first