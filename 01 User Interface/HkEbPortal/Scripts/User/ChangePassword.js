layui.use('form', function () {
    var form = layui.form;

    //监听提交
    form.on('submit(formDemo)', function (data) {
        var re = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!#$%^&*(){}:_]).{8,16}$/;
        var newPassword = data.field.newPassword;
        var confirmPassword = data.field.confirmPassword;
        if (!re.test(newPassword) || !re.test(confirmPassword)) {
            layeralert("Password shall includes Capital Letter, Small Letter, Sign and Numbers with length between 8 - 16 charactors!", { icon: 5 });
        }
        else if (newPassword != confirmPassword) {
            layeralert("2 password input is inconsistent！", { icon: 5 });
        } else {
            $.post("ChangePassword", data.field, function (data, status) {
                if (data.Code == 0) {
                    layeralert(data.Msg, { time: 2000 });
                    location.href = "../Home/Index";
                } else {
                    layeralert(data.Msg);
                }
            });
        }
        return false;
    });

    var htm = '<ul id="TANGRAM__PSP_3__pwdChecklist" >' +
        '<li id="rule1" style="color:#666"><i class="layui-icon" style="font-size: 10px; padding-right:5px;color: #FC4343;font-weight:bold;">&#x1006;</i>长度为8~16个字符</li>' +
        '<li id="rule6" style="color:#666"><i class="layui-icon" style="font-size: 10px; padding-right:5px;color: #008000;font-weight:bold;">&#xe63f;</i><span i18n="user.modify.confirmpwdprom">不允许有空格</span></li>' +
        '<li id="rule2" style="color:#666"><i class="layui-icon" style="font-size: 10px; padding-right:5px;color: #5BC92E;font-weight:bold;">&#xe605;</i>包含数字</li>' +
        '<li id="rule3" style="color:#666"><i class="layui-icon" style="font-size: 10px; padding-right:5px;color: #5BC92E;font-weight:bold;">&#xe605;</i>包含大写字母</li>' +
        '<li id="rule4" style="color:#666"><i class="layui-icon" style="font-size: 10px; padding-right:5px;color: #5BC92E;font-weight:bold;">&#xe605;</i>包含小写字母</li>' +
        '<li id="rule5" style="color:#666"><i class="layui-icon" style="font-size: 10px; padding-right:5px;color: #5BC92E;font-weight:bold;">&#xe605;</i>包含标点符号</li>' +

        '<li id="rule" style="color:#666"><i class="layui-icon" style="font-size: 15px; padding-right:5px;color: #FC4343;font-weight:bold;">&#x1006;</i>密码强度</li>' +
        '</ul>';

    $("#newPassword").focus(function () {
        layer.tips(htm, this, {
            tips: [2, '#F5F5DC'],
            time: 0,
            id:"aaaaaa",
            success: function () {
                checkvalues($("#newPassword").val());
                laodlanguage();
            }
        });

    });

    $("#newPassword").blur(function () {
        //layer.closeAll();
    });


    $("#newPassword").keyup(function () {
        var text = $("#newPassword").val();
        checkvalues(text);
    });


    $("#confirmPassword").focus(function () {
        $("#newPassword").val("hahahah");
        $("#TANGRAM__PSP_3__pwd_checklist_len").val("hahahah");
        $("#TANGRAM__PSP_3__pwd_checklist_len").html("hahahah");
        $("#aaa").html("hahahah");
    });

});

function checkvalues(value) {
    var lv = 0;
    if (value.match(/([\s])/g)) {  $("#rule1").html("包含空格"); }
    if (value.match(/([a-z])/g)) { lv++; console.log("小字母"); $("#rule1").html("rule1"); }
    if (value.match(/([A-Z])/g)) { lv++; console.log("大字母"); }
    if (value.match(/([0-9])/g)) { lv++; console.log("数字"); }
    if (value.match(/([~!@#$%^&*()_+{}|:"<>?=;',./\\\[\]\-])/g)) { lv++; console.log("符号"); }
    if (value.length < 6) { lv = 0; }
    if (lv > 3) { lv = 3; }
}