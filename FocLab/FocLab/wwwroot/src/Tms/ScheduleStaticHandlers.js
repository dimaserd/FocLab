var ScheduleConsts = (function () {
    function ScheduleConsts() {
    }
    ScheduleConsts.FilterPrefix = "filter.";
    return ScheduleConsts;
}());
var ScheduleStaticHandlers = (function () {
    function ScheduleStaticHandlers() {
    }
    ScheduleStaticHandlers.SetInnerHandlers = function () {
        console.log("ScheduleStaticHandlers.SetInnerHandlers()");
        $(".tms-next-month-btn").on("click", function () { return ScheduleStaticHandlers.RedirectToNewUserSchedule(1); });
        $(".tms-prev-month-btn").on("click", function () { return ScheduleStaticHandlers.RedirectToNewUserSchedule(-1); });
        $(".tms-update-task-btn").unbind("click").on("click", function () {
            ScheduleStaticHandlers.updateDayTask();
            CrocoAppCore.Application.ModalWorker.HideModals();
        });
        $(".tms-redirect-to-full").unbind("click").on("click", function () { return ScheduleStaticHandlers.redirectToFullVersion(); });
        $("#tms-create-task-btn").on("click", function (e) {
            console.log("tms-create-task-btn click");
            e.preventDefault();
            e.stopPropagation();
            ScheduleStaticHandlers.createDayTask();
        });
        $(document).on('click', '.tms-profile-link', function (e) {
            console.log("tms-profile-link clicked");
            var authorId = e.target.getAttribute("data-task-author-id");
            ScheduleStaticHandlers.redirectToProfile(authorId);
        });
    };
    ScheduleStaticHandlers.SetHandlers = function () {
        $(".tms-show-task-modal").unbind("click").click(function (e) {
            e.preventDefault();
            e.stopPropagation();
            var taskId = $(e.target).data("task-id");
            ScheduleStaticHandlers.ShowDayTaskModal(taskId);
        });
        $(".tms-btn-create-task").on("click", function (e) {
            e.preventDefault();
            e.stopPropagation();
            ScheduleStaticHandlers.ShowCreateTaskModal();
        });
    };
    ScheduleStaticHandlers.OnUsersSelectChanged = function () {
        var data = {
            UserIds: [],
        };
        CrocoAppCore.Application.FormDataHelper.CollectDataByPrefix(data, ScheduleConsts.FilterPrefix);
        var currentLength = ScheduleStaticHandlers.Filter.UserIds == null
            ? 0
            : ScheduleStaticHandlers.Filter.UserIds.length;
        if (data.UserIds.length !== currentLength) {
            ScheduleStaticHandlers.RedirectToNewUserSchedule(0);
        }
    };
    ScheduleStaticHandlers.RedirectToNewUserSchedule = function (monthShift) {
        console.log("RedirectToNewUserSchedule", monthShift);
        var data = {
            UserIds: [],
        };
        CrocoAppCore.Application.FormDataHelper.CollectDataByPrefix(data, ScheduleConsts.FilterPrefix);
        var nData = {
            UserIds: data.UserIds,
            MonthShift: ScheduleStaticHandlers.Filter.MonthShift + monthShift
        };
        console.log("ShowUserSchedule.Data", nData);
        var urlParts = [];
        for (var i = 0; i < nData.UserIds.length; i++) {
            urlParts.push("UserIds=" + nData.UserIds[i]);
        }
        if (nData.MonthShift !== 0) {
            urlParts.push("MonthShift=" + nData.MonthShift);
        }
        var url = "/Schedule/Index?" + urlParts.join('&');
        console.log("ShowUserSchedule.Url", url);
        location.href = url;
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
        DatePickerUtils.SetDatePicker("TaskDate1", 'RealTaskDate1');
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
        data.TaskDate = DatePickerUtils.GetDateFromDatePicker("TaskDate");
        CrocoAppCore.Application.Requester.Post("/Api/DayTask/CreateOrUpdate", data, function (resp) {
            if (resp.IsSucceeded) {
                DayTasksWorker.GetTasks();
            }
        }, null);
    };
    ScheduleStaticHandlers.createDayTask = function () {
        console.log('createDayTask clicked');
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
        data.TaskDate = DatePickerUtils.GetDateFromDatePicker("TaskDate1");
        if (data.TaskDate == null || document.getElementById("TaskDate1").value === "") {
            Requester.OnSuccessAnimationHandler({ IsSucceeded: false, Message: "Необходимо указать дату задания" });
            return;
        }
        console.log("createDayTask", data);
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
    return ScheduleStaticHandlers;
}());
ScheduleStaticHandlers.SetInnerHandlers();
ScheduleStaticHandlers.SetHandlers();
