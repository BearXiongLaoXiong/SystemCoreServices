
layui.use(['form', 'carousel', 'laydate'], function () {
    var form = layui.form;
    var laydate = layui.laydate;
    var carousel = layui.carousel;

    $("#linkHome").addClass("navbarCheckIn");

    //$("#txtPolicyUp").val("30000272");
    //$("#txtMemberUp").val("000000001995");
    //轮播
    laydate.render({
        elem: '#txtDate',
        format: 'dd/MM/yyyy',
        lang: 'en'
    });

    carousel.render({
        elem: '#indexcarouselimage'
        , width: '100%'
        , height: '750px'
        , interval: 5000
    });

    form.on('submit(btnLogin)', function (data) {
        //layer.msg(JSON.stringify(data.field));
        ShowLoading();
        $.post("Login",
            { txtpolicyNo: data.field.txtpolicyNo, txtMember: data.field.txtMember, txtPassword: data.field.txtPassword },
            function (result) {
                CloseLoading();
                if (result.Code === 0) {
                    if (result.Data.USUS_FIRST_ISACTIVE == '0') {
                        var message = "Member :</br>";
                        var memberList = result.Data.MemberList;
                        $.each(memberList, function (i, v) {
                            message += v.SYSV_MEME_REL_CD + " : " + v.MEME_NAME + "</br>";
                        });
                        layeralert1(message, function () {
                            layer.confirm('It is advisable to change the password after logging in for the first time', {
                                btn: ['Immediately modify', 'Later said'], //按钮
                                title: '',
                                icon: 6,
                                closeBtn: 0,
                                scrollbar: false,
                                shade: false //不显示遮罩
                            }, function () {
                                window.location.href = "../User/ModifyPwd";
                            }, function () {
                                window.location.href = "../Home/Index";
                            });
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
                else
                    layeralert(il8nMessage(result.Msg));
            });
        return false;
    });

    form.on('submit(btnLoginUp)', function (data) {
        //layer.msg(JSON.stringify(data.field));
        var policyUp = data.field.txtPolicyUp;
        var meberUp = data.field.txtMemberUp;
        ShowLoading();
        $.post("SignUp",
            { txtPolicyUp: policyUp, txtMemberUp: meberUp, txtBirthday: data.field.txtDate, txtEmailUp: data.field.txtEmailUp, confirmEmail: "" },
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
                                { txtPolicyUp: policyUp, txtMemberUp: meberUp, txtBirthday: data.field.txtDate, txtEmailUp: data.field.txtEmailUp, confirmEmail: "confirm" },
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