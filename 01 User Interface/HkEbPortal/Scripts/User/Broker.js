layui.use(['form'], function () {
    $("#linkReim").addClass("navbarCheckIn");

    var form = layui.form,
        layer = layui.layer;

    form.on('select(ddlPolicyNo)', function (data) {
        $('#ddlMember option').not(":first").remove();
        form.render('select');

        var selectIndex = $("#ddlPolicyNo").val();
        if (selectIndex.length > 0) {
            $.get("FindMemberList", { id: data.value }, function (dat, status) {
                $.each(dat, function (index, value) {
                    $("#ddlMember").append("<option value=" + value.Value + ">" + value.Text.trim() + "</option>");
                });
                form.render('select');
            });
        }
    });

    form.verify({
        required: function (value) {
            if (value.trim().length < 1) {
                return il8nMessage("public.common.VerifyRequired");
            }
        }
    });

    form.on('submit(btnBrokerLogin)', function (data) {

        ShowLoading();
        $.post("BrokerReplaceToMember", { id: data.field.ddlMember, r: Math.random(), __RequestVerificationToken: $('input[name="__RequestVerificationToken"]', data.form).val() }, function (result, status) {
            CloseLoading();
            if (result.Code === 1) {
                if (result.Data.USUS_FIRST_ISACTIVE === '0') {
                    if (result.Data.USUS_INFO_IS_CONFIRM === 'Y') {
                        window.location.href = "../Insurant/Index";
                    } else {
                        window.location.href = "../Insurant/Index";
                    }
                }
                else if (result.Data.USUS_INFO_IS_CONFIRM === 'Y') {
                    window.location.href = "../Insurant/Index";
                }
                else {
                    window.location.href = "../Insurant/Index";
                }
            }
            else
                layeralert(il8nMessage(result.Msg));
        });

        return false;
    });

});