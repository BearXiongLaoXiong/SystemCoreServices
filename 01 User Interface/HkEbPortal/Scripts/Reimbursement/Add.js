layui.use(['form', 'layedit', 'laydate', 'element', 'upload'], function () {
    $("#linkReim").addClass("navbarCheckIn");

    var form = layui.form,
        layer = layui.layer,
        layedit = layui.layedit,
        laydate = layui.laydate,
        upload = layui.upload;

    //执行一个laydate实例
    laydate.render({
        elem: '#CLIV_Date', //指定元素
        format: 'dd/MM/yyyy',
        lang: 'en'
    });

    form.verify({
        required: function (value) {
            if (value.trim().length < 1) {
                return il8nMessage("public.common.VerifyRequired");
            }
        }
    });

    form.on('submit(btnAdd)', function (data) {

        ShowLoading();
        $.post("Add", data.field, function (dat, status) {
            CloseLoading();

            if (dat.Code === 0) {
                window.location.href = "../Reimbursement/Index";
            } else {
                layeralert(dat.Msg);
            }
        });

        return false;
    });
    
});


function onInsufficientTips(val) {
    layer.tips(il8nMessage("pop-up.public.Insufficient"), val, {
        tips: [4, '#78BA32']
    });
}