function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    if (r != null) return unescape(r[2]); return null; //返回参数值
}

function ShowMsg() {
    layer.msg('loading.....', {
        width: '100px',
        icon: 16,
        shade: 0.2,
        time: 10000,
        anim: 5
    });
}

function ShowLoading() {
    layer.msg('loading.....', {
        width: '100px',
        icon: 16,
        shade: 0.2,
        time: 10000,
        anim: 5
    });
}

function CloseLoading() {
    layer.closeAll();
}

function ShowOpen(context) {
    layer.open({
        type: 1
        , title: false //不显示标题栏
        , closeBtn: false
        , area: '300px;'
        , shade: 0.8
        , id: 'LAY_layuipro' //设定一个id，防止重复弹出
        , btn: ['火速修改', '残忍拒绝']
        , btnAlign: 'c'
        , moveType: 1 //拖拽模式，0或者1
        , content: context
        , success: function (layero) {
            var btn = layero.find('.layui-layer-btn');
            btn.find('.layui-layer-btn0').attr({ href: '../User/ModifyPwd', target: '_blank' });
            btn.find('.layui-layer-btn1').attr({ href: '../Home/Index', target: '_self' });
        }
    });
}

function CloseMsg() {
    setTimeout(function () { layer.closeAll() }, 300);
}

function layeralert1(msg, fun) {
    layer.alert(msg, { title: "Message", skin: 'layui-layer-lan', btn: 'OK', closeBtn: 0 }, function (index) { layer.close(index); fun();  });
}

function layeralert(msg) {
    layer.alert(msg, { title: "Message", skin: 'layui-layer-lan', btn: 'OK', closeBtn: 0 });
}