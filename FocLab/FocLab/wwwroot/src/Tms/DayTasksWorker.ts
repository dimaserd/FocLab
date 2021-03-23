interface DayTasksWorkerProps {
    Tasks: Array<DayTaskModel>;
    IsAdmin: boolean;
    User: any;
    SearchModel: any;
    OpenTaskId: string;
    MyUserId: string;
}

class DayTasksWorker {

    static Tasks: DayTaskModel[];
    static IsAdmin: boolean;
    static User: any;
    static SearchModel: any;
    static OpenTaskId: string;
    static MyUserId: string;
    static CurrentTaskId: string;
    static CurrentTask: DayTaskModel;

    static Drawer: DayTaskDrawer;

    static Constructor(props: DayTasksWorkerProps) {
        this.Tasks = props.Tasks;
        this.IsAdmin = props.IsAdmin;
        this.User = props.User;
        this.SearchModel = props.SearchModel;

        this.OpenTaskId = props.OpenTaskId;

        this.MyUserId = props.MyUserId;

        this.CurrentTaskId = null;
        this.CurrentTask = null;

        this.Drawer = new DayTaskDrawer();
    }

    static OpenTaskById() : void {

        var taskId = this.OpenTaskId;

        const task = DayTasksWorker.Tasks.find(x => x.Id === taskId);

        if (task != null) {
            //открываю модал по заданию полученному из ссылки
            ScheduleStaticHandlers.ShowDayTaskModal(task.Id);
        }
    }

    static SetCurrentTaskId(taskId: string) {
        this.CurrentTaskId = taskId;

        this.CurrentTask = DayTasksWorker.Tasks.find(x => x.Id === taskId);
        console.log("SetCurrentTaskId", this.CurrentTask);
    }

    static SendNotificationToAdmin() {

        CrocoAppCore.Application.ModalWorker.ShowModal("loadingModal");

        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation("/Api/DayTask/SendToAdmin", { Id: DayTasksWorker.CurrentTaskId }, x => alert(x), null);
    }

    static GetTasks(): void {

        var data = this.SearchModel;
        data.MonthShift = +this.SearchModel.MonthShift;

        console.log("DayTasksWorker.GetTasks()", data, JSON.stringify(data));

        CrocoAppCore.Application.Requester.Post<DayTaskModel[]>("/Api/DayTask/GetTasks", data, x => {
            DayTasksWorker.Tasks = x;
            DayTasksWorker.Drawer.DrawTasks(x);
            DayTasksWorker.OpenTaskById();
        }, null);
    }

    static GetTaskById(taskId: string): DayTaskModel {
        return DayTasksWorker.Tasks.find(x => x.Id === taskId);
    }
}