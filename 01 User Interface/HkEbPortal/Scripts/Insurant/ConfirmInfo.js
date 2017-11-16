layui.use(['form', 'element'], function () {
    var form = layui.form,
        element = layui.element;
    
    reloadIsConfirm();
});

function reloadIsConfirm() {
    $.get("FindUserIsConfirmMemberInfo", { r: Math.random() }, function (data, status) {
        if (status === "success") {
            if (data === "0") { confirmEmployeeInfo(); }
            else if (data === "1") { confirmFamily(); }
            else if (data === "2") { confirmBenefit(); }
            else {
                $("#employeeInfo").addClass("layui-this");

                $("#employeeInfo").removeClass("layui-disabled");
                $("#family").removeClass("layui-disabled");
                $("#benefit").removeClass("layui-disabled");
                $("#pointsRecordes").removeClass("layui-disabled");
                layui.element.tabChange('memberInfoTable', "employeeInfo");
            }
        }
    });

    //element.on('tab(memberInfoTable)', function (data) {
    //    console.log(data);
    //    //element.tabChange('memberInfoTable', "222"); 
    //    return false;
    //});
}


function confirmEmployeeInfo() {
    $("#employeeInfo").addClass("layui-this");
    $("#family").addClass("layui-disabled");
    $("#benefit").addClass("layui-disabled");
    $("#pointsRecordes").addClass("layui-disabled");

    $("#employeeInfo").removeClass("layui-disabled");
    $("#family").removeClass("layui-this");
    $("#benefit").removeClass("layui-this");
    layui.element.tabChange('memberInfoTable', "employeeInfo");

    layer.confirm(il8nMessage("pop-up.Insurant.index.confirmEmployee"), {
        btn: [il8nMessage("public.common.Confirmed"), il8nMessage("public.common.required")], //按钮
        title: ' ',
        offset: '190px',
        closeBtn: 0,
        skin: 'layui-layer-lan'
        //shade: false
    }, function () {
        ShowLoading();
        $.post("EditUserIsConfirmMemberInfo/1", { r: Math.random(), __RequestVerificationToken: $('input[name="__RequestVerificationToken"]', $("#memberForm")).val() }, function (data, status) {
            CloseLoading();
            if (data !== "success") { layeralert("err"); }
            reloadIsConfirm();
        });

    }, function () {
        layeralert1(il8nMessage("pop-up.Insurant.index.confirmRequired"), function () { window.location.href = "../User/Logout"; });
    });
}

function confirmFamily() {
    $("#employeeInfo").addClass("layui-disabled");
    $("#family").addClass("layui-this");
    $("#benefit").addClass("layui-disabled");
    $("#pointsRecordes").addClass("layui-disabled");

    $("#employeeInfo").removeClass("layui-this");
    $("#family").removeClass("layui-disabled");
    $("#benefit").removeClass("layui-this");
    layui.element.tabChange('memberInfoTable', "family");

    layer.confirm(il8nMessage("pop-up.Insurant.index.confirmFamily"), {
        btn: [il8nMessage("public.common.Confirmed"), il8nMessage("public.common.required")], //按钮
        title: ' ',
        offset: '190px',
        closeBtn: 0,
        skin: 'layui-layer-lan',
        //shade: false
    }, function () {
        ShowLoading();
        $.post("EditUserIsConfirmMemberInfo/2", { r: Math.random(), __RequestVerificationToken: $('input[name="__RequestVerificationToken"]', $("#memberForm")).val() }, function (data, status) {
            CloseLoading();
            if (data !== "success") { layeralert("err"); }
            reloadIsConfirm();
        });

    }, function () {
        layeralert1(il8nMessage("pop-up.Insurant.index.confirmRequired"), function () { window.location.href = "../User/Logout"; });
    });
}

function confirmBenefit() {
    $("#employeeInfo").addClass("layui-disabled");
    $("#family").addClass("layui-disabled");
    $("#benefit").addClass("layui-this");
    $("#pointsRecordes").addClass("layui-disabled");

    $("#employeeInfo").removeClass("layui-this");
    $("#family").removeClass("layui-this");
    $("#benefit").removeClass("layui-disabled");
    layui.element.tabChange('memberInfoTable', "benefit");

    layer.confirm(il8nMessage("pop-up.Insurant.index.confirmBenefit"), {
        btn: [il8nMessage("public.common.Confirmed"), il8nMessage("public.common.required")], //按钮
        title: ' ',
        offset: '190px',
        closeBtn: 0,
        skin: 'layui-layer-lan',
        //shade: false
    }, function () {
        ShowLoading();
        $.post("EditUserIsConfirmMemberInfo/Y", { r: Math.random(), __RequestVerificationToken: $('input[name="__RequestVerificationToken"]', $("#memberForm")).val() }, function (data, status) {
            CloseLoading();
            if (data !== "success") { layeralert("err");
                return;
            }
            window.location.href = "../Home/Index";
           // location.reload();
        });

    }, function () {
        layeralert1(il8nMessage("pop-up.Insurant.index.confirmRequired"), function () { window.location.href = "../User/Logout"; });
    });
}