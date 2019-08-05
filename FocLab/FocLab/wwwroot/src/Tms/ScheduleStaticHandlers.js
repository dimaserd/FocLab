var ScheduleConsts = /** @class */ (function () {
    function ScheduleConsts() {
    }
    /**
     * Префикс для собирания модели фильтра
     */
    ScheduleConsts.FilterPrefix = "filter.";
    return ScheduleConsts;
}());
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
        var dataFilter = FormDataHelper.CollectDataByPrefix(data, ScheduleConsts.FilterPrefix);
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
        var t = FormDataHelper.CollectDataByPrefix(data, ScheduleConsts.FilterPrefix);
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
                TaskModalWorker.DrawComments("Comments", resp.ResponseObject);
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
                TaskModalWorker.DrawComments("Comments", resp.ResponseObject);
                DayTasksWorker.GetTasks();
            }
        }, null, false);
    };
    ScheduleStaticHandlers.updateDayTask = function () {
        var data = {
            TaskText: "",
            TaskTitle: "",
            AssigneeUserId: "",
            Id: "",
            TaskComment: "",
            TaskDate: "",
            TaskReview: "",
            TaskTarget: ""
        };
        data = FormDataHelper.CollectDataByPrefix(data, "task.");
        data.TaskDate = Utils.GetDateFromDatePicker("TaskDate");
        Requester.SendAjaxPost("/Api/DayTask/CreateOrUpdate", data, function (resp) {
            if (resp.IsSucceeded) {
                DayTasksWorker.GetTasks();
            }
        }, null, false);
    };
    ScheduleStaticHandlers.createDayTask = function () {
        var data = {
            Id: "",
            TaskText: "",
            TaskTitle: "",
            AssigneeUserId: "",
            TaskComment: "",
            TaskDate: "",
            TaskReview: "",
            TaskTarget: ""
        };
        data = FormDataHelper.CollectDataByPrefix(data, "create.");
        data.TaskDate = Utils.GetDateFromDatePicker("TaskDate1");
        Requester.SendAjaxPost("/Api/DayTask/CreateOrUpdate", data, function (resp) {
            ToastrWorker.HandleBaseApiResponse(resp);
            if (resp.IsSucceeded) {
                ScheduleStaticHandlers.hideCreateModal();
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
