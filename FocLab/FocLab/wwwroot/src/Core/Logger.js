var Logger = /** @class */ (function () {
    function Logger() {
    }
    Logger.LogException = function (exception, link) {
        $.ajax({
            type: "POST",
            data: {
                ExceptionDate: new Date().toISOString(),
                Description: "Ошибка запроса к апи",
                Message: exception,
                Uri: link !== null ? link : location.href
            },
            url: "/Api/Log/Exception",
            async: true,
            cache: false,
            success: function (data) {
                console.log("Исключение залоггировано", data);
            },
            error: function () {
                alert("Произошла ошибка в логгировании ошибки, срочно обратитесь к разработчикам приложения");
            }
        });
    };
    Logger.LogAction = function (message, description, groupName) {
        var data = {
            LogDate: new Date().toISOString(),
            GroupName: groupName,
            Uri: window.location.href,
            Description: description,
            Message: message
        };
        console.log("Logger.LogAction", data);
        $.ajax({
            type: "POST",
            data: data,
            url: "/Api/Log/Action",
            async: true,
            cache: false,
            success: function (response) {
                console.log("Действие залоггировано", response);
            },
            error: function () {
                alert("Произошла ошибка в логгировании ошибки, срочно обратитесь к разработчикам приложения");
            }
        });
    };
    return Logger;
}());
