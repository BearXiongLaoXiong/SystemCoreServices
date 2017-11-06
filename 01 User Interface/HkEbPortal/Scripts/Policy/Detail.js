layui.use(['form', 'element'], function () {
    $("#linkPolicy").addClass("navbarCheckIn");

    var form = layui.form;
    var element = layui.element;

    ShowLoading();

    form.on('submit(submitDetailRadio)', function (data) {
        var array = new Array();
        for (var key in data.field) {
            if (data.field.hasOwnProperty(key) && data.field[key].length > 0) {
                try {
                    var json = JSON.parse(data.field[key]);
                    array.push(json);
                }
                catch (e) {//
                }
            }
        }
        ShowLoading();
        $.post("Detail", { data: JSON.stringify(array), r: Math.random(), __RequestVerificationToken: $('input[name="__RequestVerificationToken"]', data.form).val() }, function (dat, status) {
            CloseLoading();
            if (dat.Code === "0") {
                layeralert1(il8nMessage("policy.detail.SelectionCompleted"), function () {
                    window.location.href = "../Policy/index";
                });
            } else {
                layeralert(dat);
            }
        });
        return false;
    });

    CloseDelayLoading();
});


function showInformation(pdpdId) {
    layer.open({
        type: 2,
        title: 'Information',
        skin: 'layui-layer-demo', //样式类名
        area: ['800px', '600px'],
        closeBtn: 1,
        shadeClose: true, //开启遮罩关闭
        content: 'Information?pdpdId=' + pdpdId,
        btnAlign: 'c'
    });
}


