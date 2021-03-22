var Requester_Resx = (function () {
    function Requester_Resx() {
        this.YouPassedAnEmtpyArrayOfObjects = "Вы подали пустой объект в запрос";
        this.ErrorOccuredWeKnowAboutIt = "Произошла ошибка! Мы уже знаем о ней, и скоро с ней разберемся!";
        this.FilesNotSelected = "Файлы не выбраны";
    }
    return Requester_Resx;
}());
var Requester = (function () {
    function Requester() {
    }
    Requester.prototype.GetParams = function (data) {
        return $.param(data);
    };
    Requester.prototype.DeleteCompletedRequest = function (link) {
        Requester.GoingRequests = Requester.GoingRequests.filter(function (x) { return x !== link; });
    };
    Requester.prototype.SendPostRequestWithAnimation = function (link, data, onSuccessFunc, onErrorFunc) {
        this.SendAjaxPostInner(link, data, onSuccessFunc, onErrorFunc, true, true);
    };
    Requester.prototype.UploadFilesToServer = function (inputId, link, onSuccessFunc, onErrorFunc) {
        var _this = this;
        var file_data = $("#" + inputId).prop("files");
        var form_data = new FormData();
        if (file_data.length === 0) {
            CrocoAppCore.ToastrWorker.ShowError(Requester.Resources.FilesNotSelected);
            if (onErrorFunc) {
                onErrorFunc();
            }
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
            success: (function (response) {
                _this.DeleteCompletedRequest(link);
                if (onSuccessFunc) {
                    onSuccessFunc(response);
                }
            }).bind(this),
            error: (function (jqXHR, textStatus, errorThrown) {
                CrocoAppCore.Application.Logger.LogException(textStatus.toString(), "ErrorOnApiRequest", link);
                _this.DeleteCompletedRequest(link);
                CrocoAppCore.Application.ModalWorker.HideModals();
                CrocoAppCore.ToastrWorker.ShowError(Requester.Resources.ErrorOccuredWeKnowAboutIt);
                if (onErrorFunc) {
                    onErrorFunc(jqXHR, textStatus, errorThrown);
                }
            }).bind(this)
        });
    };
    Requester.OnSuccessAnimationHandler = function (data) {
        CrocoAppCore.Application.ModalWorker.HideModals();
        CrocoAppCore.ToastrWorker.HandleBaseApiResponse(data);
    };
    Requester.OnErrorAnimationHandler = function () {
        CrocoAppCore.Application.ModalWorker.HideModals();
        CrocoAppCore.ToastrWorker.ShowError(Requester.Resources.ErrorOccuredWeKnowAboutIt);
    };
    Requester.prototype.Get = function (link, data, onSuccessFunc, onErrorFunc) {
        var _this = this;
        CrocoAppCore.Application.FormDataUtils.ProccessAllDateTimePropertiesAsString(data);
        CrocoAppCore.Application.FormDataUtils.ProccessAllNumberPropertiesAsString(data);
        var params = {
            type: "GET",
            data: data,
            url: link,
            async: true,
            cache: false,
            success: (function (response) {
                _this.DeleteCompletedRequest(link);
                if (onSuccessFunc) {
                    onSuccessFunc(response);
                }
            }).bind(this),
            error: (function (jqXHR, textStatus, errorThrown) {
                CrocoAppCore.Application.Logger.LogException(textStatus.toString(), "Error on Api Request", link);
                _this.DeleteCompletedRequest(link);
                if (onErrorFunc) {
                    onErrorFunc(jqXHR, textStatus, errorThrown);
                }
            }).bind(this)
        };
        $.ajax(params);
    };
    Requester.prototype.SendAjaxPostInner = function (link, data, onSuccessFunc, onErrorFunc, animations, logOnError) {
        var _this = this;
        if (data == null) {
            data = {};
        }
        CrocoAppCore.Application.FormDataUtils.ProccessAllDateTimePropertiesAsString(data);
        CrocoAppCore.Application.FormDataUtils.ProccessAllNumberPropertiesAsString(data);
        var params = {};
        params.type = "POST";
        params.data = data;
        params.url = link;
        params.async = true;
        params.cache = false;
        params.success = (function (response) {
            _this.DeleteCompletedRequest(link);
            if (animations) {
                Requester.OnSuccessAnimationHandler(response);
            }
            if (onSuccessFunc) {
                onSuccessFunc(response);
            }
        }).bind(this);
        params.error = (function (jqXHR, textStatus, errorThrown) {
            if (logOnError) {
                CrocoAppCore.Application.Logger.LogException(textStatus.toString(), "Error on Api Request", link);
            }
            _this.DeleteCompletedRequest(link);
            if (animations) {
                Requester.OnErrorAnimationHandler();
            }
            if (onErrorFunc) {
                onErrorFunc(jqXHR, textStatus, errorThrown);
            }
        }).bind(this);
        params.contentType = "application/json; charset=utf-8";
        params.dataType = "json";
        params.data = JSON.stringify(data);
        Requester.GoingRequests.push(link);
        $.ajax(params);
    };
    Requester.prototype.Post = function (link, data, onSuccessFunc, onErrorFunc) {
        this.SendAjaxPostInner(link, data, onSuccessFunc, onErrorFunc, false, true);
    };
    Requester.Resources = new Requester_Resx();
    Requester.GoingRequests = new Array();
    return Requester;
}());
