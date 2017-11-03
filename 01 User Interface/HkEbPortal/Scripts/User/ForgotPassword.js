layui.use(['form', 'layedit', 'laydate', 'element'], function () {
    var form = layui.form
        , layer = layui.layer
        , layedit = layui.layedit
        , laydate = layui.laydate;

    form.on('submit(btnSubmit)', function (data) {
        ShowLoading();

        $.post("ForgotPassword",
            { policyNo: data.field.policyNo, memberId: data.field.memberId, r: Math.random(), __RequestVerificationToken: $('input[name="__RequestVerificationToken"]', data.form).val() },
            function (result) {
                CloseLoading();
                if (result.Data === 0) {
                    layeralert1(result.Msg, function () {
                        window.location.href = "../User/Login";
                    });
                } else {
                    layeralert(result.Msg);
                }
            });
        return false;
    });
});

$(function () {
    $("#zhuanid").on('click', function () {
        $.ajax({
            type: "POST",
            url: "../User/HTMLConvertPDF",
            data: {},
            success: function (data, status) {
                layer.msg(data + "==" + status);
            }
        });
    });
});