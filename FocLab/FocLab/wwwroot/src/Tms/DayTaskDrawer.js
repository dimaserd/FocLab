var DayTaskDrawer = /** @class */ (function () {
    function DayTaskDrawer() {
    }
    DayTaskDrawer.prototype.DrawTasks = function (tasks, isAdmin) {
        this.ClearTasks();
        for (var i = 0; i < tasks.length; i++) {
            var task = tasks[i];
            this.AddTaskToDate(task);
        }
        if (isAdmin) {
            this.AddAdminActions();
        }
        ScheduleStaticHandlers.SetHandlers();
    };
    DayTaskDrawer.prototype.AddTaskToDate = function (task) {
        var dateTrailed = moment(task.TaskDate).format("DD.MM.YYYY");
        var elem = document.querySelector("[data-date='" + dateTrailed + "']");
        $(elem).children(".no-tasks-text").hide();
        var toAdd = document.createElement("div");
        toAdd.innerHTML = "<a class=\"event d-block p-1 pl-2 pr-2 mb-1 rounded text-truncate small bg-primary text-white tms-show-task-modal\" data-task-id=\"" + task.Id + "\" title=\"" + task.TaskTitle + "\">\n                                        " + task.TaskTitle + "\n                                        <span class=\"float-right\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"" + task.AssigneeUser.Email + "\">\n                                            " + ColorAvatarInitor.InitColorForAvatar(task) + "\n                                        </span>\n                                   </a>";
        elem.appendChild(toAdd);
    };
    DayTaskDrawer.prototype.AddAdminActions = function () {
        var elem = document.getElementById("createTaskBtn");
        elem.innerHTML = '';
        var toAdd = document.createElement("div");
        toAdd.innerHTML = "<a class=\"btn float-right pl-2 pr-2 mb-1 rounded text-truncate bg-success text-white d-none d-lg-inline tms-btn-create-task\">\n                        <i class=\"fas fa-plus-circle fa-fw\" style=\"font-size: 0.8rem;\"></i> \u0421\u043E\u0437\u0434\u0430\u0442\u044C \u0437\u0430\u0434\u0430\u043D\u0438\u0435\n                    </a>\n                    <a class=\"btn float-right pl-2 pr-2 mb-1 rounded text-truncate bg-success text-white  d-inline d-lg-none tms-btn-create-task\">\n                        <i class=\"fas fa-plus-circle fa-fw\" style=\"font-size: 0.8rem;\"></i> \u0421\u043E\u0437\u0434\u0430\u0442\u044C\n                    </a>";
        elem.appendChild(toAdd);
    };
    //Удаляет все нарисованные задания на календаре
    DayTaskDrawer.prototype.ClearTasks = function () {
        var paras = document.getElementsByClassName("event");
        while (paras[0]) {
            paras[0].parentNode.removeChild(paras[0]);
        }
    };
    return DayTaskDrawer;
}());
