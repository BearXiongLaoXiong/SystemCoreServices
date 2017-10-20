layui.use(['upload', 'form', 'layer'], function () {

    var $ = layui.jquery, upload = layui.upload, form = layui.form, layer = layui.layer;

    $("#linkReim").addClass("navbarCheckIn");
    $("#AddId").on("click", function () {
        var plplky = $(this).attr("data-plplky");
        location.href = "../Reimbursement/Add?plpl_ky=" + plplky;
    });

    // 檢查
    $("#UpdateId").on("click", function () {
        var val = $("input:radio[name='selectName']:checked").attr("data-clivKy");
        var plplky = $("input:radio[name='selectName']:checked").attr("data-plplky");
        if (val == null) {
            layer.msg("请选择要操作的数据！");
        } else {
            location.href = "../Reimbursement/Edit?clivKy=" + val + "&plpl_ky=" + plplky;
        }
    });


    $("#DeleteId").on("click", function () {
        var val = $("input:radio[name='selectName']:checked").attr("data-clivKy");
        if (val == null) {
            layer.msg("请选择要删除的数据！");
        } else {
            layer.open({
                id: 'one',
                //shade: 0,
                type: 0,
                title: '删除提示',
                content: '您确定要删除这条数据吗?',
                btn: ['删除', '取消'],
                btnAlign: 'c',
                yes: function (i, l) {
                    $.ajax({
                        type: 'POST',
                        url: "/Reimbursement/Delete",
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
            layer.msg("请选择要操作的数据！");
        } else {
            location.href = "../Reimbursement/Upload?clivKy=" + val;
        }
    });

    $("#submitId").on("click", function () {
        var val = $("input:radio[name='selectName']:checked").attr("data-clivKy");
        if (val == null) {
            layer.msg("请选择要提交的数据！");
        } else {
            $.ajax({
                type:"POST",
                url: "/Reimbursement/UpdateCLIVSTS",
                data: { "CLIV_KY": val },
                dataType:"json",
                success: function (dat,status) {
                    layer.msg(dat);
                }
            });
        }
       
    });
    
})



