function laodlanguage() {
    /*默认语言*/
    var defaultLang = "en";
    var cookie = $.cookie("defaultLang");
    if (cookie != null && cookie !== "undefined") defaultLang = cookie;

    $("[i18n]").i18n({
        defaultLang: defaultLang,
        filePath: "../i18n/",
        filePrefix: "i18n_",
        fileSuffix: "",
        forever: true,
        callback: function () {
            console.log("i18n has been completed.");
        }
    });

}


var languageJson;
function InitializeLanguageComponent() {

    /*默认语言*/
    var defaultLang = "en";
    var cookie = $.cookie("defaultLang");
    if (cookie != null && cookie !== "undefined") defaultLang = cookie;

    $.getJSON("../i18n/i18n_" + defaultLang + ".json",
        function (data) {
            languageJson = data;
        });
    
}

//function MessageAlert(key) {
//        alert(languageJson[key]); //显示处理后的数据 
//}



