var ScheduleStaticHandlers = /** @class */ (function () {
    function ScheduleStaticHandlers() {
    }
    ScheduleStaticHandlers.SetHandlers = function () {
        EventSetter.SetHandlerForClass("tms-next-month-btn", "click", function () { return ScheduleStaticHandlers.ApplyFilter(true); });
        EventSetter.SetHandlerForClass("tms-prev-month-btn", "click", function () { return ScheduleStaticHandlers.ApplyFilter(false); });
        EventSetter.SetHandlerForClass("tms-add-comment-btn", "click", function () { return ScheduleStaticHandlers.addComment(); });
        EventSetter.SetHandlerForClass("tms-update-comment-btn", "click", function (x) {
            var commentId = x.target.getAttribute("data-comment-id");
            ScheduleStaticHandlers.updateComment(commentId);
        });
        EventSetter.SetHandlerForClass("tms-profile-link", "click", function (x) {
            var authorId = x.target.getAttribute("data-task-author-id");
            ScheduleStaticHandlers.redirectToProfile(authorId);
        });
        EventSetter.SetHandlerForClass("tms-update-task-btn", "click", function () {
            ScheduleStaticHandlers.updateDayTask();
            ModalWorker.HideModals();
        });
        EventSetter.SetHandlerForClass("tms-create-task-btn", "click", function () { return ScheduleStaticHandlers.createDayTask(); });
        EventSetter.SetHandlerForClass("tms-btn-create-task", "click", function () { return ScheduleStaticHandlers.ShowCreateTaskModal(); });
        EventSetter.SetHandlerForClass("tms-show-task-modal", "click", function (x) {
            var taskId = $(x.target).data("task-id");
            ScheduleStaticHandlers.ShowDayTaskModal(taskId);
        });
    };
    ScheduleStaticHandlers.GetQueryParams = function (isNextMonth) {
        var data = {
            UserIds: []
        };
        var dataFilter = FormDataHelper.CollectDataByPrefix(data, "filter.");
        dataFilter.MonthShift = isNextMonth ? ScheduleStaticHandlers.Filter.MonthShift + 1 : ScheduleStaticHandlers.Filter.MonthShift - 1;
        return Requester.GetParams(data);
    };
    ScheduleStaticHandlers.ApplyFilter = function (isNextMonth) {
        location.href = "/Schedule/Index?" + ScheduleStaticHandlers.GetQueryParams(isNextMonth);
    };
    ScheduleStaticHandlers.ShowUserSchedule = function () {
        var data = {
            UserIds: []
        };
        var t = FormDataHelper.CollectDataByPrefix(data, "filter.");
        location.href = "/Schedule/Index?" + Requester.GetParams(t);
    };
    ScheduleStaticHandlers.ShowDayTaskModal = function (taskId) {
        DayTasksWorker.SetCurrentTaskId(taskId);
        var task = DayTasksWorker.GetTaskById(taskId);
        TaskModalWorker.ShowDayTaskModal(task);
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
