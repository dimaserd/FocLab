class DayTaskEditor {
    static UpdateHtmlProperties(data) {

        ModalWorker.ShowModal("loadingModal");

        Requester.SendPostRequestWithAnimation('/Api/DayTask/CreateOrUpdate', data, x => {
            if (x.IsSucceeded) {
                DayTasksWorker.GetTasks();
            }
        }, null);
    }
}