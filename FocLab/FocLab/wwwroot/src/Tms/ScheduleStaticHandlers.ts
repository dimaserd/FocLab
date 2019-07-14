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

class ScheduleStaticHandlers {
    static ShowUserSchedule() {
        var data = {
            UserIds: []
        };

        var t = FormDataHelper.CollectDataByPrefix(data, "filter.") as ShowUserSchedule;

        location.href = `/Schedule/Index?${Requester.GetParams(t)}`;
    }


    static ShowDayTaskModal(taskId: string) {

        let task = DayTasksWorker.GetTaskById(taskId);

        DayTasksWorker.SetCurrentTaskId(taskId);

        TaskModalWorker.InitTask(task, AccountWorker.User.Id);

        FormDataHelper.FillDataByPrefix(task, "task.");

        EditableComponents.InitEditable(document.getElementById("TaskTitle") as HTMLInputElement, () => ScheduleStaticHandlers.updateDayTask());

        EditableComponents.InitEditable(document.getElementById("TaskText") as HTMLInputElement, () => ScheduleStaticHandlers.updateDayTask(), true);

        Utils.SetDatePicker("input[name='task.TaskDate']");

        $("input[name='task.TaskDate']").on('change', function () {
            ScheduleStaticHandlers.updateDayTask();
        });

        ModalWorker.ShowModal("dayTaskModal");
    }

    static ShowCreateTaskModal = () => {
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
                console.log(resp.ResponseObject);
                DayTasksWorker.GetTasks();
            }
        }, null, false);
    }

    static updateDayTask() : void {

        var data = {
            //EstimationSeconds: 0,
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

    static redirectToProfile = (profileId : string) => {
        window.open(`${window.location.origin}/Client/Details/${profileId}`, '_blank');
    }
}