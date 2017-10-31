layui.use(['upload', 'form', 'layer'], function () {

    var $ = layui.jquery, upload = layui.upload, form = layui.form, layer = layui.layer;

    $("#linkReim").addClass("navbarCheckIn");
    $("#AddId").on("click", function () {
        location.href = "../Reimbursement/Add";
    });

    // 檢查
    $("#UpdateId").on("click", function () {
        var val = $("input:radio[name='selectName']:checked").attr("data-clivKy");
        if (val == null) {
            layer.msg("please select data！");
        } else {
            location.href = "../Reimbursement/Edit?clivKy=" + val;
        }
    });


    $("#DeleteId").on("click", function () {
        var val = $("input:radio[name='selectName']:checked").attr("data-clivKy");
        if (val == null) {
            layer.msg("Please select the data to be deleted！");
        } else {
            layer.open({
                id: 'one',
                //shade: 0,
                type: 0,
                title: 'Delete',
                content: 'Confirm to delete this entry?',
                btn: ['Confirm ', 'No'],
                btnAlign: 'c',
                yes: function (i, l) {
                    $.ajax({
                        type: 'POST',
                        url: "../Reimbursement/Delete",
                        data: { "CLIV_KY": val },
                        dataType: "json",
                        success: function (data) {
                            location.reload();
                            layer.msg(data);
                        }
                    });
                }
            });
        }
    });

    $("#UploadId").on("click", function () {
        var val = $("input:radio[name='selectName']:checked").attr("data-clivKy");
        if (val == null) {
            layer.msg("Please select the data to be operated！");
        } else {
            location.href = "../Reimbursement/Upload?clivKy=" + val;
        }
    });

    $("#submitId").on("click", function () {
        var val = $("input:radio[name='selectName']:checked").attr("data-clivKy");
        if (val == null) {
            layer.msg("Please select the data to be operated！");
        } else {
            $.ajax({
                type:"POST",
                url: "../Reimbursement/UpdateCLIVSTS",
                data: { "CLIV_KY": val },
                dataType:"json",
                success: function (dat,status) {
                    layer.msg(dat);
                }
            });
        }
       
    });
    
})



