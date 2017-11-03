
layui.use(['form', 'laydate'], function () {
    var form = layui.form,
        laydate = layui.laydate;

    //登陆
    form.on('submit(btnLogin)', function (data) {
        //layer.msg(JSON.stringify(data.field));
        ShowLoading();
        $.post("Login",
            { txtpolicyNo: data.field.txtpolicyNo, txtMember: data.field.txtMember, txtPassword: data.field.txtPassword, r: Math.random(), __RequestVerificationToken: $('input[name="__RequestVerificationToken"]', data.form).val() },
            function (result) {
                CloseLoading();
                if (result.Code === 0) {
                    if (result.Data.USUS_FIRST_ISACTIVE === '0') {
                        //首次登陸提示修改密碼
                        layer.confirm(il8nMessage("pop-up.user.login.firstloginconfirm"), {
                            btn: [il8nMessage("pop-up.user.login.now"), il8nMessage("pop-up.user.login.later")], //按钮
                            title: ' ',
                            icon: 6,
                            closeBtn: 0
                        }, function () {
                            window.location.href = "../User/ChangePassword";
                        }, function () {
                            window.location.href = "../Insurant/Index";
                        });
                    } else {
                        window.location.href = "../Home/Index";
                    }
                }
                else if (result.Code === 3) {
                    layer.confirm('first login,please confirm your Email:</br>' + result.Msg, {
                        title: "Confirm",
                        btn: ['OK', 'Cancel'] //按钮
                    }, function () {
                        $.post("ConfirmEmail",
                            { txtpolicyNo: data.field.txtpolicyNo, txtMember: data.field.txtMember, txtPassword: data.field.txtPassword },
                            function (result) {
                                layer.msg(result.Msg, { icon: 1 });
                            });

                    });
                }
                else if (result.Code === 1) {
                    //保單號碼, 會員編號或密碼不正確
                    layeralert(il8nMessage("pop-up.user.login.noaccount"));
                }
                else
                    layeralert(il8nMessage(result.Msg));
            });
        return false;
    });

    //注册
    form.on('submit(btnLoginUp)', function (data) {
        //layer.msg(JSON.stringify(data.field));
        var policyUp = data.field.txtPolicyUp;
        var meberUp = data.field.txtMemberUp;
        ShowLoading();
        $.post("SignUp",
            { txtPolicyUp: policyUp, txtMemberUp: meberUp, txtBirthday: data.field.txtDate, txtEmailUp: data.field.txtEmailUp, confirmEmail: "", r: Math.random(), __RequestVerificationToken: $('input[name="__RequestVerificationToken"]', data.form).val() },
            function (result) {
                CloseLoading();
                if (result.Code === 0) {
                    layeralert1(result.Msg, function () { signIn(); });
                }
                else if (result.Code === 3) {
                    layer.msg(result.Msg);
                    $("#txtEmailUp").attr("lay-verify", "required");
                    $("#emailDiv").show();
                }
                else if (result.Code === 4) {
                    layer.confirm('Please confirm your Email:</br>' + result.Msg,
                        { title: "Confirm", btn: ['OK', 'Cancel'] },
                        function () {
                            $.post("SignUp",
                                { txtPolicyUp: policyUp, txtMemberUp: meberUp, txtBirthday: data.field.txtDate, txtEmailUp: data.field.txtEmailUp, confirmEmail: "confirm", r: Math.random(), __RequestVerificationToken: $('input[name="__RequestVerificationToken"]', data.form).val() },
                                function (result) {
                                    layeralert1(result.Msg, function () { signIn(); });
                                });
                        });
                }
                else { layeralert(result.Msg); }
            });
        return false;
    });

});

function signIn() {
    $("#signIndiv").addClass("selected");
    $("#singUpdiv").removeClass("selected");

    $("#signInMessage").show();
    $("#signUpMessage").hide();

    $("#signInForm").show();
    $("#signUpForm").hide();
}

function signUp() {
    $("#signIndiv").removeClass("selected");
    $("#singUpdiv").addClass("selected");

    $("#signInMessage").hide();
    $("#signUpMessage").show();

    $("#signInForm").hide();
    $("#signUpForm").show();
}