var DayTaskEditor = (function () {
    function DayTaskEditor() {
    }
    DayTaskEditor.UpdateHtmlProperties = function (data) {
        CrocoAppCore.Application.ModalWorker.ShowModal("loadingModal");
        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation('/Api/DayTask/CreateOrUpdate', data, function (x) {
            if (x.IsSucceeded) {
                DayTasksWorker.GetTasks();
            }
        }, null);
    };
    return DayTaskEditor;
}());
