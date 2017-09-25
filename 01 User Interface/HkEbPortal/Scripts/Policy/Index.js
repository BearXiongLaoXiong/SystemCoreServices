layui.use('element', function () {
    var element = layui.element;

});


layui.use('form', function () {
    var form = layui.form;
    var memeKy = getUrlParam("memeKy");
    console.log("memeKy = " + memeKy);
    laodview('', (memeKy !== null && memeKy !== "") ? memeKy : "");

    form.on('radio', function (data) {
        console.log(data.elem); //得到radio原始DOM对象
        console.log(data.value); //被点击的radio的value值
        laodview(data.value, "");
    });

});


function laodview(status, memeKy) {
    layer.msg('laoding.....', {
        width: '100px',
        icon: 16,
        shade: 0.2,
        time: 10000,
        anim: 5
    });
    $.get("FindView",
        { status: status, memeKy: memeKy },
        function (data) {
            var index = 0;
            //追加人员名称
            $("#divMemberNames").empty();
            $.each(data.names, function (key, value) {
                //首次加载或者选择后btn class为选中状态
                var style = memeKy === "" && index === 0 ? "" : memeKy === value.MemeKy ? "" : "layui-btn-primary";
                $("#divMemberNames").append('<button name="btnMemberName" class="layui-btn ' + style + '" onclick="laodview(\'\',\'' + value.MemeKy + '\')">' + value.Name + '</button>');
                index++;
            });

            $("#memberTableBody").empty();
            $.each(data.table, function (key, value) {
                var html = "";
                html += '<tr>';
                html += '<td>' + (key + 1) + '</td>';
                html += '<td>' + value.MEME_NAME + '</td>';
                html += '<td>' + value.PLPL_DESC + '</td>';
                var statu = "";
                switch (value.SYSV_PLPL_STS) {
                    case "O": statu = '<span class="layui-badge layui-bg-orange">开放</span>'; break;
                    case "A": statu = '<span class="layui-badge layui-bg-green">有效</span>'; break;
                    case "T": statu = '<span class="layui-badge">停止</span>'; break;
                    default: statu = '<span class="layui-badge layui-bg-blue">全部</span>';; break;
                }
                html += '<td>' + statu + '</td>';
                html += '<td style="padding: 0px;" align="center" data-off="true"><div class="layui-table-cell laytable-cell-1-4"> <a class="layui-btn layui-btn-primary layui-btn-mini" lay-event="detail" i18n="public.common.detail">查看</a> <a class="layui-btn layui-btn-danger layui-btn-mini" lay-event="del" i18n="public.common.edit" href="../Policy/Detail?plplKy=' + value.PLPL_KY + '&memeKy=' + value.MEME_KY + '">编辑</a> </div></td>';
                html += '</tr>';
                $("#memberTableBody").append(html);
            });
            setTimeout(function () { layer.closeAll() }, 300);
        });
}


function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    if (r !== null) return unescape(r[2]); return null; //返回参数值
}