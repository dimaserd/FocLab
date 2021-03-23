var DayTasksWorker = (function () {
    function DayTasksWorker() {
    }
    DayTasksWorker.Constructor = function (props) {
        this.Tasks = props.Tasks;
        this.IsAdmin = props.IsAdmin;
        this.User = props.User;
        this.SearchModel = props.SearchModel;
        this.OpenTaskId = props.OpenTaskId;
        this.MyUserId = props.MyUserId;
        this.CurrentTaskId = null;
        this.CurrentTask = null;
        this.Drawer = new DayTaskDrawer();
    };
    DayTasksWorker.OpenTaskById = function () {
        var taskId = this.OpenTaskId;
        var task = DayTasksWorker.Tasks.find(function (x) { return x.Id === taskId; });
        if (task != null) {
            ScheduleStaticHandlers.ShowDayTaskModal(task.Id);
        }
    };
    DayTasksWorker.SetCurrentTaskId = function (taskId) {
        this.CurrentTaskId = taskId;
        this.CurrentTask = DayTasksWorker.Tasks.find(function (x) { return x.Id === taskId; });
        console.log("SetCurrentTaskId", this.CurrentTask);
    };
    DayTasksWorker.SendNotificationToAdmin = function () {
        CrocoAppCore.Application.ModalWorker.ShowModal("loadingModal");
        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation("/Api/DayTask/SendToAdmin", { Id: DayTasksWorker.CurrentTaskId }, function (x) { return alert(x); }, null);
    };
    DayTasksWorker.GetTasks = function () {
        var data = this.SearchModel;
        data.MonthShift = +this.SearchModel.MonthShift;
        console.log("DayTasksWorker.GetTasks()", data, JSON.stringify(data));
        CrocoAppCore.Application.Requester.Post("/Api/DayTask/GetTasks", data, function (x) {
            DayTasksWorker.Tasks = x;
            DayTasksWorker.Drawer.DrawTasks(x);
            DayTasksWorker.OpenTaskById();
        }, null);
    };
    DayTasksWorker.GetTaskById = function (taskId) {
        return DayTasksWorker.Tasks.find(function (x) { return x.Id === taskId; });
    };
    return DayTasksWorker;
}());
