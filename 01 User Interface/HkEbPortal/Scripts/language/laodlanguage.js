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



