var DayTaskEditor = /** @class */ (function () {
    function DayTaskEditor() {
    }
    DayTaskEditor.UpdateHtmlProperties = function (data) {
        ModalWorker.ShowModal("loadingModal");
        Requester.SendPostRequestWithAnimation('/Api/DayTask/Update', data, function (x) {
            if (x.IsSucceeded) {
                DayTasksWorker.GetTasks();
            }
        }, null);
    };
    return DayTaskEditor;
}());