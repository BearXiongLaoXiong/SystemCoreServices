﻿//<table class="layui-table" lay-data="{ height:500, url:'FindView',id: 'idTest' }" id="policyInfoTable" lay-filter="policyInfo">
//    <thead>
//    <tr>
//    <th lay-data="{field:'ID', width:50, fixed: true}"></th>
//    <th class="cacaca" lay-data="{field:'MEME_NAME', width:150, sort: true}"><span id="aaa">$.Policy.index.info</span></th>
//    <th lay-data="{field:'PLPL_DESC', width:400}">保单名称</th>
//    <th lay-data="{field:'SYSV_PLPL_STS', width:80, templet:'#statusTpl'}">状态</th>
//    <th lay-data="{fixed: 'right', width:160, align:'center', toolbar: '#barDemo'}"></th>
//    </tr>
//    </thead>
//    </table>

//    <script type="text/html" id="barDemo">
//    <a class="layui-btn layui-btn-primary layui-btn-mini" lay-event="detail">查看</a>
//    <a class="layui-btn layui-btn-mini" lay-event="edit">编辑</a>
//    <a class="layui-btn layui-btn-danger layui-btn-mini" lay-event="del">删除</a>
//    </script>

//@*执行状态模板 *@
//<script type="text/html" id="statusTpl">
//    {{# var status=d.SYSV_PLPL_STS }}
//{{# if (status=='O'){}}
//    <span class="layui-badge layui-bg-blue" style="font-size:13px">开放</span>
//        {{# } else{}}
//<span class="layui-badge" style="font-size:13px">有效</span>
//    {{# } }}
//</script>


layui.use(['layer', 'table'], function () {
    var table = layui.table;
    var layer = layui.layer;

    //--------------方法渲染TABLE----------------
    var tableIns = table.render({
        //elem: '#policyInfoTable',
        id: 'idTest',
        //height: 400,
        url: 'FindView',
        where: { status: 'O' },
        loading: true,
        //cols: [[
        //    { field: 'ID', width: 30, align: 'center',fixed: true },
        //    { field: 'MEME_NAME', title: '人员名称',  width: 150 },
        //    { field: 'PLPL_DESC', title: '保单名称', width: 400, align: 'left' },
        //    { field: 'SYSV_PLPL_STS', title: '状态', width: 80, align: 'center', templet: '#statusTpl' },
        //    { fixed: 'right', width: 160, align: 'center', toolbar: '#barDemo' }
        //]],
        //, response: {
        //    statusName: 'code' //数据状态的字段名称，默认：code
        //    , statusCode: 0 //成功的状态码，默认：0
        //    , msgName: 'msg' //状态信息的字段名称，默认：msg
        //    , countName: 'count' //数据总数的字段名称，默认：count
        //    , dataName: 'customer' //数据列表的字段名称，默认：data
        //}
        done: function (res, curr, count) {
            //如果是异步请求数据方式，res即为你接口返回的信息。
            //如果是直接赋值的方式，res即为：{data: [], count: 99} data为当前页数据、count为数据总长度
            //console.log(res);
            //得到当前页码
            //console.log(curr);
            alert(res.tag);
            $("#curPageIndex").val(curr);
            //得到数据总量
            //console.log(count);
        }
    });

    layui.use('form', function () {
        var form = layui.form;
        form.on('radio', function (data) {
            console.log(data.elem); //得到radio原始DOM对象
            console.log(data.value); //被点击的radio的value值
            tableIns.reload({
                where: { status: data.value }
            });
        });
    });

    //#region --------------搜索----------------
    //$("#search").click(function () {
    //    var strFilter = $("#filter").val();
    //    tableIns.reload({
    //        where: {
    //            filter: strFilter
    //        }
    //    });
    //});
    //#endregion

    //#endregion
    //工具条事件监听
    table.on('tool(policyInfo)', function (obj) {
        var data = obj.data;
        if (obj.event === 'detail') {
            layer.msg('ID：' + data.MEME_NAME + ' 的查看操作');
        } else if (obj.event === 'del') {
            layer.confirm('真的删除行么', function (index) {
                obj.del();
                layer.close(index);
            });
        } else if (obj.event === 'edit') {
            layer.alert('编辑行：<br>' + JSON.stringify(data));
        }
    });


});


//layui.use(['layer', 'table'], function () {
//    var table = layui.table;
//    var layer = layui.layer;
//    layui.laytpl.fn = function (value) {
//        //json日期格式转换为正常格式
//        var date = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
//        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
//        var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
//        return date.getFullYear() + "-" + month + "-" + day;
//    }
//    //--------------方法渲染TABLE----------------
//    var tableIns = table.render({
//        elem: '#customer'
//        , id: 'customer'
//        , url: '@Url.Action("CustomerList", "Customer")'
//        , cols: [[
//            { checkbox: true, LAY_CHECKED: false } //其它参数在此省略
//            , { field: 'rowId', title: '@Html.DisplayNameFor(a => a.rowId)', width: 100, align: 'center' }
//            , { field: 'customer_code', title: ' @Html.DisplayNameFor(a=>a.customer_code)', width: 150, align: 'center' }
//            , { field: 'company', title: '@Html.DisplayNameFor(a => a.company)', width: 300, align: 'center' }
//            , { field: 'descr', title: '@Html.DisplayNameFor(a => a.descr)', width: 200, align: 'center' }
//            , { field: 'address1', title: '@Html.DisplayNameFor(a => a.address1)', width: 300, align: 'center' }
//            , { field: 'contact', title: '@Html.DisplayNameFor(a => a.contact)', width: 100, align: 'center' }
//            , { field: 'status', title: '@Html.DisplayNameFor(a => a.status)', width: 100, align: 'center', templet: "#statusTpl" }
//            , { field: 'editwho', title: '@Html.DisplayNameFor(a => a.editwho)', width: 80, align: 'center' }
//            , { field: 'editdate', title: '@Html.DisplayNameFor(a => a.editdate)', width: 160, align: 'center', templet: "#dateTpl" }
//            , { fixed: 'right', width: 160, align: 'center', toolbar: '#bar' }
//        ]]
//        , page: true
//        , limits: [15, 20, 40, 60, 80]
//        , limit: 15 //默认采用10
//        , response: {
//            statusName: 'code' //数据状态的字段名称，默认：code
//            , statusCode: 0 //成功的状态码，默认：0
//            , msgName: 'msg' //状态信息的字段名称，默认：msg
//            , countName: 'count' //数据总数的字段名称，默认：count
//            , dataName: 'customer' //数据列表的字段名称，默认：data
//        }
//        , done: function (res, curr, count) {
//            //如果是异步请求数据方式，res即为你接口返回的信息。
//            //如果是直接赋值的方式，res即为：{data: [], count: 99} data为当前页数据、count为数据总长度
//            //console.log(res);
//            //得到当前页码
//            //console.log(curr);
//            $("#curPageIndex").val(curr);
//            //得到数据总量
//            //console.log(count);
//        }
//    });

//    //#region --------------搜索----------------
//    $("#search").click(function () {
//        var strFilter = $("#filter").val();
//        tableIns.reload({
//            where: {
//                filter: strFilter
//            }
//        });
//    });
//    //#endregion

//    //#region --------------批量删除----------------
//    $("#delete").click(function () {
//        var checkStatus = table.checkStatus('customer');
//        var count = checkStatus.data.length;//选中的行数
//        if (count > 0) {
//            parent.layer.confirm('确定要删除所选客户信息', { icon: 3 }, function (index) {
//                var data = checkStatus.data; //获取选中行的数据
//                var customer_code = '';
//                for (var i = 0; i < data.length; i++) {
//                    customer_code += data[i].customer_code + ",";
//                }
//                console.log(customer_code);
//                $.ajax({
//                    url: '@Url.Action("Delete","Customer")',
//                    type: "post",
//                    data: { customer_code: customer_code },
//                    dataType: "text",
//                    success: function (result) {
//                        if (result.length != 0) {
//                            if (result == "ok") {
//                                parent.layer.msg('删除成功', { icon: 1, shade: 0.4, time: 1000 })
//                                $("#search").click();//重载TABLE
//                                parent.layer.close(index);
//                            }
//                            else if (result == "error") {
//                                parent.layer.msg("删除失败", { icon: 5, shade: [0.4], time: 1000 });
//                            }
//                        }
//                    },
//                    error: function (error) {
//                        parent.layer.alert(error.responseText, { icon: 2, title: '提示' });
//                    }
//                })
//            });
//        }
//        else
//            parent.layer.msg("请至少选择一条数据", { icon: 5, shade: 0.4, time: 1000 });
//    });
//    //#endregion

//    //#endregion
//    //工具条事件监听
//    table.on('tool(customer)', function (obj) { //注：tool是工具条事件名，test是table原始容器的属性 lay-filter="对应的值"
//        var data = obj.data; //获得当前行数据
//        var layEvent = obj.event; //获得 lay-event 对应的值
//        var tr = obj.tr; //获得当前行 tr 的DOM对象
//        var customer_code = data.customer_code;
//        if (layEvent === 'edit') { //编辑
//            parent.layer.open({
//                type: 2,
//                title: '编辑客户信息',
//                shade: 0.4,  //阴影度
//                fix: false,
//                shadeClose: false,
//                maxmin: false,
//                area: ['1260px;', '645px;'],    //窗体大小（宽,高）
//                content: '@Url.Action("CustomerEdit","Customer")?customer_code=' + customer_code,
//                success: function (layero, index) {
//                    var body = layer.getChildFrame('body', index); //得到子页面层的BODY
//                    body.find('#hidValue').val(index); //将本层的窗口索引传给子页面层的hidValue中
//                },
//                end: function () {
//                    var handle_status = $("#handle_status").val();
//                    console.log(handle_status);
//                    if (handle_status == 'ok') {
//                        parent.layer.msg('修改成功', { icon: 1, shade: 0.4, time: 1000 });
//                        $("#search").click();
//                        $("#handle_status").val('');
//                    }
//                    else if (handle_status == "error") {
//                        parent.layer.msg('修改失败', { icon: 5, shade: 0.4, time: 1000 });
//                    }
//                }
//            });
//        }
//    });
//});