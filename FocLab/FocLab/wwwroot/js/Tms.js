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

var ColorAvatarInitor = /** @class */ (function () {
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
        /*Проверяю есть ли аватар у клиента*/
        if (task.AssigneeUser.AvatarFileId === null) {
            var idFound_1 = false;
            var color_1 = "";
            /*Если нет - проверяю, объявлен ли для него цвет*/
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
                /*Добавляю следующий цвет пользователю*/
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

///<reference path="../../../node_modules/moment/moment.d.ts"/>
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
        console.log(dateTrailed);
        var elem = document.querySelector("[data-date='" + dateTrailed + "']");
        $(elem).children(".no-tasks-text").remove();
        console.log(elem, task);
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

var DayTaskEditor = /** @class */ (function () {
    function DayTaskEditor() {
    }
    DayTaskEditor.UpdateHtmlProperties = function (data) {
        ModalWorker.ShowModal("loadingModal");
        Requester.SendPostRequestWithAnimation('/Api/DayTask/Update', data, function (x) {
            if (x.IsSucceeded) {
                DayTasksWorker.GetTasks();
            }
        }, null);
    };
    return DayTaskEditor;
}());

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
        var task = DayTasksWorker.Tasks.filter(function (x) { return x.Id === taskId; })[0];
        if (task != null) {
            //открываю модал по заданию полученному из ссылки
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


var ScheduleStaticHandlers = /** @class */ (function () {
    function ScheduleStaticHandlers() {
    }
    ScheduleStaticHandlers.SetHandlers = function () {
        EventSetter.SetHandlerForClass("tms-next-month-btn", "click", function () { return ScheduleStaticHandlers.ApplyFilter(true); });
        EventSetter.SetHandlerForClass("tms-prev-month-btn", "click", function () { return ScheduleStaticHandlers.ApplyFilter(false); });
        EventSetter.SetHandlerForClass("tms-add-comment-btn", "click", function () { return ScheduleStaticHandlers.addComment(); });
        EventSetter.SetHandlerForClass("tms-profile-link", "click", function (x) {
            var authorId = x.target.getAttribute("data-task-author-id");
            ScheduleStaticHandlers.redirectToProfile(authorId);
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

var ScheduleWorker = /** @class */ (function () {
    function ScheduleWorker() {
    }
    ScheduleWorker.Constructor = function (filter) {
        ScheduleWorker.filter = filter;
        ScheduleWorker.SetUsersSelect();
        ScheduleWorker.Users = [];
    };
    ScheduleWorker.formatStateSelection = function (state) {
        if (!state.id) {
            return state.text;
        }
        var img = "";
        if (state.avatarId) {
            var baseUrl = "/FileCopies/Images/Icon/" + state.avatarId + ".jpg";
            img = "<img src=\"" + baseUrl + "\" class=\"img-max-50\" />";
        }
        var $state = $("<span>" + img + " " + state.text + "<span>&nbsp;</span></span>");
        return $state;
    };
    ScheduleWorker.formatStateResult = function (state) {
        if (!state.id) {
            return state.text;
        }
        var img = "";
        if (state.avatarId) {
            var baseUrl = "/FileCopies/Images/Icon/" + state.avatarId + ".jpg";
            img = "<img src=\"" + baseUrl + "\" class=\"img-max-50\" />";
        }
        var $state = $("<span>" + img + " " + state.text + "<span>&nbsp;</span></span>");
        return $state;
    };
    ;
    ScheduleWorker.SetUsersSelect = function () {
        var _this = this;
        Requester.SendAjaxPost("/Api/User/Get", { Count: null, OffSet: 0 }, function (x) {
            console.log("/Api/User/Get", x);
            ScheduleWorker.Users = x.List;
            $(".usersSelect").select2({
                placeholder: "Выберите пользователя",
                language: {
                    "noResults": function () {
                        return "Пользователь не найден.";
                    }
                },
                data: x.List.map(function (t) { return ({
                    id: t.Id,
                    text: t.Name + " " + t.Email,
                    avatarId: t.AvatarFileId
                }); }),
                templateSelection: ScheduleWorker.formatStateSelection,
                templateResult: ScheduleWorker.formatStateResult,
                escapeMarkup: function (markup) {
                    return markup;
                }
            });
            FormDataHelper.FillDataByPrefix({
                UserIds: ScheduleWorker.filter.UserIds
            }, "filter.");
            $("#usersSelect").val(_this.filter.UserIds).trigger('change.select2');
            $('.select2-selection__rendered img').addClass('m--img-rounded m--marginless m--img-centered');
        }, null, false);
    };
    return ScheduleWorker;
}());

var TaskModalWorker = /** @class */ (function () {
    function TaskModalWorker() {
    }
    TaskModalWorker.InitTask = function (task, accountId) {
        task.TaskDate = new Date(task.TaskDate.toString().split('T')[0].split('-').reverse().join('/'));
        TaskModalWorker.ClearContent();
        document.getElementById("dayTaskModalTitle").innerHTML = task.TaskTitle;
        var avatar = ColorAvatarInitor.InitColorForAvatar(task);
        document.getElementById("Author").innerHTML = "<a class=\"media-left tms-profile-link\" href=\"#\" data-task-author-id=\"" + task.Author.Id + "\">" + avatar + "</a>\n                <a  href=\"#\" data-task-author-id=\"" + task.Author.Id + "\" class=\"tms-profile-link text-semibold media-heading box-inline ml-1 mb-1\">\n                    " + task.Author.Name + " " + task.Author.Email + "\n                </a>";
        document.getElementsByName('DayTaskId')[0].value = task.Id;
        $("#usersSelect1").val(task.AssigneeUser.Id).trigger('change.select2');
        TaskModalWorker.DrawComments("Comments", accountId, task);
        $("#usersSelect1").select2({
            width: '90%'
        });
    };
    TaskModalWorker.DrawComments = function (divId, userId, task) {
        TaskModalWorker.ClearContent();
        var avatar = ColorAvatarInitor.InitColorForAvatar(task);
        var html = "";
        for (var comment in task.Comments) {
            html += "\n          <div class=\"media-block\">\n            <div class=\"media-body\">\n                <div class=\"form-group m-form__group row m--margin-top-10 d-flex justify-content-between align-items-center\">\n                        <div class=\"btn\">\n                            <a href=\"#\" class=\"btn-link btn cursor-pointer tms-profile-link\" data-task-author-id=\"" + task.Author.Id + "\">\n                                " + avatar + "\n                            </a>\n                            <a href=\"#\" data-task-author-id=\"" + task.Author.Id + "\" class=\"text-semibold tms-profile-link\">\n                                " + task.Comments[comment].Author.Name + "\n                            </a>\n                        </div>";
            if (task.Comments[comment].Author.Id == userId) {
                html += "<div class=\"btn\">\n                                    <button style='height:30px; width:30px' data-editable-name=\"btnEditComment\" data-id=\"" + task.Comments[comment].Id + "\"\n                                    class=\"float-right bg-white border-0\" onclick=\"TaskModalWorker.MakeCommentFieldEditable('" + task.Comments[comment].Id + "')\">\n                                        <i class=\"fa fa-pencil-square-o\" aria-hidden=\"true\"></i>\n                                    </button>\n                            </div>\n                        </div>";
            }
            else {
                html += "</div>";
            }
            html += "<div id=\"" + task.Comments[comment].Id + "\" class=\"col-lg-12\">" + task.Comments[comment].Comment + "</div>\n                        <hr>\n                    </div>\n                  </div>";
        }
        ;
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
        var text = document.getElementById("" + commentId).innerHTML;
        $("[data-editable-name='btnEditComment']").attr('hidden', 'hidden');
        document.getElementById("" + commentId).innerHTML = "<textarea class=\"form-control\" name=\"edit.Comment\" rows=\"2\">" + text + "</textarea>\n                <button class=\"btn btn-sm btn-editable float-right m-1\" data-editable-cancel=\"" + commentId + "\" \n                        onclick=\"TaskModalWorker.ResetCommentChanges('" + commentId + "','" + text + "')\">\n                    <i class=\"fas fa-times\"></i>\n                </button>\n\n                <button id=\"updateComment\" class=\"btn btn-sm btn-editable float-right mt-1\" onClick=\"updateComment('" + commentId + "')\">\n                    <i class=\"fas fa-check\"></i>\n                </button>";
        document.querySelector("[data-id=\"" + commentId + "\"]").setAttribute('hidden', 'hidden');
    };
    TaskModalWorker.ResetCommentChanges = function (commentId, text) {
        document.getElementById("" + commentId).innerHTML = "" + text;
        $("[data-editable-name='btnEditComment']").removeAttr('hidden');
    };
    return TaskModalWorker;
}());