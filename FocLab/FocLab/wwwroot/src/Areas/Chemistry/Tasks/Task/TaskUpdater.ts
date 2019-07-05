class TaskUpdater {

    TaskId: string;

    constructor(taskId: string) {
        this.TaskId = taskId;
    }

    public UploadUserFile(fileType: string) {

        var file_data = $('#' + fileType).prop('files');

        if (file_data == null || file_data.length == 0) {
            ToastrWorker.ShowError("Файл для загрузки не выбран");
            return;
        }

        Requester.UploadFilesToServer(fileType, x => {
            var fileId: number = x.ResponseObject[0];

            var data = {
                
            };

            Requester.SendPostRequestWithAnimation('/Chemistry/Chemistry/UploadTaskFile', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);

        }, null);
    }
}