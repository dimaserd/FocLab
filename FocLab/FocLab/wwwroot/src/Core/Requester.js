var AjaxParameters = /** @class */ (function () {
    function AjaxParameters() {
    }
    return AjaxParameters;
}());
var Requester = /** @class */ (function () {
    function Requester() {
    }
    Requester.SendPostRequestWithAnimation = function (link, data, onSuccessFunc, onErrorFunc) {
        //Показываю крутилку
        ModalWorker.ShowModal("loadingModal");
        Requester.SendAjaxPost(link, data, onSuccessFunc, onErrorFunc, true);
    };
    Requester.UploadFilesToServer = function (inputId, onSuccessFunc, onErrorFunc) {
        var link = "/Api/FilesDirectory/UploadFiles";
        if (Requester.IsRequestGoing(link)) {
            return;
        }
        var file_data = $("#" + inputId).prop("files");
        var form_data = new FormData();
        if (file_data.length === 0) {
            ToastrWorker.HandleBaseApiResponse(new BaseApiResponse(false, "Файлы не выбраны"));
            return;
        }
        for (var i = 0; i < file_data.length; i++) {
            form_data.append("Files", file_data[i]);
        }
        $.ajax({
            url: link,
            type: "POST",
            data: form_data,
            async: true,
            cache: false,
            dataType: "json",
            contentType: false,
            processData: false,
            success: function (response) {
                console.log(response);
                Requester.DeleteCompletedRequest(link);
                if (onSuccessFunc) {
                    onSuccessFunc(response);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(jqXHR, textStatus, errorThrown);
                //Логгирую ошибку
                Logger.LogException(textStatus, link);
                Requester.DeleteCompletedRequest(link);
                ModalWorker.HideModals();
                var resp = new BaseApiResponse(false, "Произошла ошибка! Мы уже знаем о ней, скоро с ней разберемся!");
                ToastrWorker.HandleBaseApiResponse(resp);
                //Вызываю внешнюю функцию-обработчик
                if (onErrorFunc) {
                    onErrorFunc(jqXHR, textStatus, errorThrown);
                }
            }
        });
    };
    Requester.SendAjaxGet = function (link, data, onSuccessFunc, onErrorFunc) {
        if (Requester.IsRequestGoing(link)) {
            return;
        }
        var params = {
            type: "GET",
            data: data,
            url: link,
            async: true,
            cache: false,
            success: function (response) {
                Requester.DeleteCompletedRequest(link);
                if (onSuccessFunc) {
                    onSuccessFunc(response);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //Логгирую ошибку
                Logger.LogException(textStatus, link);
                Requester.DeleteCompletedRequest(link);
                //Вызываю внешнюю функцию-обработчик
                if (onErrorFunc) {
                    onErrorFunc(jqXHR, textStatus, errorThrown);
                }
            }
        };
        $.ajax(params);
    };
    Requester.SendAjaxPost = function (link, data, onSuccessFunc, onErrorFunc, animations) {
        if (Requester.IsRequestGoing(link)) {
            return;
        }
        if (data == null) {
            alert("Вы подали пустой объект в запрос");
            return;
        }
        var params = {};
        params.type = "POST";
        params.data = data;
        params.url = link;
        params.async = true;
        params.cache = false;
        params.success = function (response) {
            Requester.DeleteCompletedRequest(link);
            if (animations) {
                Requester.OnSuccessAnimationHandler(response);
            }
            if (onSuccessFunc) {
                onSuccessFunc(response);
            }
        };
        params.error = function (jqXHR, textStatus, errorThrown) {
            //Логгирую ошибку
            Logger.LogException(textStatus, link);
            Requester.DeleteCompletedRequest(link);
            if (animations) {
                Requester.OnErrorAnimationHandler();
            }
            //Вызываю внешнюю функцию-обработчик
            if (onErrorFunc) {
                onErrorFunc(jqXHR, textStatus, errorThrown);
            }
        };
        var isArray = data.constructor === Array;
        if (isArray) {
            params.contentType = "application/json; charset=utf-8";
            params.dataType = "json";
            params.data = JSON.stringify(data);
        }
        Requester.GoingRequests.push(link);
        $.ajax(params);
        console.log("POST \u0437\u0430\u043F\u0440\u043E\u0441 " + link, data);
    };
    Requester.GoingRequests = new Array();
    Requester.DeleteCompletedRequest = function (link) {
        Requester.GoingRequests = Requester.GoingRequests.filter(function (x) { return x !== link; });
    };
    Requester.ParseDate = function (date) {
        date = date.replace(new RegExp("/", 'g'), ".");
        var from = date.split(".");
        var d = new Date(+from[2], +from[1] - 1, +from[0]);
        return d.toISOString();
    };
    Requester.GetCombinedData = function (prefix, obj) {
        var resultObj = {};
        for (var prop in obj) {
            if (Array.isArray(obj[prop])) {
                resultObj[prefix + prop] = obj[prop];
            }
            else if (typeof obj[prop] == "object") {
                var objWithProps = Requester.GetCombinedData("" + prefix + prop + ".", obj[prop]);
                for (var innerProp in objWithProps) {
                    resultObj[innerProp] = objWithProps[innerProp];
                }
            }
            else {
                resultObj[prefix + prop] = obj[prop];
            }
        }
        return resultObj;
    };
    Requester.GetParams = function (obj) {
        obj = Requester.GetCombinedData("", obj);
        return $.param(obj, true);
    };
    Requester.IsRequestGoing = function (link) {
        var any = Requester.GoingRequests.filter(function (x) { return x === link; });
        return any.length > 0;
    };
    Requester.OnSuccessAnimationHandler = function (data) {
        ModalWorker.HideModals();
        ToastrWorker.HandleBaseApiResponse(data);
    };
    Requester.OnErrorAnimationHandler = function () {
        ModalWorker.HideModals();
        var resp = new BaseApiResponse(false, "Произошла ошибка! Мы уже знаем о ней, и скоро с ней разберемся!");
        ToastrWorker.HandleBaseApiResponse(resp);
    };
    return Requester;
}());
