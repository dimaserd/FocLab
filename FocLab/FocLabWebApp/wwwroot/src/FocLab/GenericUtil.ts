class GenericUtil {
    static GenericUpdateFileByType(fileType: string, link: string, preData: object) {

        var fileInputId = fileType;

        var file_data = $(`#${fileInputId}`).prop('files');

        if (file_data == null || file_data.length == 0) {
            CrocoAppCore.ToastrWorker.ShowError("Файл для загрузки не выбран")
            return;
        }

        CrocoAppCore.Application.Requester.UploadFilesToServer(fileInputId, "/Api/FilesDirectory/UploadFiles", (t : IGenericBaseApiResponse<number[]>) => {

            if (t.IsSucceeded) {

                var data = preData;
                preData["FileId"] = t.ResponseObject[0];
                preData["FileType"] = fileType

                CrocoAppCore.Application.Requester.SendPostRequestWithAnimation(link, data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
            }
            else {
                CrocoAppCore.ToastrWorker.ShowError(t.Message);
            }
        }, null);
    }
}