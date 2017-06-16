layui.use('laydate', function () {
    var laydate = layui.laydate;

    var start = {
        min: laydate.now()
      , max: '2099-06-16 23:59:59'
      , istoday: false
      , choose: function (datas) {
          end.min = datas; //开始日选好后，重置结束日的最小日期
          end.start = datas //将结束日的初始值设定为开始日
      }
    };

    var end = {
        min: laydate.now()
      , max: '2099-06-16 23:59:59'
      , istoday: false
      , choose: function (datas) {
          start.max = datas; //结束日选好后，重置开始日的最大日期
      }
    };

    document.getElementById('dateStartId').onclick = function () {
        start.elem = this;
        laydate(start);
    }
    document.getElementById('dateEndId').onclick = function () {
        end.elem = this
        laydate(end);
    }

});


$(function () {
    // 加载同步内容数据
    $.get("/SystemSetting/GetSyschronContext", function (result) {

        var res = result.dic;

        for (var i = 0; i < res; i++) {

        }
    });

    // 加载目标数据源
    $.get("/SystemSetting/GetTargetData", function (result) {

    });

    $("#SynchronSearchId").on("click", function () {
        var index = layer.load(0, { time: 30000 });

        alert("你查询什么？");

        $.ajax({
            url: "",
            type: "",
            data: { "": "", "": "" },
            success: function (data) {

            }
        });
        layer.close(index);
    });

    $("#SynchronId").on("click", function () {
        var index = layer.load(0, { time: 30000 });
        alert("你同步了");
        layer.close(index);
    });

});