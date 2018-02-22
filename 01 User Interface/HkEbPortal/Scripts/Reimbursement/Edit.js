layui.use(['form', 'layedit', 'laydate', 'element', 'upload'], function () {
    $("#linkReim").addClass("navbarCheckIn");

    var form = layui.form,
        layer = layui.layer,
        layedit = layui.layedit,
        laydate = layui.laydate,
        upload = layui.upload;

    //执行一个laydate实例
    laydate.render({
        elem: '#CLIV_Date', //指定元素
        format: 'dd/MM/yyyy',
        lang: 'en'
    });

    form.verify({
        required: function (value) {
            if (value.trim().length < 1) {
                return il8nMessage("public.common.VerifyRequired");
            }
        }
    });

    form.on('submit(btnEdit)', function (data) {

        ShowLoading();
        $.post("Edit", data.field, function (dat, status) {
            CloseLoading();

            if (dat.Code === 0) {
                window.location.href = "../Reimbursement/Index";
            } else {
                layeralert(dat.Msg);
            }
        });
        return false;
    });

    upload.render({
        elem: '#test10',
        url: 'UploadImg',
        accept: 'images',
        exts: 'jpg|png|bmp|jpeg',
        size: 5120,
        data: { id: getUrlParam("id"), r: Math.random(), __RequestVerificationToken: $('input[name="__RequestVerificationToken"]', $("#FsaEditForm")).val() },
        multiple: false,
        before: function (obj) { //obj参数包含的信息，跟 choose回调完全一致，可参见上文。
            ShowLoading(); //上传loading
            obj.preview(function (index, file, result) {

                //$('#demo2').append('<img src="' + result + '" alt="' + file.name + '" class="layui-upload-img">')
                $('#demo2').append('<div class="layui-upload-img"><img id="ivImage" src="' + result + '" /><p><i class="layui-icon">&#xe640;</i></p></div>');
            });
        },
        done: function (res, index, upload) {
            CloseLoading(); //关闭loading
            console.log(res);
            if (res.result === 0) {
                layeralert(res.message);
                $("#ivImage").on('click',
                    function () {
                        showimg(res.filename);
                    });
            } else {
                layeralert(res.filename + " ---" + res.message);
            }
            console.log(res);
        },
        error: function (index, upload) {
            CloseLoading(); //关闭loading
            layeralert("server error！");
        }
    });
});

function check() {
    return false;
}


function showimg(img) {
    layer.open({
        type: 1,
        title: false,
        closeBtn: 0,
        area: ['500px','500px'],
        skin: 'layui-layer-nobg', //没有背景色
        shadeClose: true,
        content: '<img src="' + img + '" />'
        //content: $('#tong')
    });
}


function onInsufficientTips(val) {
    layer.tips(il8nMessage("pop-up.public.Insufficient"), val, {
        tips: [4, '#78BA32']
    });
}
