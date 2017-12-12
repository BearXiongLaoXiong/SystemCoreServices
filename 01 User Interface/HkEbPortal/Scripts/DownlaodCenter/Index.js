layui.use(['form'], function () {
    $("#linkDownCenter").addClass("navbarCheckIn");
    var form = layui.form;

    ShowLoading();
    var memeKy = getUrlParam("memeKy");
    //console.log("memeKy = " + memeKy);
    //laodview('', (memeKy !== null && memeKy !== "") ? memeKy : "");

    //form.on('radio', function (data) {
    //    console.log(data.elem); //得到radio原始DOM对象
    //    console.log(data.value); //被点击的radio的value值
    //    laodview(data.value, "");
    //});
    CloseDelayLoading();
});
