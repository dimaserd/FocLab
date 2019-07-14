class DayTaskEditor {
    static UpdateHtmlProperties(data) {

        ModalWorker.ShowModal("loadingModal");

        Requester.SendPostRequestWithAnimation('/Api/DayTask/Update', data, x => {
            if (x.IsSucceeded) {
                DayTasksWorker.GetTasks();
            }
        }, null);
    }
}