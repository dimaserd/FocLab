class Logger {

    static Resourcses: Logger_Resx;

    public static SetResources() {
        Logger.Resourcses = new Logger_Resx();
    }

    public static LogException(exception: JQuery.Ajax.ErrorTextStatus, link: string) : void {
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
        }

    public static LogAction(message: string, description: string, groupName: string) : void {

            const data = {
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
                success: response => {
                    console.log(Logger.Resourcses.ActionLogged, response);
                },

                error: () => {
                    alert(Logger.Resourcses.LoggingAttempFailed);
                }
            });
    }
}

//Устанавливаем ресурсы
Logger.SetResources();

class Logger_Resx {
    LoggingAttempFailed: string = "Произошла ошибка в логгировании ошибки, срочно обратитесь к разработчикам приложения";

    ErrorOnApiRequest: string = "Ошибка запроса к апи";

    ActionLogged: string = "Action logged";

    ExceptionLogged: string = "Исключение залоггировано";

    ErrorOccuredOnLoggingException: string = "Произошла ошибка в логгировании ошибки, срочно обратитесь к разработчикам приложения";
}