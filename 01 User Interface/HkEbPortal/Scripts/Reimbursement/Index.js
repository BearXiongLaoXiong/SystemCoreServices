layui.use(['upload', 'form', 'layer'], function () {

    $("#linkReim").addClass("navbarCheckIn");

    var form = layui.form,
        upload = layui.upload,
        layer = layui.layer;

    ShowLoading();

    
    CloseDelayLoading();
});

function onDelete(id) {
    layer.confirm('Confirm to delete this entry?', {
        btn: ['Confirm', 'No'],
        title: ' ',
        skin: 'layui-layer-lan',
        closeBtn: 1
    }, function () {
        ShowLoading();
        $.post("Delete", { id: id, r: Math.random(), __RequestVerificationToken: $('input[name="__RequestVerificationToken"]', $("#fsaform")).val() }, function (result) {
            CloseLoading();
            layeralert1(result, function () { location.reload(); });
        });
    });
    return false;
}

function onSubmit(id) {
    layer.confirm('Confirm to submit this entry?', {
        btn: ['Confirm', 'No'],
        title: ' ',
        skin: 'layui-layer-lan',
        closeBtn: 1
    }, function () {
        ShowLoading();
        $.post("EditFsaClaimStatus", { id: id, r: Math.random(), __RequestVerificationToken: $('input[name="__RequestVerificationToken"]', data.form).val() }, function (dat, result) {
            CloseLoading();
            if (dat.Code === 0)
                layeralert1(il8nMessage("Reimbursement.Index.SubmitResult"), function () { location.reload(); });
            else
                layeralert1(dat.Msg, function () { location.reload(); });
        });
    });
    return false;
}

function onDeleteTips(val) {
    layer.tips(il8nMessage("pop-up.fsa.index.DeleteTips"), val, {
        tips: [4, '#78BA32']
    });
}

function onSubmitTips(val) {
    layer.tips(il8nMessage("pop-up.fsa.index.SubmitTips"), val, {
        tips: [4, '#78BA32']
    });
}