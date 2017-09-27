//轮播
layui.use('carousel', function () {
    var carousel = layui.carousel;
    carousel.render({
        elem: '#indexcarouselimage'
        , width: '100%'
        , height: '750px'
        , interval: 5000
    });
});


layui.use('form', function () {
    var form = layui.form;

    form.on('submit(btnLogin)', function (data) {
        //layer.msg(JSON.stringify(data.field));
        $.post("Login",
            { txtMember: data.field.txtMember, txtPassword: data.field.txtPassword },
            function (result) {
                if (result.Code === 0)
                    window.location.href = "../Home/Index";
                else
                    layer.msg(result.Msg);
            });
        return false;
    });

});

