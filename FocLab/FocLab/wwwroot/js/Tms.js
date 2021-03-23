var ColorAvatarInitor = (function () {
    function ColorAvatarInitor() {
    }
    ColorAvatarInitor._avatarsStorage = [];
    ColorAvatarInitor._colors = [
        "#007bff",
        "#6610f2",
        "#6f42c1",
        "#e83e8c",
        "#dc3545",
        "#fd7e14",
        "#ffc107",
        "#28a745",
        "#20c997",
        "#17a2b8",
        "#fff",
        "#6c757d"
    ];
    ColorAvatarInitor.InitColorForAvatar = function (task) {
        if (task.AssigneeUser.AvatarFileId === null) {
            var idFound_1 = false;
            var color_1 = "";
            ColorAvatarInitor._avatarsStorage.forEach(function (x) {
                if (x.id == task.AssigneeUser.Id) {
                    idFound_1 = true;
                    color_1 = x.color;
                }
            });
            if (idFound_1) {
                return "<span class='avatar-circle' style='background-color:" + color_1 + "'></span>";
            }
            else {
                var count = ColorAvatarInitor._avatarsStorage.length % ColorAvatarInitor._colors.length;
                ColorAvatarInitor._avatarsStorage.push({ 'id': task.AssigneeUser.Id, 'color': ColorAvatarInitor._colors[count + 1] });
                return "<span class='avatar-circle' style='background-color:" + ColorAvatarInitor._colors[count + 1] + "'></span>";
            }
        }
        else {
            return "<img style='height:30px;width:30px' class='rounded-circle' src='/FileCopies/Images/Icon/" + task.AssigneeUser.AvatarFileId + ".jpg'/>";
        }
    };
    return ColorAvatarInitor;
}());

var DayTaskDrawer = (function () {
    function DayTaskDrawer() {
    }
    DayTaskDrawer.prototype.DrawTasks = function (tasks) {
        if (tasks == null) {
            return;
        }
        this.ClearTasks();
        for (var i = 0; i < tasks.length; i++) {
            var task = tasks[i];
            this.AddTaskToDate(task);
        }
        this.AddAdminActions();
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
    DayTaskDrawer.prototype.ClearTasks = function () {
        var paras = document.getElementsByClassName("event");
        while (paras[0]) {
            paras[0].parentNode.removeChild(paras[0]);
        }
    };
    return DayTaskDrawer;
}());

var DayTaskEditor = (function () {
    function DayTaskEditor() {
    }
    DayTaskEditor.UpdateHtmlProperties = function (data) {
        CrocoAppCore.Application.ModalWorker.ShowModal("loadingModal");
        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation('/Api/DayTask/CreateOrUpdate', data, function (x) {
            if (x.IsSucceeded) {
                DayTasksWorker.GetTasks();
            }
        }, null);
    };
    return DayTaskEditor;
}());

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

var ScheduleWorker_Resx = (function () {
    function ScheduleWorker_Resx() {
        this.SelectUser = "Выберите пользователя";
        this.UserNotFound = "Пользователь не найден.";
    }
    return ScheduleWorker_Resx;
}());
var ScheduleWorker = (function () {
    function ScheduleWorker() {
    }
    ScheduleWorker.Constructor = function (filter) {
        ScheduleWorker.filter = filter;
        ScheduleWorker.SetUsersSelect();
    };
    ScheduleWorker.formatStateSelection = function (state) {
        if (!state.id) {
            return state.text;
        }
        var img = "";
        var showAvatar = false;
        if (state.avatarId) {
            var baseUrl = "/FileCopies/Images/Icon/" + state.avatarId + ".jpg";
            img = showAvatar ? "<img src=\"" + baseUrl + "\" class=\"img-max-50\" />" : "";
        }
        var $state = $("<span>" + img + " " + state.text + "<span>&nbsp;</span></span>");
        return $state;
    };
    ScheduleWorker.formatStateResult = function (state) {
        if (!state.id) {
            return state.text;
        }
        var img = "";
        var showAvatar = false;
        if (state.avatarId) {
            var baseUrl = "/FileCopies/Images/Icon/" + state.avatarId + ".jpg";
            img = showAvatar ? "<img src=\"" + baseUrl + "\" class=\"img-max-50\" />" : "";
        }
        var $state = $("<span>" + img + " " + state.text + "<span>&nbsp;</span></span>");
        return $state;
    };
    ;
    ScheduleWorker.SetUsersSelect = function () {
        var _this = this;
        CrocoAppCore.Application.Requester.Post("/Api/User/Get", { Count: null, OffSet: 0 }, function (x) {
            ScheduleWorker.Users = x.List;
            $("#usersSelect").select2({
                placeholder: ScheduleWorker.Resources.SelectUser,
                language: {
                    "noResults": function () {
                        return ScheduleWorker.Resources.UserNotFound;
                    }
                },
                data: x.List.map(function (t) { return ({
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
            CrocoAppCore.Application.FormDataHelper.FillDataByPrefix({
                UserIds: ScheduleWorker.filter.UserIds
            }, "filter.");
            $("#usersSelect").val(_this.filter.UserIds).trigger('change.select2');
            $('.select2-selection__rendered img').addClass('m--img-rounded m--marginless m--img-centered');
        }, null);
    };
    ScheduleWorker.Users = [];
    ScheduleWorker.Resources = new ScheduleWorker_Resx();
    return ScheduleWorker;
}());

var TaskModalConsts = (function () {
    function TaskModalConsts() {
    }
    TaskModalConsts.UserSelectId = "usersSelect1";
    return TaskModalConsts;
}());
var TaskModalWorker = (function () {
    function TaskModalWorker() {
    }
    TaskModalWorker.ShowDayTaskModal = function (task) {
        TaskModalWorker.InitTask(task);
        CrocoAppCore.Application.FormDataHelper.FillDataByPrefix(task, "task.");
        DatePickerUtils.SetDatePicker("TaskDate", "RealTaskDate");
        DatePickerUtils.SetDateToDatePicker("TaskDate", task.TaskDate);
        var selector = "#" + TaskModalConsts.UserSelectId;
        ScheduleStaticHandlers.InitUserSelect(selector);
        $(selector).val(task.AssigneeUser.Id).trigger('change');
        CrocoAppCore.Application.ModalWorker.ShowModal("dayTaskModal");
    };
    TaskModalWorker.InitTask = function (task) {
        task.TaskDate = moment(new Date(task.TaskDate)).format("DD/MM/YYYY");
        TaskModalWorker.ClearContent();
        document.getElementById("dayTaskModalTitle").innerHTML = task.TaskTitle;
        var avatar = ColorAvatarInitor.InitColorForAvatar(task);
        document.getElementById("Author").innerHTML = "<a class=\"media-left tms-profile-link\" data-task-author-id=\"" + task.Author.Id + "\">" + avatar + "</a>\n                <a data-task-author-id=\"" + task.Author.Id + "\" class=\"tms-profile-link text-semibold media-heading box-inline ml-1 mb-1\">\n                    " + task.Author.Name + " " + task.Author.Email + "\n                </a>";
        document.getElementsByName('DayTaskId')[0].value = task.Id;
        $("#usersSelect1").val(task.AssigneeUser.Id).trigger('change.select2');
        TaskModalWorker.DrawComments("Comments", task);
        $("#usersSelect1").select2({
            width: '100%'
        });
    };
    TaskModalWorker.DrawComments = function (divId, task) {
        TaskModalWorker.ClearContent();
        var userId = AccountWorker.User.Id;
        var avatar = ColorAvatarInitor.InitColorForAvatar(task);
        var html = "<div>";
        for (var comment in task.Comments) {
            html += "\n          <div class=\"media-block\">\n            <div class=\"media-body\">\n                <div class=\"form-group m-form__group row m--margin-top-10 d-flex justify-content-between align-items-center\">\n                        <div>\n                            <a href=\"#\" class=\"btn-link btn cursor-pointer tms-profile-link\" data-task-author-id=\"" + task.Author.Id + "\">\n                                " + avatar + "\n                            </a>\n                            <a href=\"#\" data-task-author-id=\"" + task.Author.Id + "\" class=\"text-semibold tms-profile-link\">\n                                " + task.Comments[comment].Author.Name + "\n                            </a>\n                        </div>";
            if (task.Comments[comment].Author.Id == userId) {
                html += "<div>\n                            <button style='height:30px; width:30px' data-editable-name=\"btnEditComment\" data-id=\"" + task.Comments[comment].Id + "\" class=\"float-right bg-white border-0\" onclick=\"TaskModalWorker.MakeCommentFieldEditable('" + task.Comments[comment].Id + "')\">\n                                <i class=\"fa fa-pencil-square-o\" aria-hidden=\"true\"></i>\n                            </button>\n                        </div>\n                        </div>";
            }
            else {
                html += "</div>";
            }
            html += "<div id=\"" + task.Comments[comment].Id + "\">" + task.Comments[comment].Comment + "</div>\n                        \n                    </div>\n                  </div>";
        }
        ;
        html += "</div>";
        document.getElementById(divId).innerHTML = html;
    };
    TaskModalWorker.ClearContent = function () {
        document.getElementsByName("Comment")[0].value = "";
        document.getElementById("dayTaskModalTitle").innerHTML = "";
        var paras = document.getElementsByClassName("media-block");
        while (paras[0]) {
            paras[0].remove();
        }
    };
    TaskModalWorker.MakeCommentFieldEditable = function (commentId) {
        var text = document.getElementById(commentId).innerHTML;
        $("[data-editable-name='btnEditComment']").attr('hidden', 'hidden');
        document.getElementById(commentId).innerHTML = "<textarea class=\"form-control\" name=\"edit.Comment\" rows=\"2\">" + text + "</textarea>\n                <button class=\"btn btn-sm btn-editable float-right m-1\" data-editable-cancel=\"" + commentId + "\" \n                        onclick=\"TaskModalWorker.ResetCommentChanges('" + commentId + "','" + text + "')\">\n                    <i class=\"fas fa-times\"></i>\n                </button>\n\n                <button class=\"btn btn-sm btn-editable float-right mt-1 tms-update-comment-btn\" data-comment-id=\"" + commentId + "\">\n                    <i class=\"fas fa-check\"></i>\n                </button>";
        document.querySelector("[data-id=\"" + commentId + "\"]").setAttribute('hidden', 'hidden');
    };
    TaskModalWorker.ResetCommentChanges = function (commentId, text) {
        document.getElementById("" + commentId).innerHTML = "" + text;
        $("[data-editable-name='btnEditComment']").removeAttr('hidden');
    };
    return TaskModalWorker;
}());

var TmsOnPageInitor = (function () {
    function TmsOnPageInitor() {
    }
    TmsOnPageInitor.Init = function () {
        var _a;
        var filter = window["tms-filter"];
        console.log("TmsOnPageInitor.Init", filter);
        ScheduleWorker.Constructor(filter);
        var props = {
            Tasks: null,
            User: null,
            IsAdmin: false,
            MyUserId: (_a = AccountWorker.User) === null || _a === void 0 ? void 0 : _a.Id,
            OpenTaskId: null,
            SearchModel: filter
        };
        DayTasksWorker.Constructor(props);
        ScheduleStaticHandlers.Filter = filter;
        document.addEventListener("DOMContentLoaded", function () {
            DayTasksWorker.GetTasks();
            $("#usersSelect").select2().on("change", function () { ScheduleStaticHandlers.OnUsersSelectChanged(); });
        });
        $("body").tooltip({ selector: '[data-toggle=tooltip]' });
    };
    return TmsOnPageInitor;
}());
TmsOnPageInitor.Init();