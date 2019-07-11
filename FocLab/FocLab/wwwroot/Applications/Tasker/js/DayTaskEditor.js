class DayTaskEditor {
    constructor() {
        this.setHandlers();
    }

    setHandlers() {

        this.UpdateHtmlProperties = function (data) {

            ModalWorker.ShowModal("loadingModal");

            Requester.SendPostRequestWithAnimation('/Api/DayTask/Update', data, x => {
                if (x.IsSucceeded) {
                    dayTasksWorker.GetTasks();
                }
            });
        }
    }
}