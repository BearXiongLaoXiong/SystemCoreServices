layui.use('carousel', function () {
    var carousel = layui.carousel;
    carousel.render({
        elem: '#indexcarouselimage'
        , width: '100%'
        , height: '800px'
        , interval: 500000
    });
});