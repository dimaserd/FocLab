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

interface UpdateDayTask {
    TaskText: string;
    TaskTitle: string;
    AssigneeUserId: string;
    Id: string;
    TaskDate: string;
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

class ScheduleStaticHandlers {

    static Filter: UserScheduleSearchModel;

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

        var dataFilter = FormDataHelper.CollectDataByPrefix(data, "filter.") as UserScheduleSearchModel;
        dataFilter.MonthShift = isNextMonth ? ScheduleStaticHandlers.Filter.MonthShift + 1 : ScheduleStaticHandlers.Filter.MonthShift - 1;

        return Requester.GetParams(data);
    }

    static ApplyFilter(isNextMonth: boolean) {

        location.href = `/Schedule/Index?${ScheduleStaticHandlers.GetQueryParams(isNextMonth)}`;
    }

    static ShowUserSchedule() {
        var data = {
            UserIds: []
        };

        var t = FormDataHelper.CollectDataByPrefix(data, "filter.") as ShowUserSchedule;

        location.href = `/Schedule/Index?${Requester.GetParams(t)}`;
    }


    static ShowDayTaskModal(taskId: string) {

        DayTasksWorker.SetCurrentTaskId(taskId);
        let task = DayTasksWorker.GetTaskById(taskId);

        TaskModalWorker.ShowDayTaskModal(task);
    }

    static ShowCreateTaskModal() {
        var data = {
            TaskDate: "",
            TaskText: "",
            TaskTitle: ""
        };

        FormDataHelper.FillDataByPrefix(data, "create.");

        Utils.SetDatePicker("input[name='create.TaskDate']", '0');

        ModalWorker.ShowModal("createDayTaskModal");
    }

    static updateComment(commentId : string) {
        var data = {
            Comment: ""
        }
        data = FormDataHelper.CollectDataByPrefix(data, "edit.") as { Comment: string };

        var m: UpdateDayTaskComment = {
            Comment: data.Comment,
            DayTaskCommentId: commentId
        }

        Requester.SendAjaxPost("/Api/DayTask/Comments/Update", m, resp => {
            if (resp.IsSucceeded) {
                TaskModalWorker.DrawComments("Comments", AccountWorker.User.Id, resp.ResponseObject);
                DayTasksWorker.GetTasks();
            }
        }, null, false);
    }

    static addComment() : void {
        var data: AddComment = {
            DayTaskId: "",
            Comment: ""
        }
        data = FormDataHelper.CollectData(data) as AddComment;

        Requester.SendAjaxPost("/Api/DayTask/Comments/Add", data, resp => {
            if (resp.IsSucceeded) {
                TaskModalWorker.DrawComments("Comments", AccountWorker.User.Id, resp.ResponseObject);
                DayTasksWorker.GetTasks();
            }
        }, null, false);
    }

    static updateDayTask() : void {

        var data = {
            TaskText: "",
            TaskTitle: "",
            AssigneeUserId: ""
        };
        data = FormDataHelper.CollectDataByPrefix(data, "task.") as {
            TaskText: string,
            TaskTitle: string,
            AssigneeUserId: string
        };

        var m: UpdateDayTask = {
            Id: (document.getElementsByName('DayTaskId')[0] as HTMLInputElement).value,
            TaskDate: Utils.GetDateFromDatePicker("TaskDate"),
            TaskText: data.TaskText,
            TaskTitle: data.TaskTitle,
            AssigneeUserId: data.AssigneeUserId
        };

        Requester.SendAjaxPost("/Api/DayTask/Update", m, resp => {
            if (resp.IsSucceeded) {
                DayTasksWorker.GetTasks()
            }
        }, null, false);

    }

    static createDayTask(): void {

        var data = {
            TaskText: "",
            TaskTitle: "",
            AssigneeUserId: ""
        };

        data = FormDataHelper.CollectDataByPrefix(data, "create.") as {
            TaskText: string,
            TaskTitle: string,
            AssigneeUserId: string
        };

        var m: CreateDayTask = {
            TaskText: data.TaskText,
            TaskTitle: data.TaskTitle,
            AssigneeUserId: data.AssigneeUserId,
            TaskDate: Utils.GetDateFromDatePicker("TaskDate1")
        };

        Requester.SendAjaxPost("/Api/DayTask/Create", m, resp => {
            if (resp.IsSucceeded) {
                ScheduleStaticHandlers.hideCreateModal();
            }
            else {
                ToastrWorker.HandleBaseApiResponse(resp);
            } 
        }, null, false);
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