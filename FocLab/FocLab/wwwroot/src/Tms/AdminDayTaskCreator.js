var AdminDayTaskCreator = /** @class */ (function () {
    function AdminDayTaskCreator(props) {
        this.AssigneeUserId = props.AssigneeUserId;
        this.TaskDate = "";
    }
    AdminDayTaskCreator.prototype.SetDate = function (date) {
        var a = date.split(".");
        date = a[1] + "." + a[0] + "." + a[2];
        this.TaskDate = date;
    };
    AdminDayTaskCreator.prototype.ProccessData = function (data) {
        data.AssigneeUserId = this.AssigneeUserId;
        data.TaskDate = this.TaskDate;
        return data;
    };
    AdminDayTaskCreator.prototype.CreateDayTask = function (data) {
        data = this.ProccessData(data);
        Requester.SendPostRequestWithAnimation("/Api/DayTask/Create", data, function (x) {
            if (x.IsSucceeded) {
                DayTasksWorker.GetTasks();
            }
        }, null);
    };
    AdminDayTaskCreator.prototype.EditDayTask = function (data) {
        Requester.SendPostRequestWithAnimation("/Api/DayTask/Update", data, function (x) {
            if (x.IsSucceeded) {
                DayTasksWorker.GetTasks();
            }
        }, null);
    };
    return AdminDayTaskCreator;
}());
