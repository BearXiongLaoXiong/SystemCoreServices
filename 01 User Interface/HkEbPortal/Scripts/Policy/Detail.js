layui.use('element', function () {
    var element = layui.element;

});


layui.use('form', function () {
    var form = layui.form;

    form.on('submit(formDemo)', function (data) {
        var array = new Array();
        for (var key in data.field) {
            if (data.field.hasOwnProperty(key) && data.field[key].length > 0) {
                array.push(JSON.parse(data.field[key]));
            }
        }
        
        $.post("Detail",
            {
                data: JSON.stringify(array)
            },
            function (msg) {
                alert(msg + "hhaha");
            });
        //layer.msg(JSON.stringify(data.field));
        return false;
    });

});