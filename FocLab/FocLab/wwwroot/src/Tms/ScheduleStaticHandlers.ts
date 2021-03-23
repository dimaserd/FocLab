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
}

class ScheduleConsts {
    /**
     * Префикс для собирания модели фильтра
     */
    static FilterPrefix: string = "filter.";
}

class ScheduleStaticHandlers {

    static Filter: UserScheduleSearchModel;

    static SetInnerHandlers(): void {
        console.log("ScheduleStaticHandlers.SetInnerHandlers()");
        $(".tms-next-month-btn").on("click", () => ScheduleStaticHandlers.RedirectToNewUserSchedule(1));
        $(".tms-prev-month-btn").on("click", () => ScheduleStaticHandlers.RedirectToNewUserSchedule(-1));

        $(".tms-update-task-btn").unbind("click").on("click", () => {
            ScheduleStaticHandlers.updateDayTask();
            CrocoAppCore.Application.ModalWorker.HideModals();
        });

        $(".tms-redirect-to-full").unbind("click").on("click", () => ScheduleStaticHandlers.redirectToFullVersion());
        $("#tms-create-task-btn").on("click", e => {
            console.log("tms-create-task-btn click");
            e.preventDefault();
            e.stopPropagation();
            ScheduleStaticHandlers.createDayTask();
        });
        
        $(document).on('click', '.tms-profile-link', function (e) {
            console.log("tms-profile-link clicked");

            var authorId = (e.target as Element).getAttribute("data-task-author-id");

            ScheduleStaticHandlers.redirectToProfile(authorId);
        });
    }

    static SetHandlers(): void {

        $(".tms-show-task-modal").unbind("click").click(e => {
            e.preventDefault();
            e.stopPropagation();
            var taskId = $(e.target).data("task-id") as string;
            ScheduleStaticHandlers.ShowDayTaskModal(taskId);
        });

        $(".tms-btn-create-task").on("click", e => {
            e.preventDefault();
            e.stopPropagation();
            ScheduleStaticHandlers.ShowCreateTaskModal();
        });
    }

    static OnUsersSelectChanged(): void {

        var data = {
            UserIds: [],
        };

        CrocoAppCore.Application.FormDataHelper.CollectDataByPrefix(data, ScheduleConsts.FilterPrefix);

        var currentLength = ScheduleStaticHandlers.Filter.UserIds == null
            ? 0
            : ScheduleStaticHandlers.Filter.UserIds.length; 

        // Так как в первый раз метод будет задействован, при установке данных FormDataHelper
        //страница не должна перезагрузится, а только на следующие разы когда жто изменит пользователь
        if (data.UserIds.length !== currentLength) {
            ScheduleStaticHandlers.RedirectToNewUserSchedule(0);
        }
    }

    static RedirectToNewUserSchedule(monthShift: number) : void {

        console.log("RedirectToNewUserSchedule", monthShift);

        var data = {
            UserIds: [],
        };

        CrocoAppCore.Application.FormDataHelper.CollectDataByPrefix(data, ScheduleConsts.FilterPrefix);

        var nData = {
            UserIds: data.UserIds,
            MonthShift: ScheduleStaticHandlers.Filter.MonthShift + monthShift
        }

        console.log("ShowUserSchedule.Data", nData);

        let urlParts:string[] = [];

        for (let i = 0; i < nData.UserIds.length; i++) {
            urlParts.push(`UserIds=${nData.UserIds[i]}`);
        }

        if (nData.MonthShift !== 0) {
            urlParts.push(`MonthShift=${nData.MonthShift}`);
        }
        
        var url = `/Schedule/Index?${urlParts.join('&')}`;

        console.log("ShowUserSchedule.Url", url);
        location.href = url;
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

        DatePickerUtils.SetDatePicker("TaskDate1", 'RealTaskDate1');

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
        data.TaskDate = DatePickerUtils.GetDateFromDatePicker("TaskDate");

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

        if ((document.getElementById("TaskDate1") as HTMLInputElement).value === "") {

            Requester.OnSuccessAnimationHandler({ IsSucceeded: false, Message: "Необходимо указать дату задания" });
            return;
        }

        data.TaskDate = DatePickerUtils.GetDateFromDatePicker("TaskDate1");

        console.log("createDayTask", data);

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

ScheduleStaticHandlers.SetInnerHandlers();
ScheduleStaticHandlers.SetHandlers();