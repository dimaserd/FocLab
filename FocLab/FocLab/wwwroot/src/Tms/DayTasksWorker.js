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
    };
    DayTasksWorker.SendNotificationToAdmin = function () {
        ModalWorker.ShowModal("loadingModal");
        Requester.SendPostRequestWithAnimation("/Api/DayTask/SendToAdmin", { Id: DayTasksWorker.CurrentTaskId }, function (x) { return alert(x); }, null);
    };
    DayTasksWorker.GetTasks = function () {
        var _this = this;
        Requester.SendAjaxPost("/Api/DayTask/GetTasks", this.SearchModel, function (x) {
            _this.Tasks = x;
            _this.Drawer.DrawTasks(DayTasksWorker.Tasks, true);
            _this.OpenTaskById();
        }, null, false);
    };
    DayTasksWorker.GetTaskById = function (taskId) {
        return DayTasksWorker.Tasks.find(function (x) { return x.Id === taskId; });
    };
    return DayTasksWorker;
}());
