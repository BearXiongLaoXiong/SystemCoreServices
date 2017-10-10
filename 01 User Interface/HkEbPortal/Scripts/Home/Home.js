
layui.use(['form', 'carousel', 'laydate'], function () {
    var form = layui.form;
    var laydate = layui.laydate;
    var carousel = layui.carousel;

    //$("#txtPolicyUp").val("30000272");
    //$("#txtMemberUp").val("000000001995");
    //轮播
    laydate.render({
        elem: '#txtDate'
    });

    carousel.render({
        elem: '#indexcarouselimage'
        , width: '100%'
        , height: '750px'
        , interval: 5000
    });

    form.on('submit(btnLogin)', function (data) {
        //layer.msg(JSON.stringify(data.field));
        $.post("Login",
            { txtpolicyNo: data.field.txtpolicyNo, txtMember: data.field.txtMember, txtPassword: data.field.txtPassword },
            function (result) {
                if (result.Code === 0)
                    window.location.href = "../Home/Index";
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
                else
                    layer.msg(result.Msg);
            });
        return false;
    });

    form.on('submit(btnLoginUp)', function (data) {
        //layer.msg(JSON.stringify(data.field));
        $.post("SignUp",
            { txtPolicyUp: data.field.txtPolicyUp, txtMemberUp: data.field.txtMemberUp, txtEmailUp: data.field.txtEmailUp },
            function (result) {
                if (result.Code === 3) {
                    layer.msg(result.Msg);
                    $("#txtEmailUp").attr("lay-verify", "required");
                    $("#emailDiv").show();
                }
                else
                    layer.msg(result.Msg);
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