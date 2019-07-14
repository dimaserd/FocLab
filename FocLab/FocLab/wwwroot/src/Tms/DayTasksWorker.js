var DayTasksWorker = /** @class */ (function () {
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
        var task = this.Tasks.find(function (x) { return x.Id === taskId; });
        if (task != null) {
            //открываю модал по заданию полученному из ссылки
            ScheduleStaticHandlers.ShowDayTaskModal(task.Id);
        }
    };
    DayTasksWorker.SetCurrentTaskId = function (taskId) {
        this.CurrentTaskId = taskId;
        this.CurrentTask = this.Tasks.find(function (x) { return x.Id === taskId; });
    };
    DayTasksWorker.SendNotificationToAdmin = function () {
        ModalWorker.ShowModal("loadingModal");
        Requester.SendPostRequestWithAnimation("/Api/DayTask/SendToAdmin", { Id: this.CurrentTaskId }, function (x) { return alert(x); }, null);
    };
    DayTasksWorker.GetTasks = function () {
        var _this = this;
        Requester.SendAjaxPost("/Api/DayTask/GetTasks", this.SearchModel, function (x) {
            _this.Tasks = x;
            _this.Drawer.DrawTasks(_this.Tasks, true);
            _this.OpenTaskById();
        }, null, false);
    };
    DayTasksWorker.GetTaskById = function (taskId) {
        return this.Tasks.find(function (x) { return x.Id === taskId; });
    };
    return DayTasksWorker;
}());
