layui.use('form', function () {
    var form = layui.form;

    form.verify({
        required: function (value) {
            if (value.trim().length < 1) {
                return il8nMessage("public.common.VerifyRequired");
            }
        }
    });

    //监听提交
    form.on('submit(formDemo)', function (data) {

        var verify = false,
            oldVerify = false,
            newVerify = false,
            confirmVerify = false;
        try {
            oldVerify = VerifyOldPwd();
            if (!oldVerify) {
                $("#oldPassword").focus();
                return false;
            }

            newVerify = VerifyNewPwd();
            if (!newVerify) {
                $("#newPassword").focus();
                return false;
            }

            confirmVerify = VerifyConfirmPwd();
            if (!confirmVerify) {
                $("#confirmPassword").focus();
                return false;
            }

            verify = oldVerify && newVerify && confirmVerify;
            if (verify) {
                $.post("ChangePassword", data.field, function (data, status) {
                    if (data.Code === 0) {
                        layeralert(data.Msg, function() {
                            location.href = "../Home/Index";
                        });
                    } else {
                        layeralert(data.Msg);
                    }
                });
            }
        } catch (e) {
        }
        return false;
    });

    var htmNewTips = '<ul>' +
        '<li id="rule1" class="pwd-checklist-err"><i id="icon1" class="layui-icon">&#x1006;</i><span i18n="user.modify.pwdLength">長度為8~16個字符</span></li>' +
        '<li id="rule2" class="pwd-checklist-err"><i id="icon2" class="layui-icon">&#xe605;</i><span i18n="user.modify.pwdSpace">不允許有空格</span></li>' +
        '<li id="rule3" class="pwd-checklist-err"><i id="icon3" class="layui-icon">&#x1006;</i><span i18n="user.modify.pwdNumbers">至少有一個數字</span></li>' +
        '<li id="rule4" class="pwd-checklist-err"><i id="icon4" class="layui-icon">&#x1006;</i><span i18n="user.modify.pwdUpper">至少有一個大寫字母</span></li>' +
        '<li id="rule5" class="pwd-checklist-err"><i id="icon5" class="layui-icon">&#x1006;</i><span i18n="user.modify.pwdLower">至少有一個小寫字母</span></li>' +
        '<li id="rule6" class="pwd-checklist-err"><i id="icon6" class="layui-icon">&#xe63f;</i><span i18n="user.modify.pwdPunctuation">至少有一個標點符號</span></li>' +
        //'<li class="pwd-checklist-def"><i class="layui-icon" >&#xe63f;</i>默认</li>' +
        //'<li class="pwd-checklist-suc"><i class="layui-icon" >&#xe605;</i>正确</li>' +
        //'<li class="pwd-checklist-err"><i class="layui-icon" >&#x1006;</i>错误</li>' +
        '</ul>';

    var htmConfirmTips = '<ul>' +
        '<li id="conRule1" class="pwd-checklist-err"><i id="conIcon1" class="layui-icon">&#x1006;</i><span i18n="user.modify.pwdLength">長度為[8~16]個字符</span></li>' +
        '<li id="conRule2" class="pwd-checklist-err"><i id="conIcon2" class="layui-icon">&#x1006;</i><span i18n="user.modify.pwdSame">2次輸入的密碼必須一致</span></li>' +
        '</ul>';

    $("#oldPassword").focus(function () { VerifyOldPwd(); });
    $("#oldPassword").keyup(function () { VerifyOldPwd(); });
    $("#oldPassword").blur(function () { VerifyOldPwd(); });

    $("#newPassword").focus(function () {
        layer.tips(htmNewTips, this, {
            tips: [2, '#F5F5DC'],
            time: 0,
            id: "newPasswordTips",
            success: function () {
                VerifyNewPwd();
                laodlanguage();
            }
        });
    });
    $("#newPassword").keyup(function () { VerifyNewPwd(); });
    $("#newPassword").blur(function () {
        if (VerifyNewPwd()) {
            SetRightIcon($("#newIcon"));
        } else {
            SetErrorIcon($("#newIcon"));
        }
        layer.closeAll();
    });

    $("#confirmPassword").focus(function () {
        layer.tips(htmConfirmTips, this, {
            tips: [2, '#F5F5DC'],
            time: 0,
            id: "confirmPasswordTips",
            success: function () {
                VerifyConfirmPwd();
                laodlanguage();
            }
        });
    });
    $("#confirmPassword").keyup(function () { VerifyConfirmPwd(); });
    $("#confirmPassword").blur(function () {
        if (VerifyConfirmPwd()) {
            SetRightIcon($("#confirmIcon"));
        } else {
            SetErrorIcon($("#confirmIcon"));
        }
        layer.closeAll();
    });

});


function VerifyOldPwd() {
    if ($("#oldPassword").val().trim().length > 0) {
        SetRightIcon($("#oldIcon"));
        return true;
    } else {
        SetErrorIcon($("#oldIcon"));
        return false;
    }
}

function VerifyNewPwd() {
    var value = $("#newPassword").val();
    var length = false;
    var haveSpace = false;
    var number = false;
    var upper = false;
    var lower = false;
    var punctuation = false;
    var lv = 0;

    //长度8-16
    if (value.length >= 8 && value.length <= 16) {
        length = true;
        SetSuccess($("#icon1"), $("#rule1"), true);
    } else {
        length = false;
        SetSuccess($("#icon1"), $("#rule1"), false);
    }

    //不包含空格
    if (value.match(/([\s])/g)) {
        haveSpace = false;
        SetSuccess($("#icon2"), $("#rule2"), false);
    } else {
        haveSpace = true;
        SetSuccess($("#icon2"), $("#rule2"), true);
        $("#rule2").removeClass("pwd-checklist-err");
    }

    //包含数字
    if (value.match(/([0-9])/g)) {
        lv++;
        number = true;
        SetSuccess($("#icon3"), $("#rule3"), true);
    } else {
        number = false;
        SetSuccess($("#icon3"), $("#rule3"), false);
    }

    //包含大写字母
    if (value.match(/([A-Z])/g)) {
        lv++;
        upper = true;
        SetSuccess($("#icon4"), $("#rule4"), true);
    } else {
        upper = false;
        SetSuccess($("#icon4"), $("#rule4"), false);
    }

    //包含小写字母
    if (value.match(/([a-z])/g)) {
        lv++;
        lower = true;
        SetSuccess($("#icon5"), $("#rule5"), true);
    } else {
        lower = false;
        SetSuccess($("#icon5"), $("#rule5"), false);
    }

    //包含标点符号
    if (value.match(/([~!@#$%^&*()_+{}|:"<>?=;',./\\\[\]\-])/g)) {
        lv++;
        punctuation = true;
        SetSuccess($("#icon6"), $("#rule6"), true);
    } else {
        punctuation = false;
        SetSuccess($("#icon6"), $("#rule6"), false);
    }

    //有限制3条符合条件则认为验证通过
    if (lv === 3) {
        if (!number) SetDefault($("#icon3"), $("#rule3"));
        else if (!upper) SetDefault($("#icon4"), $("#rule4"));
        else if (!lower) SetDefault($("#icon5"), $("#rule5"));
        else if (!punctuation) SetDefault($("#icon6"), $("#rule6"));
    }

    return length && haveSpace && lv > 2;
}

function VerifyConfirmPwd() {
    var length = false,
        same = false,
        newPwd = $("#newPassword").val(),
        confirmPwd = $("#confirmPassword").val();

    //长度8-16
    if (confirmPwd.length >= 8 && confirmPwd.length <= 16) {
        length = true;
        SetSuccess($("#conIcon1"), $("#conRule1"), true);
    } else {
        length = false;
        SetSuccess($("#conIcon1"), $("#conRule1"), false);
    }

    //2此输入的密码必须一致
    if (confirmPwd.length > 0 && newPwd === confirmPwd) {
        same = true;
        SetSuccess($("#conIcon2"), $("#conRule2"), true);
    } else {
        same = false;
        SetSuccess($("#conIcon2"), $("#conRule2"), false);

    }

    return length && same;
}


function SetSuccess(icon, rule, status) {
    if (status) {
        icon.html("&#xe605;");
        rule.addClass("pwd-checklist-suc");
        rule.removeClass("pwd-checklist-err");
        rule.removeClass("pwd-checklist-def");
    } else {
        icon.html("&#x1006;");
        rule.addClass("pwd-checklist-err");
        rule.removeClass("pwd-checklist-suc");
        rule.removeClass("pwd-checklist-def");
    }
}

function SetDefault(icon, rule) {
    icon.html("&#xe63f;");
    rule.addClass("pwd-checklist-def");
    rule.removeClass("pwd-checklist-suc");
    rule.removeClass("pwd-checklist-err");
}

function SetRightIcon(icon) {
    icon.html("&#xe605;");
    icon.show();
}

function SetErrorIcon(icon) {
    icon.html("&#x1006;");
    icon.show();
}