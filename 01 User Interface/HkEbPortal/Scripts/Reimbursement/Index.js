layui.use(['upload', 'form', 'layer'], function () {

    $("#linkReim").addClass("navbarCheckIn");

    var form = layui.form,
        upload = layui.upload,
        layer = layui.layer;

    ShowLoading();

    $("#btnEdit").on("click", function () {
        var id = $("input:radio[name='selectName']:checked").attr("data-clivKy");
        if (id == null) { layeralert("please select data！"); return false; }

        location.href = "../Reimbursement/Edit?id=" + id;
        return false;
    });


    form.on('submit(btnDelete)', function (data) {
        var sts = $("input:radio[name='selectName']:checked").attr("data-clivSts");
        if (sts == null) { layeralert("Please select the data to be deleted！"); return false; }

        if (sts !== "00" && sts !== "01") { layeralert("FSA Claim had been submitted,Can't delete！"); return false; }

        var id = $("input:radio[name='selectName']:checked").attr("data-clivKy");
        layer.confirm('Confirm to delete this entry?', {
            btn: ['Confirm', 'No'],
            title: ' ',
            skin: 'layui-layer-lan',
            closeBtn: 1
        }, function () {
            $.post("Delete", { id: id, r: Math.random(), __RequestVerificationToken: $('input[name="__RequestVerificationToken"]', data.form).val() }, function (result) {
                layeralert1(result, function () { location.reload(); });
            });
        });
        return false;
    });


    form.on('submit(btnSubmit)', function (data) {
        var sts = $("input:radio[name='selectName']:checked").attr("data-clivSts");

        if (sts == null) {
            layeralert("Please select the data to be operated！");
            return false;
        }

        if (sts !== "00" && sts !== "01") {
            layeralert("FSA Claim had been submitted,Can't submit！");
            return false;
        }

        var id = $("input:radio[name='selectName']:checked").attr("data-clivKy");
        ShowLoading();
        $.post("EditFsaClaimStatus", { id: id, r: Math.random(), __RequestVerificationToken: $('input[name="__RequestVerificationToken"]', data.form).val() }, function (result) {
            CloseLoading();
            layeralert1(result, function () { location.reload(); });
        });

        return false;
    });

    $("#UploadId").on("click", function () {
        var val = $("input:radio[name='selectName']:checked").attr("data-clivKy");
        if (val == null) {
            layeralert("Please select the data to be operated！");
        } else {
            location.href = "../Reimbursement/Upload?clivKy=" + val;
        }
    });

    CloseDelayLoading();
});