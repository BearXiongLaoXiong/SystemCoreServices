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
                if (result.Data === 1) {
                    layeralert1(result.Msg, function () {
                        window.location.href = "../User/Login";
                    });
                } else if (result.Data === 2){
                    layeralert(il8nMessage("pop-up.user.forgotpassword.notExistAccount"));
                } else if (result.Data === 3) {
                    layeralert(il8nMessage("pop-up.user.forgotpassword.noMailboxs"));
                } else if (result.Data === 4) {
                    layeralert(il8nMessage("pop-up.user.forgotpassword.mailboxnotactive"));
                } else if (result.Data === 5) {
                    layeralert(il8nMessage("pop-up.user.forgotpassword.notRegistered"));
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