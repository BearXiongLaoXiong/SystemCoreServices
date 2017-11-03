
layui.use(['carousel', 'laydate'], function () {
    var laydate = layui.laydate,
        carousel = layui.carousel;

    $("#linkHome").addClass("navbarCheckIn");

    //$("#txtPolicyUp").val("30000272");
    //$("#txtMemberUp").val("000000001995");
    //轮播
    laydate.render({
        elem: '#txtDate',
        format: 'dd/MM/yyyy',
        lang: 'en'
    });

    carousel.render({
        elem: '#indexcarouselimage'
        , width: '100%'
        , height: '750px'
        , interval: 5000
    });
});
