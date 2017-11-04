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
});