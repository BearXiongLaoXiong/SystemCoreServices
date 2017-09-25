layui.use(['upload', 'form', 'layer'], function () {
    var $ = layui.jquery, upload = layui.upload, form = layui.form, layer = layui.layer;

    $("#AddId").on("click", function () {
        location.href = "../Reimbursement/Add";
    });

    // 檢查
    $("#UpdateId").on("click", function () {
        var val = $("input:radio[name='selectName']:checked").attr("data-clivKy");
        if (val == null) {
            layer.msg("请选择要操作的数据！");
        } else {
            location.href = "../Reimbursement/Edit?clivKy=" + val;
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
         location.href = "../Reimbursement/Upload";
    });
    
})



