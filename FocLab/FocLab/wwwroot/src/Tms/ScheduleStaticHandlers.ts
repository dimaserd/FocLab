interface ApplicationUserModel {
    Id: string;
    Email: string;
    AvatarFileId: number;
}

interface UpdateDayTaskComment {
    DayTaskCommentId: string;
    Comment: string;
}

interface AddComment {
    DayTaskId: string;
    Comment: string;
}

interface ShowUserSchedule {
    UserIds: Array<string>;
}

interface CreateOrUpdateDayTask {
    Id: string;
    TaskText: string;
    TaskTitle: string;
    AssigneeUserId: string;
    TaskDate: Date;
    TaskTarget: string;
    TaskReview: string;
    TaskComment: string;
}

interface CreateDayTask {
    TaskText: string;
    TaskTitle: string;
    AssigneeUserId: string;
    TaskDate: string;
}

interface UserScheduleSearchModel {

    MonthShift: number;

    UserIds: Array<string>;

    ShowTasksWithNoAssignee: boolean;
}

class ScheduleConsts {
    /**
     * Префикс для собирания модели фильтра
     */
    static FilterPrefix: string = "filter.";
}

class ScheduleStaticHandlers {

    static Filter: UserScheduleSearchModel;

    static _countOfChanges: number = 0;

    static SetHandlers(): void {
        EventSetter.SetHandlerForClass("tms-next-month-btn", "click", () => ScheduleStaticHandlers.ApplyFilter(true));
        EventSetter.SetHandlerForClass("tms-prev-month-btn", "click", () => ScheduleStaticHandlers.ApplyFilter(false));
        EventSetter.SetHandlerForClass("tms-add-comment-btn", "click", () => ScheduleStaticHandlers.addComment());
        EventSetter.SetHandlerForClass("tms-update-comment-btn", "click", x => {
            var commentId = (x.target as Element).getAttribute("data-comment-id");

            ScheduleStaticHandlers.updateComment(commentId);
        })

        EventSetter.SetHandlerForClass("tms-profile-link", "click", x => {
            var authorId = (x.target as Element).getAttribute("data-task-author-id");

            ScheduleStaticHandlers.redirectToProfile(authorId);
        });

        EventSetter.SetHandlerForClass("tms-update-task-btn", "click", () => {
            ScheduleStaticHandlers.updateDayTask();
            ModalWorker.HideModals();
        });

        EventSetter.SetHandlerForClass("tms-redirect-to-full", "click", () => ScheduleStaticHandlers.redirectToFullVersion());
        EventSetter.SetHandlerForClass("tms-create-task-btn", "click", () => ScheduleStaticHandlers.createDayTask());
        EventSetter.SetHandlerForClass("tms-btn-create-task", "click", () => ScheduleStaticHandlers.ShowCreateTaskModal());
        EventSetter.SetHandlerForClass("tms-show-task-modal", "click", x =>
        {
            var taskId = $(x.target).data("task-id") as string;
            ScheduleStaticHandlers.ShowDayTaskModal(taskId);
        })
    }

    static GetQueryParams(isNextMonth: boolean) : string {
        var data = {
            UserIds: []
        };

        var dataFilter = FormDataHelper.CollectDataByPrefix(data, ScheduleConsts.FilterPrefix) as UserScheduleSearchModel;
        dataFilter.MonthShift = isNextMonth ? ScheduleStaticHandlers.Filter.MonthShift + 1 : ScheduleStaticHandlers.Filter.MonthShift - 1;

        return Requester.GetParams(data);
    }

    static ApplyFilter(isNextMonth: boolean) {

        location.href = `/Schedule/Index?${ScheduleStaticHandlers.GetQueryParams(isNextMonth)}`;
    }

    static OnUsersSelectChanged(): void {
        ScheduleStaticHandlers._countOfChanges++;

        // Так как в первый раз метод будет задействован, при установке данных FormDataHelper
        //страница не должна перезагрузится, а только на следующие разы когда жто изменит пользователь
        if (ScheduleStaticHandlers._countOfChanges > 1) {
            ScheduleStaticHandlers.ShowUserSchedule();
        }
    }

    static ShowUserSchedule() : void {
        var data = {
            UserIds: []
        };

        var t = FormDataHelper.CollectDataByPrefix(data, ScheduleConsts.FilterPrefix) as ShowUserSchedule;

        location.href = `/Schedule/Index?${Requester.GetParams(t)}`;
    }


    static ShowDayTaskModal(taskId: string) {

        DayTasksWorker.SetCurrentTaskId(taskId);
        let task = DayTasksWorker.GetTaskById(taskId);

        TaskModalWorker.ShowDayTaskModal(task);
    }

    static ShowCreateTaskModal() :void {
        var data = {
            TaskDate: "",
            TaskText: "",
            TaskTitle: ""
        };

        ScheduleStaticHandlers.InitUserSelect("#usersSelect2");
        
        CrocoAppCore.Application.FormDataHelper.FillDataByPrefix(data, "create.");

        Utils.SetDatePicker("input[name='create.TaskDate']", '0');

        CrocoAppCore.Application.ModalWorker.ShowModal("createDayTaskModal");
    }

    static InitUserSelect(selector: string) : void {
        $(selector).select2({
            placeholder: ScheduleWorker.Resources.SelectUser,

            language: {
                "noResults": function () {
                    return ScheduleWorker.Resources.UserNotFound;
                }
            },

            data: ScheduleWorker.Users.map(t => ({
                id: t.Id,
                text: t.Email,
                avatarId: t.AvatarFileId
            })),

            templateSelection: ScheduleWorker.formatStateSelection,
            templateResult: ScheduleWorker.formatStateResult,

            escapeMarkup: function (markup) {
                return markup;
            }
        });
    }

    static updateComment(commentId : string) : void {
        var data = {
            Comment: ""
        }
        CrocoAppCore.Application.FormDataHelper.CollectDataByPrefix(data, "edit.");

        var m: UpdateDayTaskComment = {
            Comment: data.Comment,
            DayTaskCommentId: commentId
        }

        CrocoAppCore.Application.Requester.Post<IGenericBaseApiResponse<DayTaskModel>>("/Api/DayTask/Comments/Update", m, resp => {
            if (resp.IsSucceeded) {
                TaskModalWorker.DrawComments("Comments", resp.ResponseObject);
                DayTasksWorker.GetTasks();
            }
        }, null);
    }

    static addComment() : void {
        var data: AddComment = {
            DayTaskId: "",
            Comment: ""
        }
        CrocoAppCore.Application.FormDataHelper.CollectDataByPrefix(data, "");

        CrocoAppCore.Application.Requester.Post<IGenericBaseApiResponse<DayTaskModel>>("/Api/DayTask/Comments/Add", data, resp => {
            if (resp.IsSucceeded) {
                TaskModalWorker.DrawComments("Comments", resp.ResponseObject);
                DayTasksWorker.GetTasks();
            }
        }, null);
    }

    static redirectToFullVersion(): void {
        let data = { Id: "" };

        CrocoAppCore.Application.FormDataHelper.CollectDataByPrefix(data, "task.");

        window.open(`${window.location.origin}/Schedule/Task/${data.Id}`, '_blank');
    }

    static updateDayTask() : void {

        var data: CreateOrUpdateDayTask = {
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
        data.TaskDate = Utils.GetDateFromDatePicker("TaskDate") as Date;

        CrocoAppCore.Application.Requester.Post<IBaseApiResponse>("/Api/DayTask/CreateOrUpdate", data, resp => {
            if (resp.IsSucceeded) {
                DayTasksWorker.GetTasks()
            }
        }, null);
    }

    static createDayTask(): void {

        var data: CreateOrUpdateDayTask = {
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

        data.TaskDate = Utils.GetDateFromDatePicker("TaskDate1") as Date;

        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation<IBaseApiResponse>("/Api/DayTask/CreateOrUpdate", data, resp => {
            
            if (resp.IsSucceeded) {
                ScheduleStaticHandlers.hideCreateModal();
            }
        }, null);
    }

    static hideCreateModal() : void {
        $("#createDayTaskModal").modal("hide");
        DayTasksWorker.GetTasks();
    }

    static redirectToProfile(profileId : string) {
        window.open(`${window.location.origin}/Client/Details/${profileId}`, '_blank');
    }
}

ScheduleStaticHandlers.SetHandlers();