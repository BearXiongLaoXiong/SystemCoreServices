function laodlanguage() {
    /*默认语言*/
    var defaultLang = "en";
    var cookie = $.cookie("defaultLang");
    if (cookie != null && cookie !== "undefined") defaultLang = cookie;
    else {
        var date = new Date();//如果没有获取到默认语言,则设置默认en
        date.setTime(date.getTime() + (1 * 24 * 60 * 60 * 1000));
        $.cookie("defaultLang", "en", { path: '/', expires: date });
    }

    $("[i18n]").i18n({
        defaultLang: defaultLang,
        filePath: "../i18n/",
        filePrefix: "i18n_",
        fileSuffix: "",
        forever: true,
        callback: function () {
            console.log("language is " + defaultLang);
        }
    });
    InitializeLanguageComponent();
}


var enlanguageJson;
var chlanguageJson;
function InitializeLanguageComponent() {
    $.ajaxSettings.async = false;

    $.getJSON("../i18n/i18n_en.json",
        function (data) {
            enlanguageJson = data;
        });

    $.getJSON("../i18n/i18n_ch.json",
        function (data) {
            chlanguageJson = data;
        });

    $.ajaxSettings.async = true;
}

function il8nMessage(key) {
    var defaultLang = "en";
    var cookie = $.cookie("defaultLang");

    if (cookie != null && cookie !== "undefined") defaultLang = cookie;
    if (defaultLang === "ch")
        return chlanguageJson[key];
    else return enlanguageJson[key];
}



