var GenericUtil = (function () {
    function GenericUtil() {
    }
    GenericUtil.GenericUpdateFileByType = function (fileType, link, preData) {
        var fileInputId = fileType;
        var file_data = $("#" + fileInputId).prop('files');
        if (file_data == null || file_data.length == 0) {
            CrocoAppCore.ToastrWorker.ShowError("Файл для загрузки не выбран");
            return;
        }
        CrocoAppCore.Application.Requester.UploadFilesToServer(fileInputId, "/Api/FilesDirectory/UploadFiles", function (t) {
            if (t.IsSucceeded) {
                var data = preData;
                preData["FileId"] = t.ResponseObject[0];
                preData["FileType"] = fileType;
                CrocoAppCore.Application.Requester.SendPostRequestWithAnimation(link, data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
            }
            else {
                CrocoAppCore.ToastrWorker.ShowError(t.Message);
            }
        }, null);
    };
    return GenericUtil;
}());
