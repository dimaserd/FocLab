var Requester = (function () {
    function Requester() {
    }
    Requester.SetResources = function () {
        Requester.Resources = new Requester_Resx();
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
    Requester.SendPostRequestWithAnimation = function (link, data, onSuccessFunc, onErrorFunc) {
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
            ToastrWorker.HandleBaseApiResponse(new BaseApiResponse(false, Requester.Resources.FilesNotSelected));
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
                Requester.DeleteCompletedRequest(link);
                if (onSuccessFunc) {
                    onSuccessFunc(response);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                Logger.LogException(textStatus, link);
                Requester.DeleteCompletedRequest(link);
                ModalWorker.HideModals();
                var resp = new BaseApiResponse(false, Requester.Resources.ErrorOccuredWeKnowAboutIt);
                ToastrWorker.HandleBaseApiResponse(resp);
                if (onErrorFunc) {
                    onErrorFunc(jqXHR, textStatus, errorThrown);
                }
            }
        });
    };
    Requester.IsRequestGoing = function (link) {
        var index = Requester.GoingRequests.indexOf(link);
        return index >= 0;
    };
    Requester.OnSuccessAnimationHandler = function (data) {
        ModalWorker.HideModals();
        ToastrWorker.HandleBaseApiResponse(data);
    };
    Requester.OnErrorAnimationHandler = function () {
        ModalWorker.HideModals();
        var resp = new BaseApiResponse(false, Requester.Resources.ErrorOccuredWeKnowAboutIt);
        ToastrWorker.HandleBaseApiResponse(resp);
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
                Logger.LogException(textStatus, link);
                Requester.DeleteCompletedRequest(link);
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
            alert(Requester.Resources.YouPassedAnEmtpyArrayOfObjects);
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
            Logger.LogException(textStatus, link);
            Requester.DeleteCompletedRequest(link);
            if (animations) {
                Requester.OnErrorAnimationHandler();
            }
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
    };
    Requester.GoingRequests = new Array();
    Requester.DeleteCompletedRequest = function (link) {
        Requester.GoingRequests = Requester.GoingRequests.filter(function (x) { return x !== link; });
    };
    Requester.GetParams = function (obj) {
        obj = Requester.GetCombinedData("", obj);
        return $.param(obj, true);
    };
    return Requester;
}());
var Requester_Resx = (function () {
    function Requester_Resx() {
        this.YouPassedAnEmtpyArrayOfObjects = "Вы подали пустой объект в запрос";
        this.ErrorOccuredWeKnowAboutIt = "Произошла ошибка! Мы уже знаем о ней, и скоро с ней разберемся!";
        this.FilesNotSelected = "Файлы не выбраны";
    }
    return Requester_Resx;
}());
