layui.use(['form', 'layedit', 'laydate', 'element'], function () {
    $("#linkReim").addClass("navbarCheckIn");

    var form = layui.form,
        layer = layui.layer,
        layedit = layui.layedit,
        laydate = layui.laydate;

    //执行一个laydate实例
    laydate.render({
        elem: '#CLIV_Date', //指定元素
        format: 'dd/MM/yyyy',
        lang: 'en'
    });

    form.on('submit(btnEdit)', function (data) {
       
        ShowLoading();
        $.post("Edit", data.field, function (dat, status) {
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

function check() {
    return false;
}
