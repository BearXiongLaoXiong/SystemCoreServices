function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    if (r !== null) return unescape(r[2]); return null; //返回参数值
}

function ShowLoading() {
    var index = layer.msg('loading.....', {
        width: '100px',
        icon: 16,
        shade: 0.2,
        time: 10000,
        anim: 5
    });
    return index;
}

function CloseLoading() {
    layer.closeAll();
}

function CloseDelayLoading() {
    setTimeout(function () { layer.closeAll() }, 500);
}

function layeralert1(msg, fun) {
    layer.alert(msg, { title: "Message", skin: 'layui-layer-lan', btn: 'OK', closeBtn: 0 }, function (index) { layer.close(index); fun(); });
}

function layeralert(msg) {
    layer.alert(msg, { title: "Message", skin: 'layui-layer-lan', btn: 'OK', closeBtn: 0, shadeClose: true });
}