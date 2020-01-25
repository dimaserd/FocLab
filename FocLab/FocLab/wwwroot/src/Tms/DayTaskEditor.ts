class DayTaskEditor {
    static UpdateHtmlProperties(data) {

        CrocoAppCore.Application.ModalWorker.ShowModal("loadingModal");

        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation<IBaseApiResponse>('/Api/DayTask/CreateOrUpdate', data, x => {
            if (x.IsSucceeded) {
                DayTasksWorker.GetTasks();
            }
        }, null);
    }
}