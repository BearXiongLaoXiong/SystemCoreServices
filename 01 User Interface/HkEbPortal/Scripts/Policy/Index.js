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
                html += '<td>' + value.PDCT_NAME + '</td>';
                html += '<td>' + value.PLAN_DESC + '</td>';
                html += '<td>' + value.PLME_AMT + '</td>';
                
                var defaultInd = "";
                switch (value.DEFAULT_IND) {
                    case "Y": defaultInd = '<span class="layui-badge layui-bg-orange" i18n="public.common.default">默认</span>'; break;
                    default: defaultInd = '<span class="layui-badge layui-bg-blue">x</span>';; break;
                }
                html += '<td>' + defaultInd + '</td>';
                html += '<td>' + value.LEVEL_IND + '</td>';
                html += '<td style="padding: 0px;" align="center" data-off="true"> <a class="layui-btn layui-btn-danger layui-btn-mini" lay-event="del" i18n="public.common.select" href="../Policy/Detail?plplKy=' + value.PLPL_KY + '&memeKy=' + value.MEME_KY + '&pdctId=' + value.PDCT_ID + '">选择</a> </div></td>';
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