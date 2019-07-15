var ScheduleStaticHandlers = /** @class */ (function () {
    function ScheduleStaticHandlers() {
    }
    ScheduleStaticHandlers.SetHandlers = function () {
        EventSetter.SetHandlerForClass("tms-btn-create-task", "click", function () { return ScheduleStaticHandlers.ShowCreateTaskModal(); });
        EventSetter.SetHandlerForClass("tms-show-task-modal", "click", function (x) {
            var taskId = $(x.target).data("task-id");
            ScheduleStaticHandlers.ShowDayTaskModal(taskId);
        });
    };
    ScheduleStaticHandlers.ShowUserSchedule = function () {
        var data = {
            UserIds: []
        };
        var t = FormDataHelper.CollectDataByPrefix(data, "filter.");
        location.href = "/Schedule/Index?" + Requester.GetParams(t);
    };
    ScheduleStaticHandlers.ShowDayTaskModal = function (taskId) {
        var task = DayTasksWorker.GetTaskById(taskId);
        DayTasksWorker.SetCurrentTaskId(taskId);
        TaskModalWorker.InitTask(task, AccountWorker.User.Id);
        FormDataHelper.FillDataByPrefix(task, "task.");
        EditableComponents.InitEditable(document.getElementById("TaskTitle"), function () { return ScheduleStaticHandlers.updateDayTask(); });
        EditableComponents.InitEditable(document.getElementById("TaskText"), function () { return ScheduleStaticHandlers.updateDayTask(); }, true);
        Utils.SetDatePicker("input[name='task.TaskDate']");
        $("input[name='task.TaskDate']").on('change', function () {
            ScheduleStaticHandlers.updateDayTask();
        });
        ModalWorker.ShowModal("dayTaskModal");
    };
    ScheduleStaticHandlers.ShowCreateTaskModal = function () {
        var data = {
            TaskDate: "",
            TaskText: "",
            TaskTitle: ""
        };
        FormDataHelper.FillDataByPrefix(data, "create.");
        Utils.SetDatePicker("input[name='create.TaskDate']", '0');
        ModalWorker.ShowModal("createDayTaskModal");
    };
    ScheduleStaticHandlers.updateComment = function (commentId) {
        var data = {
            Comment: ""
        };
        data = FormDataHelper.CollectDataByPrefix(data, "edit.");
        var m = {
            Comment: data.Comment,
            DayTaskCommentId: commentId
        };
        Requester.SendAjaxPost("/Api/DayTask/Comments/Update", m, function (resp) {
            if (resp.IsSucceeded) {
                TaskModalWorker.DrawComments("Comments", AccountWorker.User.Id, resp.ResponseObject);
                DayTasksWorker.GetTasks();
            }
        }, null, false);
    };
    ScheduleStaticHandlers.addComment = function () {
        var data = {
            DayTaskId: "",
            Comment: ""
        };
        data = FormDataHelper.CollectData(data);
        Requester.SendAjaxPost("/Api/DayTask/Comments/Add", data, function (resp) {
            if (resp.IsSucceeded) {
                TaskModalWorker.DrawComments("Comments", AccountWorker.User.Id, resp.ResponseObject);
                console.log(resp.ResponseObject);
                DayTasksWorker.GetTasks();
            }
        }, null, false);
    };
    ScheduleStaticHandlers.updateDayTask = function () {
        var data = {
            //EstimationSeconds: 0,
            TaskText: "",
            TaskTitle: "",
            AssigneeUserId: ""
        };
        data = FormDataHelper.CollectDataByPrefix(data, "task.");
        var m = {
            Id: document.getElementsByName('DayTaskId')[0].value,
            TaskDate: Utils.GetDateFromDatePicker("TaskDate"),
            TaskText: data.TaskText,
            TaskTitle: data.TaskTitle,
            AssigneeUserId: data.AssigneeUserId
        };
        Requester.SendAjaxPost("/Api/DayTask/Update", m, function (resp) {
            if (resp.IsSucceeded) {
                DayTasksWorker.GetTasks();
            }
        }, null, false);
    };
    ScheduleStaticHandlers.createDayTask = function () {
        var data = {
            TaskText: "",
            TaskTitle: "",
            AssigneeUserId: ""
        };
        data = FormDataHelper.CollectDataByPrefix(data, "create.");
        var m = {
            TaskText: data.TaskText,
            TaskTitle: data.TaskTitle,
            AssigneeUserId: data.AssigneeUserId,
            TaskDate: Utils.GetDateFromDatePicker("TaskDate1")
        };
        Requester.SendAjaxPost("/Api/DayTask/Create", m, function (resp) {
            if (resp.IsSucceeded) {
                ScheduleStaticHandlers.hideCreateModal();
            }
            else {
                ToastrWorker.HandleBaseApiResponse(resp);
            }
        }, null, false);
    };
    ScheduleStaticHandlers.hideCreateModal = function () {
        $("#createDayTaskModal").modal("hide");
        DayTasksWorker.GetTasks();
    };
    ScheduleStaticHandlers.redirectToProfile = function (profileId) {
        window.open(window.location.origin + "/Client/Details/" + profileId, '_blank');
    };
    return ScheduleStaticHandlers;
}());
ScheduleStaticHandlers.SetHandlers();
