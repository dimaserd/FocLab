var TaskUpdater = (function () {
    function TaskUpdater(taskId) {
        this.TaskId = taskId;
    }
    TaskUpdater.prototype.UploadUserFile = function (fileType) {
        var file_data = $('#' + fileType).prop('files');
        if (file_data == null || file_data.length == 0) {
            CrocoAppCore.ToastrWorker.ShowError("Файл для загрузки не выбран");
            return;
        }
        CrocoAppCore.Application.Requester.UploadFilesToServer(fileType, "/Api/FilesDirectory/UploadFiles", function (x) {
            var fileId = x.ResponseObject[0];
            var data = {};
            CrocoAppCore.Application.Requester.SendPostRequestWithAnimation('/Chemistry/Chemistry/UploadTaskFile', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
        }, null);
    };
    return TaskUpdater;
}());
