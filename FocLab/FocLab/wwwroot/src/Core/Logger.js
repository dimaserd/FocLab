var Logger = (function () {
    function Logger() {
    }
    Logger.SetResources = function () {
        Logger.Resourcses = new Logger_Resx();
    };
    Logger.LogException = function (exception, link) {
        $.ajax({
            type: "POST",
            data: {
                ExceptionDate: new Date().toISOString(),
                Description: Logger.Resourcses.ErrorOnApiRequest,
                Message: exception,
                Uri: link !== null ? link : location.href
            },
            url: "/Api/Log/Exception",
            async: true,
            cache: false,
            success: function (data) {
                console.log(Logger.Resourcses.ExceptionLogged, data);
            },
            error: function () {
                alert(Logger.Resourcses.ErrorOccuredOnLoggingException);
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
                console.log(Logger.Resourcses.ActionLogged, response);
            },
            error: function () {
                alert(Logger.Resourcses.LoggingAttempFailed);
            }
        });
    };
    return Logger;
}());
Logger.SetResources();
var Logger_Resx = (function () {
    function Logger_Resx() {
        this.LoggingAttempFailed = "Произошла ошибка в логгировании ошибки, срочно обратитесь к разработчикам приложения";
        this.ErrorOnApiRequest = "Ошибка запроса к апи";
        this.ActionLogged = "Action logged";
        this.ExceptionLogged = "Исключение залоггировано";
        this.ErrorOccuredOnLoggingException = "Произошла ошибка в логгировании ошибки, срочно обратитесь к разработчикам приложения";
    }
    return Logger_Resx;
}());
