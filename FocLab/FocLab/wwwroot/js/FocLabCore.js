var DefaultHandlers = /** @class */ (function () {
    function DefaultHandlers() {
    }
    DefaultHandlers.IfSuccessReloadPageAfter1500MSecs = function (x) {
        if (x.IsSucceeded) {
            setTimeout(function () { return location.reload(); }, 1500);
        }
    };
    DefaultHandlers.NoHandler = function (x) {
    };
    return DefaultHandlers;
}());

var EventSetter = /** @class */ (function () {
    function EventSetter() {
    }
    EventSetter.SetHandlerForClass = function (className, eventName, handlerFunction) {
        var classNameElems = document.getElementsByClassName(className);
        Array.from(classNameElems).forEach(function (x) { return x.addEventListener(eventName, handlerFunction, false); });
    };
    return EventSetter;
}());

var GenericUtil = /** @class */ (function () {
    function GenericUtil() {
    }
    GenericUtil.GenericUpdateFileByType = function (fileType, link, preData) {
        var fileInputId = fileType;
        var file_data = $("#" + fileInputId).prop('files');
        if (file_data == null || file_data.length == 0) {
            ToastrWorker.ShowError("Файл для загрузки не выбран");
            return;
        }
        Requester.UploadFilesToServer(fileInputId, function (x) {
            var t = x;
            if (t.IsSucceeded) {
                var data = preData;
                preData["FileId"] = t.ResponseObject[0];
                preData["FileType"] = fileType;
                Requester.SendPostRequestWithAnimation(link, data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
            }
            else {
                ToastrWorker.ShowError(t.Message);
            }
        }, null);
    };
    return GenericUtil;
}());