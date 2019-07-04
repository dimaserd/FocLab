class Logger {

    public static LogException = function (exception: JQuery.Ajax.ErrorTextStatus, link: string) {
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
        }

    public static LogAction = function (message: string, description: string, groupName: string) {

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
                success: function (response) {
                    console.log("Действие залоггировано", response);
                },

            error: function () {
                alert("Произошла ошибка в логгировании ошибки, срочно обратитесь к разработчикам приложения");
            }
        });
    }
}