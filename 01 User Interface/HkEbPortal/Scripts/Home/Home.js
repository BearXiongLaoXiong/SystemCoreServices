//轮播
layui.use('carousel', function () {
    var carousel = layui.carousel;
    carousel.render({
        elem: '#indexcarouselimage'
        , width: '100%'
        , height: '800px'
        , interval: 500000
    });
});


layui.use('form', function () {
    var form = layui.form;

    form.on('submit(aadfohasfsadf)', function (data) {
        layer.msg(JSON.stringify(data.field));
        return false;
    });

    form.on('submit(formDemo)', function (data) {
        layer.msg(JSON.stringify(data.field));
        return false;
    });
});

