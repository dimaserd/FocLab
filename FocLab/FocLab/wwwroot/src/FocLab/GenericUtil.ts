class GenericUtil {
    static GenericUpdateFileByType(fileType: string, link: string, preData: object) {

        var fileInputId = fileType;

        var file_data = $(`#${fileInputId}`).prop('files');

        if (file_data == null || file_data.length == 0) {
            ToastrWorker.ShowError("Файл для загрузки не выбран")
            return;
        }

        Requester.UploadFilesToServer(fileInputId, x => {

            var t = x as GenericBaseApiResponse<Array<number>>;

            if (t.IsSucceeded) {

                var data = preData;
                preData["FileId"] = t.ResponseObject[0];

                Requester.SendPostRequestWithAnimation(link, data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
            }
            else {
                ToastrWorker.ShowError(t.Message);
            }
        }, null);
    }
}