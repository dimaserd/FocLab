var ScheduleConsts = (function () {
    function ScheduleConsts() {
    }
    ScheduleConsts.FilterPrefix = "filter.";
    return ScheduleConsts;
}());
var ScheduleStaticHandlers = (function () {
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
        EventSetter.SetHandlerForClass("tms-redirect-to-full", "click", function () { return ScheduleStaticHandlers.redirectToFullVersion(); });
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
    ScheduleStaticHandlers.OnUsersSelectChanged = function () {
        ScheduleStaticHandlers._countOfChanges++;
        if (ScheduleStaticHandlers._countOfChanges > 1) {
            ScheduleStaticHandlers.ShowUserSchedule();
        }
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
        ScheduleStaticHandlers.InitUserSelect("#usersSelect2");
        CrocoAppCore.Application.FormDataHelper.FillDataByPrefix(data, "create.");
        Utils.SetDatePicker("input[name='create.TaskDate']", '0');
        CrocoAppCore.Application.ModalWorker.ShowModal("createDayTaskModal");
    };
    ScheduleStaticHandlers.InitUserSelect = function (selector) {
        $(selector).select2({
            placeholder: ScheduleWorker.Resources.SelectUser,
            language: {
                "noResults": function () {
                    return ScheduleWorker.Resources.UserNotFound;
                }
            },
            data: ScheduleWorker.Users.map(function (t) { return ({
                id: t.Id,
                text: t.Email,
                avatarId: t.AvatarFileId
            }); }),
            templateSelection: ScheduleWorker.formatStateSelection,
            templateResult: ScheduleWorker.formatStateResult,
            escapeMarkup: function (markup) {
                return markup;
            }
        });
    };
    ScheduleStaticHandlers.updateComment = function (commentId) {
        var data = {
            Comment: ""
        };
        CrocoAppCore.Application.FormDataHelper.CollectDataByPrefix(data, "edit.");
        var m = {
            Comment: data.Comment,
            DayTaskCommentId: commentId
        };
        CrocoAppCore.Application.Requester.Post("/Api/DayTask/Comments/Update", m, function (resp) {
            if (resp.IsSucceeded) {
                TaskModalWorker.DrawComments("Comments", resp.ResponseObject);
                DayTasksWorker.GetTasks();
            }
        }, null);
    };
    ScheduleStaticHandlers.addComment = function () {
        var data = {
            DayTaskId: "",
            Comment: ""
        };
        CrocoAppCore.Application.FormDataHelper.CollectDataByPrefix(data, "");
        CrocoAppCore.Application.Requester.Post("/Api/DayTask/Comments/Add", data, function (resp) {
            if (resp.IsSucceeded) {
                TaskModalWorker.DrawComments("Comments", resp.ResponseObject);
                DayTasksWorker.GetTasks();
            }
        }, null);
    };
    ScheduleStaticHandlers.redirectToFullVersion = function () {
        var data = { Id: "" };
        CrocoAppCore.Application.FormDataHelper.CollectDataByPrefix(data, "task.");
        window.open(window.location.origin + "/Schedule/Task/" + data.Id, '_blank');
    };
    ScheduleStaticHandlers.updateDayTask = function () {
        var data = {
            TaskText: "",
            TaskTitle: "",
            AssigneeUserId: "",
            Id: "",
            TaskComment: "",
            TaskDate: new Date(),
            TaskReview: "",
            TaskTarget: ""
        };
        CrocoAppCore.Application.FormDataHelper.CollectDataByPrefix(data, "task.");
        data.TaskDate = Utils.GetDateFromDatePicker("TaskDate");
        CrocoAppCore.Application.Requester.Post("/Api/DayTask/CreateOrUpdate", data, function (resp) {
            if (resp.IsSucceeded) {
                DayTasksWorker.GetTasks();
            }
        }, null);
    };
    ScheduleStaticHandlers.createDayTask = function () {
        var data = {
            Id: "",
            TaskText: "",
            TaskTitle: "",
            AssigneeUserId: "",
            TaskComment: "",
            TaskDate: new Date(),
            TaskReview: "",
            TaskTarget: ""
        };
        CrocoAppCore.Application.FormDataHelper.CollectDataByPrefix(data, "create.");
        data.TaskDate = Utils.GetDateFromDatePicker("TaskDate1");
        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation("/Api/DayTask/CreateOrUpdate", data, function (resp) {
            if (resp.IsSucceeded) {
                ScheduleStaticHandlers.hideCreateModal();
            }
        }, null);
    };
    ScheduleStaticHandlers.hideCreateModal = function () {
        $("#createDayTaskModal").modal("hide");
        DayTasksWorker.GetTasks();
    };
    ScheduleStaticHandlers.redirectToProfile = function (profileId) {
        window.open(window.location.origin + "/Client/Details/" + profileId, '_blank');
    };
    ScheduleStaticHandlers._countOfChanges = 0;
    return ScheduleStaticHandlers;
}());
ScheduleStaticHandlers.SetHandlers();
